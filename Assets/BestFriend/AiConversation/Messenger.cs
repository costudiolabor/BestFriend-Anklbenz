using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Messenger {
	[SerializeField] private MessageBoard messageBoard;
	[SerializeField] private MessageInput messageInput;
	[SerializeField] private LimitsView limitsView;
	[SerializeField] private Button closeButton;

	private readonly RequestsSender _requestSender = new();
	private readonly LimitsCounter _limitsCounter = new();

	public async UniTask Initialize() {
		var chatSettings = await DatabaseApi.instance.GetChatSettings();
		var currentData = await DatabaseApi.instance.GetUserData();

		_requestSender.Initialize(chatSettings.data, currentData.data);
		_limitsCounter.Initialize(chatSettings.data.freeLimits);
		_limitsCounter.RefreshEvent += OnRefresh;
		closeButton.onClick.AddListener(OnCloseClick);

		await _limitsCounter.Refresh();

		messageInput.SendEvent += SendAsync;
	}

	private async void SendAsync() {
		if (_limitsCounter.isEmpty) return;
		if (messageInput.inputIsEmpty) return;

		var typedMessage = messageInput.inputText;

		messageInput.isEnabled = false;
		messageBoard.CreateUserMessage(typedMessage);

		var aiResponse = await _requestSender.SendRequestAsync(typedMessage);

		messageBoard.CreateCompanionMessage(aiResponse);
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