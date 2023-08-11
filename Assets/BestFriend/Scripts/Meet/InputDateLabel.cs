using System;
using UnityEngine;

public class InputDateLabel : Label<string> {
	[SerializeField] private InputDate inputDate;

	public override string value => inputDate.selectedDate;

	private void Awake() {
		inputDate.Initialize();
	}

}
