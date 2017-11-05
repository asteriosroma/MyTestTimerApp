using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyTestTimerApp
{
    class ViewModel : INotifyPropertyChanged
    {
        Model model;
        
        public ViewModel()
        {
            model = new Model();
        }

        public void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            model.Start_timer();
        }

        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            model.Stop_timer();
        }

        public void MainWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            FocusManager.SetFocusedElement((Window)sender, (Window)sender);
        }

        public Model Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                OnMyPropertyChanged("Model");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnMyPropertyChanged([CallerMemberName]string s = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(s));
        }
    }
}
