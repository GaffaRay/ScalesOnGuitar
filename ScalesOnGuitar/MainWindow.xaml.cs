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


            createFretBoard();

            String s = new String(AllBaseNotes.A);
            label.Content = s.GetNoteFromString(1);
        }

        private void InitializeToneModeCBox()
        {
            string[] interimsArray = dicToneMode.Keys.ToArray();

            for (int i = 0; i < interimsArray.Length; i++)
            {
                CBToneMode.Items.Add(interimsArray[i]);
            }
        }

        private void InitializeBaseNoteCBox()
        {
            for (int i = 0; i < StartNotes.Length; i++)
            {
                CBBaseNote.Items.Add(StartNotes[i]);
            }
            
        }

        private void InitializeInstrumentMenu()
        {
            var interimsList = dicInstruments.Keys.ToList();
            mIGuitarSix.Header = interimsList[0].ToString();
            mIGuitarSeven.Header = interimsList[1];
            mIBassFour.Header = interimsList[2].ToString();
            mIBassFive.Header = interimsList[3];
            mIBassSix.Header = interimsList[4];
        }

        private void InitializeTuningMenu()
        {
            mTStandard.Header = Tuning.GetDicTuningsKey(0);
            mTDropD.Header = Tuning.GetDicTuningsKey(1);
        }

        private void createFretBoard()
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
                        type = dicInstruments[item.Header.ToString()].instrumentType;
                        numberOfStrings = dicInstruments[item.Header.ToString()].numberOfStrings;
                        break;
                    }
                }
                catch
                {

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

                }
            }

            Instrument firstInstrument = new Instrument(type, numberOfStrings, tuning);
            Scale scale = new Scale(CBBaseNote.SelectedItem.ToString(), dicToneMode[CBToneMode.SelectedItem.ToString()]);

            for (int i = 0; i < labelList.Count; i++)
            {
                GridApplication.Children.Remove(labelList[i]);
            }

            

            int numberofstrings = firstInstrument.numberOfStrings;
            int wantedRows = numberofstrings + 1;
            int numberoffrets = 15;
            int wantedColumns = numberoffrets + 1;

            int[] fretMarkers = { 3, 5, 7, 9, 12, 15, 17 };

            double heightOfFretBoard = 200; // Versuchen die Höhe durch eine Funktion zu bekommen
            double heightOfSingleFret = heightOfFretBoard / wantedRows;
            double widhtOfFretBoard = 507; // Versuchen die Breite durch eine Funktion zu bekommen
            double widthOfSingleFret = widhtOfFretBoard / wantedColumns;

            for (int i = 0; i < wantedRows; i++)
            {
                //iterateString = firstInstrument.stringsOfInstrument[i];


                for (int j = 0; j < wantedColumns; j++)
                {
                    Label b = new Label();
                    //String s = new String(AllBaseNotes.A);



                    //b.Content = s.GetNoteFromString(j);
                    if (i < wantedColumns - 1)
                    {
                        try
                        {
                            if (scale.wantedScale.Contains(firstInstrument.stringsOfInstrument[i].GetNoteFromString(j)))
                            {
                                b.Content = firstInstrument.stringsOfInstrument[i].GetNoteFromString(j);
                            }
                            else
                            {
                                b.Content = "";
                            }

                            //b.Content = firstInstrument.stringsOfInstrument[i].GetNoteFromString(j);
                        }
                        catch 
                        {

                        }
                        //b.Content = firstInstrument.stringsOfInstrument[i].GetNoteFromString(j);
                    }
                    

                    b.Height = heightOfSingleFret;
                    b.Width = widthOfSingleFret;
                    b.HorizontalContentAlignment = HorizontalAlignment.Center;
                    b.Background = System.Windows.Media.Brushes.Blue;

                    if (i == numberofstrings)
                    {
                        b.Content = "";
                        b.Background = System.Windows.Media.Brushes.Transparent;
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

                    labelList.Add(b);
                }
            }

        }

        //private void createFretBoard()
        //{
        //    int numberofstrings = 6;
        //    int wantedRows = numberofstrings + 1;
        //    int numberoffrets = 15;
        //    int wantedColumns = numberoffrets + 1;

        //    int[] fretMarkers = { 3, 5, 7, 9, 12, 15, 17 };

        //    double heightOfFretBoard = 200; // Versuchen die Höhe durch eine Funktion zu bekommen
        //    double heightOfSingleFret = heightOfFretBoard / wantedRows;
        //    double widhtOfFretBoard = 507; // Versuchen die Breite durch eine Funktion zu bekommen
        //    double widthOfSingleFret = widhtOfFretBoard / wantedColumns;

        //    for (int i = 0; i < wantedRows; i++)
        //    {
        //        for (int j = 0; j < wantedColumns; j++)
        //        {
        //            Label b = new Label();

        //            b.Content = "Cis";
        //            b.Height = heightOfSingleFret;
        //            b.Width = widthOfSingleFret;
        //            b.HorizontalContentAlignment = HorizontalAlignment.Center;
        //            b.Background = System.Windows.Media.Brushes.Blue;

        //            if (i == numberofstrings)
        //            {
        //                b.Content = "";
        //                b.Background = System.Windows.Media.Brushes.Transparent;
        //            }

        //            if (i == numberofstrings && fretMarkers.Contains(j))
        //            {
        //                b.Content = j;
        //            }

        //            b.VerticalAlignment = VerticalAlignment.Bottom;
        //            b.HorizontalAlignment = HorizontalAlignment.Left;

        //            b.HorizontalContentAlignment = HorizontalAlignment.Right;

        //            Thickness margin = b.Margin;
        //            margin.Left = widthOfSingleFret * j;
        //            margin.Bottom = heightOfSingleFret * i;
        //            b.Margin = margin;

        //            Grid.SetRow(b, 1);
        //            GridApplication.Children.Add(b);
        //        }
        //    }
        //}


        List<Label> labelList = new List<Label>();



        //public void createFretBoard(int number)
        //{
        //    int numberofstrings = number;
        //    int wantedRows = numberofstrings + 1;
        //    int numberoffrets = 15;
        //    int wantedColumns = numberoffrets + 1;

        //    int[] fretMarkers = { 3, 5, 7, 9, 12, 15, 17 };

        //    double heightOfFretBoard = 200; // Versuchen die Höhe durch eine Funktion zu bekommen
        //    double heightOfSingleFret = heightOfFretBoard / wantedRows;
        //    double widhtOfFretBoard = 507; // Versuchen die Breite durch eine Funktion zu bekommen
        //    double widthOfSingleFret = widhtOfFretBoard / wantedColumns;

        //    for (int i = 0; i < wantedRows; i++)
        //    {
        //        for (int j = 0; j < wantedColumns; j++)
        //        {
        //            Label b = new Label();
        //            String s = new String(AllBaseNotes.A);

        //            b.Content = s.GetNoteFromString(j);

        //            b.Height = heightOfSingleFret;
        //            b.Width = widthOfSingleFret;
        //            b.HorizontalContentAlignment = HorizontalAlignment.Center;
        //            b.Background = System.Windows.Media.Brushes.Blue;

        //            if (i == numberofstrings)
        //            {
        //                b.Content = "";
        //                b.Background = System.Windows.Media.Brushes.Transparent;
        //            }

        //            if (i == numberofstrings && fretMarkers.Contains(j))
        //            {
        //                b.Content = j;
        //            }

        //            b.VerticalAlignment = VerticalAlignment.Bottom;
        //            b.HorizontalAlignment = HorizontalAlignment.Left;

        //            b.HorizontalContentAlignment = HorizontalAlignment.Right;

        //            Thickness margin = b.Margin;
        //            margin.Left = widthOfSingleFret * j;
        //            margin.Bottom = heightOfSingleFret * i;
        //            b.Margin = margin;

        //            Grid.SetRow(b, 1);
        //            GridApplication.Children.Add(b);

        //            labelList.Add(b);
        //        }
        //    }
        //}

        //public void createFretBoard(int number)
        //{
        //    int numberofstrings = number;
        //    int wantedRows = numberofstrings + 1;
        //    int numberoffrets = 15;
        //    int wantedColumns = numberoffrets + 1;

        //    int[] fretMarkers = { 3, 5, 7, 9, 12, 15, 17 };

        //    double heightOfFretBoard = 200; // Versuchen die Höhe durch eine Funktion zu bekommen
        //    double heightOfSingleFret = heightOfFretBoard / wantedRows;
        //    double widhtOfFretBoard = 507; // Versuchen die Breite durch eine Funktion zu bekommen
        //    double widthOfSingleFret = widhtOfFretBoard / wantedColumns;

        //    for (int i = 0; i < wantedRows; i++)
        //    {
        //        for (int j = 0; j < wantedColumns; j++)
        //        {
        //            Label b = new Label();
        //            String s = new String(AllBaseNotes.A);

        //            b.Content = s.GetNoteFromString(j);

        //            b.Height = heightOfSingleFret;
        //            b.Width = widthOfSingleFret;
        //            b.HorizontalContentAlignment = HorizontalAlignment.Center;
        //            b.Background = System.Windows.Media.Brushes.Blue;

        //            if (i == numberofstrings)
        //            {
        //                b.Content = "";
        //                b.Background = System.Windows.Media.Brushes.Transparent;
        //            }

        //            if (i == numberofstrings && fretMarkers.Contains(j))
        //            {
        //                b.Content = j;
        //            }

        //            b.VerticalAlignment = VerticalAlignment.Bottom;
        //            b.HorizontalAlignment = HorizontalAlignment.Left;

        //            b.HorizontalContentAlignment = HorizontalAlignment.Right;

        //            Thickness margin = b.Margin;
        //            margin.Left = widthOfSingleFret * j;
        //            margin.Bottom = heightOfSingleFret * i;
        //            b.Margin = margin;

        //            Grid.SetRow(b, 1);
        //            GridApplication.Children.Add(b);

        //            labelList.Add(b);
        //        }
        //    }
        //}

        public static string[] AllInstruments = { "Guitar", "Bass" };

        private void InitializeCBToneMode()
        {
            //CBToneMode.ItemsSource = Enum.GetValues(typeof(ToneMode));
            //CBToneMode.SelectedIndex = 0;
        }

        private void signLabel()
        {
            var blubb = mTune.Items;

            foreach (MenuItem item in blubb)
            {
                if(item.IsChecked == true)
                {
                    label.Content = item.Header;
                }
            }
        }

        private void signLabelTwo()
        {
            foreach (MenuItem item in mInstrumentType.Items)
            {
                if (item.IsChecked == true)
                {
                    string s = Convert.ToString(item.Header);
                    label.Content = dicInstruments[s].instrumentType;
                    break;
                }
            }
        }

        //private  Dictionary<string, TuningType> dicTunings = new Dictionary<string, TuningType>()
        //    {
        //        { "Standard", TuningType.Standard },
        //        { "Drop D", TuningType.DropD },
        //        { "Semitone deeper", TuningType.SemiToneDeeper }
        //    };

        private Dictionary<string, Instrument> dicInstruments = new Dictionary<string, Instrument>()
        {
            { "6 String Guitar", new Instrument(InstrumentType.Guitar, 6) },
            { "7 String Guitar", new Instrument(InstrumentType.Guitar, 7) },
            { "4 String Bass", new Instrument(InstrumentType.Bass, 4) },
            { "5 String Bass", new Instrument(InstrumentType.Bass, 5) },
            { "6 String Bass", new Instrument(InstrumentType.Bass, 6) }
        };

        private Dictionary<string, ToneMode> dicToneMode = new Dictionary<string, ToneMode>()
        {
            { "Major", ToneMode.Major },
            { "Minor", ToneMode.Minor }
        };

        private string[] StartNotes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "H" };

        private void enableAllTunes()
        {
            mTStandard.Visibility = Visibility.Visible;
            mTDropD.Visibility = Visibility.Visible;
        }

        private void disableDropD()
        {
            mTDropD.Visibility = Visibility.Collapsed;
        }

        private void setStandard()
        {
            mTStandard.IsChecked = true;
        }

        private void uncheckAllTunes()
        {
            mTStandard.IsChecked = false;
            mTDropD.IsChecked = false;
        }

        private void uncheckAllInstruments()
        {
            mIGuitarSix.IsChecked = false;
            mIGuitarSeven.IsChecked = false;
            mIBassFour.IsChecked = false;
            mIBassFive.IsChecked = false;
            mIBassSix.IsChecked = false;
        }

        private void mISixGuitar_Click(object sender, RoutedEventArgs e)
        {
            uncheckAllInstruments();
            enableAllTunes();
            uncheckAllTunes();
            mIGuitarSix.IsChecked = true;
            setStandard();
            //signLabel();
            //signLabelTwo();
            createFretBoard();
        }

        private void mIGuitarSeven_Click(object sender, RoutedEventArgs e)
        {
            uncheckAllInstruments();
            enableAllTunes();
            uncheckAllTunes();
            mIGuitarSeven.IsChecked = true;
            setStandard();
            disableDropD();
            //signLabel();
            createFretBoard();
        }

        private void mIBassFour_Click(object sender, RoutedEventArgs e)
        {
            uncheckAllInstruments();
            enableAllTunes();
            uncheckAllTunes();
            mIBassFour.IsChecked = true;
            setStandard();
            createFretBoard();
        }

        private void mIBassFive_Click(object sender, RoutedEventArgs e)
        {
            uncheckAllInstruments();
            enableAllTunes();
            uncheckAllTunes();
            mIBassFive.IsChecked = true;
            setStandard();
            disableDropD();
            createFretBoard();
        }

        private void mIBassSix_Click(object sender, RoutedEventArgs e)
        {
            uncheckAllInstruments();
            enableAllTunes();
            uncheckAllTunes();
            mIBassSix.IsChecked = true;
            setStandard();
            disableDropD();
            createFretBoard();
        }

        private void mTStandard_Click(object sender, RoutedEventArgs e)
        {
            uncheckAllTunes();
            mTStandard.IsChecked = true;
        }

        private void mTDropD_Click(object sender, RoutedEventArgs e)
        {
            uncheckAllTunes();
            mTDropD.IsChecked = true;
        }

        private void mIGuitarSix_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            createFretBoard();
        }
    }
}
