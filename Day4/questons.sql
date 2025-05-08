-- 1) List all orders with the customer name and the employee who handled the order.
-- (Join Orders, Customers, and Employees)

select o.orderid, c.contactname as 'customer name', concat(e.firstname, ' ', e.lastname) as 'employee name'
from orders o join Customers c on o.CustomerID = c.CustomerID
	join Employees e on o.EmployeeID = e.EmployeeID;


-- 2) Get a list of products along with their category and supplier name.
-- (Join Products, Categories, and Suppliers)

select p.ProductName 'Product name', c.CategoryName 'Category', s.CompanyName 'Supplier'
from Products p join Categories c on p.CategoryID = c.CategoryID
	join Suppliers s on p.SupplierID = s.SupplierID;


-- 3) Show all orders and the products included in each order with quantity and unit price.
-- (Join Orders, Order Details, Products)

select o.OrderID, p.ProductName, od.Quantity, od.UnitPrice
from Orders o join [Order Details] od on o.OrderID = od.OrderID
	join Products p on od.ProductID = p.ProductID;

-- 4) List employees who report to other employees (manager-subordinate relationship).
-- (Self join on Employees)

select CONCAT(e.FirstName, ' ', e.LastName) as Employee, CONCAT(m.FirstName, ' ', m.LastName) as 'Manager'
from Employees e join Employees m
  on e.ReportsTo = m.EmployeeID;


-- 5) Display each customer and their total order count.
-- (Join Customers and Orders, then GROUP BY)

select c.ContactName, COUNT(*) as 'Order Count'
from orders o join customers c on o.CustomerID = c.CustomerID
group by c.ContactName;


-- 6) Find the average unit price of products per category.
-- Use AVG() with GROUP BY

select c.CategoryName, AVG(p.UnitPrice) as 'Average price'
from Categories c join Products p on c.CategoryID = p.ProductID
group by c.CategoryName;


-- 7) List customers where the contact title starts with 'Owner'.
-- Use LIKE or LEFT(ContactTitle, 5)

select * from Customers
where ContactTitle LIKE '%Owner%';


-- 8) Show the top 5 most expensive products.
-- Use ORDER BY UnitPrice DESC and TOP 5

select TOP 5 * from Products
order by UnitPrice DESC;


-- 9) Return the total sales amount (quantity × unit price) per order.
-- Use SUM(OrderDetails.Quantity * OrderDetails.UnitPrice) and GROUP BY

select OrderID, SUM(Quantity * UnitPrice) as 'Average price'
from [Order Details] 
group by OrderID;


-- 10) Create a stored procedure that returns all orders for a given customer ID.
-- Input: @CustomerID

create or alter procedure getorder(@CustomerID nvarchar(20))
as 
begin
	select * from Orders 
	where TRY_CAST(CustomerID as nvarchar(20)) = @CustomerID;
end

getorder 'ANTON';



-- 11) Write a stored procedure that inserts a new product.
-- Inputs: ProductName, SupplierID, CategoryID, UnitPrice, etc.

create or alter proc insertProduct(@ProductName nvarchar(max), @SupplierID int, @CategoryID int, @QuantityPerUnit nvarchar(max), @UnitPrice float, @UnitsInStock int, @UnitsOnOrder int, @ReorderLevel int, @Discontinued int)
as
begin
	insert into Products
	Values(
		@ProductName, 
		@SupplierID,
		@CategoryId,
		@QuantityPerUnit,
		@UnitPrice,
		@UnitsInStock,
		@UnitsOnOrder,
		@ReorderLevel,
		@Discontinued
	);
end

insertProduct 'Chair', 1, 2, '5kg', 14.00, 10,  40, 5, 1
select * from Products;


-- 12) Create a stored procedure that returns total sales per employee.
-- Join Orders, Order Details, and Employees
create or alter proc getsalary
as 
begin
select CONCAT(e.FirstName , ' ', e.LastName) AS EmployeeName, ROUND(SUM(((p.UnitPrice-(od.UnitPrice-(od.Discount*od.UnitPrice)))*od.Quantity)), 2) as 'Total Sales'
from Orders o join [Order Details] od on o.OrderID = od.OrderID
	join Employees e on e.EmployeeID = o.EmployeeID
	join Products p on od.ProductID = p.ProductID 
	GROUP BY e.EmployeeID, e.FirstName, e.LastName
	ORDER BY e.EmployeeID;
end

getsalary;


-- 13) Use a CTE to rank products by unit price within each category.
-- Use ROW_NUMBER() or RANK() with PARTITION BY CategoryID
with cte_products as(
	select p.ProductName, c.CategoryName, RANK() OVER (partition by p.CategoryID order by p.UnitPrice DESC) as 'Rank', p.UnitPrice
	from Products p join Categories c on p.CategoryID = c.CategoryID
)
select * from cte_products;


-- 14) Create a CTE to calculate total revenue per product and filter products with revenue > 10,000.
with total_revenue as
	(select p.ProductName, ROUND(SUM(((p.UnitPrice-(od.Discount*od.UnitPrice))*od.Quantity)), 2) as 'Total Revenue'
	from [Order Details] od join Products p on od.ProductID = p.ProductID
	group by p.ProductName HAVING SUM((p.UnitPrice - (od.Discount * od.UnitPrice)) * od.Quantity) > 10000)
select * from total_revenue;


-- 15) Use a CTE with recursion to display employee hierarchy.
-- Start from top-level employee (ReportsTo IS NULL) and drill down

with RecursivetoplvlEmployees as(
	select e.employeeID, CONCAT(e.FirstName, ' ', e.LastName) as Employee, CAST('NO MANAGER' AS VARCHAR(255)) AS Manager, 0 as Level
	from Employees e
	where ReportsTo is NULL
UNION ALL 
	select e.employeeID, CONCAT(e.FirstName, ' ', e.LastName) as Employee, CAST(CONCAT(m.FirstName, ' ', m.LastName) AS VARCHAR(255)) AS Manager, r.Level+1
	from Employees e 
	join RecursivetoplvlEmployees r on e.ReportsTo = r.EmployeeId
	join Employees m on e.ReportsTo = m.EmployeeID
)
select * from RecursivetoplvlEmployees;

