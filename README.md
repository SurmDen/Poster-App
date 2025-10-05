# 📝 PosterApp — ASP.NET Core & React Playground

![.NET 6](https://img.shields.io/badge/.NET-6.0-512BD4?style=for-the-badge&logo=.net)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-6.0-5C2D91?style=for-the-badge&logo=dotnet)
![React](https://img.shields.io/badge/Frontend-React-61DAFB?style=for-the-badge&logo=react&logoColor=000)
![SignalR](https://img.shields.io/badge/SignalR-Realtime-2E8B57?style=for-the-badge&logo=signal&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-CodeFirst-6F42C1?style=for-the-badge&logo=entity-framework)
![JWT](https://img.shields.io/badge/Auth-JWT-FF6B6B?style=for-the-badge&logo=json-web-tokens)

> _«PosterApp — это лаборатория идей, где каждая заметка превращается в историю.»_

---

## 🚀 Что за проект?

**PosterApp** — моя практическая площадка для ASP.NET Core 6 и React, где я отрабатывал создание контент-платформы с постами и заметками. Приложение сочетает в себе мощь backend-а и динамику фронтенда:

- ✍️ **Создание постов и заметок** с rich-редактором, Markdown и прикреплениями  
- 🗂️ **Теги и категории** для структурирования контента  
- 🤝 **Совместная работа в реальном времени** (черновики, комментарии) благодаря SignalR  
- 🧠 **Умные подсказки и автосохранение**  
- 🔐 **JWT-аутентификация** и разделение ролей (автор, редактор, админ)  
- 📣 **Уведомления** о комментариях, лайках, упоминаниях  
- 🌓 **Персональный контроль**: избранное, черновики, архив, пиннутые записи  
- 🗄️ **История версий** и возможность отката  

---

## 🔧 Технологический стек

| Слой             | Технология                             | Назначение                                      |
|------------------|-----------------------------------------|-------------------------------------------------|
| Backend          | ASP.NET Core 6                         | REST API, GraphQL endpoints (экспериментально)  |
| Data Access      | Entity Framework Core + SQL Server/PostgreSQL | ORM, миграции, LINQ                         |
| Real-Time        | SignalR                                 | Live-обновления: комментарии, блокноты          |
| Auth             | JWT + ASP.NET Identity                 | Авторизация, refresh tokens, роли               |
| Frontend         | React (TypeScript, Vite, Axios, Zustand)| SPA-интерфейс, state management                 |

---

## ⚙️ Быстрый старт

```bash
# Клонируем репозиторий
git clone https://github.com/your-org/Poster-App.git
cd posterapp

# Применяем миграции
dotnet ef database update

# Запуск API
dotnet run --project PosterApp
