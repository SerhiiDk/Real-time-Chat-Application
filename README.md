# ChatApplication

# Chat Application

This project demonstrates a simple chat application built with the following technologies and services:

- **Azure SQL Database**: Used as the primary database with **Entity Framework Core** for ORM.
- **SignalR Service (Azure)**: Enables real-time communication between users.
- **Azure Cognitive Services - Text Analytics**: Used for sentiment analysis on messages.

## Features

### Sentiment-based Message Highlighting
Messages in the chat are highlighted based on their sentiment:
- **Positive Sentiment**: Highlighted in light green.
- **Neutral Sentiment**: Highlighted in light gray.
- **Negative Sentiment**: Highlighted in light red.

### Simple Login Page
The application includes a simple login page. Two users are pre-registered in the database:

1. **User@gmail.com** with password `User123`
2. **Alex@gmail.com** with password `Alex123`

You can also create a new user to log in.

## Limitations

Due to time constraints, the following features were not fully implemented:
1. **FluentValidation**: Planned to enhance input validation.
2. **Azure Key Vault**: Intended for securely storing sensitive configuration details.
3. **User Identity System**: To manage user authentication and authorization in detail.
4. **JWT (JSON Web Token)**: For token-based authentication.

Additionally, when users connect to SignalR, I planned to save their connection information to the database for session tracking. Unfortunately, this functionality is not fully implemented due to time limitations.

---

Feel free to explore the app and provide feedback!
