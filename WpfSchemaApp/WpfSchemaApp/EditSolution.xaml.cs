using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace WpfSchemaApp
{
    //TODO - Save solution - Show active option  + swap Q position
    public partial class EditSolution : Window
    {

        private String handlingFileLocation = "../../QuestionsData/HandlingFile.JSON";
        private String dataFileLocation = "../../QuestionsData/SaveFile.JSON";

        public ReportingData importedData;
        public HandlingData handlingData;
        public int questionFontHeigth = 25;

        public EditSolution()
        {
            InitializeComponent();
            ImportFile();
            LoadQuestions();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public void ImportFile()
        {
            handlingStatus.Text = "Solution File";
            dataStatus.Text = "Question File";

            handlingData = JsonConvert.DeserializeObject<HandlingData>(File.ReadAllText(handlingFileLocation, Encoding.UTF8));            
            importedData = JsonConvert.DeserializeObject<ReportingData>(File.ReadAllText(dataFileLocation, Encoding.UTF8));

            if (importedData != null)
            {
                dataStatus.Foreground = Brushes.Green;
            }
            else
            {
                dataStatus.Foreground = Brushes.Red;
            }

            if (handlingData != null)
            {
                handlingStatus.Foreground = Brushes.Green;
            }
            else
            {
                handlingStatus.Text = "No Solution File";
                handlingStatus.Foreground = Brushes.Red;
                handlingData = new HandlingData();
            }
        }
        
        public void ExportDataFile()
        {
            JsonSerializerSettings exportJson = new JsonSerializerSettings();
            exportJson.Formatting = Formatting.Indented;

            string jsonString = JsonConvert.SerializeObject(handlingData, exportJson);
            File.WriteAllText(handlingFileLocation, jsonString, Encoding.UTF8);

            MessageBox.Show("Your Question changes has been saved!");
        }
        public void LoadQuestions()
        {
            int counter = 0;
            LoadingGrid.Children.Clear();
            LoadingGrid.ColumnDefinitions.Clear();
            LoadingGrid.RowDefinitions.Clear();

            LoadingGrid.Height = importedData.QuestionData.Count * questionFontHeigth;
            LoadingGrid.MaxWidth = 150;

            //LoadingGrid
            foreach (Question question in importedData.QuestionData)
            {
                QGenerator(question, counter);
                counter++;
            }
        }

        public void QGenerator(Question question, int rowCounter)
        {
            Button dynQButton = new Button();
            dynQButton.Name = "Button" + question.QuestionID.ToString();
            dynQButton.HorizontalAlignment = HorizontalAlignment.Left;
            dynQButton.VerticalAlignment = VerticalAlignment.Top;
            LoadingGrid.RowDefinitions.Add(new RowDefinition());
            Grid.SetRow(dynQButton, rowCounter);
            dynQButton.BorderThickness = new Thickness(0);
            dynQButton.Height = questionFontHeigth;
            dynQButton.Content = "Nr. " + question.QuestionID + " " + question.QuestionTxt;
            dynQButton.Click += QBtn_Click;
            LoadingGrid.Children.Add(dynQButton);
        }

        
        public void LoadAnswers(Question q)
        {
            int counter = 0;
            AlternativGrid.Children.Clear();
            AlternativGrid.ColumnDefinitions.Clear();
            AlternativGrid.RowDefinitions.Clear();

            AlternativGrid.Height = importedData.QuestionData.Count * questionFontHeigth;
            AlternativGrid.MaxWidth = 150;

            //LoadingGrid
            foreach (Answer a in q.AnswerData)
            {
                GenerateAnswer(a, counter);
                counter++;
            }
        }
        public void GenerateAnswer(Answer a, int rowCounter)
        {
            Button dynAButton = new Button();
            dynAButton.Name = "Button" + a.AnswerTxt;
            dynAButton.HorizontalAlignment = HorizontalAlignment.Left;
            dynAButton.VerticalAlignment = VerticalAlignment.Top;
            AlternativGrid.RowDefinitions.Add(new RowDefinition());
            Grid.SetRow(dynAButton, rowCounter);
            dynAButton.BorderThickness = new Thickness(0);
            dynAButton.Height = questionFontHeigth;
            dynAButton.Width = 50;
            dynAButton.Content = a.AnswerTxt;
            dynAButton.Click += ABtn_Click;
            AlternativGrid.Children.Add(dynAButton);
        }
        private void QBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string buttonName = button.Name;
            int buttonNameLength = buttonName.Length - 6;
            string subName = buttonName.Substring(6, buttonNameLength);
            int indexList = Int32.Parse(subName);
            ActiveQ.Text = "Question: " + indexList;
            Question tmp = importedData.QuestionData.Find(question => question.QuestionID == indexList);            
            LoadAnswers(tmp);
        }
        private void ABtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            String buttonName = button.Name;
            int buttonNameLength = buttonName.Length - 6;
            String subName = buttonName.Substring(6, buttonNameLength);            
            ActiveA.Text = "Answer: " + subName;
            Solutiontxt.Text = "";

            String qID = ActiveQ.Text;
            int buttonNameLength2 = qID.Length - 10;
            String subName2 = qID.Substring(10, buttonNameLength2);
            int num = Int32.Parse(subName2);
            Solutions temp = new Solutions();

            if (handlingData != null)
            { 
                temp = handlingData.solutionData.Find(solution => solution.QuestionID == num && solution.AnswerInput == subName);
            }

            if(temp != null)
            {
                Solutiontxt.Text = temp.Solution;
            }
            else
            {
                Solutiontxt.Text = "";
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            String qID = ActiveQ.Text;
            int qNameLength = qID.Length - 10;
            String subNameQ = qID.Substring(10, qNameLength);
            int qIDnum = Int32.Parse(subNameQ);

            String aID = ActiveA.Text;
            int aNameLength = aID.Length - 8;
            String subNameA = aID.Substring(8, aNameLength);

            Solutions temp = new Solutions();
            temp.QuestionID = qIDnum;
            temp.AnswerInput = subNameA;
            temp.Solution = Solutiontxt.Text;
            
            if (handlingData != null)
            {
                Solutions temp1 = handlingData.solutionData.Find(solution => solution.QuestionID == qIDnum && solution.AnswerInput == subNameA);
                if(temp1 != null)
                {
                    handlingData.solutionData[handlingData.solutionData.IndexOf(temp1)] = temp;
                }
                else
                {
                    handlingData.solutionData.Add(temp);                    
                }
            }
            ExportDataFile();
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            String qID = ActiveQ.Text;
            int qNameLength = qID.Length - 10;
            String subNameQ = qID.Substring(10, qNameLength);
            int qIDnum = Int32.Parse(subNameQ);

            String aID = ActiveA.Text;
            int aNameLength = aID.Length - 8;
            String subNameA = aID.Substring(8, aNameLength);

            Solutions temp = new Solutions();

            if (handlingData != null) 
            { 
                temp = handlingData.solutionData.Find(solution => solution.QuestionID == qIDnum && solution.AnswerInput == subNameA);
                handlingData.solutionData.Remove(temp);
            }

            Solutiontxt.Text = "";
            ExportDataFile();
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }        
    }
}
