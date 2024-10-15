/*
drop  table Billing
*/

create table if not exists Billing (
	BillingId serial primary key,
	UserId int,
    CardType int,
    CardNumber varchar(200),
    OwnerName varchar(200),
    ExpYear varchar(200),
    ExpMonth varchar(200),
    BillingAddressId int null,
	Created timestamp,
	Modified timestamp,
    "Status" int
);

create index if not exists IX_Billing_UserId on Billing (UserId);

alter table Cart add if not exists BillingId int null

