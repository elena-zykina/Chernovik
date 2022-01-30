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
using Chernovik.DataBase;

namespace Chernovik.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddMaterial.xaml
    /// </summary>
    public partial class AddMaterial : Page
    {
        public AddMaterial()
        {
            InitializeComponent();
            var filtItems = Transition.Context.MaterialType.ToList();

            filtItems.Insert(0, new MaterialType { Title = "Выберите тип" });
            MaterialTypeCombo.ItemsSource = filtItems;
            MaterialTypeCombo.SelectedIndex = 0;
        }
        private void DataView()
        {
            var tempDataProduct = Transition.Context.Material.ToList();

            if (MaterialTypeCombo.SelectedIndex > 0)
                tempDataProduct = tempDataProduct.Where(p => p.MaterialTypeID == (MaterialTypeCombo.SelectedItem as MaterialType).ID).ToList();
        }

        private void MaterialTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Material material = new Material()
                {
                    Title = TxtName.Text,
                    MaterialType = MaterialTypeCombo.SelectedItem as MaterialType,
                    Description = TxtDescription.Text,
                    Image = TxtImage.Text,
                    MinCount = Convert.ToInt32(TxtMinCount.Text),
                    CountInStock = Convert.ToInt32(TxtCountInStock.Text),
                    
                };
                Transition.Context.Material.Add(material);
                Transition.Context.SaveChanges();
                MessageBox.Show("Данные успешно добавлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
            Transition.MainFrame.GoBack();
        }
    }
}
