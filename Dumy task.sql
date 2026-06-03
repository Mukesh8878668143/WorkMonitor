DECLARE @TaskNo INT = 1;

WHILE @TaskNo <= 200
BEGIN
    INSERT INTO Tasks
    (
        Title,
        Description,
        Status,
        Priority,
        CreatedDate,
        DueDate,
        UserId
    )
    VALUES
    (
        CASE (@TaskNo % 20)
            WHEN 1 THEN 'Design Dashboard UI'
            WHEN 2 THEN 'Implement JWT Authentication'
            WHEN 3 THEN 'Create User Management API'
            WHEN 4 THEN 'Fix Login Bug'
            WHEN 5 THEN 'Develop Reports Module'
            WHEN 6 THEN 'Database Optimization'
            WHEN 7 THEN 'Create Task Filters'
            WHEN 8 THEN 'Implement Notifications'
            WHEN 9 THEN 'Write Unit Tests'
            WHEN 10 THEN 'Code Review'
            WHEN 11 THEN 'Setup CI/CD Pipeline'
            WHEN 12 THEN 'Add Swagger Documentation'
            WHEN 13 THEN 'Performance Testing'
            WHEN 14 THEN 'Security Audit'
            WHEN 15 THEN 'Implement Role Management'
            WHEN 16 THEN 'Refactor Services'
            WHEN 17 THEN 'Create Dashboard Widgets'
            WHEN 18 THEN 'Deploy to Staging'
            WHEN 19 THEN 'Fix API Validation'
            ELSE 'General Development Task'
        END,

        CONCAT('Task description for work item #', @TaskNo),

        CASE
            WHEN @TaskNo <= 80 THEN 1      -- Pending
            WHEN @TaskNo <= 140 THEN 2     -- In Progress
            ELSE 3                         -- Completed
        END,

        CASE
            WHEN @TaskNo % 3 = 0 THEN 3    -- High
            WHEN @TaskNo % 2 = 0 THEN 2    -- Medium
            ELSE 1                         -- Low
        END,

        DATEADD(DAY, -(@TaskNo % 30), GETDATE()),
        DATEADD(DAY, (@TaskNo % 45), GETDATE()),

        ((@TaskNo - 1) % 50) + 1
    );

    SET @TaskNo = @TaskNo + 1;
END;