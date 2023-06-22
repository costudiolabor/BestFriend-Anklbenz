using System;
using TMPro;
using UnityEngine;

public class InputText : AnimatedView {
	[SerializeField] private TMP_InputField inputField;
	[SerializeField] private TMP_Text placeholderText;

	public event Action ValueChangedEvent;
	private void Awake() =>
		inputField.onValueChanged.AddListener(delegate { ValueChangedEvent?.Invoke();});
	
	public string placeholder {
		get => placeholderText.text;
		set => placeholderText.text = value;
	}
	
	public string value => inputField.text;
}