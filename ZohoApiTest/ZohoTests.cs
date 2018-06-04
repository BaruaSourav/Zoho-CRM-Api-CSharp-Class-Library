using System;
using ZohoApiRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace ZohoApiTest
{
    [TestClass]
    public class ZohoTests
    {
        [TestMethod]
        public void getAccessTokenTest()
        {
            var zohoInstance = new ZohoApiRepository.Zoho();
            var response = zohoInstance.GetAccessToken();
            Debug.WriteLine(response);
            
        }
        [TestMethod]
        public void getNewAccessTokenTest()
        {
            var zohoInstance = new ZohoApiRepository.Zoho();
            var response = zohoInstance.GetNewToken();
            Debug.WriteLine(response);

        }
        [TestMethod]
        public void searchLeadsTest()
        {
            var resourcesInstance = new ZohoResources();
            var result =resourcesInstance.SearchLeadInZoho("test");
            Debug.WriteLine(result);
        }
        [TestMethod]
        public void searchContactsTest()
        {
            var resourcesInstance = new ZohoResources();
            var result = resourcesInstance.SearchContactInZoho("Luthan Electric Meter Testing");
            Debug.WriteLine(result);
        }
        [TestMethod]
        public void searchAccountsTest()
        {
            var resourcesInstance = new ZohoResources();
            var result = resourcesInstance.SearchAccountInZoho("test");
            Debug.WriteLine(result);
        }

    }
}
