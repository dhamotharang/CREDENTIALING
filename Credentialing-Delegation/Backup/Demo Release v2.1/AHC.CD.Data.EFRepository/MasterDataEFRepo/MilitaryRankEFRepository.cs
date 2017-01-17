using AHC.CD.Data.Repository.MasterDataRepo;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository.MasterDataEFRepo
{
    internal class MilitaryRankEFRepository : EFGenericRepository<MilitaryRank>, IMilitaryRankRepository
    {
        EFEntityContext context = new EFEntityContext();

        public void AddMilitaryRank(MilitaryRank militaryRank)
        {
            try
            {                
                var branchId = militaryRank.MilitaryBranches.First().MilitaryBranchID;
                var rankTitle = militaryRank.Title;
            
                //var branches = context.MilitaryBranches.ToList();
                var branch = context.MilitaryBranches.FirstOrDefault(x => x.MilitaryBranchID == branchId);

                var military = context.MilitaryRanks.FirstOrDefault(x => x.Title == rankTitle);

                if (military != null)
                {
                    military.MilitaryBranches = new List<MilitaryBranch>();
                    military.MilitaryBranches.Add(branch);                    
                    
                }
                else
                {
                    MilitaryRank rank = new MilitaryRank();
                    rank.Title = militaryRank.Title;
                    rank.StatusType = militaryRank.StatusType;
                    rank.LastModifiedDate = militaryRank.LastModifiedDate;
                    rank.MilitaryBranches = new List<MilitaryBranch>();
                    rank.MilitaryBranches.Add(branch);
                    context.MilitaryRanks.Add(rank);                  
                    
                }

              context.SaveChanges();                
             
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.Military_Rank_ADD_EXCEPTION, ex);
            }
        }

        public void UpdateMilitaryRank(MilitaryRank militaryRank)
        {
            try
            {
                var rank = context.MilitaryRanks.FirstOrDefault(x => x.MilitaryRankID == militaryRank.MilitaryRankID);               

                rank = AutoMapper.Mapper.Map<MilitaryRank, MilitaryRank>(militaryRank, rank);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ProfileEFRepositoryException(ExceptionMessage.Military_Rank_UPDATE_EXCEPTION, ex);
            }
        }
    }
}
