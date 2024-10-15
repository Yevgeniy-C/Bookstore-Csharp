-- clear
/*
 drop table Cart;
 drop table CartItem;
 */

create table if not exists Cart (
	CartId serial primary key,
	SessionId uuid,
	UserId int,
	Created timestamp,
	Modified timestamp
);

create index if not exists IX_Cart_SessionId on Cart (SessionId);
create index if not exists IX_Cart_UserId on Cart (UserId);
create index if not exists IX_Cart_Created on Cart (Created);


create table if not exists CartItem (
	CartItemId serial primary key,
	CartId int,
	ProductId int,
	ProductCount int,
	Created timestamp,
	Modified timestamp
);


create index if not exists IX_CartItem_CartId on CartItem (CartId);
create index if not exists IX_CartItem_ProductId on CartItem (ProductId);

