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
using System.Collections;
namespace OzlemCinarWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            ArrayList People = new ArrayList();
            string line;
                System.IO.StreamReader myFile = new System.IO.StreamReader(@"C:\temp\oscourse\360-p6.txt");
                if (myFile != null) 
                {
                    while ((line = myFile.ReadLine()) != null)
                    {
                        string[] nameSec = line.Split('"');
                        string[] numSec = nameSec[2].Split(',');
                        int firstNum = Int32.Parse(numSec[1]);
                        int secNum= Int32.Parse(numSec[2]);
                        int thirdNum = Int32.Parse(numSec[3]);            
                        People.Add
                        (
                            new People() 
                            { 
                                name = nameSec[1], 
                                firstNum = firstNum, 
                                secondNum = secNum, 
                                thirdNum = thirdNum 
                            }
                       );              
                    }
                    myFile.Close();
                }
               else
               {                
                   throw new IOException("An error occurred while processing the file.");
               }

                dataGrid.ItemsSource = People;
        }

        public class People
        {
            public string name { get; set; }
            public int firstNum { get; set; }
            public int secondNum { get; set; }
            public int thirdNum { get; set; }
        }
    }
}