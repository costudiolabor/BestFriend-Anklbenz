using System;
using TMPro;
using UnityEngine;

public class Message : AnimatedView {
	[SerializeField] private TMP_Text senderText, contentText, timeText;

	public void Set(string content, string sender = "", double time = 0) {
		contentText.text = content;

		if (!string.IsNullOrWhiteSpace(sender))
			senderText.text = sender;
		if (time > 0)
			timeText.text = Utils.UnixTimeToLocalTime(time).ToShortTimeString();
	}
}
