
CREATE TABLE emp (
	empno INTEGER, 
	empname TEXT NOT NULL, 
	salary INTEGER CHECK (salary>0),
	deptname VARCHAR(100),
	bossno INTEGER,
	PRIMARY KEY(empno),
	FOREIGN KEY (deptname) REFERENCES department(deptname),
	FOREIGN KEY (bossno) REFERENCES emp(empno)
); 

CREATE TABLE department(
	deptname TEXT, 
	floor INTEGER, 
    Deptphone integer,
	managerid INTEGER, 
	PRIMARY KEY(deptname),
	FOREIGN KEY(managerid) REFERENCES emp(empno)	
);



ALTER TABLE emp ADD deptname VARCHAR(100) NOT NULL,FOREIGN KEY (deptname) REFERENCES department(deptname);


CREATE TABLE item(
	itemname VARCHAR(100),
	itemtype VARCHAR(100),
	itemcolor VARCHAR(100),
	PRIMARY KEY(itemname)
);



CREATE TABLE sales(
	salesno INTEGER,
	saleqty INTEGER,
	itemname VARCHAR(100) NOT NULL,
	deptname VARCHAR(100), 
	PRIMARY KEY(salesno),
	FOREIGN KEY(itemname) REFERENCES item(itemname),
	FOREIGN KEY(deptname) REFERENCES department(deptname)
);



INSERT INTO EMP (empno, empname, salary, deptname, bossno) VALUES
(1, 'Alice', 75000, NULL, NULL),
(2, 'Ned', 45000, NULL, 1),
(3, 'Andrew', 25000, NULL, 2),
(4, 'Clare', 22000, NULL, 2),
(5, 'Todd', 38000, NULL, 1),
(6, 'Nancy', 22000, NULL, 5),
(7, 'Brier', 43000, NULL, 1),
(8, 'Sarah', 56000, NULL, 7),
(9, 'Sophia', 35000, NULL, 1),
(10, 'Sanjay', 15000, NULL, 3),
(11, 'Rita', 15000, NULL, 4),
(12, 'Gigi', 16000, NULL, 4),
(13, 'Maggie', 11000, NULL, 4),
(14, 'Paul', 15000, NULL, 3),
(15, 'James', 15000, NULL, 3),
(16, 'Pat', 15000, NULL, 3),
(17, 'Mark', 15000, NULL, 3);

INSERT INTO DEPARTMENT (deptname, floor, phone, empno) VALUES
('Management', 5, '34', 1),
('Books', 1, '81', 4),
('Clothes', 2, '24', 4),
('Equipment', 3, '57', 3),
('Furniture', 4, '14', 3),
('Navigation', 1, '41', 3),
('Recreation', 2, '29', 4),
('Accounting', 5, '35', 5),
('Purchasing', 5, '36', 7),
('Personnel', 5, '37', 9),
('Marketing', 5, '38', 2);

UPDATE EMP
SET deptname = 'Management' WHERE empno = 1;
UPDATE EMP
SET deptname = 'Marketing' WHERE empno = 2;
UPDATE EMP
SET deptname = 'Accounting' WHERE empno = 5;
UPDATE EMP
SET deptname = 'Purchasing' WHERE empno = 7;
UPDATE EMP
SET deptname = 'Personnel' WHERE empno = 9;
UPDATE EMP
SET deptname = 'Navigation' WHERE empno = 3;
UPDATE EMP
SET deptname = 'Books' WHERE empno = 4;
UPDATE EMP
SET deptname = 'Clothes' WHERE empno = 4;
UPDATE EMP
SET deptname = 'Recreation' WHERE empno = 4;


INSERT INTO ITEM (itemname, itemtype, itemcolor) VALUES
('Pocket Knife-Nile', 'E', 'Brown'),
('Pocket Knife-Avon', 'E', 'Brown'),
('Compass', 'N', '--'),
('Geo positioning system', 'N', '--'),
('Elephant Polo stick', 'R', 'Bamboo'),
('Camel Saddle', 'R', 'Brown'),
('Sextant', 'N', '--'),
('Map Measure', 'N', '--'),
('Boots-snake proof', 'C', 'Green'),
('Pith Helmet', 'C', 'Khaki'),
('Hat-polar Explorer', 'C', 'White'),
('Exploring in 10 Easy Lessons', 'B', '--'),
('Hammock', 'F', 'Khaki'),
('How to win Foreign Friends', 'B', '--'),
('Map case', 'E', 'Brown'),
('Safari Chair', 'F', 'Khaki'),
('Safari cooking kit', 'F', 'Khaki'),
('Stetson', 'C', 'Black'),
('Tent - 2 person', 'F', 'Khaki'),
('Tent -8 person', 'F', 'Khaki');

INSERT INTO SALES (salesno, saleqty, itemname, deptname) VALUES
(101, 2, 'Boots-snake proof', 'Clothes'),
(102, 1, 'Pith Helmet', 'Clothes'),
(103, 1, 'Sextant', 'Navigation'),
(104, 3, 'Hat-polar Explorer', 'Clothes'),
(105, 5, 'Pith Helmet', 'Equipment'),
(106, 2, 'Pocket Knife-Nile', 'Clothes'),
(107, 3, 'Pocket Knife-Nile', 'Recreation'),
(108, 1, 'Compass', 'Navigation'),
(109, 2, 'Geo positioning system', 'Navigation'),
(110, 5, 'Map Measure', 'Navigation'),
(111, 1, 'Geo positioning system', 'Books'),
(112, 1, 'Sextant', 'Books'),
(113, 3, 'Pocket Knife-Nile', 'Books'),
(114, 1, 'Pocket Knife-Nile', 'Navigation'),
(115, 1, 'Pocket Knife-Nile', 'Equipment'),
(116, 1, 'Sextant', 'Clothes'),
(117, 1, '', 'Equipment'),
(118, 1, '', 'Recreation'),
(119, 1, '', 'Furniture'),
(120, 1, 'Pocket Knife-Nile', ''),
(121, 1, 'Exploring in 10 Easy Lessons', 'Books'),
(122, 1, 'How to win foreign friends', ''),
(123, 1, 'Compass', ''),
(124, 1, 'Pith Helmet', ''),
(125, 1, 'Elephant Polo stick', 'Recreation'),
(126, 1, 'Camel Saddle', 'Recreation');

SELECT * FROM emp;
SELECT * FROM department;
SELECT * FROM sales;
SELECT * FROM item;

