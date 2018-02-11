using JP.Base.MVP.Implementation.Winforms.Contracts.Messages;

namespace JP.Base.MVP.Implementation.Winforms.Implementation.Messages
{
    public interface IRendererFactory
    {
        IMessageRenderer CreateRenderer();
    }

    public static class RendererProvider
    {
        public static IRendererFactory RendererFactory { get; set; }
    }
}