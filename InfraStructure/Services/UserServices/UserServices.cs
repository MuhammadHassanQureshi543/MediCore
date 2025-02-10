using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.ResponseModels;
using Application.Data_TransferModels.UserDTO;
using Application.Interface.User;
using Azure;
using CommonOperations.Constants;
using Domain.Models.Entities.UserEntities;
using InfraStructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InfraStructure.Services.UserServices
{
    public class UserServices : IUser
    {
        private readonly AppDbContext _dbcontext;
        private readonly IConfiguration _configuration;
        public UserServices(IConfiguration configuration,AppDbContext dbcontext)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
        }
        public async Task<ResponseVM> CreateUser(RegisterUserDto user)
        {
            ResponseVM response = ResponseVM.Instance;
            var checkEmail = await _dbcontext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if(checkEmail != null)
            {
                //response.data = reviews;
                response.responseCode = ResponseCode.BadRequest;
                response.responseMessage = "User Alredy Exsist with this Email";
                return response;
            }

            var passwordHasher = new PasswordHasher<object>();

            string hashedPassword = passwordHasher.HashPassword(null, user.Password);
            var newUser = new User
            {
                FullName = user.FullName,
                Email = user.Email,
                PasswordHash = hashedPassword,
                Role = user.Role,
                CreatedAt = DateTime.UtcNow
            };

            await _dbcontext.Users.AddAsync(newUser);
            await _dbcontext.SaveChangesAsync();

            //response.data = reviews;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "User Register Successful";

            return response;
        }

        public async Task<ResponseVM> DeleteUser(int id)
        {
            ResponseVM response = ResponseVM.Instance;
            var user = await _dbcontext.Users.FirstOrDefaultAsync(x=>x.Id == id);
            if (user == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "User Not Found with this Id";
                return response;
            }

            user.IsDeleted = true;
            await _dbcontext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "User Delete Successful";
            return response;
        }

        public async Task<ResponseVM> GetAllUser()
        {
            ResponseVM response = ResponseVM.Instance;

            var users = await _dbcontext.Users
                .Select(user => new UserDTO
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt
                })
                .ToListAsync();

            response.data = users;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Data Fetched Successful";

            return response;
        }

        public async Task<ResponseVM> GetUserbyId(int id)
        {
            ResponseVM response = ResponseVM.Instance;

            var user = await _dbcontext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "User Not Found with this Id";
                return response;
            }

            var uuser = new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };

            response.data = uuser;
            response.responseCode = ResponseCode.Success;
            response.responseMessage = "Data Fetched Successful";
            return response;
        }

        public async Task<ResponseVM> LoginUser(LoginUserDTO user)
        {
            ResponseVM response = ResponseVM.Instance;

            var checkMail = await _dbcontext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            var passwordHasher = new PasswordHasher<object>();

            if (checkMail != null)
            {
                var result = passwordHasher.VerifyHashedPassword(null, checkMail.PasswordHash, user.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    var base64Key = _configuration["JwtConfig:Key"];
                    var key = Encoding.UTF8.GetBytes(base64Key);

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new System.Security.Claims.ClaimsIdentity(new[]
                        {
                        new Claim("Id", checkMail.Id.ToString()),
                        new Claim(ClaimTypes.Name, checkMail.FullName),
                        new Claim(ClaimTypes.Role, checkMail.Role)
                    }),
                        Expires = DateTime.UtcNow.AddHours(4),
                        SigningCredentials = new SigningCredentials(
                         new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha512Signature
                        ),
                        Issuer = _configuration["JwtConfig:Issuer"],
                        Audience = _configuration["JwtConfig:Audience"]
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var ttoken = tokenHandler.WriteToken(token);

                    response.data = ttoken;
                    response.responseCode = ResponseCode.Success;
                    response.responseMessage = "User Login Successful";

                    return response;
                }

                response.responseCode = ResponseCode.BadRequest;
                response.responseMessage = "Enter Correct Password";

                return response;
            }

            response.responseCode = ResponseCode.NotFound;
            response.responseMessage = "User Not Exsist with this Email";

            return response;
        }

        public async Task<ResponseVM> UpdateUser(int id, UserUpdateDTO user)
        {
            ResponseVM response = ResponseVM.Instance;

            var exStudent = await _dbcontext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (exStudent == null) 
            {
                response.responseCode = ResponseCode.NotFound;
                response.responseMessage = "User Not Found with this Id";
                return response;
            }

            exStudent.FullName = user.FullName;
            exStudent.Email = user.Email;
            exStudent.Role = user.Role;

            await _dbcontext.SaveChangesAsync();

            response.responseCode = ResponseCode.Success;
            response.responseMessage = "User Update Successfuly";

            return response;
        }
    }
}
