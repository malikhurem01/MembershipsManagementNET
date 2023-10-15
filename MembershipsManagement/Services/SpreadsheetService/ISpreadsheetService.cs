using MembershipsManagement.DTOs.BazaDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.SpreadSheets
{
    public interface ISpreadsheetService
    {
        Task<ServiceResponse<GetSpreadsheetDto>> CreateBaza(AddSpreadsheetDto baza);
        Task<ServiceResponse<string>> DeleteBaza(DeleteSpreadsheetDto baza);
        Task<ServiceResponse<List<GetSpreadsheetDto>>> GetBazaList(string dzematId);
        Task<ServiceResponse<GetSpreadsheetDto>> GetSingleBaza(int id);
        Task<ServiceResponse<GetSpreadsheetDto>> ArchiveBaza(int id);
        Task<ServiceResponse<GetSpreadsheetDto>> UnarchiveBaza(int bazaId);

    }
}
