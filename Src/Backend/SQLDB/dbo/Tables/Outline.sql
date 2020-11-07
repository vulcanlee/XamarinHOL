CREATE TABLE [dbo].[Outline] (
    [OutlineID] INT            IDENTITY (1, 1) NOT NULL,
    [CourseID]  INT            NOT NULL,
    [Title]     NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Outline] PRIMARY KEY CLUSTERED ([OutlineID] ASC),
    CONSTRAINT [FK_Outline_Course] FOREIGN KEY ([CourseID]) REFERENCES [dbo].[Course] ([CourseID])
);

