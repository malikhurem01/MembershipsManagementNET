using MembershipsManagement.DTOs.BazaDTOs;
using MembershipsManagement.DTOs.MemberInfoDTOs;
using MembershipsManagement.DTOs.SpreadsheetDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.SpreadSheets
{
    public interface ISpreadsheetService
    {
        Task<ServiceResponse<GetSpreadsheetDto>> CreateBaza(string token, AddSpreadsheetDto baza);
        Task<ServiceResponse<List<GetSpreadsheetDto>>> GetBazaList(int dzematId);
        Task<ServiceResponse<GetSpreadsheetDto>> GetSingleBaza(int id);
        Task<ServiceResponse<object>> GetActiveSpreadsheet(string token);
        Task<ServiceResponse<SpreadsheetReportDto>> GetSpreadsheetReport(string token, int spreadsheetYear);
        Task<ServiceResponse<List<DebtWarningReport>>> GetDebtWarningReports(string token);
        Task<ServiceResponse<GetSpreadsheetDto>> ArchiveBaza(string token, ArchiveSpreadsheetDto spreadsheet);
        Task<ServiceResponse<GetSpreadsheetDto>> UnarchiveBaza(int bazaId);
        Task<ServiceResponse<string>> DeleteBaza(string token, DeleteSpreadsheetDto baza);


    }
}
