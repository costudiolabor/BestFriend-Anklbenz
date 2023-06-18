using System;
using Firebase;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class FirebaseApi {
	public FirebaseUser user => _auth.CurrentUser;
	public bool isSigned => _auth.CurrentUser != null;
	public bool isDatabaseConnected => _database != null;

	private FirebaseAuth _auth;
	private DatabaseReference _database;

	public async UniTask<RequestData> ConnectAsync() {
		try {
			var checkDependencyResult = await FirebaseApp.CheckAndFixDependenciesAsync();

			if (checkDependencyResult != DependencyStatus.Available)
				return new RequestData() {isSuccess = false, errorMessage = checkDependencyResult.ToString()};

			_auth = FirebaseAuth.DefaultInstance;
			_database = FirebaseDatabase.DefaultInstance.RootReference;

			return new RequestData() {isSuccess = true};
		}
		catch (Exception exception) {
			return new RequestData() {errorMessage = exception.GetBaseException().Message, isSuccess = false};
		}
	}

	public async UniTask<RequestData> LinkAnonymousToEmailCredential(string email, string password) {
		try {
			var credential = EmailAuthProvider.GetCredential(email, password /*"as@Msfaf.cfd", "password"*/);
			await _auth.CurrentUser.LinkWithCredentialAsync(credential);
			return new RequestData() {isSuccess = true};
		}
		catch (Exception exception) {
			return new RequestData() {isSuccess = false, errorMessage = exception.GetBaseException().Message};
		}
	}

	public async UniTask<RequestData> CreateAsAnonymousAsync() {
		try {
			var authResult = await _auth.SignInAnonymouslyAsync();
			return new RequestData() {isSuccess = true};
		}
		catch (Exception exception) {
			return new RequestData() {isSuccess = false, errorMessage = exception.GetBaseException().Message};
		}
	}

	public async UniTask<RequestData> CreateWithEmailAndPasswordAsync(string email, string password) {
		try {
			var authResult = await _auth.CreateUserWithEmailAndPasswordAsync(email, password);

			return new RequestData() {isSuccess = true};
		}
		catch (Exception exception) {
			return new RequestData() {isSuccess = false, errorMessage = exception.GetBaseException().Message};
		}
	}

	public async UniTask<RequestData> SignInWithEmailAndPasswordAsync(string email, string password) {
		try {
			var authResult = await _auth.SignInWithEmailAndPasswordAsync(email, password);
			//user = authResult.User;
			return new RequestData() {isSuccess = authResult.User != null};
		}
		catch (Exception exception) {
			return new RequestData() {isSuccess = false, errorMessage = exception.GetBaseException().Message};
		}
	}

	public async UniTask<T> GetFromDatabaseAsync<T>(string databasePath) where T : RequestData {
		try {
			var dataSnapShot = await _database.Child(databasePath).GetValueAsync();

			if (!dataSnapShot.Exists) return null;
			return dataSnapShot.HasChildren ? JsonUtility.FromJson<T>(dataSnapShot.GetRawJsonValue()) : (T)new RequestData() {rawDataIfExist = dataSnapShot.GetRawJsonValue()};
		}
		catch (Exception exception) {
			return (T)new RequestData() {isSuccess = false, errorMessage = exception.GetBaseException().Message};
		}
	}

	public async UniTask<RequestData> SaveToDatabaseAsync<T>(T data, string databasePath) {
		try {
			var dataInJson = JsonUtility.ToJson(data);
			await _database.Child(databasePath).SetRawJsonValueAsync(dataInJson);
			return new RequestData() {isSuccess = true};
		}
		catch (Exception exception) {
			return new RequestData() {isSuccess = false, errorMessage = exception.GetBaseException().Message};
		}
	}

	public void SingOut() =>
			_auth.SignOut();
}