SELECT		p.Id AS Product_Id
FROM		(
				SELECT		p.Id
				FROM		[pdb].T_Product p
							INNER JOIN [pdb].T_Category c ON p.Category_Id = c.Id
				WHERE		1 = 1
							[[product_category_criteria]]
			) AS p