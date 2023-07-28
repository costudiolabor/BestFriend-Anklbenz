using System;
using UnityEngine;

[System.Serializable]
public class Messenger {
	[SerializeField] private MessengerView view;
	private readonly RequestsBuilder _requestBuilder = new();
	private LimitsCounter _limitCounter = new();
	private bool _initialized;


	public async void Initialize() {
		if (_initialized) return;

		var chatSettings = await DatabaseApi.instance.GetChatSettings();
		var currentData = await DatabaseApi.instance.GetUserData();
		_requestBuilder.Initialize(chatSettings.data, currentData.data);
		
		_limitCounter.RefreshSuccessEvent += OnLimitsUpdated;
		await _limitCounter.Initialize(chatSettings.data.conversationSettings);
	 	await _limitCounter.Refresh();
		
		view.SendEvent += SendAsync;

		_initialized = true;
	}

	private async void SendAsync() {
	var f =	await DatabaseApi.instance.GetActualLimits();
	
		
		var sendText = view.inputText;
		if (string.IsNullOrWhiteSpace(sendText)) return;
		if (!_limitCounter.hasLimit) return;

		var userMessage = _requestBuilder.CreateUserMessage(sendText);
		var request = _requestBuilder.CreateAiRequest(userMessage);

		view.isSendControlsEnabled = false;
		view.messageBoard.TypeUserMessage(userMessage.content);

		var aiResponseRequest = await DatabaseApi.instance.SendAiRequest(request);
		if (!aiResponseRequest.isSuccess) return;

		await _limitCounter.Refresh();

		var aiResponseMessage = aiResponseRequest.data.choices[0].message;

		_requestBuilder.AddHistory(userMessage);
		_requestBuilder.AddHistory(aiResponseMessage);

		view.messageBoard.TypeCompanionMessage(aiResponseMessage.content);
		view.isSendControlsEnabled = true;
		view.ClearInput();
	}

	public void Open() => view.Open();
	public void Close() => view.Close();

	public async void FixedUpdate_SystemCall() {
		if (!_initialized) return;
		if (_limitCounter.hasMax) return;

		var timeNExtIter = _limitCounter.timeToNextIteration;
		var now = DateTime.UtcNow;
		var timeSpan = _limitCounter.timeToNextIteration.Subtract(DateTime.UtcNow);
		view.requestsText = $"{timeSpan.Minutes} : {timeSpan.Seconds} ";
	
		if (timeSpan.TotalSeconds <= 0)
			await _limitCounter.Refresh();
	}

	private void OnLimitsUpdated() {
		view.requestsText = _limitCounter.requestsAvailable.ToString();
		view.timerVisible = !_limitCounter.hasMax;
	}
}