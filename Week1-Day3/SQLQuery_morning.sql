

-- publishers who haven't publishers
select pub_name 
from publishers 
where pub_id not in (
	select distinct(pub_id) from titles
);

-- book name and publishername
select title, pub_name
from titles join publishers
on titles.pub_id = publishers.pub_id;

-- select author id and their book name
select au_id, title
from titleauthor join titles on titles.title_id = titleauthor.title_id;

-- select pubslisher name and first sales date
select pub_name, MIN(ord_date)
from publishers 
	left outer join titles on publishers.pub_id = titles.pub_id
	left outer join sales on sales.title_id = titles.title_id
	group by pub_name; 

-- select book name and store_adress
select title, stor_address
from titles 
	left outer join sales on sales.title_id = titles.title_id
	left outer join stores on sales.stor_id = stores.stor_id
	order by 1;

-- procedure creation
create procedure proc_hello
as 
begin 
	print 'Hello World!'
end

go

exec proc_hello


create table Products 
(id int identity(1,1) constraint pk_productId primary key,
name nvarchar(100) not null,
details nvarchar(max));

create proc proc_InsertProduct(@pname nvarchar(100), @pdetails nvarchar(max))
as
begin
	insert into Products(name, details) values(@pname, @pdetails)
end
go

proc_InsertProduct 'Laptop', '{"brand": "Dell", "spec":{"ram":"16GB", "cpu":"i5"}}'
go


proc_InsertProduct 'Laptop', '{"brand": "HP", "spec":{"ram":"32GB", "cpu":"ryzen5"}}'
go



select * from Products;



select JSON_QUERY(details, '$.spec') as Product_specs from Products;

create procedure proc_updateRam(@pid int, @newram varchar(20))
as 
begin
	update Products 
	set details = JSON_MODIFY(details, '$.spec.ram', @newram) 
	where id = @pid;
end


proc_updateRam 2, '16GB';


select * from Products;


select id, name, JSON_VALUE(details, '$.brand') brand_name
from Products;

select id, name, JSON_VALUE(details, '$.spec.ram') ram
from Products;








create table Posts(
	id int primary key,
	title nvarchar(100),
	user_id int,
	body nvarchar(max)
);

create or alter proc proc_BulInsertPosts(@jsondata nvarchar(max))
as
begin
	insert into Posts(user_id,id,title,body)
	select userId,id,title,body from openjson(@jsondata)
	with (userId int,id int, title nvarchar(100), body nvarchar(max))
end


proc_BulInsertPosts '
[
  {
    "userId": 1,
    "id": 1,
    "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
    "body": "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
  },
  {
    "userId": 1,
    "id": 2,
    "title": "qui est esse",
    "body": "est rerum tempore vitae\n"
  }
]'

select * from posts;




proc_InsertProduct 'Phone', '{"brand": "Nothing", "spec":{"ram":"8GB", "cpu":"mediatek"}}'

select * from Products;

select * from Products
where TRY_CAST(JSON_VALUE(details, '$.spec.ram') as nvarchar(20)) = '8GB';

create proc searchProduct(@pid int)
as
begin
	select * from Products where id = @pid;
end


searchProduct 2;
