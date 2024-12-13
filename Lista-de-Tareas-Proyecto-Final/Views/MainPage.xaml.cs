﻿using Lista_de_Tareas_Proyecto_Final.Models;

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
                    IsCompleted = false,
                    Status = "Por hacer" // Estado por defecto
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

        async void OnEditClicked(object sender, EventArgs e)
        {
            if (sender is SwipeItem swipeItem && swipeItem.BindingContext is TaskItem taskToEdit)
            {
                // Muestra un cuadro de diálogo para editar el nombre de la tarea
                string newTitle = await DisplayPromptAsync("Editar tarea", "Escribe el nuevo nombre de la tarea:", initialValue: taskToEdit.Title);

                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    taskToEdit.Title = newTitle;
                    await App.Database.SaveTaskAsync(taskToEdit);
                    LoadTasks();
                }
            }
        }

        async void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.BindingContext is TaskItem task)
            {
                task.IsCompleted = e.Value;
                await App.Database.SaveTaskAsync(task);
            }
        }

        async void OnStatusChanged(object sender, EventArgs e)
        {
            if (sender is Picker picker && picker.BindingContext is TaskItem task)
            {
                string selectedStatus = picker.SelectedItem as string;
                if (!string.IsNullOrEmpty(selectedStatus))
                {
                    task.Status = selectedStatus;
                    await App.Database.SaveTaskAsync(task);
                }
            }
        }
    }
}
