using System.Collections.Generic;
using Converter.Interfaces;

namespace Converter
{
    internal struct UnitSpec
    {
        public int Code;
        public string Name;
        public string Symbol;
        public string Plural;
    }

    public class Unit : IUnit
    {
        internal Dictionary<int, UnitSpec> DicUnits = new Dictionary<int, UnitSpec>();

        public void AddUnit(int unitCode, string name, string symbol, string plural)
        {
            if (DicUnits.ContainsKey(unitCode))
                throw new DuplicatedUnitException("Duplicate unit code");

            UnitSpec spec;
            spec.Code = unitCode;
            spec.Name = name;
            spec.Symbol = symbol;
            spec.Plural = plural;

            DicUnits[unitCode] = spec;
        }

        public string GetUnitName(int unitCode)
        {
            if (!DicUnits.ContainsKey(unitCode))
                throw new UnknownUnitException("Unknown Unit Name");
            return DicUnits[unitCode].Name;
        }
        public string GetUnitSymbol(int unitCode)
        {
            if (!DicUnits.ContainsKey(unitCode))
                throw new UnknownUnitException("Unknown Unit Symbol");
            return DicUnits[unitCode].Symbol;
        }
        public string GetUnitPlural(int unitCode)
        {
            if (!DicUnits.ContainsKey(unitCode))
                throw new UnknownUnitException("Unknown Unit Prular Name");
            return DicUnits[unitCode].Plural;
        }
    }
}
