
--SET 3

use sqlhandson

--1
select distinct deptno from EMP
--2
select ename, sal from EMP 
where sal>1500 and (deptno in (10,30))
--3
select ename,job,sal from emp 
where job in ('manager','analyst') and sal not in (1000,3000,5000)
--4
select ename,sal,comm from emp
where comm>sal*1.10
--5
select ename from emp 
where ename like '%l%l%' and (deptno =30 or mgr_id=7782)
--6
--select ename from EMP where
--7
select d.dname,e.ename
from dept d join EMP e
on d.deptno=e.deptno
order by d.dname ,e.ename desc
--8
select ename,round(datediff(MONTH,hiredate,GETDATE())/12.0,2) as Experience
from emp
where ename='miller'
--9
select * from EMP
where ename like '_____%'
--or
--select * from EMP where LEN(ename) >= 5
--10
create table emp10
(empno int ,
ename varchar(50)
)
insert into emp10 (empno,ename) select empno,ename from EMP where deptno=10
select * from emp10