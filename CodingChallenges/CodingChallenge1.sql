create database CaseStudyEcom
drop database CaseStudyEcom

use CaseStudyEcom

create table customers
(
customer_id int primary key identity(1,1),
firstName varchar(50) not null,
lastName varchar(50) not null,
email nvarchar(50) unique not null,
address nvarchar(50) not null
)
drop table customers
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
customer_id int references customers(customer_id),
product_id int references products(product_id),
quantity int not null
)

create table orders
(
order_id int primary key identity(1,1),
customer_id int references customers(customer_id),
order_date date,
total_price decimal(10,2)
--shipping_address nvarchar(100)
)
select * from orders
create table order_items
(
order_item_id int primary key identity(1,1),
order_id int references orders(order_id),
product_id int references products(product_id),
quantity int,
itemAmount decimal(10,2)
)
select * from order_items
insert into products  values('Laptop',800,'High-performance laptop',10),
('Smartphone',600,'Latest smartphone',15),
('Tablet',300,'Portable tablet',20),
('Headphones',150,'Portable tablet',30),
('TV',900,'4k Smart TV',5),
('Coffee Maker',50,'Automatic coffee maker',25),
('Refrigerator',700,'Energy-efficient',10),
('Microwave Oven',80,'Countertop microwave',15),
('Blender',70,'High-speed blender',20),
('Vacuum Cleaner',120,'Bagless vacuum cleaner',10)

select * from products

insert into customers values('John','Doe','johndoe@example.com','123 Main St,City'),
('Jane','Smith','janesmith@example.com','456 Elm St, Town'),
('Robert','Johnson','robert@example.com','789 Oak St,Village'),
('Sarah','Brown','sarah@example.com','101 Pine St,Suburb'),
('David','Lee', 'david@example.com', '234 Cedar St,District'),
('Laura','Hall', 'laura@example.com', '567 Birch St,Country'),
('Michael','Davis', 'michael@example.com', '890 Maple St,State'),
('Emma','Wilson', 'emma@example.com', '321 Redwood St,Country'),
('William','Taylor', 'william@example.com', '432 Spruce St,Province'),
('Olivia','Adams', 'olivia@example.com', '765 Fir St,Territory')

insert into orders values(1, '2023-01-05', 1200.00),
(2, '2023-02-10', 900.00),
(3, '2023-03-15', 300.00),
(4, '2023-04-20', 150.00),
(5, '2023-05-25', 1800.00),
(6, '2023-06-30', 400.00),
(7, '2023-07-05', 700.00),
(8, '2023-08-10', 160.00),
(9, '2023-09-15', 140.00),
(10, '2023-10-20', 1400.00)

insert into order_items values(1, 1, 2, 1600.00),
(1, 3, 1, 300.00),
(2, 2, 3, 1800.00),
(3, 5, 2, 1800.00),
(4, 4, 4, 600.00),
(4, 6, 1, 50.00),
(5, 1, 1, 800.00),
(5, 2, 2, 1200.00),
(6, 10, 2, 240.00),
(6, 9, 3, 210.00)

insert into cart values (1, 1, 2),
(1, 3, 1),
(2, 2, 3),
(3, 4, 4),
(3, 5, 2),
(4, 6, 1),
(5, 1, 1),
(6, 10, 2),
(6, 9, 3),
(7, 7, 2)
select * from cart

--1
update products set price=800 where name='Refrigerator'

--2
delete from cart where customer_id=3

--3
select * from products 
where price<100

--4
select * from products 
where stockQuantity>5

--5
select * from orders
where total_price between 500 and 1000

--6
select * from products
where name like '%r'

--7
select * from cart 
where customer_id=5

--8
select c.* from customers c join orders o
on o.customer_id=c.customer_id
where year(o.order_date)=2023

--9
select product_id,name, min(stockQuantity) As [Minimum Stock Quantity] 
from products
group by name,product_id

--10
select customer_id,sum(total_price) [Total amount spent] from orders
group by customer_id

--11
select customer_id,avg(total_price) [Average Order Amount] from orders
group by customer_id

--12
select customer_id,count(*) [Number of orders] from orders
group by customer_id

--13
select customer_id,max(total_price) [Maximum Order Amount] from orders
group by customer_id

--14
select customer_id,sum(total_price) [Total Amount] from orders
group by customer_id
having sum(total_price)>1000

--15
select * from products
where product_id not in
(select product_id from cart)

--16
select * from customers
where customer_id not in
(select customer_id from orders)

--17
select p.product_id, p.name, 
(SUM(oi.quantity * p.price) / (select SUM(total_price) FROM orders) * 100)
[Revenue percentage]
from order_items oi
join products p on oi.product_id = p.product_id
group by p.product_id, p.name


--18
select * FROM products 
where stockQuantity in (select MIN(stockQuantity) from products)

--19
select c.* from customers c
where c.customer_id in 
(select top 5 customer_id from orders
group by customer_id
order by sum(total_price))

