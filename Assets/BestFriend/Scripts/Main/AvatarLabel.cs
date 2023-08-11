using System;
using UnityEngine;

[System.Serializable]
public class AvatarLabel {
	[SerializeField] private AvatarLabelView labelView;
	public event Action ClickedEvent;

	public void Initialize() {
		labelView.ClickedEvent += ClickNotify;
	}
	public void Set(Avatar avatar) =>
			Set(avatar.name, avatar.role);
	
	public void Set(string avatarName, string avatarRole) {
		labelView.avatarName = avatarName;
		labelView.avatarRole = avatarRole;
	}
	
	private void ClickNotify() =>
		ClickedEvent?.Invoke();
}
