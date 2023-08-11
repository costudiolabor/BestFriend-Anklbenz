using System;
using System.Linq;
using UnityEngine;

public class InputLookLabel : Label<Look> {
	[SerializeField] private LookItem[] lookItems;
	
	public override Look value =>_value ;
	
	private Look _value = new();

	private void Awake() {
		foreach (var item  in lookItems) 
			item.ValueChangedEvent += OnValueChanged;
		acceptButton.interactable = false;
	}
	private void OnValueChanged(bool itemIsOn = false) {
		var selectedItem = lookItems.FirstOrDefault(x => x.isSelected);
		acceptButton.interactable = selectedItem != null;
		
		if (selectedItem == null) return;
		
		_value.gender = selectedItem.genderValue;
		_value.id = selectedItem.idValue;
	}
}