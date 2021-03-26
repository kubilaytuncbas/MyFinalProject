select Products.ProductName,sum([Order Details].UnitPrice * [Order Details].Quantity) AS 'TOPLAM MİKTAR'
FROM (([Order Details] inner join Products on [Order Details].ProductID=Products.ProductID) inner join Orders on orders.OrderID=[Order Details].OrderID)
group by Products.ProductName