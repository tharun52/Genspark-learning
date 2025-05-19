-- Cursor-Based Questions (5)
-- Write a cursor that loops through all films and prints titles longer than 120 minutes.

DO $$
DECLARE
    film20 CURSOR FOR 
        SELECT * FROM film WHERE length > 20;
    film_record RECORD;
BEGIN
    OPEN film20;
    
    LOOP
        FETCH film20 INTO film_record;
        
        EXIT WHEN NOT FOUND;

        RAISE NOTICE 'Film ID: %, Title: %, Length: %', 
            film_record.film_id, film_record.title, film_record.length;
    END LOOP;
	
    CLOSE film20;
END 
$$;


-- Create a cursor that iterates through all customers and counts how many rentals each made.
DO $$
DECLARE count_cur CURSOR FOR 
	SELECT * FROM customer;
	rental_count INT;
	count_record RECORD;
BEGIN
	OPEN count_cur;
	LOOP
		fetch next from count_cur into count_record;
		exit when not found;
		select count(*) into rental_count
		from rental
		where customer_id = count_record.customer_id;
		RAISE NOTICE 'Customer: %, Rentals: %',
			count_record.first_name || ' ' || count_record.last_name,  rental_count;
	END LOOP;
	CLOSE count_cur;
END 
$$;

SELECT customer_id, COUNT(*) 
FROM rentals
GROUP BY customer_id
ORDER BY COUNT(*) DESC;


-- Using a cursor, update rental rates: Increase rental rate by $1 for films with less than 5 rentals.

DO
$$
DECLARE update_cursor CURSOR FOR 
 	SELECT f.film_id, f.rental_rate
    FROM film f
    LEFT JOIN rental r ON f.rental_id = r.rental_id
    GROUP BY f.film_id, f.rental_rate
    HAVING COUNT(r.rental_id) < 5;
	update_record RECORD;
BEGIN
	OPEN update_cursor;
	LOOP
		FETCH next FROM update_cursor INTO update_record;
		EXIT WHEN NOT FOUND;

		update films
		set rental_rate = rental_rate+1 
		where film_id = update_record.film_id;
	 RAISE NOTICE 'Film ID: %, Old Rental Rate: %, New Rental Rate: %',
            update_record.film_id, update_record.rental_rate, update_record.rental_rate + 1;
	END LOOP;
END;
$$

-- Create a function using a cursor that collects titles of all films from a particular category.
CREATE OR REPLACE FUNCTION get_film_titles_by_category(cat_name TEXT)
RETURNS TABLE(title TEXT)
LANGUAGE plpgsql
AS $$
DECLARE
    film_cursor CURSOR FOR
        SELECT f.title
        FROM film f
        JOIN film_category fc ON f.film_id = fc.film_id
        JOIN category c ON fc.category_id = c.category_id
        WHERE c.name = cat_name;
	film_rec RECORD;
BEGIN
    OPEN film_cursor;

    LOOP
        FETCH film_cursor INTO film_rec;
        EXIT WHEN NOT FOUND;
        title := film_rec.title;
        RETURN NEXT;
    END LOOP;

    CLOSE film_cursor;
END;
$$;

SELECT * FROM get_film_titles_by_category('Action');

-- Loop through all stores and count how many distinct films are available in each store using a cursor.
DO $$
DECLARE
    store_rec RECORD;
    film_count INT;
    store_cursor CURSOR FOR
        SELECT store_id, address_id FROM store;
BEGIN
    OPEN store_cursor;

    LOOP
        FETCH store_cursor INTO store_rec;
        EXIT WHEN NOT FOUND;

        -- Count distinct films in each store
        SELECT COUNT(DISTINCT inventory.film_id)
        INTO film_count
        FROM inventory
        WHERE inventory.store_id = store_rec.store_id;

        -- Print the result
        RAISE NOTICE 'Store ID: %, Address ID: %, Distinct Films: %',
            store_rec.store_id, store_rec.address_id, film_count;
    END LOOP;

    CLOSE store_cursor;
END;
$$;
 
-- Trigger-Based Questions (5)

-- Write a trigger that logs whenever a new customer is inserted.
CREATE TABLE customer_log (
	log_id SERIAL PRIMARY KEY,
	customer_id INT,
	first_name TEXT,
	last_name TEXT,
	message TEXT,
	inserted_at TIMESTAMP DEFAULT current_timestamp
);

CREATE OR REPLACE FUNCTION log_new_customer()
RETURNS TRIGGER AS $$
DECLARE
    log_message TEXT;
BEGIN
    log_message := 'New customer added: ' || NEW.first_name || ' ' || NEW.last_name;

    INSERT INTO customer_log (customer_id, first_name, last_name, message)
    VALUES (NEW.customer_id, NEW.first_name, NEW.last_name, log_message);

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


CREATE TRIGGER after_customer_insert
AFTER INSERT ON customer
FOR EACH ROW
EXECUTE FUNCTION log_new_customer();

INSERT INTO customer
VALUES (1002, 1, 'Bob', 'Brown', 'bob52@gmail.com', 6, true, '1006-02-14', '2013-05-26 14:49:45.738', 1);

select * from  customer_log;

select * from customer;

-- Create a trigger that prevents inserting a payment of amount 0.

CREATE OR REPLACE FUNCTION check_amount()
RETURNS TRIGGER AS $$
BEGIN
	IF NEW.amount <= 0 THEN
		RAISE EXCEPTION 'Payment cannot be zero';
	END IF;
	RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER check_payment
BEFORE INSERT ON payment
FOR EACH ROW
EXECUTE FUNCTION check_amount();

select * from payment;

