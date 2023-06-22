using UnityEngine;

public enum InputType {
	Text,
	Date,
	Opinion,
	Nothing
}

[System.Serializable]
public class Question {
	[SerializeField] private string question;
	[SerializeField] private string buttonLabel;
	[SerializeField] private string hint;
	[SerializeField] private InputType inputType;
}