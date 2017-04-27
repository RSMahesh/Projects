using Collections = System.Collections.Generic;
using Reflect = System.Reflection;
using Emit = System.Reflection.Emit;
using IO = System.IO;
using System;
using System.Reflection;

namespace CodeGenerater
{
     sealed class CodeGen
    {
        Emit.ILGenerator il = null;
        Collections.Dictionary<string, Emit.LocalBuilder> symbolTable;
        Collections.Dictionary<string, Type> parameterTable;
        Collections.Dictionary<string, Emit.FieldBuilder> typefieldList;
        Emit.MethodBuilder methb;
        MethodGen parent;
        MethodDeclaration methodDec;

        string dllProbeDirectory = "";
        public CodeGen(Emit.MethodBuilder methb, MethodDeclaration methodDec, Collections.Dictionary<string, Emit.FieldBuilder> typefieldList, string _dllProbeDirectory,MethodGen _parent)
        {
            this.dllProbeDirectory = _dllProbeDirectory;
            this.typefieldList = typefieldList;
            this.parent = _parent;
            this.methb = methb;
            this.methodDec = methodDec;
        }

         public  void GenerateCode()
         {
             GenerateMethod(methb, methodDec);
             il.Emit(Emit.OpCodes.Ret);
         }

        private Emit.MethodBuilder GenerateMethod(Emit.MethodBuilder methb, MethodDeclaration method)
        {
            this.il = methb.GetILGenerator();
            this.symbolTable = new Collections.Dictionary<string, Emit.LocalBuilder>();
            this.parameterTable = new System.Collections.Generic.Dictionary<string, Type>();

            //foreach (string arg in method.parameters)
            //{
            //    parameterTable[arg] = typeof(string);
            //}

            foreach (Collections.KeyValuePair<string, Type> para in method.parameters)
            {
                parameterTable[para.Key] = para.Value;
            }


            this.GenStmt(method.stmt);

            Collections.List<Type> args = new System.Collections.Generic.List<Type>();

            foreach (Collections.KeyValuePair<string, Type> para in parameterTable)
            {
                args.Add(para.Value);
            }


            //foreach(string arg in method.parameters)
            //{
            //    args.Add(typeof(string));
            //}

            methb.SetParameters(args.ToArray());
            return methb;
        }

