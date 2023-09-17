using System.Diagnostics;
using Xunit.Abstractions;

namespace Fantasista.Noix.Tests
{
    public class Perlin2DTest
    {
        private ITestOutputHelper _output;

        public Perlin2DTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void TestThatItCreatesValidValues()
        {
            var width = 800;
            var height = 600;
            var perlin2d = new Perlin2D(100, width, height, new DefaultRandomGenerator(1), new SmoothStepInterpolater());
            var watch = new Stopwatch();
            using (var writer = new BinaryWriter(File.Open("/home/vegardb/perlin.data", FileMode.Create)))
            {
                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        var val = perlin2d.GetValue(x, y);
                        var valB = (Byte)(((val + 1) / 2) * 255);
                        writer.Write(valB);
                    }
                }
            }
            watch.Stop();

        }

    }
}