using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class Intro : MonoBehaviour {
	private const string NAME_TAG = "#name";
	private const int DELAY_BETWEEN_MESSAGES = 1000;

	[SerializeField] private Typewriter typewriter;
	[SerializeField] private InputText nameField;
	[SerializeField] private InputDate inputDate;
	[SerializeField] private InputOption inputOption;
	[SerializeField] private Button backButton;
	[SerializeField] private string[] messages;
	[SerializeField] private string[] genders;
	[SerializeField] private string playerNamePlaceholderText, aiNamePlaceholderText;

//	private CancellationTokenSource _source = new();
//	private CancellationToken _token;

	private DateTime _playerBirthday;
	private int _introStep, _stepCount = 7;
	private string _playerName, _aiName, _playerGender;
	private void Awake() {
		nameField.ForceClose();
		backButton.onClick.AddListener(OnBackClicked);
//		_token = _source.Token;
	}

	private async void Start() {
		nameField.Initialize();
		inputDate.Initialize();
		inputOption.Initialize();

		for (; _introStep < _stepCount; ++_introStep) {
			await GoToStep(_introStep);
			await UniTask.Delay(DELAY_BETWEEN_MESSAGES);
		}
	}

	private async UniTask GoToStep(int step) {
		switch (step) {
			case 0:
				await Say(messages[0]);
				return;
			case 1:
				await Say(messages[1]);
				return;
			case 2:
				_playerName = await SayAndAwaitAnswer(messages[2], playerNamePlaceholderText);
				return;
			case 3:
				await Say(messages[3].Replace(NAME_TAG, _playerName));
				return;
			case 4:
				_aiName = await SayAndAwaitAnswer(messages[4], aiNamePlaceholderText);
				return;
			case 5:
				await Say(messages[5]);
				return;
			case 6:
				_playerBirthday = await SayAndAwaitDate(messages[6]);
				return;
			case 7:
				_playerGender = await SayAndAwaitOpinion(messages[7], genders);
				return;
			default:
				throw new Exception($"For step {step} Behaviour not Exist");
		}
	}

	private async UniTask Say(string message, int delayBeforeHide = 2000) {
		typewriter.Open();
		await typewriter.PrintAsNew(message);
		await UniTask.Delay(delayBeforeHide);
		typewriter.Close();
	}

	private async UniTask<string> SayAndAwaitAnswer(string message, string placeholderHint = "") {
		typewriter.Open();
		await typewriter.PrintAsNew(message);

		nameField.placeholder = placeholderHint;
		nameField.Open();
		var answer = await nameField.AwaitInput();
		nameField.Close();
		typewriter.Close();
		return answer;
	}

	private async UniTask<DateTime> SayAndAwaitDate(string message) {
		typewriter.Open();
		await typewriter.PrintAsNew(message);

		inputDate.Open();
		var date = await inputDate.AwaitInput();
		inputDate.Close();
		typewriter.Close();
		return date;
	}

	private async UniTask<string> SayAndAwaitOpinion(string message, string[] options) {
		inputOption.AddItems(options);
		typewriter.Open();
		await typewriter.PrintAsNew(message);

		inputOption.Open();
		var option = await inputOption.AwaitInput();
		typewriter.Close();
		inputOption.Close();
		return option;
	}

	private void OnBackClicked() {
		if (_stepCount > 0)
			_stepCount--;
	}
}