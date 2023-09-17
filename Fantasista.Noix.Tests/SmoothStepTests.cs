namespace Fantasista.Noix.Tests;

public class SmoothStepTests
{
    [Fact]
    public void TestKnownValues()
    {
        var smooothStep = new SmoothStepInterpolater();
        Assert.Equal(0.5, smooothStep.Interpolate(100.0f, 200, 150));
        Assert.Equal(0.64800000190734863, smooothStep.Interpolate(0, 1, 0.6f));

    }
}