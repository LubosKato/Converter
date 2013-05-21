using Converter;
using NUnit.Framework;

namespace ConverterTest
{
    [TestFixture]
    public class XmlConversionTableTests : ConversionTable
    {
        [Test(Description = "Test Table creation from file explicit way")]
        public void TestCreationFromFileExplicit()
        {
            var conversionTable = new XmlConversionTable(new ConversionTable(), new Unit());
            conversionTable.XmlConversionTableInit("../../../Converter/Configs/InformationUnits.xml");

            Conversion meters = new Conversion(1, 10, (ConversionTable)conversionTable.ConversionTable, (Unit)conversionTable.Unit);
            var result = meters.Convert(3);
            Assert.AreEqual(meters.UnitName, "bit");
            Assert.AreEqual(meters.UnitPlural, "bits");
            Assert.AreEqual(meters.UnitSymbol, "b");
            Assert.AreEqual("0.001220703 KB", result.Value + " " + result.UnitSymbol);
        }

        [Test(Description = "Test information conversion od 115 bit = 1.21761E-20 ZB")]
        public void TestInformationConversionFromFile()
        {
            var conversionTable = new XmlConversionTable(new ConversionTable(), new Unit());
            conversionTable.XmlConversionTableInit("../../../Converter/Configs/InformationUnits.xml");
            Conversion meters = new Conversion(1, 115, (ConversionTable)conversionTable.ConversionTable, (Unit)conversionTable.Unit);
            var result = meters.Convert(9);
            //result is 1.2176098616781E-20 from http://www.bit-calculator.com/
            Assert.AreEqual("1.21761E-20ZB", result.Value + result.UnitSymbol);
            Assert.AreEqual("1.21761E-20 ZettaBytes", result.Value + " " + result.UnitPlural);
        }

        [Test(Description = "Test length conversion for right result 100 metes = 1000cm")]
        public void TestLengthConversionFromFile()
        {
            var conversionTable = new XmlConversionTable(new ConversionTable(), new Unit());
            conversionTable.XmlConversionTableInit("../../../Converter/Configs/LengthUnits.xml");
            Conversion meters = new Conversion(1, 100, (ConversionTable)conversionTable.ConversionTable, (Unit)conversionTable.Unit);
            var result = meters.Convert(3);
            Assert.AreEqual("10000cm", result.Value + result.UnitSymbol);
        }
    }
}