using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utils
{
    public static class Txt
    {
        public static String readTxt(String path = "./txt.txt")
        {
            String line;
            String text="";
            try
            {
                StreamReader sr = new StreamReader(path);
                line = sr.ReadLine();

                while (line != null)
                {
                    text +=line;
                    Console.WriteLine(line);
                    line = sr.ReadLine();
                }
                sr.Close();
                return text;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return null;
            }
         
        }

        public static void writeTxtCnn(String str, String path = "./txt.txt")
        {
            try
            {
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine(str);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
           
        }

        public static void writeTxt(String str, String path = "./txtPass.txt")
        {
            try
            {
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine(str);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

        }

    }
}
