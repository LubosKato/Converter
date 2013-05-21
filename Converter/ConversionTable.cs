using System.Collections.Generic;
using Converter.Interfaces;

namespace Converter
{
    public class ConversionTable : IConversionTable
    {
        private readonly Dictionary<string, ILinearConverter> _dicConversionTable = new Dictionary<string, ILinearConverter>();
        public void AddConversion(int srcCode, int destCode, LinearConverter converter)
        {
            _dicConversionTable[FormatKey(srcCode, destCode)] = converter;
            if (!_dicConversionTable.ContainsKey(FormatKey(destCode, srcCode)) && converter.AllowInverse)
                _dicConversionTable[FormatKey(destCode, srcCode)] = converter.Inverse;
        }

        private static string FormatKey(int srcCode, int destCode)
        {
            return srcCode + ":" + destCode;
        }

        public Conversion Convert(Conversion srcUnit, int destCode, Unit unit)
        {
            return new Conversion(destCode, GetConversion(srcUnit.UnitCode, destCode).Convert(srcUnit.Value), srcUnit.ConversionTable, unit);
        }

        private ILinearConverter GetConversion(int srcCode, int destCode)
        {
            if (!_dicConversionTable.ContainsKey(FormatKey(srcCode, destCode)))
                throw new NoAvailableConversionException("No Available Conversion");
            return _dicConversionTable[FormatKey(srcCode, destCode)];
        }
    }
}
