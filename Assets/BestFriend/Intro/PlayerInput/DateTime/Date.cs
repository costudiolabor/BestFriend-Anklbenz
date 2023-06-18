using System;
using System.Collections.Generic;

public class Date {
	private const int START_YEAR = 1950;

	public IEnumerable<string> GetYears() {
		var years = new List<string>();
		var currentYear = DateTime.Now.Year;

		for (int i = START_YEAR; i < currentYear; i++)
			years.Add(i.ToString());

		return years;
	}

	public IEnumerable<string> GetMonths() =>
			new[] {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"};
	//	new[] {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};


	public int DayInMonth(int year, int month) =>
			DateTime.DaysInMonth(year, month);


	public IEnumerable<string> GetDayArray(int dayCount) {
		var list = new List<string>();

		for (var i = 1; i <= dayCount; i++)
			list.Add(i.ToString());

		return list;
	}
}