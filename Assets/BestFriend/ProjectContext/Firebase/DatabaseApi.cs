using Cysharp.Threading.Tasks;

[System.Serializable]
public class DatabaseApi : FirebaseApi {
	private const string API_KEYS = "apiKeys";
	private const string OPEN_AI_KEY = "ai";
	private const string GOOGLE_SPEECH_KEY = "speech";
	private const string USERS_DATA = "userData";
	private const string AVATAR_DATA = "avatarData";
	private const string AVATAR_MESH_DATA = "avatarMeshData";
	private const string USERS_SETTINGS = "users";
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

	public async UniTask<RequestData> SetUserData(User data) =>
			await SaveToDatabaseAsync(data, userDataPath);
	public async UniTask<UserRequest> GetUserData() =>
			await GetFromDatabaseAsync<UserRequest>(userDataPath);

	public async UniTask<RequestData> SetAvatarData(Avatar data) =>
			await SaveToDatabaseAsync(data, avatarDataPath);
	
	public async UniTask<AvatarRequest> GetAvatarData() =>
			await GetFromDatabaseAsync<AvatarRequest>(avatarDataPath);

	public async UniTask<RequestData> SetAvatarMeshData(AvatarMesh data) =>
			await SaveToDatabaseAsync(data, avatarMeshDataPath);

	public async UniTask<AvatarMeshRequest> GetAvatarMeshData() =>
			await GetFromDatabaseAsync<AvatarMeshRequest>(avatarMeshDataPath);

	public async UniTask<RequestData> GetAiKey() =>
			await GetFromDatabaseAsync<RequestData>(aiPath);

	public async UniTask<RequestData> GetSpeechKey() =>
			await GetFromDatabaseAsync<RequestData>(speechPath);
}