-- 1. Create a stored procedure to encrypt a given text
-- Task: Write a stored procedure sp_encrypt_text that takes a plain text input (e.g., email or mobile number) and returns an encrypted version using PostgreSQL's pgcrypto extension.
-- Use pgp_sym_encrypt(text, key) from pgcrypto.

CREATE OR REPLACE FUNCTION func_encrypt_text(input_text TEXT, secret_key TEXT)
RETURNS BYTEA AS $$
BEGIN
    RETURN pgp_sym_encrypt(input_text, secret_key);
END;
$$ LANGUAGE plpgsql;


SELECT encode(func_encrypt_text('9876543210', 'mySecretKey123'), 'base64');

--Stored Procedure
CREATE OR REPLACE PROCEDURE proc_encrypt_text(IN input_text TEXT, IN secret_key TEXT, OUT encrypted_output BYTEA)
AS $$
BEGIN
    encrypted_output := pgp_sym_encrypt(input_text, secret_key);
END;
$$ LANGUAGE plpgsql;

DO $$
DECLARE
    encrypted_output BYTEA;
BEGIN
    CALL proc_encrypt_text('9876543210', 'mySecretKey123', encrypted_output);
    RAISE NOTICE 'Encrypted result: %', encode(encrypted_output, 'base64');
END;
$$;

-- 2. Create a stored procedure to compare two encrypted texts
-- Task: Write a procedure sp_compare_encrypted that takes two encrypted values and checks if they decrypt to the same plain text.

CREATE OR REPLACE FUNCTION func_compare_encrypted(pass1 BYTEA, pass2 BYTEA, secret_key TEXT)
RETURNS BOOLEAN AS $$ 
BEGIN
	RETURN (pgp_sym_decrypt(pass1, secret_key) = pgp_sym_decrypt(pass2, secret_key));
END;
$$ LANGUAGE plpgsql;

SELECT func_compare_encrypted(func_encrypt_text('9876543210', 'mySecretKey123'), func_encrypt_text('9876543210', 'mySecretKey123'), 'mySecretKey123');


--Stored Procedure
CREATE OR REPLACE PROCEDURE proc_compare_encrypted(IN pass1 BYTEA, IN pass2 BYTEA, IN secret_key TEXT, OUT is_equal BOOLEAN)
AS $$
BEGIN
    is_equal := (pgp_sym_decrypt(pass1, secret_key) = pgp_sym_decrypt(pass2, secret_key));
END;
$$ LANGUAGE plpgsql;

DO $$
DECLARE
    encrypted1 BYTEA;
    encrypted2 BYTEA;
    result BOOLEAN;
BEGIN
    encrypted1 := pgp_sym_encrypt('9876543210', 'mySecretKey123');
    encrypted2 := pgp_sym_encrypt('9876543210', 'mySecretKey123');

    CALL proc_compare_encrypted(encrypted1, encrypted2, 'mySecretKey123', result);

    RAISE NOTICE 'Are the values equal? %', result;
END;
$$;

-- 3. Create a stored procedure to partially mask a given text
-- Task: Write a procedure sp_mask_text that:
-- Shows only the first 2 and last 2 characters of the input string 
-- Masks the rest with *
-- E.g., input: 'john.doe@example.com' → output: 'jo***************om'

CREATE OR REPLACE FUNCTION func_mask_text(pass TEXT)
RETURNS TEXT AS $$
DECLARE
	masked_text TEXT;
	len INT;
BEGIN
	IF (len<=4) THEN
		RETURN pass;
	ELSE
		len := LENGTH(pass);
		masked_text := CONCAT(SUBSTRING(pass, 1, 2), REPEAT('*', len-4), SUBSTRING(pass, len-1, 2));
		RETURN masked_text;
	END IF;
END;
$$ LANGUAGE plpgsql;

select func_mask_text('john.doe@example.com');


