using System;
using Firebase.Auth;
[System.Serializable]
public class UserData : RequestData {
	public string name;
	public string gender;
	public string age;
}

[System.Serializable]
public class AvatarData : RequestData {
	public string name;
	public string gender;
}

[System.Serializable]
public class AvatarMeshData : RequestData {
	//... for example
	public int head;
	public int body;
	public int legs;
	public int hair;
	public int eyes;
}

[Serializable]
public class AuthData :RequestData {
	public AuthResult authResult;
}

[System.Serializable]
public class RequestData {
	public bool isSuccess;
	public string errorMessage;
	public string rawDataIfExist;
}