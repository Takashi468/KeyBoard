using System.Windows;
using System.Windows.Controls;

namespace Keyboard
{
    public partial class MainWindow : Window
    {
        private Window2 keyboardWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_Username_GotFocus(object sender, RoutedEventArgs e)
        {
            // กำหนดให้คีย์บอร์ดพิมพ์ลง TextBox นี้
            //Window2.TargetControl = sender as Control;



            //if (keyboardWindow == null || !keyboardWindow.IsVisible)
            //{
            //    keyboardWindow = new Window2
            //    {
            //        Owner = this,
            //        WindowStartupLocation = WindowStartupLocation.Manual,
            //        Top = this.Top + this.Height,
            //        Left = this.Left + 20
            //    };
            //    keyboardWindow.Show();
            //}
            // กำหนดให้คีย์บอร์ดพิมพ์ลง TextBox นี้
            Window2.TargetControl = sender as Control;

            if (keyboardWindow == null || !keyboardWindow.IsVisible)
            {
                keyboardWindow = new Window2
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    Left = this.Left + 20
                };


                double desiredTop = this.Top + this.Height;
                double screenBottom = SystemParameters.WorkArea.Bottom;
                double keyboardHeight = keyboardWindow.Height;

                if (desiredTop + keyboardHeight > screenBottom)
                {
                    keyboardWindow.Top = this.Top - keyboardHeight;
                }
                else
                {
                    keyboardWindow.Top = desiredTop;
                }


                keyboardWindow.Show();
            }

        }

        private void TextBox_Username_LostFocus(object sender, RoutedEventArgs e)
        {
            // ปิดคีย์บอร์ดเมื่อละจาก TextBox (จะใช้ Hide หรือ Close ก็ได้)
            if (keyboardWindow != null && keyboardWindow.IsVisible)
            {
                keyboardWindow.Hide();
            }
        }
    }
}