--Stored Procedure
CREATE OR REPLACE PROCEDURE proc_mask_text(IN pass TEXT, OUT masked_text TEXT)
AS $$
DECLARE
    len INT;
BEGIN
    len := LENGTH(pass);
    IF len <= 4 THEN
        masked_text := pass;
    ELSE
        masked_text := CONCAT(SUBSTRING(pass, 1, 2), REPEAT('*', len - 4), SUBSTRING(pass, len - 1, 2));
    END IF;
END;
$$ LANGUAGE plpgsql;

DO $$
DECLARE
    result TEXT;
BEGIN
    CALL proc_mask_text('john.doe@example.com', result);
    RAISE NOTICE 'Masked Text: %', result;
END;
$$;

 
-- 4. Create a procedure to insert into customer with encrypted email and masked name
-- Task:
-- Call sp_encrypt_text for email
-- Call sp_mask_text for first_name
-- Insert masked and encrypted values into the customer table
-- Use any valid address_id and store_id to satisfy FK constraints.

CREATE TABLE address (
    address_id SERIAL PRIMARY KEY,
    address_line TEXT NOT NULL,
    city TEXT NOT NULL,
    postal_code TEXT,
    country TEXT NOT NULL
);


CREATE TABLE store (
    store_id SERIAL PRIMARY KEY,
    store_name TEXT NOT NULL,
    address_id INT NOT NULL,
    FOREIGN KEY (address_id) REFERENCES address(address_id)
);

CREATE TABLE customer (
    customer_id SERIAL PRIMARY KEY,
    first_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    email BYTEA NOT NULL,         
    address_id INT NOT NULL,
    store_id INT NOT NULL,
    FOREIGN KEY (address_id) REFERENCES address(address_id),
    FOREIGN KEY (store_id) REFERENCES store(store_id)
);


INSERT INTO address (address_line, city, postal_code, country) VALUES
('123 Main St', 'Chennai', '600001', 'India'),
('456 Market Rd', 'Mumbai', '400001', 'India');

INSERT INTO store (store_name, address_id) VALUES
('Chennai Store', 1),
('Mumbai Store', 2);

CREATE OR REPLACE PROCEDURE insert_customer(f_name TEXT, l_name TEXT, email TEXT, id_address INT, id_store INT)
AS $$
BEGIN
	INSERT INTO customer(first_name, last_name, email, address_id, store_id)
	VALUES(func_mask_text(f_name), l_name, func_encrypt_text(email, 'mySecretKey123'), id_address, id_store);
	EXCEPTION WHEN OTHERS THEN
		RAISE NOTICE 'Insert Failed : %', SQLERRM;
END;
$$ LANGUAGE plpgsql;

CALL insert_customer('Grenchen', 'Miller', 'grenchen@example.com', 1, 1);
CALL insert_customer('Arthur', 'Morgan', 'arthur@rdr.com', 1, 1);

CALL insert_customer('Anakin', 'Skywalker', 'darth@gmail.com', 1, 2);

SELECT * FROM customer;


-- 5. Create a procedure to fetch and display masked first_name and decrypted email for all customers
-- Task:
-- Write sp_read_customer_masked() that:
-- Loops through all rows
-- Decrypts email
-- Displays customer_id, masked first name, and decrypted email

CREATE OR REPLACE PROCEDURE sp_read_customer_masked()
AS $$
DECLARE 
	cur CURSOR FOR SELECT * FROM customer;
	rec RECORD;
BEGIN
	OPEN cur;
	LOOP
		FETCH NEXT FROM cur INTO rec;
		EXIT WHEN NOT FOUND;
		RAISE NOTICE 'Customer Id : % Customer Name : % % Email : %' ,
			rec.customer_id,
			rec.first_name, 
			rec.last_name,
			pgp_sym_decrypt(rec.email, 'mySecretKey123');
	END LOOP;
END;
$$ LANGUAGE plpgsql;

CALL sp_read_customer_masked();
