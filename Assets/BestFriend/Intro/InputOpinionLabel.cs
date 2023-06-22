using UnityEngine;

public class InputOpinionLabel : Label {
	[SerializeField] private UIScroller scroller;

	public override string value => scroller.selected.text;
}
