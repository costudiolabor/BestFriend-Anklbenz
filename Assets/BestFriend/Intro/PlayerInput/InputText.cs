using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class InputText : InputData<string> {
	[SerializeField] private TMP_InputField inputField;
	[SerializeField] private TMP_Text placeholderText;

	public string placeholder {
		get => placeholderText.text;
		set => placeholderText.text = value;
	}

	public override void Initialize() {
		base.Initialize();
		inputField.onSubmit.AddListener(delegate { OnSubmit(); });
	}
	public override UniTask<string> AwaitInput() {
		inputField.text = string.Empty;
		return base.AwaitInput();
	}

	protected override void OnSubmit() =>
			inputCompletionSource.TrySetResult(inputField.text);

}