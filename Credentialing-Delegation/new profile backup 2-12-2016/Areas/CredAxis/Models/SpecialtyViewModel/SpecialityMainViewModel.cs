using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.SpecialtyViewModel
{
    public class SpecialityMainViewModel
    {
        public SpecialityMainViewModel()
        {
            specialityDetail = new List<SpecialtyDetailViewModel>();
            //boardDetail = new List<BoardDetailViewModel>();

        }


        public List<SpecialtyDetailViewModel> specialityDetail { get; set; }
        //public List<BoardDetailViewModel> boardDetail { get; set; }

    }
}