using Xunit.Abstractions;

namespace Fantasista.Noix.Tests
{
    public class Perlin1DTest
    {
        private ITestOutputHelper _output;

        public Perlin1DTest(ITestOutputHelper output)
        {
            _output = output;
        }

        public void TestThatItCreatesValidValues()
        {
            var width = 200;
            var perlin1d = new Perlin1D(50, width, new DefaultRandomGenerator(1), new SmoothStepInterpolater());
            var perlinValues = perlin1d.GetValues(0, width).ToArray();
            foreach (var val in perlinValues)
            {
                _output.WriteLine($"{val}");
            }
            Assert.Equal(perlinValues.Length, width);
            Assert.Equal(perlinValues[0], -0.5026628375053406);
            Assert.Equal(perlinValues[width - 1], 0.3153080003128052);
        }


    }
}