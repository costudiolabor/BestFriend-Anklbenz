using Cysharp.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

public class DatabaseApi : FirebaseApi {
	//chat settings
	private const string CHAT_SETTINGS = "chatSettings";
	private const string CONVERSATION_SETTINGS = "conversationSettings";
	private const string MODEL_SETTINGS = "modelSettings";
	//user settings
	private const string USERS_ROOT = "users";
	private const string USER_DATA = "data";
	private const string USER_LIMITS = "limitation";
	private const string CURRENT_USER_FIELD = "user";
	private const string CURRENT_AVATAR_FIELD = "avatar";
	private const string NAME = "name";
	private const string ROLE = "role";
	private const string CURRENT_AVATAR_MESH_FIELD = "avatarMesh";

	private const string AI_REQUEST_CLOUD_FUNCTION_NAME = "helloWorld"; // :)
	private const string UPDATE_LIMITS_FUNCTION_NAME = "updateLimits";
	private const string SERVER_TIME_FUNCTION_NAME = "GetCurrentTime";
	private string chatSettingsPath => $"{CHAT_SETTINGS}";
	private string userLimitPath => $"{USERS_ROOT}/{user.UserId}/{USER_LIMITS}";
	private string currentUserDataPath => $"{USERS_ROOT}/{user.UserId}/{USER_DATA}";
	private string userDataPath => $"{USERS_ROOT}/{user.UserId}/{USER_DATA}/{CURRENT_USER_FIELD}";
	private string avatarDataPath => $"{USERS_ROOT}/{user.UserId}/{USER_DATA}/{CURRENT_AVATAR_FIELD}";
	private string avatarNamePath => $"{USERS_ROOT}/{user.UserId}/{USER_DATA}/{CURRENT_AVATAR_FIELD}/{NAME}";
	private string avatarRolePath => $"{USERS_ROOT}/{user.UserId}/{USER_DATA}/{CURRENT_AVATAR_FIELD}/{ROLE}";
	private string avatarMeshDataPath => $"{USERS_ROOT}/{user.UserId}/{USER_DATA}/{CURRENT_AVATAR_FIELD}";

	public static DatabaseApi instance;

	public override async UniTask<Request> Initialize() {
		instance ??= this;
		return await base.Initialize();
	}

	public async UniTask<RequestData<UserData>> GetUserData() =>
			await GetObjectFromDatabaseAsync<UserData>(currentUserDataPath);
	
	public async UniTask<Request> SetUserData(User data) =>
			await SaveToDatabaseAsync(data, userDataPath);

	public async UniTask<Request> SetAvatarData(Avatar data) =>
			await SaveToDatabaseAsync(data, avatarDataPath);
	
	public async UniTask<Request> SetAvatarName(string data) =>
			await SaveValueToDatabaseAsync(data, avatarNamePath);
	
	public async UniTask<Request> SetAvatarRole(string data) =>
			await SaveValueToDatabaseAsync(data, avatarRolePath);

	public async UniTask<Request> InjectLimitsData() =>
			await SaveToDatabaseAsync(new Limits(), userLimitPath);

	public async UniTask<Request> SetAvatarMeshData(AvatarMesh data) =>
			await SaveToDatabaseAsync(data, avatarMeshDataPath);

	public async UniTask<RequestData<User>> GetCurrentUserData() =>
			await GetObjectFromDatabaseAsync<User>(userDataPath);

	public async UniTask<RequestData<Avatar>> GetCurrentAvatarData() =>
			await GetObjectFromDatabaseAsync<Avatar>(avatarDataPath);

	public async UniTask<RequestData<AvatarMesh>> GetAvatarMeshData() =>
			await GetObjectFromDatabaseAsync<AvatarMesh>(avatarMeshDataPath);

	public async UniTask<RequestData<ChatSettings>> GetChatSettings() =>
			await GetObjectFromDatabaseAsync<ChatSettings>(chatSettingsPath);


	public async UniTask<Request> SetLimits(Limits limit) =>
			await SaveToDatabaseAsync(limit, userLimitPath);

	public async UniTask<RequestData<Limits>> GetLimits() =>
			await GetObjectFromDatabaseAsync<Limits>(userLimitPath);

	public async UniTask<RequestData<AiResponse>> SendAiRequest(AiRequest request) {
		var cloudRequest = new CloudRequest() {data = request, uid = user.UserId};
		var messageJson = JsonUtility.ToJson(cloudRequest);
		return await GetCloudFunctionResult<AiResponse>(AI_REQUEST_CLOUD_FUNCTION_NAME, messageJson);
	}

	public async UniTask<RequestData<LimitsWithUnixNow>> GetActualLimits() {
		var cloudRequest = new UidRequest() {uid = user.UserId};
		var messageJson = JsonUtility.ToJson(cloudRequest);
		return await GetCloudFunctionResult<LimitsWithUnixNow>(UPDATE_LIMITS_FUNCTION_NAME, messageJson);
	}

	public async UniTask<RequestData<ServerTime>> GetServerTime() {
		return await GetCloudFunctionResult<ServerTime>(SERVER_TIME_FUNCTION_NAME);
	}
}

/*public async UniTask<Request> SendMessage(ChatApiRequest message) {
	var function = functions.GetHttpsCallable("helloWorld");

	try {
		string f = JsonUtility.ToJson(message);
		var result = await function.CallAsync(f);
		
		return new Request() {errorMessage = (string)result.Data};
	}
	catch (Exception e) {
		return GetFaultRequestData(e);
	}
}*/