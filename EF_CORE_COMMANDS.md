# SQL Server Migration - Command Reference

## Migration Commands Used

### 1. Add NuGet Packages to Repository Project
```powershell
cd Repository
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### 2. Add NuGet Packages to CompanyManagementApp Project
```powershell
cd CompanyManagementApp
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.DependencyInjection
```

### 3. Create Initial Migration
Run this from the project folder (where appsettings.json is located):
```powershell
cd CompanyManagementApp
dotnet ef migrations add InitialCreate --project ..\Repository\Repository.csproj
```

**Command Breakdown:**
- `dotnet ef migrations add InitialCreate` - Creates migration named "InitialCreate"
- `--project ..\Repository\Repository.csproj` - Specifies where AppDbContext is located

### 4. Update Database
```powershell
cd CompanyManagementApp
dotnet ef database update --project ..\Repository\Repository.csproj
```

**Command Breakdown:**
- `dotnet ef database update` - Applies pending migrations to the database
- `--project ..\Repository\Repository.csproj` - Specifies the project containing migrations

## Future Migration Workflow

### When You Add New Features

1. **Make model changes** to Domain entities

2. **Create a new migration:**
```powershell
cd CompanyManagementApp
dotnet ef migrations add FeatureName --project ..\Repository\Repository.csproj
```

3. **Review the generated migration** file in `Repository\Migrations\`

4. **Apply the migration:**
```powershell
dotnet ef database update --project ..\Repository\Repository.csproj
```

### To Undo a Migration

```powershell
cd CompanyManagementApp
dotnet ef migrations remove --project ..\Repository\Repository.csproj
```

## Useful EF Core Commands

### View All Migrations
```powershell
cd CompanyManagementApp
dotnet ef migrations list --project ..\Repository\Repository.csproj
```

### Script a Migration (for deployment)
```powershell
cd CompanyManagementApp
dotnet ef migrations script --project ..\Repository\Repository.csproj -o migration_script.sql
```

### Drop Database (Dangerous!)
```powershell
cd CompanyManagementApp
dotnet ef database drop --project ..\Repository\Repository.csproj
```

### Check Database Connection
```powershell
cd CompanyManagementApp
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your_connection_string"
```

## Connection String Reference

### LocalDB (Current - Easiest for Development)
```
Server=(localdb)\mssqllocaldb;Database=CompanyManagementDb;Trusted_Connection=True;TrustServerCertificate=True;
```

### SQL Server Express
```
Server=.\SQLEXPRESS;Database=CompanyManagementDb;Trusted_Connection=True;TrustServerCertificate=True;
```

### SQL Server (Named Instance)
```
Server=MACHINE_NAME\INSTANCE_NAME;Database=CompanyManagementDb;Trusted_Connection=True;TrustServerCertificate=True;
```

### SQL Server (With SQL Authentication)
```
Server=YOUR_SERVER;Database=CompanyManagementDb;User Id=sa;Password=YourPassword;Encrypt=True;TrustServerCertificate=True;
```

### Azure SQL Database
```
Server=tcp:your-server.database.windows.net,1433;Initial Catalog=CompanyManagementDb;Persist Security Info=False;User ID=your_username;Password=your_password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

## Files Modified/Created

### Modified Files:
1. `Domain\Entities\BaseEntity.cs` - Changed İd → Id
2. `Domain\Models\Employee.cs` - Added DepartmentId FK property
3. `Repository\Data\AppDbContext.cs` - Converted to real DbContext
4. `Repository\Repositories\BaseRepository.cs` - Switched to EF Core pattern
5. `Repository\Repositories\DepartamentRepository.cs` - Updated for DI
6. `Repository\Repositories\EmployeeRepository.cs` - Updated for DI
7. `Services\Services\BaseService.cs` - Fixed İd → Id
8. `Services\Services\DepartamentService.cs` - Added DI constructor
9. `Services\Services\EmployeeService.cs` - Added DI constructor
10. `CompanyManagementApp\Controllers\DepartamentController.cs` - Added DI
11. `CompanyManagementApp\Controllers\Employeecontroller.cs` - Added DI
12. `CompanyManagementApp\Program.cs` - Complete refactor with DI setup
13. `CompanyManagementApp\CompanyManagementApp.csproj` - Added package references

### New Files:
1. `Repository\Data\AppDbContextFactory.cs` - Design-time DbContext factory
2. `Repository\Migrations\20260822121915_InitialCreate.cs` - Initial migration
3. `Repository\Migrations\20260822121915_InitialCreate.Designer.cs` - Migration snapshot
4. `Repository\Migrations\AppDbContextModelSnapshot.cs` - Current model snapshot
5. `CompanyManagementApp\appsettings.json` - Configuration file

## Troubleshooting

### Issue: "A network-related or instance-specific error occurred"
**Solution:** Ensure SQL Server or LocalDB is running. Change connection string in appsettings.json

### Issue: "Unable to create a 'DbContext' of type 'AppDbContext'"
**Solution:** Ensure AppDbContextFactory exists in Repository project for design-time

### Issue: Migration not found when running "dotnet ef database update"
**Solution:** Make sure you're in CompanyManagementApp directory and using the correct --project path

### Issue: Migrations folder doesn't exist
**Solution:** This is created automatically with the first migration. If missing, ensure migration ran successfully.

## Testing the Migration

### Option 1: Run the Application
```powershell
cd CompanyManagementApp
dotnet run
```
Then create and manage departments/employees through the console menu.

### Option 2: Query Database Directly
Use SQL Server Management Studio or Azure Data Studio:
```sql
-- View Departments
SELECT * FROM Departments;

-- View Employees
SELECT e.Id, e.Name, e.Surname, e.Age, d.Name as Department 
FROM Employees e 
LEFT JOIN Departments d ON e.DepartmentId = d.Id;
```

## Best Practices Going Forward

1. **Always create migrations** for schema changes - never modify database directly
2. **Test migrations** in a development environment first
3. **Keep AppDbContextFactory updated** if connection string changes
4. **Use meaningful migration names** - helps identify what changed
5. **Review generated migrations** before applying to production
6. **Keep appsettings.json** synchronized across environments
7. **Never commit sensitive data** - use user-secrets for passwords
