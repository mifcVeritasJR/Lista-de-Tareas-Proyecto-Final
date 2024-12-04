using Microsoft.Build.Utilities;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoListApp
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TaskItem>().Wait();
        }

        public Task<List<TaskItem>> GetTasksAsync()
        {
            return _database.Table<TaskItem>().ToListAsync();
        }

        public Task<int> SaveTaskAsync(TaskItem task)
        {
            return _database.InsertAsync(task);
        }

        public Task<int> UpdateTaskAsync(TaskItem task)
        {
            return _database.UpdateAsync(task);
        }

        public Task<int> DeleteTaskAsync(TaskItem task)
        {
            return _database.DeleteAsync(task);
        }
    }
}