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

        readonly string[] _allNotes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "H" };

        public String(AllBaseNotes _baseNote)
        {
            baseNote = _baseNote.ToString();

            int firstPosition = Array.IndexOf(_allNotes, baseNote);

            for (int i = 0; i < numbersOfNotesOnFretboard; i++)
            {
                var interimsPosition = firstPosition + i;

                while (interimsPosition > (_allNotes.Length - 1))
                {
                    interimsPosition -= (_allNotes.Length);
                }

                notesOnFretBoard.Add(_allNotes[interimsPosition]);
            }
        }

        public string GetNoteFromString(int i)
        {
            var result = notesOnFretBoard[i];
            return result;
        }

    }
}
