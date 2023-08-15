namespace UnityEngine.UI {

   public class UIColorToggle : Toggle {
      public Graphic[] firstGraphics ,secondGraphics, thirdGraphics;
      public Color activeFirst = Color.white, 
		      inactiveFirst = Color.white, 
		      activeSecond = Color.white, 
		      inactiveSecond = Color.white, 
		      activeThird = Color.white, 
		      inactiveThird = Color.white;

      protected override void OnEnable(){
         base.OnEnable();
         onValueChanged.AddListener(delegate { ChangeBackgroundColor(); });
         ChangeBackgroundColor();
      }

      protected override void OnDisable(){
         onValueChanged.RemoveListener(delegate { ChangeBackgroundColor(); });
      }

      public void SetStateWithoutNotify(bool value){
         SetIsOnWithoutNotify(value);
         ChangeBackgroundColor();
      }
      
      private void ChangeBackgroundColor() {
	      var firstColor = isOn ? activeFirst : inactiveFirst;
	      var secondColor = isOn ? activeSecond : inactiveSecond;
	      var thirdColor = isOn ? activeThird : inactiveThird;
	    
	      ChangeElementsColor(firstGraphics, firstColor);
	      ChangeElementsColor(secondGraphics, secondColor);
	      ChangeElementsColor(thirdGraphics, thirdColor);
      }
      
      private void ChangeElementsColor(Graphic[] elements, Color color) {
	      if (elements.Length <= 0) return;
	      foreach (var graphicElement in elements) {
		      if (graphicElement is null) continue;
		      graphicElement.color = color;
	      }
      }
   }
}
