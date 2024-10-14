using Kakuro.Game_Tools;
using System.Windows;
using System.Windows.Input;

namespace Kakuro.Styles
{
    partial class Elements_Template : ResourceDictionary
    {
        public Elements_Template()
        {
            InitializeComponent();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            FocusNavigationHelper.HandleWASDKeyDown((UIElement)sender, e);
        }
    }
}
