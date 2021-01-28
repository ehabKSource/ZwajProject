
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ZwajApp.API.Models;

namespace ZwajApp.API.Data
{
    public class TrialData
    {
        private DataContext _datacontext;
        public TrialData(DataContext  datacontext)
        {
            _datacontext = datacontext;
        }

        public void TrialUsers()
        {

            var users = System.IO.File.ReadAllText("Data/UsersTrialData.json");

            var AllUsers = JsonConvert.DeserializeObject<List<User>>(users);
            foreach (var user in AllUsers)
            {
                byte[] passwordHash, passwordHashSalt;
                CreatePasswordHash("password",out  passwordHash,out  passwordHashSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSaltHash = passwordHashSalt;
                user.UserName = user.UserName.ToLower();
                _datacontext.Add(user);
            }

            _datacontext.SaveChanges();

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSaltHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {

                passwordSaltHash = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


    }

}
