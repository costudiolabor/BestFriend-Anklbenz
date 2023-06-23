using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class InputDate : AnimatedView {
	[SerializeField] private UIScroller daysScroll, monthScroll, yearsScroll;
	public string selectedDate => /*DateTime.Parse(*/daysScroll.selected.text + monthScroll.selected.text + yearsScroll.selected.text/*)*/;

	private readonly Date _date = new();
	private string[] _yearsNames, _monthNames;

	public void Initialize() {
		_yearsNames = _date.GetYears().ToArray();
		_monthNames = _date.GetMonths().ToArray();

		yearsScroll.AddItems(_yearsNames);
		monthScroll.AddItems(_monthNames);
		daysScroll.AddItems(_date.GetDayArray(31).ToArray());

		yearsScroll.SelectedEvent += UpdateDays;
		monthScroll.SelectedEvent += UpdateDays;
	}

	private void UpdateDays() {
		var year = yearsScroll.selected;

		var currentValue = daysScroll.GetSelectedIndex() + 1;
		var daysCount = _date.DayInMonth(int.Parse(year.text), monthScroll.GetSelectedIndex() + 1);

		if (currentValue > daysCount)
			daysScroll.SelectByIndex(daysCount - 1);

		daysScroll.ShowRange(0, daysCount + 1);
	}
}