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
