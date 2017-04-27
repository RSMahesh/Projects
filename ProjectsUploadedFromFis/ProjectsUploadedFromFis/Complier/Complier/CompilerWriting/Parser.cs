using Collections = System.Collections.Generic;
using Text = System.Text;

public sealed class Parser
{
    private int index;
    private Collections.IList<object> tokens;
    //private readonly Stmt result;
    //public  readonly MethodDeclaration result_fun;
    private readonly TypeDeclaration result;
    private ParserLanguageSetting languageSetting;

    public Parser(Collections.IList<object> tokens)
    {
        this.tokens = tokens;
        languageSetting = ParserLanguageSetting.GetInstance("languageSetting.xml");
        this.index = 0;
       // this.result = this.ParseStmt();
        this.result = this.ParseType();

        if (this.index != this.tokens.Count)
            throw new System.Exception("expected EOF");
    }

    //public Stmt Result
    //{
    //    get { return result; }
    //}

    public TypeDeclaration  Result
    {
        get { return result; }
    }

    private TypeDeclaration ParseType()
    {
        TypeDeclaration _result = new TypeDeclaration();
        if (this.index == this.tokens.Count)
        {
            throw new System.Exception("expected statement, got EOF");
        }

        if (!this.tokens[this.index++].Equals(languageSetting["Type"]))
            throw new System.Exception("Type Expected");

        _result.TypeName = this.tokens[this.index++].ToString();

        if (!this.tokens[this.index++].Equals(languageSetting["Start"]))
            throw new System.Exception("Start Expected");

         this.ParseClassVariable(_result);

        _result.methodDec = this.ParseMethod();

        if (!this.tokens[this.index++].Equals(languageSetting["End"]))
            throw new System.Exception("End Expected");

        if (this.index < this.tokens.Count &&
                this.tokens[this.index].Equals(languageSetting["Type"]))
        {
            TypeSequence TypeSeq = new TypeSequence();
            TypeSeq.First = _result;
            TypeSeq.Second = this.ParseType();
            _result = TypeSeq;

        }

        return _result;

    }

    private void ParseClassVariable(TypeDeclaration _type)
    {
        bool isStatic = false;

        if (this.tokens[this.index].Equals(languageSetting["ClassLevel"]) && !this.tokens[this.index+1].Equals(languageSetting["Method"]))
        {
            this.index++;isStatic = true; 
        }
        if (this.tokens[this.index].Equals(languageSetting["VariableDeclaration"]))
        {

            this.index++;
          
            if (this.index < this.tokens.Count &&
                this.tokens[this.index] is string)
            {
              if(isStatic)
                _type.classVariable.Add((string)this.tokens[this.index++], typeof(string));
              else
                _type.instanceVariable.Add((string)this.tokens[this.index++], typeof(string));
            }
            else
            {
                throw new System.Exception("expected variable name after 'var'");
            }

            if (this.index < this.tokens.Count && this.tokens[this.index++] != Scanner.Semi)
            {
                throw new System.Exception("expected end of statement");
            }

            this.ParseClassVariable(_type);
        }
    
    }


