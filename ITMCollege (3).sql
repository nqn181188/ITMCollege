CREATE DATABASE ITMCollege
GO
USE ITMCollege
GO
CREATE TABLE Streams
(
	StreamID int identity primary key,
	StreamName varchar(50) not null,
)
GO
CREATE TABLE Fields
(
	FieldID int identity primary key,
	FieldName varchar(50) not null,
	StreamID int foreign key references Streams(StreamID) not null
)
GO
CREATE TABLE Courses
(
	CourseID int identity primary key,
	CourseName varchar(50) not null,
	[Description] text,
	StreamID int foreign key references Streams(StreamID) not null,
	FieldID int foreign key references Fields(FieldID) not null,
	[Image] varchar(max),
)
GO
CREATE TABLE SpeSubjects
(
	SubjectID int identity primary key,
	SubjectName varchar(50) not null,
	FieldID int foreign key references Fields(FieldID) not null,
)
GO
CREATE TABLE OpSubjects
(
	SubjectID int identity primary key,
	SubjectName varchar(50) not null,
)
GO
CREATE TABLE Admissions
(
	AdmissionID bigint identity primary key,
	RegNum varchar(10) not null unique,
	FullName varchar(50) not null,
	FatherName varchar(50) not null,
	MotherName varchar(50) not null,
	Gender bit not null,
	DateOfBirth datetime not null,
	ResAddress varchar(255) not null,
	PerAddress varchar(255) not null,
	StreamID int foreign key references Streams(StreamID) not null,
	FieldID int foreign key references Fields(FieldID) not null,
	Email varchar(255) not null,
	Sport varchar(255) not null,
	ExUniversity varchar(100),
	ExEnrollNum varchar(20),
	ExCenter varchar(50),
	ExStream varchar(50),
	ExField varchar(50),
	ExMarks decimal,
	ExOutOfDate varchar(10),
	ExClass varchar(10),
)
GO
CREATE TABLE Registrations
(
	RegistrationID bigint identity primary key,
	RegNum varchar(10) foreign key references Admissions(RegNum) not null,
	[Image] varchar(max),
	SpeSubjectID int foreign key references SpeSubjects(SubjectID),
	OpSubjectID int foreign key references OpSubjects(SubjectID),
	EmergencyName varchar(50),
	EmergencyAddress varchar(255),
	EmergencyPhone varchar(3),
)
GO
CREATE TABLE Departments
(
	DepID int identity primary key,
	DepName varchar(50) not null,
	[Description] text,
	[Image] varchar(max)
)
GO
CREATE TABLE Facultys
(
	FacultyID int identity primary key,
	FalcultyName varchar(50) not null,
	DOB datetime not null,
	Degree varchar(10) not null,
	DepID int foreign key references Departments(DepID) not null,
	[Image] varchar(max),
)
GO
CREATE TABLE Facilities
(
	FacilityID int identity primary key,
	FacilityName varchar(50) not null,
	IsActive bit not null,
	[Image] varchar(max),
)
GO
CREATE TABLE Feedbacks
(
	FeedbackID int identity primary key,
	FirstName varchar(50) not null,
	LastName varchar(50) not null,
	Email varchar(255) not null,
	[Subject] varchar(max) not null,
	[Message] text not null,
)
GO
CREATE TABLE Accounts
(
	AccountID int identity primary key,
	Fullname varchar(50) not null,
	Username varchar(50) not null,
	[Password] varchar(50) not null,
	[Role] tinyint not null,
	IsActive bit not null,
)

INSERT into Fields VALUES ( N'Computer Engineering', 1)
INSERT into Fields VALUES ( N'Mobile Developement', 1)
INSERT into Fields VALUES ( N'Information Technology', 1)
INSERT into Fields VALUES ( N'Software Engineering', 1)
INSERT into Fields VALUES ( N'Business Administration', 2)
INSERT into Fields VALUES ( N'Economics', 2)
INSERT into Fields VALUES ( N'Finance', 2)
INSERT into Fields VALUES ( N'Marketing', 2)
INSERT into Fields VALUES ( N'Child Developement', 3)
INSERT into Fields VALUES ( N'Education', 3)
INSERT into Fields VALUES ( N'Higher Education', 3)
INSERT into Fields VALUES ( N'Online Teaching', 3)
INSERT into Fields VALUES ( N'Biomedical Engineering', 4)
INSERT into Fields VALUES ( N'Engineering Management', 4)
INSERT into Fields VALUES ( N'Automotive Engineering', 4)
INSERT into Fields VALUES ( N'Criminal Justice', 5)
INSERT into Fields VALUES ( N'Cyber Security', 5)
INSERT into Fields VALUES ( N'Homeland Security', 5)
INSERT into Fields VALUES ( N'Legal Studies', 5)
INSERT into Fields VALUES ( N'Nursing', 6)
INSERT into Fields VALUES ( N'Human Services', 6)
INSERT into Fields VALUES ( N'Public Heath', 6)
INSERT into Fields VALUES ( N'Health Education', 6)
INSERT into Fields VALUES ( N'Art & Art History', 7)
INSERT into Fields VALUES ( N'Film', 7)
INSERT into Fields VALUES ( N'Multimedia Design', 7)

