use library;
--Login table--
create table loginTable(
id int Not null identity(1,1) primary key,
username varchar(150) not null,
pass varchar(150) not null
)
insert into loginTable(username,pass) values ('ouassima','1234');
select * from loginTable;
-- Sign up table--
create table signUpTable(
id int Not null identity(1,1) primary key,
username varchar(150) not null,
pass varchar(150) not null,
email varchar(150) not null
)
select* from signUpTable;
Delete from signUpTable;
create table NewBook(
bid int NOT NULL IDENTITY(1,1) primary KEY,
bName varchar(250) not null,
bAuthor varchar(250) not null,
bPubl varchar(250) not null,
bPDate varchar(250) not null,
bPrice bigint not null,
bQuan bigint not null
)
select * from NewBook
Delete from NewBook;
insert into NewBook(bName,bAuthor,bPubl,bPDate,bPrice,bQuan) values ('ouassima','1234');

create table NewStudent(
stuid int NOT NULL IDENTITY(1,1) primary KEY,
sname varchar(250) not null,
enroll varchar(250) not null,
dep varchar(250) not null,
sem varchar(250) not null,
contact bigint not null,
email varchar(250) not null
)
stuid,sname,enroll,dep,sem,contact,email
select * from NewStudent
delete from NewStudent;
//issue 
create table IRBook(
id int NOT NULL IDENTITY(1,1) primary key,
std_enroll varchar(250) not null,
std_name  varchar(250) not null,
std_dep  varchar(250) not null,
std_sem  varchar(250) not null,
std_contact  bigint not null,
std_email varchar(250) not null,
book_name  varchar(1250) not null,
book_issue_date  varchar(250) not null,
book_return_date  varchar(250) 
);
select * from IRBook;
delete from IRBook;