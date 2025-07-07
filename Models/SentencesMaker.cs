
using System.Text.RegularExpressions;
using uniBomberQuote.Models;

namespace uniBomberQuote
{

    public class SentencesMaker
    {


        public static Sentences generateSentence(String sentences)
        {

            SentenceType myType = SentenceType.test;
            Regex myReg = new Regex(@"^\d+(\.|,) ");
            if (myReg.IsMatch(sentences))
            {
                myType = SentenceType.real;
            }
            else if (sentences.Length < 40)
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
            foreach (string sen in strings)
            {
                context1.Add(generateSentence(sen));
            }
            context1.SaveChanges();
        }
    }
}