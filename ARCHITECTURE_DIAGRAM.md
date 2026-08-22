# Architecture Transformation

## BEFORE: In-Memory Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                    Console Application                       │
│  (Program.cs - Manual service instantiation)                │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ▼
┌─────────────────────────────────────────────────────────────┐
│              Controllers (Manual new())                     │
│  DepartmentController  │  EmployeeController               │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ▼
┌─────────────────────────────────────────────────────────────┐
│               Services (Manual new())                       │
│  DepartmentService    │  EmployeeService                   │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ▼
┌─────────────────────────────────────────────────────────────┐
│            Repositories (Manual new())                      │
│  DepartmentRepository │ EmployeeRepository                 │
└────────────────────────┬────────────────────────────────────┘
						 │
						 ▼
			  ┌──────────────────────┐
			  │  Static List Objects │
			  │ (In-Memory Only)     │
			  │ ❌ Lost on exit      │
			  │ ❌ No persistence    │
			  └──────────────────────┘
```

**Problems:**
- ❌ No persistence (data lost on exit)
- ❌ Manual service instantiation
- ❌ Tight coupling between layers
- ❌ Hard to test (no interface injection)
- ❌ Character encoding issues (İd)

---

## AFTER: SQL Server + DI Architecture

```
┌────────────────────────────────────────────────────────────────┐
│                  Console Application                           │
│               Program.cs with DI Setup                         │
│  ┌──────────────────────────────────────────────────────────┐ │
│  │ • ConfigurationBuilder reads appsettings.json           │ │
│  │ • ServiceCollection created                             │ │
│  │ • DbContext registered with connection string           │ │
│  │ • Repositories registered as scoped                     │ │
│  │ • Services registered as scoped                         │ │
│  │ • Controllers instantiated from DI container            │ │
│  └──────────────────────────────────────────────────────────┘ │
└────────────────────────┬─────────────────────────────────────┘
						 │
						 ▼ (DI Injection)
┌────────────────────────────────────────────────────────────────┐
│         Controllers (DI-Injected Services)                     │
│  DepartmentController(IService)  │  EmployeeController(IService)
└────────────────────────┬─────────────────────────────────────┘
						 │ (DI Injection)
						 ▼
┌────────────────────────────────────────────────────────────────┐
│         Services (DI-Injected Repositories)                    │
│  DepartmentService(IRepository)  │  EmployeeService(IRepository)
└────────────────────────┬─────────────────────────────────────┘
						 │ (DI Injection)
						 ▼
┌────────────────────────────────────────────────────────────────┐
│        Repositories (DI-Injected DbContext)                    │
│  DepartmentRepository(DbContext)  │  EmployeeRepository(DbContext)
└────────────────────────┬─────────────────────────────────────┘
						 │
						 ▼ (LINQ/EF Core)
		┌───────────────────────────────┐
		│      AppDbContext             │
		│  (Entity Framework Core)      │
		│  ✅ Real DbContext            │
		│  ✅ DbSet<Departament>        │
		│  ✅ DbSet<Employee>           │
		└────────────────┬──────────────┘
						 │
						 ▼ (SQL Queries)
╔═══════════════════════════════════════╗
║    SQL Server Database (LocalDB)      ║
║  CompanyManagementDb                  ║
║  ┌─────────────────────────────────┐ ║
║  │ ✅ Departments Table            │ ║
║  │    Id, Name, Capacity           │ ║
║  └─────────────────────────────────┘ ║
║  ┌─────────────────────────────────┐ ║
║  │ ✅ Employees Table              │ ║
║  │    Id, Name, Surname, Age,      │ ║
║  │    Address, DepartmentId (FK)   │ ║
║  └─────────────────────────────────┘ ║
║  ✅ Persistent Storage                ║
║  ✅ Relational Integrity              ║
║  ✅ Indexing & Performance            ║
║  ✅ Backup & Recovery                 ║
╚═══════════════════════════════════════╝
```

**Benefits:**
- ✅ Persistent SQL Server storage
- ✅ Dependency Injection throughout
- ✅ Loose coupling between layers
- ✅ Testable with mock interfaces
- ✅ Professional architecture
- ✅ Proper character encoding (Id)
- ✅ Entity Framework migrations
- ✅ Scalable and maintainable

---

## Layer Communication Flow

### Request Flow (Top-Down)
```
User Input (Console Menu)
		 ↓
   Controller Action
	(Receives Service)
		 ↓
   Service Method
  (Receives Repository)
		 ↓
  Repository Method
   (Uses DbContext)
		 ↓
   DbContext Query
  (Uses SQL Statement)
		 ↓
   SQL Server Database
   (Returns Results)
```

### Response Flow (Bottom-Up)
```
   SQL Server Database
  (Returns Rows/Objects)
		 ↓
   DbContext Maps
	(To Entities)
		 ↓
  Repository Returns
	(T or List<T>)
		 ↓
	Service Returns
  (Transformed Result)
		 ↓
   Controller Returns
   (Formatted Output)
		 ↓
 Display to Console User
```

---

## Dependency Injection Container

```
ServiceCollection Registration:
┌──────────────────────────────────────┐
│ Scoped:                              │
│ • AppDbContext                       │
│ • IDepartmentRepository              │
│ • IEmployeeRepository                │
│ • IDepartmentService                 │
│ • IEmployeeService                   │
└──────────────────────────────────────┘
		 ↓
  ServiceProvider
  (Resolves Dependencies)
		 ↓
  Controllers Receive
  (Service Instances)
		 ↓
  Services Receive
  (Repository Instances)
		 ↓
  Repositories Receive
  (DbContext Instance)
```

---

## Migration Files Structure

```
Repository/
├── Data/
│   ├── AppDbContext.cs              ← Real DbContext
│   └── AppDbContextFactory.cs       ← Design-time factory
├── Migrations/
│   ├── 20260822121915_InitialCreate.cs           ← Migration definition
│   ├── 20260822121915_InitialCreate.Designer.cs  ← Generated code
│   └── AppDbContextModelSnapshot.cs              ← Current schema
└── Repositories/
	├── BaseRepository.cs            ← EF Core implementation
	├── DepartamentRepository.cs     ← EF Core implementation
	└── EmployeeRepository.cs        ← EF Core implementation
```

---

## Entity Relationships (Database Schema)

```
Departments
┌──────────────────┐
│ Id (PK)          │
│ Name             │
│ Capacity         │
└──────────────────┘
	  ▲
	  │ (1:N)
	  │ FK: DepartmentId
	  │
Employees
┌──────────────────┐
│ Id (PK)          │
│ Name             │
│ Surname          │
│ Age              │
│ Address          │
│ DepartmentId (FK)│
└──────────────────┘
```

---

## Dependency Resolution Example

When you call:
```csharp
var departmentController = new DepartmentController(
	serviceProvider.GetRequiredService<IDepartmentService>()
);
```

The DI container:
1. Looks up IDepartmentService binding
2. Finds it's mapped to DepartmentService
3. DepartmentService needs IDepartmentRepository
4. Finds it's mapped to DepartmentRepository
5. DepartmentRepository needs AppDbContext
6. Creates AppDbContext with connection string from options
7. Returns fully constructed dependency chain ✅

---

## Summary

This transformation represents a shift from a **prototype/proof-of-concept** architecture to a **production-ready enterprise architecture** while maintaining full backward compatibility with the existing console menu interface.

All users see the same menu and experience, but now:
- Data persists permanently ✅
- Code is properly decoupled ✅
- Application is easily testable ✅
- Scaling and maintenance are simplified ✅
