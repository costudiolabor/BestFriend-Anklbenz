using System;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class DateLabel : InputData/*<DateTime>*/ {
	[SerializeField] private Typewriter typewriter;
	[SerializeField] private InputDate inputDate;
	[SerializeField] private Button backButton, endTaskButton;
	[SerializeField] private TMP_Text buttonText;

	public event Action BackClickedEvent;

	public override async UniTask<string> SayAndAwaitAnswer(string message) {
		await typewriter.PrintAsNew(message);

		//buttonText.text = buttonCaption;

		inputDate.Open();
		var date = await AwaitTask();
		inputDate.Close();

		return date;
	}

	private void Awake() {
		endTaskButton.onClick.AddListener(OnSubmit);
		backButton.onClick.AddListener(OnBack);
	}
	protected override void OnSubmit() =>
			inputCompletionSource.TrySetResult(inputDate.selectedDate);
	private void OnBack() =>
			BackClickedEvent?.Invoke();

}