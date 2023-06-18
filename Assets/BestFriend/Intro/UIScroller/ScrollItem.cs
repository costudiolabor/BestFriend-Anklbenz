using TMPro;
using UnityEngine;
[RequireComponent(typeof(RectTransform))]
public  class ScrollItem : Item<string> {
	[SerializeField] private TMP_Text tmpText;
	[SerializeField] private RectTransform rect;
	public bool isEnabled {
		get => gameObject.activeInHierarchy;
		set => gameObject.SetActive(value);
	}

	public string text {
		get => tmpText.text;
		set => tmpText.text = value;
	}
	
	public override void SetData(string data) {
		tmpText.text = data;
		name = data;
	}
}