using System;
using UnityEngine;

public class Meet : MonoBehaviour {
	private const string DEFAULT_ROLE = "friend";
	
	[SerializeField] private InputTextLabel nameLabel, aiNameLabel;
	[SerializeField] private InputDateLabel birthdateLabel;
	[SerializeField] private InputOpinionLabel genderLabel;
	[SerializeField] private InputLookLabel lookLabel;

	public static event Action IntroReadyEvent; //static event 
	private string _playerName, _playerBirthdate, _playerGender, _aiName;
	private Look _aiLook;

	private void Awake() {
		nameLabel.AcceptedEvent += OnNameAccepted;
		birthdateLabel.AcceptedEvent += OnBirthAccepted;
		birthdateLabel.BackEvent += OnBirthBack;
		genderLabel.AcceptedEvent += OnGenderAccepted;
		genderLabel.BackEvent += OnGenderBack;
		lookLabel.AcceptedEvent += OnLookAccepted;
		lookLabel.BackEvent += OnLookBack;
		aiNameLabel.AcceptedEvent += OnAINameAccepted;
		aiNameLabel.BackEvent += OnAINameBack;

		if (!SessionCache.instance.isUserDataFilled)
			nameLabel.Open();
		else
			lookLabel.Open();
	}

	private void OnNameAccepted() {
		nameLabel.Close();
		_playerName = nameLabel.value;
		birthdateLabel.Open();
	}
	private void OnBirthAccepted() {
		birthdateLabel.Close();
		_playerBirthdate = birthdateLabel.value;
		genderLabel.Open();

	}
	private void OnBirthBack() {
		birthdateLabel.Close();
		nameLabel.Open();
	}
	private async void OnGenderAccepted() {
		genderLabel.Close();
		_playerGender = genderLabel.value;
		await DatabaseApi.instance.SetUserData(new User() {name = _playerName, age = _playerBirthdate, gender = _playerGender});
		lookLabel.Open();
	}
	private void OnGenderBack() {
		genderLabel.Close();
		birthdateLabel.Open();
	}

	private void OnLookAccepted() {
		genderLabel.Close();
		_aiLook = lookLabel.value;
		aiNameLabel.Open();
	}
	private void OnLookBack() {
		lookLabel.Close();
		genderLabel.Open();
	}

	private async void OnAINameAccepted() {
		_aiName = aiNameLabel.value;
		await DatabaseApi.instance.SetAvatarData(new Avatar() {name = _aiName, role = DEFAULT_ROLE, lookId = _aiLook.id, gender = _aiLook.gender});
		IntroReadyEvent?.Invoke();
	}

	private void OnAINameBack() {
		aiNameLabel.Close();
		lookLabel.Open();
	}
}