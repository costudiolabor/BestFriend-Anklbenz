using UnityEngine;

public class InputOpinionLabel : Label<string> {
	[SerializeField] private UIScroller scroller;

	public override string value => scroller.selected.text;
}
