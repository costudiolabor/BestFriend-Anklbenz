using UnityEngine;
using System;
using System.Collections.Generic;

public class MessageBoard : AnimatedView {
	[SerializeField] private Message userMessagePrefab, companionMessagePrefab;
	[SerializeField] private MessageDate messageDatePrefab;
	[SerializeField] private RectTransform messageParent;
	private DateTime _last = new();

	public void CreateUserMessage(string message, long unix ) =>
		CreateMessage(userMessagePrefab, message, unix);

	public void CreateCompanionMessage(string message, long unix ) =>
		CreateMessage(companionMessagePrefab, message, unix);
	
	private void CreateMessage(Message prefab, string message, long unix) {
		CreateDateLabelIfNeeded(unix);
		var messageInstance = Instantiate(prefab, messageParent);
		messageInstance.Set(message);
	}

	private void CreateDateLabelIfNeeded(long unix) {
		var messageDate = Utils.UnixTimeToLocalTime(unix);
		if(_last.Date >= messageDate.Date) return;
		
		_last = messageDate;
		CreateDateLabel(messageDate.ToString("M"));
	}

	private void CreateDateLabel(string date) {
		var messageInstance = Instantiate( messageDatePrefab, messageParent);
		messageInstance.Set(date);
	}
}
