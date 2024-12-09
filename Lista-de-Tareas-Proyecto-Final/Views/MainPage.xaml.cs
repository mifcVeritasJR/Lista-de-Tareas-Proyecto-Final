using Lista_de_Tareas_Proyecto_Final.Models;
using System.Collections.ObjectModel;
using System.IO;

namespace Lista_de_Tareas_Proyecto_Final
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }
        private readonly Database _database;

        public MainPage()
        {
            InitializeComponent();

            // Ruta para la base de datos SQLite
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tasks.db3");

            _database = new Database(dbPath);  // Inicializa la base de datos
            Tasks = new ObservableCollection<TaskItem>();

            // Cargar las tareas desde la base de datos
            LoadTasks();

            TaskList.ItemsSource = Tasks;
        }

        // Cargar tareas desde la base de datos
        private async void LoadTasks()
        {
            var tasks = await _database.GetTasksAsync();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskEntry.Text))
            {
                var task = new TaskItem
                {
                    Title = TaskEntry.Text,
                    Description = "",
                    Status = "Pending"
                };

                // Guardar la tarea en la base de datos
                await _database.SaveTaskAsync(task);

                // Agregarla a la lista local
                Tasks.Add(task);

                TaskEntry.Text = string.Empty;
            }
        }

        private async void OnRemoveClicked(object sender, EventArgs e)
        {
            var task = (sender as SwipeItem).BindingContext as TaskItem;
            if (task != null)
            {
                // Eliminar la tarea de la base de datos
                await _database.DeleteTaskAsync(task);

                // Eliminarla de la lista local
                Tasks.Remove(task);
            }
        }
    }
}
