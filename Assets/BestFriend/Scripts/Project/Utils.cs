using System;

public static class Utils 
{
	public static DateTime UnixTimeToLocalTime(double unixTimeStamp) {
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
		return dateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
	}
}
