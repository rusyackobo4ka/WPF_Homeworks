using System.Linq.Expressions;
using System.Text;

string s = "33+6*(10-5)";
test abc = new test(s);

abc.PrintRes();
class test
{
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

    public test(string s)
    {
        exspression = new StringBuilder(s);
    }

    private StringBuilder exspression;
    enum SymbolPriority
    {
        Rank1 = 10,
        Rank2 = 9,
        Rank3 = 8,
        brucket = 7,
        Error = 404
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
        Stack<int> stack = new Stack<int>();
        var examle = exspression.Split(' ');
        int secondOperant;

        foreach (string symbol in examle)
        {
            if (int.TryParse(symbol, out int tmp))
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
                        secondOperant = stack.Pop();
                        stack.Push(stack.Pop() - secondOperant);
                        break;
                    case "*":
                        stack.Push(stack.Pop() * stack.Pop());
                        break;
                    case "/":
                        secondOperant = stack.Pop();
                        stack.Push(stack.Pop() / secondOperant);
                        break;
                }
            }
        }

        return stack.Pop().ToString();
    }

    public void PrintRes()
    {
        Console.WriteLine(Calculate(Tranform_Exspression())); 
    }
}