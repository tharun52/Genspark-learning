Design the database for a shop which sells products
-- Points for consideration
--   1) One product can be supplied by many suppliers
--   2) One supplier can supply many products
--   3) All customers details have to present
--   4) A customer can buy more than one product in every purchase
--   5) Bill for every purchase has to be stored
--   6) These are just details of one shop

 
-- country
-- id, name

CREATE TABLE country(
    id INTEGER, 
    name TEXT, 
    PRIMARY KEY(id)
);
 
-- state
-- id, name, country_id

CREATE TABLE state(
    id INTEGER, 
    name NOT NULL TEXT, 
    country_id INTEGER, 
    PRIMARY KEY(id), 
    FOREIGN KEY(country_id) 
    REFERENCES country(id)
);


-- City
-- id, name, state_id

CREATE TABLE city(
    id INTEGER, 
    name NOT NULL TEXT, 
    state_id INTEGER, 
    PRIMARY KEY(id), 
    FOREIGN KEY(state_id) REFERENCES state(id)
);

-- area
-- zipcode, name, city_id

CREATE TABLE area(
    zipcode INTEGER, 
    name NOT NULL TEXT, 
    city_id INTEGER, 
    PRIMARY KEY(zipcode), 
    FOREIGN KEY(city_id) REFERENCES city(id)
);

 
-- address
-- id, door_number, addressline1, zipcode
 
CREATE TABLE address(
    id INTEGER, 
    door_number TEXT, 
    addressline1 TEXT, 
    zipcode INTEGER,
    PRIMARY KEY(id), 
    FOREIGN KEY(zipcode) REFERENCES area(zipcode)
);

-- supplier
-- id, name, contact_person, phone, email, address_id, status

CREATE TABLE supplier(
    id INTEGER, 
    name NOT NULL TEXT, 
    contact_person TEXT, 
    phone VARCHAR(10) NOT NULL CHECK (phone ~ '^[0-9]{10}$'),
    email TEXT CHECK (email LIKE '%@%'),
    address_id INTEGER,
    status TEXT, 
    PRIMARY KEY(id), 
    FOREIGN KEY(address_id) REFERENCES address(id)
);

-- product
-- id, Name, unit_price, quantity, description, image,

CREATE TABLE product(
    id INTEGER, 
    name NOT NULL TEXT, 
    unit_price DECIMAL(10, 2), 
    quantity INTEGER, 
    description TEXT, 
    image TEXT,
    PRIMARY KEY(id)
);

-- product_supplier
-- transaction_id, product_id, supplier_id, date_of_supply, quantity,
 
CREATE TABLE product_supplier(
    transaction_id INTEGER, 
    product_id INTEGER, 
    supplier_id INTEGER, 
    date_of_supply DATE,
    PRIMARY KEY(transaction_id), 
    FOREIGN KEY(product_id) REFERENCES product(id),
    FOREIGN KEY(supplier_id) REFERENCES supplier(id)
);

-- Customer
-- id, Name, Phone, age, address_id

CREATE TABLE customers (
    id INTEGER, 
    name NOT NULL TEXT, 
    phone VARCHAR(10) NOT NULL CHECK (phone ~ '^[0-9]{10}$'),
    age INTEGER, 
    address_id INTEGER,
    PRIMARY KEY(id), 
    FOREIGN KEY(address_id) REFERENCES address(id)
);
 
-- orders
--   order_number, customer_id, Date_of_order, amount, order_status

CREATE TABLE order(
    order_id INTEGER, 
    customer_id INTEGER,  
    date_of_order DATE,
    amount DECIMAL(10, 2), 
    order_status TEXT,
    PRIMARY KEY(order_id), 
);


-- order_details
--   id, order_number, product_id, quantity, unit_price

CREATE TABLE order_details(
    id INTEGER, 
    order_number INTEGER, 
    product_id INTEGER, 
    quantity INTEGER, 
    unit_price DECIMAL(10, 2),
    PRIMARY KEY(id), 
    FOREIGN KEY(order_number) REFERENCES orders(order_number),
    FOREIGN KEY(product_id) REFERENCES product(id)    
)

