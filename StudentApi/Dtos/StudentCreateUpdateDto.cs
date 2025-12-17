namespace StudentApi.Dtos
{
    public class StudentCreateUpdateDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int RollNumber { get; set; } 
        public string Gender { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ProfilePhoto { get; set; } = null!;
    }
}
