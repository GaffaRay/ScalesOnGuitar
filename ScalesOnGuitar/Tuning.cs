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
        static readonly Dictionary<string, TuningType> DicTunings = new Dictionary<string, TuningType>()
            {
                { "Standard", TuningType.Standard },
                { "Drop D", TuningType.DropD },
                { "Semitone deeper", TuningType.SemiToneDeeper }
            };

        public static string GetDicTuningsKey(int index)
        {
            string[] keys = DicTunings.Keys.ToArray();
            return keys[index];
        }

        public static TuningType GetDicTuningsValue(string key)
        {
            TuningType result;
            DicTunings.TryGetValue(key, out result);
            return result;
        }

    }
}
