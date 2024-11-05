using Application.Features.Auth.Constant;
using Application.Services.AuthServices;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, MessageResponse>
{
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IMapper mapper, IAuthService authService)
    {
        _mapper = mapper;
        _authService = authService;
    }

    public async Task<MessageResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var appUser = _mapper.Map<AppUser>(request);
        await _authService.RegisterAsync(appUser, request.Password);
        return new(AuthMessages.UserCreatedSuccessfully);
    }
}
