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
customer_id int references customers(customer_id) on delete cascade,
product_id int references products(product_id),
quantity int not null
)

create table orders
(
order_id int primary key identity(1,1),
customer_id int references customers(customer_id) on delete cascade,
order_date date,
total_price decimal(10,2),
shipping_address nvarchar(100)
)

create table order_items
(
order_item_id int primary key identity(1,1),
order_id int references orders(order_id) on delete cascade,
product_id int references products(product_id),
quantity int 
)

select * from customers;
select * from products;
select * from cart;
select * from orders;
select * from order_items;

insert into customers (name, email, password) values
('Ancy', 'ancy@example.com', 'ancy123'),
('Beni', 'beni@example.com', 'bb123'),
('Charlie', 'charlie@example.com', 'charlie123'),
('Daniel', 'daniel@example.com', 'daniel123'),
('Eric', 'eric@example.com', 'eva123');


insert into products (name, price, description, stockQuantity) values
('Laptop', 899.99, '15-inch display laptop', 10),
('Wireless Mouse', 19.99, 'Bluetooth mouse', 50),
('Headphones', 49.99, 'Noise-cancelling headphones', 25),
('USB-C Charger', 29.99, 'Fast charger for phones', 40),
('Smart Watch', 149.99, 'Fitness and health tracker', 20);


insert into cart (customer_id, product_id, quantity) values
(1, 1, 1),
(2, 2, 2),
(3, 3, 1),
(1, 4, 3),
(2, 5, 1);

insert into orders (customer_id, order_date, total_price, shipping_address) values
(1, '2024-04-01', 929.98, '123 Apple St, NY'),
(2, '2024-04-02', 169.98, '456 Banana Rd, TX'),
(3, '2024-04-03', 49.99, '789 Cherry Blvd, CA'),
(1, '2024-04-04', 29.99, '123 Apple St, NY'),
(2, '2024-04-05', 149.99, '456 Banana Rd, TX');


insert into order_items (order_id, product_id, quantity) values
(1, 1, 1),
(1, 4, 1),
(2, 2, 2),
(3, 3, 1),
(4, 4, 1),
(5, 5, 1);
