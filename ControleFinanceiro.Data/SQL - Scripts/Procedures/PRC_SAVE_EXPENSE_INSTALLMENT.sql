USE ControleFinanceiro

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PRC_SAVE_EXPENSE_INSTALLMENT')
       BEGIN
             DROP  PROCEDURE [PRC_SAVE_EXPENSE_INSTALLMENT]
       END
GO
-- ================================================================
-- Author: Ruan Gabriel de Barros
-- Create date: 22/05/2024
-- Description:     Grava ou atualiza uma parcela da Despesa amarrando-a a uma Fatura, que, se n�o existente, tamb�m ser� criada nesta proc
-- Modifications: DD/MM/YYYY      |     Author    |    Description
--         
-- ================================================================
CREATE PROCEDURE [dbo].[PRC_SAVE_EXPENSE_INSTALLMENT]
        @N_ID             INT 
       ,@N_ID_CREDIT_CARD INT 
       ,@N_ID_EXPENSE	  INT
       ,@N_INSTALLMENT	  DECIMAL(10)
       ,@S_STATUS		  NVARCHAR(80)
       ,@D_REFERENCE_DATE DATETIME
       ,@N_VALUE		  DECIMAL(9,2)
       ,@RESULT			  INT OUTPUT
       ,@RESULT_MESS	  NVARCHAR (255) OUTPUT
       WITH ENCRYPTION
AS

BEGIN
	DECLARE @CreditCardDueDay SMALLINT = (SELECT DueDay FROM CreditCard (NOLOCK) WHERE Id = @N_ID_CREDIT_CARD);	
	DECLARE @CreditCardClosingDays SMALLINT = (SELECT ClosingDays FROM CreditCard (NOLOCK) WHERE Id = @N_ID_CREDIT_CARD);	

	DECLARE @ReferenceDatePlusClosingDays DATETIME = (SELECT DATEADD(DAY, (@CreditCardClosingDays), @D_REFERENCE_DATE));
	DECLARE @CreditCardDueDate DATETIME = (SELECT DATEFROMPARTS (YEAR(@D_REFERENCE_DATE), MONTH(@ReferenceDatePlusClosingDays), @CreditCardDueDay));

	IF (@ReferenceDatePlusClosingDays > @CreditCardDueDate)
	BEGIN 
		SET @CreditCardDueDate = (SELECT DATEADD(MONTH, 1, @CreditCardDueDate))
	END

	DECLARE @IdBill INT = (SELECT Id FROM Bill (NOLOCK) WHERE DueDate = @CreditCardDueDate);
	
	IF (@IdBill IS NULL)
	BEGIN 	
		INSERT INTO Bill (IdCreditCard, DueDate)
		VALUES (@N_ID_CREDIT_CARD, @CreditCardDueDate)

		SET @IdBill = (SELECT Id FROM Bill (NOLOCK) WHERE DueDate = @CreditCardDueDate);
	END

	IF (@N_ID = 0)
	BEGIN
		INSERT INTO ExpenseInstallment(IdExpense, IdBill, Installment, [Status], ReferenceDate, [Value])
		VALUES (@N_ID_EXPENSE, @IdBill, @N_INSTALLMENT, @S_STATUS, @D_REFERENCE_DATE, @N_VALUE)
	END ELSE
	BEGIN 
		UPDATE ExpenseInstallment
		SET IdBill = @IdBill, Installment = @N_INSTALLMENT, [Status] = @S_STATUS, ReferenceDate = @D_REFERENCE_DATE, [Value] = @N_VALUE
		WHERE Id = @N_ID
	END

    SET @RESULT = @@ERROR;
    SET NOCOUNT ON;
END

GO