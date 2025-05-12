CREATE TABLE Accounts
(
ID INT PRIMARY KEY,
balance INT
);

INSERT INTO Accounts
VALUES
(1, 1000);


-- dirty read
SELECT * FROM Accounts;

BEGIN TRANSACTION;
UPDATE Accounts
SET balance = 0 
WHERE id = 1;





create table products(id INT PRIMARY KEY, price NUMERIC(10,2));

INSERT INTO products
VALUES(1, 1000),
      (2, 3000);



-- Read Committed
-- Trans A
BEGIN;
UPDATE products
SET price = 500
WHERE id = 1;

-- Trans B(second file)
-- BEGIN;
-- SELECT price FROM products
-- WHERE id = 1;
-- 450





-- Repeatable Read
-- Trans A
BEGIN ISOLATION LEVEL REPEATABLE READ;
SELECT price FROM products
WHERE id = 1; -- 1000



-- Trans A
SELECT price FROM products
WHERE id = 1;
-- even tho 2nd user commits, we still get the previous value until we commit here
COMMIT;

-- 2ND USER CODE
-- BEGIN;
-- SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
-- SELECT * FROM Accounts;








insert into accounts
VALUES 
(3, 5500),
(4, 6000),
(5, 8000),
(6, 9500),
(7, 10500),
(8, 3000);

select  * from accounts;

-- Phatom Read
SELECT * FROM orders WHERE amount > 1000; returns 5 rows.
-- Another transaction inserts a new order with amount 1200 and commits — now re-running the
-- query returns 6 rows.

-- Trans A
BEGIN;
SELECT * FROM Accounts
WHERE balance > 5000;
-- 6 rows

-- Trans B
-- BEGIN;
-- INSERT INTO Accounts
-- (id, balance)
-- VALUES
-- (9, 5200),
-- (10, 6700);
-- COMMIT;

-- Trans A
SELECT * FROM Accounts
WHERE balance > 5000;
-- 7 rows (new row updated)




-- concurrent update
select * from accounts where id = 10;

begin;
update accounts set balance = balance+10
where id = 10;


/*
Concurrency
PostgreSQL handles concurrency using:
1. MVCC (Multi-Version Concurrency Control):
MVCC allows multiple versions of the same data (rows) to exist temporarily,
so readers and writers don't block each other.

Readers don't block writers and Writers don't block readers.


-- 450

-- Examples for MVCC

-- 1. 
-- User A: Reads
-- User B: Updates

-- 2. 
-- 1000 users check balance (reads)
-- 10 users perform withdrawals (writes)

*/


/*
2. Isolation Levels : 4 --> Concurrency
   1. READ UNCOMMITTED -> PSQL not supported
   2. READ COMMITTED -> Default; MVCC
   MVCC is ACID-Compliant.
   Read Committed is powered by MVCC.
   3. Repeatable Read -> Ensures repeatabe reads
   4. Serializable -> Full isolation (safe but slow, no dirty reads, no lost updates, no repeatable reads, no phantom reads)
   

Problems without proper Concurrency Control:
1. Inconsistent Reads/Dirty Reads: Reading Uncommitted data from another transaction, which might later disappear.
Transaction A updates a row but doesn’t commit yet.
Transaction B reads that updated row.
Transaction A rolls back the update.
Now Transaction B has read data that never officially existed — that’s a dirty read.

Why Dirty Reads Happen:
They occur in databases running at low isolation levels, particularly:
Read Uncommitted isolation level → allows dirty reads.
Higher isolation levels like Read Committed, Repeatable Read, or Serializable
prevent dirty reads but come with performance trade-offs (like more locks or slower concurrency).

2. Lost Update
Transaction A reads a record.
Transaction B reads the same record.
Transaction A updates and writes it back.
Transaction B (still holding the old value) writes back its version, overwriting A’s changes.

-- Trans A
*/
BEGIN;
SELECT balance FROM Accounts
WHERE id = 1;  -- 100
-- Thinks to add 50

-- Trans B
BEGIN;
SELECT balance FROM Accounts
WHERE id = 1; -- 100
-- Thinks to sub 30
UPDATE Accounts
SET balance = 70
WHERE id = 1;
COMMIT;

-- Trans A
UPDATE Accounts
SET balance = 150
WHERE id = 1;
COMMIT;

/*
Solutions to Avoid Lost Updates:
1. Pessimistic Locking (Explicit Locks)
Lock the record when someone reads it, so no one else can read or write until the lock is released.
Example: SELECT ... FOR UPDATE in SQL.
Prevents concurrency but can reduce performance due to blocking.
2. Optimistic Locking (Versioning)
Common and scalable solution.
Each record has a version number or timestamp.
When updating, you check that the version hasn’t changed since you read it.
If it changed, you reject the update (user must retry).
Example:
UPDATE products
SET price = 100, version = version + 1
WHERE id = 1 AND version = 3; --3
3. Serializable Isolation Level
In database transactions, using the highest isolation level (SERIALIZABLE) can prevent lost updates.
But it's heavier and can cause performance issues (due to more locks or transaction retries).

Which Solution is Best?
For web apps and APIs: Optimistic locking is often the best balance (fast reads + safe writes).
For critical financial systems: Pessimistic locking may be safer.

Inconsistent reads/read anomalies
1. Dirty Read
2. Non-Repeatable Read
Transaction A reads a row, -- 100
Transaction B updates and commits the row, then --90
Transaction A reads the row again and sees different data.

-- Trans A
*/
BEGIN;
SELECT balance FROM Accounts
WHERE id = 1; -- 1000

-- Trans B
UPDATE Accounts
SET baalnce = balance - 200
WHERE id = 1;
COMMIT;

-- Trans A
BEGIN;
SELECT balance FROM Accounts
WHERE id = 1; -- 1000-200=800

