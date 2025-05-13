-- 1. Try two concurrent updates to same row → see lock in action.
-- SESSION B
SELECT * FROM PRODUCTS;
BEGIN;
UPDATE products 
SET price = 500 
WHERE id =1; 

SELECT * FROM PRODUCTS;

COMMIT;
-- waiting for query until session a commits

BEGIN;
DROP TABLE access_share_test;
-- This will be blocked until Session 1 commits or rolls back

-- When session 1 commits, this executes and the table is blocked

select * from access_share_test;
-- No table found

commit;



BEGIN;
LOCK TABLE row_share_test IN EXCLUSIVE MODE;
-- ⚠️ This will BLOCK until Session 1 commits or rolls back


-- no output comes out here until 1 commits
SELECT * FROM row_share_test;

UPDATE row_share_test 
SET name = 'aliceeeeee' 
WHERE name = 'Alice';

COMMIT;





BEGIN;
LOCK TABLE exclusive_lock_test IN ROW SHARE MODE;
-- ❌ This will BLOCK until Session 1 COMMITs

-- this does not work
select * from exclusive_lock_test;
-- only after commiting in A, it works

COMMIT;