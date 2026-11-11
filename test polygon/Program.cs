using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public class Solution
{
    public int Calculate(string s)
    {
        var stack = new Stack<int>();

        string[] ss = Regex.Matches(s.Replace(" ", ""), @"\d+|[+\-*/]")
                           .Select(m => m.Value)
                           .ToArray();

        char op = '+';
        int i = 0;

        while (i < ss.Length)
        {
            string c = ss[i];

            if (c == "+" || c == "-" || c == "*" || c == "/")
            {
                op = c.First();
            }
            else
            {
                int num = int.Parse(c);
                switch (op)
                {
                    case '+': stack.Push(num); break;
                    case '-': stack.Push(-num); break;
                    case '*': stack.Push(stack.Pop() * num); break;
                    case '/': stack.Push(stack.Pop() / num); break;
                }
            }
            i++;
        }

        return stack.Sum();
    }
}