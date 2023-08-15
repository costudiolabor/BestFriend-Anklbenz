using Cinemachine;
using UnityEngine;

[System.Serializable]
public class VirtualCamerasController {
	[SerializeField] private CinemachineVirtualCamera mainViewCamera;
	[SerializeField] private CinemachineVirtualCamera chatViewCamera;

	public void SetChatMode() {
		mainViewCamera.enabled = false;
		chatViewCamera.enabled = true;
	}

	public void SetMainMode() {
		mainViewCamera.enabled = true;
		chatViewCamera.enabled = false;
	}
}
