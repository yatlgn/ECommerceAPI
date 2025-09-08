using ECommerceAPI.Application.Bases;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Interfaces.AutoMapper;
using ECommerceAPI.Application.Interfaces.Tokens;
using ECommerceAPI.Application.Interfaces.UnitOfWorks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandRequest>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}