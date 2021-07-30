CREATE TABLE Prodotti (
Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
CodiceProdotto NVARCHAR(20) UNIQUE NOT NULL,
Categoria NVARCHAR(20) NOT NULL,
Descrizione NVARCHAR(500) NOT NULL,
PrezzoUnitario NUMERIC(10, 2) NOT NULL,
QuantitaDisponibile INT NOT NULL
);

ALTER TABLE Prodotti 
ADD CONSTRAINT Categoria CHECK (Categoria = 'Alimentari' OR Categoria = 'Cancelleria' OR Categoria = 'Sanitari')