using Converter;
using NUnit.Framework;

namespace ConverterTest
{
    [TestFixture]
    public class ProgramTests
    {
        [Test(Description = "Duplicate conversions should pass because program will ignore duplicate conversions")]
        public void TestDuplicateConversions()
        {
            XmlConversionTable xmlConversion = new XmlInitTable().TableInit("Converter.TestCases.LengthDuplicateConversions.xml");
            Conversion meters = new Conversion(1, 10, (ConversionTable)xmlConversion.ConversionTable, (Unit)xmlConversion.Unit);
            var result = meters.Convert(2);
            Assert.AreEqual("1000 cm", result.Value + " " + result.UnitSymbol);
        }

        [Test(Description = "Test big input for information conversion")]
        public void TestbigNumberToConvert()
        {
            XmlConversionTable xmlConversion = new XmlInitTable().TableInit("Converter.Configs.InformationUnits.xml");
            Conversion meters = new Conversion(1, 12345678901234567890, (ConversionTable)xmlConversion.ConversionTable, (Unit)xmlConversion.Unit);
            var result = meters.Convert(10);
            Assert.AreEqual("1.276513E-06 YB", result.Value + " " + result.UnitSymbol);
        }
    }
}
