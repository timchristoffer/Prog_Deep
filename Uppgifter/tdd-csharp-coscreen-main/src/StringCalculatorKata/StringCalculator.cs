namespace StringCalculatorKata
{
    public class StringCalculator
    {
        public static int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            // Dela upp strängen med kommatecken och ny rad-tecken som avgränsare
            string[] numberArray = numbers.Split(new char[] { ',', '\n' });

            int sum = 0;
            foreach (string num in numberArray)
            {
                // Omvandla varje delsträng till ett heltal och lägg till i summan
                sum += int.Parse(num);
            }

            return sum;
        }
    }
}
