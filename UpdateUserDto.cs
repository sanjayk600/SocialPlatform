namespace Models.User
{
    public class UpdateUserDto
    {
        public string Username { get; set; }  
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Bio { get; set; }
    }
}
