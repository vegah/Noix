namespace Fantasista.Noix;

public interface IInterpolater
{
    float Interpolate(float leftVal, float rightVal, float realVal);
}