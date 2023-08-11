using Enums;
using System;
using UnityEngine;

[Serializable]
public class AvatarPrefs {
	[SerializeField] private AvatarPrefsView prefsView;
	public event Action PreferenceChangedEvent;
	public string avatarName { get; private set; }
	public Role avatarRole { get; private set; }

	public void Initialize(Avatar avatar) {
		if (!Enum.TryParse(avatar.role, true, out Role roleParseResult)) throw new Exception($"[BF] Role named {avatar.role} parse not success");
		avatarRole = roleParseResult;
		avatarName = avatar.name;

		prefsView.avatarName = avatarName;
		prefsView.avatarRole = avatarRole;
		prefsView.AcceptEvent += OnAccept;
		prefsView.CloseEvent += Close;
		prefsView.ForceClose();
	}

	public void Open() {
		prefsView.avatarName = avatarName;
		prefsView.avatarRole = avatarRole;
		prefsView.Open();
	}

	public void Close() =>
			prefsView.Close();

	private void OnAccept() {
		var nameChanged = avatarName != prefsView.avatarName;
		var roleChanged = avatarRole != prefsView.avatarRole;

		if (nameChanged)
			SetName(prefsView.avatarName);

		if (roleChanged)
			SetRole(prefsView.avatarRole);

		if (nameChanged || roleChanged)
			PreferenceChangedEvent?.Invoke();

		prefsView.Close();
	}

	private async void SetName(string nameValue) {
		avatarName = nameValue;
		await DatabaseApi.instance.SetAvatarName(nameValue);
	}

	private async void SetRole(Role roleValue) {
		avatarRole = roleValue;
		await DatabaseApi.instance.SetAvatarRole(roleValue.ToString());
	}
}