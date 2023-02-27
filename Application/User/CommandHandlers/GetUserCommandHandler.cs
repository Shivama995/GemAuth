using Application.User.DTOs;
using Application.User.Services;
using Common.Constants;
using MediatR;

namespace Application.User.CommandHandlers
{
    public class GetUserCommand : IRequest<GetUserDTO>
    {
        public string Id { get; set; }
    }
    public class GetUserCommandHandler : IRequestHandler<GetUserCommand, GetUserDTO>
    {
        private readonly IUserService _UserService;

        public GetUserCommandHandler(IUserService userService)
        {
            _UserService = userService;
        }
        public async Task<GetUserDTO> Handle(GetUserCommand request, CancellationToken cancellationToken) =>
            await _UserService.GetUserData(UserIdentifiers.Id, request.Id);
    }
}
