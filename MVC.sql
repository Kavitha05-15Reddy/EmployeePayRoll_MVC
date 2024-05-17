create database MVC
use MVC

--Create
create table Employee
(
	EmployeeId int Identity(1,1) primary key,
	Name varchar(50) not null,
	ProfileImage varchar(100) not null,
	Gender varchar(10) not null,
	Department varchar(50) not null,
	Salary money not null,
	StartDate date not null,
	Notes varchar(max) not null
)

select * from Employee

--Insert
create or alter procedure Employee_Insert_SP
(
	@Name varchar(50),
	@ProfileImage varchar(100),
	@Gender varchar(10),
	@Department varchar(50),
	@Salary money,
	@StartDate date,
	@Notes varchar(max)
)
as 
begin
	-- Check if any parameter is NULL
	if @Name Is null or 
	@ProfileImage Is null or 
	@Gender Is null or 
	@Department Is null or
	@Salary Is null or
	@StartDate Is null or
	@Notes Is null 
	begin
		print 'All parameters must be provided.'
		return
	end

	-- Check if the Salary is greater than 0
	if @Salary <= 0
	begin 
		print 'Salary must be greater than 0.'
        return
	end

	-- Check if the StartDate is not in the future
	if @StartDate > GETDATE()
	begin
		print 'Start date cannot be in the future.'
		return
	end

	-- Insert the new employee record
	Insert into Employee (Name,ProfileImage,Gender,Department,Salary,StartDate,Notes)
	values (@Name,@ProfileImage,@Gender,@Department,@Salary,@StartDate,@Notes)

	-- Check if the record was inserted successfully
	if @@Rowcount = 1
		begin
			print 'Employee inserted successfully.'
		end
	else
		begin
			print 'Error inserting employee record.'
		end
end

Employee_Insert_SP 'Krish','krish.jpg','Male','Sales',40000,'8-5-2022','Purchases'


--Retrieve
create or alter procedure Employee_List_SP
as
begin
	begin try
		-- Check if the employee exists
		if exists (select 1 from Employee)
			begin
				select * from Employee
			end
		else
			begin 
				print 'No employees found.'
			end
	end try
    begin catch
        declare @ErrorMessage NVARCHAR(4000);
        declare @ErrorSeverity INT;
        declare @ErrorState INT;

        select @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();
        print 'Error: ' + @ErrorMessage;
    end catch
end

Employee_List_SP

--Update
create or alter procedure Employee_Update_SP
(
	@EmployeeId int,
	@Name varchar(50),
	@ProfileImage varchar(100),
	@Gender varchar(10),
	@Department varchar(50),
	@Salary money,
	@StartDate date,
	@Notes varchar(max)
)
as
begin
	begin try

		-- Check if the Salary is greater than 0
		if @Salary <= 0
		begin 
			print 'Salary must be greater than 0.'
			return
		end

		-- Check if the StartDate is not in the future
		if @StartDate > GETDATE()
		begin
			print 'Start date cannot be in the future.'
			return
		end
		-- Check if the employee exists
		if exists (select 1 from Employee where EmployeeId = @EmployeeId)
		begin
			 -- Update the employee's information
			 update Employee
			 set Name = @Name,
				 ProfileImage = @ProfileImage,
				 Gender = @Gender,
				 Department = @Department,
				 Salary = @Salary,
				 StartDate = @StartDate,
				 Notes = @Notes
			 where EmployeeId = @EmployeeId

			-- Check if any rows were affected by the update
			if @@ROWCOUNT = 1
			begin
				print 'Employee information updated successfully.'
			end
			else
			begin
				print 'Employee information could not be updated.'
			end
		end
		else
			begin
				print 'Employee not found.'
			end
	end try
	begin catch
        declare @ErrorMessage NVARCHAR(4000);
        declare @ErrorSeverity INT;
        declare @ErrorState INT;

        select @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();
        print 'Error: ' + @ErrorMessage;
    end catch
end


--Delete
create or alter procedure Employee_Delete_SP
@EmployeeId int
as
begin
	begin try
		-- Check if the employee exists
		if exists (select 1 from Employee where EmployeeId = @EmployeeId)
		begin
			 -- Delete the employee
			 delete from Employee where EmployeeId = @EmployeeId

			  -- Check if any rows were affected by the delete operation
			  if @@ROWCOUNT = 1
			  begin
				 print 'Employee deleted successfully.'
			  end
			  else
			  begin
				 print 'Employee could not be deleted.'
			  end
		end
		else
			begin
				print 'Employee not found.'
			end
	end try
	begin catch
        declare @ErrorMessage NVARCHAR(4000);
        declare @ErrorSeverity INT;
        declare @ErrorState INT;

        select @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();
        print 'Error: ' + @ErrorMessage;
    end catch
end

Employee_Delete_SP 9

--GetById
create or alter procedure Employee_GetById_SP
@EmployeeId int
as
begin
	begin try
		-- Check if the employee exists
		if exists (select 1 from Employee where EmployeeId = @EmployeeId)
			begin
				-- Retrieve the employee's data
				select * from Employee where EmployeeId = @EmployeeId
			end
		else
			begin 
				print 'Employee not found.'
			end
	end try
    begin catch
        declare @ErrorMessage NVARCHAR(4000);
        declare @ErrorSeverity INT;
        declare @ErrorState INT;

        select @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();
        print 'Error: ' + @ErrorMessage;
    end catch
end
Employee_GetById_SP 2

--Login
create or alter procedure Employee_Login_SP
(
	@EmployeeId int,
	@Name varchar(50)
)
as
begin
	begin try
		-- Check if the employee exists
		if exists (select 1 from Employee where EmployeeId=@EmployeeId and Name=@Name)
			begin
				select * from Employee where EmployeeId=@EmployeeId and Name=@Name
			end
		else
			begin
				print 'Invalid credentials. Login failed.'
			end
	end try
	begin catch
        declare @ErrorMessage NVARCHAR(4000);
        declare @ErrorSeverity INT;
        declare @ErrorState INT;

        select @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();
        print 'Error: ' + @ErrorMessage;
    end catch
end
Employee_Login_SP 1,'Kavitha'

--GetByName
create or alter procedure Employee_GetByName_SP
@Name varchar(50)
as
begin
	begin try
		-- Check if the employee exists
		if exists (select 1 from Employee where Name=@Name)
			begin
				-- Retrieve the employee's data
				select * from Employee where Name=@Name
			end
		else
			begin 
				print 'Employee not found.'
			end
	end try
    begin catch
        declare @ErrorMessage NVARCHAR(4000);
        declare @ErrorSeverity INT;
        declare @ErrorState INT;

        select @ErrorMessage = ERROR_MESSAGE(), 
               @ErrorSeverity = ERROR_SEVERITY(),
               @ErrorState = ERROR_STATE();
        print 'Error: ' + @ErrorMessage;
    end catch
end

Employee_GetByName_SP 'Kavitha'
