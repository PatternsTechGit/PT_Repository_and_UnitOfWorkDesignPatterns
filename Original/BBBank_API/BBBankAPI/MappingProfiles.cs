using AutoMapper;
using Entities;
using Entities.RequestDTO;

namespace BBBankAPI
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<AccountRequestDTO, Account>();

        }
    }
}
