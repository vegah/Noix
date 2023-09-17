namespace Fantasista.Noix
{
    public class SmoothStepInterpolater : IInterpolater
    {
        public float Interpolate(float leftVal, float rightVal, float realVal)
        {
            var edgeDiff = rightVal - leftVal;
            var realValBetween0And1 = (realVal - leftVal) / edgeDiff;
            return realValBetween0And1 * realValBetween0And1 * (3 - 2 * realValBetween0And1);
        }
    }
}