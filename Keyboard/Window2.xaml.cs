using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Media;

namespace Keyboard
{
    public partial class Window2 : Window
    {
        private bool isShift = false;
        private bool isCapsLock = false;

        public static Control TargetControl;

        public Window2()
        {
            InitializeComponent();

            // ย้ายไปด้านล่างของจอ และอยู่หน้าสุด
            Loaded += (s, e) =>
            {
                this.Top = SystemParameters.WorkArea.Bottom - this.Height;
                this.Left = 0;
            };
        }

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child is T typedChild)
                {
                    yield return typedChild;
                }

                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // logic กำหนด TargetControl ถ้ามี หรือจะกำหนดจาก outside
        }

        private void Key_Click(object sender, RoutedEventArgs e)
        {
            if (TargetControl is TextBox textbox)
            {
                var btn = sender as Button;
                var rawKey = btn?.Content.ToString() ?? "";

                if (!string.IsNullOrEmpty(rawKey))
                {
                    string finalKey = (isShift ^ isCapsLock) ? rawKey.ToUpper() : rawKey;
                    int caretIndex = textbox.CaretIndex;
                    textbox.Text = textbox.Text.Insert(caretIndex, finalKey);
                    textbox.CaretIndex = caretIndex + finalKey.Length;

                    if (isShift)
                    {
                        isShift = false;
                        UpdateKeyVisuals();
                    }
                }
            }
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (TargetControl is TextBox textbox && textbox.CaretIndex > 0)
            {
                int caret = textbox.CaretIndex;
                textbox.Text = textbox.Text.Remove(caret - 1, 1);
                textbox.CaretIndex = caret - 1;
            }
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (TargetControl is TextBox textbox)
            {
                int caret = textbox.CaretIndex;
                textbox.Text = textbox.Text.Insert(caret, "\n");
                textbox.CaretIndex = caret + 1;
            }
        }

        private void Space_Click(object sender, RoutedEventArgs e)
        {
            if (TargetControl is TextBox textbox)
            {
                int caret = textbox.CaretIndex;
                textbox.Text = textbox.Text.Insert(caret, " ");
                textbox.CaretIndex = caret + 1;
            }
        }

        private void Fullstop_Click(object sender, RoutedEventArgs e)
        {
            if (TargetControl is TextBox textbox)
            {
                int caret = textbox.CaretIndex;
                textbox.Text = textbox.Text.Insert(caret, ".");
                textbox.CaretIndex = caret + 1;
            }
        }

        private void Shift_Click(object sender, RoutedEventArgs e)
        {
            isShift = !isShift;
            UpdateKeyVisuals();
        }

        private void CapsLock_Click(object sender, RoutedEventArgs e)
        {
            isCapsLock = !isCapsLock;
            UpdateKeyVisuals();
        }

        private void Special_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Special characters layout toggle (ยังไม่ทำ)");
        }

        private void UpdateKeyVisuals()
        {
            bool isUpper = isShift ^ isCapsLock;

            foreach (var btn in FindVisualChildren<Button>(this))
            {
                if (btn.Tag is string tag && tag.Length == 1 && char.IsLetter(tag[0]))
                {
                    btn.Content = isUpper ? tag.ToUpper() : tag.ToLower();
                }
            }

            //foreach (var child in LogicalTreeHelper.GetChildren(this))
            //{
            //    if (child is Panel panel)
            //    {
            //        foreach (var btn in panel.Children)
            //        {
            //            if (btn is Button button && button.Tag is string tag)
            //            {
            //                if (tag.Length == 1 && char.IsLetter(tag[0]))
            //                {
            //                    button.Content = isUpper ? tag.ToUpper() : tag.ToLower();
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
}
