using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace mod2Lab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string textCash;
        public MainWindow()
        {
            InitializeComponent();
            LightMenuItem.IsChecked = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (textBox != null && fontBox != null)
            {
                string fontName = (sender as ComboBox).SelectedItem as String;
                FontFamily font = new FontFamily(fontName);
                textBox.FontFamily = font;
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

            if (textBox != null && sizeBox != null)
            {
                int textSize = Int32.Parse((sender as ComboBox).SelectedItem as String);
                textBox.FontSize = textSize;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FontWeight bold = FontWeights.Bold;
            FontWeight normal = FontWeights.Normal;

            if (textBox.FontWeight == normal)
            {
                textBox.FontWeight = bold;
            }
            else
            {
                textBox.FontWeight = normal;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FontStyle italic = FontStyles.Italic;
            FontStyle normal = FontStyles.Normal;

            if (textBox.FontStyle == normal)
            {
                textBox.FontStyle = italic;
            }
            else
            {
                textBox.FontStyle = normal;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (textBox.TextDecorations == null)
            {
                textBox.TextDecorations = TextDecorations.Underline;
            }
            else
            {
                textBox.TextDecorations = null;
            }

        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Brush br = Brushes.Black;
            if (textBox != null)
            {
                switch ((sender as RadioButton).Content)
                {
                    case "Чёрный":
                        br = Brushes.Black;
                        break;
                    case "Белый":
                        br = Brushes.White;
                        break;
                    case "Красный":
                        br = Brushes.Red;
                        break;
                }
                {
                    textBox.Foreground = br;
                }

            }
        }
        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (openDialog.ShowDialog() == true)
            {
                textBox.Text = File.ReadAllText(openDialog.FileName);
                textCash = textBox.Text;
            }
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (saveDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveDialog.FileName, textBox.Text);
                textCash = textBox.Text;
            }
        }

        private void SaveCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (textCash == textBox.Text)
            {
                e.CanExecute = false;
            }
            else
                e.CanExecute = true;
        }

        private void ExitExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {

            if (((sender as MenuItem).Header as string) == "Светлая тема")
            {
                DarkMenuItem.IsChecked = false;
                textBox.Foreground = Brushes.Black;
                
            }
            else
            {
                LightMenuItem.IsChecked = false;
                textBox.Foreground = Brushes.Wheat;
            }

            ThemeChange((sender as MenuItem).Header as string);
        }

        private void ThemeChange(string themeName)
        {
            Uri ThemeUri = new Uri("LightTheme.xaml", UriKind.Relative);
            if (themeName == "Тёмная тема")
            {
                ThemeUri = new Uri("DarkTheme.xaml", UriKind.Relative);
            }

            Uri FontUri = new Uri("FontsDictionary.xaml", UriKind.Relative);
            ResourceDictionary FontsResource = Application.LoadComponent(FontUri) as ResourceDictionary;
            ResourceDictionary ThemeResource = Application.LoadComponent(ThemeUri) as ResourceDictionary;
            
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(FontsResource);
            Application.Current.Resources.MergedDictionaries.Add(ThemeResource);
        }
    }
}
