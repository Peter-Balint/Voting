using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Voting.DataAccess.Models
{
    public class User : IdentityUser
    {
        [MaxLength(255)]
        public required string Name { get; set; }

        [MaxLength(100)]
        public string AccessToken { get; set; } = GenerateOpaqueToken();

        public void ResetAccessToken()
        {
            AccessToken = GenerateOpaqueToken();
        }

        private static string GenerateOpaqueToken()
        {
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
