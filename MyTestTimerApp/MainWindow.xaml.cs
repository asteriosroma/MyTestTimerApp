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

namespace MyTestTimerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel view_model;
        public MainWindow()
        {
            InitializeComponent();

            view_model = new ViewModel();

            DataContext = view_model;

            textBox.GotFocus += view_model.TextBox_GotFocus;
            textBox.LostFocus += view_model.TextBox_LostFocus;
            this.PreviewMouseDown += view_model.MainWindow_PreviewMouseDown;
        }
    }
}
