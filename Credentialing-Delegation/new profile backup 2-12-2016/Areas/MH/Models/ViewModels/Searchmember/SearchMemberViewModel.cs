using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.SearchMember
{
    public class SearchMemberViewModel
    {
        //public SearchMemberViewModel()
        //{
        //    searchFactors = new List<SearchFactorViewModel>();
        //}

        public SearchMemberFactorsViewModel SearchFactors { get; set; }

        //public List<SearchFactorViewModel> searchFactors { get; set; }

        public List<SearchMemberResultViewModel> Members { get; set; }
    }
}