-- Cursors 

-- Write a cursor to list all customers and how many rentals each made. Insert these into a summary table.
select c.customer_id, c.first_name || ' ' || c.last_name as customer_name, count(*) as rental_count
from customer c left join rental r on c.customer_id = r.customer_id
group by c.customer_id, c.first_name, c.last_name
order by c.customer_id

create table customer_summary
(
	customer_id INT PRIMARY KEY,
	customer_name TEXT,
	rental_count NUMERIC
)

do $$
declare
	rec record;
	cur cursor for
		select c.customer_id, c.first_name || ' ' || c.last_name as customer_name, count(*) as rental_count
		from customer c left join rental r on c.customer_id = r.customer_id
		group by c.customer_id, c.first_name, c.last_name
		order by c.customer_id;
begin
	open cur;
	loop
		fetch cur into rec;
		exit when not found;
		
		insert into customer_summary
		values(
			rec.customer_id,
			rec.customer_name,
			rec.rental_count
		);
	end loop;
	close cur;
end;
$$;

select * from customer_summary;


-- Using a cursor, print the titles of films in the 'Comedy' category rented more than 10 times.

do $$
declare
	rec record;
	cur cursor for
		SELECT f.film_id, f.title, COUNT(r.rental_id) AS rental_count
		FROM film f
		JOIN film_category fc ON f.film_id = fc.film_id
		JOIN category c ON fc.category_id = c.category_id
		JOIN inventory i ON f.film_id = i.film_id
		JOIN rental r ON i.inventory_id = r.inventory_id
		WHERE c.name = 'Comedy'
		GROUP BY f.film_id, f.title
		ORDER BY f.film_id;
begin
 	open cur;
	loop
		fetch cur into rec;
		exit when not found;
		if(rec.rental_count>10) then
			raise notice 'Film Name : % Rent Count : %',
				rec.title,
				rec.rental_count;
		end if;
	end loop;
	close cur;
end;
$$;

-- Create a cursor to go through each store and count the number of distinct films available, and insert results into a report table.
create table store_report(
	store_id int primary key,
	film_count numeric
);

SELECT s.store_id, COUNT(DISTINCT i.film_id) AS distinct_films_available
FROM store s
JOIN inventory i ON s.store_id = i.store_id
GROUP BY s.store_id
ORDER BY s.store_id;

do $$
declare
	rec record;
	cur cursor for
		SELECT s.store_id, COUNT(DISTINCT i.film_id) AS film_count
		FROM store s
		JOIN inventory i ON s.store_id = i.store_id
		GROUP BY s.store_id
		ORDER BY s.store_id;
begin
	open cur;
	loop
		fetch cur into rec;
		exit when not found;
		
		insert into store_report
		values(
			rec.store_id,
			rec.film_count
		);
	end loop;
	close cur;
end;
$$;

select * from store_report;
 
-- Loop through all customers who haven't rented in the last 6 months and insert their details into an inactive_customers table.
SELECT 
    c.customer_id,
    c.first_name,
    c.last_name,
    MAX(r.rental_date) AS last_rental_date
FROM 
    customer c
LEFT JOIN 
    rental r ON c.customer_id = r.customer_id
GROUP BY 
    c.customer_id, c.first_name, c.last_name
ORDER BY 
    last_rental_date;

do $$
declare
	rec record;
	cur cursor for
		SELECT c.customer_id, c.first_name, c.last_name, MAX(r.rental_date) AS last_rental_date
		FROM customer c
		LEFT JOIN 
		    rental r ON c.customer_id = r.customer_id
		GROUP BY 
		    c.customer_id, c.first_name, c.last_name
		ORDER BY 
		    last_rental_date;
begin
 	open cur;
	raise notice 'Customers who have not rented in the last 6 months :';
	loop
		fetch cur into rec;
		exit when not found;
		if rec.last_rental_date is null or rec.last_rental_date < CURRENT_DATE - INTERVAL '6 months' then
			raise notice 'Customer Name : % % Last rent date : %',
				rec.first_name,
				rec.last_name,
				DATE(rec.last_rental_date);
		end if;
	end loop;
	close cur;
