using JP.Base.MVP.Implementation.Winforms.Contracts.Base;

namespace JP.Base.MVP.Implementation.Winforms
{
    public interface IMVPListItemsControl : IBaseMVPListItemsControl
    {
        object SelectedItem
        {
            get;
        }
    }
}