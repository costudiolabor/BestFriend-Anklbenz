using System;
using System.Linq;
using UnityEngine;

public class InputDate : InputData<DateTime> {
	[SerializeField] private UIScroller daysScroll, monthScroll, yearsScroll;
	private readonly Date _date = new();
	private string[] _yearsNames, _monthNames, _dayNames;

	public override void Initialize() {
		base.Initialize();
		_yearsNames = _date.GetYears().ToArray();
		_monthNames = _date.GetMonths().ToArray();

		yearsScroll.AddItems(_yearsNames);
		monthScroll.AddItems(_monthNames);
		daysScroll.AddItems(_date.GetDayArray(31).ToArray());

		yearsScroll.SelectedEvent += UpdateDays;
		monthScroll.SelectedEvent += UpdateDays;
	}
	
	protected override void OnSubmit() =>
		inputCompletionSource.TrySetResult(GetDate());

	private void UpdateDays() {
		var year = yearsScroll.selected;

		var currentValue = daysScroll.GetSelectedIndex() + 1;
		var daysCount = _date.DayInMonth(Int32.Parse(year.text), monthScroll.GetSelectedIndex() + 1);

		if (currentValue > daysCount)
			daysScroll.SelectByIndex(daysCount - 1);

		daysScroll.ShowRange(0, daysCount + 1);
	}

	private DateTime GetDate() {
		var dateString = daysScroll.selected.text + monthScroll.selected.text + yearsScroll.selected.text;
		return DateTime.Parse(dateString);
	}
}