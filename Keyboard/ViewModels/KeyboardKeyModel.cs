namespace Keyboard.ViewModels
{
    public class KeyboardKeyModel
    {
        public string Normal { get; set; }
        public string Shifted { get; set; }
        public string Tag => Normal;
        public string CommandName { get; set; }
    }
}