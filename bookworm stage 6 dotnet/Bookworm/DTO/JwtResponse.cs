namespace Bookworm.DTO
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public JwtResponse(string token, int id, string name)
        {
            Token = token;
            Id = id;
            Name = name;
        }
    }
}