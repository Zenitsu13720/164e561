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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        public bool FlagPhoto = false;
        public string FlagAddOrEdit = "default";
        public Data.Product CurrentProduct = new Data.Product();

        public AddEditPage(Data.Product _product)
        {
            InitializeComponent();

            if (_product == null)
            {
                FlagAddOrEdit = "add";
            }
            else
            {
                CurrentProduct = _product;
                FlagAddOrEdit = "edit";
            }

            DataContext = CurrentProduct;

            Init();
        }

        public void Init()
        {
            try
            {
                CategoryComboBox.ItemsSource = Data.PetShopEntities1.GetContext().ProductCategory.ToList();
                if (FlagAddOrEdit == "add")
                {
                    IdTextBox.Visibility = Visibility.Hidden;
                    IdLabel.Visibility = Visibility.Hidden;
                    NameTextBox.Text = string.Empty;
                    CategoryComboBox.SelectedItem = null;
                    //ProductImage - либо через binding, либо через CS
                    UnitTextBox.Text = string.Empty;
                    ProviderTextBox.Text = string.Empty;
                    CostTextBox.Text = string.Empty;
                    QuantityTextBox.Text = string.Empty;
                    DescriptionTextBox.Text = string.Empty;
                }
                else if(FlagAddOrEdit =="edit")
                {
                    IdTextBox.Visibility = Visibility.Visible;
                    IdLabel.Visibility = Visibility.Visible;

                    IdTextBox.Text = CurrentProduct.ProductID.ToString();

                    NameTextBox.Text = CurrentProduct.ProductName.Name;
                    CategoryComboBox.SelectedItem = Data.PetShopEntities1.GetContext().ProductCategory.Where(d => d.ProductCategoryID == CurrentProduct.ProductCategoryID).FirstOrDefault();
                    //ProductImage - либо через binding, либо через CS
                    UnitTextBox.Text = CurrentProduct.Units.Name;
                    ProviderTextBox.Text = CurrentProduct.ProductProvider.ProviderName;
                    CostTextBox.Text = CurrentProduct.ProductCost.ToString();
                    QuantityTextBox.Text = CurrentProduct.ProductQuantityInStock.ToString();
                    DescriptionTextBox.Text = CurrentProduct.ProductDescription;
                }
            }
            catch (Exception)
            {

            }
        }

        private void DecriptionTextBox_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Classes.Manager.MainFrame.CanGoBack)
            {
                Classes.Manager.MainFrame.GoBack();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrEmpty(NameTextBox.Text))
                {
                    errors.AppendLine("Заполните наименование!");
                }
                if (CategoryComboBox.SelectedItem == null)
                {
                    errors.AppendLine("Выберите категорию!");
                }
                if (string.IsNullOrEmpty(QuantityTextBox.Text))
                {
                    errors.AppendLine("Заполните количество!");
                }
                else
                {
                    var tryQuantity = Int32.TryParse(QuantityTextBox.Text, out var resultQuantity);
                    if (!tryQuantity)
                    {
                        errors.AppendLine("Количество - целое число");
                    }
                }
                if (string.IsNullOrEmpty(UnitTextBox.Text))
                {
                    errors.AppendLine("Заполните ед. измерения!");
                }
                if (string.IsNullOrEmpty(ProviderTextBox.Text))
                {
                    errors.AppendLine("Заполните поставщика!");
                }
                if (string.IsNullOrEmpty(CostTextBox.Text))
                {
                    errors.AppendLine("Заполните стоимость!");
                }
                else
                {
                    var tryCost = Decimal.TryParse(CostTextBox.Text, out var resultCost);
                    if (!tryCost)
                    {
                        errors.AppendLine("Стоимость - дробное число");
                    }
                    else
                    {
                        var decimalSeparatorIndex = CostTextBox.Text.IndexOf(",");
                        if (decimalSeparatorIndex != -1 && CostTextBox.Text.Length - decimalSeparatorIndex - 1 > 2)
                        {
                            errors.AppendLine("Стоимость не может содержать более двух знаков после запятой.");
                        }
                    }

                    if (tryCost && resultCost < 0)
                    {
                        errors.AppendLine("Стоимость не может быть отрицательной");
                    }
                }
                if (string.IsNullOrEmpty(DescriptionTextBox.Text))
                {
                    errors.AppendLine("Заполните описание!");
                }

                //обработать фото: ограничение 300х200 пикселей

                if (FlagPhoto == false)
                {
                    errors.AppendLine("Выберите изображение!");
                }


                if (errors.Length > 0)
                {
                MessageBox.Show(errors.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                //логика

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ProductImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //логику открытия окна
            //разрешение 300х200
        }
    }
}
