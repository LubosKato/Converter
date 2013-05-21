namespace Converter
{
    internal struct UnitValue
    {
        public int Code;
        public float Value;
    }

    public class Conversion
    {
        public Conversion(int unitCode, float unitValue, ConversionTable conversionTable, Unit unit)
        {
            if (unitValue < 0)
                throw new NegativeInputException("Input is negative please fix");
            _untValue.Code = unitCode;
            _untValue.Value = unitValue;
            _tblConversion = conversionTable;
            _unit = unit;
        }
        
        private readonly ConversionTable _tblConversion;
        public ConversionTable ConversionTable
        {
            get { return _tblConversion; }
        }

        private readonly Unit _unit;
        public Unit Unit
        {
            get { return _unit; }
        }

        private readonly UnitValue _untValue;
        public float Value
        {
            get { return _untValue.Value; }
        }

        public int UnitCode
        {
            get { return _untValue.Code; }
        }

        public string UnitName
        {
            get { return _unit.GetUnitName(_untValue.Code); }
        }

        public string UnitSymbol
        {
            get { return _unit.GetUnitSymbol(_untValue.Code); }
        }

        public string UnitPlural
        {
            get { return _unit.GetUnitPlural(_untValue.Code); }
        }

        public Conversion Convert(int destCode)
        {
            return _tblConversion.Convert(this, destCode, _unit);
        }
    }
}