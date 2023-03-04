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

        public async Task<ShowUserViewModel> FindUserByIdAsync(int id)
        {
            try
            {
                var user = await userRepositories.FindUserByIdAsync(id);
                if (user == null)
                    return null;

                return new ShowUserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Active = user.Active,
                    Deleted = user.Deleted
                };
            }
            catch
            {
                return null;
            }
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

        

        public async Task<ResponseViewModel> LoginAsync(LoginViewModel model)
        {
            try
            {
                model.Password = GenerateHash(model.Password);
                var login = await userRepositories.CorrectLoginAsync(model);

                if(login == null)
                    return new ResponseViewModel { Success = false, Message = "Login or password is wrong" };


                //login.LastToken = tokenService.GenerateToken(login).ToString();
                var token = userRepositories.InsertLasTokenAsync(login);
                return new ResponseViewModel { Success = true, Message = login.LastToken };


            }
            catch
            {
                return new ResponseViewModel { Success = false, Message = "22X08 - Server failure" };
            }
        }

        public async Task<ResultViewModel<NewUserViewModel>> UpdateUserAsync(NewUserViewModel updateUser)
        {
            try
            {
                var id = await userRepositories.GetUserByLoginAsync(updateUser.Login);
                if(id == null)
                    return new ResultViewModel<NewUserViewModel>("60X45 - User not found");

                await userRepositories.UpdateUserAsync(updateUser, id);
                return new ResultViewModel<NewUserViewModel>(updateUser);
            }
            catch
            {
                return new ResultViewModel<NewUserViewModel>("80X25 - Server failure");
            }
        }

        public async Task<ResponseViewModel> DeleteUserAsync(int id)
        {
            try
            {
                var user = await userRepositories.FindUserByIdAsync(id);
                if (user == null)
                    return new ResponseViewModel { Success = false, Message = "User not found" };

                var userDeleted = await userRepositories.DeleteUserAsync(user);
                if (userDeleted)
                    return new ResponseViewModel { Success = true, Message = "Deleted" };
                else
                    return new ResponseViewModel { Success = false, Message = "65X43 - Server failure" };
            }
            catch
            {
                return new ResponseViewModel { Success = false, Message = "32Y56 - Error" };
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
