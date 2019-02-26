using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assembly asm = Assembly.GetExecutingAssembly();//如果是当前程序集
            //如果是其他文件
            string filename = @"D:\Applications\dll\SunDigital.Lib.dll";
            Assembly asm = Assembly.LoadFile(filename);
            //AssemblyDescriptionAttribute asmdis = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyDescriptionAttribute));
           // AssemblyCopyrightAttribute asmcpr = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCopyrightAttribute));
            AssemblyCompanyAttribute asmcpn = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCompanyAttribute));
            string s = string.Format("{0}  ", asmcpn.Company);
            Console.WriteLine(s);

            Console.ReadKey();

        }
    }
}
