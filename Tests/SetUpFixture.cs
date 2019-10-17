﻿using System;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using Tests.Support;

namespace Tests
{
    [SetUpFixture]
    public class SetUpFixture
    {
        private bool _portExistedBeforeTests;
        private bool _printerExistedBeforeTests;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // We only delete the port and printer if they didn't exist when tests started'
            _portExistedBeforeTests = NulPortExists();
            _printerExistedBeforeTests = NotifyPrinterExists();

            if (!_portExistedBeforeTests)
            {
                SafePrinter.AddPort(NameConstants.PortName);
            }

            if (!_printerExistedBeforeTests)
            {
                SafePrinter.AddPrinter(NameConstants.PrinterName, NameConstants.PortName, NameConstants.DriverName);
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // We can't delete the port until we have deleted the printer
            if (!_printerExistedBeforeTests)
            {
                using (var printer = new SafePrinter(NameConstants.PrinterName))
                {
                    printer.Delete();
                }
            }

            if (!_portExistedBeforeTests)
            {
                SafePrinter.DeletePort(NameConstants.PortName);
            }
        }


        private static bool NulPortExists()
        {
            return SafePrinter.EnumPorts().Any(pi => pi.Name.Equals(NameConstants.PortName, StringComparison.CurrentCultureIgnoreCase));
        }

        private static bool NotifyPrinterExists()
        {
            return SafePrinter.EnumPrinters().Any(pi => pi.pPrinterName.Equals(NameConstants.PrinterName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}