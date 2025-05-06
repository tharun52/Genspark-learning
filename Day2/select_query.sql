-- 1) Print all the titles names
 select title from titles;

-- 2) Print all the titles that have been published by 1389
 SELECT title FROM titles WHERE pub_id = 1389;
 
-- 3) Print the books that have price in range of 10 to 15
 select title from titles WHERE price BETWEEN 10 AND 15;

-- 4) Print those books that have no price
select title from titles WHERE price IS NULL;
 
-- 5) Print the book names that starts with 'The'
select title from titles WHERE title LIKE 'the%';

-- 6) Print the book names that do not have 'v' in their name
select title from titles WHERE title NOT LIKE '%V%';
 
-- 7) print the books sorted by the royalty
 select t.title from titles t 
JOIN roysched r ON t.title_id=r.title_id  
ORDER BY r.royalty;

-- 8) print the books sorted by publisher in descending then by types in ascending then by price in descending
 select t.title from titles t 
JOIN publishers p ON t.pub_id=t.pub_id  
ORDER BY p.pub_name DESC, t.type, t.price DESC;
 
-- 9) Print the average price of books in every type
 select type, AVG(price) as "AVERAGE OF PRICE" FROM	titles GROUP BY type;

-- 10) print all the types in unique
 select DISTINCT(type) FROM titles;

-- 11) Print the first 2 costliest books
 SELECT TOP 2 title, price FROM titles ORDER BY price DESC;

-- 12) Print books that are of type business and have price less than 20 which also have advance greater than 7000
 SELECT title FROM titles 
WHERE type='business' AND price<30 AND advance>7000;

-- 13) Select those publisher id and number of books which have price between 15 to 25 and have 'It' in its name. Print only those which have count greater than 2. Also sort the result in ascending order of count
SELECT pub_id, COUNT(title_id) AS book_count FROM titles t
WHERE price BETWEEN 15 AND 25
  AND title LIKE '%it%'
GROUP BY pub_id
HAVING COUNT(title_id) > 2
ORDER BY book_count;

-- 14) Print the Authors who are from 'CA'
SELECT CONCAT(au_fname, ' ', au_lname) as Name
from authors 
WHERE state='CA';

-- 15) Print the count of authors from every state
SELECT state, COUNT(*)AS 'Authors count'
FROM authors
GROUP BY state;
