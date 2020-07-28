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
    public partial class EditQuestions : Window
    {
        //TODO - add failsafe for imported file = null, scroll options, Life sort, swap Questions button, delete dudds

        private string dataFileLocation = "../../QuestionsData/SaveFile.JSON";
        public ReportingData importedData;
        public int questionFontHeigth = 25;             

        public EditQuestions()
        {
            InitializeComponent();
            ImportFile();
            LoadQuestions();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;         
        }
        public void ImportFile()
        {
            dataStatus.Text = "File has Data";
            importedData = JsonConvert.DeserializeObject<ReportingData>(File.ReadAllText(dataFileLocation, Encoding.UTF8));
            
            if (importedData != null) 
            {
                dataStatus.Foreground = Brushes.Green; 
            } else {
                dataStatus.Foreground = Brushes.Red; 
            }
        }        
        public void ExportDataFile()
        {
            importedData.QuestionData.OrderBy(i => i.QuestionID);
            JsonSerializerSettings exportJson = new JsonSerializerSettings();
            exportJson.Formatting = Formatting.Indented;

            string jsonString = JsonConvert.SerializeObject(importedData, exportJson);
            File.WriteAllText(dataFileLocation, jsonString, Encoding.UTF8);

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
            dynQButton.Click += EditBtn_Click;
            LoadingGrid.Children.Add(dynQButton);            
        }
        public void LoadAnswers(Question tmp)
        {
            ResetInputs();
            int counter = 0;
            int temp = tmp.AnswerData.Count;
            if(temp == 0) { temp = 1; }

            for (int i = 0; i <= temp; i++) { AlternativGrid1.ColumnDefinitions.Add(new ColumnDefinition()); }
            for (int i = 0; i <= temp + 1; i++) { AlternativGrid2.ColumnDefinitions.Add(new ColumnDefinition()); }

            AlternativGrid1.ColumnDefinitions[0].Width = new GridLength(45, GridUnitType.Star);
            AlternativGrid1.ColumnDefinitions[1].Width = new GridLength(25, GridUnitType.Star);
            AlternativGrid2.ColumnDefinitions[0].Width = new GridLength(65, GridUnitType.Star);
            AlternativGrid2.ColumnDefinitions[1].Width = new GridLength(180, GridUnitType.Star);
            AlternativGrid2.ColumnDefinitions[2].Width = new GridLength(55, GridUnitType.Star);

            foreach (Answer answer in tmp.AnswerData)
            {
                AGenerator(answer.AnswerTxt, answer.NQuestionId, counter);                
                counter++;
            }
        }
        public void AGenerator(String answer, int nQuestion, int rowCounter)
        {
            AlternativGrid1.Height += questionFontHeigth;
            AlternativGrid2.Height += questionFontHeigth;

            AlternativGrid1.RowDefinitions.Add(new RowDefinition());
            AlternativGrid2.RowDefinitions.Add(new RowDefinition());

            if (answer == null || nQuestion == 0) { answer = ""; nQuestion = 0; }
            TextBlock txtBlockNQTittle = new TextBlock();
            txtBlockNQTittle.Text = "Next Q: ";
            txtBlockNQTittle.Height = questionFontHeigth;
            txtBlockNQTittle.Width = 40;
            txtBlockNQTittle.HorizontalAlignment = HorizontalAlignment.Left;
            txtBlockNQTittle.VerticalAlignment = VerticalAlignment.Bottom;
            Grid.SetRow(txtBlockNQTittle, rowCounter);
            Grid.SetColumn(txtBlockNQTittle, 0);
            AlternativGrid1.Children.Add(txtBlockNQTittle);

            TextBox txtBoxNQ = new TextBox();
            txtBoxNQ.Name = "NextQ" + rowCounter;
            RegisterName(txtBoxNQ.Name, txtBoxNQ);
            txtBoxNQ.Text = nQuestion.ToString();
            txtBoxNQ.Height = questionFontHeigth-5;
            txtBoxNQ.Width = 20;
            txtBoxNQ.HorizontalAlignment = HorizontalAlignment.Left;
            txtBoxNQ.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(txtBoxNQ, rowCounter);
            Grid.SetColumn(txtBoxNQ, 1);
            AlternativGrid1.Children.Add(txtBoxNQ);

            TextBlock txtBlockAnswer = new TextBlock();
            txtBlockAnswer.Text = "Alternative text: ";
            txtBlockAnswer.Height = questionFontHeigth;
            txtBlockAnswer.Width = 60;
            txtBlockAnswer.HorizontalAlignment = HorizontalAlignment.Left;
            txtBlockAnswer.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(txtBlockAnswer, rowCounter);
            Grid.SetColumn(txtBlockAnswer, 0);
            AlternativGrid2.Children.Add(txtBlockAnswer);

            TextBox txtBoxAlt= new TextBox();
            txtBoxAlt.Name = "AnswerTxt" + rowCounter;
            RegisterName(txtBoxAlt.Name, txtBoxAlt);
            txtBoxAlt.Text = answer;
            txtBoxAlt.Height = questionFontHeigth-5;
            txtBoxAlt.Width = 175;
            txtBoxAlt.HorizontalAlignment = HorizontalAlignment.Left;
            txtBoxAlt.VerticalAlignment = VerticalAlignment.Top;
            Grid.SetRow(txtBoxAlt, rowCounter);
            Grid.SetColumn(txtBoxAlt, 1);
            AlternativGrid2.Children.Add(txtBoxAlt);

            Button delAltBtn = new Button();
            delAltBtn.Name = "Button" + rowCounter;
            delAltBtn.Content = "Remove";
            delAltBtn.Width = 50;
            delAltBtn.HorizontalAlignment = HorizontalAlignment.Left;
            delAltBtn.VerticalAlignment = VerticalAlignment.Top;    
            Grid.SetRow(delAltBtn, rowCounter);
            Grid.SetColumn(delAltBtn, 2);
            delAltBtn.BorderThickness = new Thickness(1);
            delAltBtn.Click += DelAltBtn_Click;
            AlternativGrid2.Children.Add(delAltBtn);
            
        }
        public void ResetInputs()
        {
            for(int i = 0; i < AlternativGrid1.RowDefinitions.Count; i++)
            {
                String findNQ = "NextQ" + i.ToString();
                String findAns = "AnswerTxt" + i;
                
                if (AlternativGrid1.FindName(findNQ) != null)
                {
                    AlternativGrid1.UnregisterName(findNQ);
                }
                if (AlternativGrid2.FindName(findAns) != null)
                {
                    AlternativGrid2.UnregisterName(findAns);
                }
            }

            AlternativGrid1.Children.Clear();
            AlternativGrid1.ColumnDefinitions.Clear();
            AlternativGrid1.RowDefinitions.Clear();

            AlternativGrid2.Children.Clear();
            AlternativGrid2.ColumnDefinitions.Clear();
            AlternativGrid2.RowDefinitions.Clear();

            AlternativGrid1.Height = 0;
            AlternativGrid2.Height = 0;
            
        }

        private void AddAltBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AlternativGrid1.RowDefinitions.Count == 0)
            {
                Question tmp = new Question();
                tmp.QuestionID = LoadingGrid.RowDefinitions.Count+1;
                tmp.QuestionTxt = " ";                
                tmp.AnswerData.Add(new Answer { NQuestionId = 0, AnswerTxt = " " });

                LoadAnswers(tmp);
            }
            if(AlternativGrid1.RowDefinitions.Count < 13) 
            { 
                AGenerator(" ", 0, AlternativGrid1.RowDefinitions.Count);
            }
            else
            {
                MessageBox.Show("You can only have up to 13 alternatives!");
            }            
        }

        private void DelAltBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string buttonName = button.Name;
            int rowIndex = Int32.Parse(buttonName.Substring(6, buttonName.Length - 6));
            Question q = new Question();

            //TODO - cycle children - find answer - add temp - loadQ again
            for (int i = 0; i < AlternativGrid2.RowDefinitions.Count; i++)
            {
                TextBox nextQ = (TextBox)AlternativGrid1.FindName("NextQ" + i);
                TextBox answerText = (TextBox)AlternativGrid2.FindName("AnswerTxt" + i);

                if (i != rowIndex) { q.AnswerData.Add(new Answer { NQuestionId = Int32.Parse(nextQ.Text), AnswerTxt = answerText.Text }); }
            }

            AlternativGrid1.UnregisterName("NextQ" + rowIndex);
            AlternativGrid2.UnregisterName("AnswerTxt" + rowIndex);
            AlternativGrid1.RowDefinitions.RemoveAt(rowIndex);
            AlternativGrid2.RowDefinitions.RemoveAt(rowIndex);
            AlternativGrid1.Height -= questionFontHeigth;
            AlternativGrid2.Height -= questionFontHeigth;           
            LoadAnswers(q);           
        }

        private void NewQueBtn_Click(object sender, RoutedEventArgs e)
        {
            int i = AlternativGrid1.RowDefinitions.Count + 1;
            Question tmp = new Question { QuestionID = i, QuestionTxt = " " };
            tQnr.Text = i.ToString();
            tQtxt.Text = " ";
            tmp.AnswerData.Add(new Answer { NQuestionId = 0, AnswerTxt = " " });
            LoadAnswers(tmp);
        }

        private void SaveQueBtn_Click(object sender, RoutedEventArgs e)
        {
            bool gotAllData = true;
            String findNQ;
            String findAns;

            if (tQtxt.Text == null || tQnr.Text == null || tQtxt.Text == " " || tQnr.Text == " ")
            {
                gotAllData = false;
                MessageBox.Show("Something is missing, cannot save changes!");
                return;
            }
            if (gotAllData)
            {    
                Question q = new Question();
                q.QuestionTxt = tQtxt.Text;
                int temp = Int32.Parse(tQnr.Text);
                q.QuestionID = temp;

                for (int i = 0; i < AlternativGrid2.RowDefinitions.Count; i++) 
                {
                    findNQ = "NextQ" + i.ToString();
                    findAns = "AnswerTxt" + i;

                    TextBox nextQ = (TextBox)AlternativGrid1.FindName(findNQ);                    
                    TextBox answerText = (TextBox)AlternativGrid2.FindName(findAns);

                    q.AnswerData.Add(new Answer { NQuestionId = Int32.Parse(nextQ.Text), AnswerTxt = answerText.Text });
                }

                if (importedData.QuestionData.Find(question => question.QuestionID == temp) != null)
                {
                    importedData.QuestionData[temp-1] = q;
                }
                else
                {
                    importedData.QuestionData.Add(q);
                }                
                ExportDataFile();
                ImportFile();
            }
            LoadQuestions();
            ResetInputs();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string buttonName = button.Name;
            int buttonNameLength = buttonName.Length - 6;
            string subName = buttonName.Substring(6, buttonNameLength);
            int indexList = Int32.Parse(subName);
            Question tmp = importedData.QuestionData.Find(question => question.QuestionID == indexList);
            tQnr.Text = indexList.ToString();
            tQtxt.Text = tmp.QuestionTxt;
            
            LoadAnswers(tmp);
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
