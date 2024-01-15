using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=panUsers;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            reg reg = new reg();
            reg.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var login = logTB.Text;
            var password = passTB.Password;

            var context = new AppDbContext();

            var user = context.Users.SingleOrDefault(x => (x.Login == login || x.Email == login)  && x.Password == password);
            logTB.BorderBrush = new SolidColorBrush(Colors.Black);
            passTB.BorderBrush = new SolidColorBrush(Colors.Black);
            if (user is null)
            {
                //passTB.Password = pass.Text;
                //pass.Visibility = Visibility.Visible;
                //passTB.Visibility = Visibility.Hidden;
                var user_exists = context.Users.FirstOrDefault(x => x.Login == login);
                if (user_exists is null)
                {
                    logTB.BorderBrush = new SolidColorBrush(Colors.Red);
                    error.Text = "Такого пользователя не существует";

                }
                else
                {
                    passTB.BorderBrush = new SolidColorBrush(Colors.Red);
                    error.Text = "Неверный пароль";
                }
                return;
            }
            
            MessageBox.Show("Вы успешно вошли в аккаунт!");
            kabinet kabinet = new kabinet();
            kabinet.Show();
            this.Close();
            kabinet.name.Text = $"Здравствуйте, { user.Login} ";
        }
    }
}
