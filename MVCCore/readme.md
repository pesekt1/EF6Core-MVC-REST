MVC Core project:
- SQL Server database
- MVC architecture ... controllers returning the view - http web paage
- Models mapping tables from the database
- Rest Controllers - returning data as json object

For generating model classes from the existing database we can use scaffolding:
Run something like this in your Package Manager Console in Visual Studio:
Scaffold-DbContext "Data Source=localhost\SQLEXPRESS;Initial Catalog=SchoolContext6;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models2