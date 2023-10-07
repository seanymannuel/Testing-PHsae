using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace EnigmaMachine
{
    public partial class MainWindow : Window
    {
        private List<string> refRings = new List<string>();
        private List<string> ringsList = new List<string>();
        private bool refRingsStoredSuccessfully = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            openFileDialog.DefaultExt = ".csv";

            if (openFileDialog.ShowDialog() == true)
            {
                // Handle the selected file here
                string selectedFilePath = openFileDialog.FileName;

                try
                {
                    // Read the contents of the CSV file
                    string[] lines = File.ReadAllLines(selectedFilePath);

                    // Process the CSV data and store 'Ref Ring' and 'Rings'
                    for (int i = 0; i < lines.Length; i++) // Start from the first line since there's no header
                    {
                        string[] fields = lines[i].Split('\t');
                        if (fields.Length >= 6) // Assuming there are six columns
                        {
                            refRings.Add("RingRef"); // Fixed value for the first column
                            ringsList.Add($"{fields[0]}\t{fields[1]}\t{fields[2]}\t{fields[3]}\t{fields[4]}\t{fields[5]}");
                        }
                    }

                    // Check if 'Ref Ring' values were successfully stored
                    if (refRings.Count > 0)
                    {
                        refRingsStoredSuccessfully = true;
                        RefRingStatusTextBlock.Text = "Reference Ring Status: Ok";
                    }
                    else
                    {
                        refRingsStoredSuccessfully = false;
                        RefRingStatusTextBlock.Text = "Reference Ring Status: Error";
                    }

                    // Set the TextBlock texts for file path and total number of rings
                    FilePathTextBlock.Text = "File Path: " + selectedFilePath;
                    TotalRingsTextBlock.Text = "Total Number of Rings: " + ringsList.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading the CSV file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
