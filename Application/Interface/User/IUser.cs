using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Data_TransferModels.ResponseModels;
using Application.Data_TransferModels.UserDTO;

namespace Application.Interface.User
{
    public interface IUser
    {
        Task<ResponseVM> CreateUser(RegisterUserDto user);
        Task<ResponseVM> LoginUser(LoginUserDTO user);
        Task<ResponseVM> GetAllUser();
        Task<ResponseVM> GetUserbyId(int id);
        Task<ResponseVM> UpdateUser(int id, UserUpdateDTO user);
        Task<ResponseVM> DeleteUser(int id);
    }
}
