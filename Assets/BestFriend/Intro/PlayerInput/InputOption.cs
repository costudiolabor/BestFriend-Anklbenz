using UnityEngine;

public class InputOption : InputData<string> {
	[SerializeField] private UIScroller decisionScroller;
	public override void Initialize() {
		base.Initialize();
	}

	public void AddItems(string[] opinions) {
		decisionScroller.AddItems(opinions);
	}

	protected override void OnSubmit() =>
			inputCompletionSource.TrySetResult(decisionScroller.selected.text);
}