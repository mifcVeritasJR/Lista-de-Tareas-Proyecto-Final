using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lista_de_Tareas_Proyecto_Final.Models;

namespace Lista_de_Tareas_Proyecto_Final
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TaskItem>().Wait();
        }

        public async Task<List<TaskItem>> GetTasksAsync()
        {
            try
            {
                return await _database.Table<TaskItem>().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving tasks: {ex.Message}");
                return new List<TaskItem>();
            }
        }

        public async Task<int> SaveTaskAsync(TaskItem task)
        {
            try
            {
                if (task.Id != 0)
                {
                    return await _database.UpdateAsync(task);
                }
                else
                {
                    return await _database.InsertAsync(task);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving task: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> DeleteTaskAsync(TaskItem task)
        {
            try
            {
                return await _database.DeleteAsync(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting task: {ex.Message}");
                return 0;
            }
        }
    }
}