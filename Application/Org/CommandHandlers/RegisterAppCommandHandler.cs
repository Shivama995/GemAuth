using Application.Org.DTOs;
using Application.Org.Services;
using MediatR;

namespace Application.Org.CommandHandlers
{
    public class RegisterAppCommand : IRequest<RegisterAppDTO>
    {
        public string FirstName    { get; set; }
        public string OrgName      { get; set; }
        public string LastName     { get; set; }
        public string EmailAddress { get; set; }
        public string Password     { get; set; }
    }
    public class RegisterAppCommandHandler : IRequestHandler<RegisterAppCommand, RegisterAppDTO>
    {
        private readonly IOrgService _OrgService;

        public RegisterAppCommandHandler(IOrgService orgService)
        {
            _OrgService = orgService;
        }
        public async Task<RegisterAppDTO> Handle(RegisterAppCommand request, CancellationToken cancellationToken) =>
            await _OrgService.Register(request);
    }
}
