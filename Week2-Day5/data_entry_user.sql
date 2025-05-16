SELECT * FROM students;

SELECT * FROM enrollment;

SELECT * FROM certificates;


INSERT INTO students
VALUES(7, 'Robb Stark', 'robb@winterfall.com', 9884353132);

INSERT INTO enrollment
VALUES (1010, 3, 101, CURRENT_DATE);

--Access denied
INSERT INTO certificates
VALUES(5007, 1006, CURRENT_DATE);
