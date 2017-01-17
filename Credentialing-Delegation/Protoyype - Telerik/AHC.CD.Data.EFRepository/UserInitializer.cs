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
        public void Seed(List<CDUser> users)
        {
            EFEntityContext context = new EFEntityContext();

            context.Users.AddRange(users);

            context.SaveChanges();

            for (int i = 3; i <= 7; i++)
            {
                users[i].UserRelation = new CDUserRelation()
                {
                    UserRoleRelations = new List<CDUserRoleRelation>() { new CDUserRoleRelation() { UserId = users[i + 5].CDUserID, RoleName = "PRO" } }
                };
            }

            context.SaveChanges();
        }
    }
}
