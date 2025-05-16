-- You are tasked with building a PostgreSQL-backed database for an EdTech company that manages online training and certification programs for individuals across various technologies.

-- The goal is to:

-- Design a normalized schema

-- Support querying of training data

-- Ensure secure access

-- Maintain data integrity and control over transactional updates

-- Database planning (Nomalized till 3NF)

-- A student can enroll in multiple courses

-- Each course is led by one trainer

-- Students can receive a certificate after passing

-- Each certificate has a unique serial number

-- Trainers may teach multiple courses


-- Trainers
-- Trainer_id(PK), trainer_name, address_id(FK), Age

-- Students
-- stud_id(PK), student_name, address_id(FK), Phone_number, year_of_passing, Age

-- Courses
-- Course_id(PK), Course_name, Technology, Capacity, Trainer_id(FK)

-- Assessment
-- Assessment_id(PK), course_id, Assessment_name, date, max_mark, pass_mark

-- Scores
-- Score_id(PK), Assessment_id(FK), student_id(FK), Marks, Grade, Passed

-- Enrollments
-- Enrollment_id(PK), Course_id(FK), stud_id(FK), Date_of_enrollment

-- Certificates
-- Serial_number(PK), Student_id, Course_id, Duration, Issue_date

-- Address
-- Address_id(PK), Address_line1, Address_line2, Zipcode(FK)

-- Zipcodes
-- Zipcode(PK), State_id(FK)

-- States
-- State_id(PK), State_name, Country_id(FK)

-- Countries
-- Country_id(PK), Country_name


-- Tables to Design (Normalized to 3NF):

-- Phase 2: DDL & DML

-- * Create all tables with appropriate constraints (PK, FK, UNIQUE, NOT NULL)
-- * Insert sample data using `INSERT` statements
-- * Create indexes on `student_id`, `email`, and `course_id`

-- 1. **students**
--    * `student_id (PK)`, `name`, `email`, `phone`

BEGIN;

CREATE TABLE students(
	student_id INT PRIMARY KEY, 
	name VARCHAR(100) NOT NULL, 
	email VARCHAR(100) UNIQUE, 
	phone VARCHAR(13),
	CHECK (char_length(phone) >= 10)
);

-- 2. **courses**
--    * `course_id (PK)`, `course_name`, `category`, `duration_days`

CREATE TABLE courses(
	course_id INT PRIMARY KEY, 
	course_name VARCHAR(100) NOT NULL,
	category VARCHAR(100) NOT NULL,
	duration_days INT NOT NULL CHECK (duration_days > 0)
);

-- 3. **trainers**
--    * `trainer_id (PK)`, `trainer_name`, `expertise`

CREATE TABLE trainers(
	trainer_id INT PRIMARY KEY,
	trainer_name VARCHAR(100) NOT NULL,
	expertise VARCHAR(100) NOT NULL
);

-- 4. **enrollments**
--    * `enrollment_id (PK)`, `student_id (FK)`, `course_id (FK)`, `enroll_date`

CREATE TABLE enrollment(
	enrollment_id INT PRIMARY KEY, 
	student_id INT REFERENCES students(student_id) NOT NULL,
	course_id INT REFERENCES courses(course_id) NOT NULL,
	enroll_date DATE NOT NULL DEFAULT CURRENT_DATE
);

-- 5. **certificates**
--    * `certificate_id (PK)`, `enrollment_id (FK)`, `issue_date`, `serial_no`

CREATE TABLE certificates(
	certificate_id INT PRIMARY KEY,
	enrollment_id INT REFERENCES enrollment(enrollment_id) NOT NULL,
	issue_date DATE NOT NULL DEFAULT CURRENT_DATE,
	serial_no SERIAL UNIQUE
);

-- 6. **course_trainers** (Many-to-Many if needed)
--    * `course_id`, `trainer_id`

CREATE TABLE course_trainers(
	course_id INT REFERENCES courses(course_id) NOT NULL,
	trainer_id INT REFERENCES trainers(trainer_id) NOT NULL
);

CREATE INDEX idx_students_email ON students(email);
CREATE INDEX idx_students_id ON students(student_id);
CREATE INDEX idx_courses_id ON courses(course_id);

COMMIT;


BEGIN;

INSERT INTO students(student_id, name, email, phone) VALUES
(1, 'Alice Johnson', 'alice.johnson@example.com', '1234567890'),
(2, 'Bob Smith', 'bob.smith@example.com', '9876543210'),
(3, 'Charlie Brown', 'charlie.brown@example.com', '5556667777'),
(4, 'Diana Prince', 'diana.prince@example.com', '4445556666'),
(5, 'Evan Davis', 'evan.davis@example.com', '3332221111');

INSERT INTO courses(course_id, course_name, category, duration_days) VALUES
(101, 'Python Programming', 'Programming', 30),
(102, 'Data Science Basics', 'Data Science', 45),
(103, 'Web Development', 'Programming', 40);

INSERT INTO trainers(trainer_id, trainer_name, expertise) VALUES
(201, 'John Doe', 'Python Programming'),
(202, 'Jane Smith', 'Data Science'),
(203, 'Emily Clark', 'Web Development');

INSERT INTO course_trainers(course_id, trainer_id) VALUES
(101, 201),
(102, 202),
(103, 203);

INSERT INTO enrollment(enrollment_id, student_id, course_id, enroll_date) VALUES
(1001, 1, 101, '2025-01-10'),
(1002, 1, 102, '2025-02-15'),  
(1003, 2, 101, '2025-03-01'),
(1004, 3, 103, '2025-03-20'),
(1005, 4, 102, '2025-04-10'),
(1006, 5, 101, '2025-05-05'),
(1007, 5, 103, '2025-05-15');

