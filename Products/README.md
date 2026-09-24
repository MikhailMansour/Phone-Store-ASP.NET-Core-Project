# 📱 Phone Store E-Commerce Web Application

#### 🎬 Video Demo: [Watch video on YouTube Demo]

    https://youtu.be/PYDXpGcN7sg

---

## 📋 Description

The **Phone Store** is a comprehensive, multi-role, feature-rich web application built using **ASP.NET Core** following the **Code-First** database approach. The core motivation behind this project is to create a secure, scalable, and fully functional e-commerce ecosystem specifically tailored for mobile phones and digital accessories. 

Modern online shopping demands clear separation of duties between platform administration, product listing and management, and seamless customer purchasing experiences. To achieve this, the application implements a robust role-based authorization mechanism dividing users into three distinct tiers: **Administrators**, **Sellers**, and **Buyers**. Every entity, from user authentication to product inventory management, is backed by a structured SQL database managed via Entity Framework Core migrations.

---

## 🛠️ Architecture & Technologies Used

* **Framework:** ASP.NET Core MVC (.NET)
* **Database Approach:** Entity Framework Core (Code-First) with custom LINQ repositories and data seeders.
* **Frontend:** HTML5, CSS3, Bootstrap with custom responsive layouts, and interactive DataTables.
* **Architecture Pattern:** Model-View-Controller (MVC) ensuring strict separation of concerns, maintainability, and clean code principles.

---

## 📂 Project File Structure & Components

The solution is systematically organized to ensure high cohesion and loose coupling across components:

* **`Controllers/`**: Contains the core logic routing and request handling classes:
  * `AdminController.cs`: Manages administrative operations, user oversight, and sales reporting.
  * `SallerController.cs`: Handles seller-specific actions, product additions, and updates.
  * `BuyerController.cs`: Manages customer interactions, cart management, checkout, and complaints.
  * `AuthenticationController.cs`: Handles secure user registration, logins, and role assignments.
  * `ProductController.cs` & `ProductDetailsController.cs`: Manages product catalogs and detailed specifications.
  * `ContactController.cs` & `ContactUsController.cs`: Manages incoming client messages and support requests.
  * `OffersController.cs` & `PurchaseController.cs`: Handles promotional offers and transaction processing.
  * `ProfileController.cs`: Manages user profile settings and personal information.

* **`Models/`**: Defines the database entities and schema via Code-First:
  * `ApplicationUser.cs` & `ApplicationRole.cs`: Custom identity models handling user authentication and roles.
  * `Buyer.cs` & `Product.cs`: Core entities representing customers and phone inventory.
  * `ProductImages.cs` & `ProductBuyer.cs`: Relational models handling product image galleries and purchase histories.
  * `Review.cs` & `ContactUs.cs`: Entities storing customer feedback, ratings, and client support messages.
  * `Address.cs` & `BankCard.cs`: Stores shipping addresses and secure payment methods.

* **`Infrastructure/`**: Houses core infrastructural configurations:
  * `ApplicationContext.cs`: The Entity Framework database context managing entity relationships.
  * `Repositories/`: Custom repository implementations (`BuyerRepository.cs`, `ProductRepository.cs`) for clean data access.
  * `Configurations/`: Fluent API database table mapping rules.

* **`Services/` & `Helpers/`**: Contains business logic helpers, file upload services (`FileServices.cs`), object mapping configuration (`MapsterConfig.cs`), pagination utilities (`Pagination.cs`), and automated data seeders (`DataSeeder.cs`).

* **`Views/`**: Contains user interface Razor views categorized into dedicated layouts for Admin, Seller, Buyer, and shared components.

---

## 👥 Role-Based Dashboards & Features

### 🛡️ 1. Admin Dashboard
The Administrator holds supreme oversight over the entire platform ecosystem.
* **Dashboard & Profile:** Centralized view summarizing platform metrics, system statuses, and personal admin profile settings.
* **User Management & Registration:** Full capability to register, audit, and manage user accounts, including adding new administrators or granting privileges to sellers.
* **Product Oversight:** Comprehensive listing and management of all store items uploaded across different sellers.
* **Buyers & Sellers Control:** Direct tracking and monitoring of active buyers and registered sellers.
* **Sales Reports:** Detailed analytical reports tracking total revenue, sales velocity, and financial performance.
* **Client Messages:** Dedicated inbox to review, manage, and respond to incoming support inquiries and feedback submitted through client contact forms.

### 🏷️ 2. Seller Dashboard
Sellers are empowered to manage their inventory and showcase products effectively.
* **Home & Profile:** Personalized seller landing page and editable profile settings.
* **Product Management:** Full CRUD operations to add new phone models, edit technical specifications, update pricing, and manage inventory stock levels.
* **Statistics:** Analytical insights tracking views, order counts, and individual product performance metrics.
* **Products Catalog View:** Dedicated interface displaying items managed exclusively by the logged-in seller.

### 🛒 3. Buyer Dashboard
Designed to deliver a smooth, interactive, and customer-friendly shopping experience.
* **Home & Profile:** Interactive storefront home page displaying featured phones, special offers, and personal account configurations.
* **Product Exploration & Details:** Deep dive into specific phone parameters, high-resolution image galleries, pricing details, and stock availability.
* **Purchase & Cart System:** Secure checkout workflow allowing buyers to purchase products, manage addresses, select payment methods, and review complete purchase histories.
* **Customer Reviews & Feedback:** Interactive review system enabling buyers to rate products, express opinions, and submit feedback or formal complaints to improve service quality.

---

## 💡 Design Choices & Implementation Decisions

1. **Code-First Architecture Selection:** Choosing EF Core Code-First allowed precise control over domain entities, ensuring seamless migration handling and database schema evolution as features scaled.
2. **Modular Role-Based Layouts (`_AdminLayout.cshtml`, `_SallerLayout.cshtml`, `_BuyerLayout.cshtml`):** Instead of a monolithic UI layout, splitting dashboard interfaces into dedicated master layouts optimized user experience, security boundaries, and navigation clarity.
3. **Repository Pattern Implementation:** Separating data access logic from controllers via custom repositories improved testability, code readability, and database interaction efficiency.

---





