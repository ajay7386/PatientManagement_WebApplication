using Microsoft.Extensions.Options;
using PatientManagement.Common;
using PatientManagement.Common.AppSettings;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PatientManagement.ExternalService.Wrapper
{
    public class Wrapper : IWrapper
    {
        private readonly HttpClient httpClient;
        private readonly AppSetting _appSetting;
        private readonly IHttpContextAccessor _accessor;
        public Wrapper(HttpClient httpClient, IOptions<AppSetting> appSetting, IHttpContextAccessor accessor)
        {
            this.httpClient = httpClient;
            _appSetting = appSetting.Value;
            _accessor = accessor;
        }
        public async Task<(bool, string, string)> GetToken(string username, string password)
        {
            Token token=new Token();
            bool isScussess = false;
            string msg = string.Empty;
            var url = $"{_appSetting.BaseUrl}security/Token?username={username}&password={password}";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                isScussess = true;
               string jsonToken = await response.Content.ReadAsStringAsync();
               token = JsonSerializer.Deserialize<Token>(jsonToken);
            }
            else
            {
                msg = "Please provide valide credentials!";
            }
            return (isScussess, msg, token.accesstoken);
        }
        public async Task<(bool, string, List<PatientDetails>)> GetPatientDetils(string query, int departmentId, string startAdmissionDate, string endAdmissionDate, string sortBy, int page, int pageSize, int pageCount)
        {
            bool isScussess = false;
            string msg = string.Empty;
            var token = _accessor.HttpContext.Session.GetString("Token");
            List<PatientDetails> patientDetails = new List<PatientDetails>();

            var url = $"{_appSetting.BaseUrl}Patient/SearchPatients?Query={query}&DepartmentId={departmentId}&SortBy={sortBy}&Page={page}&PageSize={pageSize}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                isScussess = true;
                var patientData = await response.Content.ReadAsStringAsync();
                patientDetails = JsonSerializer.Deserialize<List<PatientDetails>>(patientData);
            }
            else
            {
                msg = "Please provide valide credentials!";
            }
            return (isScussess, msg, patientDetails);
        }
    }
}
