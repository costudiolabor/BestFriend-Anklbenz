using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessengerView : AnimatedView {
	[SerializeField] private TMP_Text timerToNextText, requestsCountText;
	[SerializeField] private TMP_InputField input;
	[SerializeField] private Button sendButton;

	public MessageBoard messageBoard;

	public event Action SendEvent;
	public string inputText => input.text;
	public string timerText { set => timerToNextText.text = value; }
	public string requestsText { set => requestsCountText.text = value; }
	public bool isSendControlsEnabled {
		get => _isSendControlsEnabled;
		set {
			_isSendControlsEnabled = value;
			input.interactable = value;
		}
	}
	public bool timerVisible {
		set => timerToNextText.gameObject.SetActive(value);
	}
	private bool _isSendControlsEnabled;
	public void ClearInput() => input.text = string.Empty;
	private void Awake() {
		sendButton.onClick.AddListener(SendNotify);
		input.onSubmit.AddListener(delegate { SendNotify(); });
	}
	private void SendNotify() =>
			SendEvent?.Invoke();
}