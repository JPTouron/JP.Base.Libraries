namespace JP.Base.Errors.Managing
{
    public interface IProcessValidator
    {
        object ErrorId { get; set; }
        bool IsValid { get; }
        string Message { get; set; }
    }
}