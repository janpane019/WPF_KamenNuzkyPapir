using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> options = new List<string> { "Kámen", "Nůžky", "Papír" };
        private Random random { get; set; } = new Random();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int timerTicks = 0;
        private string playerChoice { get; set; }
        private int randChoiceIdx { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(ChangeLabel);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 400);
        }

        private void MakeChoice(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                playerChoice = (sender as Button).Content.ToString();
                PlayerChoiceLabel.Content = $"Vybral jsi {playerChoice}";
                ResultLabel.Content = "";
                randChoiceIdx = random.Next(3);
                dispatcherTimer.Start();
            }
        }

        private void StartGame(object sender)
        {
            int choiceIdx = options.IndexOf(playerChoice);
            if (choiceIdx == -1)
            {
                return;
            }
            dispatcherTimer.Start();
            PlayerChoiceLabel.Content = $"Vybral jsi {playerChoice}";
            OpponentChoiceLabel.Content = $"Protivník vybral {options[randChoiceIdx]}";
            if (randChoiceIdx == choiceIdx+1 || randChoiceIdx == 0 && choiceIdx == 2)
            {
                ResultLabel.Content = "Vyhrál jsi!";
            }
            else if(randChoiceIdx == choiceIdx)
            {
                ResultLabel.Content = "Remíza!";
            }
            else
            {
                ResultLabel.Content = "Prohrál jsi!";
            }
        }

        private void ChangeLabel(object sender, EventArgs e)
        {
            OpponentChoiceLabel.Content = $"Protivník vybral {options[timerTicks % 3]}";
            timerTicks++;
            if(timerTicks == 5)
            {
                timerTicks = 0;
                StartGame(sender);
                dispatcherTimer.Stop();
            }
        }
    }
}
