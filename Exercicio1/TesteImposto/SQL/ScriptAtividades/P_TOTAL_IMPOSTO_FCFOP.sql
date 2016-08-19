USE [Teste]
GO

/****** Object:  StoredProcedure [dbo].[P_TOTAL_IMPOSTO_CFOP]    Script Date: 19/08/2016 08:14:19 ******/
DROP PROCEDURE [dbo].[P_TOTAL_IMPOSTO_CFOP]
GO

/****** Object:  StoredProcedure [dbo].[P_TOTAL_IMPOSTO_CFOP]    Script Date: 19/08/2016 08:14:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- Batch submitted through debugger: P_NOTA_FISCAL_ITEM.sql|15|0|C:\TesteImposto\SQL\P_NOTA_FISCAL_ITEM.sql
CREATE PROCEDURE [dbo].[P_TOTAL_IMPOSTO_CFOP]
AS
BEGIN
	SELECT Cfop
      ,sum(BaseIcms) as Vlr_Total_Base_ICMS
      ,sum(ValorIcms) as vlr_Total_ICMS
	  ,sum(BaseIPI) as vlr_Total_Base_IPI
	  ,sum(ValorIPI) as vlr_total_IPI
      
  FROM [dbo].[NotaFiscalItem]
  group by Cfop
END


GO


