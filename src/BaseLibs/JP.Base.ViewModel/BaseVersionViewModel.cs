namespace JP.Base.ViewModel
{
    public class BaseVersionViewModel<TIdentity> : BaseViewModel<TIdentity>
    {
        public byte[] Version { get; set; }
    }
}