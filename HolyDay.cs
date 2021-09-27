namespace ddate
{
	public class HolyDay
	{
		public string Name;
		public byte Month;
		public byte DayOfMonth;

		public HolyDay(string name, byte gregorianMonth, byte gregorianDayOfMonth)
		{
			Name = name;
			Month = gregorianMonth;
			DayOfMonth = gregorianDayOfMonth;
		}
	}
}