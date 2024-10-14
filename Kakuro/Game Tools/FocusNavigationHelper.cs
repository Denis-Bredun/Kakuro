using System.Windows;
using System.Windows.Input;

namespace Kakuro.Game_Tools
{
    public static class FocusNavigationHelper
    {
        public static void HandleWASDKeyDown(UIElement currentElement, KeyEventArgs e)
        {
            if (!IsWASDKey(e.Key)) return;

            TraversalRequest request = GetTraversalRequest(e.Key);
            if (request != null)
            {
                currentElement.MoveFocus(request);
                e.Handled = true;
            }
        }

        private static TraversalRequest GetTraversalRequest(Key key)
        {
            return key switch
            {
                Key.W => new TraversalRequest(FocusNavigationDirection.Up),
                Key.A => new TraversalRequest(FocusNavigationDirection.Left),
                Key.S => new TraversalRequest(FocusNavigationDirection.Down),
                Key.D => new TraversalRequest(FocusNavigationDirection.Right),
                _ => throw new ArgumentException()
            };
        }

        private static bool IsWASDKey(Key key) => key == Key.W || key == Key.A || key == Key.S || key == Key.D;
    }

}
