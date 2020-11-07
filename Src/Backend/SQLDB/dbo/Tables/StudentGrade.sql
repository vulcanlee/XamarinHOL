CREATE TABLE [dbo].[StudentGrade] (
    [EnrollmentID] INT            IDENTITY (1, 1) NOT NULL,
    [CourseID]     INT            NOT NULL,
    [StudentID]    INT            NOT NULL,
    [Grade]        DECIMAL (3, 2) NULL,
    CONSTRAINT [PK_StudentGrade] PRIMARY KEY CLUSTERED ([EnrollmentID] ASC),
    CONSTRAINT [FK_StudentGrade_Course] FOREIGN KEY ([CourseID]) REFERENCES [dbo].[Course] ([CourseID]),
    CONSTRAINT [FK_StudentGrade_Student] FOREIGN KEY ([StudentID]) REFERENCES [dbo].[Person] ([PersonID])
);

