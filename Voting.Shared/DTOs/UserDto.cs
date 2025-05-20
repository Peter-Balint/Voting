
using System.ComponentModel.DataAnnotations;

namespace Voting.Shared.DTOs
{
    public class UserDto
    {
        public string? Id { get; set; }

        /// <summary>
        /// Felhasználó neve.
        /// </summary>
        [Required(ErrorMessage = "A név megadása kötelező.")] // feltételek a validáláshoz
        [StringLength(60, ErrorMessage = "A foglaló neve maximum 60 karakter lehet.")]
        public required string Name { get; set; }

        /// <summary>
        /// Felhasználó e-mail címe.
        /// </summary>
        [Required(ErrorMessage = "E-mail cím megadása kötelező.")]
        [EmailAddress(ErrorMessage = "Az e-mail cím nem megfelelő formátumú.")]
        [DataType(DataType.EmailAddress)] // pontosítjuk az adatok típusát
        public required string Email { get; set; }


        /// <summary>
        /// Jelszó
        /// </summary>
        [Required(ErrorMessage = "A jelszó megadása kötelező.")]
        public required string Password { get; set; }
    }
}
