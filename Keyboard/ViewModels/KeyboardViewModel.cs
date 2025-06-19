using System.Collections.ObjectModel;
using System.Windows.Input;
using Keyboard.Helpers;

namespace Keyboard.ViewModels
{
    public class KeyboardViewModel
    {
        public ObservableCollection<KeyboardKeyModel> NumberRowKeys { get; }
        public ObservableCollection<KeyboardKeyModel> QwertyRowKeys { get; }
        public ObservableCollection<KeyboardKeyModel> ControlRowKeys { get; }

        public ICommand KeyPressCommand { get; }

        private bool isShift = false;
        private bool isCapsLock = false;

        public KeyboardViewModel()
        {
            KeyPressCommand = new RelayCommand<string>(OnKeyPressed);

            NumberRowKeys = new ObservableCollection<KeyboardKeyModel>
            {
                new() { Normal = "1", Shifted = "!" },
                new() { Normal = "2", Shifted = "@" },
                new() { Normal = "3", Shifted = "#" },
                new() { Normal = "4", Shifted = "$" },
                new() { Normal = "5", Shifted = "%" },
                new() { Normal = "6", Shifted = "^" },
                new() { Normal = "7", Shifted = "&" },
                new() { Normal = "8", Shifted = "*" },
                new() { Normal = "9", Shifted = "(" },
                new() { Normal = "0", Shifted = ")" },
            };

            QwertyRowKeys = new ObservableCollection<KeyboardKeyModel>
            {
                new() { Normal = "q" }, new() { Normal = "w" }, new() { Normal = "e" },
                new() { Normal = "r" }, new() { Normal = "t" }, new() { Normal = "y" },
                new() { Normal = "u" }, new() { Normal = "i" }, new() { Normal = "o" }, new() { Normal = "p" }
            };

            ControlRowKeys = new ObservableCollection<KeyboardKeyModel>
            {
                new() { Normal = "Shift", CommandName = "Shift" },
                new() { Normal = "Caps", CommandName = "Caps" },
                new() { Normal = "Back", CommandName = "Back" },
                new() { Normal = "Enter", CommandName = "Enter" },
                new() { Normal = "Space", CommandName = "Space" }
            };
        }

        private void OnKeyPressed(string key)
        {
            if (key == "Shift") { isShift = !isShift; return; }
            if (key == "Caps") { isCapsLock = !isCapsLock; return; }
            if (key == "Back") { KeyboardHelper.Backspace(); return; }
            if (key == "Enter") { KeyboardHelper.Enter(); return; }
            if (key == "Space") { KeyboardHelper.Insert(" "); return; }

            string finalKey = key;
            if (key.Length == 1 && char.IsLetter(key[0]))
                finalKey = (isCapsLock ^ isShift) ? key.ToUpper() : key.ToLower();

            KeyboardHelper.Insert(finalKey);
            isShift = false;
        }
    }
}