using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class InputLabel : AnimatedView {
	[SerializeField] private Typewriter typewriter;
	[SerializeField] private InputText inputText;
	[SerializeField] private Button acceptButton, backButton;
	
	private void Awake() {
		inputText.ValueChangedEvent += SetButtonInteractable;
	//	acceptButton.onClick.AddListener(OnSubmit);
		SetButtonInteractable();
	}
	
	/*
	public async UniTask<string> SayAndAwaitAnswer(string message) {
		_inputCompletionSource = new UniTaskCompletionSource<string>();
		await typewriter.PrintAsNew(message);

		inputText.Open();
		var answer = await _inputCompletionSource.Task;
		inputText.Close();

		return answer;
	}
	*/

	/*
	private void OnSubmit() =>
			_inputCompletionSource.TrySetResult(inputText.value);

	protected void OnReject() {
		_inputCompletionSource.TrySetResult(string.Empty);
		ForceClose();
	}
	*/

	private void SetButtonInteractable() {
		var text = inputText.value;
		acceptButton.interactable = !(string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text));
	}
}