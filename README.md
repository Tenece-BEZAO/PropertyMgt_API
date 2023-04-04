
# Property Management API
This is a Property Management API built using ASP.NET Core Web API, EF Core, IUnitOfWork and Irepository, this was to handle seperation of concerns.

## Purpose: 
This project aims  to craete a Property Management API for  property management system.


## Approach: 
I have followed the code first approach and designed EER model diagram first by defining tables and their attributes, established relationships and database objects like tables schema is populated. 
Here is the below approach to design an EER model 
a)	Property table – Consists of details of different buildings in the vicinity or on a street. 
b)	Tenant table – consists of details of the unit like unit number, resident id, no of bedrooms, no of washrooms, pet allowed, in unit laundry facility available.
c)	Staff Table – Management Staff and Maintenance Staff are responsible for overall management of units in respective buildings. Management staff takes care of lease details, payments, inspection checks and maintenance requests logged, if any. Maintenance Staff is responsible for servicing or repairing items found during maintenance requests logged or during inspection checks.
d)	Vendors – Residents can schedule an appointment with management staff for either logging maintenance requests or any other inquiries/concerns they have during the stay.
e)	Inspection checks – This table is designed to serve the purpose that all inspection checks are tracked and if any observation is found then they can be used for corrections or warnings to be given to residents.
f)	Maintenance Requests – This table helps to keep track of maintenance requests logged and ensure that they are being serviced by the maintenance staff on time.
g)	Lease: It keeps a track of lease details agreed between management company and resident Id. It has attributes like lease id, lease term, lease start and end date etc. 
h)	Payment:  This helps in keeping track of payment details such as monthly rent, deposit, pet deposit, total payment amount etc. 
i)	Security deposit returns: This helps in keeping track of expired leases and management company is now responsible for repayment of security deposits which they took from the residents during the start of the lease. 
j)	Role Table: This defines different roles of people here. 

## Features

* Manage applicants and tenants.
* Add, search and manage applications and leases.
* Manage applicants/tenants rental history, employment history and other references.
* Manage landlords, properties, and units.
* Reports (created by [Summary Reports plugin for AppGini](https://bigprof.com/appgini/applications/summary-reports-plugin)):
	* **Applicants by status** review how many new applicants vs. tenants vs. previous tenants do we have.
	* **Applications/leases over time** review the growth of leases and applications over time.
	* **Applications/leases by property** review the growth of demand for each property over time.
	* **Leases by property over time** review the growth of actual leases for each property over time.
	* **Lease value by property over time** check the monthly rental revenue for each property and its growth over time.
* Efficient management of applications status through mass update of multiple applications.
* Quickly view leases starting and ending each month through a calendar view -- also allows adding and editing (created by [Calendar plugin for AppGini](https://bigprof.com/appgini/applications/calendar-plugin)).
### Database Objects: 

1)	Tables – This has been described above. 


### Users :
1)	Admin: This user is part of management staff who can login and check maintenance requests logged, inspection checks. 
2)	Staff : This user is part of maintenance staff who can check maintenance requests.
3)	Tenant : This is a tenant staying in that property. They can login and check all the details related to them and maintenance requests logged by them. 


# Creators
* [Egbujie Chizoba Esther](https://github.com/Chizober)
* [Kelechi Amos](https://github.com/Kellyncodes)
* [Gilbert ](https://github.com/gillb08)
