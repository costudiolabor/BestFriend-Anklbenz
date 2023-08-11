using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LookItem : MonoBehaviour {
	[SerializeField] private Toggle toggle;
	[SerializeField] private TMP_Text genderField, idField;
	[SerializeField] private string gender;
	[SerializeField] private int id;
	
	public event Action<bool> ValueChangedEvent;
	public bool isSelected => toggle.isOn;
	public string genderValue =>  gender;
	public int idValue => id;

	private void Awake() {
		idField.text = id.ToString();
		genderField.text = gender;
		toggle.onValueChanged.AddListener(OnValueChanged);
	}
	private void OnValueChanged(bool toggleValue) =>
			ValueChangedEvent?.Invoke(toggleValue);
}