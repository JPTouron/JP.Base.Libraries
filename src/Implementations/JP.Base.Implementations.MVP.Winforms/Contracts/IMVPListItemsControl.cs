using JP.Base.Implementations.MVP.Winforms.Contracts.Base;

namespace JP.Base.Implementations.MVP.Winforms.Contracts
{
    public interface IMVPListItemsControl : IBaseMVPListItemsControl
    {
        object SelectedItem
        {
            get;
        }
    }
}