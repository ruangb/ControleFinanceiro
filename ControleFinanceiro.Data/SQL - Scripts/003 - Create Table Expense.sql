CREATE TABLE ControleFinanceiro..Expense 
(Id INT NOT NULL PRIMARY KEY IDENTITY
,IdPerson INT NOT NULL
,IdCreditCard INT NULL
,[Status] NVARCHAR(40) NOT NULL
,OperationDate DATETIME NOT NULL
,[Description] NVARCHAR(100) NOT NULL
,Amount DECIMAL(10,2) NOT NULL
,ParcelQuantity SMALLINT NOT NULL
,FOREIGN KEY (IdPerson) REFERENCES Person(Id)
,FOREIGN KEY (IdCreditCard) REFERENCES CreditCard(Id))
