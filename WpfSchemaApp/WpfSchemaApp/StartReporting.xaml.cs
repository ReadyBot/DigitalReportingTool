using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSchemaApp
{    
    public partial class StartReporting : Window
    {
        private string handlingFileLocation = "../../QuestionsData/HandlingFile.JSON";
        private string dataFileLocation = "../../QuestionsData/SaveFile.JSON";
        private int currentQID = 1;

        public ReportingData importedData;
        public HandlingData handlingData;
        public int questionFontHeigth = 25;
        public List<String> userInput = new List<String>();
        public List<int> clickHistory = new List<int>();
        

        public StartReporting()
        {
            InitializeComponent();
            ImportFile();
            NextQuestion(currentQID);
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public void ImportFile()
        {
            handlingData = JsonConvert.DeserializeObject<HandlingData>(File.ReadAllText(handlingFileLocation, Encoding.UTF8));          
            importedData = JsonConvert.DeserializeObject<ReportingData>(File.ReadAllText(dataFileLocation, Encoding.UTF8));

            if (importedData == null)
            {
                MessageBox.Show("Question File is either corrupted or not setup. \nSetup a new file or Contact Admin");
            }

            if (handlingData == null)
            {
                MessageBox.Show("Solution File is either corrupted or not setup. \nSetup a new file or Contact Admin");
            }
        }
        public void ResetInputs()
        {
            //QuestionGrid.Children.Clear();
            OptionGrid.Children.Clear();
            OptionGrid.RowDefinitions.Clear();
            OptionGrid.ColumnDefinitions.Clear();
        }
        public void NextQuestion(int qID)
        {
            ResetInputs();            
            currentQID = qID;
            int count = 0;

            Question temp = importedData.QuestionData.Find(question => question.QuestionID == qID);
            if(temp != null)
            {
                tQuestion.Text = temp.QuestionTxt;
                OptionGrid.Height = temp.AnswerData.Count * (questionFontHeigth + 5);

                foreach (Answer a in temp.AnswerData)
                {
                    GenerateAlternatives(a, count);
                    count++;
                }
            }
            else
            {
                Solutions sol = new Solutions();
                sol.Solution = "Thank you for reporting.\nA solution is not filled in for this case.";

                ReportingComplete(sol);
            }            
        }
        
        public void GenerateAlternatives(Answer a, int rowCounter)
        {
            Button dynAButton = new Button();
            dynAButton.Name = "Button" + rowCounter;
            dynAButton.HorizontalAlignment = HorizontalAlignment.Left;
            dynAButton.VerticalAlignment = VerticalAlignment.Top;
            OptionGrid.RowDefinitions.Add(new RowDefinition());
            Grid.SetRow(dynAButton, rowCounter);
            dynAButton.BorderThickness = new Thickness(0);
            dynAButton.Height = questionFontHeigth;
            dynAButton.Width = 150;
            dynAButton.Content = a.AnswerTxt;
            dynAButton.Click += ABtn_Click;
            OptionGrid.Children.Add(dynAButton);
        }

        public void LogAnswers(String question, String answer)
        {
            String loggedAns = question + " " + answer;

            clickHistory.Add(currentQID);
            userInput.Add(loggedAns);            
        }

        public void ReportingComplete(Solutions solution)
        {
            String completeString = "";
            completePop.IsOpen = true;

            for (int i = 0; i < userInput.Count; i++)
            {
                completeString += userInput[i] + "\n";
            }
            completeString += "\n" + solution.Solution;
            completeSolText.Text = completeString;
        }
        

        private void PreviousBtn_Click(object sender, RoutedEventArgs e)
        {
            if (clickHistory.Count > 0)
            {
                int i = clickHistory.Count - 1;
                clickHistory.RemoveAt(clickHistory.Count);
                userInput.RemoveAt(userInput.Count);
                NextQuestion(i);
            }
            else
            {
                NextQuestion(1);
            }
        }
        private void ABtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            String buttonName = button.Content.ToString();

            String questionTxt;
            int nextQuestionIndex;
            Solutions temp = new Solutions();

            Solutions temp1 = handlingData.solutionData.Find(solution => solution.QuestionID == currentQID && solution.AnswerInput == buttonName);
            if (temp1 != null)
            {
                ReportingComplete(temp1);          
            }
            else
            {
                Question q = importedData.QuestionData.Find(question => question.QuestionID == currentQID);
                
                Answer a = q.AnswerData.Find(answer => answer.AnswerTxt == buttonName);
                nextQuestionIndex = a.NQuestionId;
                questionTxt = q.QuestionTxt;

                LogAnswers(questionTxt, buttonName);
                NextQuestion(nextQuestionIndex);
            }   
        }       
        
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
