using SQLite;

namespace Lista_de_Tareas_Proyecto_Final.Models
{
    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
    }
}
