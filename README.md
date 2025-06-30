# Demo Solution

This repo demonstrates a sample solution using .NET 9, featuring a web API server, a Blazor WebAssembly frontend, and supporting services like Redis and MySQL— orchestrated with Docker Compose.

#### Contents

- [Sample Contents](#sample-contents)
  - [API Server](#api-server)
  - [Blazor WebAssembly Client](#blazor-webassembly-client)
  - [Fluentd Integration](#fluentd-integration)
- [How to Run](#how-to-run)

# Sample Contents

Each project is containerized and communicates via shared Docker network to work seamlessly.

## API Server

ASP.NET Core 9 Web API Server. Hosts REST APIs for authentication and user registration.

#### Features

- `LoginSerice`: Handles user login and token issuance.
- `AccountService`: Manages account registration and basic user info.
- `ChatService`: Provides OpenAI chat interaction endpoints.
- `Session Management`: Uses [Redis]() to manage and validate user sessions.
- `Fluentd` logging management

## Blazor WebAssembly Client

.NET 9 Blazor WebAssembly (Standalone).

A modern SPA frontend using Blazor WASM and Fluent UI, connecting to the API server.

#### Features

- Shared loading overlay with state management.
- Token-based session control using [Blazored.LocalStorage]().
- Login, Register, and Home components with UI-driven navigation
- Auth state tracking using [AuthenticationStateProvider]()

## Fluentd Integration

This project includes a modular Fluentd logging system that enables scalable, centralized log collection across services.

#### Architecture

The Fluentd setup follows a Forwarder–Aggregator pattern:

1. Docker containers emit logs using Docker’s native Fluentd logging driver.
2. Logs are first received by a Fluentd Forwarder container (per service).
3. Forwarder relays logs to a central Fluentd Aggregator, which:
   - Parses and filters JSON logs.
   - Adds contextual metadata (e.g., UID, action).
   - Persists structured logs into a MySQL database using the `mysql_bulk` plugin.
   - Uses robust buffering and fallback mechanisms for fault tolerance.

This architecture is designed to isolate lightweight log routing (forwarder) from heavy processing (aggregator), making it scalable and production-ready.

Further details about Fluentd architecture, plugin setup, and buffering strategies are provided in a separate [README.md](Fluentd/README.md)

## How to Run

You can launch the full stack with docker compose using the provided [compose.yaml](compose.yaml) file.

To run the full stack locally:

1. Make sure you have Docker Desktop installed and running on Windows.
2. Run the following command:

```bash
docker-compose up -d
```

This will start all services in the background.
