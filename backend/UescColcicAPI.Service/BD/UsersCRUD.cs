using System;
using System.Collections.Generic;
using System.Linq;
using UescColcicAPI.Services.BD.Interfaces;
using UescColcicAPI.Core;
using UescColcicAPI.Services.ViewModels;
using UescColcicAPI.Services.InputModels;

namespace UescColcicAPI.Services.BD
{
    public class UsersCRUD : IUserCRUD
    {
        private readonly UescColcicAPIDbContext _context;

        public UsersCRUD(UescColcicAPIDbContext context)
        {
            _context = context;
        }

        public int Create(UserInputModel userViewModel)
        {
            var user = new User
            {
                Username = userViewModel.Username,
                Password = userViewModel.Password,
                Roles = userViewModel.Roles
            };

            // Verificar se já existe um usuário com o mesmo nome de usuário
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                throw new InvalidOperationException($"A user with username {user.Username} already exists.");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return user.UserId; 
        }

        public void Update(int id, UserInputModel userViewModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                // Verificar se já existe outro usuário com o mesmo nome de usuário
                if (_context.Users.Any(u => u.Username == userViewModel.Username && u.UserId != id))
                {
                    throw new InvalidOperationException($"Another user with username {userViewModel.Username} already exists.");
                }

                user.Username = userViewModel.Username;
                user.Password = userViewModel.Password;
                user.Roles = userViewModel.Roles;

                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public UserViewModel ReadById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                throw new InvalidOperationException($"User with id {id} not found.");
            }
            return new UserViewModel
            {
                UserId = user.UserId,
                Username = user.Username,
                Roles = user.Roles
            };
        }

        public IEnumerable<UserViewModel> ReadAll()
        {
            var users = _context.Users.ToList();
            return users.Select(u => new UserViewModel
            {
                UserId = u.UserId,
                Username = u.Username,
                Roles = u.Roles
            });
        }
    }
}
