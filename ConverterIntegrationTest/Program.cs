using System;
using Converter;
using ConverterIntegrationTest.Helpers;

namespace ConverterIntegrationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string fromFileName = string.Empty;
            string toFileName = string.Empty;

            args.GetCommandLineSwitch(Constants.INFILE_SWITCH, ref fromFileName);
            args.GetCommandLineSwitch(Constants.OUTFILE_SWITCH, ref toFileName);
            try
            {
                if (fromFileName.Length > 0 && toFileName.Length > 0)
                {
                    long val = long.Parse(args[0]);
                    int fromVal = int.Parse(fromFileName);
                    int toVal = int.Parse(toFileName);
                    XmlConversionTable table = new XmlInitTable().LengthTable;
                    //ConversionTable table = InitTable.LengthTableInit("Converter.TestCases.LengthDuplicateUnits.xml");
                    Conversion meters = new Conversion(fromVal, val, (ConversionTable)table.ConversionTable, (Unit)table.Unit);
                    Conversion result = meters.Convert(toVal);
                    if(val > 1)
                    Console.WriteLine("ConversionTable from {0} to {1} is {2}", args[0] + " " + result.Unit.GetUnitPlural(fromVal), result.UnitPlural, result.Value + " " + result.UnitSymbol);
                    else
                    {
                        Console.WriteLine("ConversionTable from {0} to {1} is {2}", args[0] + " " + result.Unit.GetUnitName(fromVal), result.UnitName, result.Value + " " + result.UnitSymbol);
                    }
                    Console.ReadLine();
                }
                else
                {
                    CommandLineHelper.ShowUsage();
                }  
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}
