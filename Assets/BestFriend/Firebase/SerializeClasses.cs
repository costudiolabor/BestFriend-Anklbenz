[System.Serializable]
public class User {
	public string name;
	public string gender;
	public string age;
}

[System.Serializable]
public class Avatar  {
	public string name;
	public string gender;
	public string type;
}

[System.Serializable]
public class AvatarMesh 
{
	//... for example
	public int head;
	public int body;
	public int legs;
	public int hair;
	public int eyes;
}


[System.Serializable]
public class UserRequest : RequestData {
	public User data;
}
[System.Serializable]
public class AvatarRequest : RequestData {
	public Avatar data;
} 
[System.Serializable]
public class AvatarMeshRequest : RequestData {
	public AvatarMesh data;
} 

[System.Serializable]
public class RequestData {
	public bool isSuccess;
	public int errorCode; // cast to enum AuthError can get error description
	public string errorMessage;
	public string rawDataIfExist;
}