using Microsoft.AspNetCore.Http;
using System.Security.Authentication;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace OcelotGateWay
{
    internal class CachedResponseAuthentication
    {
        private bool ValidateRequestHeader(string authHeader)
        {
            //var exception = controllerName.GetType().GetCustomAttributes(typeof(SkipCheckTokenAttribute), true).Any();

            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                //if (exception)
                //    return false;
                throw new AuthenticationException("دسترسی شما مجاز نمی باشد" + "Token :" + authHeader);
            }
            return true;
        }

        public bool Authenticatie()
        {
            bool result = false;

            var httpContextAccessor = ServiceProviderHandler.GetService<IHttpContextAccessor>();
            if (httpContextAccessor.HttpContext == null) return result;

            string authHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (authHeader == null)
                authHeader = httpContextAccessor.HttpContext.Request.Query["Authorization"];

            if (!ValidateRequestHeader(authHeader))
                return result;

            var IMEI = httpContextAccessor.HttpContext.Request.Headers["IMEI"];
            if (string.IsNullOrEmpty(IMEI))
                IMEI = httpContextAccessor.HttpContext.Request.Query["IMEI"];

            result = CheckAPIToken(authHeader, IMEI);

            return result;
        }

        private bool CheckAPIToken(string token, string imei)
        {
            bool authenticated = false;

            var configuration = ServiceProviderHandler.GetService<IConfiguration>();
            var inputDTO = new CheckAPITokenInputDTO()
            {
                IMEI = imei,
                Token = token
            };

            //Request Token from API
            string url = configuration.GetSection("MicroserviceUrls:AuthBaseUrl").Value + "/api/v1/AuthAPI/CheckAPIToken";

            CheckAPITokenOutputDTO outputDTO = null;

            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(inputDTO);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = client.PostAsync(url, content).Result;

                result.EnsureSuccessStatusCode();

                string responseBody = result.Content.ReadAsStringAsync().Result;

                outputDTO = JsonConvert.DeserializeObject<CheckAPITokenOutputDTO>(responseBody);
            }

            if (outputDTO == null || !outputDTO.TokenIsValid)
                throw new SecurityTokenDecryptionFailedException("اطلاعات امنیتی فرستاده شده صحیح نمی باشد");
            else if (outputDTO != null && outputDTO.TokenIsValid)
                authenticated = true;

            return authenticated;
        }
    }
}
