using AHC.CD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository
{
    public class UserInitializer 
    {
        public void Seed(List<User> users)
        {
            EFEntityContext context = new EFEntityContext();

            context.Users.AddRange(users);

            context.SaveChanges();

            for (int i = 3; i <= 7; i++)
            {
                users[i].UserRelation = new UserRelation()
                {
                    UserRoleRelations = new List<UserRoleRelation>() { new UserRoleRelation() { UserId = users[i + 5].UserID, RoleName = "PRO" } }
                };
            }

            context.SaveChanges();
        }
    }
}
