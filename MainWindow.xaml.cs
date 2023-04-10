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
            if (toggle_on_stock.IsChecked == true) user_toy.on_stock = true;
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
        private void ChangeButtonStyleInGrid(string style, string menuStyle, Brush textColor, Brush textBoxColor, Brush backgroundTextBoxColor)
        {
            var grid = (Grid)FindName("Base");

            foreach (var child in GetAllChildren(grid))
            {
                if (child is Button button)
                {
                    button.Style = (Style)FindResource(style);
                }
                else if (child is TextBlock textBlock)
                {
                    textBlock.Foreground = textColor;
                }
                else if (child is TextBox textBox)
                {
                    textBox.Foreground = textBoxColor;
                    textBox.Background = backgroundTextBoxColor;
                }
                else if (child is Menu menu)
                {
                    menu.Style = (Style)FindResource(menuStyle);
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

        private void SakuraPalette()
        {
            Brush textColor = (Brush)new BrushConverter().ConvertFrom("#4D4D4D");
            Brush textBoxColor = (Brush)new BrushConverter().ConvertFrom("#F0F1F1");
            Brush backgroundTextBoxColor = (Brush)new BrushConverter().ConvertFrom("#755345");
            Brush basedPeach = (Brush)new BrushConverter().ConvertFrom("#FFDCB8");
            Brush rectBrown = (Brush)new BrushConverter().ConvertFrom("#755345");
            Brush titleWhite = (Brush)new BrushConverter().ConvertFrom("#F0F1F1");
            Brush buttonPink = (Brush)new BrushConverter().ConvertFrom("#FF99E2");
            Result_Listbox.Background = basedPeach;
            Base.Background = basedPeach;
            ChangeButtonStyleInGrid("SakuraPaletteButton", "SakuraPaletteMenu", textColor, textBoxColor, backgroundTextBoxColor);
            Title.Foreground = titleWhite;
            Upper.Fill = rectBrown;
            Lower.Fill = rectBrown;
            toggle_on_stock.Background = buttonPink;
            toggle_on_stock.Foreground = textBoxColor;
            Result_Listbox.Foreground = textColor;
            
        }

        private void GreenPalette()
        {
            Brush textColor = (Brush)new BrushConverter().ConvertFrom("#606060");
            Brush textBoxColor = (Brush)new BrushConverter().ConvertFrom("#606060");
            Brush backgroundTextBoxColor = (Brush)new BrushConverter().ConvertFrom("#E2FA99");
            Brush basedGreen = (Brush)new BrushConverter().ConvertFrom("#95FDBF");
            Brush rectGreen = (Brush)new BrushConverter().ConvertFrom("#86E38C");
            Brush titleWhite = (Brush)new BrushConverter().ConvertFrom("#606060");
            Brush buttonGreen = (Brush)new BrushConverter().ConvertFrom("#E2FA99");
            Result_Listbox.Background = basedGreen;
            Base.Background = basedGreen;
            ChangeButtonStyleInGrid("GreenPaletteButton", "GreenPaletteMenu", textColor, textBoxColor, backgroundTextBoxColor);
            Title.Foreground = titleWhite;
            Upper.Fill = rectGreen;
            Lower.Fill = rectGreen;
            toggle_on_stock.Background = buttonGreen;
            toggle_on_stock.Foreground = textBoxColor;
            Result_Listbox.Foreground = textColor;

        }

        private void PorschePalette()
        {
            Brush textColor = (Brush)new BrushConverter().ConvertFrom("#291F08");
            Brush textBoxColor = (Brush)new BrushConverter().ConvertFrom("#291F08");
            Brush backgroundTextBoxColor = (Brush)new BrushConverter().ConvertFrom("#2E88E6");
            Brush basedBlue = (Brush)new BrushConverter().ConvertFrom("#2ED3E6");
            Brush rectOrange = (Brush)new BrushConverter().ConvertFrom("#F24822");
            Brush titleBlack = (Brush)new BrushConverter().ConvertFrom("#291F08");
            Brush buttonWhite = (Brush)new BrushConverter().ConvertFrom("#F2F2F2");
            Result_Listbox.Background = basedBlue;
            Base.Background = basedBlue;
            ChangeButtonStyleInGrid("PorschePaletteButton", "PorschePaletteMenu", textColor, textBoxColor, backgroundTextBoxColor);
            Title.Foreground = titleBlack;
            Upper.Fill = rectOrange;
            Lower.Fill = rectOrange;
            toggle_on_stock.Background = buttonWhite;
            toggle_on_stock.Foreground = textBoxColor;
            Result_Listbox.Foreground = textColor;

        }

        private void BananaFishPalette()
        {
            Brush textColor = (Brush)new BrushConverter().ConvertFrom("#36290B");
            Brush textBoxColor = (Brush)new BrushConverter().ConvertFrom("#36290B");
            Brush backgroundTextBoxColor = (Brush)new BrushConverter().ConvertFrom("#F0E900");
            Brush basedColor = (Brush)new BrushConverter().ConvertFrom("#DCD39E");
            Brush rectColor = (Brush)new BrushConverter().ConvertFrom("#F0BC00");
            Brush titleColor = (Brush)new BrushConverter().ConvertFrom("#36290B");
            Brush buttonColor = (Brush)new BrushConverter().ConvertFrom("#F8E50C");
            Result_Listbox.Background = basedColor;
            Base.Background = basedColor;
            ChangeButtonStyleInGrid("BananaFishPaletteButton", "BananaFishPaletteMenu", textColor, textBoxColor, backgroundTextBoxColor);
            Title.Foreground = titleColor;
            Upper.Fill = rectColor;
            Lower.Fill = rectColor;
            toggle_on_stock.Background = buttonColor;
            toggle_on_stock.Foreground = textBoxColor;
            Result_Listbox.Foreground = textColor;

        }
        private void StyleBox_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            StackPanel selectedStackPanel = comboBox.SelectedItem as StackPanel;
            if (selectedStackPanel != null && selectedStackPanel.Name == "SakuraCircles")
            {
                SakuraPalette();
            }
            else if (selectedStackPanel != null && selectedStackPanel.Name == "GreenCircles")
            {
                GreenPalette();
            }
            else if (selectedStackPanel != null && selectedStackPanel.Name == "PorscheCircles")
            {
                PorschePalette();
            }
            else if (selectedStackPanel != null && selectedStackPanel.Name == "BananaFishCircles")
            {
                BananaFishPalette();
            }
        }

        public void ColorizeElements(Brush textColor, Brush textBoxColor, Brush backgroundTextBoxColor)
        {
            // получаем доступ к StackPanel и Grid по имени
            //StackPanel stackPanel = (StackPanel)Application.Current.MainWindow.FindName("stackPanelName");
            Grid grid = (Grid)Application.Current.MainWindow.FindName("Base");
            foreach (var element in grid.Children)
            {
                if (element is TextBlock textBlock)
                {
                    textBlock.Foreground = textColor;
                }
                else if (element is TextBox textBox)
                {
                    textBox.Foreground = textBoxColor;
                    textBox.Background = backgroundTextBoxColor;
                }
            }
        }
    }
}
