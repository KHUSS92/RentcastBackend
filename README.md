Rentcast Property Management System
Overview

The Rentcast Property Management System is a full-stack application that integrates with the Rentcast API to fetch property data and utilizes Neo4j as a graph database for advanced relationship-based data management. It includes a React frontend (optional) for visualization and interaction.

Features

    API Integration:
        Fetch detailed property information from Rentcast API.
        Store property details in Neo4j for graph-based analysis.

    Neo4j Integration:
        Store properties, relationships, and additional metadata in a graph structure.
        Perform advanced graph queries for relationship mapping.

    Endpoints:
        GET /api/property/{id}: Fetch property details from Rentcast or Neo4j.
        POST /api/graph/nodes: Add a new node to Neo4j.
        POST /api/graph/relationships: Create relationships between nodes.
        GET /api/graph/nodes: Retrieve all nodes from Neo4j.
        GET /api/graph/relationships: Retrieve relationships between nodes.

    Unit Testing:
        Comprehensive tests for services, controllers, and graph interactions using NUnit and NSubstitute.

Technologies Used

    Backend:
        ASP.NET Core Web API
        Neo4jClient for Neo4j integration
        Rentcast API for property data

    Frontend (Optional):
        React (Planned for graph visualization)

    Testing:
        NUnit
        NSubstitute
        MockHttpMessageHandler for API mocking

    Database:
        Neo4j (Graph Database)

Setup Instructions

Clone the Repository:

Set Up Neo4j:

    Install and run Neo4j.
    Update connection credentials in appsettings.json:

    {
        "Neo4j": {
            "Url": "bolt://localhost:7687",
            "Username": "neo4j",
            "Password": "password"
        }
    }

Configure Rentcast API Key:

    Add your API key to appsettings.json:

    {
        "RentcastApi": {
            "ApiKey": "your-rentcast-api-key"
        }
    }

Run the Application:

    dotnet run

Run Unit Tests:

    dotnet test

Project Structure

    RentcastBackend/
    ├── Controllers/
    │   ├── PropertyController.cs
    │   ├── GraphController.cs
    ├── Models/
    │   ├── Property.cs
    │   ├── PropertyDetails.cs
    │   ├── HOA.cs
    │   ├── PropertyFeatures.cs
    │   ├── TaxAssessment.cs
    │   ├── PropertyTax.cs
    │   ├── HistoryEvent.cs
    │   ├── OwnerModel.cs
    ├── Services/
    │   ├── PropertyService.cs
    │   ├── RentcastService.cs
    │   ├── Neo4jService.cs
    ├── Tests/
    │   ├── PropertyControllerTests.cs
    │   ├── PropertyServiceTests.cs
    │   ├── RentcastServiceTests.cs
    │   ├── Neo4jServiceTests.cs
    │   ├── GraphControllerTests.cs
    ├── appsettings.json
    ├── appsettings.Development.json
    └── Program.cs

Planned Features

    React frontend for visualizing property relationships in Neo4j.
    CI/CD pipeline for deployment.
    Enhanced graph querying features for more detailed analysis.
