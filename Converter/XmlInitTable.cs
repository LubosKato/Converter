using System.Reflection;

namespace Converter
{
    public class XmlInitTable
    {
        XmlConversionTable _table;
        public XmlConversionTable TableInit(string path)
        {
            var conversionTable = new XmlConversionTable(new ConversionTable(), new Unit());
            conversionTable.XmlConversionTableInit(Assembly.GetExecutingAssembly().GetManifestResourceStream(path));
            return conversionTable;
        }

        public XmlConversionTable LengthTable
        {
            get { return _table ?? (_table = TableInit("Converter.Configs.LengthUnits.xml")); }
        }

        public XmlConversionTable WeightTable
        {
            get { return _table ?? (_table = TableInit("Converter.Configs.WeightUnits.xml")); }
        }

        public XmlConversionTable TemperatureTable
        {
            get { return _table ?? (_table = TableInit("Converter.Configs.TemperatureUnits.xml")); }
        }

        public XmlConversionTable InformationTable
        {
            get { return _table ?? (_table = TableInit("Converter.Configs.InformationUnits.xml")); }
        }
    }
}