        private void GenStmt(Stmt stmt)
        {
            if (stmt is Sequence)
            {
                Sequence seq = (Sequence)stmt;
                this.GenStmt(seq.First);
                this.GenStmt(seq.Second);
            }

            else if (stmt is DeclareVar)
            {
                // declare a local
                DeclareVar declare = (DeclareVar)stmt;
                this.symbolTable[declare.Ident] = this.il.DeclareLocal(this.TypeOfExpr(declare.Expr));

                // set the initial value
                Assign assign = new Assign();
                assign.Ident = declare.Ident;
                assign.Expr = declare.Expr;
                this.GenStmt(assign);
            }

            else if (stmt is Assign)
            {
                Assign assign = (Assign)stmt;

                   if (this.typefieldList.ContainsKey(assign.Ident))
            {
                if (!this.typefieldList[assign.Ident].IsStatic)
                    this.il.Emit(Emit.OpCodes.Ldarg_0);
            }
                        

               // if(assign.Ident 
                this.GenExpr(assign.Expr, this.TypeOfExpr(assign.Expr));
                this.Store(assign.Ident, this.TypeOfExpr(assign.Expr));
            }
            else if (stmt is Print)
            {
                // the "print" statement is an alias for System.Console.WriteLine. 
                // it uses the string case
                this.GenExpr(((Print)stmt).Expr, typeof(string));
                this.il.Emit(Emit.OpCodes.Call, typeof(System.Console).GetMethod("WriteLine", new System.Type[] { typeof(string) }));
                // this.il.Emit(
            }
            else if (stmt is VoidMethodCall)
            {
                this.GenerteMethodCallCode((MethodCall)((VoidMethodCall)stmt).Expr);
            }

            else if (stmt is IfCondition)
            {
                IfCondition ifCon = (IfCondition)stmt;
                this.GenExpr(ifCon.BooleanExp,/*harcoded for string */ typeof(string));
      
                Emit.Label endOfIfBlock = this.il.DefineLabel();


                if (((BinExpr)ifCon.BooleanExp).Op == BinOp.EqualTo)
                {
                    this.il.Emit(Emit.OpCodes.Brfalse_S, endOfIfBlock);
                }
                else
                {
                    this.il.Emit(Emit.OpCodes.Brtrue_S, endOfIfBlock);
                }

                    this.GenStmt(ifCon.Body);

                this.il.MarkLabel(endOfIfBlock);
               // this.GenerteMethodCallCode((MethodCall)((VoidMethodCall)stmt).Expr);
            }

            else if (stmt is WhileLoop)
            {
                WhileLoop whileLoop = (WhileLoop)stmt;

                Emit.Label whileBodyStart = this.il.DefineLabel();
                Emit.Label whileBodyEnd = this.il.DefineLabel();
                Emit.Label whileCondition = this.il.DefineLabel();

                this.il.Emit(Emit.OpCodes.Br_S, whileBodyEnd);

                this.il.MarkLabel(whileBodyStart);
                this.GenStmt(whileLoop.Body);
                this.il.MarkLabel(whileBodyEnd);
                
                this.GenExpr(whileLoop.BooleanExp,/*harcoded for int */ typeof(int));

                if (((BinExpr)whileLoop.BooleanExp).Op == BinOp.EqualTo)
                {
                    this.il.Emit(Emit.OpCodes.Brtrue_S, whileBodyStart);
                }
                else
                {
                    this.il.Emit(Emit.OpCodes.Brfalse_S, whileBodyStart);
                }
            }


                // System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
           
            else if (stmt is ReadInt)
            {
                this.il.Emit(Emit.OpCodes.Call, typeof(System.Console).GetMethod("ReadLine", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, null, new System.Type[] { }, null));
                this.il.Emit(Emit.OpCodes.Call, typeof(int).GetMethod("Parse", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, null, new System.Type[] { typeof(string) }, null));
                this.Store(((ReadInt)stmt).Ident, typeof(int));
            }
            else if (stmt is ForLoop)
            {
                // example: 
                // for x = 0 to 100 do
                //   print "hello";
                // end;

                // x = 0
                ForLoop forLoop = (ForLoop)stmt;
                Assign assign = new Assign();
                assign.Ident = forLoop.Ident;
                assign.Expr = forLoop.From;
                this.GenStmt(assign);
                // jump to the test
                Emit.Label test = this.il.DefineLabel();
                this.il.Emit(Emit.OpCodes.Br, test);

                // statements in the body of the for loop
                Emit.Label body = this.il.DefineLabel();
                this.il.MarkLabel(body);
                this.GenStmt(forLoop.Body);

                // to (increment the value of x)
                this.il.Emit(Emit.OpCodes.Ldloc, this.symbolTable[forLoop.Ident]);
                this.il.Emit(Emit.OpCodes.Ldc_I4, 1);
                this.il.Emit(Emit.OpCodes.Add);
                this.Store(forLoop.Ident, typeof(int));

                // **test** does x equal 100? (do the test)
                this.il.MarkLabel(test);
                this.il.Emit(Emit.OpCodes.Ldloc, this.symbolTable[forLoop.Ident]);
                this.GenExpr(forLoop.To, typeof(int));
                this.il.Emit(Emit.OpCodes.Blt, body);
            }
            else
            {
                throw new System.Exception("don't know how to gen a " + stmt.GetType().Name);
            }

        }

        private void Store(string name, System.Type type)
        {
            if (this.symbolTable.ContainsKey(name))
            {
                Emit.LocalBuilder locb = this.symbolTable[name];

                if (locb.LocalType == type)
                {
                    this.il.Emit(Emit.OpCodes.Stloc, this.symbolTable[name]);
                }
                else
                {
                    throw new System.Exception("'" + name + "' is of type " + locb.LocalType.Name + " but attempted to store value of type " + type.Name);
                }
            }
            else if (this.typefieldList.ContainsKey(name))
            {
                if (this.typefieldList[name].IsStatic)
                    this.il.Emit(Emit.OpCodes.Stsfld, this.typefieldList[name]);
                else
                    this.il.Emit(Emit.OpCodes.Stfld, this.typefieldList[name]);
            }
            else
            {
                throw new System.Exception("undeclared variable '" + name + "'");
            }
        }

