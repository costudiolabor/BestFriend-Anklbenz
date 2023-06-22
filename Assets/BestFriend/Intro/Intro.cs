using UnityEngine;

public class Intro : MonoBehaviour {
	[SerializeField] private InputTextLabel nameLabel;
	[SerializeField] private InputDateLabel birthdateLabel;	
	[SerializeField] private InputOpinionLabel  genderLabel;

	private string _playerName, _playerBirthdate, _playerGender;
	
	private  void Awake() {
		nameLabel.AcceptedEvent += OnNameAccepted;
		birthdateLabel.AcceptedEvent += OnBirthAccepted;
		birthdateLabel.BackEvent += OnBirthBack;
		genderLabel.AcceptedEvent += OnGenderSelected;
		genderLabel.BackEvent += OnGenderBack;
		
		// Check in session cache has data or not
	//	nameLabel.Open();
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
	private void OnGenderSelected() {
		genderLabel.Close();
		_playerGender = genderLabel.value;
		// gotoAvatarSettings
	}
	private void OnGenderBack() {
		genderLabel.Close();
		birthdateLabel.Open();
	}
}