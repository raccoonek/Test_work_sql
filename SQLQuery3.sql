 WITH t AS(
 SELECT d.name,
		 SUM (Salary) sum_salary,
         COUNT (*) count_employees
    FROM Employee e JOIN Departament d ON e.Departament_id = d.id
GROUP BY d.name
)
SELECT*
FROM t
WHERE sum_salary = (SELECT MAX(sum_salary) max_sum_salary
FROM t)