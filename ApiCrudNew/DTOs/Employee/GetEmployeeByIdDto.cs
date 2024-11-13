namespace ApiCrudNew.DTOs.Employee
{
    public class GetEmployeeByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
    }
}
