-- SELECT Queries
-- List all films with their length and rental rate, sorted by length descending.
-- Columns: title, length, rental_rate

select title, length, rental_rate from film
order by length desc;


-- Find the top 5 customers who have rented the most films.
-- Hint: Use the rental and customer tables.

select concat(first_name, ' ', last_name) as customer_name, count(*) as "Rent Count"
from customer c 
	join rental r on c.customer_id = r.customer_id
group by c.customer_id, first_name, last_name
order by count(r.customer_id) desc 
limit 5;


-- Display all films that have never been rented.
-- Hint: Use LEFT JOIN between film and inventory → rental.

select f.film_id, f.title 
from film f
	left join inventory i on f.film_id = i.film_id
	left join rental r on i.inventory_id = r.inventory_id
where r.rental_id is NULL
order by f.title;


-- JOIN Queries
-- List all actors who appeared in the film ‘Academy Dinosaur’.
-- Tables: film, film_actor, actor

select concat(first_name, ' ', last_name) Actors
from actor a
	join film_actor fa on fa.actor_id = a.actor_id
	join film f on f.film_id = fa.film_id
where f.title LIKE 'Academy Dinosaur';
	

-- List each customer along with the total number of rentals they made and the total amount paid.
-- Tables: customer, rental, payment
select  concat(first_name, ' ', last_name), count(DISTINCT r.rental_id) as "Rent Count", SUM(p.amount) 
from customer c 
	join rental r on c.customer_id = r.customer_id
	join payment p on p.customer_id = c.customer_id
group by first_name, last_name
order by count(DISTINCT r.rental_id) desc;

-- CTE-Based Queries
-- Using a CTE, show the top 3 rented movies by number of rentals.
-- Columns: title, rental_count

with cte_top_rents as
(
	select f.title, count(r.rental_id) as rental_count
	from film f
		left join inventory i on f.film_id = i.film_id
		left join rental r on i.inventory_id = r.inventory_id
	group by f.title
	order by f.title
)
select * from cte_top_rents
order by rental_count desc
limit 3;

-- Find customers who have rented more than the average number of films.
-- Use a CTE to compute the average rentals per customer, then filter.

with cte_rental as 
(
	select concat(c.first_name, ' ', c.last_name) as customer_name, count(r.rental_id) as rental_count
	from customer c
		join rental r on c.customer_id = r.customer_id
	group by c.first_name, c.last_name 
)
select customer_name from cte_rental 
where rental_count>
	(SELECT AVG(rental_count) FROM cte_rental);


--  Function Questions
-- Write a function that returns the total number of rentals for a given customer ID.
-- Function: get_total_rentals(customer_id INT)

create or replace function get_total_rentals(c_id INT)
returns int as
$$
declare total_rentals INT; 
begin
	select count(*) into total_rentals
	from rental r
	where r.customer_id = c_id;
return total_rentals;
end
$$ 
language plpgsql

select customer_id,  from rental;

select get_total_rentals(399);

SELECT customer_id, concat(first_name, ' ', last_name) as customer_name, get_total_rentals(customer_id) AS total_rentals
FROM customer 
ORDER BY total_rentals DESC;


-- Stored Procedure Questions
-- Write a stored procedure that updates the rental rate of a film by film ID and new rate.
-- Procedure: update_rental_rate(film_id INT, new_rate NUMERIC)

create or replace procedure update_rental_rate(f_id INT, new_rate NUMERIC)
as 
$$
begin
	
	update film set rental_rate = new_rate 
	where film_id = f_id;
	
end;
$$
language plpgsql

select * from film;

select film_id, title, rental_rate as "old rate" from film where film_id = 1;

call update_rental_rate(1, 2.99)

select film_id, title, rental_rate as "new rate" from film where film_id = 1;



CREATE OR REPLACE PROCEDURE get_overdue_rentals()
AS $$
DECLARE
    r RECORD;
BEGIN
    FOR r IN 
        SELECT rental_id, customer_id, rental_date 
        FROM rental
        WHERE return_date IS NULL 
          AND rental_date < CURRENT_DATE - INTERVAL '7 days'
    LOOP
        RAISE NOTICE 'Overdue Rental: %, %, %', r.rental_id, r.customer_id, r.rental_date;
    END LOOP;
END;
$$ 
LANGUAGE plpgsql;


call get_overdue_rentals()