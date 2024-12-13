﻿using SQLite;

namespace Lista_de_Tareas_Proyecto_Final.Models
{
    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string Status { get; set; }

        public TaskItem()
        {
            Description = string.Empty;
            IsCompleted = false;
            Status = "Por hacer"; // Estado inicial por defecto
        }
    }
}
