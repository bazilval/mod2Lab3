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
using System.Windows.Forms;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mod2Lab5
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

        private void ColorChange()
        {
            if (red != null && green != null && blue != null && canvas != null)
            {
                byte redValue = Convert.ToByte(red.Value);
                byte greenValue = Convert.ToByte(green.Value);
                byte blueValue = Convert.ToByte(blue.Value);
                canvas.DefaultDrawingAttributes.Color = Color.FromRgb(redValue, greenValue, blueValue);
                colorRect.Fill = new SolidColorBrush(Color.FromRgb(redValue, greenValue, blueValue));
            }
        }

        private void SliderWheel(Slider slider, MouseWheelEventArgs e)
        {
            int delta = e.Delta / Math.Abs(e.Delta);
            slider.Value = slider.Value + delta * 10;
        }

        private void red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColorChange();
            redBox.Text = Convert.ToString(Math.Round((sender as Slider).Value,0));
        }

        private void red_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            SliderWheel(sender as Slider, e);
        }

        private void green_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            SliderWheel(sender as Slider, e);
        }

        private void blue_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            SliderWheel(sender as Slider, e);
        }

        private void green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColorChange();
            greenBox.Text = Convert.ToString(Math.Round((sender as Slider).Value, 0));
        }

        private void blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ColorChange();
            blueBox.Text = Convert.ToString(Math.Round((sender as Slider).Value, 0));
        }

        private void brushButton_Checked(object sender, RoutedEventArgs e)
        {
            if (eraserButton != null && canvas != null)
            {
                canvas.EditingMode = InkCanvasEditingMode.Ink;
                eraserButton.IsChecked = false;
                selectButton.IsChecked = false;

                brushSize.IsEnabled = true;
                brushSize.Value = canvas.DefaultDrawingAttributes.Width;
            }
        }

        private void eraserButton_Checked(object sender, RoutedEventArgs e)
        {
            canvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            brushButton.IsChecked = false;
            selectButton.IsChecked = false;

            brushSize.IsEnabled = true;
            brushSize.Value = canvas.EraserShape.Width;
            
        }

        private void brushSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (canvas != null)
            {
                if ((bool)brushButton.IsChecked)
                {
                    canvas.DefaultDrawingAttributes.Width = brushSize.Value;
                    canvas.DefaultDrawingAttributes.Height = brushSize.Value;

                }
                else if((bool)eraserButton.IsChecked)
                {
                    canvas.EditingMode = InkCanvasEditingMode.None;
                    canvas.EraserShape = new RectangleStylusShape(brushSize.Value, brushSize.Value);
                    canvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                }
            }
        }

        private void selectButton_Checked(object sender, RoutedEventArgs e)
        {
            canvas.EditingMode = InkCanvasEditingMode.Select;
            brushSize.IsEnabled = false;

            brushButton.IsChecked = false;
            eraserButton.IsChecked = false;
        }

        private void brushSize_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            SliderWheel(sender as Slider, e);
        }

        private void colorRect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                red.Value = colorDialog.Color.R;
                green.Value = colorDialog.Color.G;
                blue.Value = colorDialog.Color.B;
            }
        }


        private void export_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Файл изображения (*.jpg)|*.jpg|Все файлы (*.*)|*.*";
            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //get the dimensions of the ink control
                // int margin = (int)canvas.Margin.Left;
                int width = (int)canvas.ActualWidth;
                int height = (int)canvas.ActualHeight;

                //render ink to bitmap
                RenderTargetBitmap rtb =
                   new RenderTargetBitmap(width, height, 96d, 96d, PixelFormats.Default);
                rtb.Render(canvas);

                //save the ink to a file
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));
                using (FileStream fs = new FileStream(saveDialog.FileName,FileMode.Create))
                {
                    fs.Position = 0;
                    encoder.Save(fs);
                }
            }

        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Файл проекта (*.canvus)|*.canvus|Все файлы (*.*)|*.*";
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (FileStream fs = new FileStream(openDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    StrokeCollection strokes = new StrokeCollection(fs);
                    canvas.Strokes = strokes;
                    fs.Close();
                }
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Файл проекта (*.canvus)|*.canvus|Все файлы (*.*)|*.*";
            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (FileStream fs = new FileStream(saveDialog.FileName, FileMode.Create))
                {
                    canvas.Strokes.Save(fs);
                    fs.Close();
                }
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
