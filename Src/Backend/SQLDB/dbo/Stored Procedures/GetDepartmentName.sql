
CREATE PROCEDURE [dbo].[GetDepartmentName]
      @ID int,
      @Name nvarchar(50) OUTPUT
      AS
      SELECT @Name = Name FROM Department
      WHERE DepartmentID = @ID