INSERT into streams VALUES ( N'Computers & Technology')
INSERT into streams VALUES ( N'Business & Management')
INSERT into streams VALUES ( N'Education & Teaching')
INSERT into streams VALUES ( N'Science & Engineering')
INSERT into streams VALUES ( N'Criminal Justice & Legal')
INSERT into streams VALUES ( N'Nursing & Healthcare')
INSERT into streams VALUES ( N'Art & Design')




INSERT into courses VALUES ( N'Software Engineering', N'Start date : 10/3//2022. Duration : 4 Years. Fee : 40000USD',1,1,'')
INSERT into courses VALUES ( N'Finance', N'Start date : 3/3//2022. Duration : 4 Years. Fee : 45000USD',2,7,'')
INSERT into courses VALUES ( N'Higher Education', N'Start date : 01/3//2022. Duration : 3.5 Years. Fee : 40000USD',3,11,'')
INSERT into courses VALUES ( N'Automotive Engineering', N'Start date : 06/3//2022. Duration : 4.5 Years. Fee : 50000USD',4,15,'')
INSERT into courses VALUES ( N'Homeland Security', N'Start date : 09/3//2022. Duration : 4 Years. Fee : 40000USD',5,18,'')
INSERT into courses VALUES ( N'Nursing', N'Start date : 03/3//2022. Duration : 4 Years. Fee : 40000USD',6,20,'')
INSERT into courses VALUES ( N'Film', N'Start date : 02/3//2022. Duration : 4 Years. Fee : 44000USD',7,25,'')
INSERT into courses VALUES ( N'Marketing', N'Start date : 11/3//2022. Duration : 4 Years. Fee : 50000USD',2,8,'')
INSERT into courses VALUES ( N'Software Engineering', N'Start date : 15/3//2022. Duration : 4 Years. Fee : 55000USD',1,4,'')
INSERT into courses VALUES ( N'Education', N'Start date : 19/3//2022. Duration : 4 Years. Fee : 42000USD',3,10,'')

Insert into Departments VALUES ( N'Computers & Technology', N'Computer technology involves expanding existing computer capacities.',N'Images/Department/department1.jpg')
Insert into Departments VALUES ( N'Business & Management', N'Business management rule #1 is delegation, assign the best qualified people to each position and trust your staff to do the work instead of trying to do everything yourself.',N'Images/Department/department2.jpg')
Insert into Departments VALUES ( N'Education & Teaching', N'Teacher education or teacher training refers to the policies, procedures, and provision designed to equip (prospective) teachers with the knowledge, attitudes, behaviors, and skills they require to perform their tasks effectively in the classroom, school, and wider community', N'Images/Department/department3.jpg')
Insert into Departments VALUES ( N'Nursing & Healthcare', N'Nursing is a profession within the health care sector focused on the care of individuals, families, and communities so they may attain, maintain, or recover optimal health and quality of life',N'Images/Department/department4.jpg')
Insert into Departments VALUES ( N'Science & Engineering', N'Science and Engineering A provides an international medium for the publication of theoretical and experimental studies related to the load-bearing capacity of materials as influenced by their basic properties, processing history, microstructure and operating environment',N'Images/Department/department5.jpg')
Insert into Departments VALUES ( N'Criminal Justice & Legal', N'Criminal justice is the delivery of justice to those who have been accused of committing crimes. ',N'Images/Department/department6.jpg')
Insert into Departments VALUES ( N'Art & Design', N'The difference between art and design has long been a running debate',N'Images/Department/department7.jpg')

SET DATEFORMAT DMY
Insert into Facultys VALUES (N'Jacke Masito',N'22/02/1969',N'Advisor',1,N'Images/Faculty/faculty1.jpg')
Insert into Facultys VALUES (N'Robert Mike',N'23/05/1970',N'Master',1,N'Images/Faculty/faculty2.jpg')
Insert into Facultys VALUES (N'John Doe',N'23/05/1972',N'Master',2,N'Images/Faculty/faculty3.jpg')
Insert into Facultys VALUES (N'Devid Cobom',N'23/05/1989',N'Master',3,N'Images/Faculty/faculty4.jpg')
Insert into Facultys VALUES (N'Martin',N'12/05/1998',N'Advisor',4,N'Images/Faculty/faculty5.jpg')
Insert into Facultys VALUES (N'Mark Mike',N'12/02/1994',N'Master',4,N'Images/Faculty/faculty6.jpg')
Insert into Facultys VALUES (N'Duc Nam',N'28/07/2002',N'Advisor',7,N'Images/Faculty/faculty7.jpg')
Insert into Facultys VALUES (N'Joe Eliza',N'05/05/1994',N'Master',5,N'Images/Faculty/faculty8.jpg')
Insert into Facultys VALUES (N'Katalyna',N'12/03/1992',N'Advisor',5,N'Images/Faculty/faculty9.jpg')
Insert into Facultys VALUES (N'Rowan',N'25/05/1979',N'Master',6,N'Images/Faculty/faculty10.jpg')
Insert into Facultys VALUES (N'Wanna Bike',N'03/05/1989',N'Advisor',6,N'Images/Faculty/faculty11.jpg')
Insert into Facultys VALUES (N'Dieter',N'04/08/1992',N'Master',7,N'Images/Faculty/faculty12.jpg')
