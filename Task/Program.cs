using Task.Methods;

namespace Task
{
    public class Program
    {
        public static (short, short) GetValues(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Input.txt is not found!");

            string text = File.ReadAllText(path);
            short[] vals = text.Split(new[] { ' ', ',', }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => short.TryParse(x, out short value) ? value : throw new FormatException("Each value must be an integer and non-negative!"))
                .ToArray();

            if (vals.Length != 2)
                throw new InvalidDataException("The input file must contain only 2 values of integer type!");
            if (vals.First() < 1 || vals.First() > 20) 
                throw new ArgumentOutOfRangeException("Number of tosses must be in range 1 <= numberOfTosses <= 20");
            if (vals.Last() < 0 || vals.Last() > vals.First()) 
                throw new ArgumentOutOfRangeException($"Number of tails must be in range 0 <= numberOfTails <= numberOfTosses({vals.First()})");
            
            return (vals.First(), vals.Last());
        }

        public static void Main(string[] args)
        {
            string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\input.txt");
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\output.txt");

            try
            {
                (short n, short m) = GetValues(inputPath);
                long result = HeadsAndTails.CalculateNumOfTailsCombinations(n, m);
                
                File.WriteAllText(outputPath, result.ToString());
                Console.WriteLine("Your answer has been recorded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
            }
        }
    }
}