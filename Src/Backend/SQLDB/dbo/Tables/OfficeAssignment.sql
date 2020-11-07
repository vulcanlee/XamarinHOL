CREATE TABLE [dbo].[OfficeAssignment] (
    [InstructorID] INT           NOT NULL,
    [Location]     NVARCHAR (50) NOT NULL,
    [Timestamp]    ROWVERSION    NOT NULL,
    CONSTRAINT [PK_OfficeAssignment] PRIMARY KEY CLUSTERED ([InstructorID] ASC),
    CONSTRAINT [FK_OfficeAssignment_Person] FOREIGN KEY ([InstructorID]) REFERENCES [dbo].[Person] ([PersonID])
);

