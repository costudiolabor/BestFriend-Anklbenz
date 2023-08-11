using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageDate : AnimatedView {
	[SerializeField] private TMP_Text dateText;

	public void Set(string value) {
		dateText.text = value;
	}
}
