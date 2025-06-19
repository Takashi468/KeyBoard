using System.Windows;
using System.Windows.Controls;

namespace Keyboard.Helpers
{
    public static class KeyboardHelper
    {
        public static Control TargetControl;

        public static void Insert(string key)
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

        public static void Backspace()
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

        public static void Enter() => Insert("\n");
    }
}