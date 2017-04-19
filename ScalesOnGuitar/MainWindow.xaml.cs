using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScalesOnGuitar
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {        
            InitializeComponent();

            InitializeInstrumentMenu();
            InitializeTuningMenu();
            InitializeToneModeCBox();
            InitializeBaseNoteCBox();

            CreateFretBoard();
        }

        private void InitializeToneModeCBox()
        {
            string[] interimsArray = _dicToneMode.Keys.ToArray();

            for (int i = 0; i < interimsArray.Length; i++)
            {
                CBToneMode.Items.Add(interimsArray[i]);
            }
        }

        private void InitializeBaseNoteCBox()
        {
            for (int i = 0; i < _startNotes.Length; i++)
            {
                CBBaseNote.Items.Add(_startNotes[i]);
            }
            
        }

        private void InitializeInstrumentMenu()
        {
            var interimsList = _dicInstruments.Keys.ToList();
            mIGuitarSix.Header = interimsList[0];
            mIGuitarSeven.Header = interimsList[1];
            mIBassFour.Header = interimsList[2];
            mIBassFive.Header = interimsList[3];
            mIBassSix.Header = interimsList[4];
        }

        private void InitializeTuningMenu()
        {
            mTStandard.Header = Tuning.GetDicTuningsKey(0);
            mTDropD.Header = Tuning.GetDicTuningsKey(1);
        }
        

        private void CreateFretBoard()
        {
            InstrumentType type = InstrumentType.Guitar;
            int numberOfStrings = 6;
            TuningType tuning = TuningType.Standard;

            // Die Schleife geht durch die MenuItems der Instrumente durch und ignoriert den Seperator

            for (int i = 0; i < mInstrumentType.Items.Count; i++)
            {
                try
                {
                    MenuItem item = (MenuItem)mInstrumentType.Items[i];
                    if (item.IsChecked == true)
                    {
                        type = _dicInstruments[item.Header.ToString()].InstrumentType;
                        numberOfStrings = _dicInstruments[item.Header.ToString()].NumberOfStrings;
                        break;
                    }
                }
                catch
                {
                    // ignored
                }
            }

            for (int i = 0; i < mTune.Items.Count; i++)
            {
                try
                {
                    MenuItem item = (MenuItem)mTune.Items[i];
                    if (item.IsChecked == true)
                    {
                        tuning = Tuning.GetDicTuningsValue(item.Header.ToString());
                    }
                }
                catch
                {
                    // ignored
                }
            }

            Instrument firstInstrument = new Instrument(type, numberOfStrings, tuning);
            Scale scale = new Scale(CBBaseNote.SelectedItem.ToString(), _dicToneMode[CBToneMode.SelectedItem.ToString()]);

            for (int i = 0; i < _labelList.Count; i++)
            {
                GridApplication.Children.Remove(_labelList[i]);
            }

            int numberofstrings = firstInstrument.NumberOfStrings;
            int wantedRows = numberofstrings + 1;
            int numberoffrets = 15;
            int wantedColumns = numberoffrets + 1;

            int[] fretMarkers = { 3, 5, 7, 9, 12, 15, 17 };

            double heightOfFretBoard = 170; //200; // Versuchen die Höhe durch eine Funktion zu bekommen
            double heightOfSingleFret = heightOfFretBoard / numberOfStrings; //wantedRows;
            double widhtOfFretBoard = 507; // Versuchen die Breite durch eine Funktion zu bekommen
            double widthOfSingleFret = widhtOfFretBoard / wantedColumns;

            for (int i = 0; i < wantedRows; i++)
            {
               for (int j = 0; j < wantedColumns; j++)
               {
                   Label b = new Label {BorderBrush = Brushes.Black};

                   if (i < wantedColumns - 1)
                    {
                        try
                        {
                            if (scale.wantedScale.Contains(firstInstrument.StringsOfInstrument[i].GetNoteFromString(j)))
                            {
                                b.Content = firstInstrument.StringsOfInstrument[i].GetNoteFromString(j);
                                b.Background = Brushes.DarkGray;

                            }
                            else
                            {
                                b.Content = ""; 
                                b.Background = Brushes.Transparent;  //(SolidColorBrush)new BrushConverter().ConvertFromString("#0306FF");
                            }

                            b.BorderThickness = j == 0 ? new Thickness(0, 0, 3, 1) : new Thickness(0, 0, 1, 1);
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    

                    b.Height = heightOfSingleFret;
                    b.Width = widthOfSingleFret;
                    b.HorizontalContentAlignment = HorizontalAlignment.Center;

                    if (i == numberofstrings)
                    {
                        b.Content = "";
                        b.Background = System.Windows.Media.Brushes.Transparent;
                        b.BorderThickness = new Thickness(0, 0, 0, 1);
                        b.Height = 30;
                    }

                    if (i == numberofstrings && fretMarkers.Contains(j))
                    {
                        b.Content = j;
                    }

                    b.VerticalAlignment = VerticalAlignment.Bottom;
                    b.HorizontalAlignment = HorizontalAlignment.Left;

                    b.HorizontalContentAlignment = HorizontalAlignment.Right;

                    Thickness margin = b.Margin;
                    margin.Left = widthOfSingleFret * j;
                    margin.Bottom = heightOfSingleFret * i;
                    b.Margin = margin;

                    Grid.SetRow(b, 1);
                    GridApplication.Children.Add(b);

                    _labelList.Add(b);
                }
            }

        }

        List<Label> _labelList = new List<Label>();

        public static string[] AllInstruments = {"Guitar", "Bass"};

        private readonly Dictionary<string, Instrument> _dicInstruments = new Dictionary<string, Instrument>()
        {
            { "6 String Guitar", new Instrument(InstrumentType.Guitar, 6) },
            { "7 String Guitar", new Instrument(InstrumentType.Guitar, 7) },
            { "4 String Bass", new Instrument(InstrumentType.Bass, 4) },
            { "5 String Bass", new Instrument(InstrumentType.Bass, 5) },
            { "6 String Bass", new Instrument(InstrumentType.Bass, 6) }
        };

        private readonly Dictionary<string, ToneMode> _dicToneMode = new Dictionary<string, ToneMode>()
        {
            { "Major", ToneMode.Major },
            { "Minor", ToneMode.Minor }
        };

        private readonly string[] _startNotes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "H" };

        private void EnableAllTunes()
        {
            mTStandard.Visibility = Visibility.Visible;
            mTDropD.Visibility = Visibility.Visible;
        }

        private void DisableDropD()
        {
            mTDropD.Visibility = Visibility.Collapsed;
        }

        private void SetStandard()
        {
            mTStandard.IsChecked = true;
        }

        private void UncheckAllTunes()
        {
            mTStandard.IsChecked = false;
            mTDropD.IsChecked = false;
        }

        private void UncheckAllInstruments()
        {
            mIGuitarSix.IsChecked = false;
            mIGuitarSeven.IsChecked = false;
            mIBassFour.IsChecked = false;
            mIBassFive.IsChecked = false;
            mIBassSix.IsChecked = false;
        }

        private void mISixGuitar_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllInstruments();
            EnableAllTunes();
            UncheckAllTunes();
            mIGuitarSix.IsChecked = true;
            SetStandard();
            CreateFretBoard();
        }

        private void mIGuitarSeven_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllInstruments();
            EnableAllTunes();
            UncheckAllTunes();
            mIGuitarSeven.IsChecked = true;
            SetStandard();
            DisableDropD();
            CreateFretBoard();
        }

        private void mIBassFour_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllInstruments();
            EnableAllTunes();
            UncheckAllTunes();
            mIBassFour.IsChecked = true;
            SetStandard();
            CreateFretBoard();
        }

        private void mIBassFive_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllInstruments();
            EnableAllTunes();
            UncheckAllTunes();
            mIBassFive.IsChecked = true;
            SetStandard();
            DisableDropD();
            CreateFretBoard();
        }

        private void mIBassSix_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllInstruments();
            EnableAllTunes();
            UncheckAllTunes();
            mIBassSix.IsChecked = true;
            SetStandard();
            DisableDropD();
            CreateFretBoard();
        }

        private void mTStandard_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllTunes();
            mTStandard.IsChecked = true;
            CreateFretBoard();
        }

        private void mTDropD_Click(object sender, RoutedEventArgs e)
        {
            UncheckAllTunes();
            mTDropD.IsChecked = true;
            CreateFretBoard();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            CreateFretBoard();
        }
    }
}
