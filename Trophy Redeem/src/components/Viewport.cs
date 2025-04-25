using System.Windows.Controls;
using System.Windows.Shapes;

namespace Trophy_Redeem.src.graphics
{
    internal class Viewport
    {

        Panel panel;

        public Viewport(Panel panel)
        {
            this.panel = panel;
        }

        public void Add(Shape element)
        {
            if (!panel.Children.Contains(element))
                panel.Children.Add(element);
        }

        public void Remove(Shape element)
        {
            panel.Children.Remove(element);
        }

    }
}
