using HP.ClearingCenter.Application.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Configuration
{
    public static partial class Data
    {
        const string CUSTOM_ATTRIBUTE_native_video_resolution = "native_video_resolution";
        const string CUSTOM_ATTRIBUTE_number_of_ports = "number_of_ports";
        const string CUSTOM_ATTRIBUTE_number_of_cpus = "number_of_cpus";
        const string CUSTOM_ATTRIBUTE_can_scan = "can_scan";
        const string CUSTOM_ATTRIBUTE_can_print = "can_print";
        const string CUSTOM_ATTRIBUTE_screen_size = "screen_size";
        const string CUSTOM_ATTRIBUTE_number_of_drives = "number_of_drives";
        const string CUSTOM_ATTRIBUTE_min_print_width = "min_print_width";
        const string CUSTOM_ATTRIBUTE_max_print_width = "max_print_width";
        const string CUSTOM_ATTRIBUTE_printer_color_count = "printer_color_count";
        const string CUSTOM_ATTRIBUTE_printer_technology = "printer_technology";
        const string CUSTOM_ATTRIBUTE_memory_in_gigabytes = "memory_in_gigabytes";

        const string CUSTOM_ATTRIBUTE_is_monochrome = "is_monochrome";
        const string CUSTOM_ATTRIBUTE_has_cutter = "has_cutter";
        const string CUSTOM_ATTRIBUTE_is_mfp = "is_mfp";
        const string CUSTOM_ATTRIBUTE_supports_multiroll = "supports_multiroll";
        const string CUSTOM_ATTRIBUTE_inkjet_print_technology = "inkjet_print_technology";

        private static void CustomAttributes(ClearingCenterDataContext db)
        {
            // custom attribute data types
            var stringType = db.CustomAttributeDataTypes.Add(new CustomAttributeDataType(CustomAttributeType.String));
            var intType = db.CustomAttributeDataTypes.Add(new CustomAttributeDataType(CustomAttributeType.Integer));
            var decimalType = db.CustomAttributeDataTypes.Add(new CustomAttributeDataType(CustomAttributeType.Decimal));
            var boolType = db.CustomAttributeDataTypes.Add(new CustomAttributeDataType(CustomAttributeType.Boolean));
            
            var numberOfPorts = db.CustomAttributes.Add(new CustomAttribute { CustomAttributeDataType = intType, ExternalCode = CUSTOM_ATTRIBUTE_number_of_ports, ShortName = "Number of Ports" });
            var numberOfDrives = db.CustomAttributes.Add(new CustomAttribute { CustomAttributeDataType = intType, ExternalCode = CUSTOM_ATTRIBUTE_number_of_drives, ShortName = "Number of Drives" });
            var numberOfCpus = db.CustomAttributes.Add(new CustomAttribute { CustomAttributeDataType = intType, ExternalCode = CUSTOM_ATTRIBUTE_number_of_cpus, ShortName = "Number of CPUs" });
            var memoryInGigabytes = db.CustomAttributes.Add(new CustomAttribute { CustomAttributeDataType = decimalType, ExternalCode = CUSTOM_ATTRIBUTE_memory_in_gigabytes, ShortName = "Memory in Gigabytes (GB)", UnitText = "GB" });

            var diskspace = db.CustomAttributes.Add(new CustomAttribute { CustomAttributeDataType = decimalType, ExternalCode = "disk_space_in_gb", ShortName = "Disk Space in GB", UnitText = "GB" });

            var minPrintWidth = db.CustomAttributes.Add(new CustomAttribute { CustomAttributeDataType = decimalType, ExternalCode = CUSTOM_ATTRIBUTE_min_print_width, ShortName = "Minimum Print Width", UnitText = "Inches" });
            var maxPrintWidth = db.CustomAttributes.Add(new CustomAttribute { CustomAttributeDataType = decimalType, ExternalCode = CUSTOM_ATTRIBUTE_max_print_width, ShortName = "Maximum Print Width", UnitText = "Inches" });

            var printTechnology = db.CustomAttributes.Add(new CustomAttribute { CustomAttributeDataType = stringType, ExternalCode = CUSTOM_ATTRIBUTE_printer_technology, ShortName = "Print Technology", IsOptionListItemsEnabled = true, IsStrictToOptions = true });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printTechnology, DisplayText = "Inkjet", ValueText = "print_technology_inkjet" });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printTechnology, DisplayText = "Laser", ValueText = "print_technology_laser" });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printTechnology, DisplayText = "Dot Matrix", ValueText = "print_technology_dotmatrix" });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printTechnology, DisplayText = "UltraViolet", ValueText = "print_technology_ultraviolet" });

            var inkjetPrintTechnology = db.CustomAttributes.Add(new CustomAttribute { CustomAttributeDataType = stringType, ExternalCode = CUSTOM_ATTRIBUTE_inkjet_print_technology, ShortName = "Inkjet Print Technology", IsOptionListItemsEnabled = true, IsStrictToOptions = true });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printTechnology, DisplayText = "Standard", ValueText = "inkjet_technology_standard" });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printTechnology, DisplayText = "Aquous", ValueText = "inkjet_technology_aquous" });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printTechnology, DisplayText = "Eco-/Solvent", ValueText = "inkjet_technology_ecosolvent" });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printTechnology, DisplayText = "Oil-based", ValueText = "inkjet_technology_oilbased" });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printTechnology, DisplayText = "Water-based", ValueText = "inkjet_technology_waterbased" });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printTechnology, DisplayText = "UV Printing", ValueText = "inkjet_technology_uvprinting" });

            var printerColorCount = db.CustomAttributes.Add(new CustomAttribute { CustomAttributeDataType = intType, ExternalCode = CUSTOM_ATTRIBUTE_printer_color_count, ShortName = "Color Count", IsOptionListItemsEnabled = true, IsStrictToOptions = true });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printerColorCount, DisplayText = "1 (Black)", ValueText = "1" });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printerColorCount, DisplayText = "3 (RGB)", ValueText = "3" });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printerColorCount, DisplayText = "4 (RGB + Black)", ValueText = "4" });
            db.OptionListItems.Add(new OptionListItem { CustomAttribute = printerColorCount, DisplayText = "5 (CMYK + Black)", ValueText = "3" });
        }
    }
}
