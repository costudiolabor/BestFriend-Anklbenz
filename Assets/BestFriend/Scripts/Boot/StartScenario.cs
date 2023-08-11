using System;


public class StartScenario : IDisposable {
	private bool isSingedToFirebase => DatabaseApi.instance.user != null;
	public void Initialize() {
		Auth.SignedInEvent += OnSignedIn;
		Auth.SignedUpEvent += OnSignedUp;
		Meet.IntroReadyEvent += OnIntroReady;
	}

	public async void SelectScenario() {
		if (isSingedToFirebase) {
			
			var request = await DatabaseApi.instance.GetUserData();
			if (!request.isSuccess) throw new Exception("[BF] Error for requesting User data");
			SessionCache.instance.data = request.data;
			
			OnSignedIn();
		}
		else {
			ScenesLoader.instance.GotoAuthScene();
		}
	}

	private void OnSignedUp() =>
			ScenesLoader.instance.GotoIntroScene();

	private void OnSignedIn() {
		if (SessionCache.instance.isAllDataFilled)
			OnIntroReady();
		else
			ScenesLoader.instance.GotoIntroScene();
	}

	private async void OnIntroReady() {
		await DatabaseApi.instance.InjectLimitsData();
		ScenesLoader.instance.GotoConversationScene();
	}

	public void Dispose() {
		Auth.SignedInEvent -= OnSignedIn;
		Auth.SignedUpEvent -= OnSignedUp;
		Meet.IntroReadyEvent -= OnIntroReady;
	}
}