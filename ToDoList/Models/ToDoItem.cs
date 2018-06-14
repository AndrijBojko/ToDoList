using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    [Table("Item")]
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
