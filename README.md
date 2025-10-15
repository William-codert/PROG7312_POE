# 🏛️ SiguduVille Municipality Portal

**Connecting Citizens to Services**

A web-based municipal management portal that enhances **community engagement**, **service reporting**, and **information accessibility**.  
Built with **ASP.NET Core MVC**, this project enables citizens to:
- Report issues (e.g., potholes, water leaks, or outages)
- Stay informed on **local events** and **public announcements**
- Receive personalized event recommendations based on search activity

---

## 📋 Features

### 🧾 Core Functionalities
- **Issue Reporting System**
  - Citizens can report issues with category, location, and description.
  - Option to attach supporting images or documents.
  - Automatic timestamp and unique issue ID.
  - Displays success message upon submission.

- **Local Events Directory**
  - Browse and search community events by **keyword**, **category**, or **date**.
  - Sort events by **name**, **category**, or **date**.
  - Personalized recommendations based on recent search trends.

- **Announcements Board**
  - Displays official municipal updates such as load-shedding schedules, public notices, and service alerts.
  - Search and filter announcements by keyword or posting date.

- **Home Dashboard**
  - Highlights featured events and important announcements.
  - Shows basic statistics such as number of events and reports.

---

## 🏗️ Project Architecture

**Framework:** ASP.NET Core MVC (.NET 8 compatible)  
**Design Pattern:** Model–View–Controller (MVC)  
**Data Handling:** In-Memory Repository (no external database)  

### 📂 Folder Structure

PROG7312_POE/
│
-├── Controllers/
  - │ ├── HomeController.cs # Home landing page logic
  - │ ├── LocalEventsController.cs # Event & announcement logic
  - │ ├── ReportIssuesController.cs # Report issue submission handling
- │
- ├── Models/
  - │ ├── EventItem.cs # Event entity
  - │ ├── AnnouncementItem.cs # Announcement entity
  - │ ├── ReportIssue.cs # Citizen issue report entity
  - │ ├── InMemoryRepository.cs # Data store & logic for searching, seeding, tracking
- │
- ├── Views/
  - │ ├── Home/Index.cshtml # Landing page UI
  - │ ├── LocalEvents/Index.cshtml # Events and announcements UI
  - │ ├── ReportIssues/Create.cshtml # Report form UI
  - │ └── Shared/_Layout.cshtml # Master page layout & navbar
- │
- ├── wwwroot/
  - │ ├── css/site.css # Global custom styles
  - │ ├── images/hero.png # Header image/logo
  - │ └── uploads/ # Uploaded user attachments
- │
- └── Program.cs / Startup.cs # ASP.NET Core entrypoint and service registration


---

## ⚙️ Installation & Setup

### 🔧 Requirements
- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022 / VS Code
- (Optional) Git for version control

### ▶️ Running Locally

1. **Clone the repository**
   ```bash
   git clone https://github.com/<your-username>/PROG7312_POE.git
   cd PROG7312_POE
- Build the project
  - dotnet build
  - Run the web app

  - dotnet run
  - Open in browser

- arduino
  - http://localhost:5000
- or if using HTTPS:
  - https://localhost:7000

---

## 🌟 Sample Data
- The app includes seeded sample data in InMemoryRepository.cs:

- Events: Community, Education, Health, Market, and Entertainment categories.

- Announcements: Load shedding, water disruptions, recycling schedule updates, etc.

- This allows the site to run without a database and demonstrate all features out of the box.

## 💡 Future Enhancements
- 🔄 Add database persistence (SQL Server or SQLite)

- 📱 Create mobile-friendly PWA support

- 📬 Allow issue status tracking for citizens

- 🧭 Implement geolocation for report submissions

- 👥 Add role-based authentication for municipal employees

## 🧑‍💻 Contributors
- Developer: William Sigudu

- Institution: ADvTECH / VC Nelson Mandela Bay

- Course: PROG7312 — Advanced Software Development Practice

- Year: 2025
