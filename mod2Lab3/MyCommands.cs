using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mod2Lab3
{
    class MyCommands
    {
        public static RoutedUICommand Exit { get; set; }

        static MyCommands()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.T, ModifierKeys.Control, "Ctrl+T"));
            Exit = new RoutedUICommand("_Выход", nameof(Exit), typeof(MyCommands), inputs);
        }
    }
}
