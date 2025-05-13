-- 1. Try two concurrent updates to same row → see lock in action.
-- SESSION A

BEGIN;
UPDATE products 
SET price = 5000 
WHERE id =1; 

SELECT * FROM PRODUCTS;

COMMIT;
-- 2. Write a query using SELECT...FOR UPDATE and check how it locks row.
-- 3. Intentionally create a deadlock and observe PostgreSQL cancel one transaction.
-- 4. Use pg_locks query to monitor active locks.
-- 5. Explore about Lock Modes.



-- Access share(automatic)
BEGIN;
SELECT * FROM access_share_test;
-- Do not COMMIT or ROLLBACK yet, keep the lock active\
-- then run session 2

COMMIT;



BEGIN;
SELECT * FROM row_share_test FOR UPDATE;
-- This acquires ROW SHARE lock on the table
-- Keep this transaction open (don’t commit yet)

-- these work fine here
UPDATE row_share_test 
SET name = 'aliceee' 
WHERE name = 'Alice';

SELECT * FROM row_share_test;


COMMIT;




BEGIN;
LOCK TABLE exclusive_lock_test IN EXCLUSIVE MODE;
-- Lock is held until COMMIT or ROLLBACK


--this works
SELECT * FROM exclusive_lock_test;

commit;