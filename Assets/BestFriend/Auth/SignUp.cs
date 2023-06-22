using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class SignUp {
	[SerializeField] private AuthView view;
	public event Action SignInSelectedEvent, SignUpLaterSelectedEvent, BackSelectedEvent;
	private UniTaskCompletionSource<bool> _loginCompletionSource;

	public void Initialize() {
	   view.ForceClose();
		view.AcceptEvent += OnCreateSelected;
		view.ActionOneEvent += OnLoginAuthSelected;
		view.ActionTwoEvent += OnSingUpLaterSelected;
		view.BackEvent += OnBackSelected;
	}

	public async UniTask<bool> TryAsync() {
		_loginCompletionSource = new UniTaskCompletionSource<bool>();
		view.Open();
		var result = await _loginCompletionSource.Task;
		view.Close();
		return result;
	}

	private async void OnCreateSelected() {
		var result = await DatabaseApi.instance.CreateWithEmailAndPasswordAsync(view.loginText, view.passwordText);
		view.errorMessage = result.errorMessage;
	
		if (result.isSuccess)
			_loginCompletionSource.TrySetResult(true);
	}

	private void OnSingUpLaterSelected() {
		CancelTask();
		SignUpLaterSelectedEvent?.Invoke();
	}

	private void OnLoginAuthSelected() {
		CancelTask();
		SignInSelectedEvent?.Invoke();
	}

	private void OnBackSelected() {
		CancelTask();
		BackSelectedEvent?.Invoke();
	}

	private void CancelTask() =>
			_loginCompletionSource.TrySetResult(false);
}