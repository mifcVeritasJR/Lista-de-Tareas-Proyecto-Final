using System.Collections.ObjectModel;

namespace Lista_de_Tareas_Proyecto_Final
{
    public partial class MainPage : ContentPage
    {

        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainPage()
        {
            InitializeComponent();


            Tasks = new ObservableCollection<TaskItem>();


            TaskList.ItemsSource = Tasks;
        }


        private void OnSaveClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskEntry.Text))
            {

                Tasks.Add(new TaskItem
                {
                    Title = TaskEntry.Text,
                    IsCompleted = false
                });


                TaskEntry.Text = string.Empty;
            }
        }


        private void OnRemoveClicked(object sender, EventArgs e)
        {
            var task = (sender as SwipeItem).BindingContext as TaskItem;
            if (task != null)
            {
                Tasks.Remove(task);
            }
        }
    }


    public class TaskItem
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}