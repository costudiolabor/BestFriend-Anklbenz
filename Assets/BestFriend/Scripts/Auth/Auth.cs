//	private bool wasAuthorized => PlayerPrefs.GetInt(WAS_AUTHORIZED_PREVIOUSLY_KEY) != 0;
//	private const string WAS_AUTHORIZED_PREVIOUSLY_KEY = "wasAuthorizedPreviously";
using System;
using UnityEngine;

public class Auth : MonoBehaviour {
	[SerializeField] private Welcome welcome;
	[SerializeField] private SignIn signIn;
	[SerializeField] private SignUp signUp;

	public static event Action SignedUpEvent, SignedInEvent;

	private void Awake() {
		welcome.Initialize();
		welcome.SingInEvent += TrySignInAndNotify;
		welcome.SingUpEvent += TrySignUpAndNotify;
		welcome.SingUpLaterEvent += TrySignUpAnonymousAndNotify;

		signIn.Initialize();
		signIn.SignUpSelectedEvent += TrySignUpAndNotify;
		signIn.BackSelectedEvent += SelectAuth;

		signUp.Initialize();
		signUp.SignInSelectedEvent += TrySignInAndNotify;
		signUp.SignUpLaterSelectedEvent += TrySignUpAnonymousAndNotify;
		signUp.BackSelectedEvent += SelectAuth;

		SelectAuth();
	}

	private async void SelectAuth() =>
			await welcome.SelectAsync();

	private async void TrySignUpAnonymousAndNotify() {
		var result = await DatabaseApi.instance.CreateAsAnonymousAsync();

		if (result.isSuccess)
			SignedUpEvent?.Invoke();
	}

	private async void TrySignInAndNotify() {
		var loginInResult = await signIn.TryAsync();
		if (loginInResult)
			SignedInEvent?.Invoke();
	}

	private async void TrySignUpAndNotify() {
		var registerResult = await signUp.TryAsync();

		if (registerResult)
			SignedUpEvent?.Invoke();
	}
}