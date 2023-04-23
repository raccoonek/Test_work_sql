SELECT*FROM Employee WHERE salary=(SELECT MAX(salary) FROM Employee);
