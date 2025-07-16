using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;

namespace uniBomberQuote.Shared.Models {
    public static class SeedData
    {

        public static void SeedDatabase(DataContext context)
        {
            if (!context.DataSentences.Any())
            {
                using StreamReader reader = new StreamReader("Industrial_Society.txt");
                string[] strings = reader.ReadToEnd().Split("\n\n"); 
            
                SentencesMaker.AddAlldata(context, strings);
                context.SaveChanges();
            }
        }
        
    }
    public class SentencesMaker
    {
        private static MyText _GenerateSentence(String sentences)
        {

            SentenceType myType = SentenceType.test;

            if (_CheckReal(sentences))
            {
                myType = SentenceType.real;
            }
            else if (_CheckStrng(sentences))
            {
                myType = SentenceType.headers;
            }
            else
            {
                myType = SentenceType.note;
            }
            return new MyText
            {
                SentenceType = myType,
                MySentences = sentences
            };

        }

        public static void AddAlldata(DataContext context1, string[] strings)
        {
            if (!context1.DataSentences.Any())
            {
                foreach (string sen in strings)
                {

                    context1.DataSentences.Add(_GenerateSentence(sen));
                    context1.SaveChanges();
                }
            }
        }

        private static bool _CheckStrng(string s)
        {
            s = s.Replace(" ", "");
            s = s.Replace("‘", "");

            Regex myReg1 = new Regex(@"^([A-Z]+|[A-Z][a-z]+)");
            return myReg1.IsMatch(s) && s.Length < 70;

        }

        private static bool _CheckReal(string s)
        {

            Regex myReg = new Regex(@"^\d+(\.|,) ");
            Regex myReg1 = new Regex(@"^\d+(\.|,) \(");
            return myReg.IsMatch(s) && !myReg1.IsMatch(s);
        }

        public static MyText RandSent(List<MyText> sen)
        {
            Random ran = new Random();
            MyText se = sen.ToArray()[ran.Next(sen.Count - 1)];
            return se;
        }

        public static string RandomWord(List<MyText> sen)
        {
            StringBuilder r = new StringBuilder();
            foreach (MyText item in sen)
            {
                r.AppendLine(item.MySentences);
            }
            string[] ds = r.ToString()
            .Replace(",", " ")
            .Replace(".", " ")
            .Replace("\n", " ")
            .Replace("[", " ")
            .Replace("(", " ")
            .Replace("]", " ")
            .Replace(")", " ")
            .Replace(":", "")
            .Replace(";"," ")
            .Replace(":", "")
            .Replace("?", "")
            .Replace(";", " " )
            .Replace('\"', ' ')
            .Replace('“', ' ')
            .Replace('”', ' ')
            .Replace("-", "")
            .Replace("—", "")
            .Replace("  ", " ")
            .ToLower()
            .Split(" ");

            r.Clear();
            foreach (string item in ds.Distinct())
            {
                r.Append(", "+item);
            }
            ds = r.ToString().Split(", ");
            Random ran = new Random();
            string my = ds[ran.Next(ds.Length -1)];
            return my;
        }
    }
}