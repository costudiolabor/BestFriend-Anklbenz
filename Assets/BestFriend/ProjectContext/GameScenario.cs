using System;
using UnityEngine;

public class GameScenario : IDisposable
{
	public void Initialize() {
		Auth.SignedInEvent += OnSignedIn;
		Auth.SignedUpEvent += OnSignedUp;
	}

	public void Select() {
		if (DatabaseApi.instance.isSigned)
			Debug.Log("Go to next");
		else
			ScenesLoader.instance.GoToLoginScene();
	}
	
	private void OnSignedUp() {
		Debug.Log("SignedUp");
	   // ScenesLoader.instance.GoToIntroScene();	
	}
	private async void OnSignedIn() {
		Debug.Log("SignedIn");
		var user = await DatabaseApi.instance.GetUserData();
	//	if(user)
	}
	
	public void Dispose() {
		Auth.SignedInEvent -= OnSignedIn;
		Auth.SignedUpEvent -= OnSignedUp;
	}
}
