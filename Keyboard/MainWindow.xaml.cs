using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Keyboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window1 keyboardWindow;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_Username_GotFocus(object sender, RoutedEventArgs e)
        {
            // กำหนดเป้าให้คีย์บอร์ดรู้ว่าจะพิมพ์ลง TextBox ไหน
            Window1.TargetControl = sender as Control;

            if (keyboardWindow == null || !keyboardWindow.IsVisible)
            {
                keyboardWindow = new Window1
                {
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.Manual,
                    Top = this.Top + this.Height,   // แสดงล่างหน้าต่างหลัก
                    Left = this.Left + 20           // ปรับตำแหน่งตามต้องการ
                };
                keyboardWindow.Show();
            }
        }

        private void TextBox_Username_LostFocus(object sender, RoutedEventArgs e)
        {
            // ปิดคีย์บอร์ดเมื่อออกจาก TextBox (optional)
            if (keyboardWindow != null && keyboardWindow.IsVisible)
            {
                keyboardWindow.Hide(); // หรือ .Close() ถ้าต้องการ destroy
            }
        }

        private void TextBox_Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ยังไม่จำเป็นต้องทำอะไรตรงนี้
        }
    }
}