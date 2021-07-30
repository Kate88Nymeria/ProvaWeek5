--Q1 : Mostra prodotti con giacenza limitata (Quantità Disponibile < 10)
SELECT *
FROM Prodotti
WHERE QuantitaDisponibile < 10



--Q2 : Mostra il numero di prodotti per ogni categoria
SELECT Categoria, COUNT(*) AS [Numero Prodotti]
FROM Prodotti
GROUP BY Categoria
