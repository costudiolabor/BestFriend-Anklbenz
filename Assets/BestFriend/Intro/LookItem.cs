using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LookItem : MonoBehaviour {
	[SerializeField] private Toggle toggle;
	[SerializeField] private TMP_Text textField;
	public event Action<bool> ValueChangedEvent;
	public bool isSelected => toggle.isOn;
	public string value => textField.text;

	private void Awake() =>
			toggle.onValueChanged.AddListener(OnValueChanged);

	private void OnValueChanged(bool toggleValue) =>
			ValueChangedEvent?.Invoke(toggleValue);
}