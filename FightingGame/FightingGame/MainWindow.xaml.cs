using System;
using System.Windows;

namespace BattleGameApp
{
    public partial class MainWindow : Window
    {
        private int playerHealth = 100;
        private int playerAttack = 10;

        private int enemyHealth;
        private int enemyAttack = 8;

        public MainWindow()
        {
            InitializeComponent();
            ResetGame();
        }

        private void AttackButton_Click(object sender, RoutedEventArgs e)
        {
            // Player's turn
            int playerDamage = CalculateDamage(playerAttack);
            enemyHealth -= playerDamage;
            UpdateUI();

            // Check if the enemy is defeated
            if (enemyHealth <= 0)
            {
                MessageBox.Show("Congratulations! You defeated the enemy!");
                ResetGame();
                return;
            }

            // Enemy's turn
            int enemyDamage = CalculateDamage(enemyAttack);
            playerHealth -= enemyDamage;
            UpdateUI();

            // Check if the player is defeated
            if (playerHealth <= 0)
            {
                MessageBox.Show("Game over! The enemy defeated you.");
                ResetGame();
            }
        }

        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Add logic for the "Item" button if needed
            MessageBox.Show("You used an item!");
        }

        private void ActButton_Click(object sender, RoutedEventArgs e)
        {
            // Add logic for the "Act" button if needed
            MessageBox.Show("You took an action!");
        }

        private void SpareButton_Click(object sender, RoutedEventArgs e)
        {
            // Add logic for the "Spare" button if needed
            MessageBox.Show("You spared the enemy!");
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thanks for playing! Goodbye!");
            Close();
        }

        private void UpdateUI()
        {
            PlayerHealthLabel.Content = "Player Health: " + playerHealth;
            EnemyHealthLabel.Content = "Enemy Health: " + enemyHealth;
        }

        private int CalculateDamage(int attackPower)
        {
            // This is a simple damage calculation. You can modify it based on your game logic.
            Random random = new Random();
            return random.Next(attackPower / 2, attackPower + 1);
        }

        private void ResetGame()
        {
            playerHealth = 100;
            enemyHealth = new Random().Next(50, 101); // Random enemy health between 50 and 100
            UpdateUI();
        }
    }
}
