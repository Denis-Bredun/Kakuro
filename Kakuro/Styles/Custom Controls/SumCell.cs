using Kakuro.Game_Tools;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kakuro.Styles.Custom_Controls
{
    // #BAD: tests shall be written
    public class SumCell : Control
    {
        static SumCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SumCell), new FrameworkPropertyMetadata(typeof(SumCell)));
        }

        public string SumRight
        {
            get { return (string)GetValue(SumRightProperty); }
            set { SetValue(SumRightProperty, value); }
        }

        public static readonly DependencyProperty SumRightProperty =
            DependencyProperty.Register("SumRight", typeof(string), typeof(SumCell), new PropertyMetadata(string.Empty));

        public string SumBottom
        {
            get { return (string)GetValue(SumBottomProperty); }
            set { SetValue(SumBottomProperty, value); }
        }

        public static readonly DependencyProperty SumBottomProperty =
            DependencyProperty.Register("SumBottom", typeof(string), typeof(SumCell), new PropertyMetadata(string.Empty));

        public SumCell()
        {
            Focusable = true;
            IsTabStop = true;
            KeyDown += SumCell_KeyDown;
        }

        private void SumCell_KeyDown(object sender, KeyEventArgs e)
        {
            FocusNavigationHelper.HandleWASDKeyDown(this, e);
        }
    }
}
