--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4
-- Dumped by pg_dump version 17.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'WIN1252';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: sp_add_rental_log(integer, integer, numeric); Type: PROCEDURE; Schema: public; Owner: VC
--

CREATE PROCEDURE public.sp_add_rental_log(IN p_customer_id integer, IN p_film_id integer, IN p_amount numeric)
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO rental_log (rental_time, customer_id, film_id, amount)
    VALUES (CURRENT_TIMESTAMP, p_customer_id, p_film_id, p_amount);
EXCEPTION WHEN OTHERS THEN
    RAISE NOTICE 'Error occurred: %', SQLERRM;
END;
$$;


ALTER PROCEDURE public.sp_add_rental_log(IN p_customer_id integer, IN p_film_id integer, IN p_amount numeric) OWNER TO "VC";

--
-- Name: update_audit_log(); Type: FUNCTION; Schema: public; Owner: replicator
--

CREATE FUNCTION public.update_audit_log() RETURNS trigger
    LANGUAGE plpgsql
    AS $_$
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
$_$;


ALTER FUNCTION public.update_audit_log() OWNER TO replicator;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: rental_log; Type: TABLE; Schema: public; Owner: VC
--

CREATE TABLE public.rental_log (
    log_id integer NOT NULL,
    rental_time timestamp without time zone,
    customer_id integer,
    film_id integer,
    amount numeric,
    logged_on timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public.rental_log OWNER TO "VC";

--
-- Name: rental_log_log_id_seq; Type: SEQUENCE; Schema: public; Owner: VC
--

CREATE SEQUENCE public.rental_log_log_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.rental_log_log_id_seq OWNER TO "VC";

--
-- Name: rental_log_log_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: VC
--

ALTER SEQUENCE public.rental_log_log_id_seq OWNED BY public.rental_log.log_id;


--
-- Name: rental_log_updates; Type: TABLE; Schema: public; Owner: replicator
--

CREATE TABLE public.rental_log_updates (
    id integer NOT NULL,
    column_name text,
    old_value text,
    new_value text,
    "time" timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public.rental_log_updates OWNER TO replicator;

--
-- Name: rental_log_updates_id_seq; Type: SEQUENCE; Schema: public; Owner: replicator
--

CREATE SEQUENCE public.rental_log_updates_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.rental_log_updates_id_seq OWNER TO replicator;

--
-- Name: rental_log_updates_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: replicator
--

ALTER SEQUENCE public.rental_log_updates_id_seq OWNED BY public.rental_log_updates.id;


--
-- Name: summa; Type: TABLE; Schema: public; Owner: VC
--

CREATE TABLE public.summa (
    id integer,
    name integer
);


ALTER TABLE public.summa OWNER TO "VC";

--
-- Name: rental_log log_id; Type: DEFAULT; Schema: public; Owner: VC
--

ALTER TABLE ONLY public.rental_log ALTER COLUMN log_id SET DEFAULT nextval('public.rental_log_log_id_seq'::regclass);


--
-- Name: rental_log_updates id; Type: DEFAULT; Schema: public; Owner: replicator
--

ALTER TABLE ONLY public.rental_log_updates ALTER COLUMN id SET DEFAULT nextval('public.rental_log_updates_id_seq'::regclass);


--
-- Data for Name: rental_log; Type: TABLE DATA; Schema: public; Owner: VC
--

COPY public.rental_log (log_id, rental_time, customer_id, film_id, amount, logged_on) FROM stdin;
1	2025-05-14 14:37:37.242614	10	11	14.99	2025-05-14 14:37:37.242614
\.


--
-- Data for Name: rental_log_updates; Type: TABLE DATA; Schema: public; Owner: replicator
--

COPY public.rental_log_updates (id, column_name, old_value, new_value, "time") FROM stdin;
1	amount	4.99	14.99	2025-05-14 15:19:29.424339
2	customer_id	1	10	2025-05-14 15:20:08.758319
3	film_id	100	11	2025-05-14 15:20:08.758319
\.


--
-- Data for Name: summa; Type: TABLE DATA; Schema: public; Owner: VC
--

COPY public.summa (id, name) FROM stdin;
1	12
\.


--
-- Name: rental_log_log_id_seq; Type: SEQUENCE SET; Schema: public; Owner: VC
--

SELECT pg_catalog.setval('public.rental_log_log_id_seq', 1, true);


--
-- Name: rental_log_updates_id_seq; Type: SEQUENCE SET; Schema: public; Owner: replicator
--

SELECT pg_catalog.setval('public.rental_log_updates_id_seq', 3, true);


--
-- Name: rental_log rental_log_pkey; Type: CONSTRAINT; Schema: public; Owner: VC
--

ALTER TABLE ONLY public.rental_log
    ADD CONSTRAINT rental_log_pkey PRIMARY KEY (log_id);


--
-- Name: rental_log_updates rental_log_updates_pkey; Type: CONSTRAINT; Schema: public; Owner: replicator
--

ALTER TABLE ONLY public.rental_log_updates
    ADD CONSTRAINT rental_log_updates_pkey PRIMARY KEY (id);


--
-- Name: rental_log update_amount; Type: TRIGGER; Schema: public; Owner: VC
--

CREATE TRIGGER update_amount AFTER UPDATE ON public.rental_log FOR EACH ROW EXECUTE FUNCTION public.update_audit_log('amount');


--
-- Name: rental_log update_customer_id; Type: TRIGGER; Schema: public; Owner: VC
--

CREATE TRIGGER update_customer_id AFTER UPDATE ON public.rental_log FOR EACH ROW EXECUTE FUNCTION public.update_audit_log('customer_id');


--
-- Name: rental_log update_film_id; Type: TRIGGER; Schema: public; Owner: VC
--

CREATE TRIGGER update_film_id AFTER UPDATE ON public.rental_log FOR EACH ROW EXECUTE FUNCTION public.update_audit_log('film_id');


--
-- Name: rental_log update_rental_time; Type: TRIGGER; Schema: public; Owner: VC
--

CREATE TRIGGER update_rental_time AFTER UPDATE ON public.rental_log FOR EACH ROW EXECUTE FUNCTION public.update_audit_log('rental_time');


--
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: pg_database_owner
--

GRANT ALL ON SCHEMA public TO replicator;


--
-- Name: TABLE rental_log; Type: ACL; Schema: public; Owner: VC
--

GRANT ALL ON TABLE public.rental_log TO replicator;


--
-- Name: SEQUENCE rental_log_log_id_seq; Type: ACL; Schema: public; Owner: VC
--

GRANT ALL ON SEQUENCE public.rental_log_log_id_seq TO replicator;


--
-- Name: TABLE summa; Type: ACL; Schema: public; Owner: VC
--

GRANT ALL ON TABLE public.summa TO replicator;


--
-- PostgreSQL database dump complete
--

