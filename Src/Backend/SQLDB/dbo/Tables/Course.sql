CREATE TABLE [dbo].[Course] (
    [CourseID]     INT            NOT NULL,
    [Title]        NVARCHAR (100) NOT NULL,
    [Credits]      INT            NOT NULL,
    [DepartmentID] INT            NOT NULL,
    CONSTRAINT [PK_School.Course] PRIMARY KEY CLUSTERED ([CourseID] ASC),
    CONSTRAINT [FK_Course_Department] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[Department] ([DepartmentID])
);

