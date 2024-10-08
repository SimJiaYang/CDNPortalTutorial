namespace CDNPortalTutorial.Model.Dto
{
    public class UpdateUserDto
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string>? Skillsets { get; set; }
        public List<string>? Hobby { get; set; }
    }
}
