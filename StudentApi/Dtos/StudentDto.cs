namespace StudentApi.Dtos
{
    public class StudentDto: StudentCreateUpdateDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt {get; set;}
    }
}
