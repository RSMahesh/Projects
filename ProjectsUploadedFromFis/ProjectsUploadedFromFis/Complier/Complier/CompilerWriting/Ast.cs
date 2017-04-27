using System;
using System.Collections.Generic;
/* <stmt> := var <ident> = <expr>
	| <ident> = <expr>
	| for <ident> = <expr> to <expr> do <stmt> end
	| read_int <ident>
	| print <expr>
	| <stmt> ; <stmt>
  */

public class TypeDeclaration
{
    public string TypeName;
    public Dictionary<string, Type> instanceVariable = new Dictionary<string,Type>();
    public Dictionary<string, Type> classVariable = new Dictionary<string, Type>();
    public MethodDeclaration methodDec;
}

public class TypeSequence : TypeDeclaration
{
    public TypeDeclaration First;
    public TypeDeclaration Second;
}


public class MethodDeclaration
{
    public bool IsStatic;
    public string MethodName;
    public Stmt stmt;
    //public List<string> parameters = new List<string>();
    public Dictionary<string,Type> parameters = new Dictionary<string,Type>();
    public Type returnType;
}

public class MethodSequence : MethodDeclaration 
{
    public MethodDeclaration First;
    public MethodDeclaration Second;
}

public abstract class Stmt
{
}

// var <ident> = <expr>
public class DeclareVar : Stmt
{
    public string Ident;
    public Expr Expr;
}

// print <expr>
public class Print : Stmt
{
    public Expr Expr;
}

public class VoidMethodCall : Stmt
{
    public Expr Expr;
}

// <ident> = <expr>
public class Assign : Stmt
{
    public string Ident;
    public Expr Expr;
}

// for <ident> = <expr> to <expr> do <stmt> end
public class IfCondition : Stmt
{
    public Expr BooleanExp;
    public Stmt Body;
}

// for <ident> = <expr> to <expr> do <stmt> end
public class WhileLoop : Stmt
{
    public Expr BooleanExp;
    public Stmt Body;
}


// for <ident> = <expr> to <expr> do <stmt> end
public class ForLoop : Stmt
{
    public string Ident;
    public Expr From;
    public Expr To;
    public Stmt Body;
}

// read_int <ident>
public class ReadInt : Stmt
{
    public string Ident;
}

// <stmt> ; <stmt>
public class Sequence : Stmt
{
    public Stmt First;
    public Stmt Second;
}

/* <expr> := <string>
 *  | <int>
 *  | <arith_expr>
 *  | <ident>
 */
public abstract class Expr
{
}

// <string> := " <string_elem>* "
public class StringLiteral : Expr
{
	public string Value;
}

// <int> := <digit>+
public class IntLiteral : Expr
{
	public int Value;
}

// <ident> := <char> <ident_rest>*
// <ident_rest> := <char> | <digit>
public class Variable : Expr
{
    public Variable() { }
    public Variable(string ident) {this.Ident = ident;}
    public string Ident;
}

// <funtionCall> := Call <Funation>(<Parameters>) 

public class MethodCall : Expr
{
    public string assemblyName;
    public string className;
    public string methodName;
    public Type returnType;
    public List<object> parameters = new List<object>();
    public bool IsConstrutor;
}

// <bin_expr> := <expr> <bin_op> <expr>
public class BinExpr : Expr
{
	public Expr Left;
	public Expr Right;
	public BinOp Op;
}

// <bin_op> := + | - | * | /
public enum BinOp
{
	Add,
	Sub,
	Mul,
	Div,
    EqualTo,
    NotEqualTo
}



abstract public class  AMExpression
{

}

public class ADD : AMExpression
{
}
public class SUB : AMExpression
{
}

public class MUL : AMExpression
{
}

public class DIV : AMExpression
{
}

abstract public class LogicalExpression
{

}
public class EqualTo : LogicalExpression
{
}
public class NotEqualTo : LogicalExpression
{
}
