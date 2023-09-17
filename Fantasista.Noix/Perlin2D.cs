using System.Numerics;
namespace Fantasista.Noix
{
    public class Perlin2D
    {
        private readonly IRandomGenerator _randomGenerator;
        private readonly int _squareSize;
        private readonly int _width;
        private readonly int _height;
        private readonly Vector2[] _scalarValues;
        private readonly IInterpolater _interpolater;
        const float EPSILON = 0.00001f;

        public Perlin2D(int squareSize, int width, int height) : this(squareSize, width, height, new DefaultRandomGenerator(), new SmoothStepInterpolater()) { }

        public Perlin2D(int squareSize, int width, int height, IRandomGenerator randomGenerator, IInterpolater interpolater)
        {
            _randomGenerator = randomGenerator;
            _squareSize = squareSize;
            _width = width;
            _height = height;
            _scalarValues = GenerateScalarValues((width * height / squareSize) + 1);
            _interpolater = interpolater;
        }

        public IEnumerable<float> GetValues(int x, int y, int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    yield return GetValue(x + i, y + j);
                }
            }
        }

        public float GetValue(int x, int y)
        {
            var newX = (float)x / _squareSize;
            var newY = (float)y / _squareSize;
            var xindex1 = (int)Math.Floor(newX);
            var xindex2 = xindex1 + 1;
            var yindex1 = (int)Math.Floor(newY);
            var yindex2 = yindex1 + 1;
            var deltax = newX - xindex1;
            var deltay = newY - yindex1;
            var distanceVector1 = new Vector2(deltax, deltay);
            var distanceVector2 = new Vector2(deltax - 1, deltay);
            var distanceVector3 = new Vector2(deltax, deltay - 1);
            var distanceVector4 = new Vector2(deltax - 1, deltay - 1);
            // Console.WriteLine($"x : {x}, y : {y} - Deltax: {deltax} - Deltay : {deltay} - delta: {xindex1}");

            var dot1 = Vector2.Dot(distanceVector1, _scalarValues[xindex1 + (yindex1 * _squareSize)]);
            var dot2 = Vector2.Dot(distanceVector2, _scalarValues[xindex2 + (yindex1 * _squareSize)]);
            var dot3 = Vector2.Dot(distanceVector3, _scalarValues[xindex1 + (yindex2 * _squareSize)]);
            var dot4 = Vector2.Dot(distanceVector4, _scalarValues[xindex2 + (yindex2 * _squareSize)]);

            var weight1 = _interpolater.Interpolate(0, _squareSize, deltax * _squareSize);
            var weight2 = _interpolater.Interpolate(0, _squareSize, deltay * _squareSize);
            var val1 = (1 - weight1) * dot1 + weight1 * dot2;
            // Console.WriteLine($"Dot1: {dot1} - Dot2: {dot2} - {val1} - {weight1}");

            var val2 = (1 - weight1) * dot3 + weight1 * dot4;
            return (1 - weight2) * val1 + weight2 * val2;
        }


        private Vector2[] GenerateScalarValues(int number)
        {
            return Enumerable.Range(0, number)
                .Select(i => new Vector2(_randomGenerator.GetNext(), _randomGenerator.GetNext()))
                // .Select(i => Vector2.Normalize(i))
                .ToArray();
        }


    }
}