﻿using System;
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
using System.Windows.Threading;

namespace LR07_C121_SavolaynenDmitriy
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        public string age_out = "Младенец";
        public string discount_out = "нет";
        public bool bolbol = false;
        public struct Toy_info
        {
            public string name;
            public string fabric_name;
            public string age;
            public string date;
            public string price;
            public string discount;
            public bool on_stock;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Current_date_textblock.Text = DateTime.Now.ToString("dd:MM:yyyy");
            Current_time_textblock.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Clear_btn_Click(object sender, RoutedEventArgs e)
        {
            Result_Listbox.Items.Clear();
        }

        private void Output_btn_Click(object sender, RoutedEventArgs e)
        {
            string result;
            Toy_info user_toy = new Toy_info();
            user_toy.name = toy_name_textbox.Text.ToString();
            user_toy.fabric_name = fabric_name_textbox.Text.ToString();
            user_toy.date = date_toy.Text.ToString();
            user_toy.price = toy_price_textbox.Text.ToString();
            if(toggle_on_stock.IsChecked == true) user_toy.on_stock = true;
            else user_toy.on_stock = false;
            user_toy.age = age_out;
            user_toy.discount = discount_out;
            double discouted_price = price_with_discount(user_toy.discount, int.Parse(user_toy.price));
            if (user_toy.on_stock) result = "Игрушка " + user_toy.name + " изготовлена на " + user_toy.fabric_name + ". Играть может " + user_toy.age + ". Изготовлена " + user_toy.date + ". Стоимость:" + discouted_price + " (" + user_toy.price + ")" + ". Есть в наличии на складе.";
            else result = "Игрушка " + user_toy.name + " изготовлена на " + user_toy.fabric_name + ". Играть может " + user_toy.age + ". Изготовлена " + user_toy.date + ". Стоимость:" + discouted_price + " (" + user_toy.price + ")" + ". Нет в наличии на складе.";
            Result_Listbox.Items.Add(result);
        }
        private double price_with_discount(string discount, double price)
        {
            switch (discount)
            {
                case "3%":
                    return price - (price * 0.03);
                case "5%":
                    return price - (price * 0.05);
                case "10%":
                    return price - (price * 0.1);
                case "15%":
                    return price - (price * 0.15);
                default:
                    return price;
            }
        }

        private void age_low_underschool_Checked(object sender, RoutedEventArgs e)
        {
            age_out = age_low_underschool.Content.ToString();
        }

        private void age_high_underschool_Checked(object sender, RoutedEventArgs e)
        {
            age_out = age_high_underschool.Content.ToString();
        }

        private void age_low_school_Checked(object sender, RoutedEventArgs e)
        {
            age_out = age_low_school.Content.ToString();
        }

        private void age_school_Checked(object sender, RoutedEventArgs e)
        {
            age_out = age_school.Content.ToString();
        }

        private void combo_box_mouseEnterLeave(object sender, MouseEventArgs e)
        {
            discount_out = discount_combo.Text;
        }
        private void combo_box_title_size_enterleave(object sender, MouseEventArgs e)
        {
            if (FontSize_title.Text != "")
            {
                Title.FontSize = int.Parse(FontSize_title.Text);
            }
        }
        private void ChangeTitleFontPeculiarities(object sender, MouseEventArgs e)
        {
            if (Title_bold.IsChecked == true)
            {
                Title.FontWeight = FontWeights.Bold;
            }
            else
            {
                Title.FontWeight = FontWeights.Normal;
            }
            if (Title_italic.IsChecked == true)
            {
                Title.FontStyle = FontStyles.Italic;
            }
            else
            {
                Title.FontStyle = FontStyles.Normal;
            }
            if (Title_unreline.IsChecked == true)
            {
                Title.TextDecorations = TextDecorations.Underline;
            }
            else
            {
                Title.TextDecorations = null;
            }
        }
        private void ChangeToSakuraPalette(object sender, MouseEventArgs e)
        {
            if (ComboBox.SelectedItemProperty.Name == "SakuraCircles")
            {
                
            }
        }
        private void ChangeButtonStyleInGrid(string style)
        {
            var grid = (Grid)FindName("Base");

            foreach (var child in GetAllChildren(grid))
            {
                if (child is Button button)
                {
                    button.Style = (Style)FindResource(style);
                }
            }
        }

        private IEnumerable<DependencyObject> GetAllChildren(DependencyObject parent)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                yield return child;
                foreach (var grandChild in GetAllChildren(child))
                {
                    yield return grandChild;
                }
            }
        }

        private void StyleBox_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            StackPanel selectedStackPanel = comboBox.SelectedItem as StackPanel;
            if (selectedStackPanel != null && selectedStackPanel.Name == "SakuraCircles")
            {
                Brush basedBlue = (Brush) new BrushConverter().ConvertFrom("#FFDCB8");
                Brush rectBrown = (Brush)new BrushConverter().ConvertFrom("#755345");
                Result_Listbox.Background = basedBlue;
                Base.Background = basedBlue;
                ChangeButtonStyleInGrid("SakuraPaletteButton");
                Upper.Fill = rectBrown;
                Lower.Fill = rectBrown;
            }
        }
    }
}
