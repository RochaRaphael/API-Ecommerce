using API_Ecommerce.Models;
using API_Ecommerce.Repositories;
using API_Ecommerce.ViewModels;
using Konscious.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API_Ecommerce.Services
{
    public class Password
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
    public class UserServices
    {
        private readonly UserRepositories userRepositories;
        public UserServices(UserRepositories userRepositories)
        {
            this.userRepositories = userRepositories;
        }

        public async Task<ShowUserViewModel> GetByIdAsync(int id)
        {
            try
            {
                var user = await userRepositories.GetByIdAsync(id);
                if (user == null)
                    return null;

                return new ShowUserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Login = user.Login,
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
        public async Task<ResponseViewModel> RegisterUserAsync(NewUserViewModel model)
        {
            try
            {
                if (await userRepositories.UserExistsAsync(model.Login))
                    return new ResponseViewModel { Success = false, Message = "This user already exists" };

                var hashSalt = GenerateHash(model.Password);
                model.Password = hashSalt.Hash;
                model.Salt = hashSalt.Salt;
                var newUser = new User
                {
                    Name = model.Name,
                    Login = model.Login,
                    Email = model.Email,
                    Password = hashSalt.Hash,
                    Salt = hashSalt.Salt,
                    Active = true,
                    Deleted = false,
                    VerificationKey = Guid.NewGuid().ToString()
                };
                await userRepositories.RegisterUserAsync(newUser);
                return new ResponseViewModel { Success = true, Message = "User successfully created"};
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
                var user = await userRepositories.GetByLoginAsync(model.Login);
                model.Password = GenerateHash(model.Password, user.Salt).Hash;

                var login = await userRepositories.CorrectLoginAsync(model);

                if(login == null)
                    return new ResponseViewModel { Success = false, Message = "Login or password is wrong" };


                //login.LastToken = tokenService.GenerateToken(login).ToString();
                var token = userRepositories.InsertLasTokenAsync(login);
                return new ResponseViewModel { Success = true, Message = "login.LastToken" };


            }
            catch
            {
                return new ResponseViewModel { Success = false, Message = "22X08 - Server failure" };
            }
        }

        public async Task<ResultViewModel<NewUserViewModel>> UpdateUserAsync(NewUserViewModel updateUser, int id)
        {
            try
            {
                var user = await userRepositories.GetByIdAsync(id);
                if(user == null)
                    return new ResultViewModel<NewUserViewModel>("60X45 - User not found");

                await userRepositories.UpdateUserAsync(updateUser, id);
                return new ResultViewModel<NewUserViewModel>(updateUser);
            }
            catch
            {
                return new ResultViewModel<NewUserViewModel>("80X25 - Server failure");
            }
        }

        public async Task<ResultViewModel<bool>> DeleteUserAsync(int id)
        {
            try
            {
                var user = await userRepositories.GetByIdAsync(id);
                if (user == null)
                    return new ResultViewModel<bool>(false, new List<string> { "User not found" });

                var userDeleted = await userRepositories.DeleteUserAsync(user);
                if (userDeleted)
                    return new ResultViewModel<bool>(true);
                else
                    return new ResultViewModel<bool>(false, new List<string> {"65X43 - Server failure"});
            }
            catch
            {
                return new ResultViewModel<bool>(false, new List<string> { "32Y56 - Error"});
            }
        }


        private static Password GenerateHash(string password)
        {
            int saltSize = 16; // Define o tamanho do salt em bytes
            int hashSize = 32; // Define o tamanho do hash em bytes

            var salt = new byte[saltSize]; // Cria um array de bytes para o salt
            var rng = RandomNumberGenerator.Create(); // Cria um gerador de números aleatórios criptográficos CNG
            rng.GetBytes(salt); // Gera um salt aleatório usando o RNG criptográfico

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)); // Cria um objeto Argon2id usando a senha como entrada

            argon2.Salt = salt; // Define o salt para o objeto Argon2id
            argon2.DegreeOfParallelism = 8; // Define o grau de paralelismo para 8
            argon2.Iterations = 4; // Define o número de iterações para 4
            argon2.MemorySize = 1024 * 1024; // Define o tamanho da memória em bytes para 1 GB

            byte[] hash = argon2.GetBytes(hashSize); // Gera o hash da senha usando o objeto Argon2id

            byte[] hashBytes = new byte[saltSize + hashSize]; // Cria um array de bytes para armazenar o salt e o hash
            Array.Copy(salt, 0, hashBytes, 0, saltSize); // Copia o salt para o início do array de bytes
            Array.Copy(hash, 0, hashBytes, saltSize, hashSize); // Copia o hash para o final do array de bytes

            // Cria um objeto Password que contém o hash e o salt como strings Base64
            Password hashSalt = new Password { Hash = Convert.ToBase64String(hashBytes), 
                                               Salt = Convert.ToBase64String(salt) };
            
            return hashSalt; // Retorna o objeto Password

        }


        private static Password GenerateHash(string password, string saltString)
        {
            byte[] salt = Convert.FromBase64String(saltString);
            int hashSize = 32;
            int saltSize = 16;

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // four cores
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 1024; // 1 GB

            byte[] hash = argon2.GetBytes(hashSize);

            byte[] hashBytes = new byte[saltSize + hashSize];
            Array.Copy(salt, 0, hashBytes, 0, saltSize);
            Array.Copy(hash, 0, hashBytes, saltSize, hashSize);

            Password hashSalt = new Password { Hash = Convert.ToBase64String(hashBytes), 
                                               Salt = Convert.ToBase64String(salt) };
            return hashSalt;

        }

        //public static string GenerateHash(string senha)
        //{
        //    var md5 = MD5.Create();
        //    byte[] bytes = Encoding.ASCII.GetBytes(senha);
        //    byte[] hash = md5.ComputeHash(bytes);

        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < hash.Length; i++)
        //    {
        //        sb.Append(hash[i].ToString("X2"));
        //    }
        //    return sb.ToString();
        //}
    }
}
