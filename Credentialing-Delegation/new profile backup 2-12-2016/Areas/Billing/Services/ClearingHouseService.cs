using PortalTemplate.Areas.Billing.Models.Clearing_House;
using PortalTemplate.Areas.Billing.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Services.Services
{
    public class ClearingHouseService : IClearingHouseService
    {
        readonly List<ClearingHouseViewModel> ClearingHouseList;
        public ClearingHouseService()
        {
            ClearingHouseList = new List<ClearingHouseViewModel>();
       
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "84736", ClearingHouseName = "Ability", PayersCount = "5" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "84736", ClearingHouseName = "Ability", PayersCount = "5" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "84736", ClearingHouseName = "Ability", PayersCount = "5" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "84736", ClearingHouseName = "Ability", PayersCount = "5" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "84736", ClearingHouseName = "Ability", PayersCount = "5" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "84736", ClearingHouseName = "Ability", PayersCount = "5" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "84736", ClearingHouseName = "Ability", PayersCount = "5" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "84736", ClearingHouseName = "Ability", PayersCount = "5" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "84736", ClearingHouseName = "Ability", PayersCount = "5" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "84736", ClearingHouseName = "Ability", PayersCount = "5" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "84736", ClearingHouseName = "Ability", PayersCount = "5" });
            ClearingHouseList.Add(new ClearingHouseViewModel { ClearingHouseId = "90389", ClearingHouseName = "Emdeon", PayersCount = "4" });
        }

        public List<ClearingHouseViewModel> GetClearingHouseList()
        {

            return ClearingHouseList;
        }


        public ClearingHouseViewModel AddClearingHouse()
        {
            ClearingHouseViewModel ClearingHouse = new ClearingHouseViewModel();
            List<PayerViewModel> payerList = new List<PayerViewModel>();
            payerList.Add(new PayerViewModel { PayerId = "1", PayerName = "Humana" });
            payerList.Add(new PayerViewModel { PayerId = "2", PayerName = "Welcare" });
            payerList.Add(new PayerViewModel { PayerId = "3", PayerName = "Freedom" });
            payerList.Add(new PayerViewModel { PayerId = "4", PayerName = "Coventry" });
            payerList.Add(new PayerViewModel { PayerId = "5", PayerName = "Ultimate" });
            ClearingHouse.ClearingHousePayers = payerList;
            return ClearingHouse;
        }

        public ClearingHouseViewModel ViewClearingHouse(string ClearingHouseId)
        {
            ClearingHouseViewModel ClearingHouse = new ClearingHouseViewModel();
            foreach (var item in ClearingHouseList)
            {
                if (item.ClearingHouseId == ClearingHouseId)
                {
                    ClearingHouse = item;
                    break;
                }
            }
            ClearingHouse.ClearingHouseAddressLine1 = "Arkansas";
            ClearingHouse.ClearingHouseAddressLine2 = "55512";
            ClearingHouse.ClearingHouseCity = "Spring Hills";
            ClearingHouse.ClearingHouseState = "Florida";
            ClearingHouse.ClearingHouseZip = "34609";

            List<PayerViewModel> payerList = new List<PayerViewModel>();
            payerList.Add(new PayerViewModel { PayerId = "1", PayerName = "Humana" });
            payerList.Add(new PayerViewModel { PayerId = "2", PayerName = "Welcare" });
            payerList.Add(new PayerViewModel { PayerId = "3", PayerName = "Freedom" });
            payerList.Add(new PayerViewModel { PayerId = "4", PayerName = "Coventry" });
            payerList.Add(new PayerViewModel { PayerId = "5", PayerName = "Ultimate" });
            ClearingHouse.ClearingHousePayers = payerList;

            return ClearingHouse;
        }

        public ClearingHouseViewModel EditClearingHouse(string ClearingHouseId)
        {
            ClearingHouseViewModel ClearingHouse = new ClearingHouseViewModel();

            foreach (var item in ClearingHouseList)
            {
                if (item.ClearingHouseId == ClearingHouseId)
                {
                    ClearingHouse = item;
                    break;
                }
            }
            ClearingHouse.ClearingHouseAddressLine1 = "Arkansas";
            ClearingHouse.ClearingHouseAddressLine2 = "55512";
            ClearingHouse.ClearingHouseCity = "Spring Hills";
            ClearingHouse.ClearingHouseState = "Florida";
            ClearingHouse.ClearingHouseZip = "34609";

            List<PayerViewModel> payerList = new List<PayerViewModel>();
            payerList.Add(new PayerViewModel { PayerId = "1", PayerName = "Humana" });
            payerList.Add(new PayerViewModel { PayerId = "2", PayerName = "Welcare" });
            payerList.Add(new PayerViewModel { PayerId = "3", PayerName = "Freedom" });
            payerList.Add(new PayerViewModel { PayerId = "4", PayerName = "Coventry" });
            payerList.Add(new PayerViewModel { PayerId = "5", PayerName = "Ultimate" });
            ClearingHouse.ClearingHousePayers = payerList;

            return ClearingHouse;
        }
    }
}