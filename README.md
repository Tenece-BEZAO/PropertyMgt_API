
# ASP.NETCore  Property Management API

This is a Property Management API built using ASP.NET Core Web API, EF Core, IUnitOfWork and Irepository, this was to handle seperation of concerns.

## Objective: 

The Property Management API is a SaaS platform that helps property managers and landlords manage their rental properties. The API should provide features for managing leases, collecting rent, handling maintenance requests, and managing tenant data.

## Features

* Property management: The API should allow property managers to manage multiple properties, including adding and removing properties, updating property details, and managing tenant data.


* Lease management: The API should provide tools for managing lease agreements, including creating new leases, renewing leases, and tracking lease details such as rent payments and security deposits.


* Rent collection: The API should allow landlords to collect rent payments online, including automatic payment processing and payment reminders.


* Maintenance requests: The API should provide a system for handling maintenance requests, including creating new requests, tracking the status of requests, and assigning maintenance tasks to staff or contractors.


## Screenshots

|                                                                 |                                                                                         |
|:---------------------------------------------------------------:|:---------------------------------------------------------------------------------------:|
| **Lease Routes**                                                  |                               **Manager Routes**                                               |
| [<img src="./images/Lease.png" alt="Lease" width="100%"/>](./images/Lease.png") | [<img src="./images/Manager.png" alt="Manager" width="100%"/>](./images/Manager.png) |
|  **User Authentication**                                        |                                                 **Tenant details**                      |
| [<img src="./images/userAuth.png" alt="User Auth" width="700"/>](./images/userAuth.png)| [<img src="./documentation/pictures/tenantcontract.png" alt="drawing" width="350"/>](./documentation/pictures/tenantcontract.png) | |
| **Properties page**           | **Property details**               | |
| [<img src="./documentation/pictures/properties.png" alt="drawing" width="350"/>](./documentation/pictures/properties.png) | [<img src="./documentation/pictures/property.png" alt="drawing" width="350"/>](./documentation/pictures/property.png)| |
| **Landlord page**             | **Template leases**                | **Author a contract**          |
| [<img src="./documentation/pictures/landlord.png" alt="drawing" width="350"/>](./documentation/pictures/landlord.png) | [<img src="./documentation/pictures/leases.png" alt="drawing" width="350"/>](./documentation/pictures/leases.png) | [<img src="./documentation/pictures/contracttemplate.png" alt="drawing" width="350"/>](./documentation/pictures/contracttemplate.png) |
| **Members**                        | |
| [<img src="./documentation/pictures/members.png" alt="drawing" width="350"/>](./documentation/pictures/members.png) | |

## Getting started

### Clone the GitHub repository
```shell
$ git clone https://github.com/yourusername/property-management-api.git
```
- Open the solution in Visual Studio.
- Run the following commands in the Package Manager Console:

```shell 
 Add-Migration InitialCreate
 Update-Database
```


## Technologies and Libraries
- .NET 6
- ASP.NET Core 6.0
- Swagger UI
- Entity Framework Core
- SQLServer
- AutoMapper
- MicrosoftEntityFramework Tools
- MicrosoftEntityFramework Design


## Principles and Patterns
- Clean Architecture
- Clean Code
- SOLID Principles
- Acyclic Dependencies Principle
- Separation of Concerns

## Benefits
- Simple and evolutionary architecture.
- Standardized and centralized flow for validation, log, security, return, etc.
- Avoid cyclical references.
- Avoid unnecessary dependency injection.
- Segregation by feature instead of technical type.
- Single responsibility for each request and response.

Features IMPLEMENTED
====================
Admin-End
--------
- Authentication/Registration
- Adding of Properties
- Editing/Updating of Properties with photos, location and property features
- Database Migrations and Seeder
- Manage Users from the Backend - by Admin alone
- Super-Admin Access Control


## Approach: 
I followed the code first approach, created Entities, their attributes, established relationships and these entities are migrated to database as tables. 

Below are the entities used:

*	Property table – Consists of details of different buildings in the vicinity or on a street. 

*	Tenant table – consists of details of the unit like unit number, resident id, no of bedrooms, no of washrooms, pet allowed, in unit laundry facility available.

*	Staff Table – Management Staff and Maintenance Staff are responsible for overall management of units in respective buildings. Management staff takes care of lease details, payments, inspection checks and maintenance requests logged, if any. Maintenance Staff is responsible for servicing or repairing items found during maintenance requests logged or during inspection checks.


*	Inspection checks – This table is designed to serve the purpose that all inspection checks are tracked and if any observation is found then they can be used for corrections or warnings to be given to residents.

*	MaintenanceRequests – This table helps to keep track of maintenance requests logged and ensure that they are being serviced by the maintenance staff on time.

*	Lease: It keeps a track of lease details agreed between management company and resident Id. It has attributes like lease id, lease term, lease start and end date etc. 

* Payment:  This helps in keeping track of payment details such as monthly rent, deposit, pet deposit, total payment amount etc. 

*	Security deposit returns: This helps in keeping track of expired leases and management company is now responsible for repayment of security deposits which they took from the residents during the start of the lease. 



##### _points to note_

- Migration Commands (in Nuget console)
  - `add migration <name>`
  - `update-database`
- Use inbuilt Swagger or else postman and use the APIs to add some dummy data to table
- Run the WebAPI first(recommeneded to run from visual studio)


### Users :

1)	Admin: This user is part of management staff who can login and check maintenance requests logged, inspection checks. 
2)	Staff : This user is part of maintenance staff who can check maintenance requests.
3)	Tenant : This is a tenant staying in that property. They can login and check all the details related to them and maintenance requests logged by them. 

### Known Errors:

- If any error pops up regarding namespaces, add the reference to the DataAccess project or dll from the webapi project
### Contributing

- Contributions are welcome! If you find any issues or have suggestions for improvements, please submit an issue using  [New Issue](https://github.com/Tenece-BEZAO/PropertyMgt_API/issues/new) button, or a pull request.

# Creators
* [Egbujie Chizoba Esther](https://github.com/Chizober)
* [Kelechi Amos](https://github.com/Kellyncodes)
* [Gilbert ](https://github.com/gillb08)
