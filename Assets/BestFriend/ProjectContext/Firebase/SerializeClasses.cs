[System.Serializable]

public class UserData {
	public User user;
	public Avatar avatar;
	public AvatarMesh avatarMesh;
}
[System.Serializable]
public class User {
	public string name;
	public string gender;
	public string age;
}

[System.Serializable]
public class Avatar  {
	public string name;
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
public class ChatSettings {
	public ConversationsSettings conversationSettings;
	public AiRequest modelSettings;
}

[System.Serializable]
public class ConversationsSettings {
	public string legend;
	public int messagesInMemory;
	public int freeMessagesCount;
	public long freeMessageIncrementTimeMilliseconds;
}

[System.Serializable]
public class ModelSettings {
	public AiRequest requestSettings;
}

[System.Serializable]
public class RequestData<T> : Request {
	public T data;
}

[System.Serializable]
public class Limits {
	public long lastRequest;
	public int requestsAvailable;
}

[System.Serializable]
public class Request {
	public bool isSuccess;
	public int errorCode; // cast to enum AuthError can get error description
	public string errorMessage;
}