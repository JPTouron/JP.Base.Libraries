namespace JP.Base.Exceptions.Winforms
{
    public interface IProcessValidator
    {
        object ErrorId { get; set; }
        bool IsValid { get; }
        string Message { get; set; }
    }
}