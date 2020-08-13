﻿--drop proc USP_CLR_DECBASE62
--drop proc USP_CLR_ENCBASE62

-- Drop the assembly
IF EXISTS (SELECT * FROM sys.assemblies WHERE name = 'BASE62_CLR')
BEGIN
 DROP ASSEMBLY BASE62_CLR
END
GO

-- Create the assembly
CREATE ASSEMBLY BASE62_CLR
FROM 'D:\mssqlClr\BASE62.dll'
WITH PERMISSION_SET = UNSAFE;
--WITH PERMISSION_SET = SAFE;
GO



CREATE PROCEDURE USP_CLR_DECBASE62 @STR NVARCHAR(MAX)
WITH EXECUTE AS CALLER AS EXTERNAL NAME BASE62_CLR.StoredProcedures.DecodeBase62
GO


CREATE PROCEDURE USP_CLR_ENCBASE62 @STR NVARCHAR(MAX)
WITH EXECUTE AS CALLER AS EXTERNAL NAME BASE62_CLR.StoredProcedures.EncodeBase62
GO