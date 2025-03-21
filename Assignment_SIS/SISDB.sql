-- Task 1. Database Design:

create database SISDB
drop database SISDB

use SISDB
 
create table Students
(
student_id int primary key identity(1,1),
first_name varchar(50) not null,
last_name varchar(50) not null,
date_of_birth date not null,
email varchar(100) unique not null,
phone_number varchar(15) unique not null
)

create table Teacher
(
teacher_id int primary key identity(1,1),
first_name varchar(50) not null,
last_name varchar(50) not null,
email varchar(50) unique not null
)

create table Courses
(
course_id int primary key identity(1,1),
course_name varchar(100) not null,
credits int not null,
teacher_id int references Teacher(teacher_id) on delete set null 
)

create table Enrollments
(
enrollment_id int primary key identity(1,1),
student_id int references Students(student_id) on delete cascade,
course_id int references Courses(course_id) on delete cascade,
enrollment_date date
)

create table Payments
(
payment_id int primary key identity(1,1),
student_id int foreign key references Students(student_id) on delete cascade,
amount decimal(10,2) not null,
payment_date date
)

insert into Students values
('Jacky', 'Chan', '2000-01-15', 'jack.dril@example.com', '9876543210'),
('Joseph', 'Park', '1999-05-22', 'jane.smith@example.com', '9123456789'),
('Micke', 'Johnson', '2001-03-11', 'michael.johnson@example.com', '9988776655'),
('Emy', 'Jackson', '2000-09-30', 'emily.davis@example.com', '9876543201'),
('Daniel', 'Immansingh', '2002-07-14', 'daniel.brown@example.com', '9123456798'),
('Eric', 'Wilson', '2001-11-05', 'eric.wilson@example.com', '9786543210'),
('Spark', 'Taylor', '2003-02-28', 'spark.taylor@example.com', '9988776644'),
('Sophia', 'Akil', '2000-08-18', 'sophia.anderson@example.com', '9876543220'),
('James', 'Jessy', '2002-04-04', 'james.martinez@example.com', '9123456788'),
('Isabella', 'Garcia', '2001-06-25', 'isabella.garcia@example.com', '9786543211')

insert into Teacher values
('Alan', 'Walker', 'alan.walker@example.com'),
('Brenda', 'Harris', 'brenda.harris@example.com'),
('Charles', 'Evans', 'charles.evans@example.com'),
('Diana', 'Clark', 'diana.clark@example.com'),
('Edward', 'Wright', 'edward.wright@example.com'),
('Fiona', 'Lopez', 'fiona.lopez@example.com'),
('George', 'Scott', 'george.scott@example.com'),
('Hannah', 'Adams', 'hannah.adams@example.com'),
('Ian', 'Baker', 'ian.baker@example.com'),
('Julia', 'Nelson', 'julia.nelson@example.com')

insert into Courses values
('Mathematics', 4, 1),
('Physics', 3, 2),
('Chemistry', 3, 3),
('Biology', 4, 4),
('English Literature', 3, 5),
('Computer Science', 4, 6),
('History', 2, 7),
('Economics', 3, 8),
('Political Science', 2, 9),
('Psychology', 3, 10)

insert into Enrollments values
(1, 4, '2024-01-10'),
(2, 2, '2024-01-15'),
(3, 3, '2024-02-01'),
(4, 2, '2024-02-10'),
(5, 5, '2024-03-05'),
(6, 1, '2024-03-12'),
(7, 9, '2024-04-01'),
(8, 6, '2024-04-15'),
(9, 7, '2024-05-01'),
(10, 8, '2024-05-10')

insert into Payments values
(1, 5000.00, '2024-01-20'),
(2, 4500.00, '2024-01-25'),
(3, 4000.00, '2024-02-05'),
(4, 5200.00, '2024-02-12'),
(5, 3000.00, '2024-03-10'),
(6, 3500.00, '2024-03-20'),
(7, 4800.00, '2024-04-10'),
(8, 4100.00, '2024-04-25'),
(9, 2900.00, '2024-05-05'),
(10, 3400.00, '2024-05-15'),
(4, 5000.00, '2025-01-20'),
(6, 4500.00, '2025-01-25')
--update Payments set payment_date='2025-01-20' where payment_id=12
--update Payments set amount='2000' where payment_id=12
select * from Students
select * from Teacher
select * from Courses
select * from Enrollments
select * from Payments

-- Tasks 2: Select, Where, Between, AND, LIKE:
--1
insert into Students 
values ('John', 'Doe', '1995-08-15', 'john.doe@example.com', '1234567890')
--2
insert into Enrollments (student_id, course_id, enrollment_date)
values (11, 1, '2024-03-15')
--3
update Teacher set email='diana@example.com' where first_name='diana'
--4
delete from Enrollments where course_id=1 and student_id=11
--5
update Courses set teacher_id=5 where course_id=7
--6
delete from Students where student_id=9
--7
update Payments set amount=50000 where payment_id=4

