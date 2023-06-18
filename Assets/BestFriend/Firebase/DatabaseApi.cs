using Cysharp.Threading.Tasks;

[System.Serializable]
public class DatabaseApi : FirebaseApi {
	private const string API_KEYS = "apiKeys";
	private const string OPEN_AI_KEY = "ai";
	private const string GOOGLE_SPEECH_KEY = "speech";
	private const string USERS_DATA = "usersData";
	private const string AVATAR_DATA = "avatarData";
	private const string AVATAR_MESH_DATA = "avatarMeshData";
	private const string USERS_SETTINGS = "usersSettings";
	private string aiPath => $"{API_KEYS}/{OPEN_AI_KEY}";
	private string speechPath => $"{API_KEYS}/{GOOGLE_SPEECH_KEY}";
	private string userDataPath => $"{USERS_SETTINGS}/{user.UserId}/{USERS_DATA}";
	private string avatarDataPath => $"{USERS_SETTINGS}/{user.UserId}/{AVATAR_DATA}";
	private string avatarMeshDataPath => $"{USERS_SETTINGS}/{user.UserId}/{AVATAR_MESH_DATA}";

	public static DatabaseApi instance;

	public async UniTask Initialize() {
		instance ??= this;
		await ConnectAsync();
	}

	public async UniTask<RequestData> SetUserData(UserData data) =>
			await SaveToDatabaseAsync(data, userDataPath);
	public async UniTask<UserData> GetUserData() =>
			await GetFromDatabaseAsync<UserData>(userDataPath);

	public async UniTask<RequestData> SetAvatarData(AvatarData data) =>
			await SaveToDatabaseAsync(data, avatarDataPath);
	public async UniTask<AvatarData> GetAvatarData() =>
			await GetFromDatabaseAsync<AvatarData>(avatarDataPath);

	public async UniTask<RequestData> SetAvatarMeshData(AvatarMeshData data) =>
			await SaveToDatabaseAsync(data, avatarMeshDataPath);

	public async UniTask<AvatarMeshData> GetAvatarMeshData() =>
			await GetFromDatabaseAsync<AvatarMeshData>(avatarMeshDataPath);

	public async UniTask<RequestData> GetAiKey() =>
			await GetFromDatabaseAsync<RequestData>(aiPath);

	public async UniTask<RequestData> GetSpeechKey() =>
			await GetFromDatabaseAsync<RequestData>(speechPath);
}