WITH tree(id, Chief_id, lvl) AS (
    SELECT id, Chief_id, 0
    FROM Employee WHERE Chief_id IS NULL
    UNION ALL
    SELECT Employee.id, Employee.Chief_id, lvl+1
    FROM Employee INNER JOIN tree on tree.id = Employee.Chief_id
)
SELECT MAX(lvl) max_length
FROM tree;