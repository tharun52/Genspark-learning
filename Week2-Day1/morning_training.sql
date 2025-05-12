create table tbl_bank_accounts(
	account_id SERIAL PRIMARY KEY,
	account_name VARCHAR(100), 
	balance NUMERIC(10, 2)
);

INSERT INTO tbl_bank_accounts(account_name, balance)
VALUES ('Alice', 5000.00),
	('Bob', 3000.00);

SELECT * FROM tbl_bank_accounts;


-- no error transact
BEGIN;

UPDATE tbl_bank_accounts
SET balance = balance-500
WHERE account_name = 'Alice';

UPDATE tbl_bank_accounts
SET balance = balance+500
WHERE account_name = 'Bob';

SELECT * FROM tbl_bank_accounts;

COMMIT;


--transact with error
BEGIN;

UPDATE tbl_bank_accounts
SET balance = balance-500
WHERE account_name = 'Alice';

UPDATE tbl_bank_account
SET balance = balance+500
WHERE account_name = 'Bob';

ROLLBACK;
COMMIT;

SELECT * FROM tbl_bank_accounts;


--transact with savepoint
BEGIN;

UPDATE tbl_bank_accounts
SET balance = balance-500
WHERE account_name = 'Alice';

SAVEPOINT after_alice;

UPDATE tbl_bank_account
SET balance = balance+500
WHERE account_name = 'Bob';

ROLLBACK TO after_alice;

COMMIT;

SELECT * FROM tbl_bank_accounts;




UPDATE tbl_bank_accounts
SET balance = balance+5000
WHERE account_name = 'Alice';

-- NOTICE
BEGIN;
DO $$
DECLARE current_balance NUMERIC;
BEGIN
SELECT balance INTO current_balance
FROM tbl_bank_accounts
WHERE account_name = 'Alice';
IF current_balance >= 4500 THEN
	UPDATE tbl_bank_accounts SET balance = balance - 4500 WHERE account_name = 'Alice';
	UPDATE tbl_bank_accounts SET balance = balance + 4500 WHERE account_name = 'Bob';
ELSE
	RAISE NOTICE 'INSUFFICIENT FUNDS';
	ROLLBACK;
END IF;
END;
$$;


COMMIT;


SELECT * FROM tbl_bank_accounts
WHERE account_name = 'Alice';



BEGIN TRANSACTION;
UPDATE tbl_bank_accounts
set balance = 0
where account_name = 'Alice';


begin TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

select * from tbl_bank_accounts;

	