
			LEFT OUTER JOIN (
				SELECT		pcv.Product_Id
							, pcv.[[input_value]] AS '[[custom_attribute_column]]'
				FROM		[pdb].T_ProductCustomAttributeValue pcv
				WHERE		pcv.CustomAttributeExternalCode = [[custom_attribute_external_code_parameter]]
			) AS [[custom_attribute_table]] ON p.Id = [[custom_attribute_table]].Product_Id
			
