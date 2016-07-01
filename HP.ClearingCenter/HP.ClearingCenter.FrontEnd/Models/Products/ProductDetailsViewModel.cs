using HP.ClearingCenter.Application.Data.Dto;
using HP.ClearingCenter.Application.Products.Commands;
using HP.ClearingCenter.Application.Products.Queries;
using HP.ClearingCenter.Infrastructure.Contracts;
using HP.ClearingCenter.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HP.ClearingCenter.FrontEnd.Models.Products
{
    public class ProductDetailsViewModel
    {
        private IApplicationContext app;
        
        public ProductDetailsViewModel(IApplicationContext app)
        {
            this.app = app;
            this.AddOrUpdateProductCommand = new AddOrUpdateProductCommand();
            this.SubmitProductAttributesCommand = new SubmitProductAttributesCommand();
        }

        public AddOrUpdateProductCommand AddOrUpdateProductCommand { get; private set; }
        public SubmitProductAttributesCommand SubmitProductAttributesCommand { get; private set; }
        public bool IsAddNew { get; private set; }
        public IEnumerable<SelectListItem> Manufacturers { get; private set; }
        public IEnumerable<CategoryData> Categories { get; private set; }
        public IEnumerable<SelectListItem> Countries { get; private set; }
        public IEnumerable<SelectListItem> Years { get; private set; }
        public IEnumerable<SelectListItem> LengthUnits { get; private set; }
        public IEnumerable<SelectListItem> WeightUnits { get; private set; }
        public CustomAttributeData[] CategoryAttributes { get; private set; }
        public IDictionary<string, ProductCustomAttributeValueData> CustomAttributeValues { get; private set; }

        public bool IsProductFound { get; private set; }

        public ProductDetailsViewModel Initialize(int productId)
        {
            var pd = this.app
                .PerformQuery(new SimpleSearchProductsQuery { ProductId = productId })
                .FirstOrDefault();

            if (pd.Exists())
            {
                var cmd = this.AddOrUpdateProductCommand;
                cmd.ProductId = pd.ProductId.GetValueOrDefault();
                cmd.ShortName = pd.ProductName;
                cmd.ProductNumber = pd.ProductNumber;
                cmd.Description = pd.Description;
                cmd.ManufacturerId = pd.ManufacturerId;
                cmd.CategoryId = pd.CategoryId;
                cmd.Length = pd.Length;
                cmd.Width = pd.Width;
                cmd.Height = pd.Height;
                cmd.LengthUnit = pd.LengthUnit;
                cmd.Weight = pd.Weight;
                cmd.WeightUnit = pd.WeightUnit;
                cmd.YearOfConstruction = pd.YearOfConstruction;
                cmd.OriginCountryIsoCode = pd.OriginCountryIsoCode;
                cmd.IsActive = pd.IsActive;

                this.SubmitProductAttributesCommand.ProductId = cmd.ProductId;
                this.InitializeDependencies(pd);
                this.SetupProductAttributes(pd);
                this.IsProductFound = true;
            }

            return this;
        }

        public ProductDetailsViewModel Initialize(AddOrUpdateProductCommand command)
        {
            this.IsAddNew = true;
            this.AddOrUpdateProductCommand = command;            
            this.InitializeDependencies();
            return this;
        }

        private void InitializeDependencies(ProductData pd = null)
        {
            int selectedManufacturerId = pd.Exists() ? pd.ManufacturerId : 0;
            string selectedCountry = pd.Exists() ? pd.OriginCountryIsoCode : string.Empty;
            int? selectedYear = pd.Exists() ? pd.YearOfConstruction : null;
            string selectedLengthUnit = pd.Exists() ? pd.LengthUnit : string.Empty;
            string selectedWeightUnit = pd.Exists() ? pd.WeightUnit : string.Empty;
            
            this.Manufacturers = this.app
                .PerformQuery(new SearchManufacturersQuery { MaxRows = int.MaxValue })
                .Select(x => new SelectListItem {
                  Text = x.Shortname,
                  Value = x.Id.ToString(),
                  Selected = x.Id == selectedManufacturerId
                });

            this.Categories = this.app
                .PerformQuery(new ProductCategorySearchQuery { MaxResults = int.MaxValue });
            
            this.BuildCountries(selectedCountry);
            this.BuildYearList(selectedYear);
            this.BuildLengthUnits(selectedLengthUnit);
            this.BuildWeightUnits(selectedWeightUnit);
        }

        private void SetupProductAttributes(ProductData pd)
        {
            if (pd == null) return;

            this.CategoryAttributes = this.app
                .PerformQuery(new GetCategoryAttributesQuery { CategoryId = pd.CategoryId })
                .ToArray();

            this.CustomAttributeValues = this.app
                .PerformQuery(new GetProductAttributeValuesQuery { ProductId = pd.ProductId.GetValueOrDefault(), CustomAttributeExternalCodes = this.CategoryAttributes.Select(x => x.ExternalCode) })
                .Select(x => new { x.CustomAttributeExternalCode, Value = x })
                .ToDictionary(x => x.CustomAttributeExternalCode, x => x.Value);
        }

        private void BuildWeightUnits(string selectedWeightUnit)
        {
            var weightUnitList = new List<SelectListItem>();
            weightUnitList.Add(new SelectListItem { Text = string.Empty, Value = string.Empty });
            foreach (var key in KeyValueData.WeightUnits.Keys)
            {
                weightUnitList.Add(new SelectListItem { Text = KeyValueData.WeightUnits[key], Value = key, Selected = selectedWeightUnit == key });
            }
            this.WeightUnits = weightUnitList;
        }

        private void BuildLengthUnits(string selectedLengthUnit)
        {
            var lengthUnitList = new List<SelectListItem>();
            lengthUnitList.Add(new SelectListItem { Text = string.Empty, Value = string.Empty });
            foreach (var key in KeyValueData.LengthUnits.Keys)
            {
                lengthUnitList.Add(new SelectListItem { Text = KeyValueData.LengthUnits[key], Value = key, Selected = selectedLengthUnit == key });
            }
            this.LengthUnits = lengthUnitList;
        }

        private void BuildYearList(int? selectedYear)
        {
            var yearList = new List<SelectListItem>();
            for (int yr = 1960; yr <= DateTime.UtcNow.Year + 1; yr++)
            {
                string val = yr.ToString();
                yearList.Add(new SelectListItem { Text = val, Value = val, Selected = yr == selectedYear });
            }
            yearList.Insert(0, new SelectListItem { Value = "0", Text = string.Empty, Selected = selectedYear == null });
            this.Years = yearList;
        }

        private void BuildCountries(string selectedCountry)
        {
            var countryList = new List<SelectListItem>(CountryData.All
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.IsoCode,
                    Selected = x.IsoCode == selectedCountry
                }));

            countryList.Insert(0, new SelectListItem { Value = String.Empty, Text = String.Empty, Selected = selectedCountry == string.Empty });
            this.Countries = countryList;
        }
    }
}