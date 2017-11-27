using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            string tokenString = "+ * 10 2 3";
            List<string> tokenList = new List<string>(tokenString.Split(' '));

            IExpression expression = new TokenReader().ReadToken(tokenList);
            Console.WriteLine(expression.Interpret());    // (10 * 2) + 3 = 23

            tokenString = "- + 10 5 - 8 2";
            tokenList = new List<string>(tokenString.Split(' '));

            expression = new TokenReader().ReadToken(tokenList);
            Console.WriteLine(expression.Interpret());   // (10 + 5) - (8 - 2) = 9

            Console.ReadLine();
        }
    }

    public interface IExpression
    {
        int Interpret();
    }

    //terminal expression
    public class NumberExpression : IExpression
    {
        int number;
        public NumberExpression(int i)
        {
            number = i;
        }

        int IExpression.Interpret()
        {
            return number;
        }
    }

    //nonterminal expression, contains left and right expressions below it
    public class AddExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public AddExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        int IExpression.Interpret()
        {
            return leftExpression.Interpret() + rightExpression.Interpret();
        }
    }

    //nonterminal expression, contains left and right expressions below it
    public class SubtractExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public SubtractExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        int IExpression.Interpret()
        {
            return leftExpression.Interpret() - rightExpression.Interpret();
        }
    }

    public class MultipleExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public MultipleExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        int IExpression.Interpret()
        {
            return leftExpression.Interpret() * rightExpression.Interpret();
        }
    }


    public class TokenReader
    {
        public IExpression ReadToken(List<string> tokenList)
        {
            return ReadNextToken(tokenList);
        }

        private IExpression ReadNextToken(List<string> tokenList)
        {
            int i;
            if (int.TryParse(tokenList.First(), out i))  //if the token is integer (terminal)
            {
                tokenList.RemoveAt(0);   //process terminal expression
                return new NumberExpression(i);
            }
            else
            {
                return ReadNonTerminal(tokenList);  //process nonTerminal expression
            }
        }

        private IExpression ReadNonTerminal(List<string> tokenList)
        {
            string token = tokenList.First();
            tokenList.RemoveAt(0);   //read the symbol
            IExpression left = ReadNextToken(tokenList); //read left expression
            IExpression right = ReadNextToken(tokenList);  //read right expression

            if (token == "+")
                return new AddExpression(left, right);
            else if (token == "-")
                return new SubtractExpression(left, right);
            else if (token == "*")
                return new MultipleExpression(left, right);
            return null;
        }
    }

}
