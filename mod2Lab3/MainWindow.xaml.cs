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

namespace mod2Lab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FontFamily font = ((sender as ComboBox).SelectedItem as TextBlock).FontFamily;
            if (textBox != null)
            {
                textBox.FontFamily = font;
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            int textSize = Int32.Parse(((sender as ComboBox).SelectedItem as TextBlock).Text);
            if (textBox != null)
            {
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

            if (textBox != null)
            {
                textBox.Foreground = Brushes.Black;
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            if (textBox != null)
            {
                textBox.Foreground = Brushes.Red;
            }
        }
    }
}