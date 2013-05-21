using System;
using System.IO;
using System.Xml;
using Converter.Helpers;
using Converter.Interfaces;

namespace Converter
{
    /// <summary>
    /// Xml Initializable Conversion Table
    /// Reads information about units and their Converter from a Xml document.
    /// </summary>
    public class XmlConversionTable
    {
        public readonly IConversionTable ConversionTable;
        public readonly IUnit Unit;

        public XmlConversionTable(IConversionTable conversionTable, IUnit unit)
        {
            ConversionTable = conversionTable;
            Unit = unit;
        }

        public void XmlConversionTableInit(Stream stream)
        {
            var units = new XmlDocument();
            try
            {
                 units.Load(stream);
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException("File Not found", e);
            }

            Initialize(units[Constants.ELEMENT_UNITTABLE]);
        }

        public void XmlConversionTableInit(string fileName)
        {
            var units = new XmlDocument();
            try
            {
                units.Load(fileName);
            }
            catch (FileNotFoundException e)
            {
                throw new FileNotFoundException("File Not found", e);
            }

            Initialize(units[Constants.ELEMENT_UNITTABLE]);
        }

        private void Initialize(XmlElement unitTableElement)
        {
            var xmlElement = unitTableElement[Constants.ELEMENT_UNITS];
            if (xmlElement != null)
                foreach (XmlElement unitElement in xmlElement.ChildNodes)
                {
                    int code;
                    string name, symbol, plural;

                    CreateUnit(unitElement, out code, out name, out symbol, out plural);
                    Unit.AddUnit(code, name, symbol, plural);
                }

            var element = unitTableElement[Constants.ELEMENT_ConversionS];
            if (element != null)
                foreach (XmlElement conversionElement in element.ChildNodes)
                {
                    int srcCode, destCode;

                    LinearConverter converter = CreateConversion(conversionElement, out srcCode, out destCode);
                    ConversionTable.AddConversion(srcCode, destCode, converter);
                }
        }

        private static void CreateUnit(XmlElement unitElement, out int unitCode, out string unitName, out string unitSymbol, out string unitPlural)
        {
            try
            {
                unitPlural = (unitElement.Attributes[Constants.ATTRIBUTE_PLURAL] != null) ?
                  unitElement.Attributes[Constants.ATTRIBUTE_PLURAL].Value : unitElement.Attributes[Constants.ATTRIBUTE_NAME].Value;

                unitCode = int.Parse(unitElement.Attributes[Constants.ATTRIBUTE_CODE].Value);
                unitName = unitElement.Attributes[Constants.ATTRIBUTE_NAME].Value;
                unitSymbol = unitElement.Attributes[Constants.ATTRIBUTE_SYMBOL].Value;
            }
            catch (Exception ex)
            {
                throw new UnitCreationException("Unit tag is not correct", ex);
            }
        }

        private static LinearConverter CreateConversion(XmlElement conversionElement, out int srcCode, out int destCode)
        {
            LinearConverter result = null;
            try
            {
                srcCode = int.Parse(conversionElement.Attributes[Constants.ATTRIBUTE_SRCCODE].Value);
                destCode = int.Parse(conversionElement.Attributes[Constants.ATTRIBUTE_DESTCODE].Value);
            }
            catch (Exception ex)
            {
                throw new ConversionCreationException("Creation tag is not correct", ex);
            }

            if (conversionElement.Name == Constants.ELEMENT_LINEAR)
            {
                try
                {
                    double factor = conversionElement.Attributes[Constants.ATTRIBUTE_FACTOR] == null ?
                      1d : double.Parse(conversionElement.Attributes[Constants.ATTRIBUTE_FACTOR].Value);
                    double deltha = conversionElement.Attributes[Constants.ATTRIBUTE_DELTHA] == null ?
                      0d : double.Parse(conversionElement.Attributes[Constants.ATTRIBUTE_DELTHA].Value);

                    result = new LinearConverter(factor, deltha);
                }
                catch (Exception ex)
                {
                    throw new ConversionCreationException("Creation tag is not correct", ex);
                }
            }

            if (result == null)
                throw new ConversionCreationException("Creation tag is not correct");

            return result;
        }
    }
}