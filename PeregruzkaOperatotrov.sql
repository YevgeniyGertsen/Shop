create database Peregruzka;

create table Tovar
(
IdTovar int identity(1,1) not null primary key
, Naimenovanie varchar(50) not null
, IdProizvoditel int not null FOREIGN KEY REFERENCES Proizvoditeli(IdProizvoditel)
, IdIzmereniya int not null FOREIGN KEY REFERENCES Izmereniya(IdIzmereniya)
, Price money
)

create table Izmereniya
(
IdIzmereniya int identity(1,1) not null primary key 
, EdinicaIzmereniya varchar(2)
)

create table Proizvoditeli
(
IdProizvoditel int identity (1,1) not null primary key 
, NameProizvoditelya varchar(100)
)