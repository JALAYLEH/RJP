# Subscription Management System

This is a **Subscription Management System** built using **ASP.NET Core 7**, implementing a **3-tier architecture** (API, BLL, DAL). The system manages **Users**, **Plans**, and **Subscriptions**, providing CRUD operations and subscription management features.

---

## **Project Structure**

The project follows a 3-tier architecture:

1. **Models Layer (`.Models`)**
    - Contains **DTOs**, **Entities**, **ViewModels**, **Enums**, and **AutoMapper Profiles**.
    - Responsible for defining the data structures, mapping between Entities and DTOs, and providing enums for constants.

2. **Data Access Layer (`.DAL`)**
    - Implements repositories and **Unit of Work** pattern for data access.
    - Uses **Specification Pattern** for advanced queries.
    - Includes:
        - `IRepository<T>`: Generic repository interface for CRUD operations.
        - `IUnitOfWork`: Central access point for all repositories.
        - `Specifications`: For dynamic query inclusion (e.g., loading related data).
        - Entity Framework Migrations folder for database management.

3. **Business Logic Layer (`.BLL`)**
    - Contains **Services** and **Interfaces** (`IServices`) implementing business rules.
    - Uses **AutoMapper** to map between DTOs and Entities.
    - Encapsulates all business logic before passing data to the API layer.

4. **API Layer**
    - ASP.NET Core Web API handling HTTP requests.
    - Injects **BLL Services** via Dependency Injection.
    - Provides CRUD endpoints for **Users**, **Plans**, and **Subscriptions**.

---

## **Key Concepts**

### **1. 3-Tier Architecture**
- **DAL (Data Access Layer):** Handles database operations and data queries.
- **BLL (Business Logic Layer):** Implements business rules and processing logic.
- **API/UI Layer:** Serves HTTP requests and returns responses to the client.

### **2. Repository & Unit of Work**
- **IRepository<T>**: Generic CRUD interface.
- **UnitOfWork**: Combines all repositories into a single component to simplify Dependency Injection.

### **3. Specification Pattern**
- Allows building flexible queries dynamically.
- Example:
```csharp
public class SubscriptionWithUserAndPlanSpecification : BaseSpecification<Subscription>
{
    public SubscriptionWithUserAndPlanSpecification()
    {
        AddInclude(s => s.User);
        AddInclude(s => s.Plan);
    }

    public SubscriptionWithUserAndPlanSpecification(Guid id)
        : base(s => s.Id == id)
    {
        AddInclude(s => s.User);
        AddInclude(s => s.Plan);
    }
}
