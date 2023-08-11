using UnityEngine;

public class Main : MonoBehaviour {
	[SerializeField] private Messenger messenger;
	[SerializeField] private AvatarPrefs avatarPrefs;
	[SerializeField] private AvatarLabel avatarLabel;
	
	private async void Awake() {
		var chatSettings = await DatabaseApi.instance.GetChatSettings();
		var userData = await DatabaseApi.instance.GetUserData();
		var avatarData = userData.data.avatar;
		
		avatarPrefs.Initialize(avatarData);
		avatarPrefs.PreferenceChangedEvent += OnPreferencesChanged;
		
		avatarLabel.Initialize();
		avatarLabel.Set(avatarData);
		avatarLabel.ClickedEvent += OpenAvatarPreferences;
		
		await messenger.Initialize(chatSettings.data, userData.data);
	}

	private void OnPreferencesChanged() {
		avatarLabel.Set(avatarPrefs.avatarName, avatarPrefs.avatarRole.ToString());
		// Change Legend by new Role
	}
	private void OpenAvatarPreferences() {
		avatarPrefs.Open();
	}

	private void FixedUpdate() {
		messenger.FixedUpdate_SystemCall();
	}
}