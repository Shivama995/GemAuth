using Application.User.DTOs;
using Application.User.Services;
using MediatR;

namespace Application.User.CommandHandlers
{
    public class AddUserCommand : IRequest<AddUserDTO>
    {
        public string FirstName    { get; set; }
        public string LastName     { get; set; }
        public string EmailAddress { get; set; }
        public string Password     { get; set; }
        public string Role         { get; set; }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AddUserDTO>
    {
        private readonly IUserService _UserService;

        public AddUserCommandHandler(IUserService userService)
        {
            _UserService = userService;
        }
        public async Task<AddUserDTO> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            return await _UserService.CreateNewUser(request);
        }
    }
}