        private void GenExpr(Expr expr, System.Type expectedType)
        {
            System.Type deliveredType;

            if (expr is StringLiteral)
            {
                deliveredType = typeof(string);
                this.il.Emit(Emit.OpCodes.Ldstr, ((StringLiteral)expr).Value);
            }
            else if (expr is IntLiteral)
            {
                deliveredType = typeof(int);
                this.il.Emit(Emit.OpCodes.Ldc_I4, ((IntLiteral)expr).Value);
            }
            else if (expr is Variable)
            {
                string ident = ((Variable)expr).Ident;
                deliveredType = this.TypeOfExpr(expr);

                if (this.symbolTable.ContainsKey(ident))
                {
                    this.il.Emit(Emit.OpCodes.Ldloc, this.symbolTable[ident]);
                }
                else if (this.parameterTable.ContainsKey(ident))
                {
                    //Only for First paramete...have to change
                    this.il.Emit(Emit.OpCodes.Ldarg_S, Convert.ToByte(0));
                }
                else if (this.typefieldList.ContainsKey(ident))
                {
                    if (this.typefieldList[ident].IsStatic)
                        this.il.Emit(Emit.OpCodes.Ldsfld, this.typefieldList[ident]);
                    else
                    {
                        //Load current object(this) on stack.. which always first argument..
                        this.il.Emit(Emit.OpCodes.Ldarg_0);
                        this.il.Emit(Emit.OpCodes.Ldfld, this.typefieldList[ident]);
                    }
                }

                else
                {
                    throw new System.Exception("undeclared variable '" + ident + "'");
                }


            }
            else if (expr is BinExpr)
            {
                BinExpr binExpr = expr as BinExpr;
                deliveredType = this.TypeOfExpr(expr);

                if (binExpr.Left is IntLiteral)
                    this.il.Emit(Emit.OpCodes.Ldc_I4, ((IntLiteral)binExpr.Left).Value);
                if (binExpr.Left is Variable)
                    this.il.Emit(Emit.OpCodes.Ldloc, this.symbolTable[((Variable)binExpr.Left).Ident]);

                if (binExpr.Right is IntLiteral)
                    this.il.Emit(Emit.OpCodes.Ldc_I4, ((IntLiteral)binExpr.Right).Value);
                if (binExpr.Right is Variable)
                    this.il.Emit(Emit.OpCodes.Ldloc, this.symbolTable[((Variable)binExpr.Right).Ident]);

                switch (binExpr.Op)
                {
                    case BinOp.Add:
                        this.il.Emit(Emit.OpCodes.Add);
                        break;
                    case BinOp.Sub:
                        this.il.Emit(Emit.OpCodes.Sub);
                        break;
                    case BinOp.Mul:
                        this.il.Emit(Emit.OpCodes.Mul);
                        break;
                    case BinOp.Div:
                        this.il.Emit(Emit.OpCodes.Div);
                        break;
                    case BinOp.EqualTo:
                    case BinOp.NotEqualTo:
                        this.il.Emit(Emit.OpCodes.Ceq);
                        break;
                    
                }

            }
            else if (expr is MethodCall)
            {
                deliveredType = GenerteMethodCallCode((MethodCall)expr);

            }
            else
            {
                throw new System.Exception("don't know how to generate " + expr.GetType().Name);
            }

            if (deliveredType != expectedType)
            {
                if (deliveredType == typeof(int) &&
                    expectedType == typeof(string))
                {
                    this.il.Emit(Emit.OpCodes.Box, typeof(int));
                    this.il.Emit(Emit.OpCodes.Callvirt, typeof(object).GetMethod("ToString"));
                }
                else
                {
                    throw new System.Exception("can't coerce a " + deliveredType.Name + " to a " + expectedType.Name);
                }
            }

        }

