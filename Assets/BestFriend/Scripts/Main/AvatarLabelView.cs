using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarLabelView : AnimatedView {
	[SerializeField] private TMP_Text nameField;
	[SerializeField] private TMP_Text roleField;
	[SerializeField] private Button avatarPreferencesButton;

	public event Action ClickedEvent;

	public string avatarName {
		get => nameField.text;
		set => nameField.text = value;
	}

	public string avatarRole {
		get => roleField.text;
		set => roleField.text = value;
	}

	private void Awake() {
		avatarPreferencesButton.onClick.AddListener(SettingsNotify);
	}
	private void SettingsNotify() =>
		ClickedEvent?.Invoke();
}