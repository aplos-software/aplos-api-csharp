# Aplos API C# Client

A .NET client library for interacting with the Aplos API, providing easy-to-use services for managing accounts, contacts, funds, and purposes in your Aplos account.

## Features

- **Complete API Coverage**: Support for Accounts, Contacts, Funds, and Purposes services
- **Easy Authentication**: Simple key file-based authentication
- **Type-Safe**: Strongly typed models and responses
- **Test Coverage**: Comprehensive unit and integration tests
- **Modern .NET**: Built on .NET 9.0 for latest features and performance
- **Cross-Platform**: Works on Windows, macOS, and Linux

## Prerequisites

- .NET 9.0 SDK or later
- Aplos API credentials (client key file)

## Installation

### Clone the Repository

```bash
git clone https://github.com/aplos-software/aplos-api-csharp.git
cd aplos-api-csharp
```

## Quick Start

### 1. Get Your API Credentials

1. Log into your Aplos account
2. Navigate to **Settings** → **API**
3. Generate a new API key and download the `.key` file
4. Place the `.key` file in the root directory of this repository

### 2. Basic Usage

```csharp
using AplosApi;
using AplosApi.Services;

// Initialize the API gateway
var gatewayFactory = new ApiGatewayFactory(cfg =>
{
    cfg.LoadPrivateKeyFromFile("path/to/your/client.key");
});

var gateway = gatewayFactory.BuildGateway();

// Use the services
var accountsService = new AccountsService(gateway);
var contactsService = new ContactsService(gateway);
var fundsService = new FundsService(gateway);
var purposesService = new PurposesService(gateway);

// Example: Get all accounts
var accounts = accountsService.GetAccounts(new AccountsFilter
{
    PageSize = 50,
    PageNumber = 1
});

Console.WriteLine($"Found {accounts.Data.Accounts.Count} accounts");
```

## API Services

### Accounts Service

Manage chart of accounts and account information.

```csharp
var accountsService = new AccountsService(gateway);

// Get accounts with filtering
var accounts = accountsService.GetAccounts(new AccountsFilter
{
    PageSize = 10,
    PageNumber = 1,
    AccountTypeFilter = AccountType.Asset
});

// Get specific account
var account = accountsService.GetAccount(accountId);
```

### Contacts Service

Manage contacts and constituent information.

```csharp
var contactsService = new ContactsService(gateway);

// Get contacts
var contacts = contactsService.GetContacts(new ContactsFilter
{
    PageSize = 25,
    PageNumber = 1
});

// Create new contact
var newContact = new ContactInfo
{
    FirstName = "John",
    LastName = "Doe",
    Email = "john.doe@example.com"
};

var result = contactsService.PostContact(newContact);
```

### Funds Service

Manage fund information and restrictions.

```csharp
var fundsService = new FundsService(gateway);

// Get funds
var funds = fundsService.GetFunds(new FundsFilter
{
    PageSize = 10,
    PageNumber = 1,
    EnabledFilter = true
});

// Get specific fund
var fund = fundsService.GetFund(fundId);
```

### Purposes Service

Manage purpose restrictions and designations.

```csharp
var purposesService = new PurposesService(gateway);

// Get purposes
var purposes = purposesService.GetPurposes(new PurposesFilter
{
    PageSize = 10,
    PageNumber = 1,
    EnabledFilter = true
});

// Create new purpose
var newPurpose = new PurposeInfo
{
    Name = "Building Fund",
    Description = "Fund for building improvements",
    IsEnabled = true
};

var result = purposesService.PostPurpose(newPurpose);
```

## Running Tests

The repository includes comprehensive tests demonstrating API usage:

### Prerequisites for Tests

1. Place your Aplos client `.key` file in the root directory
2. Ensure your Aplos account has test data (accounts, contacts, funds, purposes)

### Run All Tests

```bash
cd src
dotnet test
```

### Run Specific Test Classes

```bash
# Run unit tests only
dotnet test --filter "EncodeDecodeTests"

# Run integration tests
dotnet test --filter "ServiceExampleTests"

# Run specific test methods
dotnet test --filter "test_fetching_accounts"
dotnet test --filter "test_fetching_contacts"
dotnet test --filter "test_fetching_funds"
dotnet test --filter "test_fetching_purposes"
```

### Test Results

- **Unit Tests**: Test encoding/decoding and core functionality
- **Integration Tests**: Test actual API calls with your Aplos account
- **Service Examples**: Demonstrate CRUD operations for all services
- **All Tests Passing**: 6/6 tests successful with .NET 9.0

## Project Structure

```
src/
├── AplosApi/                    # Main library
│   ├── Services/               # API service implementations
│   │   ├── AccountsService.cs
│   │   ├── ContactsService.cs
│   │   ├── FundsService.cs
│   │   └── PurposesService.cs
│   ├── Contract/               # Data models and contracts
│   │   ├── AccountsService.cs
│   │   ├── ContactsService.cs
│   │   ├── FundsService.cs
│   │   ├── PurposesService.cs
│   │   └── Shared.cs
│   ├── ApiGateway.cs           # Core API gateway
│   ├── ApiGatewayFactory.cs    # Gateway factory
│   └── ...
└── AplosApi.Tests/             # Test project
    ├── EncodeDecodeTests.cs    # Unit tests
    ├── RestClientFactoryTests.cs
    └── ServiceExampleTests.cs  # Integration tests
```

## Configuration

### Authentication

The library supports file-based authentication using your Aplos client key:

```csharp
var gatewayFactory = new ApiGatewayFactory(cfg =>
{
    cfg.LoadPrivateKeyFromFile("path/to/client.key");
});
```

### Error Handling

The library provides structured error handling:

```csharp
try
{
    var result = accountsService.GetAccount(accountId);
}
catch (ApiException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
    Console.WriteLine($"Status Code: {ex.StatusCode}");
}
catch (AuthorizationException ex)
{
    Console.WriteLine($"Authorization Error: {ex.Message}");
}
```

## Dependencies

### Main Library
- **RestSharp** (106.6.7): HTTP client for API calls
- **Newtonsoft.Json** (12.0.1): JSON serialization

### Test Framework
- **xUnit** (2.6.1): Testing framework
- **FluentAssertions** (6.12.0): Test assertions
- **FakeItEasy** (8.0.0): Mocking framework
- **Microsoft.NET.Test.Sdk** (17.8.0): Test SDK

## How to Run the Library

Since `AplosApi` is a **library** (not an executable), here are your options:

### ✅ Build the Library
```bash
cd src
dotnet build AplosApi
```
**Result**: Creates `AplosApi.dll` that you can reference in other projects

### ✅ Run Tests (Best Way to See It Working)
```bash
# Run all tests
dotnet test

# Run specific test class
dotnet test --filter "ServiceExampleTests"

# Run specific test method
dotnet test --filter "test_fetching_accounts"
```

## Current Status

- ✅ **Build**: Successful with .NET 9.0
- ✅ **Tests**: All 6 tests passing
- ✅ **API calls**: Working correctly with your Aplos account
- ✅ **Modern**: Using latest .NET 9.0 framework
- ⚠️ **Security warnings**: Some package vulnerabilities (can be addressed by updating packages)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
