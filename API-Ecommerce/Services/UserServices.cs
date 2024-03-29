﻿using API_Ecommerce.Models;
using API_Ecommerce.Repositories;
using API_Ecommerce.Services.Caching;
using API_Ecommerce.ViewModels;
using Konscious.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        private readonly RoleRepositories roleRepositories;
        private readonly RoleServices roleServices;
        private readonly TokenService tokenService;
        private readonly ICachingService cache;

        public UserServices(UserRepositories userRepositories, RoleRepositories roleRepositories, RoleServices roleServices, TokenService tokenService, ICachingService cache)
        {
            this.userRepositories = userRepositories;
            this.roleRepositories = roleRepositories;
            this.roleServices = roleServices;
            this.tokenService = tokenService;
            this.cache = cache;
        }

        public async Task<ShowUserViewModel> GetByIdAsync(int id)
        {
            try
            {
                User? user;

                var userCache = await cache.GetAsync(id.ToString());
                if (userCache != null)
                {
                    user = JsonConvert.DeserializeObject<User>(userCache);
                    if (user == null)
                        return null;
                }                 
                else
                {
                    user = await userRepositories.GetByIdAsync(id);
                }
                    

                await cache.SetAsync(id.ToString(), JsonConvert.SerializeObject(user));
                var showUser =  new ShowUserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Login = user.Login,
                    Email = user.Email,
                    Active = user.Active,
                    Deleted = user.Deleted,
                };
                List<string> rolesList = new List<string>();
                foreach (var role in user.Roles)
                {
                    rolesList.Add(role.Name);
                }
                showUser.Roles = rolesList;

                return showUser;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ResultViewModel<User>> RegisterUserAsync(NewUserViewModel model)
        {
            try
            {
                List<Role>? rolesList = new List<Role>();
                if (await userRepositories.UserExistsAsync(model.Login))
                    return new ResultViewModel<User>("This user already exists");

                await roleServices.RegisterRoleListAsync(model.Roles);
                var roleList = await roleRepositories.GetListByNamesAsync(model.Roles);
                

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
                    VerificationKey = Guid.NewGuid().ToString(),
                    Roles = new List<Role>()
                };
                foreach (var role in roleList)
                {
                    newUser.Roles.Add(role);
                }

                await userRepositories.RegisterUserAsync(newUser);
                return new ResultViewModel<User>(newUser);
            }
            catch (DbUpdateException)
            {
                return new ResultViewModel<User>("94X63 - Server failure");
            }
        }

        

        public async Task<ResultViewModel<User>> LoginAsync(LoginViewModel model)
        {
            try
            {
                var user = await userRepositories.GetByLoginAsync(model.Login);
                model.Password = GenerateHash(model.Password, user.Salt).Hash;

                var login = await userRepositories.CorrectLoginAsync(model);

                if (login == null)
                    return new ResultViewModel<User>("Login or password is wrong");

               login.LastToken = tokenService.GenerateToken(login);
               await userRepositories.InsertLastTokenAsync(login);

               return new ResultViewModel<User>(login);
            }
            catch
            {
                return new ResultViewModel<User>("22X08 - Server failure");
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
