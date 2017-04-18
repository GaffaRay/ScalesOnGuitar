using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalesOnGuitar
{
    public enum TuningType
    {
        Standard,
        DropD,
        SemiToneDeeper
    };

    public static class Tuning
    {
        static Dictionary<string, TuningType> _dicTunings = new Dictionary<string, TuningType>()
            {
                { "Standard", TuningType.Standard },
                { "Drop D", TuningType.DropD },
                { "Semitone deeper", TuningType.SemiToneDeeper }
            };

        public static string GetDicTuningsKey(int index)
        {
            string[] keys = _dicTunings.Keys.ToArray();
            return keys[index];
        }

        public static TuningType GetDicTuningsValue(string key)
        {
            TuningType result;
            _dicTunings.TryGetValue(key, out result);
            return result;
        }

    }
}
