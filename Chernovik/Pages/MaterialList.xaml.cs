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
    /// Логика взаимодействия для MaterialList.xaml
    /// </summary>
    public partial class MaterialList : Page
    {
        public MaterialList()
        {
            InitializeComponent();

            var filtItems = Transition.Context.MaterialType.ToList();
            filtItems.Insert(0, new MaterialType { Title = "Все элементы" });
            MaterialTypeBox.ItemsSource = filtItems;

            MaterialTypeBox.SelectedIndex = 0;
            SortCBox.SelectedIndex = 0;

            MaterialTypeBox.ItemsSource = Transition.Context.Material.ToList();
        }
        public void DataView()
        {
            var tempDataMaterial = Transition.Context.Material.ToList();

            if (MaterialTypeBox.SelectedIndex > 0)
                tempDataMaterial = tempDataMaterial.Where(p => p.MaterialTypeID == (MaterialTypeBox.SelectedItem as MaterialType).ID).ToList();

            if (SearchMaterialBox.Text != "Введите для поиска" && SearchMaterialBox.Text != null)
                tempDataMaterial = tempDataMaterial
                    .Where(p => p.Title.ToLower().Contains(SearchMaterialBox.Text.ToLower())
                    || p.Description.ToLower().Contains(SearchMaterialBox.Text.ToLower()))
                    .ToList();
            switch (SortCBox.SelectedIndex)
            {
                case 1:
                    {
                        if (!(bool)CheckSort.IsChecked)
                            tempDataMaterial = tempDataMaterial
                                    .OrderBy(p => p.Title)
                                    .ToList();
                        else
                            tempDataMaterial = tempDataMaterial
                                    .OrderByDescending(p => p.Title)
                                    .ToList();

                        ListMaterial.ItemsSource = tempDataMaterial;
                        break;
                    }
                case 2:
                    {
                        if (!(bool)CheckSort.IsChecked)
                            tempDataMaterial = tempDataMaterial
                                    .OrderBy(p => p.CountInStock)
                                    .ToList();
                        else
                            tempDataMaterial = tempDataMaterial
                                    .OrderByDescending(p => p.CountInStock)
                                    .ToList();

                        ListMaterial.ItemsSource = tempDataMaterial;
                        break;
                    }
                case 3:
                    {
                        if (!(bool)CheckSort.IsChecked)
                            tempDataMaterial = tempDataMaterial
                                    .OrderBy(p => p.Cost)
                                    .ToList();
                        else
                            tempDataMaterial = tempDataMaterial
                                    .OrderByDescending(p => p.Cost)
                                    .ToList();

                        ListMaterial.ItemsSource = tempDataMaterial;
                        break;
                    }
            }
            ListMaterial.ItemsSource = tempDataMaterial;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Transition.Context.ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DataView();
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SortCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView();
        }

        private void SearchMaterialBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchMaterialBox.Text != "Введите для поиска")
                DataView();
        }

        private void SearchMaterialBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchMaterialBox.Text))
                SearchMaterialBox.Text = "Введите для поиска";
        }

        private void SearchMaterialBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchMaterialBox.Text = null;
        }

        private void CheckSort_Checked(object sender, RoutedEventArgs e)
        {
            DataView();
        }

        private void CheckSort_Unchecked(object sender, RoutedEventArgs e)
        {
            DataView();
        }

        private void MaterialTypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataView();
        }

        private void ListMaterial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListMaterial.SelectedItems.Count > 0)
            {
                BtnDelete.Visibility = Visibility.Visible;
                BtnEdit.Visibility = Visibility.Visible;
            }
            else
            {
                BtnDelete.Visibility = Visibility.Hidden;
                BtnEdit.Visibility = Visibility.Hidden;
            }
        }
    }
}
