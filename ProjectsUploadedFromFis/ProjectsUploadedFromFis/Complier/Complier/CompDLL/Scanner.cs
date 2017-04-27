using System;
using Collections = System.Collections.Generic;
using IO = System.IO;
using Text = System.Text;

public sealed class Scanner
{
    private readonly Collections.IList<object> result;
    private ParserLanguageSetting languageSetting;
    private readonly Collections.Dictionary<int, int> tokenLineNumber;
    private int currentLineNumber;

    public Scanner(IO.TextReader input)
    {
        this.result = new Collections.List<object>();
        tokenLineNumber = new System.Collections.Generic.Dictionary<int, int>();
        languageSetting = ParserLanguageSetting.GetInstance("languageSetting.xml");
        currentLineNumber = 1;
        this.Scan(input);
    }

    public Collections.IList<object> Tokens
    { 
        get { return this.result; }
    }

    #region ArithmiticConstants

    // Constants to represent arithmitic tokens. This could
    // be alternatively written as an enum.
    public static readonly object Add = new ADD();
    public static readonly object Sub = new SUB();
    public static readonly object Mul = new MUL();
    public static readonly object Div = new DIV();
    public static readonly object Semi = new object();
    public static readonly object Equal = new object();
    public static readonly object EqualTo = new EqualTo();
    public static readonly object NotEqualTo = new NotEqualTo();
    public static readonly object Call = new object();
    public static readonly object OpeningParentheses = new object();
    public static readonly object ClosingParentheses = new object();
    public static readonly object Comma = new object();
    public static readonly object newobj = new object();



    #endregion

    private void Scan(IO.TextReader input)
    {
        while (input.Peek() != -1)
        {
            char ch = (char)input.Peek();
            


            // Scan individual tokens
            if (char.IsWhiteSpace(ch))
            {
                if (ch == '\n')
                    currentLineNumber++;
                // eat the current char and skip ahead!
                input.Read();
            }
            else if (IsLetter(ch))
            {
                // keyword or identifier

                Text.StringBuilder accum = new Text.StringBuilder();

                while (IsLetter(ch) || ch == '.')
                {
                    accum.Append(ch);
                    input.Read();

                    if (input.Peek() == -1)
                    {
                        break;
                    }
                    else
                    {
                        ch = (char)input.Peek();
                    }
                }
                if (IsAmExpression(accum) || IsFunctionCall(accum) || IsNewObjCall(accum) || IsLogicalExpression(accum))
                    ;
                else
                {
                    if (result.Count > 0 && this.result[result.Count - 1] == Scanner.Call)
                        AddToken(languageSetting[accum.ToString()] ?? accum.ToString());
                    else
                        AddToken(accum.ToString());
                }
            }
            else if (ch == '"')
            {
                // string literal
                Text.StringBuilder accum = new Text.StringBuilder();

                input.Read(); // skip the '"'

                if (input.Peek() == -1)
                {
                    throw new System.Exception("unterminated string literal");
                }

                while ((ch = (char)input.Peek()) != '"')
                {
                    accum.Append(ch);
                    input.Read();

                    if (input.Peek() == -1)
                    {
                        throw new System.Exception("unterminated string literal");
                    }
                }

                // skip the terminating"
                input.Read();
                AddToken(accum);
            }
            else if (char.IsDigit(ch))
            {
                // numeric literal

                Text.StringBuilder accum = new Text.StringBuilder();

                while (char.IsDigit(ch))
                {
                    accum.Append(ch);
                    input.Read();

                    if (input.Peek() == -1)
                    {
                        break;
                    }
                    else
                    {
                        ch = (char)input.Peek();
                    }
                }

                AddToken(int.Parse(accum.ToString()));
            }
            //Obselete Code
            else switch (ch)
                {
                    case '+':
                        input.Read();
                        AddToken(Scanner.Add);
                        break;

                    case '-':
                        input.Read();
                        AddToken(Scanner.Sub);
                        break;

                    case '*':
                        input.Read();
                        AddToken(Scanner.Mul);
                        break;

                    case '/':
                        input.Read();
                        AddToken(Scanner.Div);
                        break;

                    case '=':
                        input.Read();
                        AddToken(Scanner.Equal);
                        break;

                    case ';':
                        input.Read();
                        AddToken(Scanner.Semi);
                        break;

                    case '(':
                        input.Read();
                        AddToken(Scanner.OpeningParentheses);
                        break;

                    case ')':
                        input.Read();
                        AddToken(Scanner.ClosingParentheses);
                        break;

                    case ',':
                        input.Read();
                        AddToken(Scanner.Comma);
                        break;


                    default:
                        throw new System.Exception("Scanner encountered unrecognized character '" + ch + "'");
                }

        }
    }

    private bool IsAmExpression(Text.StringBuilder keyWord)
    {
        string uKeyWord = keyWord.ToString().ToUpperInvariant();

        if (string.Compare(uKeyWord, languageSetting["+"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            AddToken(Scanner.Add);
            return true;
        }
        else if (string.Compare(uKeyWord, languageSetting["-"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            AddToken(Scanner.Sub);
            return true;
        }
        else if (string.Compare(uKeyWord, languageSetting["*"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            AddToken(Scanner.Mul);
            return true;
        }
        else if (string.Compare(uKeyWord, languageSetting["/"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            AddToken(Scanner.Div);
            return true;
        }
        else
        {
            return false;
        }

    }

      private bool IsLogicalExpression(Text.StringBuilder keyWord)
    {
        string uKeyWord = keyWord.ToString().ToUpperInvariant();

          if (string.Compare(uKeyWord, languageSetting["EqualTo"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            AddToken(Scanner.EqualTo);
            return true;
        }
        if (string.Compare(uKeyWord, languageSetting["NotEqualTo"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            AddToken(Scanner.NotEqualTo);
            return true;
        }
    
        else
        {
            return false;
        }

    }

    private bool IsFunctionCall(Text.StringBuilder keyWord)
    {
        string uKeyWord = keyWord.ToString();
        if (string.Compare(uKeyWord, languageSetting["Call"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            AddToken(Scanner.Call);
            return true;
        }
        else
        {
            return false;
        }


    }
    private bool IsNewObjCall(Text.StringBuilder keyWord)
    {
        string uKeyWord = keyWord.ToString();
        if (string.Compare(uKeyWord, languageSetting["new"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            AddToken(Scanner.newobj);
            return true;
        }
        else
        {
            return false;
        }


    }

    private bool IsLetter(char ch)
    {
        if (Char.IsLetter(ch) || ch == '_' || ch == '$')
            return true;
        return false;
    }

    private void AddToken(object token)
    {
     this.result.Add(token);
     this.tokenLineNumber[this.result.Count] = currentLineNumber;
    }

    public int GetTokenLineNumber(int tokenNumber)
    {
        return this.tokenLineNumber[tokenNumber];
    }
}


