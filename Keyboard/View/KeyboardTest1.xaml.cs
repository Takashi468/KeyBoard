using System.Windows;
using Keyboard.ViewModels;

namespace Keyboard.View
{
    public partial class KeyboardTest1 : Window
    {
        public KeyboardTest1()
        {
            InitializeComponent();
            this.DataContext = new KeyboardViewModel();
        }
    }
}