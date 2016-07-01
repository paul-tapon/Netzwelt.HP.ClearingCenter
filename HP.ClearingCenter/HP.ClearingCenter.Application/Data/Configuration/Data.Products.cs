using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Migrations;
using System.Globalization;

namespace HP.ClearingCenter.Application.Data.Configuration
{
    using HP.ClearingCenter.Application.TransactionTransports.Entities;
    using HP.ClearingCenter.Application.Products.Entities;

    public static partial class Data
    {
        private static StandardCategory Categories(ClearingCenterDataContext db)
        {
            /*
            Accessories / Expandables
            Devices
              Computers
                 Desktops 
                 Notebooks
                 Retail Point-of-sale
                 Servers
                 Thin Clients
              Mobile
              Networking
                 Network Security
                 Network Management
                 Routers
                 Switches
                 Transceivers and Accessories
                 Wireless LAN
              Storage Solutions
              Printers
                Consumer
                Industrial
                   Analog LF Copier
                   Inkjet Printers
                   Laser and LED
                   Plotter
            Others     
            Services
            */

            var accessories = db.Categories.Add(new Category { ExternalCode = "accessories", ShortName = "Accessories & Expandables" });
            
            var devices = db.Categories.Add(new Category { ExternalCode = "devices", ShortName = "Devices" });
            var computers = db.Categories.Add(new Category { ExternalCode = "devices_computers", ShortName = "Computers", ParentCategory = devices, NavigationPath = "{0}".WithTokens(devices.ShortName) });
            db.CategoryAttributeAssignments.Add(new CategoryAttributeAssignment { Category = computers, CustomAttributeExternalCode = Data.CUSTOM_ATTRIBUTE_number_of_cpus });
            db.CategoryAttributeAssignments.Add(new CategoryAttributeAssignment { Category = computers, CustomAttributeExternalCode = Data.CUSTOM_ATTRIBUTE_memory_in_gigabytes });

            var notebooks = db.Categories.Add(new Category { ExternalCode = "devices_computers_notebooks", ShortName = "Notebooks", ParentCategory = computers, NavigationPath = "{0} » {1}".WithTokens(devices.ShortName, computers.ShortName) });
            var rpos = db.Categories.Add(new Category { ExternalCode = "devices_computers_rpos", ShortName = "Retail Point-of-sale", ParentCategory = computers, NavigationPath = "{0} » {1}".WithTokens(devices.ShortName, computers.ShortName) });
            var servers = db.Categories.Add(new Category { ExternalCode = "devices_computers_servers", ShortName = "Servers", ParentCategory = computers, NavigationPath = "{0} » {1}".WithTokens(devices.ShortName, computers.ShortName) });
            var thinclients = db.Categories.Add(new Category { ExternalCode = "devices_computers_thinclients", ShortName = "Thin Clients", ParentCategory = computers, NavigationPath = "{0} » {1}".WithTokens(devices.ShortName, computers.ShortName) });

            var mobile = db.Categories.Add(new Category { ExternalCode = "devices_mobile", ShortName = "Mobile", ParentCategory = devices, NavigationPath = "{0}".WithTokens(devices.ShortName) });

            var networking = db.Categories.Add(new Category { ExternalCode = "devices_networking", ShortName = "Networking", ParentCategory = devices, NavigationPath = "{0}".WithTokens(devices.ShortName) });
            var routers = db.Categories.Add(new Category { ExternalCode = "devices_networking_routers", ShortName = "Routers", ParentCategory = networking, NavigationPath = "{0} » {1}".WithTokens(devices.ShortName, networking.ShortName) });
            var switches = db.Categories.Add(new Category { ExternalCode = "devices_networking_switches", ShortName = "Switches", ParentCategory = networking, NavigationPath = "{0} » {1}".WithTokens(devices.ShortName, networking.ShortName) });
            var transceivers = db.Categories.Add(new Category { ExternalCode = "devices_networking_transceivers", ShortName = "Transceivers", ParentCategory = networking, NavigationPath = "{0} » {1}".WithTokens(devices.ShortName, networking.ShortName) });
            var wirelesslan = db.Categories.Add(new Category { ExternalCode = "devices_networking_wirelesslan", ShortName = "Wireless LAN", ParentCategory = networking, NavigationPath = "{0} » {1}".WithTokens(devices.ShortName, networking.ShortName) });

            var storagesolutions = db.Categories.Add(new Category { ExternalCode = "storagedevices", ShortName = "Storage Solutions", ParentCategory = devices, NavigationPath = "{0}".WithTokens(devices.ShortName) });

            var printers = db.Categories.Add(new Category { ExternalCode = "devices_printers", ShortName = "Printers", ParentCategory = devices, NavigationPath = "{0}".WithTokens(devices.ShortName) });
            db.CategoryAttributeAssignments.Add(new CategoryAttributeAssignment { Category = printers, CustomAttributeExternalCode = Data.CUSTOM_ATTRIBUTE_printer_color_count });
            db.CategoryAttributeAssignments.Add(new CategoryAttributeAssignment { Category = printers, CustomAttributeExternalCode = Data.CUSTOM_ATTRIBUTE_min_print_width });
            db.CategoryAttributeAssignments.Add(new CategoryAttributeAssignment { Category = printers, CustomAttributeExternalCode = Data.CUSTOM_ATTRIBUTE_max_print_width });
            db.CategoryAttributeAssignments.Add(new CategoryAttributeAssignment { Category = printers, CustomAttributeExternalCode = Data.CUSTOM_ATTRIBUTE_is_monochrome });
            db.CategoryAttributeAssignments.Add(new CategoryAttributeAssignment { Category = printers, CustomAttributeExternalCode = Data.CUSTOM_ATTRIBUTE_is_mfp });
            db.CategoryAttributeAssignments.Add(new CategoryAttributeAssignment { Category = printers, CustomAttributeExternalCode = Data.CUSTOM_ATTRIBUTE_printer_technology });

            var consumer = db.Categories.Add(new Category { ExternalCode = "devices_printers_consumer", ShortName = "Consumer / Small Office / Home Office", ParentCategory = printers, NavigationPath = "{0} » {1}".WithTokens(devices.ShortName, printers.ShortName) });
            
            var industrial = db.Categories.Add(new Category { ExternalCode = "devices_printers_industrial", ShortName = "Industrial", ParentCategory = printers, NavigationPath = "{0} » {1}".WithTokens(devices.ShortName, printers.ShortName) });
            db.CategoryAttributeAssignments.Add(new CategoryAttributeAssignment { Category = industrial, CustomAttributeExternalCode = Data.CUSTOM_ATTRIBUTE_has_cutter });
            db.CategoryAttributeAssignments.Add(new CategoryAttributeAssignment { Category = industrial, CustomAttributeExternalCode = Data.CUSTOM_ATTRIBUTE_supports_multiroll });

            var analoglf = db.Categories.Add(new Category { ExternalCode = "devices_printers_industrial_analoglfcopier", ShortName = "Analog Long-format Copier", ParentCategory = industrial, NavigationPath = "{0} » {1} » {2}".WithTokens(devices.ShortName, printers.ShortName, industrial.ShortName) });

            var inkjet = db.Categories.Add(new Category { ExternalCode = "devices_printers_industrial_inkjet", ShortName = "Inkjet", ParentCategory = industrial, NavigationPath = "{0} » {1} » {2}".WithTokens(devices.ShortName, printers.ShortName, industrial.ShortName) });
            db.CategoryAttributeAssignments.Add(new CategoryAttributeAssignment { Category = inkjet, CustomAttributeExternalCode = Data.CUSTOM_ATTRIBUTE_inkjet_print_technology });

            var laser = db.Categories.Add(new Category { ExternalCode = "devices_printers_industrial_laser", ShortName = "Laser and LED", ParentCategory = industrial, NavigationPath = "{0} » {1} » {2}".WithTokens(devices.ShortName, printers.ShortName, industrial.ShortName) });
            var plotter = db.Categories.Add(new Category { ExternalCode = "devices_printers_industrial_plotter", ShortName = "Plotter", ParentCategory = industrial, NavigationPath = "{0} » {1} » {2}".WithTokens(devices.ShortName, printers.ShortName, industrial.ShortName) });

            var others = db.Categories.Add(new Category { ExternalCode = "others", ShortName = "Others" } );

            var services = db.Categories.Add(new Category { ExternalCode = "services", ShortName = "Services" });

            return new StandardCategory
            {
                Computers = computers,
                Computers_Notebooks = notebooks,
                Computers_Servers = servers,

                Printers = printers,
                Printers_Consumer = consumer,
                Printers_Industrial = industrial,
                Printers_AnalogLf = analoglf,
                Printers_Inkjet = inkjet,
                Printers_Laser = laser,
                Printers_Plotter = plotter,
                Networking_Routers = routers,
                Networking_Switches = switches
            };
        }
        
