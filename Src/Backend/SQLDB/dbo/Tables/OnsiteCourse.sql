CREATE TABLE [dbo].[OnsiteCourse] (
    [CourseID] INT           NOT NULL,
    [Location] NVARCHAR (50) NOT NULL,
    [Days]     NVARCHAR (50) NOT NULL,
    [Time]     SMALLDATETIME NOT NULL,
    CONSTRAINT [PK_OnsiteCourse] PRIMARY KEY CLUSTERED ([CourseID] ASC),
    CONSTRAINT [FK_OnsiteCourse_Course] FOREIGN KEY ([CourseID]) REFERENCES [dbo].[Course] ([CourseID])
);

