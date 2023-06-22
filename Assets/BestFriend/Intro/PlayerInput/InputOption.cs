using UnityEngine;

public class InputOption : AnimatedView {
	[SerializeField] private UIScroller decisionScroller;

	public string selectedOption => decisionScroller.selected.text;
	
	public void AddItems(string[] opinions) {
		decisionScroller.AddItems(opinions);
	}
	
}