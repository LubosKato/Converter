using System.IO;
using Converter;
using NUnit.Framework;

namespace ConverterTest
{
    public class ExceptionsTests
    {
        [Test(Description = "Test with wrong Conversion tag")]
        [ExpectedException(typeof(ConversionCreationException), ExpectedMessage = "Creation tag is not correct")]
        public void TestWithWrongConversionTag()
        {
            XmlConversionTable xmlConversion = new XmlInitTable().TableInit("Converter.TestCases.LengthConversionTagFail.xml");
            Conversion meters = new Conversion(1, 10, (ConversionTable)xmlConversion.ConversionTable, (Unit)xmlConversion.Unit);
        }

        [Test(Description = "Test with 0 factor value")]
        [ExpectedException(typeof(ConversionCreationException), ExpectedMessage = "Creation tag is not correct")]
        public void TestWithZeroFactorValue()
        {
            XmlConversionTable xmlConversion = new XmlInitTable().TableInit("Converter.TestCases.LengthUnits0factor.xml");
            new LinearConverter(0, 12);
        }

        [Test(Description = "Test with wrong unit tag")]
        [ExpectedException(typeof(UnitCreationException), ExpectedMessage = "Unit tag is not correct")]
        public void TestWithWrongUnitTagParameter()
        {
            XmlConversionTable xmlConversion = new XmlInitTable().TableInit("Converter.TestCases.LengthUnitsTagFail.xml");
            Conversion meters = new Conversion(1, 10, (ConversionTable)xmlConversion.ConversionTable, (Unit)xmlConversion.Unit);
        }

        [Test(Description = "Test negative input for information conversion")]
        [ExpectedException(typeof(NegativeInputException), ExpectedMessage = "Input is negative please fix")]
        public void TestnegativeNumberToConvert()
        {
            XmlConversionTable xmlConversion = new XmlInitTable().TableInit("Converter.Configs.InformationUnits.xml");
            Conversion meters = new Conversion(1, -125, (ConversionTable)xmlConversion.ConversionTable, (Unit)xmlConversion.Unit);
            var result = meters.Convert(10);
            Assert.AreEqual("1.2765132794883E-6 YB", result.Value + " " + result.UnitSymbol);
        }

        [Test(Description = "Test call of missing conversion part of config")]
        [ExpectedException(typeof(NoAvailableConversionException), ExpectedMessage = "No Available Conversion")]
        public void TestNotAvailableConversion()
        {
            XmlConversionTable xmlConversion = new XmlInitTable().TableInit("Converter.TestCases.LengthUnits.xml");
            Conversion meters = new Conversion(2, 100, (ConversionTable)xmlConversion.ConversionTable, (Unit)xmlConversion.Unit);
            var result = meters.Convert(9);
        }

        [Test(Description = "Test with missing unit in conversion part of config")]
        [ExpectedException(typeof(UnknownUnitException), ExpectedMessage = "Unknown Unit Name")]
        public void TestNotExistingUnitCall()
        {
            XmlConversionTable xmlConversion = new XmlInitTable().TableInit("Converter.TestCases.LengthUnits.xml");
            Conversion meters = new Conversion(2, 100, (ConversionTable)xmlConversion.ConversionTable, (Unit)xmlConversion.Unit);
            var unitname = meters.UnitName;
        }

        [Test(Description = "Duplicate unit code")]
        [ExpectedException(typeof(DuplicatedUnitException), ExpectedMessage = "Duplicate unit code")]
        public void TestDuplicateUnits()
        {
            XmlConversionTable xmlConversion = new XmlInitTable().TableInit("Converter.TestCases.LengthDuplicateUnits.xml");
            Conversion meters = new Conversion(1, 10, (ConversionTable)xmlConversion.ConversionTable, (Unit)xmlConversion.Unit);
        }

        [Test(Description = "missing configuration xml")]
        [ExpectedException(typeof(FileNotFoundException), ExpectedMessage = "File Not found")]
        public void TestInformationConversionFromNotExistingFile()
        {
            var conversionTable = new XmlConversionTable(new ConversionTable(), new Unit());
            conversionTable.XmlConversionTableInit("InformationUnitsFail.xml");
        }
    }
}