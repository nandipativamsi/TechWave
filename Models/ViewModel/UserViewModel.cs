using Microsoft.AspNetCore.Identity;
using TechWave.Models.DomainModel;

namespace TechWave.Models.ViewModel
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; } = null!;
        //public IEnumerable<IdentityRole> Roles { get; set; } = null!;
    }
}
