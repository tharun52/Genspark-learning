use pubs
go 

select * from Products;

select * from products where 
try_cast(json_value(details,'$.spec.cpu') as nvarchar(20)) ='i5'

create or alter proc proc_FilterProducts(@pcpu varchar(20), @pcount int out)
as
begin
    set @pcount = (select count(*) from products where 
	try_cast(json_value(details,'$.spec.cpu') as nvarchar(20)) =@pcpu)
end


declare @cnt int
exec proc_FilterProducts 'i5', @cnt out
print concat('The number of computers is ',@cnt)




sp_help authors

BULK INSERT people from 'C:\Users\VC\Downloads\Data.csv'
   with(
   FIRSTROW =2,
   FIELDTERMINATOR=',',
   ROWTERMINATOR = '\n')
	

create table people
(id int primary key,
name nvarchar(20),
age int)

create or alter proc proc_BulkInsert(@filepath nvarchar(500))
as
begin
   declare @insertQuery nvarchar(max)

   set @insertQuery = 'BULK INSERT people from '''+ @filepath +'''
   with(
   FIRSTROW =2,
   FIELDTERMINATOR='','',
   ROWTERMINATOR = ''\n'')'
   exec sp_executesql @insertQuery
end


proc_BulkInsert 'C:\Users\VC\Downloads\Data.csv'




create table BulkInsertLog
(LogId int identity(1,1) primary key,
FilePath nvarchar(1000),
status nvarchar(50) constraint chk_status Check(status in('Success','Failed')),
Message nvarchar(1000),
InsertedOn DateTime default GetDate())

create or alter proc proc_BulkInsert(@filepath nvarchar(500))
as
begin
  Begin try
	   declare @insertQuery nvarchar(max)

	   set @insertQuery = 'BULK INSERT people from '''+ @filepath +'''
	   with(
	   FIRSTROW =2,
	   FIELDTERMINATOR='','',
	   ROWTERMINATOR = ''\n'')'

	   exec sp_executesql @insertQuery

	   insert into BulkInsertLog(filepath,status,message)
	   values(@filepath,'Success','Bulk insert completed')
  end try
  begin catch
		 insert into BulkInsertLog(filepath,status,message)
		 values(@filepath,'Failed',Error_Message())
  END Catch
  select message as status from bulkinsertlog;
end

proc_BulkInsert 'C:\Users\VC\Downloads\Data.csv'

select * from BulkInsertLog;



select * from authors;

with cteAuthors
as
(select au_id, concat(au_fname,' ',au_lname) author_name, state from authors)

--update cteAuthors set state='KS' where author_name LIKE 'Abraham Bennet';
select * from cteAuthors;


with cteAuthors
as
(select au_id, concat(au_fname,' ',au_lname) author_name, state 
from authors)




declare @page int =2, @pageSize int=10;
with PaginatedBooks as
( select  title_id,title, price, ROW_Number() over (order by price desc) as RowNum
  from titles
)
select * from PaginatedBooks where rowNUm between((@page-1)*@pageSize) and (@page*@pageSize)


--create a sp that will take the page number and size as param and print the books

create or alter proc getPage(@page int, @pageSize int)
as
begin
with PaginatedBooks as
( select  title_id,title, price, ROW_Number() over (order by price desc) as RowNum
  from titles
)
select * from PaginatedBooks where rowNUm between((@page-1)*@pageSize) and (@page*@pageSize);
end

getPage 2, 10;


select  ROW_Number() over (order by price desc) as RowNum, title_id,title, price 
from titles
order by price DESC
offset 2 rows fetch next 10 rows only;

CREATE OR ALTER PROCEDURE getPage
  @page INT,
  @pageSize INT
AS
BEGIN
    WITH PaginatedBooks AS (
        SELECT 
			ROW_Number() over (order by price desc) as RowNum,
            title_id,
            title,
            price
        FROM titles
    )
    SELECT *
    FROM PaginatedBooks
    ORDER BY price DESC
    OFFSET (@page - 1) * @pageSize ROWS
    FETCH NEXT @pageSize ROWS ONLY;
END;
GO

getPage 2, 5;


 create function  fn_CalculateTax(@baseprice float, @tax float)
  returns float
  as
     return (@baseprice +(@baseprice*@tax/100))
  

  select dbo.fn_CalculateTax(1000,10)

 
create or alter function fn_tableSample(@minprice float)
returns table
as
return select title,price from titles where price>= @minprice
	

select price from dbo.fn_tableSample(10)


--older and slower but supports more logic
create function fn_tableSampleOld(@minprice float)
  returns @Result table(Book_Name nvarchar(100), price float)
  as
  begin
    insert into @Result select title,price from titles where price>= @minprice
    return 
end
select * from dbo.fn_tableSampleOld(10)
