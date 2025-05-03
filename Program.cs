using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DAL.Models;
using WindowsFormsApp1.Presentation;
namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Gọi hàm khởi tạo và seed dữ liệu
            ApplicationDbContext.InitializeDatabase();

            using (var context = new ApplicationDbContext())
            {
                if (!context.Users.Any())
                {
                    var initializer = new ApplicationDbContext.DbInitializer();
                    initializer.InitializeDatabase(context); // Gọi seed thủ công
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
        }
    }
}
