alter table Users add TryesCount int;
alter table Users add LastLogonDate datetime;
alter table Users add DateOfBlock datetime;

create table VisitHistory (
 HistID int primary key identity(1,1)
,UserID int
,IP varchar(50)
,ClientAgent varchar (50)
,ClientDevice varchar (50)
)

create table BlockHistory (
 BlockUserID int
,BlockUnblockDate datetime
,BlockUnblockReason varchar(500)
,BlockUnblockInitiator int
)