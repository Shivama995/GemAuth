using Application.Org.CommandHandlers;
using Application.Org.DTOs;

namespace Application.Org.Services
{
    public interface IOrgService
    {
        public Task<RegisterAppDTO> Register(RegisterAppCommand request);
    }
}
