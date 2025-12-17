using System;
using System.Collections.Generic;

namespace StudentApi.Models;

public partial class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RollNumber { get; set; }

    public string? Gender { get; set; }

    public string? ProfilePhoto { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? UpdatedAt { get; set; }

    public bool IsDelete { get; set; }
}
