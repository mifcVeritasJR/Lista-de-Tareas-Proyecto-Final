using Lista_de_Tareas_Proyecto_Final.Models;

namespace Lista_de_Tareas_Proyecto_Final
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadTasks();
        }


        async void LoadTasks()
        {
            var tasks = await App.Database.GetTasksAsync();
            TaskList.ItemsSource = tasks;
        }


        async void OnSaveClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskEntry.Text))
            {
                var newTask = new TaskItem
                {
                    Title = TaskEntry.Text,
                    Description = "",
                    Status = "Pending"
                };

                await App.Database.SaveTaskAsync(newTask);

                TaskEntry.Text = string.Empty;
                LoadTasks();
            }
            else
            {
                await DisplayAlert("Error", "Por favor, escribe una tarea antes de guardar.", "OK");
            }
        }


        async void OnRemoveClicked(object sender, EventArgs e)
        {
            if (sender is SwipeItem swipeItem && swipeItem.BindingContext is TaskItem taskToDelete)
            {
                await App.Database.DeleteTaskAsync(taskToDelete);
                LoadTasks();
            }
        }
    }
}
