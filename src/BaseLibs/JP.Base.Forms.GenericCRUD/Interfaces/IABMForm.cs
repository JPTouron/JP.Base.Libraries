using JP.Base.Common.Forms;
using JP.Base.Forms.GenericCRUD.EventHandlers;

namespace JP.Base.Forms.GenericCRUD.Forms.Interfaces
{
    public interface IABMForm
    {
        bool DisplayProgressBar { get; set; }

        bool EnableQueryFunctionality { get; set; }

        bool EnableSearchFunctionality { get; set; }

        bool EnhanceDisplayArea { get; set; }

        bool FormStateAffectsContainedControls { get; set; }

        ABMCommonRoutines.enModes ModoActual { get; }

        event CancellingHandler Cancelling;

        event ChangeItemsListStateHandler ChangeItemsListState;

        event DeletingItemHandler DeletingItem;

        event EnterEditingModeHandler EnterEditingMode;

        event EnterNewModeHandler EnterNewMode;

        event EnterNormalModeHandler EnterNormalMode;

        event EnterQueryModeHandler EnterQueryMode;

        event EnterSearchModeHandler EnterSearchMode;

        event SavingItemHandler SavingItem;

        event NodeSelectedHandler SelectedNode;

        void InitializeProgressBar(int minimum, int maximum);

        void ResetProgressBar();

        void SimulateLoadingProgressBar(int minimum, int maximum);

        void StopProgressBarSimulation();

        void UpdateProgressBar(int value);
    }
}