namespace BloodDonor.Mvc.Extension
{
    public static class IntExtensions
    {
        public static string MethodNotExistInDefaultIntType(this int number)
        {
            if (number < 0) throw new ArgumentOutOfRangeException(nameof(number), "Number must be non-negative.");
            if (number % 100 is >= 11 and <= 13)
                return $"{number}th";
            return number switch
            {
                _ when number % 10 == 1 => $"{number}st",
                _ when number % 10 == 2 => $"{number}nd",
                _ when number % 10 == 3 => $"{number}rd",
                _ => $"{number}th"
            };
        }
    }
}