--Task 3. Aggregate functions, Having, Order By, GroupBy and Joins:
--1
select s.student_id,s.first_name,s.last_name ,sum(amount) [Total amount paid]
from Students s join Payments p
on s.student_id=p.student_id
where s.student_id=4
group by s.student_id,s.first_name,s.last_name
--2
select c.course_id,c.course_name,count(e.student_id) [No of Students enrolled]
from Courses c left join Enrollments e
on c.course_id=e.course_id
group by c.course_id,c.course_name
--3
select s.student_id,s.first_name,s.last_name 
from Students s left join Enrollments e
on s.student_id=e.student_id
where e.student_id is null
--4
select s.first_name,s.last_name,c.course_name
from Students s join Enrollments e 
on s.student_id=e.student_id
join Courses c
on c.course_id=e.course_id
--5
select t.teacher_id,t.first_name,t.last_name,c.course_name
from Teacher t left join Courses c
on t.teacher_id=c.teacher_id
--6
select s.student_id,s.first_name,s.last_name,e.enrollment_date
from Students s join Enrollments e
on s.student_id=e.student_id
join Courses c 
on e.course_id=c.course_id 
where c.course_id=2
--7
select s.first_name,s.last_name 
from Students s left join Payments p
on p.student_id=s.student_id
where p.payment_id is null
--8
select c.course_id,c.course_name 
from Courses c left join Enrollments e
on c.course_id=e.course_id
where e.enrollment_id is null
--9
--inserting into enrollement cause no students have enrolled more than 1 course
insert into Enrollments values(8, 1, '2025-04-15')
--now will get output
select s.student_id,s.first_name,s.last_name,count(e.course_id) [Courses enrolled]
from Students s join Enrollments e
on s.student_id=e.student_id
group by s.student_id,s.first_name,s.last_name 
having count(e.course_id) >1
--or
--SELECT DISTINCT s.student_id, s.first_name, s.last_name FROM Students s JOIN Enrollments e1 ON s.student_id = e1.student_id JOIN Enrollments e2 ON s.student_id = e2.student_id WHERE e1.course_id <> e2.course_id;
--10
select t.teacher_id,t.first_name,t.last_name
from Teacher t left join Courses c
on t.teacher_id=c.teacher_id
where c.teacher_id is null

--Task 4. Subquery and its type:
--1
select c.course_id,c.course_name,avg(e.co) as [Average Students enrolled]
from  
(select course_id,count(student_id) co from Enrollments group by course_id) as e
right join Courses c
on c.course_id=e.course_id
group by c.course_id,c.course_name
--2
select s.student_id,s.first_name,s.last_name,p1.amount from Students s join Payments p1
on p1.student_id=s.student_id
where p1.amount=(select max(amount) from Payments )
--3
select course_id, course_name
from Courses
where course_id = 
(select top 1 course_id from Enrollments
group by course_id
order by COUNT(student_id) desc)
--4
select t.teacher_id,t.first_name,t.last_name,
(select sum(p.amount) from Payments p join Enrollments e
on  e.student_id=p.student_id join Courses c
on c.course_id=e.course_id
where c.teacher_id=t.teacher_id) as TotalPayment from teacher t
--5
select e.student_id,s.first_name,s.last_name from Enrollments e join Students s
on e.student_id=s.student_id
 group by e.student_id,s.first_name,s.last_name 
 having count(e.course_id)=(select COUNT(c.course_id) from Courses c)
--6
select t1.teacher_id,t1.first_name,t1.last_name  from teacher t1
where t1.teacher_id not in(select t.teacher_id from Teacher t join Courses c
on t.teacher_id=c.teacher_id)
--7
select avg(s.age) [Average age] from 
(select DATEDIFF(year,date_of_birth,getdate()) [age] from Students) s
--8
select c.course_id,c.course_name from Courses c
where course_id not in (select e.course_id from Enrollments e group by e.course_id) 
--9
select p.student_id,c.course_id,c.course_name,sum(p.amount)[Total payments] from Payments p join Enrollments e 
on p.student_id=e.student_id
join Courses c
on c.course_id=e.course_id
group by c.course_id,c.course_name,p.student_id
order by p.student_id
--10
select s.student_id,s.first_name,s.last_name,count(p.payment_id) [No of Payments made] 
from Students s inner join Payments p
on s.student_id=p.student_id
group by s.student_id,s.first_name,s.last_name
having count(p.payment_id)>1
--11
select s.student_id,s.first_name,s.last_name,sum(p.amount) [Amount paid] 
from Students s join Payments p 
on p.student_id=s.student_id
group by s.student_id,s.first_name,s.last_name
--12
select c.course_id,c.course_name,count(e.course_id) [Students enrolled] 
from Courses c left join Enrollments e
on e.course_id=c.course_id
group by c.course_id,c.course_name
--13
select s.student_id,s.first_name,s.last_name,avg(amount) [Average amount paid] 
from Payments p join Students s
on p.student_id=s.student_id
group by s.student_id,s.first_name,s.last_name
