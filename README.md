<<<<<<< HEAD
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
=======
# Municipality Report Issues Portal (MVC_POE)

##Overview
The **Municipality Report Issues Portal** is a web application designed to allow citizens to report local municipal issues such as sanitation problems, road repairs, utility disruptions, and loadshedding.  

The system provides an intuitive interface for users to:
- Submit reports
- Upload media attachments
- Review all submissions

Built using **ASP.NET Core MVC**, this application stores reports temporarily using an in-memory **HashSet** service and features a dynamic progress bar, confirmation workflow, and search/filter functionality.  

It also includes an **Event Dashboard** and **Announcements Section** for community engagement.

---

##Features

| Feature | Description |
|----------|--------------|
| **Submit Reports** | Citizens can report issues by entering Location, Category, Description, and optional media attachments. |
| **File Upload & Preview** | Uploaded images are previewed on the confirmation page. |
| **Confirmation Workflow** | Users can review their submission before final confirmation. |
| **Dynamic Progress Bar** | Shows the completion progress of the form. |
| **View All Reports** | Displays all submitted reports as mini-cards grouped by location. |
| **Search & Filter** | Filter reports by location and category. |
| **Download Attachments** | View or download uploaded images/files. |
| **Event Dashboard** | Displays local events, allows favorites, and shows recommendations. |
| **Favorites & Recommendations** | Uses a **Priority Queue** to rank and recommend events based on user interests. |
| **Announcements Section** | Displays important notices, news, and community updates. |

---

##Technologies Used
- **C# / ASP.NET Core MVC**
- **Razor Pages** for Views
- **Bootstrap 5** for layout and styling
- **JavaScript** for interactivity
- **HTML & CSS** for UI
- **In-memory HashSet Service** for temporary data storage
- **PriorityQueue** for managing and recommending events
- **IFormFile** for media uploads

---

##Models

### **ReportIssuesForm**
| Property | Type | Description |
|-----------|------|--------------|
| FormId | Guid | Unique ID for each report |
| Location | string | Location of the issue |
| Category | string | Issue category (e.g., Sanitation, Roads) |
| Description | string | Detailed description of the problem |
| MediaAttachment | string? | Optional file path of the uploaded image |

### **Event**
| Property | Type | Description |
|-----------|------|--------------|
| EventId | Guid | Unique ID for each event |
| EventName | string | Event title |
| EventCategory | string | Event category (e.g., Music, Walk) |
| EventDescription | string | Description of the event |
| EventDateTime | DateTime | Scheduled date/time |
| EventImage | string? | Optional image path |
| IsFavorite | bool | Indicates if user favorited it |

### **Announcement**
| Property | Type | Description |
|-----------|------|--------------|
| AnnouncementId | Guid | Unique ID for announcement |
| AnnouncementName | string | Title of the announcement |
| AnnouncementCategory | string | Category (e.g., News, Event) |
| AnnouncementDescription | string | Description of announcement |
| AnnouncementDate | DateTime | Date of posting |

---

##Controllers

### **ReportIssuesController**
Handles issue report creation and display.

| Action | Method | Description |
|--------|---------|-------------|
| `CreateReportIssues` | GET | Displays the report form |
| `ReportIssuesConfirmation` | POST | Shows confirmation before saving |
| `ConfirmReportIssues` | POST | Final submission; saves to in-memory store |
| `ViewReportIssues` | GET | Displays all reports grouped by location |

### **EventController**
Manages event dashboard and favorites.

| Action | Method | Description |
|--------|---------|-------------|
| `AllEvents` | GET | Lists all events by category |
| `EventDetails` | GET | Shows event info and favorite option |
| `FavoriteEvent` | POST | Adds an event to favorites |
| `UnfavoriteEvent` | POST | Removes from favorites |
| `RecommendedEvents` | GET | Shows events based on favorites |

### **AnnouncementController**
Displays and filters announcements.

| Action | Method | Description |
|--------|---------|-------------|
| `AllAnnouncements` | GET | Shows latest and older announcements |
| `AnnouncementDetails` | GET | Displays a single announcement |
| `FilterAnnouncements` | POST | Filters announcements by category/date |

---

##Implemented Data Structures

### **HashSet – For Storing Reports**
- Used to store all submitted issue reports in-memory.
- Prevents duplicates by automatically ensuring unique entries.
- Enables **O(1)** average lookup and insertion time, ensuring fast performance.
  
**Example:**
private static HashSet<ReportIssuesForm> _reports = new();
Each time a report is submitted, it is added to this HashSet.
This allows the “Service Request Status” feature to quickly retrieve reports without database queries, improving response time.

## PriorityQueue – For Event Recommendations
Manages event scheduling and favorites.

Events are sorted by date or user interest (priority).

Ensures efficient retrieval of the most relevant events (O(log n)).

**Example:**
PriorityQueue<Event, DateTime> eventQueue = new();
When users favorite events, the system prioritizes similar categories in future recommendations.

## Dictionary – For Category Grouping
Used to group service requests or events by location or category.

Allows constant-time lookup of grouped items.

Example:
var groupedReports = reports.GroupBy(r => r.Location)
                            .ToDictionary(g => g.Key, g => g.ToList());
This enables the “Community Reports” page to display grouped data efficiently, reducing redundant iterations.

#Running the Application 
Step 1: Clone the Project
Open Visual Studio 2022 or later.
Go to File → Clone Repository.
Enter the repository URL:
https://github.com/VCNMB-3rd-years/PROG7312_POE.git
Click Clone.

🖼️ Screenshots
Section	Description	Screenshot
Report Submission Page	Users fill in issue details and upload media	

Confirmation Page	Displays submitted details before saving	

Community Reports	Shows all reports grouped by location	

Event Dashboard	Displays local events and favorites	

Announcements Page	Lists latest and older announcements	

#Summary

The Municipality Report Issues Portal delivers an accessible and efficient digital platform for citizens to report and track local municipal issues. It empowers communities to communicate directly with their municipalities through an intuitive interface designed for speed, clarity, and transparency.
By utilizing in-memory data structures such as HashSet, Dictionary, and PriorityQueue, the system achieves high performance, real-time responsiveness, and organized data management—all without the need for a traditional database. This design ensures that reports, events, and announcements are efficiently processed and displayed to users in real time.

#Conclusion
The Service Request Status feature exemplifies the portal’s focus on performance and user experience through intelligent use of data structures:
HashSet guarantees that every service request is unique, preventing duplicates and ensuring data integrity.
Dictionary enables instant grouping and retrieval of reports by status, location, or category—reducing load times and improving clarity.
List ensures that service requests and events are presented in a clean, ordered, and user-friendly format.
PriorityQueue enhances the recommendation engine by efficiently prioritizing events based on user interest and time relevance.
Together, these components form a robust, scalable, and responsive system that not only streamlines the reporting process but also strengthens community engagement. The result is a modern municipal portal that prioritizes efficiency, transparency, and user satisfaction, serving as a model for smart civic service management.
>>>>>>> 8e495d99c3b2246c4762ac0e89f82dabf9581635
