using JobPortal.DTO;
using JobPortal.DTO.ReturnObjects;
using JobPortal.Models;
using JobPortal.Utility;

namespace JobPortal.Repository.IRepostiory;

public interface IUserRepository
{
    Task<Response<LoginResponse>> SignIn(LoginRequestDTO loginRequestDTO);
    Task<Response> Confirm(string id, string token);

    Task<Response<RegisterResponse>> Register(RegisterationRequestDTO registerationRequestDTO);
    Task<Response> addRole(string userId, string roleName);
    Task<Response> RemoveRole(string userId, string roleName);
}