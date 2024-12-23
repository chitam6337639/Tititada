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



select *
from employees e 