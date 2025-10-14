using AutoMapper;
using eDocCore.Application.DTOs;
using eDocCore.Application.Features.Documents.Commands.CreateDocument;
using eDocCore.Application.Features.Documents.Commands.UpdateDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eDocCore.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Document Mappings
            CreateMap<Document, DocumentDto>().ReverseMap();
            CreateMap<CreateDocumentCommand, Document>();
            CreateMap<UpdateDocumentCommand, Document>();
        }
    }

}
