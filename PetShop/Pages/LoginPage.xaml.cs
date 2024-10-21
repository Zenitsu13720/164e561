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

namespace PetShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrEmpty(LoginTextBox.Text))
                {
                    errors.AppendLine("Заполните логин");
                }
                if(string.IsNullOrEmpty(PasswordBox.Password))
                {
                    errors.AppendLine("Заполните пароль");
                }
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Data.PetShopEntities1.GetContext().User.Any(d => d.Login == LoginTextBox.Text &&
                d.Password == PasswordBox.Password))
                {
                    var user = Data.PetShopEntities1.GetContext().User.Where(d => d.Login == LoginTextBox.Text &&
                 d.Password == PasswordBox.Password).FirstOrDefault();

                    Classes.Manager.CurrentUser = user;

                    switch (user.UserRole.UserRole1)
                    {
                        case "Администратор":
                            Classes.Manager.MainFrame.Navigate(new Pages.AdminLKPage());
                            break;
                        case "Клиент":
                            Classes.Manager.MainFrame.Navigate(new Pages.ViewProducts());
                            break;
                        case "Менеджер":
                            Classes.Manager.MainFrame.Navigate(new Pages.ViewProducts());
                            break;
                    }

                    MessageBox.Show("Успешный вход", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Учетная запись не найдена", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.ViewProducts());
        }
    }
}
