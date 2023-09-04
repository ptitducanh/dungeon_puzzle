namespace Scripts.Components.DataComponents.Stats
{
    public abstract class BaseStat<T>
    {
        public string Name;
        public T Value;
    }
    
    public class IntStat : BaseStat<int>
    {
        public const int DefaultValue = int.MinValue;
    }
    
    public class FloatStat : BaseStat<float>
    {
        public const float DefaultValue = float.MinValue;
    }
    
    public class StringStat : BaseStat<string>
    {
       public const string DefaultValue = ""; 
    }
}