    private MethodDeclaration ParseMethod()
    {
        MethodDeclaration _result = new MethodDeclaration();
        _result.returnType = typeof(void);

        if (this.index == this.tokens.Count)
        {
            throw new System.Exception("expected statement, got EOF");
        }

        if (this.tokens[this.index].Equals(languageSetting["ClassLevel"]))
        {
            _result.IsStatic = true;
            this.index++;
        }

        if (!this.tokens[this.index++].Equals(languageSetting["Method"]))
            throw new System.Exception("Method Expected");

        _result.MethodName = this.tokens[this.index++].ToString();

     if(this.tokens[this.index++] != Scanner.OpeningParentheses)
         throw new System.Exception("Opening Paranthisis Expected");
    
    
        //TO DO :Logic for parametes
     while (this.tokens[this.index] != Scanner.ClosingParentheses)
     {

         if (this.tokens[this.index] != Scanner.Comma)
             // _result.parameters.Add(this.tokens[this.index].ToString());
             _result.parameters.Add(this.tokens[this.index].ToString(), typeof(string));
         index++;
     }


        
     if (this.tokens[this.index++] != Scanner.ClosingParentheses)
         throw new System.Exception("Closing Paranthisis Expected");

     if (!this.tokens[this.index++].Equals(languageSetting["Start"]))
         throw new System.Exception("Start Expected");

     _result.stmt  = this.ParseStmt();
     
        if (!this.tokens[this.index++].Equals(languageSetting["End"]))
         throw new System.Exception("End Expected");

     if (this.index < this.tokens.Count -1 &&
             (this.tokens[this.index].Equals(languageSetting["Method"]) ||
             this.tokens[this.index + 1].Equals(languageSetting["Method"])
             ))
     {
         MethodSequence methodSeq = new MethodSequence();
         methodSeq.First = _result;
         methodSeq.Second = this.ParseMethod();
         _result = methodSeq;

     }
        
        return _result;
    
    }


    private Stmt ParseStmt()
    {
        Stmt result = null;

        if (this.index == this.tokens.Count)
        {
            throw new System.Exception("expected statement, got EOF");
        }

        // <stmt> := print <expr> 

        // <expr> := <string>
        // | <int>
        // | <arith_expr>
        // | <ident>
        if (this.tokens[this.index].Equals(languageSetting["End"]))
        {
            this.index++;
        }
        else if (this.tokens[this.index].Equals(languageSetting["Start"]))
        {
            this.index++;
            return ParseStmt();
        }
      
        else if (this.tokens[this.index].Equals(languageSetting["Print"]))
        {
            this.index++;
            Print print = new Print();
            print.Expr = this.ParseExpr();
            result = print;
        }

        else if (this.tokens[this.index] == Scanner.Call)
        {
            VoidMethodCall vmc = new VoidMethodCall();
            vmc.Expr = this.ParseExpr();
            result = vmc;
        }
        else if (this.tokens[this.index].Equals(languageSetting["VariableDeclaration"]))
        {
            this.index++;
            DeclareVar declareVar = new DeclareVar();

            if (this.index < this.tokens.Count &&
                this.tokens[this.index] is string)
            {
                declareVar.Ident = (string)this.tokens[this.index];
            }
            else
            {
                throw new System.Exception("expected variable name after 'var'");
            }

            this.index++;

            if (this.index == this.tokens.Count ||
                this.tokens[this.index] != Scanner.Equal)
            {
                throw new System.Exception("expected = after 'var ident'");
            }

            this.index++;

            declareVar.Expr = this.ParseExpr();
            result = declareVar;
        }
        else if (this.tokens[this.index].Equals(languageSetting["if"]))
        {
            this.index++;
            IfCondition ifCon = new IfCondition();
            ifCon.BooleanExp  = this.ParseExpr();
            
            if (!this.tokens[++this.index].Equals(languageSetting["Start"]))
                throw new System.Exception("Start Expected");
            
            ifCon.Body = this.ParseStmt();

            if (!this.tokens[this.index++].Equals(languageSetting["End"]))
                throw new System.Exception("End Expected");

            result = ifCon;
        }


        else if (this.tokens[this.index].Equals(languageSetting["while"]))
        {
            this.index++;
            WhileLoop whileLoop = new WhileLoop();
            whileLoop.BooleanExp = this.ParseExpr();

            if (!this.tokens[++this.index].Equals(languageSetting["Start"]))
                throw new System.Exception("Start Expected");

            whileLoop.Body = this.ParseStmt();

            if (!this.tokens[this.index++].Equals(languageSetting["End"]))
                throw new System.Exception("End Expected");

            result = whileLoop;
        }


        else if (this.tokens[this.index].Equals("read_int"))
        {
            this.index++;
            ReadInt readInt = new ReadInt();

            if (this.index < this.tokens.Count &&
                this.tokens[this.index] is string)
            {
                readInt.Ident = (string)this.tokens[this.index++];
                result = readInt;
            }
            else
            {
                throw new System.Exception("expected variable name after 'read_int'");
            }
        }
        else if (this.tokens[this.index].Equals("for"))
        {
            this.index++;
            ForLoop forLoop = new ForLoop();

            if (this.index < this.tokens.Count &&
                this.tokens[this.index] is string)
            {
                forLoop.Ident = (string)this.tokens[this.index];
            }
            else
            {
                throw new System.Exception("expected identifier after 'for'");
            }

            this.index++;

            if (this.index == this.tokens.Count ||
                this.tokens[this.index] != Scanner.Equal)
            {
                throw new System.Exception("for missing '='");
            }

            this.index++;

            forLoop.From = this.ParseSingleExpression();

            if (this.index == this.tokens.Count ||
                !this.tokens[this.index].Equals("to"))
            {
                throw new System.Exception("expected 'to' after for");
            }

            this.index++;

            forLoop.To = this.ParseSingleExpression();

            if (this.index == this.tokens.Count ||
                !this.tokens[this.index].Equals("do"))
            {
                throw new System.Exception("expected 'do' after from expression in for loop");
            }

            this.index++;

            forLoop.Body = this.ParseStmt();
            result = forLoop;

            if (this.index == this.tokens.Count ||
                !this.tokens[this.index].Equals("end"))
            {
                throw new System.Exception("unterminated 'for' loop body");
            }

            this.index++;
        }
        else if (this.tokens[this.index] is string)
        {
            // assignment

            Assign assign = new Assign();
            assign.Ident = (string)this.tokens[this.index++];

            if (this.index == this.tokens.Count ||
                this.tokens[this.index] != Scanner.Equal)
            {
                throw new System.Exception("expected '='");
            }

            this.index++;

            assign.Expr = this.ParseExpr();
            result = assign;
        }
        else
        {
            throw new System.Exception("parse error at token " + this.index + ": " + this.tokens[this.index]);
        }

        if (this.index < this.tokens.Count && this.tokens[this.index] == Scanner.Semi)
        {
            this.index++;

            if (this.index < this.tokens.Count &&
                !this.tokens[this.index].Equals(languageSetting["End"]))
            {
                Sequence sequence = new Sequence();
                sequence.First = result;
                sequence.Second = this.ParseStmt();
                result = sequence;
            }
        }

        return result;
    }

