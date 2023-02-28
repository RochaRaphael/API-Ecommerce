using API_Ecommerce.Models;
using API_Ecommerce.Repositories;
using API_Ecommerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API_Ecommerce.Services
{
    public class UserServices
    {
        private readonly UserRepositories userRepositories;
        public UserServices(UserRepositories userRepositories)
        {
            this.userRepositories = userRepositories;
        }

        public async Task<ResponseViewModel> RegisterUserAsync(NewUserViewModel user)
        {
            try
            {
                if (await userRepositories.UserExistsAsync(user.Login))
                    return new ResponseViewModel { Success = false, Message = "This user already exists" };

                var newUser = new User
                {
                    Name = user.Name,
                    Login = user.Login,
                    Email = user.Email,
                    Password = GenerateHash(user.Password),
                    Active = true,
                    Deleted = false,
                    VerificationKey = Guid.NewGuid().ToString()
                };
                await userRepositories.RegisterUserAsync(newUser);
                return new ResponseViewModel { Success = true, Message = "User successfully created" };
            }
            catch (DbUpdateException)
            {
                return new ResponseViewModel { Success = false, Message = "94X63 - Server failure" };
            }
        }

        public static string GenerateHash(string senha)
        {
            var md5 = MD5.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(senha);
            byte[] hash = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
