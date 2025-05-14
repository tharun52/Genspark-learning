CREATE TABLE rental_log_updates (
    id SERIAL PRIMARY KEY,
    column_name TEXT,
    old_value TEXT,
    new_value TEXT,
    time TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


CREATE OR REPLACE FUNCTION update_audit_log()
RETURNS TRIGGER AS $$
DECLARE
    col_name TEXT := TG_ARGV[0];
    o_value TEXT;
    n_value TEXT;
BEGIN
    EXECUTE FORMAT('SELECT ($1).%I::TEXT', col_name) INTO o_value USING OLD;
    EXECUTE FORMAT('SELECT ($1).%I::TEXT', col_name) INTO n_value USING NEW;

    IF o_value IS DISTINCT FROM n_value THEN
        INSERT INTO rental_log_updates(column_name, old_value, new_value)
        VALUES (col_name, o_value, n_value);
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER update_rental_time
AFTER UPDATE ON rental_log
FOR EACH ROW
EXECUTE FUNCTION update_audit_log('rental_time');

CREATE TRIGGER update_customer_id
AFTER UPDATE ON rental_log
FOR EACH ROW
EXECUTE FUNCTION update_audit_log('customer_id');

CREATE TRIGGER update_film_id
AFTER UPDATE ON rental_log
FOR EACH ROW
EXECUTE FUNCTION update_audit_log('film_id');

CREATE TRIGGER update_amount
AFTER UPDATE ON rental_log
FOR EACH ROW
EXECUTE FUNCTION update_audit_log('amount');



update rental_log set amount = amount+10 where log_id = 1;
update rental_log set customer_id = 10 where log_id = 1;
update rental_log set film_id = 11 where log_id = 1;

select * from rental_log_updates;
