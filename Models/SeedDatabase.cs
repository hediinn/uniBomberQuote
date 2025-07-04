using Microsoft.EntityFrameworkCore;

namespace uniBomberQuote.Models {
    public static class SeedData
    {

        public static void SeedDatabase(DataContext context)
        { 
            context.Database.Migrate();
            if (context.Sentences.Count() == 0)
            {
                Sentences s1 = new()
                {
                    sentenceId = 0,
                    MySentences = "test",
                    SentenceType = SentenceType.test
                };
                Console.WriteLine($"{s1.MySentences}");
                context.Sentences.Add(s1);
                context.SaveChanges();

            }
        }
    }
}