using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Excluded from everything coz 
namespace pBot.Model.Functions
{
    public class SaveModel
    {
        [Key]
        public int Key { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public class SaveContext : DbContext
    {
        public SaveContext() : base(@"Server=tcp:pixdata.database.windows.net,1433;Data Source=pixdata.database.windows.net;Initial Catalog=Botdatabse;Persist Security Info=False;User ID=myadmin12;Password=Pix123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
        {
            
        }
        public DbSet<SaveModel> Table { get; set; } // not virtual to disable lazy loading
    }

    public class SaveManager
    {
        public SaveContext SaveContext = new SaveContext();

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
                SaveContext.Table.Remove(
                    SaveContext.Table.SingleOrDefault(x => x.Title.Equals(title))
                    );
            }
            SaveContext.Table.Add(new SaveModel() {Message = message, Title = title});

            SaveContext.SaveChanges();

        }
    }
}
