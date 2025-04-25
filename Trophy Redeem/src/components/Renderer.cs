using System.Windows.Shapes;

namespace Trophy_Redeem.src.graphics
{
    internal class Renderer
    {

        Viewport viewport;

        public Renderer(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void Render(Component component)
        {
            foreach (Shape element in component.GetElements())
            {
                viewport.Add(element);
            }
        }

    }
}
