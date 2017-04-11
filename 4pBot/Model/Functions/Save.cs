using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Excluded from everything coz 
namespace pBot.Model.Functions
{
    public class StartupSomethingTodoChangeNameDao
    {
        public StartupSomethingTodoChangeNameDao(string database)
        {
        }

        public string Get(string title)
        {
            var value = SaveContext.Table.SingleOrDefault(x => x.Title.Equals(title));
            if (value ==  null || value.Message.Equals(""))
            {
                return null;
            }
            return value.Message;
        }

        public void Save(string title, string message)
        {
            if (SaveContext.Table.Any(x=>x.Title.Equals(title)))
            {
                SaveContext.Table.Remove(SaveContext.Table.SingleOrDefault(x => x.Title.Equals(title)));
            }
            SaveContext.Table.Add(new UserMessage() {Message = message, User = title});

            SaveContext.SaveChanges();

        }
    }
}
