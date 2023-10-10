using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;

namespace WPFsortowanie
{

    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double[] tekst;
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new ViewModel();
        }

        //public class ViewModel : INotifyPropertyChanged
        //{
        //    public event PropertyChangedEventHandler PropertyChanged;
        //    void RaisePropertyChanged(string ReadTextFile_)
        //    {
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs(ReadTextFile_));
        //        }
        //    }
        //        string _ReadTextFile;
        //        public string ReadTextFile
        //    {
        //        get
        //        {
        //            return _ReadTextFile;
        //        }
        //        set
        //        {
        //            _ReadTextFile = value;
        //            RaisePropertyChanged("");
        //        }
            
        //    }
        //}


        private void ChooseFiles_button(object sender, RoutedEventArgs e)
        { 
            string text = ReadTextFile();
            tekst = StringToArray(text);
            if (tekst != null)
            {
                TextFromAllFiles.Text = text;
            }
        }
        private string ReadTextFile()
        {
            string allText = "";
            try
            {
                OpenFileDialog ofd = new OpenFileDialog
                {
                    DefaultExt = ".txt",
                    Filter = "Text documents (.txt)|*.txt",
                    Multiselect = true,
                };
                if (ofd.ShowDialog() == true)
                {
                    string[] path = ofd.FileNames;
                    foreach (string filePath in path)
                    {
                        if (filePath != "")
                        {
                            string text = File.ReadAllText(filePath);
                            allText += text + " ";
                        }
                    }
                }
            }
            catch (ArgumentNullException)
            {

                MessageBox.Show("Wystąpił błąd, nie wybrałeś pliku!");
            }
            return allText;
        }

        public double[] StringToArray(string text)
        {
            if (text == "")
            {
                return null;
            }
            else
            {
                string[] substringedText;
                double[] numArray;
                try
                {
                        substringedText = text.Split(new string[] { }, StringSplitOptions.RemoveEmptyEntries);
                        numArray = new double[substringedText.Length];
                        int index = 0;
                        foreach (string wyraz in substringedText)
                        {
                            numArray[index] = Convert.ToDouble(wyraz);
                            index++;
                        }

                }
                catch (FormatException)
                {
                    MessageBox.Show("Wybrałeś/aś plik w którym są litery, wybierz inny.");
                    return null;
                }

                return numArray;
            }
         
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            var list = new List<double>();

                
            if(tekst != null)
            {
                list.AddRange(tekst);
            }

            double[] finalArray = list.ToArray();

            for (int i = 0; i < finalArray.Length; i++)
            {
                _ = finalArray[i];
                Array.Sort(finalArray);
                    var output = string.Join(", ", finalArray);
                    sortedTextBlock.Text = output;
            }
        }

        private void SaveFile_Button(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDocumentFile = new SaveFileDialog
            {
                FileName = "Document",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };
            if (saveDocumentFile.ShowDialog() == true)
                File.WriteAllText(saveDocumentFile.FileName, sortedTextBlock.Text);
        }

    }
}
