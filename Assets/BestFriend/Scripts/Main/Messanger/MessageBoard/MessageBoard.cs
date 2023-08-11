using UnityEngine;
using System;

public class MessageBoard : AnimatedView {
	[SerializeField] private Message userMessagePrefab, companionMessagePrefab;
	[SerializeField] private RectTransform messageParent;

	public void CreateUserMessage(string message, string senderName = null) {
		var userMessage = Instantiate(userMessagePrefab, messageParent);
		userMessage.Set(senderName, message, (DateTimeOffset.Now.ToUnixTimeSeconds()));
	}
	public void CreateCompanionMessage(string message, string senderName = null) {
		var companionMessage = Instantiate(companionMessagePrefab, messageParent);
		companionMessage.Set(senderName, message, DateTimeOffset.Now.ToUnixTimeSeconds());
	}
}