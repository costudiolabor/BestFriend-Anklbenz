using System.Linq;
using UnityEngine;

public class InputLookLabel : Label {
	[SerializeField] private LookItem[] lookItems;
	
	public override string value =>_value ;
	
	private string _value = string.Empty;

	private void Awake() {
		foreach (var item  in lookItems) 
			item.ValueChangedEvent += OnValueChanged;
		acceptButton.interactable = false;
	}
	private void OnValueChanged(bool itemIsOn = false) {
		var anyIsOn = lookItems.FirstOrDefault(x => x.isSelected);
		acceptButton.interactable = anyIsOn != null;
		if (anyIsOn != null)
			_value = anyIsOn.value;
	}
}