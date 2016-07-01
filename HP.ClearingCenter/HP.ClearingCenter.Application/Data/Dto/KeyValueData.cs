using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application.Data.Dto
{
    public class KeyValueData
    {
        public readonly static IDictionary<string, string> LengthUnits = new Dictionary<string, string>
        {
            {"mm", "Millimeters (Metric)"},
            {"cm", "Centimeters (Metric)"},
            {"m", "Meters (Metric)"},
            {"km", "Kilometers (Metric)"},            
            {"in", "Inches (Imperial)"},
            {"ft", "Feet (Imperial)"},
            {"mi", "Miles (Imperial)"},
        };

        public readonly static IDictionary<string, string> WeightUnits = new Dictionary<string, string>
        {
            {"mg", "Milligrams (Metric)"},
            {"g", "Grams (Metric)"},
            {"kg", "Kilograms (Metric)"},
            {"t", "Metric Tonnes (Metric)"},
            {"oz", "Ounces (Imperial)"},
            {"lb", "Pounds (Imperial)"},
        };

        public string DisplayText { get; set; }
        public string ValueText { get; set; }
        public string TranslatorShortcut { get; set; }
    }
}
