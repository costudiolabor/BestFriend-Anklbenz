using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class Welcome {
	[SerializeField] private WelcomeView view;
	public event Action SingInEvent, SingUpEvent, SingUpLaterEvent;
	private UniTaskCompletionSource _completionSource;

	public void Initialize() {
		view.ForceClose();
		view.SignInEvent += OnSingIn;
		view.SignUpEvent += OnSignUp;
		view.SignUpLaterEvent += OnSignUpLater;
	}

	public async UniTask SelectAsync() {
		_completionSource = new UniTaskCompletionSource();
		view.Open();
      await _completionSource.Task;
		view.Close();
	}
	
	private void OnSignUpLater() {
		SingUpLaterEvent?.Invoke();
		CompleteTask();
	}
	private void OnSignUp() {
		SingUpEvent?.Invoke();
		CompleteTask();
	}
	private void OnSingIn() {
		SingInEvent?.Invoke();
		CompleteTask();
	}

	private void CompleteTask() =>
			_completionSource.TrySetResult();
}
