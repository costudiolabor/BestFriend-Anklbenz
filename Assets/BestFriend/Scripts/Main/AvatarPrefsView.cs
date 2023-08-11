using System;
using System.Linq;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AvatarPrefsView : AnimatedView {
	[SerializeField] private Button nameEditButton, closeButton, acceptButton;
	[SerializeField] private TMP_InputField nameInput;
	[SerializeField] private RoleItem[] roles;
	public event Action AcceptEvent, CloseEvent;

	public string avatarName {
		get => nameInput.text;
		set => nameInput.text = value;
	}
	
	public Role avatarRole {
		get {
			var selectedItem = roles.FirstOrDefault(x => x.isOn == true);
			return selectedItem != null ? selectedItem.value : roles[0].value;
		}
		set {
			var selectedItem = roles.FirstOrDefault(x => x.value == value);
			if (selectedItem != null)
				selectedItem.isOn = true;
			else
				roles[0].isOn = true;
		}
	}

	private void Awake() {
		acceptButton.onClick.AddListener(OnAcceptNotify);
		closeButton.onClick.AddListener(OnCloseNotify);
		nameEditButton.onClick.AddListener(delegate { SetNameFieldActivate(true); });
		nameInput.onEndEdit.AddListener(delegate { SetNameFieldActivate(false); });
		SetNameFieldActivate(false);
	}
	private void OnAcceptNotify() =>
			AcceptEvent?.Invoke();
	
	private void OnCloseNotify() =>
			CloseEvent?.Invoke();
	
	private void SetNameFieldActivate(bool isInteractable) =>
		nameInput.interactable = isInteractable;
}