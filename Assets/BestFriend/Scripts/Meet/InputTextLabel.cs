using TMPro;
using UnityEngine;

public class InputTextLabel : Label<string> {
	[SerializeField] private TMP_InputField inputText;
	public override string value => inputText.text;

	protected override void OnEnable() {
		base.OnEnable();
		inputText.onValueChanged.AddListener(OnValueChanged);
		acceptButton.interactable = !(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
	}

	protected override void OnDisable() {
		base.OnDisable();
		inputText.onValueChanged.RemoveListener(OnValueChanged);
	}

	private void OnValueChanged(string inputValue = "") =>
			acceptButton.interactable = !(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
}