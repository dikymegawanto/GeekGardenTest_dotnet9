GeekGardenTest - ASP.NET Core Web API (template)
Target: .NET 7

Structure:
- Controllers: EmailController, UsersController, KPIController
- Models: EmailRequest, EmailHistory, User, Company, KPI
- Services: EmailService
- Data: AppDbContext

How to run:
1. Install .NET 7 SDK
2. Open folder GeekGarden.API in VS Code or Visual Studio
3. Update connection string in appsettings.json
4. dotnet restore
5. dotnet ef database update (if using migrations) or ensure DB exists
6. dotnet run
