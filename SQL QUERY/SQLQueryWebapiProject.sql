Create database ServiceBooking

use ServiceBooking
drop table UserServiceInfo
drop table SpecializationTable
drop table Booking
drop table SpecificationTable

Create table SpecializationTable( Name varchar(50) primary key)

insert into SpecializationTable values ('admin')

Create table SpecificationTable( id int primary key identity,SpecializationName varchar(50) foreign key references SpecializationTable(Name), Name varchar(50))

insert into SpecificationTable values ('admin','admin')

Create table UserServiceInfo(
USid varchar(50) primary key ,
Username varchar(50) unique,
Phoneno varchar(10),
EmailId varchar(50),
Password varchar(50),
specialization varchar(50) foreign key references SpecializationTable(Name),
Specification varchar(50),
ServiceCity varchar(100),
Address varchar(max),Aadhaarno varchar(50),role varchar(50),experience int,costperhour int,rating int,IsNewProvider bit default 0,IsProvicedBooked bit default 0)

insert into UserServiceInfo (USid,Username,Phoneno,EmailId,Password,specialization,Specification,role) values('9876543210','admin','9876543210','admin@homeservice.com','admin','admin','admin','admin')
delete from UserServiceInfo where USid='9344418426admin'
UPDATE UserServiceInfo  set role='admin' where username='admin'
alter table UserServiceInfo drop column IsNewProvicer 


alter table UserServiceInfo add IsNewProvider bit default 0
drop table Booking

create table Booking(Bookingid int primary key identity ,CustomerId  varchar(50) FOREIGN KEY REFERENCES UserServiceInfo(USid),
	ServiceProviderID  varchar(50) FOREIGN KEY REFERENCES UserServiceInfo(USid),servicedate date,starttime int,endtime int,
	estimatedcost int,Bookingstatus bit default 0,Servicestatus bit default 0,Rating int
)



