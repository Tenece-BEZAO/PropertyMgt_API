
# ASP.NET Core  Property Management API

This is a Property Management API built using ASP.NET Core Web API, EF Core, IUnitOfWork and Irepository, this was to handle seperation of concerns.

## Objective: 
The Property Management API is a SaaS platform that helps property managers and landlords manage their rental properties. The API should provide features for managing leases, collecting rent, handling maintenance requests, and managing tenant data.

> c#

> .Net 6.0
> 
 ###  Asp.netcore  web API application with sqlserver as backend (Code first approach)
##### Some major Components Used
> Asp.netcore web api Material

> Swagger UI

> Nuget Packages

## Approach: 
I have followed the code first approach and designed EER model diagram first by defining tables and their attributes, established relationships and database objects like tables schema is populated. 
Here is the below approach to design an EER model 
-	Property table – Consists of details of different buildings in the vicinity or on a street. 
-	Tenant table – consists of details of the unit like unit number, resident id, no of bedrooms, no of washrooms, pet allowed, in unit laundry facility available.
-	Staff Table – Management Staff and Maintenance Staff are responsible for overall management of units in respective buildings. Management staff takes care of lease details, payments, inspection checks and maintenance requests logged, if any. Maintenance Staff is responsible for servicing or repairing items found during maintenance requests logged or during inspection checks.
- Vendors – Residents can schedule an appointment with management staff for either logging maintenance requests or any other inquiries/concerns they have during the stay.
-	Inspection checks – This table is designed to serve the purpose that all inspection checks are tracked and if any observation is found then they can be used for corrections or warnings to be given to residents.
-	Maintenance Requests – This table helps to keep track of maintenance requests logged and ensure that they are being serviced by the maintenance staff on time.
-	Lease: It keeps a track of lease details agreed between management company and resident Id. It has attributes like lease id, lease term, lease start and end date etc. 
- Payment:  This helps in keeping track of payment details such as monthly rent, deposit, pet deposit, total payment amount etc. 
-	Security deposit returns: This helps in keeping track of expired leases and management company is now responsible for repayment of security deposits which they took from the residents during the start of the lease. 



##### _points to note_

- Migration Commands (in Nuget console)
  - `add migration <name>`
  - `update-database`
- Use inbuilt Swagger or else postman and use the APIs to add some dummy data to table
- Run the WebAPI first(recommeneded to run from visual studio)
## Features

* Property management: The API should allow property managers to manage multiple properties, including adding and removing properties, updating property details, and managing tenant data.


* Lease management: The API should provide tools for managing lease agreements, including creating new leases, renewing leases, and tracking lease details such as rent payments and security deposits.


* Rent collection: The API should allow landlords to collect rent payments online, including automatic payment processing and payment reminders.


* Maintenance requests: The API should provide a system for handling maintenance requests, including creating new requests, tracking the status of requests, and assigning maintenance tasks to staff or contractors.


### Users :

1)	Admin: This user is part of management staff who can login and check maintenance requests logged, inspection checks. 
2)	Staff : This user is part of maintenance staff who can check maintenance requests.
3)	Tenant : This is a tenant staying in that property. They can login and check all the details related to them and maintenance requests logged by them. 

### Known Errors:

- If any error pops up regarding namespaces, add the reference to the DataAccess project or dll from the webapi project

# Creators
* [Egbujie Chizoba Esther](https://github.com/Chizober)
* [Kelechi Amos](https://github.com/Kellyncodes)
* [Gilbert ](https://github.com/gillb08)
