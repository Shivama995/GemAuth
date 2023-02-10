﻿using Application.Authentication.CommandHandlers;
using Application.Authentication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) { }

        [HttpPost]
        public async Task<VerifyCredentialsDTO> VerifyCredentials(VerifyCredentialsCommand request) =>
            await CommandAsync(request);
            
    }
}