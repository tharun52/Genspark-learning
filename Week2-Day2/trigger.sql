CREATE TABLE audit_log
(
	audit_id SERIAL PRIMARY KEY,
	table_name TEXT,
	fieldname TEXT,
	old_value TEXT,
	new_value TEXT, 
	update_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP 
)



CREATE OR REPLACE FUNCTION update_audit_log()
RETURNS TRIGGER 
AS $$
BEGIN
	INSERT INTO audit_log(table_name, fieldname, old_value, new_value)
	VALUES('customer', 'email', OLD.email, NEW.email);

	RETURN NEW;
END;
$$
LANGUAGE plpgsql;


CREATE TRIGGER trig_log_customer_email_change
BEFORE UPDATE
ON customer
FOR EACH ROW
EXECUTE FUNCTION update_audit_log();


SELECT * FROM customer;

UPDATE customer SET email = 'mary.smith@gmail.com'
WHERE customer_id = 1;

SELECT * FROM audit_log;








-- generic trigger
create or replace function Update_Audit_log()
returns trigger 
as $$
declare 
   col_name text := TG_ARGV[0];
   tab_name text := TG_ARGV[1];
   o_value text;
   n_value text;
begin
	-- Retrieve the old value for the column
	EXECUTE FORMAT('select ($1).%I::TEXT', col_name) INTO o_value USING OLD;

	-- Retrieve the new value for the column
	EXECUTE FORMAT('select ($1).%I::TEXT', col_name) INTO n_value USING NEW;

	-- If the old and new values are distinct, log the change
	if o_value is distinct from n_value then
		insert into audit_log(table_name, fieldname, old_value, new_value) 
		values(tab_name, col_name, o_value, n_value);
	end if;

    return new;
end;
$$ language plpgsql;

-- Drop the trigger if it exists
DROP TRIGGER IF EXISTS trg_log_customer_last_name_change ON customer;

-- Create the trigger to log changes
create trigger trg_log_customer_email_Change
after update
on customer
for each row
execute function Update_Audit_log('last_name','customer');

-- Example update to test the trigger
update customer set last_name = 'Smithhhj' where customer_id = 1;


select * from audit_log;

SELECT 
    tg.tgname AS trigger_name,
    c.relname AS table_name,
    p.proname AS function_name
FROM 
    pg_trigger tg
JOIN 
    pg_class c ON tg.tgrelid = c.oid
JOIN 
    pg_proc p ON tg.tgfoid = p.oid
WHERE 
    NOT tg.tgisinternal;  -- Exclude system/internal triggers
