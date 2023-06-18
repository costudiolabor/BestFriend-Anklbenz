using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public abstract class InputData<T> : AnimatedView {
	[SerializeField] private Button button;
	protected UniTaskCompletionSource<T> inputCompletionSource;

	public virtual void Initialize() =>
			button.onClick.AddListener(OnSubmit);

	public virtual async UniTask<T> AwaitInput() {
		inputCompletionSource = new UniTaskCompletionSource<T>();
		return await inputCompletionSource.Task;
	}

	protected abstract void OnSubmit();
}