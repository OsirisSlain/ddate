namespace ddate
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using static NumToEng;

	class DateFormatter
	{
		private readonly DiscordianDate _ddate;
		public DateFormatter(DiscordianDate ddate)
		{
			_ddate = ddate;
		}

		public string GetStandardDate()
		{
			var fullDayName = _ddate.IsStTibsDay
				? DiscordianDate.StTibsDay.Name
				: String.Format("{0}, the {2} day of {1}", _ddate.DayName, _ddate.SeasonName, ToRankedNumeric(_ddate.DayOfSeason));

			Console.WriteLine("");
			const string dateFormat = "Today is {0}, in the Year of Our Lady of Discord {1}";
			return String.Format(dateFormat, fullDayName, ToEnglishWords(_ddate.Year));
		}

		public string GetStandardHolyDays()
		{
			return "It is " + EnglishJoin(_ddate.TodaysHolyDays);
		}

		private string EnglishJoin(IEnumerable<string> words)
		{
			if (words == null)
				return String.Empty;
			var actualWords = words.Where(x => !String.IsNullOrWhiteSpace(x)).ToList();

			switch (actualWords.Count)
			{
				case 0:
					return String.Empty;
				case 1:
					return actualWords.First();
				case 2:
					return String.Join(" and ", actualWords);
				default:
					var lastWord = actualWords.Last();
					actualWords.RemoveAt(actualWords.Count - 1);
					return String.Join(", ", actualWords) + ", and " + lastWord;
			}
		}
	}
}