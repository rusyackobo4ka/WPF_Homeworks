using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lesson2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        exspression = "0";
        state = StateCalculator.Start;

        mathExpressionTextBlock.Text = exspression;

        //1. Переписать весь код с построением мат. выражение на StringBuilder()
        //2. Написать алгоритм разбора мат. выражения и решение его (без переполнения) https://leetcode.com/problems/basic-calculator-ii/description/
    }

    private string exspression;

    private StateCalculator state;

    enum StateCalculator
    {
        Start,
        LasDigit,
        LasOperator
    }

    private void Button_Add_Digit(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            var symbolStr = (string)button.CommandParameter;
            var symbol = symbolStr.First();

            if (char.IsDigit(symbol))
            {
                if (state == StateCalculator.Start)
                {
                    if (symbol == '0')
                    {
                        return;
                    }

                    exspression = string.Empty;
                    exspression += symbol;

                    state = StateCalculator.LasDigit;
                }
                else if (state == StateCalculator.LasDigit || state == StateCalculator.LasOperator)
                {
                    exspression += symbol;
                    state = StateCalculator.LasDigit;
                }
            }
            else
            {
                if (state == StateCalculator.LasOperator)
                {
                    return;
                }

                exspression += symbol;

                state = StateCalculator.LasOperator;
            }

            mathExpressionTextBlock.Text = exspression;
        }
    }

    private void Button_Solve_Expression(object sender, RoutedEventArgs e)
    {
        //123+123-456/123
    }
}