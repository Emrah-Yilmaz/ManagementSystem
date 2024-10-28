namespace CommonLibrary.Messages
{
    public class CreatedDepartmentMessage
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedOn  { get; set; }
        public string CreatedBy { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
    }
}
