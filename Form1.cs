using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Wordle01
{
    public partial class Form1 : Form
    {
        private int curCol = 0; 
        private int curRow = 0; 
        private Label[] letterLabels = new Label[30];
        private string guess = "";
        private int guesses = 0;
        private const int maxGuesses = 6;
        private string word = "EXAMP"; 
        List<Label> letterLabelsList = new List<Label>();
        HashSet<string> wordList = new HashSet<string>();

        public Form1()
        {
            InitializeComponent();
            generateLabels();
            loadWords();
            showWord();
            this.KeyPress += Form1_KeyPress;
            this.KeyPreview = true;
        }

        private void Form1_KeyPress(object? sender, KeyPressEventArgs e)
        {
            HandleKeyPress(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Additional initialization if needed
        }

        public void loadWords()
        {
            string tempWord = "";
            Random rdnNum = new Random();

            try
            {
                string[] allWords = File.ReadAllLines("validWords.txt");

                foreach (string temp in allWords)
                {
                    tempWord = temp.Trim().ToUpperInvariant();
                    wordList.Add(tempWord);
                }

                if (wordList.Count > 0)
                {
                    int element = rdnNum.Next(0, wordList.Count);
                    word = wordList.ElementAt(element);
                }
                else
                {
                    MessageBox.Show("No valid words available.");
                    word = "EXAMP"; // Temporary fallback word for testing
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("The word file is missing.");
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error loading valid words: " + ex.Message);
            }
        }


        public void showWord()
        {
            MessageBox.Show(word);
        }

        public void generateLabels()
        {
            int width = 45;
            int height = 45;
            int spacing = 15;
            int rowWidth = (width * 5) + (spacing * 4);
            int x = ((this.ClientSize.Width - rowWidth) / 2) - 10;
            int y = 50;

            for (int i = 0; i < 6; i++) 
            {
                for (int j = 0; j < 5; j++) 
                {
                    Label lblLetter = new Label
                    {
                        Text = " ", // Set to desired text (use empty space)
                        Font = new Font("Rockwell", 24, FontStyle.Bold),
                        Size = new Size(width + 10, height + 10),
                        TextAlign = ContentAlignment.MiddleCenter,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White,
                        Location = new Point(x + j * (width + spacing), y + i * (height + spacing))
                    };

                    this.Controls.Add(lblLetter);
                    letterLabels[i * 5 + j] = lblLetter; 
                }
                y += 5;
            }
        }

        private void HandleKeyPress(KeyPressEventArgs e)
        {
            char letter = ' ';

            if (char.IsLetter(e.KeyChar) && curCol < 5) 
            {
                letter = char.ToUpper(e.KeyChar);
                letterLabels[curRow * 5 + curCol].Text = letter.ToString();
                curCol++;
            }
            else if (e.KeyChar == (char)Keys.Back) 
            {
                if (curCol > 0) 
                {
                    curCol--; 
                    letterLabels[curRow * 5 + curCol].Text = " "; 
                }
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                guess = buildWorld();

                if (checkValid(guess))
                {
                    guesses++;
                    updateLabel(guess);
                }
                else
                {
                    MessageBox.Show("Not a valid Word");
                    return;
                }

                if (checkWin(guess))
                {
                    MessageBox.Show("You win!");
                    Application.Exit();
                }
                else if (guesses >= maxGuesses)
                {
                    MessageBox.Show("You ran out of guesses. The word was " + word);
                    Application.Exit();
                }
                else
                {
                    curRow++;
                    curCol = 0; 
                }
            }
        }

        public string buildWorld()
        {
            guess = ""; 
            for (int i = 0; i < 5; i++)
            {
                guess += letterLabels[curRow * 5 + i].Text;
            }
            return guess;
        }

        public void updateLabel(string tempGuess)
        {
            int startIndex = 0;

            bool[] matchedIndex = new bool[5];
            bool[] matchedInGuess = new bool[5];
            bool[] matchedInWord = new bool[5];

            startIndex = curRow * 5;

            for (int i = 0; i < 5; i++)
            {
                if (tempGuess[i] == word[i])
                {
                    letterLabels[startIndex + i].BackColor = Color.Green;
                    matchedIndex[i] = true;
                    matchedInGuess[i] = true;
                    matchedInWord[i] = true;
                }
            }
            for (int i = 0; i < 5; i++)
            {
                if (!matchedIndex[i])
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (tempGuess[i] == word[j] && !matchedInWord[j] && !matchedInGuess[i])
                        {
                            letterLabels[startIndex + i].BackColor = Color.Yellow;
                            matchedInGuess[i] = true;
                            matchedInWord[j] = true; 
                            break;
                        }
                    }
                }
            }
        }

        public bool checkWin(string tempGuess)
        {
            return tempGuess.Equals(word, StringComparison.OrdinalIgnoreCase);
        }

        public bool checkValid(string tempGuess)
        {
            return wordList.Contains(tempGuess);
        }
    }
}
