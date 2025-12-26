using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;
using PracticeTracker.Domain.Entities;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;

namespace PracticeTracker.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreatedAt { get; set; }

        // Навигационные свойства
        public virtual Student? Student { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }

    public enum UserType
    {
        Student,
        Teacher,
        Admin
    }
}

using System;
using System.Collections.Generic;

namespace PracticeTracker.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public UserType UserType { get; set; }
        public DateTime CreatedAt { get; set; }

        // Навигационные свойства
        public virtual Student? Student { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }

    public enum UserType
    {
        Student,
        Teacher,
        Admin
    }
}

namespace PracticeTracker.Domain.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public string? RecordBookNumber { get; set; }

        // Навигационные свойства
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}

