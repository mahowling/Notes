using Notes.Wpf.UI.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notes.Wpf.UI.UserControls
{
    /// <summary>
    /// Interaction logic for NoteControl.xaml
    /// </summary>
    public partial class NoteControl : UserControl
    {

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(NoteControl), new PropertyMetadata(false));

        public static readonly DependencyProperty LostKeyboardFocusCommandProperty =
            DependencyProperty.Register("LostKeyboardFocusCommand", typeof(RelayCommand), typeof(NoteControl), new PropertyMetadata(null));

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        
        public RelayCommand LostKeyboardFocusCommand
        {
            get { return (RelayCommand)GetValue(LostKeyboardFocusCommandProperty); }
            set { SetValue(LostKeyboardFocusCommandProperty, value); }
        }
        
        public NoteControl()
        {
            InitializeComponent();
        }

        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (LostKeyboardFocusCommand != null && LostKeyboardFocusCommand.CanExecute(null))
                LostKeyboardFocusCommand.Execute(null);

            base.OnLostKeyboardFocus(e);
        }
    }


}
