using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UnicontaAPI
{
    public sealed class UnicontaCredentialsDto
    {
        [FromHeader]
        [Required]
        public string APIKey { get; set; }

        [FromHeader]
        [Required]
        public string LoginId { get; set; }

        [FromHeader]
        [Required]
        public string Password { get; set; }
    }
}