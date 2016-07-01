using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HP.ClearingCenter.Application
{
    public class Page
    {
        public static Page Texts { get; set; }

        #region common

        public string SubmitButton { get; set; }

        public string SearchButton { get; set; }

        public string RemoveButton { get; set; }

        public string CancelButton { get; set; }

        public string SaveButton { get; set; }

        public string ApplicationTitle { get; set; }

        public string Logout { get; set; }

        public string PrivacyStatement { get; set; }

        public string TermsOfUse { get; set; }

        public string Actions { get; set; }

        public string Actions_Details { get; set; }

        #endregion 

        #region menu

        public string Menu_Transactions { get; set; }
        public string Menu_TransactionsSubtitle { get; set; }
        public string Menu_ProductDatabase { get; set; }
        public string Menu_ProductDatabaseSubtitle { get; set; }
        public string Menu_Add { get; set; }
        public string Menu_Manage { get; set; }
        public string Menu_Categories { get; set; }
        public string Menu_CustomAttributes { get; set; }
        public string Menu_Manufacturers { get; set; }
        public string Menu_Products { get; set; }
        public string Menu_ProductGroups { get; set; }
        public string Menu_ProductGroupsSubtitle { get; set; }

        #endregion

        #region signin

        public string SignIn_TitleText { get; set; }

        public string SignIn_IntroText { get; set; }

        public string SignIn_Button { get; set; }

        #endregion

        #region receiving

        public string Receiving_TitleText { get; set; }

        public string Receiving_IntroText { get; set; }

        #endregion

        #region clearing

        public string Clearing_TitleText { get; set; }

        public string Clearing_IntroText { get; set; }

        public string Clearing_SelectedProduct { get; set; }

        public string Clearing_SelectedProductRemarks { get; set; }

        public string Clearing_ChangeSelectedProduct { get; set; }

        public string Clearing_ManualStatusOverrideText { get; set; }

        #endregion

        #region transaction search

        public string TransactionSearch_TitleText { get; set; }

        public string TransactionSearch_IntroText { get; set; }

        public string TransactionSearch_TransportOrQuoteNumber { get; set; }

        public string TransactionSearch_ReceivingDetails { get; set; }

        public string TransactionSearch_ClearingDetails { get; set; }

        #endregion

        #region product group search

        public string ProductGroupsSearch_TitleText { get; set; }
        public string ProductGroupsSearch_IntroText { get; set; }
        public string ProductGroupsSearch_ExternalCode { get; set; }
        public string ProductGroupsSearch_MarketingProgram { get; set; }
        public string ProductGroupsSearch_ShortName { get; set; }
        

        #endregion

        #region product group details

        public string ProductGroupDetails_TitleText { get; set; }
        public string ProductGroupDetails_IntroText { get; set; }
        public string ProductGroupDetails_BasicData { get; set; }
        public string ProductGroupDetails_CategoryFilters { get; set; }
        public string ProductGroupDetails_RemoveAttributeFilter { get; set; }
        public string ProductGroupDetails_AddCategoryButton { get; set; }
        public string ProductGroupDetails_AddFilter { get; set; }

        #endregion

        #region manufacturers search

        public string ManufacturersSearch_TitleText { get; set; }
        public string ManufacturersSearch_IntroText { get; set; }
        public string ManufacturersSearch_Name { get; set; }
        public string ManufacturersSearch_ExternalCode { get; set; }

        #endregion

        #region Manufacturer Details

        public string ManufacturerDetails_TitleText { get; set; }
        public string ManufacturerDetails_IntroText { get; set; }

        #endregion

        #region Custom attributes search

        public string CustomAttributesSearch_TitleText { get; set; }
        public string CustomAttributesSearch_IntroText { get; set; }
        public string CustomAttributesSearch_ShortCode { get; set; }
        public string CustomAttributesSearch_ShortName { get; set; }
        public string CustomAttributesSearch_Unit { get; set; }
        public string CustomAttributesSearch_DataType { get; set; }

        #endregion

        #region custom attribute details

        public string CustomAttributeDetails_TitleText { get; set; }
        public string CustomAttributeDetails_IntroText { get; set; }

        #endregion

        #region categories search

        public string CategoriesSearch_TitleText { get; set; }
        public string CategoriesSearch_IntroText { get; set; }
        public string CategoriesSearch_CategoryShortCode { get; set; }
        public string CategoriesSearch_ShortName { get; set; }
        public string CategoriesSearch_ParentCategory { get; set; }

        #endregion 

        #region category details

        public string CategoryDetails_TitleText { get; set; }
        public string CategoryDetails_IntroText { get; set; }
        public string CategoryDetails_AddCustomAttributeButton { get; set; }
        public string CategoryDetails_CategoryAttributes { get; set; }
        public string CategoryDetails_ExternalCode { get; set; }
        public string CategoryDetails_ShortName { get; set; }
        public string CategoryDetails_DataType { get; set; }
        public string CategoryDetails_IsInherited { get; set; }        

        #endregion 

        #region products search

        public string ProductsSearch_TitleText { get; set; }
        public string ProductsSearch_IntroText { get; set; }
        public string ProductsSearch_ProductName { get; set; }
        public string ProductsSearch_Category { get; set; }

        #endregion

        #region product details

        public string ProductDetails_TitleText { get; set; }
        public string ProductDetails_IntroText { get; set; }
        public string ProductDetails_SaveAttributes { get; set; }

        #endregion
    }
}
