namespace JP.Base.ViewModel
{
    public class BaseViewModelConcurrent<TIdentity> : BaseViewModel<TIdentity>
    {
        public byte[] Version { get; set; }
    }
}