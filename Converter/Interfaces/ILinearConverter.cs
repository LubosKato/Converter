namespace Converter.Interfaces
{
    public interface ILinearConverter
    {
        float Convert(float source);
        bool AllowInverse { get; }
        ILinearConverter Inverse { get; }
    }
}