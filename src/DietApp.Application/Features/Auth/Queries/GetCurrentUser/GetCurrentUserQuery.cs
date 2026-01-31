using DietApp.Application.Features.Auth.DTOs;
using MediatR;

namespace DietApp.Application.Features.Auth.Queries.GetCurrentUser;

public sealed record GetCurrentUserQuery : IRequest<UserDto>;
