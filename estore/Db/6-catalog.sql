-- clear
/*
 drop table Author;
 drop table ProductSerie;
 drop table Category;
 drop table Product;
 drop table ProductDetail;
 drop table ProductAuthor;
 */

create table if not exists Author (
	AuthorId serial primary key,
	FirstName varchar(50),
	MiddleName varchar(50),
	LastName varchar(50),
	Description text,
	AuthorImage varchar(100),
	UniqueId varchar(50)
);


create table if not exists ProductSerie (
	ProductSerieId serial primary key,
	SerieName varchar(50)
);

create table if not exists Category (
	CategoryId serial primary key,
	ParentCategoryId int, 
	CategoryName varchar(50),
	CategoryUniqueId varchar(50)
);

create index if not exists IX_Category_ParentCategoryId on Category (ParentCategoryId);

create table if not exists Product (
	ProductId serial primary key,
	CategoryId int, 
	ProductName varchar(200),
	Price int,
	Description text,
	ProductImage varchar(200),
	ReleaseDate timestamp,
	UniqueId varchar(100),
	ProductSerieId int
);

create index if not exists IX_Product_ParentCategoryId on Product (CategoryId);
create index if not exists IX_Product_ProductSerieId on Product (ProductSerieId);

create table if not exists ProductDetail (
	ProductDetailId serial primary key,
	ProductId int,
	ParamName varchar(50),
	StringValue varchar(100)
);

create index if not exists IX_ProductDetail_ProductId on ProductDetail (ProductId);

create table if not exists ProductAuthor (
	ProductId int,
	AuthorId int,
	PRIMARY KEY (ProductId, AuthorId)
);