INSERT INTO certificates(certificate_id, enrollment_id, issue_date) VALUES
(5001, 1001, '2025-02-15'),  
(5002, 1002, '2025-03-20'),  
(5003, 1003, '2025-04-01'), 
(5004, 1005, '2025-05-15'),  
(5005, 1007, '2025-06-20');

COMMIT;

SELECT * FROM students;
SELECT * FROM courses;
SELECT * FROM trainers;
SELECT * FROM course_trainers;
SELECT * FROM enrollment;
SELECT * FROM certificates;


-- Phase 3: SQL Joins Practice
-- Write queries to:

-- 1. List students and the courses they enrolled in

SELECT s.name, c.course_name
FROM students s
	JOIN enrollment e ON s.student_id = e.student_id
	JOIN courses c ON e.course_id = c.course_id
	ORDER BY s.name;
 

-- 2. Show students who received certificates with trainer names

SELECT s.name, t.trainer_name
FROM students s 
	JOIN enrollment e ON s.student_id = e.student_id
	JOIN certificates c ON c.enrollment_id = e.enrollment_id
	JOIN course_trainers ct ON e.course_id = ct.course_id
	JOIN trainers t ON t.trainer_id = ct.trainer_id
	WHERE c.certificate_id IS NOT NULL
	ORDER BY s.name;
	
-- 3. Count number of students per course

SELECT s.student_id, s.name, COUNT(*)
FROM students s
	JOIN enrollment e ON e.student_id = s.student_id
	GROUP BY s.student_id
	ORDER BY s.student_id;
	
-- ---

-- Phase 4: Functions & Stored Procedures

-- Function:

-- Create `get_certified_students(course_id INT)`
-- → Returns a list of students who completed the given course and received certificates.

CREATE OR REPLACE FUNCTION get_certified_students(courseid INT)
RETURNS TABLE(student_id INT, name VARCHAR(100))
AS $$
BEGIN
	RETURN QUERY
	SELECT s.student_id, s.name
	FROM students s 
		JOIN enrollment e ON s.student_id = e.student_id
		JOIN certificates c ON c.enrollment_id = e.enrollment_id
		WHERE certificate_id IS NOT NULL
		AND e.course_id = courseid
		ORDER BY s.student_id;
END;
$$ LANGUAGE plpgsql;


SELECT * FROM get_certified_students(102);

-- Stored Procedure:

-- Create `sp_enroll_student(p_student_id, p_course_id)`
-- → Inserts into `enrollments` and conditionally adds a certificate if completed (simulate with status flag).

CREATE OR REPLACE PROCEDURE sp_enroll_student(p_student_id INT, p_course_id INT, status BOOLEAN)
AS $$
DECLARE
	current_enroll_id INT;
	current_certificate_id INT;
BEGIN
	SELECT MAX(enrollment_id)+1 
	INTO current_enroll_id 
	FROM enrollment;
	
	INSERT INTO enrollment(enrollment_id, student_id, course_id)
	VALUES(current_enroll_id, p_student_id, p_course_id);

	IF status THEN 
		SELECT MAX(certificate_id)+1 
		INTO current_certificate_id 
		FROM certificates;
		
		INSERT INTO certificates(certificate_id, enrollment_id)
		VALUES (current_certificate_id, current_enroll_id);
	END IF;

EXCEPTION WHEN OTHERS THEN
	RAISE NOTICE 'Insertion Failed: %', SQLERRM;
END;
$$ LANGUAGE plpgsql;


CALL sp_enroll_student(5, 102, TRUE);

SELECT * FROM enrollment;

SELECT * FROM certificates;

CALL sp_enroll_student(2, 102, FALSE);

-- Phase 5: Cursor
-- Use a cursor to:
-- * Loop through all students in a course
-- * Print name and email of those who do not yet have certificates


	
DO $$
DECLARE 
	cur CURSOR FOR
		SELECT s.name, s.email
		FROM students s 
		LEFT JOIN enrollment e ON e.student_id = s.student_id
		LEFT JOIN certificates c ON c.enrollment_id = e.enrollment_id
		WHERE c.certificate_id IS NULL;
	rec RECORD;
BEGIN
	OPEN cur;
	LOOP
		FETCH cur INTO rec;
		EXIT WHEN NOT FOUND;

		RAISE NOTICE 
			'Name : % Email : %',
			rec.name,
			rec.email;
	END LOOP;

	CLOSE cur;
END;
$$

-- ---

-- Phase 6: Security & Roles

-- 1. Create a `readonly_user` role:

--    * Can run `SELECT` on `students`, `courses`, and `certificates`
--    * Cannot `INSERT`, `UPDATE`, or `DELETE`

CREATE ROLE readonly 
WITH LOGIN PASSWORD 'pass123';

GRANT SELECT ON students, courses, certificates TO readonly;


-- 2. Create a `data_entry_user` role:

--    * Can `INSERT` into `students`, `enrollments`
--    * Cannot modify certificates directly

CREATE ROLE data_entry_user 
WITH LOGIN PASSWORD 'pass123';

GRANT SELECT ON ALL TABLES IN SCHEMA public TO data_entry_user;

GRANT INSERT ON students, enrollment TO data_entry_user;

-- ---

-- Phase 7: Transactions & Atomicity

-- Write a transaction block that:

-- * Enrolls a student
-- * Issues a certificate
-- * Fails if certificate generation fails (rollback)

-- ```sql
-- BEGIN;
-- -- insert into enrollments
-- -- insert into certificates
-- -- COMMIT or ROLLBACK on error
-- ```