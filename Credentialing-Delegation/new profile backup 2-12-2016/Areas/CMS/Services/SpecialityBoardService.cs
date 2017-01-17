using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.CMS.Models;
using PortalTemplate.Areas.CMS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CMS.Services
{
    public class SpecialityBoardService : ISpecialityBoardService
    {
        /// <summary>
        /// ServiceUtility object reference
        /// </summary>
        private ServiceUtility utility;

        /// <summary>
        /// SpecialityBoardService constructor For ServiceUtility
        /// </summary>
        public SpecialityBoardService()
        {
            utility = new ServiceUtility();
        }


        /// <summary>
        /// Return List Of SpecialityBoard
        /// </summary>
        /// <returns>List of SpecialityBoard</returns>
        public List<SpecialityBoardViewModel> GetAll()
        {
            List<SpecialityBoardViewModel> SpecialityBoardList = new List<SpecialityBoardViewModel>();
            Task<string> SpecialityBoard = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetAllSpecialityBoards?IncludedInactive=true");
                return msg;
            });
            try
            {
                if (SpecialityBoard.Result != null)
                {
                    SpecialityBoardList = JsonConvert.DeserializeObject<List<SpecialityBoardViewModel>>(SpecialityBoard.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return SpecialityBoardList;
        }

        /// <summary>
        /// Return Object By Unique Code
        /// </summary>
        /// <param name="SpecialityBoardCode">SpecialityBoard's Code Parameter</param>
        /// <returns>Object Type</returns>
        public SpecialityBoardViewModel GetByUniqueCode(string Code)
        {
            SpecialityBoardViewModel _object = new SpecialityBoardViewModel();
            Task<string> SpecialityBoard = Task.Run(async () =>
            {
                string msg = await utility.GetDataFromService("api/Common/GetSpecialityBoard?SpecialityBoardCode=" + Code + "");
                return msg;
            });
            try
            {
                if (SpecialityBoard.Result != null)
                {
                    _object = JsonConvert.DeserializeObject<SpecialityBoardViewModel>(SpecialityBoard.Result);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _object;
        }


        /// <summary>
        /// Create New SpecialityBoard and Return Updated SpecialityBoard
        /// </summary>
        /// <param name="SpecialityBoard">SpecialityBoard to Create</param>
        /// <returns>Updated SpecialityBoard</returns>
        public SpecialityBoardViewModel Create(SpecialityBoardViewModel SpecialityBoard)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonAdd/AddSpecialityBoard", SpecialityBoard);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SpecialityBoardViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Update SpecialityBoard and Return Updated SpecialityBoard
        /// </summary>
        /// <param name="SpecialityBoard">SpecialityBoard to Update</param>
        /// <returns>Updated SpecialityBoard</returns>
        public SpecialityBoardViewModel Update(SpecialityBoardViewModel SpecialityBoard)
        {
            Task<string> _object = Task.Run(async () =>
            {
                string msg = await utility.PostDataToService("api/CommonUpdate/UpdateSpecialityBoard", SpecialityBoard);
                return msg;
            });
            try
            {
                if (_object.Result != null) { return JsonConvert.DeserializeObject<SpecialityBoardViewModel>(_object.Result); }
                else { return null; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}