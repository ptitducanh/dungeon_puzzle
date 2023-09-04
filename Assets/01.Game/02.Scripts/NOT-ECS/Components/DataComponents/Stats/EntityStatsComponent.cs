using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Scripts.Components.DataComponents.Stats
{
    public class EntityStatsComponent : DataComponent
    {
        protected Dictionary<string, object> stats = new();

        #region public functions

        #region int stats
        public int GetIntStat(string name)
        {
            if (stats.TryGetValue(name, out var stat) && stat is IntStat intStat)
            {
                return intStat.Value;
            }
            
            return IntStat.DefaultValue;
        }
        
        public void UpdateIntStat(string name, int offsetValue)
        {
            if (stats.TryGetValue(name, out var stat) && stat is IntStat intStat)
            {
                intStat.Value += offsetValue;
            }
        }
        
        public void SetIntStat(string name, int value)
        {
            if (stats.TryGetValue(name, out var stat) && stat is IntStat intStat)
            {
                intStat.Value = value;
            }
        }
        #endregion

        #region float stats
        public float GetFloatStat(string name)
        {
            if (stats.TryGetValue(name, out var stat) && stat is FloatStat floatStat)
            {
                return floatStat.Value;
            }
            
            return FloatStat.DefaultValue;
        }
        
        public void UpdateFloatStat(string name, float offsetValue)
        {
            if (stats.TryGetValue(name, out var stat) && stat is FloatStat floatStat)
            {
                floatStat.Value += offsetValue;
            }
        }
        
        public void SetFloatStat(string name, float value)
        {
            if (stats.TryGetValue(name, out var stat) && stat is FloatStat floatStat)
            {
                floatStat.Value = value;
            }
        }
        #endregion
        
        
        #region string stats
        public string GetStringStat(string name)
        {
            if (stats.TryGetValue(name, out var stat) && stat is StringStat stringStat)
            {
                return stringStat.Value;
            }
            
            return StringStat.DefaultValue;
        }
        #endregion
        
        #endregion

        #region private functions
        private BaseStat<T> GetStat<T>(string name)
        {
            return stats[name] as BaseStat<T>;
        }
        
        protected void AddIntStat(string name, int value)
        {
            stats.Add(name, new IntStat() { Name = name, Value = value });
        }
        
        protected void AddFloatStat(string name, float value)
        {
            stats.Add(name, new FloatStat() { Name = name, Value = value });
        }
        
        protected void AddStringStat(string name, string value)
        {
            stats.Add(name, new StringStat() { Name = name, Value = value });
        }

        #endregion
    }
}