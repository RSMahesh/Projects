using System;
using Collections = System.Collections.Generic;
using IO = System.IO;
using Text = System.Text;

public sealed class Scanner
{
    private readonly Collections.IList<object> result;
    private ParserLanguageSetting languageSetting;

    public Scanner(IO.TextReader input)
    {
        this.result = new Collections.List<object>();
        languageSetting = ParserLanguageSetting.GetInstance("languageSetting.xml");
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
                        this.result.Add(languageSetting[accum.ToString()] ?? accum.ToString());
                    else
                        this.result.Add(accum.ToString());
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
                this.result.Add(accum);
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

                this.result.Add(int.Parse(accum.ToString()));
            }
            //Obselete Code
            else switch (ch)
                {
                    case '+':
                        input.Read();
                        this.result.Add(Scanner.Add);
                        break;

                    case '-':
                        input.Read();
                        this.result.Add(Scanner.Sub);
                        break;

                    case '*':
                        input.Read();
                        this.result.Add(Scanner.Mul);
                        break;

                    case '/':
                        input.Read();
                        this.result.Add(Scanner.Div);
                        break;

                    case '=':
                        input.Read();
                        this.result.Add(Scanner.Equal);
                        break;

                    case ';':
                        input.Read();
                        this.result.Add(Scanner.Semi);
                        break;

                    case '(':
                        input.Read();
                        this.result.Add(Scanner.OpeningParentheses);
                        break;

                    case ')':
                        input.Read();
                        this.result.Add(Scanner.ClosingParentheses);
                        break;

                    case ',':
                        input.Read();
                        this.result.Add(Scanner.Comma);
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
            this.result.Add(Scanner.Add);
            return true;
        }
        else if (string.Compare(uKeyWord, languageSetting["-"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            this.result.Add(Scanner.Sub);
            return true;
        }
        else if (string.Compare(uKeyWord, languageSetting["*"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            this.result.Add(Scanner.Mul);
            return true;
        }
        else if (string.Compare(uKeyWord, languageSetting["/"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            this.result.Add(Scanner.Div);
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
            this.result.Add(Scanner.EqualTo);
            return true;
        }
        if (string.Compare(uKeyWord, languageSetting["NotEqualTo"], StringComparison.OrdinalIgnoreCase) == 0)
        {
            this.result.Add(Scanner.NotEqualTo);
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
            this.result.Add(Scanner.Call);
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
            this.result.Add(Scanner.newobj);
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
}


