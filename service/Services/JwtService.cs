﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
 using infrastructure.Repositories;
 using infrastructure.DataModels;
 using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace service.Services;

public class JwtService
{
    private TokenRepository _tokenRepository;
    
    private static readonly byte[] Secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("jwtkey")!);

    public JwtService(TokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }

    public string createToken(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null.");
        }

        // Print user details to console
        Console.WriteLine($"User Details: UserId={user.UserId}, Email={user.Email}");

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            }),
            Expires = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now.AddDays(7)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Secret), SecurityAlgorithms.HmacSha256Signature)
        };

        try
        {
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception e)
        {
            // Log the exception details for debugging purposes
            Console.WriteLine($"Exception while creating token: {e}");

            // Rethrow the original exception without attempting to access InnerException
            throw new Exception("Failed to create a token", e);
        }

    }


    public User validateTokenAndReturnUser(string token)
    {
        var principal = validateAndReturnToken(token); //Validating the token has not been tampered
    
        var nameClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name); // Saves username of the user

        User userFromToken = _tokenRepository.userFromEmail(nameClaim.Value);

        return userFromToken;
    }
    

    private ClaimsPrincipal validateAndReturnToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Secret),
                ValidateIssuer = false, // No Issuer, not needed for this scale of program
                ValidateAudience = false, // No targeted audience
                ClockSkew = TimeSpan.Zero // Amount of time the token can be over date
            };

            SecurityToken validatedToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            return principal;
        }
        catch (Exception ex)
        {
            // Token validation failed
            throw new Exception("Token validation failed", ex);
        }
    }
    
}