using MembershipsManagement.DTOs.SpreadsheetDTOs;
using MembershipsManagement.DTOs.MemberInfoDTOs;
using MembershipsManagement.DTOs.SpreadsheetDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.SpreadSheets
{
    public interface ISpreadsheetService
    {
        Task<ServiceResponse<GetSpreadsheetDto>> CreateSpreadsheet(string token, AddSpreadsheetDto spreadsheet);
        Task<ServiceResponse<List<GetSpreadsheetDto>>> GetSpreadsheetList(string token, int dzematId);
        Task<ServiceResponse<GetSpreadsheetDto>> GetSingleSpreadsheet(string token, int spreadsheetId);
        Task<ServiceResponse<object>> GetActiveSpreadsheet(string token, int? dzematId);
        Task<ServiceResponse<SpreadsheetReportDto>> GetSpreadsheetReport(string token, int spreadsheetYear, int? dzematId);
        Task<ServiceResponse<List<DebtWarningReport>>> GetDebtWarningReports(string token);
        Task<ServiceResponse<GetSpreadsheetDto>> ArchiveSpreadsheet(string token, ArchiveSpreadsheetDto spreadsheet);
        Task<ServiceResponse<GetSpreadsheetDto>> UnarchiveSpreadsheet(int spreadsheetId);
        Task<ServiceResponse<string>> DeleteSpreadsheet(string token, DeleteSpreadsheetDto spreadsheet);


    }
}
