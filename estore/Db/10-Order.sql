-- clear
/*
 drop table "Order";
 drop table OrderItem;
 drop table "Address";
 */

create table if not exists "Order" (
	OrderId serial primary key,
	SessionId uuid,
	UserId int,
	Created timestamp,
	Modified timestamp,
    AddressId int null
);

create index if not exists IX_Order_UserId on "Order" (UserId);

create table if not exists OrderItem (
	OrderItemId serial primary key,
	OrderId int,
	ProductId int,
	ProductCount int,
	Price int,
	Created timestamp,
	Modified timestamp
);


create index if not exists IX_OrderItem_OrderId on OrderItem (OrderId);
create index if not exists IX_OrderItem_ProductId on OrderItem (ProductId);

create table if not exists "Address" (
	AddressId serial primary key,
    UserId int null,
	Region varchar(50),
	City varchar(50),
	ZipCode varchar(10),
	Street varchar(50),
	"Status" int,

	House varchar(50),
	Appartment varchar(10),
	RecieverName varchar(100),
	Phone varchar(10),
	Email varchar(50),
	
	Created timestamp,
	Modified timestamp
);

create index if not exists IX_Address_UserId on "Address" (UserId);

alter table Cart add if not exists AddressId int null