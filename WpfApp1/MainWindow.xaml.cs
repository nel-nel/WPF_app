using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;


namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string SearchWord;
        string CurrentText;

        public MainWindow()
        {
            InitializeComponent();
            setSearchWord("");
            setCurrentText("");
        }

        public void setSearchWord(string SearchWord)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchWord))
                {
                    Word.Text = "Please enter a word!";
                }
                else
                {
                    this.SearchWord = SearchWord;
                }                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        public string getSearchWord()
        {
            return this.SearchWord;
        }

        public void setCurrentText(string CurrentText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CurrentText)){
                    FileContent.Text = "Please choose a file!";
                }
                else
                {
                    this.CurrentText = CurrentText;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }


        }
      public string GetCurrentText(){
            return this.CurrentText;    
        }
        
        private void FileFind(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                // only .txt files!
                openFileDialog.Filter = "Text|*.txt"; 

                if (openFileDialog.ShowDialog() == true)
                {
                    FileContent.Text = File.ReadAllText(openFileDialog.FileName);
                    setCurrentText(FileContent.Text);
                    CurrentText = GetCurrentText();
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
           }       

        private void FindText_Click(object sender, RoutedEventArgs e)
        {   //FileContent and Word are xaml elements
            setCurrentText(FileContent.Text.ToLower()); 
            setSearchWord(Word.Text.ToLower());
            StringBuilder occurrences = new StringBuilder();
            bool NotFound = true;
            int index = 0;
            do
            {               
                    index = GetCurrentText().IndexOf(SearchWord, index);
                    if (index != -1)
                    {
                        NotFound = false;
                        occurrences.Append(" " + index + ", ");
                       index++;
                    }
                    
              
            } while (index != -1);

            Indexes.Clear();

            if (NotFound)
            {
                Indexes.AppendText("No Match!");
            } else
            {
                string repeats = occurrences.ToString();
                Indexes.AppendText(repeats);
            }


            index = 0;
        }

         private void Format_Click(object sender, RoutedEventArgs e)
        {
            FileContent.Text = CurrentText.Replace(" ",string.Empty);
        }
    }
}

