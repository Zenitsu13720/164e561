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
    /// Логика взаимодействия для AdminLKPage.xaml
    /// </summary>
    public partial class AdminLKPage : Page
    {
        public AdminLKPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            ProductsListView.ItemsSource = Data.PetShopEntities1.GetContext().Product.ToList();
            CountOfLabel.Content = $"{Data.PetShopEntities1.GetContext().Product.Count()}" +
                $"/{Data.PetShopEntities1.GetContext().Product.Count()}";
            if (Classes.Manager.CurrentUser != null)
            {
                FIOLabel.Content = $"{Classes.Manager.CurrentUser.Surname}" +
                    $"{Classes.Manager.CurrentUser.Name}" +
                    $"{Classes.Manager.CurrentUser.Patronymic}";
            }

            SearchTextBox.Text = string.Empty;
            SortUpRadioButton.IsChecked = false;
            SortDownRadioButton.IsChecked = false;

            var manufacturerList = Data.PetShopEntities1.GetContext().ProductManufacturer.ToList();
            manufacturerList.Insert(0, new Data.ProductManufacturer { ManufacturerName = "Все производители" });
            ManufacturerComboBox.ItemsSource = manufacturerList;
            ManufacturerComboBox.SelectedIndex = 0;

            if (Classes.Manager.CurrentUser != null && Classes.Manager.CurrentUser.UserRole.UserRole1 == "Администратор")
            {
                BackButton.Visibility = Visibility.Visible;
            }
        }

        public List<Data.Product> _currentProducts = Data.PetShopEntities1.GetContext().Product.ToList();

        public void Update()
        {
            try
            {
                _currentProducts = Data.PetShopEntities1.GetContext().Product.ToList();

                _currentProducts = (from item in Data.PetShopEntities1.GetContext().Product
                                    where item.ProductName.Name.ToLower().Contains(SearchTextBox.Text) ||
                                    item.ProductDescription.ToLower().Contains(SearchTextBox.Text) ||
                                    item.ProductCost.ToString().ToLower().Contains(SearchTextBox.Text) ||
                                    item.ProductQuantityInStock.ToString().ToLower().Contains(SearchTextBox.Text)
                                    select item).ToList();

                if (SortUpRadioButton.IsChecked == true)
                {
                    _currentProducts = _currentProducts.OrderBy(d => d.ProductCost).ToList();
                }
                if (SortDownRadioButton.IsChecked == true)
                {
                    _currentProducts = _currentProducts.OrderByDescending(d => d.ProductCost).ToList();
                }

                var selected = ManufacturerComboBox.SelectedItem as Data.ProductManufacturer;
                if (selected != null && selected.ManufacturerName != "Все производители")
                {
                    _currentProducts = _currentProducts.Where(d => d.ProductManufacturerID == selected.ProductManufacturerID).ToList();
                }
                ProductsListView.ItemsSource = _currentProducts;

                CountOfLabel.Content = $"{_currentProducts.Count()}" +
                $"/{Data.PetShopEntities1.GetContext().Product.Count()}";

            }
            catch (Exception)
            {


            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void SortUpRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Update();

        }

        private void SortDownRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Update();

        }

        private void ManufacturerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = (sender as Button).DataContext as Data.Product;
                var forDelete = Data.PetShopEntities1.GetContext().OrderProduct.Where(d => d.ProductID == selected.ProductID).ToList();
                if (forDelete.Count > 0)
                {
                    MessageBox.Show("Товар, который присутствует в заказе, удалить нельзя!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    Data.PetShopEntities1.GetContext().Product.Remove(selected);
                    Data.PetShopEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Удалено", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Update();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.Manager.MainFrame.CanGoBack)
            {
                Classes.Manager.MainFrame.GoBack();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.AddEditPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Classes.Manager.MainFrame.Navigate(new Pages.AddEditPage((sender as Button).DataContext as Data.Product));
        }
    }
}
