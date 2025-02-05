# AirbnbDemo-ASP.NET

AirbnbDemo-ASP.NET is an ASP.NET application that demonstrates the core functionalities of an Airbnb-style platform. This project includes a sample solution with layered architecture, including data access, infrastructure, and utility components.

## Project Structure

- **AirbnbDemo**: The main ASP.NET project containing the presentation layer.
- **DataAccess**: Contains data access logic and Entity Framework models.
- **Infrastructure**: Provides the foundational services and dependency injection setup.
- **Utility**: Holds common utility classes and helper functions.
- **node_modules**, **package.json**, **package-lock.json**: Files related to front-end dependencies and Bootstrap integration.
- **AirbnbDemo.sln**: The solution file to open the entire project in Visual Studio.

## Prerequisites

- **.NET SDK**: Make sure you have the appropriate version of the .NET SDK installed. You can download it from [Microsoft .NET Downloads](https://dotnet.microsoft.com/download).
- **Visual Studio 2019/2022** (or any preferred IDE that supports ASP.NET development).

## Getting Started

1. **Clone the repository:**
   ```bash
   git clone https://github.com/bilgehandk/AirbnbDemo-ASP.NET.git

2. **Open the solution:**
   Open AirbnbDemo.sln in Visual Studio or your preferred IDE.

3. **Restore NuGet packages:**
   Visual Studio should automatically restore the required NuGet packages. Alternatively, you can run.
   ```bash
   dotnet restore

4. **Run the application:**
   Set AirbnbDemo as the startup project.
   Press F5 or run the project from Visual Studio to start debugging.
   
