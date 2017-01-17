using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository.MasterDataRepo
{
    public interface IMilitaryRankRepository : IGenericRepository<MilitaryRank>
    {
        void AddMilitaryRank(MilitaryRank militaryRank);
        void UpdateMilitaryRank(MilitaryRank militaryRank);
    }
}
