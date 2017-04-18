using ScalesOnGuitar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalesOnGuitar
{
    enum InstrumentType
    {
        Guitar,
        Bass
    }

    class Instrument
    {
        public InstrumentType instrumentType;
        public int numberOfStrings;
        public TuningType tuning;

        public List<String> stringsOfInstrument = new List<String>();

        public Instrument(InstrumentType type, int _numberOfStrings, TuningType _tuning)
        {
            instrumentType = type;
            numberOfStrings = _numberOfStrings;
            tuning = _tuning;

            switch (instrumentType)
            {
                case InstrumentType.Guitar:
                    switch (numberOfStrings)
                    {
                        case 6:
                            stringsOfInstrument.Add(new String(AllBaseNotes.E));
                            stringsOfInstrument.Add(new String(AllBaseNotes.A));
                            stringsOfInstrument.Add(new String(AllBaseNotes.D));
                            stringsOfInstrument.Add(new String(AllBaseNotes.G));
                            stringsOfInstrument.Add(new String(AllBaseNotes.H));
                            stringsOfInstrument.Add(new String(AllBaseNotes.E));
                            break;
                        case 7:
                            stringsOfInstrument.Add(new String(AllBaseNotes.H));
                            goto case 6;
                    }
                    break;
                case InstrumentType.Bass:
                    switch (numberOfStrings)
                    {
                        case 4:
                            stringsOfInstrument.Add(new String(AllBaseNotes.E));
                            stringsOfInstrument.Add(new String(AllBaseNotes.A));
                            stringsOfInstrument.Add(new String(AllBaseNotes.D));
                            stringsOfInstrument.Add(new String(AllBaseNotes.G));
                            break;
                        case 5:
                            stringsOfInstrument.Add(new String(AllBaseNotes.H));
                            goto case 4;
                        case 6:
                            stringsOfInstrument.Add(new String(AllBaseNotes.H));
                            stringsOfInstrument.Add(new String(AllBaseNotes.E));
                            stringsOfInstrument.Add(new String(AllBaseNotes.A));
                            stringsOfInstrument.Add(new String(AllBaseNotes.D));
                            stringsOfInstrument.Add(new String(AllBaseNotes.G));
                            stringsOfInstrument.Add(new String(AllBaseNotes.C));
                            break;
                    }
                    break;
            }
        }

        public Instrument(InstrumentType _type, int _numberOfStrings)
        {
            instrumentType = _type;
            numberOfStrings = _numberOfStrings;
        }
    }
}
