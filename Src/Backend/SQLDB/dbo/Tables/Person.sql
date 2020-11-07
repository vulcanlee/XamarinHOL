CREATE TABLE [dbo].[Person] (
    [PersonID]       INT           IDENTITY (1, 1) NOT NULL,
    [LastName]       NVARCHAR (50) NOT NULL,
    [FirstName]      NVARCHAR (50) NOT NULL,
    [HireDate]       DATETIME      NULL,
    [EnrollmentDate] DATETIME      NULL,
    CONSTRAINT [PK_School.Student] PRIMARY KEY CLUSTERED ([PersonID] ASC)
);

