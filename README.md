# 🏛 Municipality Report Issues Portal (MVC_POE)

## 📖 Overview
The **Municipality Report Issues Portal** is a web application designed to allow citizens to report local municipal issues such as sanitation problems, road repairs, utility disruptions, and loadshedding.  

The system provides an intuitive interface for users to:
- Submit reports
- Upload media attachments
- Review all submissions

Built using **ASP.NET Core MVC**, this application stores reports temporarily using an in-memory **HashSet** service and features a dynamic progress bar, confirmation workflow, and search/filter functionality.  

It also includes an **Event Dashboard** and **Announcements Section** for community engagement.

---

## ✨ Features

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

## 🧩 Technologies Used
- **C# / ASP.NET Core MVC**
- **Razor Pages** for Views
- **Bootstrap 5** for layout and styling
- **JavaScript** for interactivity
- **HTML & CSS** for UI
- **In-memory HashSet Service** for temporary data storage
- **PriorityQueue** for managing and recommending events
- **IFormFile** for media uploads

---

## 🧱 Models

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

## ⚙️ Controllers

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

## 🧠 Implemented Data Structures

### **1️⃣ HashSet – For Storing Reports**
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
