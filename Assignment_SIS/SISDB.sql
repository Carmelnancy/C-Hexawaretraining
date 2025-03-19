-- Task 1. Database Design:

create database SISDB

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

create table Courses
(
course_id int primary key identity(1,1),
course_name varchar(100) not null,
credits int not null,
teacher_id int foreign key references Teacher(teacher_id) on delete cascade 
)

create table Enrollments
(
enrollment_id int primary key identity(1,1),
student_id int foreign key references Students(student_id) on delete cascade,
course_id int foreign key references Courses(course_id) on delete cascade,
enrollment_date date
)

create table Teacher
(
teacher_id int primary key identity(1,1),
first_name varchar(50) not null,
laast_name varchar(50) not null,
email varchar(50) unique not null
)

create table Payments
(
payment_id int primary key identity(1,1),
student_id int foreign key references Students(student_id) on delete cascade,
amount decimal(10,2) not null,
payment_date date
)

insert into Students values
('Jack', 'Drill', '2000-01-15', 'jack.dril@example.com', '9876543210'),
('Jane', 'Smith', '1999-05-22', 'jane.smith@example.com', '9123456789'),
('Michael', 'Johnson', '2001-03-11', 'michael.johnson@example.com', '9988776655'),
('Emily', 'Davis', '2000-09-30', 'emily.davis@example.com', '9876543201'),
('Daniel', 'Brown', '2002-07-14', 'daniel.brown@example.com', '9123456798'),
('Olivia', 'Wilson', '2001-11-05', 'olivia.wilson@example.com', '9786543210'),
('Matthew', 'Taylor', '2003-02-28', 'matthew.taylor@example.com', '9988776644'),
('Sophia', 'Anderson', '2000-08-18', 'sophia.anderson@example.com', '9876543220'),
('James', 'Martinez', '2002-04-04', 'james.martinez@example.com', '9123456788'),
('Isabella', 'Garcia', '2001-06-25', 'isabella.garcia@example.com', '9786543211')

select * from Students

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

select * from Courses

insert into Enrollments values
(1, 1, '2024-01-10'),
(2, 2, '2024-01-15'),
(3, 3, '2024-02-01'),
(4, 4, '2024-02-10'),
(5, 5, '2024-03-05'),
(6, 6, '2024-03-12'),
(7, 7, '2024-04-01'),
(8, 8, '2024-04-15'),
(9, 9, '2024-05-01'),
(10, 10, '2024-05-10')
insert into Enrollments values
(6, 1, '2025-01-10')
select * from Enrollments

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

select * from Teacher

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
(10, 3400.00, '2024-05-15')

select * from Payments

-- Tasks 2: Select, Where, Between, AND, LIKE:
--1
insert into Students 
values ('John', 'Doe', '1995-08-15', 'john.doe@example.com', '1234567890');
--2
insert into Enrollments (student_id, course_id, enrollment_date)
values (11, 1, '2024-03-15');
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
select s.student_id,s.first_name,s.last_name ,sum(amount)
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
select t.teacher_id,t.first_name,t.laast_name,c.course_name
from Teacher t left join Courses c
on t.teacher_id=c.teacher_id
--6
select s.student_id,s.first_name,s.last_name,e.enrollment_date
from Students s join Enrollments e
on s.student_id=e.student_id
join Courses c 
on e.course_id=c.course_id 
where c.course_id=4
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
select s.student_id,s.first_name,s.last_name,count(e.course_id) [Courses enrolled]
from Students s join Enrollments e
on s.student_id=e.student_id
group by s.student_id,s.first_name,s.last_name 
having count(e.course_id) >1
--or
SELECT DISTINCT s.student_id, s.first_name, s.last_name FROM Students s JOIN Enrollments e1 ON s.student_id = e1.student_id JOIN Enrollments e2 ON s.student_id = e2.student_id WHERE e1.course_id <> e2.course_id;
--10
select t.teacher_id,t.first_name,t.laast_name
from Teacher t left join Courses c
on t.teacher_id=c.teacher_id
where c.teacher_id is null

--Task 4. Subquery and its type:
