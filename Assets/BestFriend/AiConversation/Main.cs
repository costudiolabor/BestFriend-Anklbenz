using UnityEngine;

public class Main : MonoBehaviour {
	[SerializeField] private Messenger messenger;
	
	private async void Awake() {
		await messenger.Initialize();
	}

	private void FixedUpdate() {
		messenger.FixedUpdate_SystemCall();
	}
}
