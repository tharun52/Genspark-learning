BEGIN;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SELECT * FROM Accounts;








select * from products;


BEGIN;
SELECT price FROM products
WHERE id = 1;
--1000(original value)






-- Trans B reapeatable read
BEGIN;
UPDATE products
SET price = 1000 WHERE id = 1;

SELECT price FROM products
WHERE id = 1;

COMMIT;




-- Trans B (pandthom read)
BEGIN;
INSERT INTO Accounts
(id, balance)
VALUES
(9, 5200),
(10, 6700);
COMMIT;


-- Trans B (concurrent update)
begin;

select * from accounts where id = 10;
-- old value(6700)
update accounts set balance = balance+10
where id = 10;
