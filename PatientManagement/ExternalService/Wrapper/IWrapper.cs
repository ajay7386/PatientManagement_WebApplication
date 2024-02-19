using PatientManagement.Common;

namespace PatientManagement.ExternalService.Wrapper;
public interface IWrapper
{
    public Task<(bool, string, string)> GetToken(string username, string password);
    Task<(bool, string, List<PatientDetails>)> GetPatientDetils(string query, int departmentId, string startAdmissionDate, string endAdmissionDate, string sortBy, int page, int pageSize, int pageCount);
}

