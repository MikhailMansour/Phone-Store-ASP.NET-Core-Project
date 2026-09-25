# 📱 Phone Store E-Commerce Web Application ###

📢 My Communication

[🎬 Watch video on LinkedIn]


## 📋 Description ###

A secure and comprehensive e-commerce platform for online mobile phone retail built using modern ASP.NET Core technologies. The system features a robust role-based

authorization mechanism divided into three main tiers—(Administrators) (Sellers)and (Buyers)—ensuring professional product management and a seamless shopping and checkout experience.

🛠️ Architecture & Technologies Used ###

⚙️ Framework: ASP.NET Core MVC

🗄️ Database: Entity Framework Core (Code-First) & LINQ

🎨 Frontend: HTML5, CSS3, Bootstrap, and DataTables

📐 Design Pattern: Model-View-Controller (MVC) for clean separation of concerns

## ✨ Project Features Overview ### 

🛡️ Admin Dashboard

📊 Centralized system metrics & store analytics tracking.

👥 Full user auditing, management, and role permission control.

➕ Ability to register new administrators and grant seller privileges.

📋 Global oversight of all products across different sellers.

💬 Dedicated support inbox to manage incoming inquiries and feedback.

🏷️ Seller Dashboard

📱 Full CRUD operations for product inventory and specifications.

🏷️ Direct management of pricing, stock levels, and special offers.

📊 Dedicated catalog view for items managed by the active seller.

📈 Individual product performance insights and order metrics.

🛒 Buyer Storefront

🛍️ Interactive home page featuring phones and special deals.

🔍 Detailed product exploration with specs, images, and availability.

💳 Simulated checkout workflow (PayPal & Credit Card) with automated receipts.

📦 Dedicated order tracking and purchase history dashboard.

⭐ Customer review system, product ratings, and complaint/feedback submission.

🎨 System & UI

📐 Modular role-based master layouts for high security and navigation clarity.

📱 Modern, responsive design built with Bootstrap and interactive DataTables.

## 🚀 Highlights  ###  
🛡️ Role-Based Access Control

👨‍💼 Administrator Control Panel

🏷️ Seller Inventory Management

🛒 Buyer E-Commerce Storefront

📱 Mobile Phone Specifications Catalog

➕ Full Product CRUD Operations

💳 Simulated Checkout Workflow

🧾 Automated Order Receipts

📊 Sales Analytics & Reports

💬 Support Messaging Inbox

🎨 Responsive Bootstrap Interface

📋 Interactive DataTables Integration

📐 Clean MVC Architecture Pattern

## 📂 Project Structure ####

 ```text
PhonesStore/
├── Controllers/
│   ├── AdminController.cs
│   ├── SallerController.cs
│   ├── BuyerController.cs
│   ├── AuthenticationController.cs
│   ├── ProductController.cs
│   ├── ProductDetailsController.cs
│   ├── ContactController.cs
│   ├── OffersController.cs
│   ├── PurchaseController.cs
│   └── ProfileController.cs
├── Models/
│   ├── ApplicationUser.cs
│   ├── ApplicationRole.cs
│   ├── Buyer.cs
│   ├── Product.cs
│   ├── ProductImages.cs
│   ├── ProductBuyer.cs
│   ├── Review.cs
│   ├── ContactUs.cs
│   ├── Address.cs
│   └── BankCard.cs
├── Data/
│   ├── ApplicationContext.cs
│   └── DataSeeder.cs
├── Repositories/
│   ├── BuyerRepository.cs
│   └── ProductRepository.cs
├── Services/
│   └── FileServices.cs
├── Helpers/
│   ├── MapsterConfig.cs
│   └── Pagination.cs
├── Views/
│   ├── Admin/
│   ├── Saller/
│   ├── Buyer/
│   ├── Product/
│   ├── Shared/
│   │   ├── _AdminLayout.cshtml
│   │   ├── _SallerLayout.cshtml
│   │   ├── _BuyerLayout.cshtml
│   │   └── _Layout.cshtml
│   └── _ViewImports.cshtml
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
├── appsettings.json
└── Program.cs

