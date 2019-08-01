IF EXISTS (SELECT 1 FROM sys.views WHERE Name = N'MyNewView')
                BEGIN
                    DROP VIEW [dbo].MyNewView
                END
GO
                  CREATE VIEW [dbo].MyNewView AS 
                    SELECT
	                    TheColumn
                    FROM [dbo].[MyTable]
GO

