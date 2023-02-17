using Application.Org.DTOs;
using AutoMapper;
using Data.Org.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Org.Mappings
{
    public class RegisterAppDTOMapper : Profile
    {
        public RegisterAppDTOMapper()
        {
            CreateMap<OrgDetails, RegisterAppDTO>();
        }
    }
}
