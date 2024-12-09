using Lista_de_Tareas_Proyecto_Final;
using System.IO;

namespace Lista_de_Tareas_Proyecto_Final
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tareas.db3");
                    database = new Database(dbPath);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }
    }
}
