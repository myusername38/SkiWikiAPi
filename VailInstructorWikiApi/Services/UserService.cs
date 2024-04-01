using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VailInstructorWikiApi.DTOs.User;
using VailInstructorWikiApi.Models;
using VailInstructorWikiApi.Repos;

namespace VailInstructorWikiApi.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(int skip = 0, int take = 100);
        Task<User> GetUserByEmail(string email);
        Task<User> CreateUser(User user);
        Task<User> GetUser(int id);
        Task DeleteUserById(int id);
        Task DeleteUserByEmail(string email);
    }

    public class UserService : IUserService
    {
        private readonly UserRepo _userRepo;

        public UserService(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _userRepo.Users().FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _userRepo.Users().FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<List<User>> GetUsers(int skip = 0, int take = 100)
        {
            var users = await _userRepo.Users()
                .AsNoTracking()
                .Skip(skip)
                .Take(take)
                .OrderBy(u => u.Id)
                .ToListAsync();
            return users;
        }


        public async Task<User> CreateUser(User user)
        {
            _userRepo.Users().Add(user);
            await _userRepo.Save();
            return user;
        }

        public async Task DeleteUserByEmail(string email)
        {
            var user = await GetUserByEmail(email);
            if (user != null)
            {
                await DeleteUser(user);
            }
            return;
        }

        public async Task DeleteUserById(int id)
        {
            var user = await _userRepo.Users()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                await DeleteUser(user);
            }
            return;
        }

        private async Task DeleteUser(User user)
        {

            _userRepo.Users().Remove(user);
            await _userRepo.Save();
        }
    }
}

