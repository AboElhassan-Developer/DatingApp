# 💘 Dating Application

> A full-stack dating web application built with **ASP.NET Core** & **Angular** — featuring real-time messaging, photo management, likes system, and role-based administration.

---

## ✨ Features

### 👤 Authentication & Security
- JWT-based authentication with **Refresh Token** (stored in HttpOnly cookies)
- Automatic token renewal in the background
- Secure logout that invalidates refresh tokens
- Role-based authorization: **Member**, **Moderator**, **Admin**

### 💌 Real-Time Messaging
- Instant messaging powered by **SignalR**
- Read receipts ("Seen", "Not read", "Delivered")
- Message inbox & outbox with pagination
- Soft delete (message disappears only for the deleting user)

### 🔥 Likes System
- Like / Unlike members
- View who you liked and who liked you back ("mutual likes")
- Paginated likes list

### 🖼️ Photo Management
- Upload photos via **Cloudinary** (auto-cropped & face-focused)
- Set a main profile photo
- Delete photos (main photo is protected)
- Admin/Moderator photo moderation panel

### 👥 Member Discovery
- Filter members by **gender**, **age range**, and **order by** (newest / last active)
- Online status indicator powered by SignalR Presence Hub
- Persistent filters saved in `localStorage`
- Fully paginated member grid

### 🛡️ Admin Panel
- View all users and their roles
- Edit user roles dynamically
- Photo moderation (Admin & Moderator access)

### 🎨 UI & UX
- **21 themes** powered by DaisyUI (Light, Dark, Synthwave, Cyberpunk, and more)
- Fully responsive layout (mobile-first)
- Smart HTTP caching interceptor (5-minute TTL, auto-invalidated on mutations)
- Loading spinner interceptor
- Custom toast notifications with avatar support and routing

---

## 🏗️ Architecture

```
Dating Application/
├── API/                          # ASP.NET Core Backend
│   ├── Controllers/              # AccountController, MembersController, MessagesController, LikesController, AdminController
│   ├── Data/                     # DbContext, Repositories (Unit of Work pattern)
│   │   ├── AppDbContext.cs
│   │   ├── MemberRepository.cs
│   │   ├── MessageRepository.cs
│   │   ├── LikesRepository.cs
│   │   └── UnitOfWork.cs
│   ├── Entities/                 # AppUser, Member, Photo, Message, MemberLike, Group, Connection
│   ├── DTOs/                     # Data Transfer Objects
│   ├── Interfaces/               # Repository & Service interfaces
│   ├── Services/                 # TokenService, PhotoService
│   ├── SignalR/                  # MessageHub, PresenceHub, PresenceTracker
│   ├── Helpers/                  # Pagination, Filters, LogUserActivity
│   ├── Extensions/               # AppUser & Message extension methods
│   └── Migrations/               # EF Core Migrations
│
└── client/                       # Angular Frontend
    └── src/
        ├── app/
        │   ├── core/
        │   │   ├── services/     # AccountService, MemberService, MessageService, LikesService, PresenceService, etc.
        │   │   ├── guards/       # authGuard, adminGuard, preventUnsavedChangesGuard
        │   │   ├── interceptors/ # JWT interceptor, Loading/Cache interceptor
        │   │   └── pipes/        # AgePipe, TimeAgoPipe
        │   ├── features/
        │   │   ├── home/         # Landing page & registration
        │   │   ├── members/      # Member list, detail, profile, photos, messages
        │   │   ├── lists/        # Likes list
        │   │   ├── messages/     # Inbox/Outbox
        │   │   └── admin/        # User management, photo moderation
        │   └── shared/           # Reusable components (paginator, confirm-dialog, image-upload, star/delete buttons, text-input, etc.)
        └── environments/         # Dev & Production environment configs
```

---

## 🛠️ Tech Stack

### Backend
| Technology | Purpose |
|---|---|
| ASP.NET Core 8 | REST API |
| Entity Framework Core | ORM |
| SQL Server | Database |
| ASP.NET Identity | User management & roles |
| JWT + Refresh Tokens | Authentication |
| SignalR | Real-time messaging & presence |
| Cloudinary | Photo upload & transformation |
| Docker | SQL Server containerization |

