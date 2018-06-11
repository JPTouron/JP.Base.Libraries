using JP.Base.Implementations.MVP.Winforms.Contracts.Messages;

namespace JP.Base.Implementations.MVP.Winforms.Implementation.Messages
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