    private Expr ParseExpr()
    {
        if (this.index == this.tokens.Count)
        {
            throw new System.Exception("expected expression, got EOF");
        }

        Expr expr = null;

        while (this.tokens[this.index] != Scanner.Semi)
        {
            if (this.tokens[this.index] is Text.StringBuilder)
            {
                string value = ((Text.StringBuilder)this.tokens[this.index++]).ToString();
                StringLiteral stringLiteral = new StringLiteral();
                stringLiteral.Value = value;
                return stringLiteral;
            }
            else if (this.tokens[this.index] is AMExpression)
            {
                AMExpression amExpression = (AMExpression)this.tokens[this.index++];
                BinExpr binExpr = new BinExpr();
                binExpr.Op = BinOp.Mul;

                if (amExpression is ADD)
                    binExpr.Op = BinOp.Add;
                else if (amExpression is SUB)
                    binExpr.Op = BinOp.Sub;
                else if (amExpression is MUL)
                    binExpr.Op = BinOp.Mul;
                else if (amExpression is DIV)
                    binExpr.Op = BinOp.Div;

                binExpr.Left = expr;
                expr = binExpr;
            }
            else if (this.tokens[this.index] is LogicalExpression)
            {
                LogicalExpression logicalExpression = (LogicalExpression)this.tokens[this.index++];
                BinExpr binExpr = new BinExpr();
              
                if (logicalExpression is EqualTo)
                    binExpr.Op  = BinOp.EqualTo;
                else if (logicalExpression is NotEqualTo)
                    binExpr.Op = BinOp.NotEqualTo;
            
                binExpr.Left = expr;
                expr = binExpr;
            }
           
            else if (this.tokens[this.index] is int && expr != null)
            {
                int intValue = (int)this.tokens[this.index++];
                IntLiteral intLiteral = new IntLiteral();
                intLiteral.Value = intValue;
                BinExpr binExp = expr as BinExpr;
                binExp.Right = intLiteral;

            }

            else if (this.tokens[this.index] is int && expr == null)
            {
                int intValue = (int)this.tokens[this.index++];
                IntLiteral intLiteral = new IntLiteral();
                intLiteral.Value = intValue;
                expr = intLiteral;
            }
            else if (this.tokens[this.index] is string && expr != null)
            {
                string ident = (string)this.tokens[this.index++];
                Variable var = new Variable();
                var.Ident = ident;
                BinExpr binExp = expr as BinExpr;
                binExp.Right = var;
            }
            else if (this.tokens[this.index] is string && expr == null)
            {
                string ident = (string)this.tokens[this.index++];
                Variable var = new Variable();
                var.Ident = ident;
                expr = var;
            }
            else if (this.tokens[this.index] == Scanner.newobj)
            {
                this.index++;
                expr = ParseMethodCall(true);
            }
          
            else if (this.tokens[this.index++] == Scanner.Call)
            {
                expr = ParseMethodCall(false);
            }
            else
            {
                throw new System.Exception("expected string literal, int literal, or variable");
            }
        }

        return expr;
    }


