# Weather Viewer - Test Task

This test task involves creating a web application with a graphical user interface using either classic MVC or Razor Pages to upload and display weather archives for a selected locality.

## Requirements

1. **Home page**:
    - A simple navigation menu with links to the viewing page and informational page.

2. **Weather archives viewing page**:
    - Display a list of uploaded weather archives with pagination.
    - Allow navigation to view detailed weather observations for a selected archive.

3. **Weather archives upload page**:
    - Form to upload Excel archive file containing weather data selecting target locality.

4. **Specific archive weather observations page**:
    - Display detailed weather observations for a selected archive with pagination.
    - Filter observations by month and year.

## Installation

1. **Clone the repository**:
    ```sh
    git clone https://github.com/Vazovsk1y/TestTask.WeatherViewer.git
    cd TestTask.WeatherViewer
    ```

2. **Configure the database**:
    - Ensure you have a PostgreSQL instance running.
    - Update the connection string in `appsettings.json`.

3. **Run the Application**:
    ```sh
    dotnet run
    ```

## Technologies Used

- ASP.NET Core Razor Pages
- Entity Framework Core
- PostgreSQL