        private static void Products(ClearingCenterDataContext db)
        {
            #region manufacturers
            // manufacturers
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SIEMENS-FUJITSU", Shortname = "SIEMENS-FUJITSU" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "APPLE", Shortname = "APPLE" });
            var hp = db.Manufacturers.Add(new Manufacturer { ExternalCode = "HP", Shortname = "HP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TOSHIBA", Shortname = "TOSHIBA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ACER", Shortname = "ACER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CANON", Shortname = "CANON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COMPAQ", Shortname = "COMPAQ" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DELL", Shortname = "DELL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EPSON", Shortname = "EPSON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IBM", Shortname = "IBM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LEXMARK", Shortname = "LEXMARK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OKI", Shortname = "OKI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PHILIPS", Shortname = "PHILIPS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ALR", Shortname = "ALR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DEC", Shortname = "DEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SUN", Shortname = "SUN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NEC", Shortname = "NEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ASUS", Shortname = "ASUS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EIZO", Shortname = "EIZO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BELINEA", Shortname = "BELINEA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IIYAMA", Shortname = "IIYAMA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MICROSCAN", Shortname = "MICROSCAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NOKIA", Shortname = "NOKIA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OLIVETTI", Shortname = "OLIVETTI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PEACOCK", Shortname = "PEACOCK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SAMSUNG", Shortname = "SAMSUNG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SONY", Shortname = "SONY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CISCO", Shortname = "CISCO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BROTHER", Shortname = "BROTHER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CALCOMP", Shortname = "CALCOMP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DLJ", Shortname = "DLJ" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ENCAD", Shortname = "ENCAD" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GERICOM", Shortname = "GERICOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KYOCERA", Shortname = "KYOCERA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OCE", Shortname = "OCE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PANASONIC", Shortname = "PANASONIC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "QMS", Shortname = "QMS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ROLAND", Shortname = "ROLAND" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SEIKOSHA", Shortname = "SEIKOSHA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TEKTRONIX", Shortname = "TEKTRONIX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "VERSATEC", Shortname = "VERSATEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "XEROX", Shortname = "XEROX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "3 M", Shortname = "3 M" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "3COM ", Shortname = "3COM " });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ACCTON", Shortname = "ACCTON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ASANTE", Shortname = "ASANTE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BAY NETWORKS", Shortname = "BAY NETWORKS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CNET", Shortname = "CNET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CABLETRON", Shortname = "CABLETRON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COMMODORE", Shortname = "COMMODORE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DIGICOM", Shortname = "DIGICOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "D-LINK", Shortname = "D-LINK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EDIMAX", Shortname = "EDIMAX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GENICOM", Shortname = "GENICOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INTEL", Shortname = "INTEL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NBASE", Shortname = "NBASE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NETCOR", Shortname = "NETCOR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NETWORTH", Shortname = "NETWORTH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OVISLINK", Shortname = "OVISLINK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SHARP", Shortname = "SHARP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SURECOM", Shortname = "SURECOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SYNOPTICS", Shortname = "SYNOPTICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GENIUS", Shortname = "GENIUS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IOMEGA", Shortname = "IOMEGA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ILFORD", Shortname = "ILFORD" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RIKADENKI", Shortname = "RIKADENKI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "UNISYS", Shortname = "UNISYS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RICOH", Shortname = "RICOH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MINOLTA", Shortname = "MINOLTA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SEAGATE", Shortname = "SEAGATE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MANNESMANN", Shortname = "MANNESMANN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NETWORK PERIPHERALS", Shortname = "NETWORK PERIPHERALS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AGFA", Shortname = "AGFA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KODAK", Shortname = "KODAK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ZETA", Shortname = "ZETA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COLORSPAN", Shortname = "COLORSPAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LANNET", Shortname = "LANNET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NEOLT", Shortname = "NEOLT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IMATION", Shortname = "IMATION" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ETHER PRIME", Shortname = "ETHER PRIME" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TALLY", Shortname = "TALLY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SANYO", Shortname = "SANYO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TEXAS INSTRUMENTS", Shortname = "TEXAS INSTRUMENTS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EXABYTE", Shortname = "EXABYTE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "QUANTUM", Shortname = "QUANTUM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SCITEX", Shortname = "SCITEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LASERMASTER", Shortname = "LASERMASTER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WANGTEK", Shortname = "WANGTEK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DUPONT", Shortname = "DUPONT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EUROPATEC", Shortname = "EUROPATEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MICRONET", Shortname = "MICRONET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ALLIED TELESYN", Shortname = "ALLIED TELESYN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "STAR", Shortname = "STAR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRIUMPH ADLER", Shortname = "TRIUMPH ADLER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GCC", Shortname = "GCC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TANDBERG", Shortname = "TANDBERG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HIGHSCREEN", Shortname = "HIGHSCREEN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EMULEX", Shortname = "EMULEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SUMMAGRAPHICS", Shortname = "SUMMAGRAPHICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MUTOH", Shortname = "MUTOH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GRAPHTEC", Shortname = "GRAPHTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SCHLUMBERGER", Shortname = "SCHLUMBERGER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SCHNEIDER", Shortname = "SCHNEIDER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RASTER GRAPHICS", Shortname = "RASTER GRAPHICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MITA", Shortname = "MITA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SAGEM", Shortname = "SAGEM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MICROSPOT", Shortname = "MICROSPOT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SELEX", Shortname = "SELEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "REX ROTARY", Shortname = "REX ROTARY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MADGE", Shortname = "MADGE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FALCON", Shortname = "FALCON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "XANTE", Shortname = "XANTE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SMC", Shortname = "SMC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ONSTREAM", Shortname = "ONSTREAM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "VIDAR", Shortname = "VIDAR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COMPUPRINT", Shortname = "COMPUPRINT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KONICA", Shortname = "KONICA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AT&T", Shortname = "AT&T" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FACIT", Shortname = "FACIT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ATARI", Shortname = "ATARI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SKYLINK", Shortname = "SKYLINK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SDR", Shortname = "SDR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CHIPCOM", Shortname = "CHIPCOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "APOLLO", Shortname = "APOLLO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OLYMPIA", Shortname = "OLYMPIA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MIMAKI", Shortname = "MIMAKI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NETGEAR", Shortname = "NETGEAR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LEVELONE", Shortname = "LEVELONE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NORTEL", Shortname = "NORTEL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ARTISAN", Shortname = "ARTISAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CREOSCITEX", Shortname = "CREOSCITEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FUJI", Shortname = "FUJI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PINACL", Shortname = "PINACL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CONTEX", Shortname = "CONTEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FIBRONICS", Shortname = "FIBRONICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DATAPRODUCTS", Shortname = "DATAPRODUCTS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COLORTRAC", Shortname = "COLORTRAC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ARCHIVE", Shortname = "ARCHIVE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GFC", Shortname = "GFC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HOUSTON INSTRUMENTS", Shortname = "HOUSTON INSTRUMENTS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SEIKO", Shortname = "SEIKO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EMC", Shortname = "EMC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HDS", Shortname = "HDS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RISO", Shortname = "RISO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AHLBORN", Shortname = "AHLBORN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "UTAX", Shortname = "UTAX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WILSMANN", Shortname = "WILSMANN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LANART", Shortname = "LANART" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "C.ITHO", Shortname = "C.ITHO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CENTRONICS", Shortname = "CENTRONICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CLAXAN", Shortname = "CLAXAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AMSTRAD", Shortname = "AMSTRAD" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BROCADE", Shortname = "BROCADE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MCDATA", Shortname = "MCDATA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INRANGE", Shortname = "INRANGE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BENSON", Shortname = "BENSON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IMC NETWORKS", Shortname = "IMC NETWORKS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LANIER", Shortname = "LANIER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "UNGERMANN-BASS", Shortname = "UNGERMANN-BASS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DAYNA", Shortname = "DAYNA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BOSELAN", Shortname = "BOSELAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KTI", Shortname = "KTI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OZALID", Shortname = "OZALID" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ALPS", Shortname = "ALPS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MAGNUM", Shortname = "MAGNUM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TELINDUS", Shortname = "TELINDUS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MULTITECH", Shortname = "MULTITECH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ELITE", Shortname = "ELITE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SIEMENS", Shortname = "SIEMENS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KINGSTON", Shortname = "KINGSTON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PLANET", Shortname = "PLANET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "XYPLEX", Shortname = "XYPLEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DIGITAL", Shortname = "DIGITAL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SHIVA", Shortname = "SHIVA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "JETFAX", Shortname = "JETFAX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WANG", Shortname = "WANG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NWAY", Shortname = "NWAY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ROWE", Shortname = "ROWE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MUSTEK", Shortname = "MUSTEK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TEVION", Shortname = "TEVION" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TARGA", Shortname = "TARGA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SILVER REED", Shortname = "SILVER REED" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GESTETNER", Shortname = "GESTETNER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CITIZEN", Shortname = "CITIZEN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BARCO", Shortname = "BARCO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NUCOM", Shortname = "NUCOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ARTEC", Shortname = "ARTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NASHUATEC", Shortname = "NASHUATEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "VOBIS", Shortname = "VOBIS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ARCHE", Shortname = "ARCHE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KIP", Shortname = "KIP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WIDECOM", Shortname = "WIDECOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ERGO", Shortname = "ERGO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EAGLE SLI", Shortname = "EAGLE SLI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ASSEMBLATO", Shortname = "ASSEMBLATO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NEWGEN", Shortname = "NEWGEN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SIHL", Shortname = "SIHL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COMPUTE MATE", Shortname = "COMPUTE MATE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RADIO SHACK", Shortname = "RADIO SHACK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FUJITSU", Shortname = "FUJITSU" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MEMOREX TELEX", Shortname = "MEMOREX TELEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FRANCE TELECOM", Shortname = "FRANCE TELECOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HCS INFOTEC", Shortname = "HCS INFOTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "METEM", Shortname = "METEM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EASYCOM", Shortname = "EASYCOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DEX", Shortname = "DEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRENDNET", Shortname = "TRENDNET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INFOTEC", Shortname = "INFOTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LANTECH", Shortname = "LANTECH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SMITH-CORONA", Shortname = "SMITH-CORONA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NRG", Shortname = "NRG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BULL", Shortname = "BULL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INTEGRAL DATA SYSTEMS", Shortname = "INTEGRAL DATA SYSTEMS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TAXAN", Shortname = "TAXAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "UMAX", Shortname = "UMAX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ROBOTRON", Shortname = "ROBOTRON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AEG", Shortname = "AEG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TANDY", Shortname = "TANDY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WILD", Shortname = "WILD" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DIAZO", Shortname = "DIAZO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OLYMPUS", Shortname = "OLYMPUS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SANCO", Shortname = "SANCO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MAXTOR", Shortname = "MAXTOR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "JUKI", Shortname = "JUKI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PACKARD BELL", Shortname = "PACKARD BELL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IRIS", Shortname = "IRIS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NIXDORF", Shortname = "NIXDORF" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SIEMENS-NIXDORF", Shortname = "SIEMENS-NIXDORF" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ORION", Shortname = "ORION" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AEG-OLYMPIA", Shortname = "AEG-OLYMPIA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRANSTEL", Shortname = "TRANSTEL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HONEYWELL BULL", Shortname = "HONEYWELL BULL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MINITEX", Shortname = "MINITEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HSC", Shortname = "HSC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OEM", Shortname = "OEM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EUROTERMINAL", Shortname = "EUROTERMINAL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MITSUBISHI", Shortname = "MITSUBISHI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GRETAG", Shortname = "GRETAG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DYNAMODE", Shortname = "DYNAMODE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RACAL", Shortname = "RACAL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LANMASTER", Shortname = "LANMASTER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HONEYWELL", Shortname = "HONEYWELL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DIGIDEV", Shortname = "DIGIDEV" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ANITECH", Shortname = "ANITECH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ELONEX", Shortname = "ELONEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TEC", Shortname = "TEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MICROLINE", Shortname = "MICROLINE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "VISA", Shortname = "VISA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BT", Shortname = "BT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ADDTRON", Shortname = "ADDTRON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "REM", Shortname = "REM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CASIO", Shortname = "CASIO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FARGO", Shortname = "FARGO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GENERAL PARAMETER", Shortname = "GENERAL PARAMETER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MEMOREX", Shortname = "MEMOREX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RITEMAN", Shortname = "RITEMAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DEVELOP", Shortname = "DEVELOP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WDV", Shortname = "WDV" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COLORADO", Shortname = "COLORADO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HITACHI", Shortname = "HITACHI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "YUKON", Shortname = "YUKON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "C.T.I.", Shortname = "C.T.I." });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LINTEC", Shortname = "LINTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HYUNDAI", Shortname = "HYUNDAI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MINOLTA-QMS", Shortname = "MINOLTA-QMS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "VIGLEN", Shortname = "VIGLEN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OMNITRAC", Shortname = "OMNITRAC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ARCNET", Shortname = "ARCNET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DIMAX", Shortname = "DIMAX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ACSYS", Shortname = "ACSYS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MICROTEK", Shortname = "MICROTEK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HIRSCHMANN", Shortname = "HIRSCHMANN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CONNECTWARE", Shortname = "CONNECTWARE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RAD", Shortname = "RAD" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AMP", Shortname = "AMP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DIAZIT", Shortname = "DIAZIT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "XYLAN", Shortname = "XYLAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OLICOM", Shortname = "OLICOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NEXLAND", Shortname = "NEXLAND" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SAFIR", Shortname = "SAFIR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "VOLKSNET", Shortname = "VOLKSNET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "VOLTAGE", Shortname = "VOLTAGE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COREGA", Shortname = "COREGA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PRIMAX", Shortname = "PRIMAX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NOVA", Shortname = "NOVA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DANKA", Shortname = "DANKA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "REGMA", Shortname = "REGMA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MEMOTECH", Shortname = "MEMOTECH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BLUE CHIP", Shortname = "BLUE CHIP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BRONDI", Shortname = "BRONDI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HERMES", Shortname = "HERMES" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GATEWAY", Shortname = "GATEWAY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ZENITH", Shortname = "ZENITH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AST", Shortname = "AST" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ARTIST", Shortname = "ARTIST" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BOG", Shortname = "BOG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FORE", Shortname = "FORE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TELECOM ITALIA", Shortname = "TELECOM ITALIA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CETRONICS", Shortname = "CETRONICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INTERGRAPH", Shortname = "INTERGRAPH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BTC", Shortname = "BTC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TULIP", Shortname = "TULIP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KN COMPUTER", Shortname = "KN COMPUTER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "Q-JET", Shortname = "Q-JET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BCT", Shortname = "BCT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PANATRONIC", Shortname = "PANATRONIC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CUBIK", Shortname = "CUBIK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PRIME", Shortname = "PRIME" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BOSCH", Shortname = "BOSCH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IMTEC", Shortname = "IMTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PRINTRONIX", Shortname = "PRINTRONIX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TELESYSTEMS", Shortname = "TELESYSTEMS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ADM-TEK", Shortname = "ADM-TEK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ALCATEL", Shortname = "ALCATEL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ANATECH", Shortname = "ANATECH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OSBORNE", Shortname = "OSBORNE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRANSTEC", Shortname = "TRANSTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RENKER", Shortname = "RENKER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NETAPP", Shortname = "NETAPP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ALLNET", Shortname = "ALLNET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COMPEX", Shortname = "COMPEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "XYLOGICS", Shortname = "XYLOGICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ECOKEY", Shortname = "ECOKEY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DITEC", Shortname = "DITEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ZDS", Shortname = "ZDS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TANGENT", Shortname = "TANGENT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IOLINE", Shortname = "IOLINE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRIGEM", Shortname = "TRIGEM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PIONEER", Shortname = "PIONEER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LANTRONIX", Shortname = "LANTRONIX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LUCENT", Shortname = "LUCENT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NETWORK SYSTEMS", Shortname = "NETWORK SYSTEMS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CRAY", Shortname = "CRAY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SUMMIT", Shortname = "SUMMIT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ETHER", Shortname = "ETHER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ESTE", Shortname = "ESTE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OTHER", Shortname = "OTHER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BRUNNING", Shortname = "BRUNNING" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRIMM", Shortname = "TRIMM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NCP", Shortname = "NCP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "APC", Shortname = "APC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FUJIX", Shortname = "FUJIX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EUROLOGIC", Shortname = "EUROLOGIC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "E-NET", Shortname = "E-NET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CAMEO", Shortname = "CAMEO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRUST", Shortname = "TRUST" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AEROMATRIX", Shortname = "AEROMATRIX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LONGSHINE", Shortname = "LONGSHINE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MACROMATE", Shortname = "MACROMATE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "METOPLAN", Shortname = "METOPLAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "UNKNOWN", Shortname = "UNKNOWN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INFONET", Shortname = "INFONET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ASUTEK", Shortname = "ASUTEK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EXTREME NETWORKS", Shortname = "EXTREME NETWORKS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PHOENIX", Shortname = "PHOENIX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FOCUS NETWORKS", Shortname = "FOCUS NETWORKS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NEWBRIDGE", Shortname = "NEWBRIDGE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ACROBAT", Shortname = "ACROBAT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GARRETTCOM", Shortname = "GARRETTCOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MOUNTAIN", Shortname = "MOUNTAIN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INMAC", Shortname = "INMAC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DELTACOM", Shortname = "DELTACOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SIECOR", Shortname = "SIECOR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FOREST", Shortname = "FOREST" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MTI", Shortname = "MTI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LINKPRO", Shortname = "LINKPRO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NPI", Shortname = "NPI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MAXDATA", Shortname = "MAXDATA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "JDL", Shortname = "JDL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COMPU-SHACK", Shortname = "COMPU-SHACK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COMPAREX", Shortname = "COMPAREX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "UNEX", Shortname = "UNEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ADIC", Shortname = "ADIC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MOTOROLA", Shortname = "MOTOROLA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ADP", Shortname = "ADP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TCI", Shortname = "TCI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "1DUMMYKAMERA", Shortname = "1DUMMYKAMERA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LSI", Shortname = "LSI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "4LAN", Shortname = "4LAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MICROSOLUTIONS", Shortname = "MICROSOLUTIONS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DSI", Shortname = "DSI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRANSITION NETWORKS", Shortname = "TRANSITION NETWORKS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "POWERWARE", Shortname = "POWERWARE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "STORAGETEK", Shortname = "STORAGETEK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GRAND JUNCTION", Shortname = "GRAND JUNCTION" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MACSENSE", Shortname = "MACSENSE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ROTRONIC", Shortname = "ROTRONIC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DATA GENERAL", Shortname = "DATA GENERAL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NETWORK APLIANCE", Shortname = "NETWORK APLIANCE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RELIA", Shortname = "RELIA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FUJIFILM", Shortname = "FUJIFILM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ALGOTEX", Shortname = "ALGOTEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AOS", Shortname = "AOS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NALCOM", Shortname = "NALCOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OSICOM", Shortname = "OSICOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ISOLAN", Shortname = "ISOLAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CSP", Shortname = "CSP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WHITTAKER", Shortname = "WHITTAKER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NORTH HILLS", Shortname = "NORTH HILLS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AIWA", Shortname = "AIWA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ONLINE", Shortname = "ONLINE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MGE UPS SYSTEMS", Shortname = "MGE UPS SYSTEMS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TERRA", Shortname = "TERRA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "VARIOTRONICS", Shortname = "VARIOTRONICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CANARY", Shortname = "CANARY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FARALLON", Shortname = "FARALLON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ENTERASYS", Shortname = "ENTERASYS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MAXSERVER", Shortname = "MAXSERVER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MITSUMI", Shortname = "MITSUMI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AOPEN", Shortname = "AOPEN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TECMAR", Shortname = "TECMAR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OVERLAND", Shortname = "OVERLAND" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ARGO", Shortname = "ARGO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CINET", Shortname = "CINET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WANGDAT", Shortname = "WANGDAT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DAVID SYSTEMS", Shortname = "DAVID SYSTEMS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SYSKONNECT", Shortname = "SYSKONNECT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LANOPTICS", Shortname = "LANOPTICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TC", Shortname = "TC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ARP DATACON", Shortname = "ARP DATACON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INFOCUS", Shortname = "INFOCUS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HUGHES", Shortname = "HUGHES" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ASK", Shortname = "ASK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ICL", Shortname = "ICL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TELEKOM", Shortname = "TELEKOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BEST TECHNOLOGIES", Shortname = "BEST TECHNOLOGIES" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ATL PRODUCTS", Shortname = "ATL PRODUCTS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "REPOTEC", Shortname = "REPOTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IPC", Shortname = "IPC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CPT", Shortname = "CPT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EM ELECTRONICS", Shortname = "EM ELECTRONICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MURATA", Shortname = "MURATA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NETWORK", Shortname = "NETWORK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SAFEWAY", Shortname = "SAFEWAY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GENERAL ELECTRIC", Shortname = "GENERAL ELECTRIC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ANDREW", Shortname = "ANDREW" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PRONET", Shortname = "PRONET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GALCOM", Shortname = "GALCOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BATM", Shortname = "BATM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "UB NETWORKS", Shortname = "UB NETWORKS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SCORPION", Shortname = "SCORPION" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "POWERCOMP", Shortname = "POWERCOMP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KENTEK", Shortname = "KENTEK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FOUNDRY NETWORKS", Shortname = "FOUNDRY NETWORKS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WELLFLEET", Shortname = "WELLFLEET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CITAM", Shortname = "CITAM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PUREDATA", Shortname = "PUREDATA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BELGACOM", Shortname = "BELGACOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RUNJIANG", Shortname = "RUNJIANG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TC PLUS", Shortname = "TC PLUS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EVEREX", Shortname = "EVEREX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BINTEC", Shortname = "BINTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "A+K", Shortname = "A+K" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ATEN", Shortname = "ATEN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CELAN", Shortname = "CELAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FREECOM", Shortname = "FREECOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SYQUEST", Shortname = "SYQUEST" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "XYRATEX", Shortname = "XYRATEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DIATEC", Shortname = "DIATEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DIGITUS", Shortname = "DIGITUS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LIEBERT", Shortname = "LIEBERT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NORDIC DATA COM", Shortname = "NORDIC DATA COM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ROLINE", Shortname = "ROLINE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LINKSYS", Shortname = "LINKSYS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PLUSTEK", Shortname = "PLUSTEK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PCPI", Shortname = "PCPI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ETHERWAN", Shortname = "ETHERWAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "POWERTRONIX", Shortname = "POWERTRONIX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OLIDATA", Shortname = "OLIDATA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "POWERCOM", Shortname = "POWERCOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PLASMON", Shortname = "PLASMON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ERREPI", Shortname = "ERREPI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "QLOGIC", Shortname = "QLOGIC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MRV", Shortname = "MRV" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DORO", Shortname = "DORO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CMC", Shortname = "CMC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GEHA", Shortname = "GEHA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LANCONNECTION", Shortname = "LANCONNECTION" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EMC/DELL", Shortname = "EMC/DELL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DUPLO", Shortname = "DUPLO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "YAMAHA", Shortname = "YAMAHA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "POLAROID", Shortname = "POLAROID" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "VICTRON", Shortname = "VICTRON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MAYNARD", Shortname = "MAYNARD" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AVAYA", Shortname = "AVAYA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MEMOIRE VIVE", Shortname = "MEMOIRE VIVE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NO NAME", Shortname = "NO NAME" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WYSE", Shortname = "WYSE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CONCEN", Shortname = "CONCEN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ICP", Shortname = "ICP" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GADZOOX", Shortname = "GADZOOX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TECO", Shortname = "TECO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "METASYSTEM", Shortname = "METASYSTEM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BELKIN", Shortname = "BELKIN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "JMR", Shortname = "JMR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MANHATTAN", Shortname = "MANHATTAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SOCOMEC", Shortname = "SOCOMEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "N. SICON INDIA ", Shortname = "N. SICON INDIA " });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SVEC", Shortname = "SVEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SYNERGY 21", Shortname = "SYNERGY 21" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NETOPIA", Shortname = "NETOPIA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TECNOWARE", Shortname = "TECNOWARE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COMPU-DIF", Shortname = "COMPU-DIF" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "X-NET", Shortname = "X-NET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SYMANTEC", Shortname = "SYMANTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CONNER", Shortname = "CONNER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "Q-TEC", Shortname = "Q-TEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "U.S.ROBOTICS", Shortname = "U.S.ROBOTICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LANPRO", Shortname = "LANPRO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EMI", Shortname = "EMI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PHEENET", Shortname = "PHEENET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MITSUSAKI", Shortname = "MITSUSAKI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FISKARS", Shortname = "FISKARS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DELTEC", Shortname = "DELTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BEST POWER TECHNOLOGY", Shortname = "BEST POWER TECHNOLOGY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OPTROX", Shortname = "OPTROX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "REVSCAN", Shortname = "REVSCAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TELDAT", Shortname = "TELDAT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SALDAB", Shortname = "SALDAB" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MAESTRO ", Shortname = "MAESTRO " });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CANADA", Shortname = "CANADA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CYBEX", Shortname = "CYBEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TALLGRASS", Shortname = "TALLGRASS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ZYXEL", Shortname = "ZYXEL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "REALTECH", Shortname = "REALTECH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KENTROX", Shortname = "KENTROX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LINDY", Shortname = "LINDY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BLACK BOX", Shortname = "BLACK BOX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RADWARE", Shortname = "RADWARE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ARLOTTO", Shortname = "ARLOTTO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SWEEX", Shortname = "SWEEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NCR", Shortname = "NCR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OPENET ICS", Shortname = "OPENET ICS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ARTISOFT", Shortname = "ARTISOFT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INTELLINET", Shortname = "INTELLINET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TIARA", Shortname = "TIARA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DGI", Shortname = "DGI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KPN", Shortname = "KPN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AARQUE", Shortname = "AARQUE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EVERGREEN", Shortname = "EVERGREEN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INFOBYTE", Shortname = "INFOBYTE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NETWORK ENGINES", Shortname = "NETWORK ENGINES" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TARGET", Shortname = "TARGET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IRWIN", Shortname = "IRWIN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "Andere merken", Shortname = "Andere merken" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "Clone maschines ", Shortname = "Clone maschines " });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PALM-HUB", Shortname = "PALM-HUB" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SOHO", Shortname = "SOHO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WEBER", Shortname = "WEBER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OPTIMUS", Shortname = "OPTIMUS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MICRODOWELL", Shortname = "MICRODOWELL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AVOCENT", Shortname = "AVOCENT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PURELAN", Shortname = "PURELAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LAN RESEARCH", Shortname = "LAN RESEARCH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GAZELLE", Shortname = "GAZELLE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "THOMAS CONRAD", Shortname = "THOMAS CONRAD" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "APUS", Shortname = "APUS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SPECIALIX", Shortname = "SPECIALIX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ACS", Shortname = "ACS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NUR", Shortname = "NUR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DELTA", Shortname = "DELTA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RUBYTECH", Shortname = "RUBYTECH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HBO", Shortname = "HBO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GREYCOM", Shortname = "GREYCOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CLARINET", Shortname = "CLARINET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RCE", Shortname = "RCE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IPTIME", Shortname = "IPTIME" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TELEWELL", Shortname = "TELEWELL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TEKCOMM", Shortname = "TEKCOMM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COPYER CO", Shortname = "COPYER CO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MAXIVISION", Shortname = "MAXIVISION" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TECH JET", Shortname = "TECH JET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GANDALF", Shortname = "GANDALF" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "QUALSTAR", Shortname = "QUALSTAR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CREO", Shortname = "CREO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DILOG", Shortname = "DILOG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DACOMEX", Shortname = "DACOMEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DM-ELECTRONIC", Shortname = "DM-ELECTRONIC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PCI", Shortname = "PCI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RIGOLI FIME", Shortname = "RIGOLI FIME" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "POMI", Shortname = "POMI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ABACOM", Shortname = "ABACOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LIBRA", Shortname = "LIBRA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DIGITAL NETWORK", Shortname = "DIGITAL NETWORK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PRIMERA", Shortname = "PRIMERA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MAYNSTREAM", Shortname = "MAYNSTREAM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TYCO", Shortname = "TYCO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SEQUENT", Shortname = "SEQUENT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NETWORX", Shortname = "NETWORX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NET LYNX", Shortname = "NET LYNX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FLAMINGO", Shortname = "FLAMINGO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DAKOTA", Shortname = "DAKOTA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "IMV", Shortname = "IMV" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LANCAST", Shortname = "LANCAST" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "UNIFORM", Shortname = "UNIFORM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MATAN", Shortname = "MATAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NORTEL NETWORK", Shortname = "NORTEL NETWORK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "METASTOR", Shortname = "METASTOR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ANY MANUFACTURE", Shortname = "ANY MANUFACTURE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "JVC", Shortname = "JVC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LG", Shortname = "LG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRAKKER", Shortname = "TRAKKER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SUNTEK", Shortname = "SUNTEK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RIELLO", Shortname = "RIELLO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SSR", Shortname = "SSR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NDC", Shortname = "NDC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRANGTEC", Shortname = "TRANGTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EUSSO ", Shortname = "EUSSO " });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PEABIRD", Shortname = "PEABIRD" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CENT", Shortname = "CENT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SITECOM", Shortname = "SITECOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "WINGS", Shortname = "WINGS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OPTIUPS", Shortname = "OPTIUPS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LACIE", Shortname = "LACIE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AMIGO", Shortname = "AMIGO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DIVERSE", Shortname = "DIVERSE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INALP NETWORKS", Shortname = "INALP NETWORKS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SUPERMICRO", Shortname = "SUPERMICRO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CLONICO", Shortname = "CLONICO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GEMBIRD", Shortname = "GEMBIRD" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GERBER", Shortname = "GERBER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FORTECH", Shortname = "FORTECH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CONNECT TECH", Shortname = "CONNECT TECH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TP-LINK", Shortname = "TP-LINK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LEGATO", Shortname = "LEGATO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MICROBOX", Shortname = "MICROBOX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "METEOR", Shortname = "METEOR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MULTICO", Shortname = "MULTICO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ETEN", Shortname = "ETEN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TELESYN", Shortname = "TELESYN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SERVELINUX", Shortname = "SERVELINUX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HAMLET", Shortname = "HAMLET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EXCEL", Shortname = "EXCEL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CENTRECOM", Shortname = "CENTRECOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SPEEDSTREAM", Shortname = "SPEEDSTREAM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "THOMSON", Shortname = "THOMSON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MFK", Shortname = "MFK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MEDACOM", Shortname = "MEDACOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SPIDER SYSTEMS", Shortname = "SPIDER SYSTEMS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ITEK", Shortname = "ITEK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MITSUMA", Shortname = "MITSUMA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OPTIMA TECH.SOL", Shortname = "OPTIMA TECH.SOL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "VOLITION", Shortname = "VOLITION" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MENTOR", Shortname = "MENTOR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TECWIN", Shortname = "TECWIN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DATADIRECT NET.", Shortname = "DATADIRECT NET." });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ALLIED TELESIS", Shortname = "ALLIED TELESIS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ZEBRA", Shortname = "ZEBRA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SICON SOCOMEC", Shortname = "SICON SOCOMEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MERU NETWORKS", Shortname = "MERU NETWORKS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RUCKUS", Shortname = "RUCKUS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SYMBOL", Shortname = "SYMBOL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRAPEZE", Shortname = "TRAPEZE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRAPEZE NETWORK", Shortname = "TRAPEZE NETWORK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AEROHIVE", Shortname = "AEROHIVE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ARUBA NETWORKS", Shortname = "ARUBA NETWORKS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BLUE SOCKET", Shortname = "BLUE SOCKET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "FIRETIDE", Shortname = "FIRETIDE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INTERMEC", Shortname = "INTERMEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COLUBRIS", Shortname = "COLUBRIS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BELAIR", Shortname = "BELAIR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MICROLINK", Shortname = "MICROLINK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TYAN", Shortname = "TYAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INTEL EXPRESS", Shortname = "INTEL EXPRESS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "COMBI", Shortname = "COMBI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DTK", Shortname = "DTK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LENOVO", Shortname = "LENOVO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ATB", Shortname = "ATB" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NILOX", Shortname = "NILOX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MAXTRON", Shortname = "MAXTRON" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MICRO ONE", Shortname = "MICRO ONE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GAB GEMINI", Shortname = "GAB GEMINI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GEFEN INC.", Shortname = "GEFEN INC." });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "APPRO", Shortname = "APPRO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CONCEPTTRONIC", Shortname = "CONCEPTTRONIC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AMT DATASOUTH", Shortname = "AMT DATASOUTH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ADAPTEC", Shortname = "ADAPTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CHASE", Shortname = "CHASE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PROXIM", Shortname = "PROXIM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LECTRA", Shortname = "LECTRA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "YUNTO", Shortname = "YUNTO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DURST", Shortname = "DURST" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TENOVIS", Shortname = "TENOVIS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TECKWIN", Shortname = "TECKWIN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PROFI COMPUTER", Shortname = "PROFI COMPUTER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "M.A.C.", Shortname = "M.A.C." });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ZUND", Shortname = "ZUND" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LOGITECH", Shortname = "LOGITECH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SUMMA", Shortname = "SUMMA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "POLICROM", Shortname = "POLICROM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GRUNDIG", Shortname = "GRUNDIG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "D.GEN", Shortname = "D.GEN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DYMO", Shortname = "DYMO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "W7200=36", Shortname = "W7200=36" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DESIGNJET CC800", Shortname = "DESIGNJET CC800" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CADET", Shortname = "CADET" });            
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SKY COLOR", Shortname = "SKY COLOR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BIOGRAPH", Shortname = "BIOGRAPH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "VUTEK", Shortname = "VUTEK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SALICRU", Shortname = "SALICRU" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ZIGOR", Shortname = "ZIGOR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RECO", Shortname = "RECO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ADPOS", Shortname = "ADPOS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AUGEND", Shortname = "AUGEND" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "STRATASYS", Shortname = "STRATASYS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "INFINITI", Shortname = "INFINITI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LIYU", Shortname = "LIYU" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KYOCERA-MITA", Shortname = "KYOCERA-MITA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "XEROX FX", Shortname = "XEROX FX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KEUNDO", Shortname = "KEUNDO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "3DSYSTEMS", Shortname = "3DSYSTEMS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ALCATEL-LUCENT", Shortname = "ALCATEL-LUCENT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "JUNIPER", Shortname = "JUNIPER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EONSTOR", Shortname = "EONSTOR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "RADIANT", Shortname = "RADIANT" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "POSIFLEX", Shortname = "POSIFLEX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DIGIPOS", Shortname = "DIGIPOS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ECR", Shortname = "ECR" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "STEGENWALNER", Shortname = "STEGENWALNER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GONGZHENG", Shortname = "GONGZHENG" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TALLYGENICOM", Shortname = "TALLYGENICOM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LEFTHAND", Shortname = "LEFTHAND" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DANCALL", Shortname = "DANCALL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ALLWIN", Shortname = "ALLWIN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SYNOLOGY", Shortname = "SYNOLOGY" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ORACLE", Shortname = "ORACLE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SPECTRALOGIC", Shortname = "SPECTRALOGIC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HUAWEI", Shortname = "HUAWEI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DOTHILL", Shortname = "DOTHILL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "KONICA MINOLTA", Shortname = "KONICA MINOLTA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OKI PRINTING SOLUTIONS", Shortname = "OKI PRINTING SOLUTIONS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "OKIDATA", Shortname = "OKIDATA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MERLIN GERIN", Shortname = "MERLIN GERIN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LECAI", Shortname = "LECAI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "NVIDIA", Shortname = "NVIDIA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ASBRU", Shortname = "ASBRU" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "PANTUM", Shortname = "PANTUM" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ATL", Shortname = "ATL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DATA DOMAIN", Shortname = "DATA DOMAIN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MECER", Shortname = "MECER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "CARETTA", Shortname = "CARETTA" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SPEEDJET", Shortname = "SPEEDJET" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HYRICAN", Shortname = "HYRICAN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "BLUEDISK", Shortname = "BLUEDISK" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "SHUTTLE", Shortname = "SHUTTLE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "LYNX", Shortname = "LYNX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TAROX", Shortname = "TAROX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MEDION", Shortname = "MEDION" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AMILO", Shortname = "AMILO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "STONE", Shortname = "STONE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ITALIANO", Shortname = "ITALIANO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ONE", Shortname = "ONE" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GANDINOVATIONS", Shortname = "GANDINOVATIONS" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "MSI", Shortname = "MSI" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "DEPO", Shortname = "DEPO" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "GREY COMPUTER", Shortname = "GREY COMPUTER" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "TRONIC 5", Shortname = "TRONIC 5" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ITALIANORAINBOX", Shortname = "ITALIANORAINBOX" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "ANTEC", Shortname = "ANTEC" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HEDEN", Shortname = "HEDEN" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "HERCULES", Shortname = "HERCULES" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "XYXEL", Shortname = "XYXEL" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "EUROTECH", Shortname = "EUROTECH" });
            db.Manufacturers.Add(new Manufacturer { ExternalCode = "AURA ECOTECH", Shortname = "AURA ECOTECH" });
            #endregion

            var categories = Categories(db);

            #region products
            
            string username = "system";
            DateTime createdAt = DateTime.UtcNow.Date;

            // notebooks
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Computers_Notebooks, ProductNumber = "NBK1234", ShortName = "Elitebook 480p", IsActive = true, CreatedAt = createdAt, CreatedBy = username });
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Computers_Notebooks, ProductNumber = "NBO3332", ShortName = "Elitebook 720p", IsActive = true, CreatedAt = createdAt, CreatedBy = username });
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Computers_Notebooks, ProductNumber = "XXB1479", ShortName = "Elitebook 1020x", IsActive = true, CreatedAt = createdAt, CreatedBy = username });

            // servers
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Computers_Servers, ProductNumber = "SRV9837", ShortName = "Proliant X110", IsActive = true, CreatedAt = createdAt, CreatedBy = username });
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Computers_Servers, ProductNumber = "SRV3326", ShortName = "Proliant X230", IsActive = true, CreatedAt = createdAt, CreatedBy = username });

            // switches
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Networking_Switches, ProductNumber = "NSW7771", ShortName = "1820 Switch", IsActive = true, CreatedAt = createdAt, CreatedBy = username });
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Networking_Switches, ProductNumber = "XSW3288", ShortName = "FlexFabric 5900CP Switch", IsActive = true, CreatedAt = createdAt, CreatedBy = username });
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Networking_Switches, ProductNumber = "YSI1655", ShortName = "FlexFabric 7900 Switch", IsActive = true, CreatedAt = createdAt, CreatedBy = username });
                        
            // mfp
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Printers_Consumer, ProductNumber = "PPA1123", ShortName = "OfficeJet 33X", IsActive = true, CreatedAt = createdAt, CreatedBy = username });
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Printers_Consumer, ProductNumber = "PPA6923", ShortName = "Envy 4500", IsActive = true, CreatedAt = createdAt, CreatedBy = username });

            // printers
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Printers_Consumer, ProductNumber = "LJA1123", ShortName = "LaserJet 330p", IsActive = true, CreatedAt = createdAt, CreatedBy = username });
            db.Products.Add(new Product { Manufacturer = hp, Category = categories.Printers_Consumer, ProductNumber = "LJX2399", ShortName = "LaserJet 1020x", IsActive = true, CreatedAt = createdAt, CreatedBy = username });

            #endregion
        }

        class StandardCategory
        {
            public Category Computers { get; set; }

            public Category Computers_Notebooks { get; set; }

            public Category Computers_Servers { get; set; }

            public Category Printers { get; set; }

            public Category Printers_Consumer { get; set; }

            public Category Printers_Industrial { get; set; }

            public Category Printers_AnalogLf { get; set; }

            public Category Printers_Inkjet { get; set; }

            public Category Printers_Laser { get; set; }

            public Category Printers_Plotter { get; set; }

            public Category Networking_Routers { get; set; }

            public Category Networking_Switches { get; set; }
        }
    }
}

