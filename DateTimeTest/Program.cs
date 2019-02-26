using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var time = DateTime.Now;
            var birthTime=new DateTime(2014,9,28);

            if (birthTime.Month < 9)
            {
                Console.WriteLine("9月份之前出生的");
            }
            else
            {
                Console.WriteLine("9月份之后出生的");
            }


            var year = birthTime.Year;
            var month = birthTime.Month;
            Console.WriteLine("年："+year+"，月："+month);
            Console.ReadLine();
        }
    }
}
