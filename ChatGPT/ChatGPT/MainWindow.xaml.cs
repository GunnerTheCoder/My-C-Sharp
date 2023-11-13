using System;
using System.Windows;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {
        private int targetNumber;
        private string targetWord;
        private int num1, num2, mathAnswer;
        private int attempts = 0;

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
            StartNewWordGame();
            StartNewMathGame();
        }

        private void StartNewGame()
        {
            Random random = new Random();
            targetNumber = random.Next(1, 101);
            attempts = 0;
            resultText.Text = "";
        }

        private void StartNewWordGame()
        {
            string[] words = { "apple", "banana", "chocolate", "elephant", "programming" };
            Random random = new Random();
            targetWord = words[random.Next(0, words.Length)].ToLower();
            wordGuessTextBox.Text = "";
            wordResultText.Text = "";
        }

        private void StartNewMathGame()
        {
            Random random = new Random();
            num1 = random.Next(1, 11);
            num2 = random.Next(1, 11);
            mathAnswer = num1 + num2;
            mathAnswerTextBox.Text = "";
            mathResultText.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int userGuess = int.Parse(guessTextBox.Text);
                CheckGuess(userGuess);
            }
            catch (FormatException)
            {
                resultText.Text = "Please enter a valid number.";
            }
        }

        private void WordButton_Click(object sender, RoutedEventArgs e)
        {
            string userGuess = wordGuessTextBox.Text.ToLower();
            CheckWordGuess(userGuess);
        }

        private void MathButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int userAnswer = int.Parse(mathAnswerTextBox.Text);
                CheckMathAnswer(userAnswer);
            }
            catch (FormatException)
            {
                mathResultText.Text = "Please enter a valid number.";
            }
        }

        private void CheckGuess(int guess)
        {
            attempts++;

            if (guess < targetNumber)
            {
                resultText.Text = $"Too low! Try again. Attempts: {attempts}";
            }
            else if (guess > targetNumber)
            {
                resultText.Text = $"Too high! Try again. Attempts: {attempts}";
            }
            else
            {
                resultText.Text = $"Congratulations! You guessed the number in {attempts} attempts.";
                StartNewGame();
            }
        }

        private void CheckWordGuess(string guess)
        {
            if (guess == targetWord)
            {
                wordResultText.Text = "Congratulations! You guessed the word.";
                StartNewWordGame();
            }
            else
            {
                wordResultText.Text = "Incorrect. Try again.";
            }
        }

        private void CheckMathAnswer(int answer)
        {
            if (answer == mathAnswer)
            {
                mathResultText.Text = "Correct! You solved the math problem.";
                StartNewMathGame();
            }
            else
            {
                mathResultText.Text = "Incorrect. Try again.";
            }
        }
    }
}
