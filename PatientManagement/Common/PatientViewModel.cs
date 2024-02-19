using Microsoft.AspNetCore.Mvc.Rendering;

namespace PatientManagement.Common;

public class PatientViewModel
{
    public List<PatientDetails> PatientDetails { get; set; }
    public List<SelectListItem> DepartmentList { get; set; }
    public List<SelectListItem> SortByList { get; set; }
    public int Department { get; set; }
    public string SortBy { get; set; }
    public string Search { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPazeSize { get; set; }
}

