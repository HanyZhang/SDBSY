using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InsertData
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (SqlConnection conn = new SqlConnection(@"server=.\SQLEXPRESS;user id=sa;password=sundigital123;database=SDBSY"))
            using (SqlConnection conn = new SqlConnection(@"server=.\SQLEXPRESS;user id=sdbsy;password=sdbsy@SQL2012;database=SDBSY"))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                /*foreach (string line in File.ReadLines(@"d:\country.txt"))
                {
                    
                        cmd.CommandText = "insert into T_Countries (Name,CreateDateTime,IsDeleted) values('"+line.Trim()+"',GetDate(),0)";
                        cmd.ExecuteNonQuery();
                    
                }*/
                /*foreach (string line in File.ReadLines(@"d:\minzu.txt"))
                {

                    cmd.CommandText = "insert into T_Nations (Name,CreateDateTime,IsDeleted) values('" + line.Trim() + "',GetDate(),0)";
                    cmd.ExecuteNonQuery();

                }*/
                foreach (string line in File.ReadLines(@"d:\diqu.txt"))
                {
                    string[] strs = line.Split('|');
                    string code = strs[0].Trim();//区域代码
                    string parentId = "0";
                    if (code.EndsWith("0000"))
                    {//省或者直辖市

                    }
                    else if(code.EndsWith("000"))
                    {//省直管
                        parentId = code.Substring(0, 2) + "0000";
                    }
                    else if (code.EndsWith("00"))
                    {//地级市
                        parentId = code.Substring(0,2)+"0000";
                    }
                    else
                    {
                        parentId = code.Substring(0, 4) + "00";
                    }

                    cmd.CommandText = "insert into T_NewPlaces (Id,Name,Code,CreateDateTime,IsDeleted,ParentId) values('"+ Convert.ToInt64(code) + "','"+ strs[1].Trim() + "','"+code+"',GetDate(),0,"+Convert.ToInt64(parentId)+")";
                    cmd.ExecuteNonQuery();

                }
                /*foreach (string line in File.ReadLines(@"d:\data.txt"))
                {
                    cmd.CommandText = "insert into T_DataDictionaries (Name,Value,CreateDateTime,IsDeleted) values ('HealthyType','" + line.Trim() + "',GetDate(),0)";
                    cmd.ExecuteNonQuery();

                }*/
            }
            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
