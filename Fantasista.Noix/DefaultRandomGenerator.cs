namespace Fantasista.Noix
{
    public class DefaultRandomGenerator : IRandomGenerator
    {
        private int _seed;
        private Random _random;

        public DefaultRandomGenerator() : this(System.DateTime.Now.Millisecond)
        {
        }

        public DefaultRandomGenerator(int seed)
        {
            _seed = seed;
            _random = new Random(_seed);
        }

        public float GetNext()
        {
            return (_random.NextSingle() * 2) - 1;
        }
    }
}