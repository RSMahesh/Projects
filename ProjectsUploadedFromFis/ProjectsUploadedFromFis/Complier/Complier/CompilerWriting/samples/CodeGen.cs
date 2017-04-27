using Collections = System.Collections.Generic;
using Reflect = System.Reflection;
using Emit = System.Reflection.Emit;
using IO = System.IO;
using System;
using System.Reflection;

namespace CodeGenerater
{
    public sealed class CodeGen_AAAAAAAAAAAAAAAAAAAAA
    {
        Emit.ILGenerator il = null;
        Collections.Dictionary<string, Emit.LocalBuilder> symbolTable;
        string dllProbeDirectory = "";
        public CodeGen_AAAAAAAAAAAAAAAAAAAAA(MethodDeclaration methodDec, string moduleName, string _dllProbeDirectory)
        {
            if (IO.Path.GetFileName(moduleName) != moduleName)
            {
                throw new System.Exception("can only output into current directory!");
            }

            this.dllProbeDirectory = _dllProbeDirectory;
            Reflect.AssemblyName name = new Reflect.AssemblyName(IO.Path.GetFileNameWithoutExtension(moduleName));
            Emit.AssemblyBuilder asmb = System.AppDomain.CurrentDomain.DefineDynamicAssembly(name, Emit.AssemblyBuilderAccess.Save);
            Emit.ModuleBuilder modb = asmb.DefineDynamicModule(moduleName);
            Emit.TypeBuilder typeBuilder = modb.DefineType("Foo");

            //    Emit.MethodBuilder methb = typeBuilder.DefineMethod("Main", Reflect.MethodAttributes.Static, typeof(void), System.Type.EmptyTypes);

            // CodeGenerator
            //  this.il = methb.GetILGenerator();
            //  this.symbolTable = new Collections.Dictionary<string, Emit.LocalBuilder>();

            // Go Compile!
            // this.GenStmt(stmt);
            Emit.MethodBuilder methb = GenerateMethod(typeBuilder, methodDec);
            il.Emit(Emit.OpCodes.Ret);
            typeBuilder.CreateType();
            modb.CreateGlobalFunctions();
            asmb.SetEntryPoint(methb);
            asmb.Save(moduleName);
            this.symbolTable = null;
            this.il = null;
        }

