namespace ddate
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class DiscordianDate
	{
		private readonly DateTime _date;
		private readonly int _modifiedDayOfYear;
		public DiscordianDate(DateTime gregorianDate)
		{
			_date = gregorianDate;

			// St. Tib's day is not considered part of the year for day of week and month calculations
			_modifiedDayOfYear = (DateTime.IsLeapYear(_date.Year) && _date.Month > StTibsDay.Month)
				? _date.DayOfYear - 1
				: _date.DayOfYear;
		}

		const int YearOffset = 1166;
		public int Year => _date.Year + YearOffset;

		public const int DaysInWeek = 5;
		public int DayOfWeek => GetDayOfPeriod(DaysInWeek);
		public int WeekOfYear => GetIterationOfPeriod(DaysInWeek);

		public static string[] DayNames = { "Sweetmorn", "Boomtime", "Pungenday", "Prickle-Prickle", "Setting Orange" };
		public string DayName => DayNames[DayOfWeek - 1];

		public const int DaysInSeason = 73;
		public int DayOfSeason => GetDayOfPeriod(DaysInSeason);
		public int Season => GetIterationOfPeriod(DaysInSeason);

		public static string[] SeasonNames = {"Chaos", "Discord", "Confusion", "Bureaucracy", "The Aftermath"};
		public string SeasonName => SeasonNames[Season - 1];

		// each season has a patron apostle
		public static string[] ApostleNames = {"Hung Mung", "Dr. Van Van Mojo", "Sri Syadasti", "Zarathud", "The Elder Malaclypse"};
		public string PatronApostle => ApostleNames[Season - 1];

		// day of the week and day of the month are both meaningless on St. Tib's day
		public static HolyDay StTibsDay = new HolyDay("St. Tib's Day", 2, 29);
		public bool IsStTibsDay => _date.Month == StTibsDay.Month && _date.Day == StTibsDay.DayOfMonth;

		public const byte SeasonalHolyDay = 5; // the 5th of each season is a seasonal holy day
		public static string[] SeasonalHolyDays = {"Chaoflux", "Discoflux", "Confuflux", "Bureflux", "Afflux"};

		public const byte ApostleHolyDay = 50; // the 50th of each season is an apostle's holy day
		public static string[] ApostleHolyDays = {"Mungday", "Mojoday", "Syaday", "Zaraday", "Maladay"};

		public static IEnumerable<HolyDay> UnofficialHolyDays = new[]
		{
			//these are defined using the corresponding gregorian month and gregorian day of month
			new HolyDay("Discordians for Jesus / Love Your Neighbor Day", 3, 25),
			new HolyDay("Jake Day", 4, 6),
			new HolyDay("Jake Day (maybe)", 5, 23), //yes, there are two jake days... sometimes
			new HolyDay("St Camping's Day", 5, 21),
			new HolyDay("Towel Day", 5, 25),
			new HolyDay("Mid Year's Day", 7, 2),
			new HolyDay("X-Day", 7, 5),
			new HolyDay("Multiversal Underwear Day", 8, 10),
		};

		public IEnumerable<string> TodaysHolyDays
		{
			get
			{
				var holyDays = new List<string>();

				if (IsStTibsDay)
					holyDays.Add(StTibsDay.Name);

				if (DayOfSeason == SeasonalHolyDay)
					holyDays.Add(ApostleHolyDays[Season - 1]);

				if (DayOfSeason == ApostleHolyDay)
					holyDays.Add(SeasonalHolyDays[Season - 1]);

				var unofficialHolyDays = UnofficialHolyDays
					.Where(x => x.Month == _date.Month && x.DayOfMonth == _date.Day)
					.Select(x => x.Name + " (unofficial)").ToList();
				holyDays.AddRange(unofficialHolyDays);

				return holyDays;
			}
		}

		private int GetDayOfPeriod(int periodInDays)
		{
			var day = _modifiedDayOfYear % periodInDays;
			// on the last day in a period, day will be 0
			return day == 0 ? periodInDays : day;
		}

		private int GetIterationOfPeriod(int periodInDays)
		{
			return (int)Math.Ceiling(_modifiedDayOfYear / (double)periodInDays);
		}
	}
}