### Frontend
| Technology | Purpose |
|---|---|
| Angular 19 | SPA Framework |
| TypeScript | Language |
| DaisyUI + TailwindCSS | UI Components & Styling |
| SignalR Client | Real-time communication |
| Angular Signals | Reactive state management |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js 20+](https://nodejs.org/)
- [Docker](https://www.docker.com/) (for SQL Server)
- A [Cloudinary](https://cloudinary.com/) account

---

### 1. Clone the Repository

```bash
git clone https://github.com/YOUR_USERNAME/dating-application.git
cd dating-application
```

### 2. Start the Database

```bash
docker-compose up -d
```

This spins up SQL Server on port `1433` with the credentials defined in `docker-compose.yml`.

### 3. Configure the Backend

Edit `API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=datingdb;User Id=SA;Password=Password@1;TrustServerCertificate=true"
  },
  "TokenKey": "your_super_secret_key_at_least_64_characters_long_here",
  "CloudinarySettings": {
    "CloudName": "YOUR_CLOUD_NAME",
    "ApiKey": "YOUR_API_KEY",
    "ApiSecret": "YOUR_API_SECRET"
  }
}
```

> ⚠️ **Never commit real secrets to GitHub.** Use [dotnet user-secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) or environment variables in production.

### 4. Run the Backend

```bash
cd API
dotnet run
```

The API will start at `https://localhost:5001`. On first run, EF Core will automatically:
- Apply migrations
- Seed the database with **10 sample members** and an **admin** account

> **Default credentials (seed data):**
> - Members: `lisa@test.com` / `Pa$$w0rd`
> - Admin: `admin@test.com` / `Pa$$w0rd`

### 5. Run the Frontend

```bash
cd client
npm install
ng serve
```

The app will be available at `https://localhost:4200`.

---

## ☁️ Deployment (Azure)

The application is deployed on **Microsoft Azure**.

🔗 **Live URL:** [https://dating-app.azurewebsites.net/](https://dating-app.azurewebsites.net/)

The Angular build output is placed directly inside `API/wwwroot`, so the .NET app serves the Angular SPA as static files — making it a single deployable unit.

To build for production:

```bash
cd client
ng build --configuration production
```

Then publish the `API/` folder to Azure App Service.

---

## 🎬 Demo Video

📹 **Watch the full walkthrough:** [https://youtu.be/sEOz4jKxHac](https://youtu.be/sEOz4jKxHac)

The video covers:
- Registration & Login flow
- Browsing and filtering members
- Real-time messaging between users
- Likes system
- Photo upload & management
- Admin panel and role management
- Theme switching

---

## 🔐 Roles & Permissions

| Role | Access |
|---|---|
| **Member** | Browse members, like, message, manage own profile & photos |
| **Moderator** | All member permissions + photo moderation panel |
| **Admin** | All moderator permissions + user role management |

---

## 📡 API Endpoints

| Method | Endpoint | Description | Auth |
|---|---|---|---|
| POST | `/api/account/register` | Register new user | Public |
| POST | `/api/account/login` | Login | Public |
| POST | `/api/account/refresh-token` | Refresh JWT | Cookie |
| POST | `/api/account/logout` | Logout | ✅ |
| GET | `/api/members` | Get paginated members | ✅ |
| GET | `/api/members/{id}` | Get member by ID | ✅ |
| PUT | `/api/members` | Update own profile | ✅ |
| POST | `/api/members/add-photo` | Upload photo | ✅ |
| PUT | `/api/members/set-main-photo/{id}` | Set main photo | ✅ |
| DELETE | `/api/members/delete-photo/{id}` | Delete photo | ✅ |
| POST | `/api/likes/{targetId}` | Toggle like | ✅ |
| GET | `/api/likes` | Get likes list | ✅ |
| GET | `/api/messages` | Get messages (inbox/outbox) | ✅ |
| GET | `/api/messages/thread/{id}` | Get message thread | ✅ |
| POST | `/api/messages` | Send message | ✅ |
| DELETE | `/api/messages/{id}` | Delete message | ✅ |
| GET | `/api/admin/users-with-roles` | Get users with roles | 🔒 Admin |
| POST | `/api/admin/edit-roles/{userId}` | Edit user roles | 🔒 Admin |

### SignalR Hubs

| Hub | URL | Events |
|---|---|---|
| Presence | `/hubs/presence` | `UserOnline`, `UserOffline`, `GetOnlineUsers`, `NewMessageReceived` |
| Messages | `/hubs/messages` | `ReceiveMessageThread`, `NewMessage` |

---

## 🤝 Contributing

Pull requests are welcome! For major changes, please open an issue first.

---

## 📄 License

This project is licensed under the **MIT License**.

---

<div align="center">
  Made with ❤️ using .NET & Angular
</div>
