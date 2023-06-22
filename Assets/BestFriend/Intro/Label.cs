using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Label : AnimatedView {
	[SerializeField] protected Button acceptButton, backButton;

	public event Action AcceptedEvent, BackEvent;
	public abstract string value { get; }
	
	private void BackNotify() =>
			BackEvent?.Invoke();
	private void AcceptNotify() =>
			AcceptedEvent?.Invoke();
	
	protected virtual void OnEnable() {
		acceptButton.onClick.AddListener(AcceptNotify);
		backButton.onClick.AddListener(BackNotify);
	}
	
	protected virtual void OnDisable() {
		acceptButton.onClick.RemoveListener(AcceptNotify);
		backButton.onClick.RemoveListener(BackNotify);
	}
}