end;
$$;

--loop through all the films and update the rental rate by +1 for teh films when rental count < 5

create or replace procedure proc_update_rental_rate()
language plpgsql
as $$
declare
  rec record;
  cur_film_rent_count cursor for
  select f.film_id, f.rental_rate, count(r.rental_id) as rental_count 
  from film f left join inventory i on f.film_id = i.film_id
  left join rental r on i.inventory_id = r.inventory_id
  group by f.film_id, f.rental_rate;
Begin
  open cur_film_rent_count;

  Loop
  	Fetch cur_film_rent_count into rec;
	exit when not found;

	if rec.rental_count < 5 then
	   update film set rental_rate= rental_rate +1
	   where film_id =  rec.film_id;

	   raise notice 'updated file  with id % . The new rental rate is %',rec.film_id,rec.rental_rate+1;
	end if;
end loop;
close cur_film_rent_count;
end;
$$;

call proc_update_rental_rate();

-- --------------------------------------------------------------------------
 
-- Transactions 

-- Write a transaction that inserts a new customer, adds their rental, and logs the payment – all atomically.

CREATE OR REPLACE PROCEDURE proc_create_customer_rental_payment(
	p_first_name TEXT,
	p_last_name TEXT, 
	p_email TEXT,
	p_address_id INT, 
	p_inventory_id int, 
	p_store_is int, 
	p_staff_id int,
	p_amount numeric
)
AS $$
DECLARE
    v_customer_id INT;
    v_rental_id INT;
BEGIN
	IF (p_amount <= 0) THEN
		RAISE EXCEPTION 'Amount cannot be Zero';
	END IF;
  	BEGIN
	    INSERT INTO customer (store_id, first_name, last_name, email, address_id, active, create_date)
	    VALUES (p_store_is,p_first_name,p_last_name,p_email,p_address_id, 1, CURRENT_DATE)
	    RETURNING customer_id INTO v_customer_id;
	 
	    INSERT INTO rental (rental_date, inventory_id, customer_id, staff_id)
	    VALUES (CURRENT_TIMESTAMP, p_inventory_id, v_customer_id, p_staff_id)
	    RETURNING rental_id INTO v_rental_id;
	    
	    INSERT INTO payment (customer_id, staff_id, rental_id, amount, payment_date)
	    VALUES (v_customer_id, p_staff_id, v_rental_id, p_amount, CURRENT_TIMESTAMP);
	EXCEPTION WHEN OTHERS THEN
		RAISE NOTICE 'Transaction failed %', SQLERRM;
	END;
END; 
$$
LANGUAGE plpgsql;

call proc_create_customer_rental_payment ('Keith','Hedger','kh@gmail.com',3,1,1,1,12);

select * from customer where customer_id = 600;
select * from rental where rental_id = 1004;
select * from payment;


-- Simulate a transaction where one update fails (e.g., invalid rental ID), and ensure the entire transaction rolls back.

begin;

update rental
set return_date = current_date
where rental_id = 1;

update rental
set return_dateee = current_date
where rental_id = -999;  

commit;

-- Use SAVEPOINT to update multiple payment amounts. Roll back only one payment update using ROLLBACK TO SAVEPOINT.

BEGIN;


UPDATE payment SET amount = amount + 5 WHERE payment_id = 17503;
SAVEPOINT sp1;

select payment_id, amount from payment where payment_id in (17503, 17504, 17505);

UPDATE payment SET amount = amount + 10 WHERE payment_id = 17504;
SAVEPOINT sp2;
ROLLBACK TO SAVEPOINT sp1;

UPDATE payment SET amount = amount + 15 WHERE paymentid = 17505;  --error due to invalid name
ROLLBACK TO SAVEPOINT sp2;

