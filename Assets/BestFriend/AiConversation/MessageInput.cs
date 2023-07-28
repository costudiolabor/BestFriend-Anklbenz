using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageInput : AnimatedView {
	[SerializeField] private TMP_InputField input;
	[SerializeField] private Button sendButton;

	public event Action SendEvent;
	public string inputText => input.text;
	public bool inputIsEmpty => string.IsNullOrWhiteSpace(input.text);

	public bool isEnabled {
		get => _isEnabled;
		set {
			_isEnabled = value;
			input.interactable = value;
		}
	}

	private bool _isEnabled;

	private void Awake() {
		sendButton.onClick.AddListener(SendNotify);
		input.onSubmit.AddListener(delegate { SendNotify(); });
		input.onValueChanged.AddListener(delegate { HideButtonsIfInputEmpty(); });
		HideButtonsIfInputEmpty();
	}

	public void ClearInput() =>
			input.text = string.Empty;
	private void SendNotify() =>
			SendEvent?.Invoke();
	private void HideButtonsIfInputEmpty() =>
			sendButton.gameObject.SetActive(!inputIsEmpty);

}