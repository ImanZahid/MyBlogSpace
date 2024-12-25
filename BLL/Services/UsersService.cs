using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public interface IUsersService
    {
        Service Create(User user);
        Service Update(User user);
        Service Delete(int id);
        IQueryable<UsersModels> Query();
    }

    public class UsersService : Service, IUsersService
    {
        public UsersService(DB db) : base(db)
        {
        }
        public Service Create(User user)
        {
            try
            {
                if (_db.Users.Any(u => u.UserName.ToUpper() == user.UserName.Trim().ToUpper()))
                    return Error("User with the same username already exists!");
                _db.Users.Add(user);
                _db.SaveChanges();
                return Success("User created successfully.");
            }
            catch (Exception ex)
            {
                return Error($"Error creating user: {ex.Message}");
            }
        }

        public Service Update(User user)
        {
            var existingUser = _db.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
            {
                return Error("User not found.");
            }

            try
            {
                existingUser.UserName = user.UserName;
                existingUser.Password = user.Password;
                existingUser.IsActive = user.IsActive;
                existingUser.RoleId = user.RoleId;

                _db.SaveChanges();
                return Success("User updated successfully.");
            }
            catch (Exception ex)
            {
                return Error($"Error updating user: {ex.Message}");
            }
        }

        public Service Delete(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return Error("User not found.");
            }

            try
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
                return Success("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return Error($"Error deleting user: {ex.Message}");
            }
        }

        public IQueryable<UsersModels> Query()
        {
            return _db.Users.Where(u => u.IsActive).Include(u => u.Role).Select(u => new UsersModels { Record = u });
        }
    }
}
