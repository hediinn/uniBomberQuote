using System.IO;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO.Compression;
using System.Globalization;

enum ARGS{
    debug,
    help,
    lineNum
}
class TestClass
{
    static void Main(string[] args)
    {
        List<List<String>> strings = new List<List<string>>();
        using(StreamReader readText = new StreamReader("Industrial_Society.txt",System.Text.Encoding.UTF8)) {
            string ln;
            bool tes = false;
            int newLines = 0;
            int start = 0;
            int end = 0;
            bool changed = false;
            while((ln = readText.ReadLine())!=null) {
                ln = ln.Replace('\u201c','"').Replace('\u201d','"').Replace('\u2019','\'');
                if(ln.Contains("1.")) {
                    tes = true;
                }

                if(ln.CompareTo("\n")>0 && tes) {
                    newLines++;
                }
                if(Regex.IsMatch(ln,@"(\d\.|\d\d,) \w")){
                    List<string> s = new List<string>();
                    s.Add(ln);
                    strings.Add(s);
                    start = newLines;
                    end ++;
                    changed = true;
                }else{
                    changed = false;

                }
                if(start > 0 && changed == false) {
                    strings[end-1].Add(ln);
                }
            }
            readText.Close();
        }
        if(args.Length > 1){
            int argcount = 0;
            ARGS? eArgs = null;
            foreach (var arg in args)
            {
                argcount ++;
                bool broken = false;
                switch (arg)
                {
                case "-h":
                    Console.WriteLine("help");
                    broken = true;  
                    eArgs = ARGS.help;
                    break; 
                case "-d":
                    Console.WriteLine("debug");
                    broken = true;  

                    eArgs = ARGS.debug;
                    break;
                case "-n":
                    Console.WriteLine("line number");
                    broken = true;
                    eArgs = ARGS.lineNum;
                    break; 
                default:
                    break;
            }
                if(broken){
                    break;
                }
            }

            if(eArgs.Equals(ARGS.debug)){
                int argsint1 = Int32.Parse(args[argcount]);
                int argsint2 = Int32.Parse(args[argcount+1]);
                int count = 0;
                List<string> string1 = strings[argsint1];
                foreach (var item in string1)
                {
                    System.Console.WriteLine(item);  
                    for (int i = 0; i < item.Length; i++)
                    {
                        Console.WriteLine($"s[{i}] = '{item[i]}' ('\\u{(int)item[i]:x4}')");
                    
                    }
                    if(count > argsint2){
                        break;
                    }
                    count ++;
                } 
                }else if(eArgs.Equals(ARGS.lineNum)){
                    int argsint1 = Int32.Parse(args[argcount]);
                    List<string> string1 = strings[argsint1];
                    foreach (var item in string1)
                    {
                        System.Console.WriteLine(item);  
                    }
                } 
            }else{

        Random random = new Random();
        List<string> string1 = strings[random.Next(0,strings.Count-1)];
        int conts = 0;
        bool breaks = false;
        foreach (var item in string1)
        {
            if((Regex.IsMatch(item,@"(\.)|(\. )") ||item.Contains('?')) && conts >4){
                breaks = true;
            }

            if(breaks){
                Console.WriteLine(Regex.Replace(item,@"(\. )(.*)|(\?)(.*)","."));
                break;
            }else{

            System.Console.WriteLine(item);
            }
            conts++;
        }
        }
    }

}