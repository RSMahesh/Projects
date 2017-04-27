using System;
using System.Text;
using System.Collections.Generic;
using Reflect = System.Reflection;
using Emit = System.Reflection.Emit;
using IO = System.IO;
using System.Reflection;
namespace CodeGenerater
{
    class TypeGen
    {
        MethodGen child;
        readonly Emit.ModuleBuilder modb;
        List<Emit.MethodBuilder> methodList = null;
        Dictionary<string, Emit.FieldBuilder> fieldList;
        Emit.TypeBuilder typeBuilder = null;
        TypeDeclaration typeDec = null;
        public AssemblyGen parent;


        public TypeGen(TypeDeclaration typeDec, Emit.ModuleBuilder modb, string dllProbeDirectory, AssemblyGen parent)
        {
            methodList = new List<System.Reflection.Emit.MethodBuilder>();
            typeBuilder = modb.DefineType(typeDec.TypeName, TypeAttributes.Public);
            this.parent = parent;
            this.typeDec = typeDec;
        }

        public void GenerateCode()
        {
            DefineClassVaribale(typeBuilder, typeDec);
            child = new MethodGen(typeBuilder, fieldList, "", this);
            GenerateMethod(typeDec.methodDec);
            typeBuilder.CreateType();
        }

        public Type GetVariableType(string varibaleName)
        {
            Type type = child.GetVariableType(varibaleName);
            if (type == null)
            {
                type = this.fieldList.ContainsKey(varibaleName) ? this.fieldList[varibaleName].FieldType : child.GetVariableType(varibaleName);
            }
            return type;
        }

        private void DeclareConstructor()
        {
            //ConstructorBuilder pointCtor = pointTypeBld.DefineConstructor(
            //                        MethodAttributes.Public,
            //                        CallingConventions.Standard,
            //                        ctorParams);
        }

        private void DefineClassVaribale(Emit.TypeBuilder typeBuilder, TypeDeclaration _type)
        {
            fieldList = new Dictionary<string, Emit.FieldBuilder>();
            Emit.FieldBuilder classVariable;
            foreach (KeyValuePair<string, Type> para in _type.instanceVariable)
            {
                classVariable = typeBuilder.DefineField(para.Key,
                        para.Value,
                        FieldAttributes.Private);
                fieldList.Add(para.Key, classVariable);

            }
            foreach (KeyValuePair<string, Type> para in _type.classVariable)
            {
                classVariable = typeBuilder.DefineField(para.Key,
                        para.Value,
                        FieldAttributes.Private | FieldAttributes.Static);

                fieldList.Add(para.Key, classVariable);

            }

            //mthdIL.Emit(OpCodes.Ldarg_0);
            //mthdIL.Emit(OpCodes.Ldarg_0);
            //mthdIL.Emit(OpCodes.Ldfld, balanceAmtBldr);
            //mthdIL.Emit(OpCodes.Ldarg_2);
            //mthdIL.Emit(OpCodes.Add);


        }

        private void GenerateMethod(MethodDeclaration methodDec)
        {
            MethodDeclaration temp = null;
            bool flag = true;
            while (flag)
            {
                if (methodDec is MethodSequence)
                {
                    temp = (methodDec as MethodSequence).First;
                    methodDec = (methodDec as MethodSequence).Second;
                }
                else
                {
                    temp = methodDec;
                    flag = false;
                }

                methodList.Add(child.GenerateCode(temp));
            }
        }

        public Emit.MethodBuilder MethBuilder
        {
            //   foreach(

            get { return methodList[0]; }
            //   foreach(Emit. MethodBase


        }

        public Emit.MethodBuilder GetEntryPoint()
        {
            foreach (Emit.MethodBuilder methodB in methodList)
            {
                if (methodB.Name.Equals("Main", StringComparison.OrdinalIgnoreCase))
                    return methodB;
            }

            return null;

        }
        public MethodBase GetMethodInfo(MethodCall objCall)
        {
            MethodBase methodBase = null;
            foreach (Emit.MethodBuilder methodBuilder in methodList)
            {
                if (methodBuilder.Name == objCall.methodName)
                {
                  if(  CompareMethodSignature(objCall))
                    methodBase = (MethodBase)methodBuilder;
                }
            }

            if (methodBase == null)
            {
                return parent.GetMethodInfo(objCall);
            }
            else
            {
                return methodBase;
            }
        }

        private bool CompareMethodSignature(MethodCall objCall)
        {
            MethodDeclaration temp = null;
            MethodDeclaration methodDec = typeDec.methodDec;
            bool flag = true;
            while (flag)
            {
                if (methodDec is MethodSequence)
                {
                    temp = (methodDec as MethodSequence).First;
                    methodDec = (methodDec as MethodSequence).Second;
                }
                else
                {
                    temp = methodDec;
                    flag = false;
                }

                if (temp.MethodName == objCall.methodName &&
                    temp.parameters.Count == objCall.parameters.Count)
                {
                  Dictionary<string,Type>.Enumerator enumer =    temp.parameters.GetEnumerator();
                  int indx = 0;
                  while (enumer.MoveNext())
                  {
                          if (enumer.Current.Value != child.GetVariableType (objCall.parameters[indx++].ToString()))
                          return false;
                  }

                }

              
            }

            return true;
        }

        public Type GetMethodReturnType(MethodCall objCall)
        {
            MethodDeclaration temp = null;
            MethodDeclaration methodDec = typeDec.methodDec;
            Type returnType = null;
            bool flag = true;
            while (flag)
            {
                if (methodDec is MethodSequence)
                {
                    temp = (methodDec as MethodSequence).First;
                    methodDec = (methodDec as MethodSequence).Second;
                }
                else
                {
                    temp = methodDec;
                    flag = false;
                }

                if (temp.MethodName == objCall.methodName &&
                    temp.parameters.Count == objCall.parameters.Count)
                {
                    Dictionary<string, Type>.Enumerator enumer = temp.parameters.GetEnumerator();
                    int indx = 0;
                    while (enumer.MoveNext())
                    {
                        if (enumer.Current.Value != child.GetVariableType(objCall.parameters[indx++].ToString()))
                            return null;
                    }
                    returnType = temp.returnType;
                }

            }

            return returnType;
        }










    }
}
