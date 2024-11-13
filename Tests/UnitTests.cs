using Task.Methods;

namespace Tests
{
    public class UnitTests
    {
        [Theory]
        [InlineData(2, 0, 4)]
        [InlineData(2, 1, 3)]
        [InlineData(2, 2, 1)]
        [InlineData(3, 0, 8)]
        [InlineData(3, 1, 7)]
        [InlineData(3, 2, 4)]
        public void TestCalculateNumOfTailsCombinations_ValidInput_ReturnResult(ushort tossesNum, ushort tailsNum, long expectedResult)
        {
            Assert.Equal(expectedResult, HeadsAndTails.CalculateNumOfTailsCombinations(tossesNum, tailsNum));
        }

        [Fact]
        public void TestGetValues_ValidPath_ReturnValuesFromTheFile()
        {
            string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\testInput.txt");
            File.WriteAllText(inputPath, "2 0");

            (short res1, short res2) = Task.Program.GetValues(inputPath);

            Assert.Equal(2, res1);
            Assert.Equal(0, res2);
        }

        [Fact]
        public void TestGetValues_InvalidPath_ThrowsFileNotFoundException()
        {
            string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\invalidTestInput.txt");

            Assert.Throws<FileNotFoundException>(() => Task.Program.GetValues(inputPath));
        }

        [Theory]
        [InlineData("a b")]
        [InlineData("3, $")]
        [InlineData(". 2")]
        public void TestGetValues_InvalidTypeOfValue_ThrowsFormatException(string testText)
        {
            string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\testInput.txt");
            File.WriteAllText(inputPath, testText);

            Assert.Throws<FormatException>(() => Task.Program.GetValues(inputPath));
        }

        [Theory]
        [InlineData("0 3")]
        [InlineData("21 5")]
        [InlineData("-1 3")]
        [InlineData("4 12")]
        public void TestGetValues_InvalidArgument_ThrowsArgumentOutOfRangeException(string testText)
        {
            string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\testInput.txt");
            File.WriteAllText(inputPath, testText);

            Assert.Throws<ArgumentOutOfRangeException>(() => Task.Program.GetValues(inputPath));
        }

        [Theory]
        [InlineData("")]
        [InlineData("2 0 1")]
        [InlineData("1")]
        public void TestGetValues_InvalidNumberOfValues_ThrowsInvalidDataException(string testText)
        {
            string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\testInput.txt");
            File.WriteAllText(inputPath, testText);

            Assert.Throws<InvalidDataException>(() => Task.Program.GetValues(inputPath));
        }
    }
}