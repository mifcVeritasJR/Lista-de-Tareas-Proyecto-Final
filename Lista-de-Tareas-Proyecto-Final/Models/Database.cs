using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lista_de_Tareas_Proyecto_Final
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        // Constructor que recibe la ruta de la base de datos
        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TaskItem>().Wait();  // Asegura que la tabla se cree si no existe
        }

        // Método para obtener todas las tareas de la base de datos
        public Task<List<TaskItem>> GetTasksAsync()
        {
            return _database.Table<TaskItem>().ToListAsync();
        }

        // Método para guardar una tarea (insertar o actualizar)
        public Task<int> SaveTaskAsync(TaskItem task)
        {
            if (task.Id != 0)
            {
                // Si el Id de la tarea no es 0, se actualiza
                return _database.UpdateAsync(task);
            }
            else
            {
                // Si el Id es 0, significa que es una nueva tarea, por lo que se inserta
                return _database.InsertAsync(task);
            }
        }

        // Método para eliminar una tarea de la base de datos
        public Task<int> DeleteTaskAsync(TaskItem task)
        {
            return _database.DeleteAsync(task);
        }
    }

    public class TaskItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }  // Título de la tarea
        public string Description { get; set; }  // Descripción de la tarea
        public string Status { get; set; }  // Estado de la tarea (Ejemplo: Pendiente, Completada)

        // Constructor que inicializa la descripción y el estado por defecto
        public TaskItem()
        {
            Description = string.Empty;
            Status = "Pending";  // Estado inicial por defecto
        }
    }
}
