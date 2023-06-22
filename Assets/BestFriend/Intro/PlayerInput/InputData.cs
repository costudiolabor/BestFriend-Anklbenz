using Cysharp.Threading.Tasks;

public abstract class InputData : AnimatedView {
	public abstract UniTask<string> SayAndAwaitAnswer(string message);
	protected UniTaskCompletionSource<string> inputCompletionSource;
	protected async UniTask<string> AwaitTask() {
		inputCompletionSource = new UniTaskCompletionSource<string>();
		return await inputCompletionSource.Task;
	}
	protected abstract void OnSubmit();
	protected void OnReject() =>
			inputCompletionSource.TrySetResult(string.Empty);
}