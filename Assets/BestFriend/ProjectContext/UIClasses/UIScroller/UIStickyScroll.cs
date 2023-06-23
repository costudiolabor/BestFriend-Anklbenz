using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIStickyScroll : ScrollRect {
	private const float STICK_START_VELOCITY = 10f;
	private const float OFFSET_ACCURACY = 0.05f;
	
	public event Action SelectedEvent;
	public RectTransform lastSelected { get; private set; }
	private float smoothSpeed => elasticity / 4;
	private bool _isStickNeeded, _isDrag, _isMoveSmoothNeeded;

	private void FixedUpdate() {
		if (_isDrag) return;

		if (_isMoveSmoothNeeded)
			_isMoveSmoothNeeded = !MoveToSmooth(lastSelected);

		if (Mathf.Abs(velocity.y) > STICK_START_VELOCITY) return;

		if (!_isStickNeeded) return;

		lastSelected = GetClosestContentItem();
		_isMoveSmoothNeeded = true;

		_isStickNeeded = false;
		SetSelectedAndNotify(lastSelected);
	}

	public void MoveTo(RectTransform destination) {
		var offset = (Vector2)transform.position - (Vector2)destination.position;
		var position = content.anchoredPosition;
		position.y += offset.y;
		SetContentAnchoredPosition(position);

		SetSelectedAndNotify(destination);
	}

	private bool MoveToSmooth(RectTransform destination) {
		var offset = (Vector2)transform.position - (Vector2)destination.transform.position;
		var speed = 1f;
		var deltaTime = Time.unscaledDeltaTime;
		var position = content.anchoredPosition;

		if (Mathf.Abs(offset[1]) > OFFSET_ACCURACY ) {
			position.y = Mathf.SmoothDamp(position.y, position.y + offset[1], ref speed, smoothSpeed, Mathf.Infinity, deltaTime);
			SetContentAnchoredPosition(position);
			return false;
		} 
		
		return true;
	}

	private RectTransform GetClosestContentItem() {
		RectTransform closest = null;
		float minDistance = -1;

		foreach (RectTransform rect in content) {
			var distance = Vector2.Distance(transform.position, rect.transform.position);

			if (distance > minDistance && minDistance > 0) continue;
			minDistance = distance;
			closest = rect;
		}

		return closest;
	}

	private void SetSelectedAndNotify(RectTransform item) {
		lastSelected = item;
		SelectedEvent?.Invoke();
	}

	public override void OnEndDrag(PointerEventData eventData) {
		base.OnEndDrag(eventData);
		_isStickNeeded = true;
		_isDrag = false;
	}

	public override void OnDrag(PointerEventData eventData) {
		base.OnDrag(eventData);
		_isDrag = true;
		_isMoveSmoothNeeded = false;
	}
}