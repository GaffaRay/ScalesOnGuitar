using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalesOnGuitar
{
    public enum ToneMode
    {
        Major,
        Minor
    };

    public enum AllBaseNotes
    {
        C, Cis, D, Dis, E, F, Fis, G, Gis, A, Ais, H
    };

    public class Scale
    {
        string baseNote;
        ToneMode t;

        //List<string> tonesOfScale = new List<string>();
        public List<string> wantedScale = new List<string>();
        string[] allNotes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "H" };

        //int[] majorMode = { 0, 2, 4, 5, 7, 9, 11 };
        //int[] minorMode = { 0, 2, 3, 5, 7, 8, 10 };

        Dictionary<ToneMode, List<int>> ScaleModes = new Dictionary<ToneMode, List<int>>()
        {
            { ToneMode.Major, new List<int> { 0, 2, 4, 5, 7, 9, 11 } },
            { ToneMode.Minor, new List<int> { 0, 2, 3, 5, 7, 8, 10 } }
        };
            
        //new Dictionary<ToneMode, List<int>>(); //{ { ToneMode.Major, new int[] { 0, 2, 4, 5, 7, 9, 11 } }; 

        List<int> interimsMode;

        public void GenerateScale(ToneMode tonemode)
        {
            interimsMode = ScaleModes[tonemode];

            int indexOfFirstNote = Array.IndexOf(allNotes, baseNote);
            int indexOfNote;

            for (int i = 0; i < interimsMode.Count; i++)
            {
                indexOfNote = indexOfFirstNote + interimsMode[i] >= allNotes.Length 
                              ? indexOfFirstNote + interimsMode[i] - allNotes.Length 
                              : indexOfFirstNote + interimsMode[i];

                wantedScale.Add(allNotes[indexOfNote]);
            }

        }

        public Scale(string _baseNote, ToneMode tonemode)
        {
            baseNote = _baseNote;
            t = tonemode;

            GenerateScale(t);
        }

    }

   
}
