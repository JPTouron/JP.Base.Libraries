namespace ADO.DAL.Commands.Model
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Manager
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Person
    {
        public int? Age { get; set; }
        public int Id { get; set; }

        public int ManagerId { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
    }

    public class PersonDept
    {
        public int DepartmentId { get; set; }
        public int Id { get; set; }
        public int PersonId { get; set; }
    }
}