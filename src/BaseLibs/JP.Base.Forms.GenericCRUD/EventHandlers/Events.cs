using System;
using System.Windows.Forms;
using JP.Base.Common.Forms;

namespace JP.Base.Forms.GenericCRUD.EventHandlers
{
    public delegate void CancellingHandler(ABMCommonRoutines.enModes currentMode);

    public delegate void ChangeItemsListStateHandler(bool enabled);

    public delegate void DeletingItemHandler();

    public delegate void EnterEditingModeHandler();

    public delegate void EnterNewModeHandler();

    public delegate void EnterNormalModeHandler();

    public delegate void EnterQueryModeHandler();

    public delegate void EnterSearchModeHandler();

    public delegate void NodeSelectedHandler(Object sender, TreeViewEventArgs e);

    public delegate void SavingItemHandler(bool newItem, ref bool cancel);
}