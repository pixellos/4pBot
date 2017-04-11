using System.ComponentModel.DataAnnotations;

//Excluded from everything coz 
namespace pBot.Model.Functions
{
    public class UserMessage
    {
        [Key]
        public int Key { get; set; }
        public string User { get; set; }
        public string Message { get; set; }
    }
}
