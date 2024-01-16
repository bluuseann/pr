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
using System.Windows.Shapes;

namespace pr
{
    /// <summary>
    /// Логика взаимодействия для kabinet.xaml
    /// </summary>
    public partial class kabinet : Window
    {
        public kabinet()
        {
            InitializeComponent();
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            var context = new AppDbContext();
            //var user = context.Users.SingleOrDefault(x => (x.Login == "admin" || x.Email == "") && x.Password == "123");
            var user = context.Users.Where(x => (x.Login == "admin" || x.Email == "") && x.Password == "123").AsEnumerable().Select(x => { x.Password = passchange.Text; return x; });
            foreach(var item in user)
            {
                context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            context.SaveChanges();

        }
        
    }
}
