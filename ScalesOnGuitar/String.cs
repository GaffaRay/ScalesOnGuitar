using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalesOnGuitar
{
    public class String
    {
        string baseNote;
        List<string> notesOnFretBoard = new List<string>();
        int numbersOfNotesOnFretboard = 17;

        string[] allNotes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "H" };

        public String(AllBaseNotes _baseNote)
        {
            baseNote = _baseNote.ToString();

            int firstPosition = Array.IndexOf(allNotes, baseNote);
            int interimsPosition;

            for (int i = 0; i < numbersOfNotesOnFretboard; i++)
            {
                interimsPosition = firstPosition + i;

                while (interimsPosition > (allNotes.Length - 1))
                {
                    interimsPosition -= (allNotes.Length);
                }

                //while (interimsPosition > (allNotes.Length - 1))
                //{
                //    interimsPosition -= (allNotes.Length - 1);
                //}

                notesOnFretBoard.Add(allNotes[interimsPosition]);
            }
        }

        public string GetNoteFromString(int i)
        {
            string result;

            result = notesOnFretBoard[i];
            return result;

        }

    }
}
