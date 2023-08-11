using System;
using Firebase;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Functions;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class FirebaseApi {
	public FirebaseUser user => _auth.CurrentUser;
	public bool isSigned => _auth.CurrentUser != null;
	public bool isDatabaseConnected => _database != null;

	private FirebaseAuth _auth;
	private DatabaseReference _database;
	protected FirebaseFunctions functions;

	public virtual async UniTask<Request> Initialize() {
		var checkDependencyResult = await FirebaseApp.CheckAndFixDependenciesAsync();

		if (checkDependencyResult != DependencyStatus.Available)
			return new Request() {isSuccess = false, errorMessage = checkDependencyResult.ToString()};

		FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);

		_auth = FirebaseAuth.DefaultInstance;
		_database = FirebaseDatabase.DefaultInstance.RootReference;
		functions = FirebaseFunctions.DefaultInstance;

		return new Request() {isSuccess = true};
	}

	public async UniTask<Request> LinkAnonymousToEmailCredential(string email, string password) {
		try {
			var credential = EmailAuthProvider.GetCredential(email, password);
			await _auth.CurrentUser.LinkWithCredentialAsync(credential);
			return new Request() {isSuccess = true};
		}
		catch (Exception exception) {
			return GetFaultRequest(exception);
		}
	}

	public async UniTask<Request> CreateAsAnonymousAsync() {
		try {
			var authResult = await _auth.SignInAnonymouslyAsync();
			return new Request() {isSuccess = true};
		}
		catch (Exception exception) {
			return GetFaultRequest(exception);
		}
	}

	public async UniTask<Request> CreateWithEmailAndPasswordAsync(string email, string password) {
		try {
			var authResult = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);

			return new Request() {isSuccess = true};
		}
		catch (Exception exception) {
			return GetFaultRequest(exception);
		}
	}

	public async UniTask<Request> SignInWithEmailAndPasswordAsync(string email, string password) {
		try {
			var authResult = await _auth.SignInWithEmailAndPasswordAsync(email, password);
			//user = authResult.User;
			return new Request() {isSuccess = authResult.User != null};
		}
		catch (Exception exception) {
			return GetFaultRequest(exception);
		}
	}

	public async UniTask<RequestData<T>> GetObjectFromDatabaseAsync<T>(string databasePath) where T : new() {
		try {
			var dataSnapShot = await _database.Child(databasePath).GetValueAsync();
			if (!dataSnapShot.Exists) throw new Exception($"On current path {databasePath}, data dont found");
			return new RequestData<T>() {isSuccess = true, data = JsonUtility.FromJson<T>(dataSnapShot.GetRawJsonValue())};
		}
		catch (Exception exception) {
			return GetFaultRequestData<T>(exception);
		}
	}

	public async UniTask<Request> SaveValueToDatabaseAsync<T>(T data, string databasePath) {
		try {
		   await _database.Child(databasePath).SetValueAsync(data);
			return new Request() {isSuccess = true};
		}
		catch (Exception exception) {
			return GetFaultRequest(exception);
		}
	}
	
	public async UniTask<Request> SaveToDatabaseAsync<T>(T data, string databasePath) {
		try {
			var dataInJson = JsonUtility.ToJson(data);
			await _database.Child(databasePath).SetRawJsonValueAsync(dataInJson);
			return new Request() {isSuccess = true};
		}
		catch (Exception exception) {
			return GetFaultRequest(exception);
		}
	}
	
	public async UniTask<RequestData<T>> GetCloudFunctionResult<T>(string functionName, string jsonParam = null) where T : new() {
		var function = functions.GetHttpsCallable(functionName);
		try {
			var result = await function.CallAsync(jsonParam);
			return new RequestData<T>() {isSuccess = true, data = JsonUtility.FromJson<T>((string)result.Data)};
		}
		catch (Exception exception) {
			return GetFaultRequestData<T>(exception);
		}
	}
	
	protected Request GetFaultRequest(Exception exception) =>
			new() {
					isSuccess = false,
					errorCode = exception.InnerException is FirebaseException firebaseException ? firebaseException.ErrorCode : 0,
					errorMessage = exception.GetBaseException().Message
			};

	protected RequestData<T> GetFaultRequestData<T>(Exception exception) where T : new() =>
			new() {
					data = new(),
					isSuccess = false,
					errorCode = exception.InnerException is FirebaseException firebaseException ? firebaseException.ErrorCode : 0,
					errorMessage = exception.GetBaseException().Message
			};
	
	public void SignOut() =>
			_auth.SignOut();
}