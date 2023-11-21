using Negr.ClassApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negr.Requests
{
    public class Requests
    {
        public static Users GetUser(string username, string password)
        {
            return DBConection.dbContext.Users
                   .FirstOrDefault(user => user.Username == username && user.Password == password);
        }

        public static Users GetUserByUsername(string username)
        {
            return DBConection.dbContext.Users.FirstOrDefault(user => user.Username == username);
        }

        public static void AddUser(Users newUser)
        {
            DBConection.dbContext.Users.Add(newUser);
            DBConection.dbContext.SaveChanges();
        }

        public static void SaveRecipeToDatabase(Recipes recipe)
        {
            DBConection.dbContext.Recipes.Add(recipe);
            DBConection.dbContext.SaveChanges();
        }
    }
}