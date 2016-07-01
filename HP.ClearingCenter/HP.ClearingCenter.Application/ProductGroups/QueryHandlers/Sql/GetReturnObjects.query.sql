SELECT			c.Id AS CategoryID
				, c.ExternalCode AS CategoryExternalCode
				, c.ShortName AS CategoryName

				, m.Id AS ManufacturerId
				, m.ExternalCode AS ManufacturerExternalCode
				, m.Shortname AS ManufacturerName

				, p.Id AS ProductId
				, p.ShortName AS ProductName
				, p.ProductNumber AS ProductNumber
				, p.[Length] 
				, p.[Width] 
				, p.Height
				, p.LengthUnit
				, p.[Weight]
				, p.WeightUnit

FROM			[pdb].[T_Product] p
				INNER JOIN [pdb].[T_Manufacturer] m ON p.Manufacturer_Id = m.Id
				INNER JOIN [pdb].[T_Category] c ON p.Category_Id = c.Id 

				