        private System.Type TypeOfExpr(Expr expr)
        {
            if (expr is StringLiteral)
            {
                return typeof(string);
            }
            else if (expr is IntLiteral)
            {
                return typeof(int);
            }
            else if (expr is BinExpr)
            {
                return TypeOfExpr(((BinExpr)expr).Right);
            }

            else if (expr is Variable)
            {
                Variable var = (Variable)expr;
                if (this.symbolTable.ContainsKey(var.Ident))
                {
                    Emit.LocalBuilder locb = this.symbolTable[var.Ident];
                    return locb.LocalType;
                }
                else if (this.parameterTable.ContainsKey(var.Ident))
                {
                    return this.parameterTable[var.Ident];
                }
                else if (this.typefieldList.ContainsKey(var.Ident))
                {
                    return this.typefieldList[var.Ident].FieldType;
                }

                else
                {
                    throw new System.Exception("undeclared variable '" + var.Ident + "'");
                }
            }
            else if (expr is MethodCall)
            {
                MethodCall callMethod = (MethodCall)expr;
                if (callMethod.returnType == null)
                {
                    if (!callMethod.IsConstrutor)
                        callMethod.returnType = ((MethodInfo)GetMethodInfo(callMethod)).ReturnParameter.ParameterType;
                    else
                        callMethod.returnType = ((System.Reflection.MemberInfo)GetMethodInfo(callMethod)).ReflectedType;
                       
                 //   typefieldList = typeof(void);
                    // callMethod.returnType = ((ConstructorInfo)GetMethodInfo(callMethod)).ReturnParameter.ParameterType;
                }

                return callMethod.returnType;
            }

            else
            {
                throw new System.Exception("don't know how to calculate the type of " + expr.GetType().Name);
            }
        }

        private Type GenerteMethodCallCode(MethodCall objCall)
        {
            Type type = null;
          
            MethodBase methodBase = GetMethodInfo(objCall);

            foreach (object para in objCall.parameters)
            {
                string varaibleName = para.ToString();
                Type expectedType = null;
                if (symbolTable.ContainsKey(varaibleName))
                    expectedType = symbolTable[varaibleName].LocalType;
                else if (parameterTable.ContainsKey(varaibleName))
                    expectedType = parameterTable[varaibleName];
                else if (typefieldList.ContainsKey(varaibleName))
                    expectedType = typefieldList[varaibleName].FieldType;

                this.GenExpr(new Variable(varaibleName), expectedType);
            }

            if (objCall.IsConstrutor)
            {
                this.il.Emit(Emit.OpCodes.Newobj,(ConstructorInfo) methodBase);
                type = ((System.Reflection.MemberInfo)methodBase).ReflectedType;
            }
            else
            {
                int indx = 0;
                foreach (string  para in symbolTable.Keys)
                {
                  if(para.Equals(objCall.className, StringComparison.OrdinalIgnoreCase))
                  {
                       this.il.Emit(Emit.OpCodes.Ldloc_S ,Convert.ToByte(indx));
                       break;
                  }
                  indx++;  
                }

                //this.il.Emit(Emit.OpCodes.Callvirt,(MethodInfo) methodBase);
                this.il.Emit(Emit.OpCodes.Call, (MethodInfo)methodBase);
                try
                {
                    type = ((MethodInfo)methodBase).ReturnParameter.ParameterType;
                }
                catch (InvalidOperationException)
                {
                    type = parent.parent.GetMethodReturnType(objCall);
                }
            }
            return type;
        }

        private MethodBase GetMethodInfo(MethodCall objCall)
        {
            return parent.GetMethodInfo(objCall);
        }
      
        public Type GetVariableType(string varibaleName)
        {

            Type type = null;
            if (this.symbolTable.ContainsKey(varibaleName))
            {
                type = this.symbolTable[varibaleName].LocalType;
            }
            else if (this.parameterTable.ContainsKey(varibaleName))
            {
                type = this.parameterTable[varibaleName];
            }
            return type;
        }

    }
}