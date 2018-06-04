using RestSharp;
using System;
using System.Data.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Zoho;
using System.Data.Entity.Validation;
using Zoho.Utilities;


namespace ZohoApiRepository
{
    /// <summary>
    /// Zoho base class to verify the communication and get the access token from hard coded
    /// grant token and refresh that with the refresh token
    /// </summary>
    public class Zoho
    {
        private string clientID;
        private string clientSecret;
        private string redirectUri;
        //Self Client grant key
        private string grantKey = "{YOUR_GRANT_KEY}"
        ConfigRepo _config;
        public Zoho()
        {
            redirectUri = "http://secure.trackyourtruck.com/oauth2callback";
            clientSecret = "{YOUR_CLIENT_SECRET}";
            clientID = "{YOUR_CLIENT_ID}";
            _config = new ConfigRepo();   
        }

        public string GetSavedToken() {
            
            string at;
            at =_config.ReadSetting("zoho_access_token");
            //using (zohotestEntities1 context = new zohotestEntities1())
            //{
            //    var tokenRow = context.zohotokens.Find(1);
            //    at = tokenRow.accessToken;
            //}
            return at;
            
        }

        /// <summary>
        /// Getting the access token from the grant token once
        /// </summary>
        public string GetAccessToken()
        {
            var client = new RestClient("https://accounts.zoho.com/oauth/v2/token");
            RestRequest request = new RestRequest() { Method = Method.POST };
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("client_id", clientID);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("redirect_uri", redirectUri);
            request.AddParameter("code", grantKey);


            var response = client.Execute(request);
            var content= (JObject)JsonConvert.DeserializeObject(response.Content);

            var at = (string)content["access_token"];
            var rt = (string)content["refresh_token"];
            //Saving in configuration file

            _config.AddUpdateAppSettings("zoho_access_token","");
            // for testing purpose
            //using (zohotestEntities1 context = new zohotestEntities1())
            //{
            //    try
            //    {
            //        context.zohotokens.Add(
            //        new zohotoken
            //        {
            //            id = 1,
            //            accessToken = at,
            //            refreshToken = rt
            //        }
            //        );
            //        context.SaveChanges();
            //    }
            //    catch(DbEntityValidationException e)
            //    {
            //        string error= string.Empty;
            //        foreach (var eve in e.EntityValidationErrors)
            //        {
                        
            //            error= error+ eve.Entry.Entity.GetType().Name+ eve.Entry.State;
            //            foreach (var ve in eve.ValidationErrors)
            //            {
                            
            //             error+= (ve.PropertyName+ve.ErrorMessage);
            //            }
            //        }
            //        return error;
            //    }
            //}
            //refreshToken = content["refresh_token"].Value<string>();
            return ("at ="+ at+ "\n rt ="+ rt ); 
           
        }
        /// <summary>
        /// Get new access token in case of access token expiration submitting the refresh token 
        /// </summary>
        public string GetNewToken()
        {
            string refreshToken;
            var client = new RestClient("https://accounts.zoho.com/oauth/v2/token");RestRequest request = new RestRequest() { Method = Method.POST };
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("client_id", clientID);
            request.AddParameter("client_secret", clientSecret);

            //using (zohotestEntities1 context = new zohotestEntities1())
            //{
            //    var tokenRow = context.zohotokens.Find(1);
            //    refreshToken = tokenRow.refreshToken;
            //}
            refreshToken = _config.ReadSetting("zoho-refresh-token");

            request.AddParameter("refresh_token", refreshToken);
            var response = client.Execute(request);
            var content = (JObject)JsonConvert.DeserializeObject(response.Content);
            var accesstoken= (string)content["access_token"];

            //using (zohotestEntities1 context = new zohotestEntities1())
            //{
            //    var tokenRow = context.zohotokens.Find(1);
            //    tokenRow.accessToken = accesstoken;
            //    context.SaveChanges();
            //}
            _config.AddUpdateAppSettings("zoho-access-token", accesstoken);

            //refreshToken = (string)content["refresh_token"];
            return ("new access token : "+ content +" "+_config.ReadSetting("zoho-refresh-token"));
        }
    }
}
