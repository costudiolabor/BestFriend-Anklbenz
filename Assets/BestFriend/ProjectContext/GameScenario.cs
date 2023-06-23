using System;

[System.Serializable]
public class GameScenario : IDisposable {
	private bool isSingedToFirebase => DatabaseApi.instance.user != null;
	public void Initialize() {
		Auth.SignedInEvent += OnSignedIn;
		Auth.SignedUpEvent += OnSignedUp;
		Intro.IntroReadyEvent += OnIntroReady;
	}

	public async void SelectScenario() {

		if (isSingedToFirebase) {
			await SessionCache.instance.GetInitializeData();
			OnSignedIn();
		}
		else {
			ScenesLoader.instance.GotoAuthScene();
		}
	}

	private void OnSignedUp() =>
			ScenesLoader.instance.GotoIntroScene();

	private void OnSignedIn() {
		if (SessionCache.instance.isIntroReady)
			OnIntroReady();
		else
			ScenesLoader.instance.GotoIntroScene();
	}

	private void OnIntroReady() =>
			ScenesLoader.instance.GotoConversationScene();

	public void Dispose() {
		Auth.SignedInEvent -= OnSignedIn;
		Auth.SignedUpEvent -= OnSignedUp;
		Intro.IntroReadyEvent -= OnIntroReady;
	}
}