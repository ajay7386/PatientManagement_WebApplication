// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetPatientDetails() {
    var searchData = $('#txtSearch').val();
    var department = $('#ddlDepartment').val();
    var sortBy = $('#ddlSortBy').val();
    if (searchData == null || searchData == "") {
        searchData = "";
    }
    window.location = "/Patient?sortBy=" + sortBy + "&query=" + searchData + "&departmentId=" + department+"&startAdmissionDate=''&endAdmissionDate=''&page=1&pageSize=10";
}
