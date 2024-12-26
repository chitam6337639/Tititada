CREATE TABLE employees (
    employee_id SERIAL PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    hire_date DATE NOT NULL,
    salary NUMERIC(10, 2) NOT NULL,
    department_id INT REFERENCES departments(department_id)
);

CREATE TABLE projects (
    project_id SERIAL PRIMARY KEY,
    project_name VARCHAR(100) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE,
    department_id INT REFERENCES departments(department_id)
);

INSERT INTO departments (department_name, location)
VALUES
    ('HR', 'New York'),
    ('IT', 'San Francisco'),
    ('Finance', 'Chicago'),
    ('Marketing', 'Los Angeles');

INSERT INTO employees (first_name, last_name, email, hire_date, salary, department_id)
VALUES
    ('Alice', 'Johnson', 'alice.johnson@example.com', '2020-01-15', 70000, 1),
    ('Bob', 'Smith', 'bob.smith@example.com', '2019-03-20', 80000, 2),
    ('Charlie', 'Brown', 'charlie.brown@example.com', '2021-07-10', 60000, 3),
    ('Diana', 'Prince', 'diana.prince@example.com', '2018-05-25', 75000, 2),
    ('Eve', 'Taylor', 'eve.taylor@example.com', '2022-09-30', 55000, 4);

SELECT first_name, last_name, email 
FROM employees 
WHERE department_id = (SELECT department_id FROM departments WHERE department_name = 'IT');

INSERT INTO employees (first_name, last_name, email, hire_date, salary, department_id)
VALUES ('Frank', 'White', 'frank.white@example.com', '2023-05-01', 50000, 4);

UPDATE employees
SET salary = salary * 1.10
WHERE department_id = (SELECT department_id FROM departments WHERE department_name = 'HR');


SELECT e.first_name, e.last_name, p.project_name
FROM assignments a
INNER JOIN employees e ON a.employee_id = e.employee_id
INNER JOIN projects p ON a.project_id = p.project_id;

SELECT e.first_name, e.last_name, p.project_name
FROM employees e
LEFT JOIN assignments a ON e.employee_id = a.employee_id
LEFT JOIN projects p ON a.project_id = p.project_id;

CREATE INDEX idx_department_id ON employees(department_id);


SELECT *
FROM employees e 


--function tổng lương của nhân viên trong 1 phòng ban
CREATE OR REPLACE FUNCTION totalSalaryDepartment(dept_name VARCHAR)
RETURNS NUMERIC as $$
DECLARE
    total_salary NUMERIC(10, 2);
BEGIN

    SELECT SUM(salary) INTO total_salary
    FROM employees e
    INNER JOIN departments d ON e.department_id = d.department_id
    WHERE d.department_name = dept_name;
    RETURN total_salary;
END;
$$ LANGUAGE plpgsql;



--lấy danh sách phòng ban
CREATE OR REPLACE FUNCTION getEmployeeDepartment(dept_name VARCHAR)
RETURNS TABLE(employee_id INT, first_name VARCHAR, last_name VARCHAR, email VARCHAR) as $$
BEGIN
    RETURN QUERY
    SELECT e.employee_id, e.first_name, e.last_name, e.email
    FROM employees e
    INNER JOIN departments d ON e.department_id = d.department_id
    WHERE d.department_name = dept_name;
END;
$$ LANGUAGE plpgsql;

-- gọi lại hàm
SELECT totalSalaryDepartment('IT');
SELECT getEmployeeDepartment('IT');


--LeetCode
--Ex1: https://leetcode.com/problems/rank-scores/
--Input: 
--Scores table:
+----+-------+
| id | score |
+----+-------+
| 1  | 3.50  |
| 2  | 3.65  |
| 3  | 4.00  |
| 4  | 3.85  |
| 5  | 4.00  |
| 6  | 3.65  |
+----+-------+
--Output: 
+-------+------+
| score | rank |
+-------+------+
| 4.00  | 1    |
| 4.00  | 1    |
| 3.85  | 2    |
| 3.65  | 3    |
| 3.65  | 3    |
| 3.50  | 4    |

--Query
select score, dense_rank () over (order by score desc) as rank
from scores


--Ex2: https://leetcode.com/problems/customers-who-never-order/
Input: 
Customers table:
+----+-------+
| id | name  |
+----+-------+
| 1  | Joe   |
| 2  | Henry |
| 3  | Sam   |
| 4  | Max   |
+----+-------+
Orders table:
+----+------------+
| id | customerId |
+----+------------+
| 1  | 3          |
| 2  | 1          |
+----+------------+
Output: 
+-----------+
| Customers |
+-----------+
| Henry     |
| Max       |
+-----------+

--Query
select name as "Customers"
from customers c left join orders o on c.id = o.customerId
where o.customerId is null

--Ex3: https://leetcode.com/problems/department-highest-salary/
Input: 
Employee table:
+----+-------+--------+--------------+
| id | name  | salary | departmentId |
+----+-------+--------+--------------+
| 1  | Joe   | 70000  | 1            |
| 2  | Jim   | 90000  | 1            |
| 3  | Henry | 80000  | 2            |
| 4  | Sam   | 60000  | 2            |
| 5  | Max   | 90000  | 1            |
+----+-------+--------+--------------+
Department table:
+----+-------+
| id | name  |
+----+-------+
| 1  | IT    |
| 2  | Sales |
+----+-------+
Output: 
+------------+----------+--------+
| Department | Employee | Salary |
+------------+----------+--------+
| IT         | Jim      | 90000  |
| Sales      | Henry    | 80000  |
| IT         | Max      | 90000  |
+------------+----------+--------+

--Query
select Department.name as Department, Employee.name as Employee, Salary
from Employee join Department on Department.id = Employee.departmentId
where (Employee.departmentId, Salary) in (select departmentId, max(Salary) 
                                          from Employee 
                                          group by departmentId);

--Ex4: https://leetcode.com/problems/find-customer-referee/
--Find the names of the customer that are not referred by the customer with id = 2.
--Return the result table in any order.
--The result format is in the following example.
Input: 
Customer table:
+----+------+------------+
| id | name | referee_id |
+----+------+------------+
| 1  | Will | null       |
| 2  | Jane | null       |
| 3  | Alex | 2          |
| 4  | Bill | null       |
| 5  | Zack | 1          |
| 6  | Mark | 2          |
+----+------+------------+
Output: 
+------+
| name |
+------+
| Will |
| Jane |
| Bill |
| Zack |
+------+

--Query
select name
from Customer
where COALESCE(referee_id, 0) <> 2;

--Ex5: https://leetcode.com/problems/customer-placing-the-largest-number-of-orders/
--Write a solution to find the customer_number for the customer who has placed the largest number of orders.
--The test cases are generated so that exactly one customer will have placed more orders than any other customer.
--The result format is in the following example.
Input: 
Orders table:
+--------------+-----------------+
| order_number | customer_number |
+--------------+-----------------+
| 1            | 1               |
| 2            | 2               |
| 3            | 3               |
| 4            | 3               |
+--------------+-----------------+
Output: 
+-----------------+
| customer_number |
+-----------------+
| 3               |
+-----------------+

--Query
select customer_number
from Orders
group by customer_number
order by count(*) desc
limit 1;


