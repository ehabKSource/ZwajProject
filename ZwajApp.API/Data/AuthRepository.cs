using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using ZwajApp.API.Models;

namespace ZwajApp.API.Data
{
    public class AuthRepository : IAthuRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        } 
        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.UserName==userName);
            if (user== null) { return null; }

            if(VerifyPassword(password , user.PasswordHash, user.PasswordSaltHash)==false) { return null; }

            return user;
        }

        private bool VerifyPassword(string password , byte[] passwordHash , byte[] passwordSaltHash)
        {
            byte[] passwordHashTemp;
            
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                hmac.Key = passwordSaltHash;
                passwordHashTemp = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

            for (int i = 0; i < passwordHashTemp.Length; i++)
            {

                if(passwordHashTemp[i]!= passwordHash[i])
                {
                    return false;
                }

            }

            return true; 
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash; byte[] passwordSaltHash;

            CreatePasswordHash(password,out  passwordHash, out  passwordSaltHash);

            user.PasswordHash = passwordHash;

            user.PasswordSaltHash = passwordSaltHash;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await _context.Users.AnyAsync(x => x.UserName == userName)) return true;

            return false;
        }

        private  void CreatePasswordHash(string password ,out  byte[] passwordHash ,out  byte[] passwordSaltHash)
        {
          using(var hmac= new  System.Security.Cryptography.HMACSHA512()){

              passwordSaltHash=hmac.Key;
              passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
          }
        }




    }
}