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

namespace Trophy_Redeem
{
    /// <summary>
    /// Interaktionslogik für PauseMenu.xaml
    /// </summary>
    public partial class PauseMenu : UserControl
    {
        public PauseMenu()
        {
            InitializeComponent();
        }

        public event EventHandler ResumeBtnClick;
        public event EventHandler SaveBtnClick;
        public event EventHandler BackBtnClick;

        private void resume_btn_Click(object sender, RoutedEventArgs e)
        {
            if(ResumeBtnClick != null)
            {
                ResumeBtnClick(this, EventArgs.Empty);
            }

        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            if (SaveBtnClick != null)
            {
                SaveBtnClick(this, EventArgs.Empty);
            }
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            if(BackBtnClick != null)
            {
                BackBtnClick(this, EventArgs.Empty);
            }
        }

        internal void Close()
        {
            this.Visibility = Visibility.Collapsed; 
            
        }

        internal void Open()
        {
            this.Visibility = Visibility.Visible;
        }
    }
}
