using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleItem : MonoBehaviour {
	[SerializeField] private Toggle toggle;
	[SerializeField] private Image outline;
	[SerializeField] private RectTransform lockerTransform;
	[SerializeField] private TMP_Text label;
	[SerializeField] private CanvasGroup canvasGroup;
	[SerializeField] private Role role;
	[SerializeField] private bool _isLocked;
	
	public bool isOn {
		get => toggle.isOn;
		set => toggle.isOn = value;
	}

	public bool isLocked {
		get => _isLocked;
		set {
			_isLocked = value;
			canvasGroup.interactable = !value;
			lockerTransform.gameObject.SetActive(value);
			outline.gameObject.SetActive(!value);
		}
	}

	public Role value => role;

	private void OnValidate() {
		isLocked = _isLocked;
		label.text = role.ToString();
	}

}
