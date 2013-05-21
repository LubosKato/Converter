namespace Converter.Interfaces
{
    public interface IConversionTable
    {
        void AddConversion(int srcCode, int destCode, LinearConverter converter);
        Conversion Convert(Conversion srcUnit, int destCode, Unit unit);
    }
}