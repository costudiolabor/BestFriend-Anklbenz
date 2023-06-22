using System;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeView : AnimatedView {
	[SerializeField] private Button signUpButton, signUpLaterButton, signInButton;

	public event Action SignUpEvent, SignUpLaterEvent, SignInEvent;

	private void Awake() {
		signInButton.onClick.AddListener(OnSignIn);
		signUpButton.onClick.AddListener(OnSignUp);
		signUpLaterButton.onClick.AddListener(OnSignUpLater);
	}
	private void OnSignUpLater() =>
			SignUpLaterEvent?.Invoke();

	private void OnSignUp() =>
			SignUpEvent?.Invoke();
	private void OnSignIn() =>
			SignInEvent?.Invoke();
}