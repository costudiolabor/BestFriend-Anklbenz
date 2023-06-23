using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class SessionCache {
	public static SessionCache instance;

	public UserRequest userData;
	public AvatarRequest avatarData;
	public AvatarMeshRequest avatarMeshData;

	public bool isUserDataFilled => !string.IsNullOrEmpty(userData.data.age) && !string.IsNullOrEmpty(userData.data.gender) && !string.IsNullOrEmpty(userData.data.name);
	public bool isAvatarDataFilled => !string.IsNullOrEmpty(avatarData.data.gender) && !string.IsNullOrEmpty(avatarData.data.name) && !string.IsNullOrEmpty(avatarData.data.type);

	public bool isIntroReady => isAvatarDataFilled && isUserDataFilled;

	public void Initialize() =>
			instance ??= this;

	public async UniTask GetInitializeData() {
		userData = await DatabaseApi.instance.GetUserData();
		avatarData = await DatabaseApi.instance.GetAvatarData();
		avatarMeshData = await DatabaseApi.instance.GetAvatarMeshData();
	}
}