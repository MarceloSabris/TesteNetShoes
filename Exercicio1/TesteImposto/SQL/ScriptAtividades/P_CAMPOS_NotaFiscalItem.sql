BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.NotaFiscalItem ADD
	BaseIPI decimal(18, 5) NULL,
	AliquotaIPI decimal(18, 5) NULL,
	ValorIPI decimal(18, 5) NULL,
	ValorDesconto decimal(18, 5) NULL

GO
ALTER TABLE dbo.NotaFiscalItem SET (LOCK_ESCALATION = TABLE)
GO
COMMIT