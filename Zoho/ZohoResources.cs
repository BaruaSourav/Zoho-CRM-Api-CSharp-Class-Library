using Newtonsoft.Json;
using RestSharp;
using System.Web;
using Newtonsoft.Json.Linq;

namespace ZohoApiRepository
{
    public class ZohoResources
    {
        private Zoho _zoho;
        private string authtoken;
        public ZohoResources()
        {
            _zoho = new Zoho();
            _zoho.GetNewToken(); //save in db
            authtoken = "Zoho-oauthtoken "+_zoho.GetSavedToken();
        }
          
        public string SearchLeadInZoho(string companyName,int fromIndex=0,int toIndex=200)
        {
            int page;
            page = toIndex/200; //mapping the fromIndex & toIndex to the page parameter
            var client = new RestClient("https://www.zohoapis.com/crm/v2/Leads/search");
            RestRequest request = new RestRequest() { Method = Method.GET };
            request.AddParameter("criteria", "(Company:starts_with:*"+companyName +"*)");
            request.AddParameter("page", page);
            //request.AddParameter("client_id", clientID);
            request.AddHeader("Authorization", authtoken);
            var response = client.Execute(request);
            //var content = (JObject)JsonConvert.SerializeObject(response.Content); //serializing into json
            return response.Content;
        }
        


        public string SearchContactInZoho(string searchText, int fromIndex = 0, int toIndex = 200)
        {
            int page;
            page = (int)toIndex / 200;
            var client = new RestClient("https://www.zohoapis.com/crm/v2/Contacts/search");
            RestRequest request = new RestRequest() { Method = Method.GET };
            request.AddParameter("criteria", "(Account_Name:equals:" + searchText+ ")");
            request.AddParameter("page", page);
            //request.AddParameter("client_id", clientID);
            request.AddHeader("Authorization", authtoken);
            var response = client.Execute(request);
            //var content = (JObject)JsonConvert.DeserializeObject(response.Content);
            return response.Content;

        }
        public string SearchAccountInZoho(string searchText, int fromIndex = 0, int toIndex = 200)
        {
            int page;
            page = (int)toIndex / 200;
            var client = new RestClient("https://www.zohoapis.com/crm/v2/Accounts/search");
            RestRequest request = new RestRequest() { Method = Method.GET };
            request.AddParameter("criteria", "(Account_Name:starts_with:*" + searchText+ ")");
            //request.AddParameter("page", page);

            //request.AddParameter("client_id", clientID);
            request.AddHeader("Authorization", authtoken);
            var response = client.Execute(request);
            //var content = (JObject)JsonConvert.DeserializeObject(response.Content);
            return response.Content;

        }


    }
}
