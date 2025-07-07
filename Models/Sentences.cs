
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace uniBomberQuote.Models
{

    public enum SentenceType : int
    {
        real = 0,
        note = 1,
        headers = 2,
        test=-1
    }   
    public class Sentences
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key,Column(Order=0)]
        public long sentenceId{ get; set; }
        public required SentenceType SentenceType { set; get; }
        public required string MySentences{ set; get; }
    }
}