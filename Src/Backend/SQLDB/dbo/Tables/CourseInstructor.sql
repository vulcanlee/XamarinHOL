CREATE TABLE [dbo].[CourseInstructor] (
    [CourseID] INT NOT NULL,
    [PersonID] INT NOT NULL,
    CONSTRAINT [PK_CourseInstructor] PRIMARY KEY CLUSTERED ([CourseID] ASC, [PersonID] ASC),
    CONSTRAINT [FK_CourseInstructor_Course] FOREIGN KEY ([CourseID]) REFERENCES [dbo].[Course] ([CourseID]),
    CONSTRAINT [FK_CourseInstructor_Person] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([PersonID])
);

