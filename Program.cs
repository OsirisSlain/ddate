using System;
using System.Linq;

namespace ddate
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine( Environment.NewLine + GetFormattedDate(DateTime.Now) + Environment.NewLine );
		}

		private static string GetFormattedDate(DateTime date)
		{
			var ddate = new DiscordianDate(date);
			var dateFormatter = new DateFormatter(ddate);

			bool isWatchedOver = new Random(date.DayOfYear + (int)date.DayOfWeek).Next() % 5 == 0;
			return dateFormatter.GetStandardDate()
				+ (ddate.TodaysHolyDays.Any() ? Environment.NewLine + dateFormatter.GetStandardHolyDays() : null)
				+ (isWatchedOver ? Environment.NewLine + ddate.PatronApostle + " watches over you" : null);
		}
	}
}