    private MethodCall ParseMethodCall(bool IsConstructor)
    {
        MethodCall callMethod;
        GetClassAndFunctionName(out callMethod,IsConstructor);
        if (this.tokens[this.index++] != Scanner.OpeningParentheses)
            throw new System.Exception("Invalid Function Syntax");

        while (true)
        {
            if (this.tokens[this.index] == Scanner.ClosingParentheses)
            {
                this.index++;
                break;
            }
            if (this.tokens[this.index] == Scanner.Comma)
                this.index++;
            callMethod.parameters.Add(this.tokens[this.index++]);
        }

    //    callMethod.assemblyName = @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\mscorlib.dll";
      //  callMethod.assemblyName = @"C:\Mahesh\Projects\Complier\ClassLibrary1\bin\Debug\ClassLibraryTest.dll";
        return callMethod;
    }


    

    private void GetClassAndFunctionName(out MethodCall callMethod, bool IsConstructor)
    {
        callMethod = new MethodCall();
        string temp = this.tokens[this.index++].ToString();

        int indx = temp.LastIndexOf('.');

        if (indx == -1)
        {
            callMethod.className = "this";
            callMethod.methodName = temp;
            return;
        }

        if (IsConstructor)
        {
            callMethod.IsConstrutor = true;
            callMethod.className = temp;
            callMethod.methodName = ".ctor";
        }
        else
        {
            callMethod.className = temp.Substring(0, indx);
            callMethod.methodName = temp.Substring(++indx);
        }

    }

    private Expr ParseSingleExpression()
    {
        if (this.index == this.tokens.Count)
        {
            throw new System.Exception("expected expression, got EOF");
        }

        if (this.tokens[this.index] is Text.StringBuilder)
        {
            string value = ((Text.StringBuilder)this.tokens[this.index++]).ToString();
            StringLiteral stringLiteral = new StringLiteral();
            stringLiteral.Value = value;
            return stringLiteral;
        }
        else if (this.tokens[this.index] is int)
        {
            int intValue = (int)this.tokens[this.index++];
            IntLiteral intLiteral = new IntLiteral();
            intLiteral.Value = intValue;
            return intLiteral;
        }
        else if (this.tokens[this.index] is string)
        {
            string ident = (string)this.tokens[this.index++];
            Variable var = new Variable();
            var.Ident = ident;
            return var;
        }
        else
        {
            throw new System.Exception("expected string literal, int literal, or variable");
        }
    }
}
