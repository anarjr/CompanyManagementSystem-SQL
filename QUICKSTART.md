# 🚀 Quick Start Guide

## Get Running in 30 Seconds

### 1. Build the Project
```powershell
cd C:\vs new ttest1\CompanyManagementApp
dotnet build
```

### 2. Run the Application
```powershell
cd CompanyManagementApp
dotnet run
```

### 3. Use the Menu
The application will display your familiar menu. All data now persists in SQL Server!

---

## What You Can Do Now

✅ Create Departments - Data saves to database  
✅ Create Employees - Data saves to database  
✅ Update Records - Changes persist  
✅ Delete Records - Permanently removed from database  
✅ Search & Filter - Works with database queries  
✅ Close and Reopen - Your data is still there!

---

## Database Info

- **Database Name:** CompanyManagementDb
- **Server:** LocalDB (localhost)
- **Status:** Already created and migrated ✅
- **Tables:** Departments, Employees
- **Relationship:** Employees → Departments (many-to-one)

---

## If Connection Fails

1. **Ensure LocalDB is installed:**
   - Comes with Visual Studio
   - Or download SQL Server Express

2. **Check connection string** in `CompanyManagementApp/appsettings.json`

3. **Try different connection:**
   - For SQL Server Express: `Server=.\SQLEXPRESS;...`
   - For named instance: `Server=MACHINE_NAME\INSTANCE_NAME;...`

---

## File Locations

| File | Purpose |
|------|---------|
| `CompanyManagementApp/appsettings.json` | Database connection string |
| `Repository/Data/AppDbContext.cs` | Database context (EF Core) |
| `Repository/Migrations/` | Database schema history |
| `CompanyManagementApp/Program.cs` | DI setup & application entry |

---

## Future: Adding Database Changes

When your requirements change:

```powershell
cd CompanyManagementApp

# 1. Make model changes in Domain/
# 2. Create migration
dotnet ef migrations add DescriptiveName --project ..\Repository\Repository.csproj

# 3. Apply to database  
dotnet ef database update --project ..\Repository\Repository.csproj
```

---

## Complete! ✨

Your application is now ready to use with persistent SQL Server storage and professional dependency injection architecture.

For detailed commands, see: **EF_CORE_COMMANDS.md**  
For technical details, see: **MIGRATION_SUMMARY.md**
