using System;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Enums;

[Serializable]
public class Messenger {
	[SerializeField] private MessageBoard messageBoard;
	[SerializeField] private MessageInput messageInput;
	[SerializeField] private LimitsView limitsView;
	[SerializeField] private Button closeButton;

	private readonly RequestsSender _requestSender = new();
	private readonly LimitsCounter _limitsCounter = new();
	private readonly History _history = new();

	private long currentUnix => ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();

	public async UniTask Initialize(ChatSettings chatSettings, UserData userData) {
		_requestSender.Initialize(chatSettings, userData);
		_limitsCounter.Initialize(chatSettings.freeLimits);
		_limitsCounter.RefreshEvent += OnRefresh;
		closeButton.onClick.AddListener(OnCloseClick);
		LoadHistory();
		await _limitsCounter.Refresh();
      
		messageInput.SendEvent += SendAsync;
	}

	private async void SendAsync() {
		if (_limitsCounter.isEmpty) return;
		if (messageInput.isEmpty) return;

		var typedMessage = messageInput.text;

		messageInput.isEnabled = false;
		messageBoard.CreateUserMessage(typedMessage, currentUnix);
		_history.SaveUserMessage(typedMessage);

		var aiResponse = await _requestSender.SendRequestAsync(typedMessage);

		_history.SaveCompanionMessage(aiResponse);
		messageBoard.CreateCompanionMessage(aiResponse, currentUnix);
		messageInput.ClearInput();
		
		await _limitsCounter.Refresh();

		messageInput.isEnabled = true;
	}

	public async void FixedUpdate_SystemCall() {
		if (!_limitsCounter.isReady || _limitsCounter.isMax) return;

		var timesLeft = TimeSpan.FromMilliseconds(_limitsCounter.timeLeftToNextMilliseconds);

		if (timesLeft.TotalSeconds <= 0)
			await _limitsCounter.Refresh();
		else
			limitsView.timerText = timesLeft.ToString(@"mm\:ss");
	}
	
	private void LoadHistory() {
		var messages = _history.Get();
		
		foreach (var message in messages) {
			switch (message.role) {
				case HistoryMessageType.User:
					messageBoard.CreateUserMessage(message.message, message.unixTime);
					break;
				case HistoryMessageType.Companion:
					messageBoard.CreateCompanionMessage(message.message, message.unixTime);
					break;
			}
		}
	}
	
	private void OnRefresh() {
		limitsView.requestsText = _limitsCounter.requestsAvailable.ToString();
		if (_limitsCounter.isMax)
			limitsView.timerText = "Full";
	}

	private void OnCloseClick() {
		if (messageBoard.isActive)
			messageBoard.Close();
		else
			messageBoard.Open();
	}
}