using System.Collections.Generic;
using VartotojuValdymoSistema.Core.Models;
using VartotojuValdymoSistema.Core.Repositories;

namespace VartotojuValdymoSistema.Core.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(string connectionString)
        {
            _userRepository = new UserRepository(connectionString);
        }

        public void RegisterUser(User user)
        {
            _userRepository.AddUser(user);
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.ListUsers();
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }

        public void RemoveUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

        public void UpdatePassword(int id, string newPassword)
        {
            _userRepository.ChangePassword(id, newPassword);
        }

        public void ActivateUser(int id)
        {
            _userRepository.SetUserStatus(id, true);
        }

        public void DeactivateUser(int id)
        {
            _userRepository.SetUserStatus(id, false);
        }
    }
}