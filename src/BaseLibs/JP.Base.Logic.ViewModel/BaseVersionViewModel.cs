namespace JP.Base.Logic.ViewModel
{
    public class BaseViewModelConcurrent<TIdentity> : BaseViewModel<TIdentity>
    {
        public byte[] Version { get; set; }
    }
}