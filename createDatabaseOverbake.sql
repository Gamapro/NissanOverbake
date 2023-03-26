drop database if exists overBake;
create database overBake;
use overBake;

drop table if exists users;
drop table if exists userTypes;
drop table if exists plcLogs;
drop table if exists logs;

create table userTypes(
	id int not null primary key,
    userType varchar(50) not null
);

insert into userTypes(id, userType) Values (1, "User");
insert into userTypes(id, userType) Values (2, "Admin");

create table users(
	id int not null auto_increment primary key,
    user VARCHAR(50) not null,
    password VARCHAR(50) not null,
    name VARCHAR(100) not null,
    lastName VARCHAR(100) not null,
    userTypeId int not null,
    token varchar(100) not null,
    foreign key (userTypeId) references userTypes(Id)
);

insert into users(user, password, name, lastName, userTypeId, token) Values ('user', 'test', "Luis", "Perez", 1, "1bbd31d7-6783-4172-94f8-83d188d656f4");
insert into users(user, password, name, lastName, userTypeId, token) Values ('admin', '1234', "Angel", "Chavez", 2, "	062a19bf-c286-4c8b-945b-5c4716c5ba80");
insert into users(user, password, name, lastName, userTypeId, token) Values ('qw', 'qw', "Luis", "Perez", 2, "1bbd31d7-6783-4172-94f8-83d188d656f4");
insert into users(user, password, name, lastName, userTypeId, token) Values ('as', 'as', "Angel", "Chavez", 1, "	062a19bf-c286-4c8b-945b-5c4716c5ba80");

drop table if exists plcs;

create table plcs(
	id int not null primary key,
    name varchar(50) not null,
    enabled bool default false
);

insert into plcs(id, name, enabled) values (1, "top-coat-1", 1);
insert into plcs(id, name, enabled) values (2, "top-coat-2", 0);
insert into plcs(id, name, enabled) values (3, "top-coat-3", 1);
insert into plcs(id, name, enabled) values (4, "top-coat-4", 0);

-- insert into plcs(id, name, enabled) values (5, "top-coat-prueba", 0);
-- update plcs set enabled=0 where id=5;
-- delete from plcs where id=5;

drop table if exists plcLogs;

create table plcLogs(
	id int not null auto_increment primary key,
    time datetime,
    plcId int not null,
    message varchar(70),
    foreign key (plcId) references plcs(id)
);

insert into plcLogs(time, plcId, message) values('2022-01-01 00:00:01', 1, 'Mensaje de prueba 1');

drop table if exists logs;

create table logs(
	id int not null auto_increment primary key,
    time datetime,
    plcId int not null,
    serialNumber varchar(50),
    status varchar(10),
    message varchar(50),
    foreign key (plcId) references plcs(id)
);	

insert into logs(time, plcId, serialNumber, status, message) values('2022-01-01 00:00:01', 1, '12345', 'OK', 'Mensaje de prueba 1');
insert into logs(time, plcId, serialNumber, status, message) values('2022-01-01 00:00:02', 2, '23456', 'OK', 'Mensaje de prueba 2');
insert into logs(time, plcId, serialNumber, status, message) values('2022-01-01 00:00:03', 1, '34567', 'ERROR', 'Mensaje de prueba 3');
insert into logs(time, plcId, serialNumber, status, message) values('2022-01-01 00:00:04', 2, '45678', 'OK', 'Mensaje de prueba 4');
insert into logs(time, plcId, serialNumber, status, message) values('2022-01-01 00:00:05', 1, '56789', 'ERROR', 'Mensaje de prueba 5');
insert into logs(time, plcId, serialNumber, status, message) values('2022-01-01 00:00:06', 2, '67890', 'OK', 'Mensaje de prueba 6');
insert into logs(time, plcId, serialNumber, status, message) values('2022-01-01 00:00:07', 3, '78901', 'ERROR', 'Mensaje de prueba 7');
insert into logs(time, plcId, serialNumber, status, message) values('2022-01-01 00:00:08', 2, '89012', 'OK', 'Mensaje de prueba 8');
insert into logs(time, plcId, serialNumber, status, message) values('2022-01-01 00:00:09', 4, '90123', 'ERROR', 'Mensaje de prueba 9');
insert into logs(time, plcId, serialNumber, status, message) values('2022-01-01 00:00:10', 4, '01234', 'OK', 'Mensaje de prueba 10');
insert into logs(time, plcId, serialNumber, status, message) values('2022-01-01 00:00:11', 4, '12345', 'OK', 'Mensaje de prueba 11');


select users.user, userTypes.UserType, users.Token from users, userTypes where users.user = "user" and users.password = "Test" and usertypes.id = users.userTypeId;