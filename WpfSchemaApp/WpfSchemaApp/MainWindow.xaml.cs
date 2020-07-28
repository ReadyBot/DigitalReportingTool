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

namespace WpfSchemaApp

{    
    public partial class MainWindow : Window
    {
        private string dataFileLocation = "../../QuestionsData/SaveFile.JSON";
        private string handlingFileLocation = "../../QuestionsData/HandlingFile.JSON";


        public MainWindow()
        {            
            InitializeComponent();
            CheckFile();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }


        //Buttons:
        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            StartReporting startWindow = new StartReporting();
            startWindow.Show();
            this.Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EQBtn_Click(object sender, RoutedEventArgs e)
        {
            EditQuestions startWindow = new EditQuestions();
            startWindow.Show();
            this.Close();
        }
        private void ESBtn_Click(object sender, RoutedEventArgs e)
        {            
            EditSolution startWindow = new EditSolution();
            startWindow.Show();
            this.Close();
        }

        public void CheckFile()
        {
            string dataFile = System.IO.Path.Combine(Directory.GetCurrentDirectory(), dataFileLocation);
            string handlingFile = System.IO.Path.Combine(Directory.GetCurrentDirectory(), handlingFileLocation);

            CheckQs(dataFile);
            CheckSol(handlingFile);            
        }
        public void CheckQs(String dataFile)
        {
            if (!File.Exists(dataFile))
            {
                checkTxtMW1.Text = "Error";
                checkTxtMW1.Foreground = Brushes.Red;
                MessageBox.Show("There is no sanctions file ready. \nPlease Contact Admin if you wish to use this program!");
                return;
            }
            else
            {
                checkTxtMW1.Text = "Ready";
                checkTxtMW1.Foreground = Brushes.Green;
            }
        }
        public void CheckSol(String handlingFile)
        {
            if (!File.Exists(handlingFile))
            {
                checkTxtMW2.Text = "Error";
                checkTxtMW2.Foreground = Brushes.Red;
                MessageBox.Show("There is no handling file ready. \nPlease Contact Admin if you wish to use this program!");
                return;
            }

            else
            {
                checkTxtMW2.Text = "Ready";
                checkTxtMW2.Foreground = Brushes.Green;
            }
        }

    }    
}
