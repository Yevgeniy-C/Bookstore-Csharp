alter table "Order" add if not exists BillingId int null;

alter table Cart add if not exists IsArchived int null;

drop index if exists IX_Cart_UserId ;

create index if not exists IX_Cart_UserIdIsArchived on Cart (UserId, IsArchived);

alter table "Order" add if not exists CartId int;

