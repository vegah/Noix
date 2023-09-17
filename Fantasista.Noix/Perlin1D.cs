namespace Fantasista.Noix;
public class Perlin1D
{
    private int _squareSize;
    private readonly IRandomGenerator _randomGenerator;
    private readonly float[] _scalarValues;
    private readonly IInterpolater _interpolater;

    public Perlin1D(int squareSize, int width) : this(squareSize, width, new DefaultRandomGenerator(), new SmoothStepInterpolater()) { }

    public Perlin1D(int squareSize, int width, IRandomGenerator randomGenerator, IInterpolater interpolater)
    {
        _randomGenerator = randomGenerator;
        _squareSize = squareSize;
        _scalarValues = GenerateScalarValues(width / squareSize + 1);
        _interpolater = interpolater;
    }

    public double GetValue(int x)
    {
        var index = x / _squareSize;
        var value1 = _scalarValues[index];
        var value2 = _scalarValues[index + 1];
        var weight = _interpolater.Interpolate(index * _squareSize, (index + 1) * _squareSize, x);
        return value1 * (1 - weight) + value2 * weight;
    }

    public IEnumerable<double> GetValues(int fromX, int toX)
    {
        for (var x = fromX; x < toX; x++)
        {
            yield return GetValue(x);
        }
    }

    private float[] GenerateScalarValues(int number)
    {
        return Enumerable.Range(0, number).Select(i => _randomGenerator.GetNext()).ToArray();
    }
}
