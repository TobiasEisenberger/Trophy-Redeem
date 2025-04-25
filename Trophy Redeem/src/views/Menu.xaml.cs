using System;
using System.Windows;
using System.Windows.Controls;

namespace Trophy_Redeem
{
    /// <summary>
    /// Interaktionslogik für Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        public event EventHandler NewBtnClick;
        public event EventHandler StartBtnClick;
        public event EventHandler LoadBtnClick;

        public void EnableContinueButton()
        {
            start_btn.Visibility = Visibility.Visible;
        }

        private void new_btn_Click(object sender, RoutedEventArgs e)
        {
            if (NewBtnClick != null)
            {
                NewBtnClick(this, EventArgs.Empty);
            }
        }

        private void start_btn_Click(object sender, RoutedEventArgs e)
        {
            if (StartBtnClick != null)
            {
                StartBtnClick(this, EventArgs.Empty);
            }
        }

        private void load_btn_Click(object sender, RoutedEventArgs e)
        {
            if (LoadBtnClick != null)
            {
                LoadBtnClick(this, EventArgs.Empty);
            }
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
