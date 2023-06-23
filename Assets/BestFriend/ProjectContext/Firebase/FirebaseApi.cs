using System;
using Firebase;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

[System.Serializable]
public class FirebaseApi {
	public FirebaseUser user => _auth.CurrentUser;
	public bool isSigned => _auth.CurrentUser != null;
	public bool isDatabaseConnected => _database != null;

	private FirebaseAuth _auth;
	private DatabaseReference _database;

	public async UniTask<RequestData> ConnectAsync() {
		var checkDependencyResult = await FirebaseApp.CheckAndFixDependenciesAsync();

		if (checkDependencyResult != DependencyStatus.Available)
			return new RequestData() {isSuccess = false, errorMessage = checkDependencyResult.ToString()};

		_auth = FirebaseAuth.DefaultInstance;
		_database = FirebaseDatabase.DefaultInstance.RootReference;

		return new RequestData() {isSuccess = true};
	}

	public async UniTask<RequestData> LinkAnonymousToEmailCredential(string email, string password) {
		try {
			var credential = EmailAuthProvider.GetCredential(email, password /*"as@Msfaf.cfd", "password"*/);
			await _auth.CurrentUser.LinkWithCredentialAsync(credential);
			return new RequestData() {isSuccess = true};
		}
		catch (Exception exception) {
			return GetFaultRequestData<RequestData>(exception);
		}
	}

	public async UniTask<RequestData> CreateAsAnonymousAsync() {
		try {
			var authResult = await _auth.SignInAnonymouslyAsync();
			return new RequestData() {isSuccess = true};
		}
		catch (Exception exception) {
			return GetFaultRequestData<RequestData>(exception);
		}
	}

	public async UniTask<RequestData> CreateWithEmailAndPasswordAsync(string email, string password) {
		try {
			var authResult = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);

			return new RequestData() {isSuccess = true};
		}
		catch (Exception exception) {
			return GetFaultRequestData<RequestData>(exception);
		}
	}

	public async UniTask<RequestData> SignInWithEmailAndPasswordAsync(string email, string password) {
		try {
			var authResult = await _auth.SignInWithEmailAndPasswordAsync(email, password);
			//user = authResult.User;
			return new RequestData() {isSuccess = authResult.User != null};
		}
		catch (Exception exception) {
			return GetFaultRequestData<RequestData>(exception);
		}
	}

	public async UniTask<T> GetFromDatabaseAsync<T>(string databasePath) where T : RequestData, new() {
		try {
			var dataSnapShot = await _database.Child(databasePath).GetValueAsync();

			if (!dataSnapShot.Exists) throw new Exception($"On current path {databasePath}, data dont found");
			return dataSnapShot.HasChildren ? JsonUtility.FromJson<T>(dataSnapShot.GetRawJsonValue()) : new T() {rawDataIfExist = dataSnapShot.GetRawJsonValue()};
		}
		catch (Exception exception) {
			return GetFaultRequestData<T>(exception);
		}
	}

	public async UniTask<RequestData> SaveToDatabaseAsync<T>(T data, string databasePath) {
		try {
			var dataInJson = JsonUtility.ToJson(data);
			await _database.Child(databasePath).SetRawJsonValueAsync(dataInJson);
			return new RequestData() {isSuccess = true};
		}
		catch (Exception exception) {
			return GetFaultRequestData<RequestData>(exception);
		}
	}

	public void SingOut() =>
			_auth.SignOut();

	private T GetFaultRequestData<T>(Exception exception) where T : RequestData, new() =>
			new T() {
					isSuccess = false,
					errorCode = exception.InnerException is FirebaseException firebaseException ? firebaseException.ErrorCode : 0,
					errorMessage = exception.GetBaseException().Message
			};
}
