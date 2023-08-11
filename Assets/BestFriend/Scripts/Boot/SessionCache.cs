using Cysharp.Threading.Tasks;

[System.Serializable]
public class SessionCache {
	public static SessionCache instance;

	public UserData data;

	public bool isUserDataFilled => !string.IsNullOrEmpty(data.user?.age) && !string.IsNullOrEmpty(data.user?.gender) && !string.IsNullOrEmpty(data.user?.name);
	public bool isAvatarDataFilled => !string.IsNullOrEmpty(data.avatar?.name) && !string.IsNullOrEmpty(data.avatar?.role);

	public bool isAllDataFilled => isAvatarDataFilled && isUserDataFilled;

	public void Initialize() =>
			instance ??= this;
	
}