COMMIT;
select payment_id, amount from payment where payment_id in (17503, 17504, 17505);

-- Perform a transaction that transfers inventory from one store to another (delete + insert) safely.
BEGIN;
delete from inventory where inventory_id = 4585;
delete from rental where inventory_id = 4585;
insert into inventory values(4585, 1, 999, CURRENT_DATE);
COMMIT;

-- Create a transaction that deletes a customer and all associated records (rental, payment), ensuring referential integrity.
BEGIN;

DELETE FROM payment
WHERE customer_id = 1002;

DELETE FROM rental
WHERE customer_id = 1002;

DELETE FROM customer
WHERE customer_id = 1002;

COMMIT;

-- ----------------------------------------------------------------------------
 
-- Triggers

-- Create a trigger to prevent inserting payments of zero or negative amount.
CREATE OR REPLACE FUNCTION checkzero()
RETURNS TRIGGER
AS $$
BEGIN 
	if (NEW.amount>=0) then
		RAISE NOTICE 'Payment cannot be zero or less than zero';
	end if;
	RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER tgr_check_payment_zero
AFTER INSERT
ON payment
EXECUTE FUNCTION checkzero();

INSERT INTO payment VALUES(32099, 264, 2, 14243, 0);


-- Set up a trigger that automatically updates last_update on the film table when the title or rental rate is changed.
CREATE OR REPLACE FUNCTION update_last_update()
RETURNS TRIGGER
AS $$
BEGIN
	IF (OLD.title IS DISTINCT FROM NEW.title OR OLD.rental_rate IS DISTINCT FROM NEW.rental_rate) THEN 
		NEW.last_update := NOW();
	END IF;
	RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER tgr_last_update
BEFORE UPDATE ON film
FOR EACH ROW
EXECUTE FUNCTION update_last_update();


update film set title = 'agent truman show'
where film_id = 6;

select last_update from film
where film_id = 6;


-- Write a trigger that inserts a log into rental_log whenever a film is rented more than 3 times in a week.
CREATE TABLE rental_log(
	film_id INT, 
	title VARCHAR(255),
	rental_count BIGINT,
	rental_week TIMESTAMP
)

CREATE OR REPLACE FUNCTION rental_3_weeks()
RETURNS TRIGGER
AS $$
DECLARE
    film_id INT;
    title TEXT;
    rental_count INT;
    rental_week TIMESTAMP;
BEGIN
    SELECT 
        f.film_id,
        f.title,
        COUNT(r.rental_id),
        DATE_TRUNC('week', NEW.rental_date)
    INTO 
        film_id, title, rental_count, rental_week
    FROM rental r
    JOIN inventory i ON r.inventory_id = i.inventory_id
    JOIN film f ON i.film_id = f.film_id
    WHERE f.film_id = (
        SELECT i2.film_id
        FROM inventory i2
        WHERE i2.inventory_id = NEW.inventory_id
    )
    AND DATE_TRUNC('week', r.rental_date) = DATE_TRUNC('week', NEW.rental_date)
    GROUP BY f.film_id, f.title;


    IF rental_count > 3 THEN
        INSERT INTO rental_log (film_id, title, rental_count, rental_week)
        VALUES (film_id, title, rental_count, rental_week);
        RAISE NOTICE 'Log inserted!';
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


CREATE TRIGGER tgr_rental_3_weeks
AFTER INSERT ON rental
FOR EACH ROW 
EXECUTE FUNCTION rental_3_weeks();

insert into rental values(16099, CURRENT_DATE, 73, 64, CURRENT_DATE, 2);
insert into rental values(160675, CURRENT_DATE, 1712, 331, CURRENT_DATE, 2);
insert into rental values(160537, CURRENT_DATE, 176, 216, CURRENT_DATE, 2);
insert into rental values(160520, CURRENT_DATE, 2919, 150, CURRENT_DATE, 1);

select * from rental_log;