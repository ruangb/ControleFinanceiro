CREATE TABLE ControleFinanceiro..ExpenseInstallment 
(IdExpense INT NOT NULL
,Installment SMALLINT NOT NULL
,[Status] NVARCHAR(40) NOT NULL
,DueDate DATETIME NOT NULL
,[Value] DECIMAL(10,2) NOT NULL
,FOREIGN KEY (IdExpense) REFERENCES Expense(Id)
,PRIMARY KEY CLUSTERED (IdExpense, Installment))
