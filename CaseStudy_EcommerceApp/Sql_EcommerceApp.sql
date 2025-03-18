create database EcommerceApp;

use EcommerceApp

create table customers
(
customer_id int primary key identity(1,1),
name varchar(100) not null,
email nvarchar(50) unique not null,
password nvarchar(50) not null
)
select * from customers

create table products
(
product_id int primary key identity(1,1),
name varchar(100) not null,
price decimal(10,2) not null,
description nvarchar(100),
stockQuantity int not null
)

create table cart
(
cart_id int primary key identity(1,1),
customer_id int foreign key references customers(customer_id),
product_id int foreign key references products(product_id),
quantity int not null
)

create table orders
(
order_id int primary key identity(1,1),
customer_id int foreign key references customers(customer_id),
order_date date,
total_price decimal(10,2),
shipping_address nvarchar(100)
)

create table order_items
(
order_item_id int primary key identity(1,1),
order_id int foreign key references orders(order_id),
product_id int foreign key references products(product_id),
quantity int 
)

