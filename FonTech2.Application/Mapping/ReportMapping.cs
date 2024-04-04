using AutoMapper;
using FonTech2.Domain.Dto.Report;
using FonTech2.Domain.Entity;

namespace FonTech2.Application.Mapping;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Report, ReportDto>()
            .ForCtorParam(ctorParamName: "Name",m => m.MapFrom(s=>s.Name))
            .ForCtorParam(ctorParamName: "Id",m=>m.MapFrom(s=>s.Id))
            .ForCtorParam(ctorParamName: "Description",m=>m.MapFrom(s=>s.Description))
            .ForCtorParam(ctorParamName: "DateCreated",m=>m.MapFrom(s=>s.CreatedAt))
            .ReverseMap();
    }
}