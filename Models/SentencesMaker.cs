
using System.Text.RegularExpressions;
using uniBomberQuote.Models;

namespace uniBomberQuote.Models
{

    public class SentencesMaker
    {


        public static Sentences generateSentence(String sentences)
        {

            SentenceType myType = SentenceType.test;

            if (CheckReal(sentences))
            {
                myType = SentenceType.real;
            }
            else if (checkStrng(sentences))
            {
                myType = SentenceType.headers;
            }
            else
            {
                myType = SentenceType.note;
            }
            return new Sentences
            {
                SentenceType = myType,
                MySentences = sentences
            };

        }

        public static void addAlldata(DataContext context1, string[] strings)
        {
            if (!context1.DataSentences.Any())
            {
                foreach (string sen in strings)
                {

                    context1.DataSentences.Add(generateSentence(sen));
                    context1.SaveChanges();
                }
            }
        }
        private static bool checkStrng(string s)
        {
            s = s.Replace(" ", "");
            s = s.Replace("‘", "");

    Regex myReg1 = new Regex(@"^([A-Z]+|[A-Z][a-z]+)");
            return myReg1.IsMatch(s) && s.Length < 70;
        
        }
        private static bool CheckReal(string s)
        {

            Regex myReg = new Regex(@"^\d+(\.|,) ");
            Regex myReg1 = new Regex(@"^\d+(\.|,) \(");
            return myReg.IsMatch(s) && !myReg1.IsMatch(s);
        }
        public static Sentences RandSent(List<Sentences> sen)
        {
            Random ran = new Random();
            Sentences se = sen.ToArray()[ran.Next(sen.Count - 1)];
            return se;
        }
    }
}