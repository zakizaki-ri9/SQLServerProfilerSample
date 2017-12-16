
USE SampleDB

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE GetSampleTable
GO

CREATE PROCEDURE GetSampleTable
	@sample_name	NVARCHAR(MAX) = null
,	@option_a		NVARCHAR(MAX) = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM SampleTable WHERE
		(@sample_name IS NULL OR sample_name = @sample_name) AND
		(@option_a IS NULL OR option_a = @option_a)
END
GO
