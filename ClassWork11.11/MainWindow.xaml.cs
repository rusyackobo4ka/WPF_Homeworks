using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassWork11._11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int coins = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Check_Answers(object sender, RoutedEventArgs e)
        {
            coins = 0;

            if (Q1CorrectAns.IsChecked.Value)
            {
                coins += 20;
            }
            if (Q4CorrectAns.IsChecked.Value)
            {
                coins += 10;
            }

            int coinsForSecondtQuestion = 0;
            if (Q2Answer2Cor.IsChecked.Value && Q2Answer3Cor.IsChecked.Value && !Q2Answer1.IsChecked.Value && !Q2Answer4.IsChecked.Value)
            {
                coins += 30;
            }
            if (Q3Answer1Cor.IsChecked.Value && Q3Answer3Cor.IsChecked.Value && Q3Answer4Cor.IsChecked.Value && !Q3Answer2.IsChecked.Value && !Q3Answer5.IsChecked.Value)
            {
                coins += 30;
            }

            coins += coinsForSecondtQuestion;

            Result.Text = $"Выши очки = {coins}";
        }
    }
}