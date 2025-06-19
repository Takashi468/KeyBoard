using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Keyboard
{
    public partial class Window1 : Window

    {
        public static Control TargetControl; // ใช้ร่วมได้ทั้ง TextBox และ PasswordBox
        private readonly List<KeyboardKey> allKeys = new();


        private bool isShift = false;
        private bool isCapsLock = false;

        private Button shiftButton;
        private Button capsLockButton;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var screenHeight = SystemParameters.PrimaryScreenHeight;
            var screenWidth = SystemParameters.PrimaryScreenWidth;

            this.Left = 0;
            this.Top = screenHeight - this.Height; // ชิดขอบล่างของหน้าจอ
        }


        // Map สัญลักษณ์เมื่อกด Shift
        private readonly Dictionary<string, string> shiftSymbols = new()
        {
            { "1", "!" }, { "2", "@" }, { "3", "#" }, { "4", "$" }, { "5", "%" },
            { "6", "^" }, { "7", "&" }, { "8", "*" }, { "9", "(" }, { "0", ")" },
            { "`", "~" }, { "-", "_" }, { "=", "+" }, { "[", "{" }, { "]", "}" },
            { "\\", "|" }, { ";", ":" }, { "'", "\"" }, { ",", "<" }, { ".", ">" },
            { "/", "?" }
        };

        public Window1()
        {
            InitializeComponent();
            RegisterKeys();
        }

        private void Key_Click(object sender, RoutedEventArgs e)
        {
            if (TargetControl == null) return;

            var btn = sender as Button;
            string key = btn.Tag?.ToString() ?? btn.Content?.ToString();
            if (string.IsNullOrEmpty(key)) return;

            // ตรวจสอบว่าคือตัวอักษร
            if (key.Length == 1 && char.IsLetter(key[0]))
            {
                key = (isCapsLock ^ isShift) ? key.ToUpper() : key.ToLower();
            }
            else if (isShift && shiftSymbols.ContainsKey(key))
            {
                key = shiftSymbols[key];
            }

            InsertText(key);

            // ปิด Shift หลังใช้
            if (isShift)
            {
                isShift = false;
                if (shiftButton != null)
                    shiftButton.Background = (Brush)new BrushConverter().ConvertFrom("#F2F2F2");
            }

            
        }

        private void InsertText(string key)
        {
            switch (TargetControl)
            {
                case TextBox tb:
                    tb.Text += key;
                    tb.CaretIndex = tb.Text.Length;
                    tb.Focus();
                    break;
                case PasswordBox pb:
                    pb.Password += key;
                    pb.Focus();
                    break;
            }
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            switch (TargetControl)
            {
                case TextBox tb when tb.Text.Length > 0:
                    tb.Text = tb.Text[..^1];
                    tb.CaretIndex = tb.Text.Length;
                    break;
                case PasswordBox pb when pb.Password.Length > 0:
                    pb.Password = pb.Password[..^1];
                    break;
            }
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            SendKey(Key.Enter);
        }

        private void Tab_Click(object sender, RoutedEventArgs e)
        {
            SendKey(Key.Tab);
        }

        private void Space_Click(object sender, RoutedEventArgs e)
        {
            InsertText(" ");
        }

        //private void Shift_Click(object sender, RoutedEventArgs e)
        //{
        //    isShift = !isShift;
        //    shiftButton = sender as Button;
        //    shiftButton.Background = isShift
        //        ? Brushes.LightBlue
        //        : (Brush)new BrushConverter().ConvertFrom("#F2F2F2");
        //}

        //private void CapsLock_Click(object sender, RoutedEventArgs e)
        //{
        //    isCapsLock = !isCapsLock;
        //    capsLockButton = sender as Button;
        //    capsLockButton.Background = isCapsLock
        //        ? Brushes.LightBlue
        //        : (Brush)new BrushConverter().ConvertFrom("#F2F2F2");
        //}

        private void SendKey(Key key)
        {
            var target = System.Windows.Input.Keyboard.FocusedElement;
            var e = new KeyEventArgs(System.Windows.Input.Keyboard.PrimaryDevice, PresentationSource.FromVisual(this), 0, key)
            {
                RoutedEvent = System.Windows.Input.Keyboard.KeyDownEvent
            };
            Debug.WriteLine(e);
        }

        public class KeyboardKey
        {
            public Button Button { get; set; }
            public string Normal { get; set; }    // ตัวอักษรปกติ เช่น a
            public string Shifted { get; set; }   // ตัวอักษรที่เปลี่ยนเมื่อ Shift เช่น A หรือ @
        }


        private void UpdateKeyVisuals()
        {
            foreach (var child in GetAllButtons(this))
            {
                if (child.Content is string str && str.Length == 1 && char.IsLetter(str[0]))
                {
                    // ปุ่มแบบ Content="a"
                    child.Content = (isCapsLock ^ isShift)
                        ? str.ToUpper()
                        : str.ToLower();
                }
                else if (child.Content is StackPanel panel)
                {
                    string key = child.Tag?.ToString();

                    // ถ้าเป็น key เดี่ยว เช่น '1', ';' เป็นต้น
                    if (!string.IsNullOrEmpty(key))
                    {
                        string mainSymbol = (isShift && shiftSymbols.ContainsKey(key))
                            ? shiftSymbols[key]
                            : key;

                        // หา TextBlock ที่มี FontSize ใหญ่ (ตัวหลัก)
                        foreach (var item in panel.Children)
                        {
                            if (item is TextBlock tb && tb.FontSize >= 16)
                            {
                                tb.Text = mainSymbol;
                            }
                        }
                    }
                }
            }
        }

        private IEnumerable<Button> GetAllButtons(DependencyObject parent)
        {
            if (parent == null) yield break;

            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is Button button)
                {
                    yield return button;
                }

                foreach (var descendant in GetAllButtons(child))
                {
                    yield return descendant;
                }
            }
        }


        private void RegisterKeys()
        {
            foreach (var child in LogicalTreeHelper.GetChildren(this))
            {
                RegisterButtonsRecursive(child);
            }
        }

        private void RegisterButtonsRecursive(object obj)
        {
            if (obj is Button btn)
            {
                string normal = btn.Content?.ToString() ?? btn.Tag?.ToString();
                if (!string.IsNullOrEmpty(normal) && normal.Length == 1 && char.IsLetter(normal[0]))
                {
                    allKeys.Add(new KeyboardKey
                    {
                        Button = btn,
                        Normal = normal.ToLower(),
                        Shifted = normal.ToUpper()
                    });
                }
            }
            else if (obj is Panel panel)
            {
                foreach (var child in panel.Children)
                    RegisterButtonsRecursive(child);
            }
        }


        private void Shift_Click(object sender, RoutedEventArgs e)
        {
            isShift = !isShift;
            shiftButton = sender as Button;
            shiftButton.Background = isShift
                ? Brushes.LightBlue
                : (Brush)new BrushConverter().ConvertFrom("#F2F2F2");

            UpdateKeyVisuals(); 
        }

        private void CapsLock_Click(object sender, RoutedEventArgs e)
        {
            isCapsLock = !isCapsLock;
            capsLockButton = sender as Button;
            capsLockButton.Background = isCapsLock
                ? Brushes.LightBlue
                : (Brush)new BrushConverter().ConvertFrom("#F2F2F2");

            UpdateKeyVisuals(); 
        }


    }
}
