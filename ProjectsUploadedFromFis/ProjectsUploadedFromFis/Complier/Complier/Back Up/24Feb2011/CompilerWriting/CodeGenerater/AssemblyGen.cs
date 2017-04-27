using System;
using System.Text;
using System.Collections.Generic;
using Reflect = System.Reflection;
using Emit = System.Reflection.Emit;
using IO = System.IO;
using System.Reflection;

namespace CodeGenerater
{
	class AssemblyGen
	{
        List<TypeGen> typeList;
        TypeGen child = null;
        string dllProbeDirectory;

        public AssemblyGen(TypeDeclaration  methodDec, string moduleName, string dllProbeDirectory)
        {

            if (IO.Path.GetFileName(moduleName) != moduleName)
            {
                throw new System.Exception("can only output into current directory!");
            }

            this.dllProbeDirectory = dllProbeDirectory;

            typeList = new List<TypeGen>();

            Reflect.AssemblyName name = new Reflect.AssemblyName(IO.Path.GetFileNameWithoutExtension(moduleName));
            Emit.AssemblyBuilder asmb = System.AppDomain.CurrentDomain.DefineDynamicAssembly(name, Emit.AssemblyBuilderAccess.Save);
            Emit.ModuleBuilder modb = asmb.DefineDynamicModule(moduleName);
            GenerateType(methodDec, modb,dllProbeDirectory);
            modb.CreateGlobalFunctions();
            asmb.SetEntryPoint(GetEntryPoint());
            asmb.Save(moduleName);
         }

        private Emit.MethodBuilder GetEntryPoint()
        {
            Emit.MethodBuilder methodB = null;
            foreach (TypeGen typeGen in typeList)
            {
                methodB = typeGen.GetEntryPoint();
                if (methodB == null)
                    continue;
                return methodB;
            }
            return methodB;
        }
	    
       private void GenerateType(TypeDeclaration  typeDec,Emit.ModuleBuilder modb,string  dllProbeDirectory)
        {
            TypeDeclaration temp = null;
            bool flag = true;
            while (flag)
            {
                if (typeDec is TypeSequence)
                {
                    temp = (typeDec as TypeSequence).First;
                    typeDec = (typeDec as TypeSequence).Second;
                }
                else
                {
                    temp = typeDec;
                    flag = false;
                }
                child = new TypeGen(temp, modb,dllProbeDirectory,this);
                child.GenerateCode();
                typeList.Add(child);
            }
        }



        public MethodBase GetMethodInfo(MethodCall objCall)
        {

            List<string> dllPath = null;

            System.Reflection.Assembly objAssembly = null;
            Type classType = null;
            MethodBase methodInfo = null;

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

                //Check whether its local object on which method is called
                classType = child.GetVariableType(objCall.className);

                if (classType == null)
                {
                    methodInfo = null;
                    objAssembly = System.Reflection.Assembly.LoadFrom(path);
                    classType = objAssembly.GetType(objCall.className);
                }
             
                if (classType == null)
                    continue;
              
             
                if (classType == null)
                    continue;

                if (objCall.parameters.Count < 1)
                {
                    if (objCall.IsConstrutor)
                        methodInfo = classType.GetConstructor(new System.Type[] { });
                    else
                        methodInfo = classType.GetMethod(objCall.methodName, new System.Type[] { });
                }
                else
                {
                    List<Type> types = new System.Collections.Generic.List<Type>();
                    foreach (object para in objCall.parameters)
                    {
                       Type type = child.GetVariableType(para.ToString());
                       if (type == null)
                       {
                           throw new Exception("Variable Not Declared:" + para);
                       }

                      types.Add(type);
        
                    }

                    methodInfo = classType.GetMethod(objCall.methodName, types.ToArray());
                }

                if (methodInfo != null)
                {
                    objCall.assemblyName = ((System.Reflection.MemberInfo)(methodInfo)).Module.FullyQualifiedName;
                    break;
                }

            }

            if (methodInfo == null)
                throw new Exception("Method Not Found:" + objCall.className + "." + objCall.methodName);

            return methodInfo;
        }


        private List<string> GetAssemblyPath(string fullyQualifiedTypeName)
        {
            char[] split = new char[] { '.' };
            string assemblyPath = "";
            string[] arr = fullyQualifiedTypeName.Split(split);
            List<string> dllPath = new System.Collections.Generic.List<string>(10);
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

            return (List<string>)dllPath;
        }
    }

  }
