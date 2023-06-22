using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Firebase.Auth;

[System.Serializable]
public class SignIn {
	[SerializeField] private AuthView view;
	public event Action SignUpSelectedEvent, ForgotPasswordEvent, BackSelectedEvent;
	private UniTaskCompletionSource<bool> _loginCompletionSource;

	public void Initialize() {
		view.ForceClose();
		view.AcceptEvent += OnTryAccept;
		view.ActionOneEvent += OnRegistrationSelected;
		view.BackEvent += OnBackSelected;
	}

	public async UniTask<bool> TryAsync() {
		_loginCompletionSource = new UniTaskCompletionSource<bool>();
		view.Open();
		var result = await _loginCompletionSource.Task;
		view.Close();
		return result;
	}

	private async void OnTryAccept() {
		var result = await DatabaseApi.instance.SignInWithEmailAndPasswordAsync(view.loginText, view.passwordText);
		view.errorMessage = result.errorMessage;
		var va = (AuthError)result.errorCode;
		if (!result.isSuccess) return;

		_loginCompletionSource.TrySetResult(true);
	}

	private void OnForgotPassword() {
		CancelTask();
		ForgotPasswordEvent?.Invoke();
	}
	
	private void OnRegistrationSelected() {
		CancelTask();
		SignUpSelectedEvent?.Invoke();
	}

	public void OnBackSelected() {
		CancelTask();
		BackSelectedEvent?.Invoke();
	}
	
	private void CancelTask() =>
			_loginCompletionSource.TrySetResult(false);
}