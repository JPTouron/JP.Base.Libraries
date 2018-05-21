namespace JP.Base.ViewModel
{
    public class BaseViewModel<T>
    {
        public T Id { get; set; }

        public byte[] Version { get; set; }
    }
}