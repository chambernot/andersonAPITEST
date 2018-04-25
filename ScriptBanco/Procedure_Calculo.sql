-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE sp_CriaCalculoHistorico
	-- Add the parameters for the stored procedure here
	@Nome varchar(100),
	@Localizacao varchar(200),
	@NomeProximo varchar(200),
	@LocalizacaoProximo varchar(200),
	@distancia decimal(18,2),
	@identificacaoexterna UNIQUEIDENTIFIER
AS
BEGIN
	DECLARE @id UNIQUEIDENTIFIER = NEWID();

	INSERT INTO CalculoHistoricoLog (
		Id,
		Nome,
		Localizacao,
		NomeProximo,
		LocalizacaoProximo,
		distancia,
		IdidenticacaoExterno)
	VALUES (
		@id,
		@Nome ,
		@Localizacao ,
		@NomeProximo ,
		@LocalizacaoProximo ,
		@distancia,
		@identificacaoexterna );
END
GO
