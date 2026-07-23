# CineVault CLI

A command-line application for managing a personal movie and TV series library built with C# and .NET 8.

Users can browse their collection, add movies and TV series, update watched status, delete entries, and search The Movie Database (TMDB) for new titles. All data is persisted locally using JSON files.

---

## Features

- View movies and TV series
- Add movies and TV series manually
- Search and import movies from TMDB
- Search and import TV series from TMDB
- Update watched status
- Delete movies and TV series
- Persistent local storage using JSON
- Generic media service architecture

---

## Technologies

- C#
- .NET 8
- System.Text.Json
- TMDB API
- Generic Collections
- Object-Oriented Programming

---

## Project Structure

```
CineVault.CLI
│
├── Menus/
├── Models/
├── Services/
│   ├── External/
│   ├── Library/
|   ├── Search/
│   └── Storage/
├── Utilities/
├── Data/
│   ├── movies.json
│   └── tvseries.json
└── Program.cs
```

---

## Storage

Movie and TV series data is stored locally in JSON format.

```
Data/
├── movies.json
└── tvseries.json
```

The files are automatically created when the application runs for the first time.

---

## Running the Project

Clone the repository

```bash
git clone https://github.com/yourusername/cinevault-cli.git
```

Navigate into the project

```bash
cd cinevault-cli
```

Run the application

```bash
dotnet run --project CineVault.CLI
```

---

## Functionality

### Movies

- View all movies
- Search TMDB
- Search My Movie List
- Add movies
- Edit watched status
- Delete movies

### TV Series

- View all TV series
- Search TMDB
- Search My TV Series List
- Add TV series
- Edit watched status
- Delete TV series

---

## Architecture

```
           IMediaService<T>
                  ▲
                  │
          MediaService<T>
                  ▲
          ┌───────┴────────┐
          │                │
    MovieService     TvSeriesService
```

- `IMediaService<T>` defines the CRUD contract.
- `MediaService<T>` provides the shared generic CRUD implementation.
- `MovieService` and `TvSeriesService` inherit from `MediaService<T>` and handle movie/TV-specific functionality such as JSON persistence and seed data.

---

## Future Improvements

- Mark favourites
- Filter by genre
- Sort by rating or release year
- Pagination
- Unit testing
- SQLite database support
- User authentication
- Export/import library

---

## Author

Lelona Ntshiba
