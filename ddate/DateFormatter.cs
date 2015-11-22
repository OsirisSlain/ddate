namespace ddate {
using System;
using System.Collections.Generic;
using System.Linq;

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
		if (words == null || !words.Any())
				return null;
		switch (words.Count())
		{
			case 1:
				return words.First();
			case 2:
				return String.Join(" and ", words);
			default:
				return String.Join(", ", words.Where(x => x != words.Last())) + ", and " + words.Last();
		}
	}

	private static readonly string[] Ones = { null, "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
	private static readonly string[] Teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
	private static readonly string[] Tens = {null, null, "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
	private static readonly string[] BigNumbers = {"Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion" };

	public static string ToEnglishWords(long number)
	{
		var words = new List<string>();
		bool isNegative = number < 0;

		GetValue(number, words);
		for (int ii = 3; ii <= BigNumbers.Length*3; ii += 3)
		{
			long pow = PowTen(ii);
			if (number/pow > 0)
			{
				var bigText = BigNumbers[ii/3 - 1];
				words.Add(bigText);
				if (!GetValue(number/pow, words))
					words.Remove(bigText);
			}
		}

		words.RemoveAll(x => x == null);
		words.Reverse();
		return (isNegative ? "negative " : null) + String.Join("-", words);
	}

	private static long PowTen(long x)
	{
		long y = 10;
		for (int ii = 1; ii < x; ii++)
			y *= 10; 
		return y;
	}

	private static bool GetValue(long number, List<string> words)
	{
		var wordCount = words.Count(x => x != null);
		if (number == 0) words.Add("zero");
		number = Math.Abs(number);
		var ones = number%10;
		number /= 10; //tens
		if (number > 0)
		{
			var tens = number%10;
			if (tens == 1)
			{
				words.Add(Teens[ones]);
			}
			else
			{
				words.Add(Ones[ones]);
				words.Add(Tens[tens]);
			}
		}
		else
		{
			words.Add(Ones[ones]);
		}
		number /= 10; //hundreds
		if (number > 0)
		{
			if (number%10 != 0)
			{
				words.Add("hundred");
				words.Add(Ones[number%10]);
			}
		}
		return wordCount != words.Count(x => x != null);
	}

	private string ToRankedNumeric(int number)
	{
		return number + GetRankSuffix(number);
	}

	private string GetRankSuffix(int number)
	{
		if ((Math.Abs(number) / 10) % 10 == 1)
			return "th";

		switch (Math.Abs(number) % 10)
		{
			case 1:
				return "st";
			case 2:
				return "nd";
			case 3:
				return "rd";
			default:
				return "th";
		}
	}
}
}