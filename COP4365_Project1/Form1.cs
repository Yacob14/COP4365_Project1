using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace COP4365_Project1
{
    public partial class Form1 : Form
    {
        List<Control> gameButtons;
        List<String> wordList;
        char[] chosenLetters;
        Random rng;

        bool gameActive;

        public Form1()
        {
            InitializeComponent();
        }

        private void GO_button_Click(object sender, EventArgs e)
        {
            //activate game
            gameActive = true;

            //activate timer
            timer.Enabled = true;

            //return a list of all buttons in the form
            gameButtons = Controls.OfType<Button>().Cast<Control>().ToList();

            //seed the random number generator
            rng = new Random();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            foreach (Button button in gameButtons){
                if (button.Name != "GO_button")
                {
                    char rc = (char)rng.Next('A', 'Z' + 1);
                    button.Text = "" + rc;
                }
                
            }
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gameActive = false;
            StreamReader inputFile;
            inputFile = File.OpenText("Words5.txt");

            wordList = new List<string>();
            chosenLetters = new char[5];

            while (inputFile.EndOfStream == false)
            {
                wordList.Add(inputFile.ReadLine());
            }

            inputFile.Close();

            words_listBox.Items.AddRange(wordList.ToArray());

        }
/****************************************************************************/

 /*     //Each button click is essentially the same
       private void Letter0_Click(object sender, EventArgs e)
        {
           //Letter0.Text = textBox1.Text;
           List<string> newList = new List<string>();      //create new list to send to sortList
            foreach (string word in words_listBox.Items)    //uses only words currently in listbox
            {
                newList.Add(word);
            }
            sortList(sender, e, newList);
        }

        private void Letter1_Click(object sender, EventArgs e)
        {
            //Letter1.Text = textBox1.Text;                 //I just created a textbox and manually
            List<string> newList = new List<string>();      //input letters to the buttons
            foreach (string word in words_listBox.Items)
            {
                newList.Add(word);
            }
            sortList(sender, e, newList);
        }

        private void Letter2_Click(object sender, EventArgs e)
        {
            //Letter2.Text = textBox1.Text;
            List<string> newList = new List<string>();
            foreach (string word in words_listBox.Items)
            {
                newList.Add(word);
            }
            sortList(sender, e, newList);
        }

        private void Letter3_Click(object sender, EventArgs e)
        {
            //Letter3.Text = textBox1.Text;
            List<string> newList = new List<string>();
            foreach (string word in words_listBox.Items)
            {
                newList.Add(word);
            }
            sortList(sender, e, newList);
        }

        private void Letter4_Click(object sender, EventArgs e)
        {
            //Letter4.Text = textBox1.Text;
            List<string> newList = new List<string>();
            foreach (string word in words_listBox.Items)
            {
                newList.Add(word);
            }
            sortList(sender, e, newList);
        }

        private void sortList(object sender, EventArgs e, List<string> tempList)
        {
            List<string> newList = new List<string>();
            Button button = (Button)sender;
            int index = (int)char.GetNumericValue(button.Name[6]);
            foreach (string word in tempList)
            {
                if (button.Text[0] == word[index - 1])
                {
                    newList.Add(word);                  //Adds words to new list if the letter matches
                }                                       //the respective index letter of the words in the
            }                                           //list box
            displayList(newList);
        }

        private void displayList(List<string> mainList)
        {
            words_listBox.Items.Clear();
            foreach (string val in mainList)
            {
                words_listBox.Items.Add(val);
            }

            if (mainList.Capacity == 0)             //Didn't do much bounds/error checking, just seeing if it
            {                                       //works with an empty list
                MessageBox.Show("There are no more words to choose from");
            }
        } 
  */ 
/**************************************************************************/
        private void Letter0_Click(object sender, EventArgs e)
        {

            if (gameActive && gameButtons.Contains(sender))
            {
                Button button = (Button)sender;
                
                //get the index of the newly selected letter in relation to the word onscreen
                int index = (int)Char.GetNumericValue(button.Name[6]);
                chosenLetters[index] = button.Text[0];

                List<string> matchList = checkWordList();

                if (matchList.Count == 0)
                {
                    button.BackColor = Color.Red;
                }
                else
                {
                    words_listBox.Items.Clear();
                    words_listBox.Items.AddRange(matchList.ToArray());
                    button.BackColor = Color.LightGreen;
                    gameButtons.Remove(button);
                }

            }
            
        }

        private List<string> checkWordList()
        {
            List<string> newList = new List<string>();
            foreach (string word in wordList)
            {
                if (canLettersFit(chosenLetters, word))
                {
                    newList.Add(word);
                }
            }

            return newList;
        }

        private bool canLettersFit(char[] currentWords, string fullWord){
            for (int i = 0; i < currentWords.Length; i++)
            {
                if (currentWords[i] == 0) continue;

                if (currentWords[i] != fullWord[i]) return false;
            }

            return true;
        }

    }

    
}
