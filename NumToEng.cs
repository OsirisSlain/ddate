using System;
using System.Collections.Generic;

namespace ddate
{
	class NumToEng
	{
		private static readonly string[] Ones = { null, "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
		private static readonly string[] Teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
		private static readonly string[] Tens = {null, null, "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
		private static readonly string[] BigNumbers = {"Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion" };

		public static string ToEnglishWords(long number, bool useDash = true)
		{
			if (number == 0) return "Zero";
			var words = new List<string>();
			bool isNegative = number < 0;

			GetOnesToHundreds(number, words);
			for (int ii = 1; ii <= BigNumbers.Length; ii++)
			{
				long pow = number / PowTen(ii*3);
				if (pow < 1) break;

				words.Add(BigNumbers[ii - 1]);
				GetOnesToHundreds(pow, words);
			}

			words.RemoveAll(x => x == null);
			words.Reverse();
			return (isNegative ? "Negative " : null) + String.Join(useDash ? "-" : " ", words);
		}

		public static string ToRankedNumeric(int number)
		{
			return number + GetRankSuffix(number);
		}

		private static long PowTen(long x)
		{
			long y = 10;
			for (int ii = 1; ii < x; ii++)
				y *= 10; 
			return y;
		}

		private static void GetOnesToHundreds(long number, List<string> words)
		{
			if (number == 0) return;
			number = Math.Abs(number);
			var ones = number%10;
			var tens = (number/10)%10;
			var hundreds = (number/100)%10;

			if (tens == 1)
			{
				words.Add(Teens[ones]);
			}
			else
			{
				words.Add(Ones[ones]);
				words.Add(Tens[tens]);
			}
			if (hundreds != 0)
			{
				words.Add("Hundred");
				words.Add(Ones[hundreds]);
			}
		}

		private static string GetRankSuffix(int number)
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
