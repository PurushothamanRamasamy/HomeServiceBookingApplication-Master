Create database ServiceBooking

use ServiceBooking
drop table UserServiceInfo

Create table SpecializationTable( Name varchar(50) primary key)

insert into SpecializationTable values ('admin')

Create table SpecificationTable( Name varchar(50) primary key,SpecializationName varchar(50) foreign key references SpecializationTable(Name))

insert into SpecificationTable values ('admin','admin')

Create table UserServiceInfo(
USid varchar(50) primary key ,
Username varchar(50) unique,
Phoneno varchar(10),
EmailId varchar(50),
Password varchar(50),
specialization varchar(50) foreign key references SpecializationTable(Name),
Specification varchar(50) foreign key references SpecificationTable(Name),
ServiceCity varchar(100),
Address varchar(max),Aadhaarno varchar(50),role varchar(50),experience int,costperhour int,rating int)

insert into UserServiceInfo (USid,Username,Phoneno,EmailId,Password,specialization,Specification) values('9344418426admin','admin','9344418426','purushothaman.ramasamy@kanini.com','admin','admin','admin')
delete from UserServiceInfo where USid='9344418426admin'

alter table UserServiceInfo drop column IsNewProvicer 


alter table UserServiceInfo add IsNewProvider bit default 0
drop table Booking

create table Booking(Bookingid int primary key,CustomerId  varchar(50) FOREIGN KEY REFERENCES UserServiceInfo(USid),
	ServiceProviderID  varchar(50) FOREIGN KEY REFERENCES UserServiceInfo(USid),servicedate date,starttime time,endtime time,
	estimatedcost int,Bookingstatus bit default 0,Servicestatus bit default 0,Rating int
)



