using System;
using UnityEngine;

public class Main : MonoBehaviour {
	[SerializeField] private Messenger messenger;

	private void Awake() {
		messenger.Initialize();
	}

	private void FixedUpdate() {
		messenger.FixedUpdate_SystemCall();
	}
}
