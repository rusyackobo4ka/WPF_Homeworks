using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text;

namespace Homework1;
// sdfhopgkjsdfhgopi
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        exspression = new StringBuilder("0");
        state = StateCalculator.Start;

        mathExpressionTextBlock.Text = exspression.ToString();

        //1. Переписать весь код с построением мат. выражение на StringBuilder() dddd
        //2. Написать алгоритм разбора мат. выражения и решение его (без переполнения) https://leetcode.com/problems/basic-calculator-ii/description/
    }

    private StringBuilder exspression;

    private StateCalculator state;

    private SymbolPriority Priority;

    short bucketBalance = 0;

    enum StateCalculator
    {
        Start,
        LasDigitOrBucket,
        LasOperator
    }

    enum SymbolPriority
    {
        Rank1 = 10,
        Rank2 = 9,
        Rank3 = 8,
        brucket = 7,
        Error = 404
    }

    private SymbolPriority Get_Symbol_Priority(char symbol)
    {
        switch (symbol)
        {
            case '^': return SymbolPriority.Rank1;
            case '*': case '/': return SymbolPriority.Rank2;
            case '+': case '-': return SymbolPriority.Rank3;
            case '(': return SymbolPriority.brucket;
            default: return SymbolPriority.Error;
        }
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

                    exspression.Clear();
                    exspression.Append(symbol);

                    state = StateCalculator.LasDigitOrBucket;
                }
                else if (state == StateCalculator.LasDigitOrBucket || state == StateCalculator.LasOperator)
                {
                    exspression.Append(symbol);
                    state = StateCalculator.LasDigitOrBucket;
                }
            }
            else
            {
                switch (symbol)
                {
                    case '(':
                        if (state == StateCalculator.Start)
                        {
                            exspression.Clear();
                            exspression.Append(symbol);

                            state = StateCalculator.LasDigitOrBucket;
                        }
                        bucketBalance++;
                        break;
                    case ')':
                        if (bucketBalance > 0)
                        {
                            exspression.Append(symbol);
                            bucketBalance--;
                        }
                        break;
                    default:
                        if (state == StateCalculator.LasOperator)
                        {
                            exspression.Remove(exspression.Length - 1, 1);
                            exspression.Append(symbol);
                            mathExpressionTextBlock.Text = exspression.ToString();
                            return;
                        }

                        exspression.Append(symbol);

                        state = StateCalculator.LasOperator;
                        break;
                }
            }
            mathExpressionTextBlock.Text = exspression.ToString();
        }
    }

    private string Tranform_Exspression()
    {
        StringBuilder endExspression = new StringBuilder();
        Stack<char> stack = new Stack<char>();

        SymbolPriority inRank;
        SymbolPriority outRank;

        foreach (char symbol in exspression.ToString())
        {
            if (char.IsDigit(symbol))
            {
                endExspression.Append(symbol);
            }

            else if (symbol == '(')
            {
                stack!.Push(symbol);
            }

            else if (symbol == ')')
            {
                while (stack!.Peek() != '(')
                {
                    endExspression.Append(' ');
                    endExspression.Append(stack.Pop());
                }
                stack.Pop();
            }

            else
            {
                if (!stack!.Any())
                {
                    endExspression.Append(' ');
                    stack!.Push(symbol);
                }

                else
                {
                    inRank = Get_Symbol_Priority(stack.Peek());
                    outRank = Get_Symbol_Priority(symbol);

                    if (outRank > inRank)
                    {
                        endExspression.Append(' ');
                        stack.Push(symbol);
                    }

                    else
                    {
                        while (stack!.Any())
                        {
                            endExspression.Append(' ');
                            endExspression.Append(stack.Pop());
                        }
                        endExspression.Append(' ');
                        stack.Push(symbol);
                    }
                }
            }
        }
        while (stack!.Any())
        {
            endExspression.Append(' ');
            endExspression.Append(stack.Pop());
        }

        return endExspression.ToString();
    }
    private string Calculate(string exspression)
    {
        Stack<double> stack = new Stack<double>();
        var examle = exspression.Split(' ');
        double secondOperant, firstOperant;

        foreach (string symbol in examle)
        {
            if (double.TryParse(symbol, out double tmp))
            {
                stack.Push(tmp);
            }

            else
            {
                switch (symbol)
                {
                    case "+":
                        stack.Push(stack.Pop() + stack.Pop());
                        break;
                    case "-":
                        stack.Push((stack.Pop() - stack.Pop()) * -1);
                        break;
                    case "*":
                        stack.Push(stack.Pop() * stack.Pop());
                        break;
                    case "/":
                        secondOperant = stack.Pop();
                        if (secondOperant == 0)
                        {
                            return "Error";
                        }
                        stack.Push(stack.Pop() / secondOperant);
                        break;
                    case "^":
                        secondOperant = stack.Pop();
                        firstOperant = stack.Pop();
                        double res = 1;
                        for (int i = 0; i < secondOperant; ++i)
                        {
                            res *= firstOperant;
                        }
                        stack.Push(res);
                        break;
                }
            }
        }

        return stack.Pop().ToString();
    }
    private void Button_Solve_Expression(object sender, RoutedEventArgs e)
    {
        if (bucketBalance != 0)
        {
            return;
        }
        string ans = Calculate(Tranform_Exspression());

        mathExpressionTextBlock.Text = ans;
        exspression = new StringBuilder("0");
        state = StateCalculator.Start;
    }

    private void Button_Delete_Last_Symbol(object sender, RoutedEventArgs e)
    {
        if (state == StateCalculator.Start)
        {
            return;
        }

        exspression.Remove(exspression.Length - 1, 1);

        if (exspression.Length == 0)
        {
            exspression = new StringBuilder("0");
            state = StateCalculator.Start;
            mathExpressionTextBlock.Text = exspression.ToString();
            return;
        }

        if (char.IsDigit(exspression
            .ToString()
            .Last()))
        { 
            state = StateCalculator.LasDigitOrBucket;
        }

        mathExpressionTextBlock.Text = exspression.ToString();
    }
}