CREATE PROCEDURE [Northwind].[CustOrdersStatistic]
	@CustomerID nchar(5),
	@Shipped int OUTPUT,
	@All int OUTPUT
AS
BEGIN
	SELECT @Shipped = Count(*) 
	FROM [Northwind].Orders
	WHERE CustomerID = @CustomerID
		and ShippedDate is not null;

	SELECT @All = Count(*) 
	FROM [Northwind].Orders
	WHERE CustomerID = @CustomerID

END
