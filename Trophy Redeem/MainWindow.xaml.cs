using System.Windows;
using Trophy_Redeem.src;

namespace Trophy_Redeem
{

    public partial class MainWindow : Window
    {

        private GameEngine gameEngine;

        public MainWindow()
        {
            InitializeComponent();
            gameEngine = new GameEngine(ref MainContent, ref OverlayContent);
        }

    }

}
