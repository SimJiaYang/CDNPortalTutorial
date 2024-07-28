namespace CDNPortalTutorial.Model.Dto
{
    public class UpdateUserDto
    {
        public string? Name { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string? PhoneNumber { get; set; }
        public List<string>? Skillsets { get; set; }
        public List<string>? Hobby { get; set; }
    }
}
