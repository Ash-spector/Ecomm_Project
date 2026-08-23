# Ecomm_Project — Full-Stack Online Bookstore

An end-to-end e-commerce web application for browsing, purchasing, and managing books, built as a multi-project ASP.NET Core MVC solution using the Repository and Unit of Work patterns.

## Features

- Browse book catalog with categories and search
- Shopping cart with session management
- Secure checkout via **Stripe** and **PayPal**
- Order confirmation via **Twilio SMS**
- [ ] Add: user authentication / account features
- [ ] Add: admin panel features (inventory, orders, etc.) if present

## Tech Stack

**Backend**
- ASP.NET Core MVC
- Entity Framework Core
- Repository Pattern + Unit of Work
- SQL Server

**Integrations**
- Stripe (payments)
- PayPal (payments)
- Twilio (SMS notifications)

## Project Structure

```
Ecomm_Project/
├── Ecomm_Project/              # Main MVC web app
├── Ecomm_Project.DataAccess/   # Data access layer (EF Core, repositories)
├── Ecomm_Project.Models/       # Domain models
├── Ecomm_Project.Utility/      # Shared utilities/helpers
└── Ecomm_Project.slnx          # Solution file
```

## Getting Started

### Prerequisites
- .NET 8 SDK (or your target version — confirm)
- SQL Server (LocalDB or full instance)
- Stripe account + API keys
- PayPal developer account + API keys
- Twilio account + API keys

### Setup
```bash
git clone https://github.com/Ash-spector/Ecomm_Project.git
cd Ecomm_Project
```

1. Update `appsettings.json` with your SQL Server connection string, Stripe/PayPal keys, and Twilio credentials.
2. Run EF Core migrations:
   ```bash
   dotnet ef database update
   ```
3. Run the app:
   ```bash
   dotnet run --project Ecomm_Project
   ```

## Notes

This was an earlier learning project focused on core e-commerce patterns (Repository/Unit of Work, payment integration, session-based cart). [ ] Add anything else worth noting — what you learned, what you'd do differently now, etc.

## License

[ ] MIT / none specified
