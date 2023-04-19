using FCBankBasicHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserEqualizerWorkerService.Models
{
    public class PlaceHolderUser
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; } = null!;
        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; } = null!;
        [JsonPropertyName("lastname")]
        public string LastName { get; set; } = null!;
        [JsonPropertyName("birthday")]
        public DateTime Birthday { get; set; }
        [JsonPropertyName("passportnumber")]
        public string PassportNumber { get; set; } = null!;
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("isactive")]
        public bool IsActive { get; set; }
    }
}
