using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Keyboard
{
    internal class KeyboardKey
    {
        public Button Button { get; set; }
        public string Normal { get; set; }    // ตัวอักษรปกติ เช่น a
        public string Shifted { get; set; }   // ตัวอักษรที่เปลี่ยนเมื่อ Shift เช่น A หรือ @
    }
}
