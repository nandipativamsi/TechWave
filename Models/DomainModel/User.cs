using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechWave.Models.DomainModel
{
    public class User: IdentityUser
    {
        [NotMapped]
        public IList<string> RoleNames { get; set; }
    }
}
