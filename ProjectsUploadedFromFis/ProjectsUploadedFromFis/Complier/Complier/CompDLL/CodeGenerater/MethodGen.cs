using System;
using System.Text;
using Collections = System.Collections.Generic;
using Reflect = System.Reflection;
using Emit = System.Reflection.Emit;
using IO = System.IO;
using System.Reflection;
namespace CodeGenerater
{
    class MethodGen
    {
        readonly  Emit.TypeBuilder typeBuilder;
        readonly string dllProbeDirectory;
        readonly Collections.Dictionary<string, Emit.FieldBuilder> typefieldList;
        public  TypeGen parent = null;
        CodeGen child = null; 
            
        public MethodGen(Emit.TypeBuilder typeBuilder,Collections.Dictionary<string, Emit.FieldBuilder> typefieldList, string dllProbeDirectory, TypeGen parent)
        {
            this.typeBuilder = typeBuilder;
            this.dllProbeDirectory = dllProbeDirectory;
            this.typefieldList = typefieldList;
            this.parent = parent;
         }

        public  Emit.MethodBuilder GenerateCode(MethodDeclaration methodDec)
        {
            MethodAttributes methodAttributes = MethodAttributes.Public;

            if(methodDec.IsStatic)
            methodAttributes = methodAttributes | MethodAttributes.Static;

        //Type[] types = { typeof(string) };

            //Emit.MethodBuilder methb = typeBuilder.DefineMethod(methodDec.MethodName, methodAttributes, typeof(void), types);
            Emit.MethodBuilder methb = typeBuilder.DefineMethod(methodDec.MethodName, methodAttributes, typeof(void), System.Type.EmptyTypes);
            
            child = new CodeGen(methb, methodDec, typefieldList, dllProbeDirectory,this);
            child.GenerateCode();
            return methb;
        }

        public  MethodBase GetMethodInfo(MethodCall objCall)
        {
           return  parent.GetMethodInfo(objCall);
        }

        public Type GetVariableType(string varibaleName)
        {
            return child.GetVariableType(varibaleName);
        }

    }
}
