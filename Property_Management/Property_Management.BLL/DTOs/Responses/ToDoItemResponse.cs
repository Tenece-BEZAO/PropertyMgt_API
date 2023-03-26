namespace Property_Management.BLL.DTOs.Responses
{
    public class ToDoItemResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }
}
