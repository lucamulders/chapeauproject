using ChapeauProject.Models;
using ChapeauProject.Repositories;
using System.Text;
using System.Security.Cryptography;

namespace ChapeauProject.Services
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        private string HashPassword(string password)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                byte[] hashBytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public List<Staff> GetAll()
        {
            return _staffRepository.GetAll();
        }

        public Staff? GetById(int id)
        {
            return _staffRepository.GetById(id);
        }

        public Staff? GetByLoginCredentials(int staffID, string password)
        {
            return _staffRepository.GetByLoginCredentials(staffID, HashPassword(password));
        }
    }
}
