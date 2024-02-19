namespace PatientManagement.Common;
public class PatientDetails
{
    public int PatientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime AdminsionDate { get; set; }
    public string Problem { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int DepartMentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
}

