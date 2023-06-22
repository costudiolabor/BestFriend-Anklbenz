using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AuthView : AnimatedView {
	[SerializeField] private TMP_Text errorMessageText;
	[SerializeField] private TMP_InputField loginInput, passwordInput;
	[SerializeField] private Button acceptButton, backButton, actionOneButton, actionTwoButton;
	public string errorMessage { get => errorMessageText.text; set => errorMessageText.text = value; }
	public string loginText => loginInput.text;
	public string passwordText => passwordInput.text;

	public event Action AcceptEvent, BackEvent, ActionOneEvent, ActionTwoEvent;

	private void Awake() {
		acceptButton.onClick.AddListener(OnAccept);
		backButton.onClick.AddListener(OnBack);
		actionOneButton.onClick.AddListener(ActionOneNotify);
		actionTwoButton?.onClick.AddListener(ActionTwoNotify);
	}

	private void ActionOneNotify() =>
			ActionOneEvent?.Invoke();
	private void ActionTwoNotify() =>
			ActionTwoEvent?.Invoke();
	private void OnBack() =>
			BackEvent?.Invoke();
	private void OnAccept() =>
			AcceptEvent?.Invoke();
}