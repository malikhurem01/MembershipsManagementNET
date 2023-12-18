using AutoMapper;
using MembershipsManagement.DTOs.SpreadsheetDTOs;
using MembershipsManagement.Entities;

namespace MembershipsManagement.AutoMapperMaps
{
    public class SpreadsheetMap : Profile
    {
        public SpreadsheetMap()
        {
            CreateMap<Spreadsheet, GetSpreadsheetDto>();
            CreateMap<AddSpreadsheetDto, Spreadsheet>();
        }
    }
}
