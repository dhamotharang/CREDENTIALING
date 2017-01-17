using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AHC.CD.Business;
using AHC.CD.Data.EFRepository;
using AHC.CD.Entities.ProviderInfo;

namespace AHC.DC.Business.Tests
{
    [TestClass]
    public class ProvidersManagerUnitTest
    {
        [TestMethod]
        public void SaveIndividualProviderTest()
        {
            IProvidersManager mgr = new ProvidersManager(new EFUnitOfWork());
            Provider p = new Individual();
            p.FullName = "Provider Full Name";
            p.ProviderStatus = ProviderStatus.Active;
            p.LastUpdatedDateTime = DateTime.Now;
            p.Relation = ProviderRelation.Affiliated;

            
            int i = mgr.SaveIndividualProvider(p);
            Assert.AreEqual(1, i);
        }
    }
}
