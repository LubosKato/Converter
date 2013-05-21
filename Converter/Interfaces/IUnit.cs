namespace Converter.Interfaces
{
    public interface IUnit
    {
        void AddUnit(int unitCode, string name, string symbol, string plural);
        string GetUnitName(int unitCode);
        string GetUnitSymbol(int unitCode);
        string GetUnitPlural(int unitCode);
    }
}