        private Emit.MethodBuilder GenerateMethod(Emit.TypeBuilder typeBuilder, MethodDeclaration method)
        {

            Emit.MethodBuilder methb = typeBuilder.DefineMethod(method.MethodName, Reflect.MethodAttributes.Static, typeof(void), System.Type.EmptyTypes);
            this.il = methb.GetILGenerator();
            this.symbolTable = new Collections.Dictionary<string, Emit.LocalBuilder>();
            this.GenStmt(method.stmt);
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

                if (!this.symbolTable.ContainsKey(ident))
                {
                    throw new System.Exception("undeclared variable '" + ident + "'");
                }

                this.il.Emit(Emit.OpCodes.Ldloc, this.symbolTable[ident]);
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
                else
                {
                    throw new System.Exception("undeclared variable '" + var.Ident + "'");
                }
            }
            else if (expr is MethodCall)
            {
                MethodCall callMethod = (MethodCall)expr;
                if (callMethod.returnType == null && !callMethod.IsConstrutor)
                {
                    callMethod.returnType = GetMethodInfo(callMethod).ReturnParameter.ParameterType;
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
            MethodInfo methodInfo = GetMethodInfo(objCall);
            foreach (object para in objCall.parameters)
            {
                string varaibleName = para.ToString();
                this.GenExpr(new Variable(varaibleName), symbolTable[varaibleName].LocalType);
            }

            this.il.Emit(Emit.OpCodes.Call, methodInfo);
            type = methodInfo.ReturnParameter.ParameterType;
            return type;
        }


        private MethodInfo GetMethodInfo(MethodCall objCall)
        {
            Collections.List<string> dllPath = null;

            System.Reflection.Assembly objAssembly = null;
            Type classType = null;
            MethodInfo methodInfo = null;

            if (objCall.assemblyName == null)
            {
                dllPath = GetAssemblyPath(objCall.className);
            }
            else
            {
                dllPath = new System.Collections.Generic.List<string>();
                dllPath.Add(objCall.assemblyName);
            }

            foreach (string path in dllPath)
            {

                methodInfo = null;
                objAssembly = System.Reflection.Assembly.LoadFrom(path);
                classType = objAssembly.GetType(objCall.className);
                if (classType == null)
                    continue;

                if (objCall.parameters.Count < 1)
                    methodInfo = classType.GetMethod(objCall.methodName, new System.Type[] { });
                else
                {
                    Collections.List<Type> types = new System.Collections.Generic.List<Type>();
                    foreach (object para in objCall.parameters)
                    {
                        string varaibleName = para.ToString();
                        types.Add(this.symbolTable[varaibleName].LocalType);
                    }

                    methodInfo = classType.GetMethod(objCall.methodName, types.ToArray());
                    if (methodInfo != null)
                    {
                        objCall.assemblyName = path;
                        break;
                    }
                }
            }

            if (methodInfo == null)
                throw new Exception("Method Not Found:" + objCall.className + "." + objCall.methodName);

            return methodInfo;
        }

        private Collections.List<string> GetAssemblyPath(string fullyQualifiedTypeName)
        {
            char[] split = new char[] { '.' };
            string assemblyPath = "";
            string[] arr = fullyQualifiedTypeName.Split(split);
            Collections.List<string> dllPath = new System.Collections.Generic.List<string>(10);
            if (arr[0].Equals("System", StringComparison.OrdinalIgnoreCase))
            {
                dllPath.Add(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\mscorlib.dll");
            }
            else
            {
                string DirectoryName = IO.Path.GetDirectoryName(dllProbeDirectory);
                string[] files = IO.Directory.GetFiles(DirectoryName);

                foreach (string file in files)
                    if (IO.Path.GetExtension(file) == ".dll")
                        dllPath.Add(file);
            }

            return dllPath;
        }

        private void CallMethodTest()
        {

            //CallMethod objCall = new CallMethod();
            //objCall.assemblyName = @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\mscorlib.dll";
            //objCall.className = "System.Console";
            //objCall.methodName = "WriteLine";
            //Variable var = new Variable();
            //var.Ident = "A";
            //objCall.parameters.Add(var);
            //string varaibleName = ((Variable) objCall.parameters[0]).Ident;
            //this.GenExpr((Expr) objCall.parameters[0],symbolTable[varaibleName].LocalType);
            //System.Reflection.Assembly objAssembly = System.Reflection.Assembly.LoadFrom(objCall.assemblyName);
            //Type classType = objAssembly.GetType(objCall.className);
            //this.il.Emit(Emit.OpCodes.Call, classType.GetMethod("WriteLine", new System.Type[] { symbolTable[varaibleName].LocalType }));


            MethodCall objCall = new MethodCall();
            objCall.assemblyName = @"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\mscorlib.dll";
            objCall.className = "System.Console";
            objCall.methodName = "ReadLine";
            System.Reflection.Assembly objAssembly = System.Reflection.Assembly.LoadFrom(objCall.assemblyName);
            Type classType = objAssembly.GetType(objCall.className);
            this.il.Emit(Emit.OpCodes.Call, classType.GetMethod(objCall.methodName, new System.Type[] { }));
            this.Store("A", typeof(System.Int32));


            //  string methodName = "WriteLine";
            // System.Reflection.Assembly objAssembly = System.Reflection.Assembly.LoadFrom(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\mscorlib.dll");
            //Type type1 = objAssembly.GetType("System.Console");


            //MethodInfo[] mi = type1.GetMethods();

            //// Display methods supported by MyClass. 
            //foreach (MethodInfo m in mi)
            //{
            //    // Display return type and name. 
            //    if (m.Name == "WriteLine")
            //    {
            //        string pp = "ssds";
            //    }

            //    // Console.Write("   " + m.ReturnType.Name +
            //    //                " " + m.Name + "(");

            //    // Display parameters. 
            //    ParameterInfo[] pi = m.GetParameters();

            //    }

            //foreach (Type type in objAssembly.GetTypes())
            //{
            //    if (type.FullName.IndexOf("Console", 0) > -1)
            //    //if (type.IsClass == true && type.)
            //    {
            //        //  test += type.FullName + Environment.NewLine;

            //    }
            //}

            //string op = symbolTable[((Print)stmt).Expr.ToString()].ToString();

        }
    }
}