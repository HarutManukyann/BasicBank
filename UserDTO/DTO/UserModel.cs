namespace Models.DTO
{

    public class UserModel
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime Birthday { get; set; }

        public string PassportNumber { get; set; } = null!;

        public string? Address { get; set; }       

        public bool IsActive { get; set; }
    }
}