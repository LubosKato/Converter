using Converter;
using NUnit.Framework;

namespace ConverterTest
{
    [TestFixture]
    public class InitTableTests
    {
        [Test(Description = "Test information conversion by calling built-in implicit initializatio")]
        public void TestCreationFromFileImplicit()
        {
            XmlConversionTable xmlConversion = new XmlInitTable().InformationTable;
            Conversion conversion = new Conversion(1, 100, (ConversionTable)xmlConversion.ConversionTable, (Unit)xmlConversion.Unit);
            Assert.AreEqual(conversion.UnitName, "bit");
            Assert.AreEqual(conversion.UnitPlural, "bits");
            Assert.AreEqual(conversion.UnitSymbol, "b");
            var result = conversion.Convert(3);
            Assert.AreEqual("0.01220703 KB", result.Value + " " + result.UnitSymbol);
        }

        [Test(Description = "Test length conversion by calling built-in implicit initialization")]
        public void TestLengthConversionFromImplicitInit()
        {
            XmlConversionTable xmlConversion = new XmlInitTable().LengthTable;
            Conversion conversion = new Conversion(1, 100, (ConversionTable)xmlConversion.ConversionTable, (Unit)xmlConversion.Unit);
            Assert.AreEqual(conversion.UnitName, "Meter");
            Assert.AreEqual(conversion.UnitPlural, "Meters");
            Assert.AreEqual(conversion.UnitSymbol, "m");
            var result = conversion.Convert(3);
            Assert.AreEqual("10000cm", result.Value + result.UnitSymbol);
        }
    }
}