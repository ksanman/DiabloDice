using System;
using System.Collections.Generic;
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

namespace DiabloDice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DiabloDiceGameLogic gameLogic;
        private int _playerScore;
        private int _diabloScore;
        private int _rollScore;
        private int RollScore
        {
            get { return _rollScore; }
            set
            {
                if(value != _rollScore)
                {
                    _rollScore = value;
                    CurrentRoll.Text = _rollScore.ToString();
                }
            }
        }

        private int PlayerScore
        {
            get { return _playerScore; }
            set
            {
                if (value != _playerScore)
                {
                    _playerScore = value;
                    PlayerScoreText.Text = _playerScore.ToString();
                }
                if(value > MaxScore)
                {
                    DisplayPlayerWinMessage();
                    hasWon = true;
                }
            }
        }
        private int DiabloScore
        {
            get { return _diabloScore; }
            set
            {
                if(value != _diabloScore)
                {
                    _diabloScore = value;
                    ElDiabloScore.Text = _diabloScore.ToString();
                }
                if(value >= MaxScore)
                {
                    DisplayComputerWinMessage();
                    hasWon = true;
                }
            }
        }
        private static int MaxScore = 50;
        private string playerWinMessage;
        private string playerWinCaption;
        private string computerWinMessage;
        private string computerWinCaption;
        private string bustMessage;
        private string bustCaption;
        private bool isPlayAgain;
        private bool hasWon;

        public MainWindow()
        {
            gameLogic = new DiabloDiceGameLogic(MaxScore);
            InitializeComponent();
            playerWinMessage = "Congrats! You won! \n Would you like to play again?";
            playerWinCaption = "You won.";
            computerWinMessage = "El Diablo won! \n Would you like to play again?";
            computerWinCaption = "El Diablo won.";
            bustMessage = "Bust!";
            bustCaption = "Busted!";
            ResetGame();
    }

        /// <summary>
        /// Initiates the players turn. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            var roll = gameLogic.RollDice();
            if(roll > 1)
            {
                RollScore += roll;
            }
            else
            {
                DisplayBustMessage();
                RunComputerTurn();
            }
        }

        /// <summary>
        /// Runs the computer turn. 
        /// </summary>
        private void RunComputerTurn()
        {
            RollScore = 0;

            if (hasWon) return;

            DisablePlayerInteraction();
            var computerRoll = gameLogic.GetComputerDiceRoll(DiabloScore);
            if(computerRoll <= 1)
            {
               // Do something here later. 
            }
            else
            {
                DiabloScore += computerRoll;
            }
            
            if (!isPlayAgain)
            {
                ResetButton.IsEnabled = true;
                return;
            }

            EnablePlayerControl();
        }

        /// <summary>
        /// Displays a player win message and prompts a game reset. 
        /// </summary>
        private void DisplayPlayerWinMessage()
        {
            if(MessageBox.Show(playerWinMessage, playerWinCaption, MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.Yes)
            {
                ResetGame();
            }
            else
            {
                RollButton.IsEnabled = false;
                PassButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Resets the state of the game. 
        /// </summary>
        private void ResetGame()
        {
            RollButton.IsEnabled = true;
            PassButton.IsEnabled = true;
            DiabloScore = 0;
            PlayerScore = 0;
            isPlayAgain = true;
            hasWon = false;
        }

        /// <summary>
        /// Displays a computer win message and prompts a game reset. 
        /// </summary>
        private void DisplayComputerWinMessage()
        {
            if (MessageBox.Show(computerWinMessage, computerWinCaption, MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.Yes)
            {
                ResetGame();
            }
            else
            {
                RollButton.IsEnabled = false;
                PassButton.IsEnabled = false;
                isPlayAgain = false;
            }
        }

        /// <summary>
        /// Enables player control ater the computer turn executes. 
        /// </summary>
        private void EnablePlayerControl()
        {
            RollButton.IsEnabled = true;
            PassButton.IsEnabled = true;
            ResetButton.IsEnabled = true;
        }

        /// <summary>
        /// Disables player control of the game and simulates a computer turn. 
        /// </summary>
        private void DisablePlayerInteraction()
        {
            RollButton.IsEnabled = false;
            PassButton.IsEnabled = false;
            ResetButton.IsEnabled = false;
        }

        /// <summary>
        /// Displays a bust message if the player busts. (rolls a 1)
        /// </summary>
        private void DisplayBustMessage()
        {
            MessageBox.Show(bustMessage, bustCaption, MessageBoxButton.OK);
        }

        /// <summary>
        /// Passes the turn from the player to the computer. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassButton_Click(object sender, RoutedEventArgs e)
        {
            PlayerScore += RollScore;
            RunComputerTurn();
        }

        /// <summary>
        /// Resets the game status. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }
    }
}
