namespace Scripts.Common
{
    public class IDGenerator
    {
        private static uint _id = 0;
        public static uint Generate()
        {
            return _id++;
        }
    }
}