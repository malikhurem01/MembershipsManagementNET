using MembershipsManagement.DTOs.Baza;
using MembershipsManagement.Entities;

namespace MembershipsManagement.Services.SpreadSheets
{
    public interface IBazaService
    {
        Task<ServiceResponse<GetBazaDto>> CreateBaza(AddBazaDto baza);
        Task<ServiceResponse<string>> DeleteBaza(DeleteBazaDto baza);
        Task<ServiceResponse<List<GetBazaDto>>> GetBazaList(int dzematId);
        Task<ServiceResponse<GetBazaDto>> GetSingleBaza(int id);
        Task<ServiceResponse<GetBazaDto>> ArchiveBaza(int id);
        Task<ServiceResponse<GetBazaDto>> UnarchiveBaza(int bazaId);

    }
}
