# 🗂️ TaskMonitor

**This project contains a sample ASP.NET Core app. This app is an example of the article I produced for the Telerik Blog (telerik.com/blogs)**

**TaskMonitor** is an ASP.NET Core job application designed to automatically clean up old historical data from a SQL Server database.
It uses **Quazar.NET** for job scheduling and **Entity Framework Core** for data access.

---

## 🚀 Features

* 🧹 **Automated Cleanup** – Periodically removes outdated historical records.
* ⏱ **Flexible Scheduling** – Managed by **Quazar.NET** for precise job execution timing.
* 🗄 **SQL Server Support** – Optimized for Microsoft SQL Server.
* 🛠 **EF Core Integration** – Easy-to-maintain and type-safe database access.

---

## 📦 Setup

1. **Configure the database connection** in `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
   }
   ```

2. **Apply migrations**

   ```bash
   dotnet ef database update
   ```

3. **Run the job**

   ```bash
   dotnet run
   ```

---

## 📜 License

This project is licensed under the MIT License.
