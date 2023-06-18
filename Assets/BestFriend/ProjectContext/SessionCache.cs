using UnityEngine;

[System.Serializable]
public class SessionCache {
	
    public string fbxFileName { get; set; }
    public bool hasAnimation { get; set; }

    public static SessionCache instance;

    public SessionCache(){
        if (instance is not null) return;
        instance = this;
    }

    public void Clear(){
  //
    }
}