insert into payment
VALUES(18503, 2, 1, 2129, 0, '2009-02-17 19:23:24.996577');

 
-- Set up a trigger to automatically set last_update on the film table before update.
ALTER TABLE film
ADD COLUMN IF NOT EXISTS last_update TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

CREATE OR REPLACE FUNCTION update_last_modified()
RETURNS TRIGGER AS $$
BEGIN
    NEW.last_update := CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

DROP TRIGGER IF EXISTS set_last_update ON film;

CREATE TRIGGER set_last_update
BEFORE UPDATE ON film
FOR EACH ROW
EXECUTE FUNCTION update_last_modified();

UPDATE film
SET rental_rate = rental_rate + 1
WHERE film_id = 1;

SELECT * FROM film WHERE film_id = 1;

-- Create a trigger to log changes in the inventory table (insert/delete).

CREATE TABLE inventory_log (
    log_id SERIAL PRIMARY KEY,
    action_type TEXT,
    inventory_id INTEGER,
    film_id INTEGER,
    store_id INTEGER,
    action_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE OR REPLACE FUNCTION log_inventory_changes()
RETURNS TRIGGER AS $$
BEGIN
	--TG_OP means trigger operation
    IF TG_OP = 'INSERT' THEN
        INSERT INTO inventory_log(action_type, inventory_id, film_id, store_id)
        VALUES ('INSERT', NEW.inventory_id, NEW.film_id, NEW.store_id);
    ELSIF TG_OP = 'DELETE' THEN
        INSERT INTO inventory_log(action_type, inventory_id, film_id, store_id)
        VALUES ('DELETE', OLD.inventory_id, OLD.film_id, OLD.store_id);
    END IF;
    RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_inventory_log
AFTER INSERT OR DELETE ON inventory
FOR EACH ROW
EXECUTE FUNCTION log_inventory_changes();

INSERT INTO inventory (inventory_id, film_id, store_id, last_update)
VALUES (9999, 1, 1, CURRENT_TIMESTAMP);

DELETE FROM inventory WHERE inventory_id = 9999;

SELECT * FROM inventory_log;


-- Write a trigger that ensures a rental can’t be made for a customer who owes more than $50.
CREATE OR REPLACE FUNCTION check_customer_balance()
RETURNS TRIGGER AS $$
DECLARE
    unpaid_balance NUMERIC := 0;
BEGIN
    -- Calculate the total rental cost for rentals that have no payment
    SELECT COALESCE(SUM(f.rental_rate), 0) INTO unpaid_balance
    FROM rental r
    JOIN inventory i ON r.inventory_id = i.inventory_id
    JOIN film f ON i.film_id = f.film_id
    LEFT JOIN payment p ON r.rental_id = p.rental_id
    WHERE r.customer_id = NEW.customer_id
      AND p.payment_id IS NULL;

    IF unpaid_balance >= 50 THEN
        RAISE EXCEPTION 'Customer % has unpaid rentals of $%. Cannot rent more.', NEW.customer_id, unpaid_balance;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER block_high_balance_rental
BEFORE INSERT ON rental
FOR EACH ROW
EXECUTE FUNCTION check_customer_balance();
 
-- Transaction-Based Questions (5)

-- Write a transaction that inserts a customer and an initial rental in one atomic operation.
BEGIN;
DO $$
DECLARE
    new_customer_id INT;
BEGIN
    INSERT INTO customer (store_id, first_name, last_name, address_id, email, active, create_date, last_update)
    VALUES (1, 'John', 'Doe', 5, 'john.doe@example.com', TRUE, CURRENT_DATE, CURRENT_TIMESTAMP)
    RETURNING customer_id INTO new_customer_id;

    INSERT INTO rental (rental_date, inventory_id, customer_id, staff_id, last_update)
    VALUES (CURRENT_TIMESTAMP, 1, new_customer_id, 1, CURRENT_TIMESTAMP);
END $$;

-- here an error occurs for both insert together so we rollback
ROLLBACK;



-- Simulate a failure in a multi-step transaction (update film + insert into inventory) and roll back.

SELECT rental_rate FROM film WHERE film_id = 1;

BEGIN;

UPDATE film
SET rental_rate = rental_rate + 1
WHERE film_id = 1;

INSERT INTO inventory (film_id, store_id, last_update)
VALUES (1243252, '999', CURRENT_TIMESTAMP);

COMMIT;

ROLLBACK;

SELECT rental_rate FROM film WHERE film_id = 1;
-- rental_rate did not change because there was an error in film


-- Create a transaction that transfers an inventory item from one store to another.

select * from inventory
where inventory_id = 4
AND store_id = 2;

BEGIN;

UPDATE inventory
SET store_id = 3,
    last_update = CURRENT_TIMESTAMP
WHERE inventory_id = 4
  AND store_id = 1;

COMMIT;

-- this returns nothing since the value is changed
select * from inventory
where inventory_id = 4
AND store_id = 2;

 
-- Demonstrate SAVEPOINT and ROLLBACK TO SAVEPOINT by updating payment amounts, then undoing one.


select * from payment;

BEGIN;

SELECT amount FROM payment WHERE payment_id = 17503;

SAVEPOINT before_update;

UPDATE payment
SET amount = amount + 100
WHERE payment_id = 17503;

SELECT amount FROM payment WHERE payment_id = 17503;

ROLLBACK TO SAVEPOINT before_update;

SELECT amount FROM payment WHERE payment_id = 17503;
-- update did not take place
COMMIT;
 
-- Write a transaction that deletes a customer and all associated rentals and payments, ensuring atomicity.
BEGIN;

DELETE FROM payment
WHERE customer_id = 1002;

DELETE FROM rental
WHERE customer_id = 1002;

DELETE FROM customer
WHERE customer_id = 1002;

COMMIT;

