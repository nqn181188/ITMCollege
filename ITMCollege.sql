Drop DATABASE ITMCollege
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
	StreamID int not null,
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
	Sport varchar(255) null,
	ExUniversity varchar(100),
	ExEnrollNum varchar(20),
	ExCenter varchar(50),
	ExStream varchar(50),
	ExField varchar(50),
	ExMarks decimal,
	ExOutOfDate varchar(10),
	ExClass varchar(10),
	[Status] tinyint default (0),
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
	EmergencyPhone varchar(10),
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

INSERT into streams VALUES ( N'Computers & Technology')
INSERT into streams VALUES ( N'Business & Management')
INSERT into streams VALUES ( N'Education & Teaching')
INSERT into streams VALUES ( N'Science & Engineering')
INSERT into streams VALUES ( N'Criminal Justice & Legal')
INSERT into streams VALUES ( N'Nursing & Healthcare')
INSERT into streams VALUES ( N'Art & Design')



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

INSERT into courses VALUES ( N'Software Engineering', N'Start date : 10/3/2022. Duration : 4 Years. Fee : 40000USD',1,1,'Images/Course/course_8.jpeg')
INSERT into courses VALUES ( N'Finance', N'Start date : 3/3/2022. Duration : 4 Years. Fee : 45000USD',2,7,'Images/Course/course_19.jpg')
INSERT into courses VALUES ( N'Higher Education', N'Start date : 01/3/2022. Duration : 3.5 Years. Fee : 40000USD',3,11,'Images/Course/course_9.jpg')
INSERT into courses VALUES ( N'Automotive Engineering', N'Start date : 06/3/2022. Duration : 4.5 Years. Fee : 50000USD',4,15,'Images/Course/course_11.jpg')
INSERT into courses VALUES ( N'Homeland Security', N'Start date : 09/3/2022. Duration : 4 Years. Fee : 40000USD',5,18,'Images/Course/course_18.jpg')
INSERT into courses VALUES ( N'Nursing', N'Start date : 03/3/2022. Duration : 4 Years. Fee : 40000USD',6,20,'Images/Course/course_17.jpg')
INSERT into courses VALUES ( N'Film', N'Start date : 02/3/2022. Duration : 4 Years. Fee : 44000USD',7,25,'Images/Course/course_13.jpg')
INSERT into courses VALUES ( N'Marketing', N'Start date : 11/3/2022. Duration : 4 Years. Fee : 50000USD',2,8,'Images/Course/course_5.jpg')
INSERT into courses VALUES ( N'Software Engineering', N'Start date : 15/3/2022. Duration : 4 Years. Fee : 55000USD',1,4,'Images/Course/course_6.jpg')
INSERT into courses VALUES ( N'Education', N'Start date : 19/3/2022. Duration : 4 Years. Fee : 42000USD',3,10,'Images/Course/course_3.jpg')


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


Insert into Facilities VALUES (N'Pools',1,N'Images/Facility/facilities1.jpg')
Insert into Facilities VALUES (N'Canteen',1,N'Images/Facility/facilities2.jpg')
Insert into Facilities VALUES (N'Gyms',1,N'Images/Facility/facilities3.jpg')
Insert into Facilities VALUES (N'Library',1,N'Images/Facility/facilities4.jpg')
Insert into Facilities VALUES (N'Meeting-Hall',1,N'Images/Facility/facilities5.jpg')
Insert into Facilities VALUES (N'Hostel',1,N'Images/Facility/facilities6.jpg')
Insert into Facilities VALUES (N'Administrator Office',1,N'Images/Facility/facilities7.jpg')
Insert into Facilities VALUES (N'Classroom',1,N'Images/Facility/facilities8.jpg')


USE [ITMCollege]
GO
SET IDENTITY_INSERT [dbo].[OpSubjects] ON 

INSERT [dbo].[OpSubjects] ([SubjectID], [SubjectName]) VALUES (2, N'Football')
INSERT [dbo].[OpSubjects] ([SubjectID], [SubjectName]) VALUES (4, N'Basketball')
INSERT [dbo].[OpSubjects] ([SubjectID], [SubjectName]) VALUES (5, N'Dancing')
INSERT [dbo].[OpSubjects] ([SubjectID], [SubjectName]) VALUES (6, N'English')
INSERT [dbo].[OpSubjects] ([SubjectID], [SubjectName]) VALUES (7, N'Taekwondo')
INSERT [dbo].[OpSubjects] ([SubjectID], [SubjectName]) VALUES (8, N'Karatedo')
INSERT [dbo].[OpSubjects] ([SubjectID], [SubjectName]) VALUES (9, N'Music Basic')
INSERT [dbo].[OpSubjects] ([SubjectID], [SubjectName]) VALUES (11, N'Volleyball')
SET IDENTITY_INSERT [dbo].[OpSubjects] OFF
GO

USE [ITMCollege]
GO
SET IDENTITY_INSERT [dbo].[SpeSubjects] ON 

INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (10, N'Art History', 24)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (11, N'Art Practice', 24)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (12, N'Design', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (13, N'Film and Media Studies', 25)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (14, N'Film Production', 25)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (15, N'Machine Design and Industrial Drafting', 15)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (16, N'Electrical Machines and Electronics', 15)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (17, N'Material Science', 15)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (18, N'Manufacturing Process', 15)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (19, N'Fluid Mechanics', 15)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (20, N'Dynamics of Machines', 15)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (21, N'Control Engineering', 15)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (22, N'Component Design', 15)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (23, N'Alternative Fuels and Engines', 15)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (24, N'Vehicle Dynamics Controller', 15)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (25, N'Contemporary Management', 5)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (26, N'Marketing Management', 5)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (27, N'Accounting and Financial Management', 5)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (28, N'Strategic Management', 5)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (29, N'Entrepreneurship Project.', 5)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (30, N'Business Economics', 5)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (31, N'Corporate Finance', 5)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (32, N'Human Resource Management', 5)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (33, N'Leadership - A Critical Perspective', 5)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (34, N'Project Management OR ITC505 Project Management', 5)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (35, N'Mathematics and Physics of Biomedical Engineering', 13)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (36, N'Basic Mechanics for Biomedical Engineering', 13)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (37, N'Bio-fluid Mechanics', 13)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (38, N'Strength of Materials for Biomedical Engineering', 13)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (39, N'Thermodynamics for Biomedical Engineering', 13)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (40, N'Medical Aspects of Electromagnetic Theory', 13)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (41, N'Electrical and Electronic Circuits', 13)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (42, N'Medical Molecular Biology', 13)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (43, N'Basic Biology', 13)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (44, N'Education and Society', 9)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (45, N'The Languages of Children', 9)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (46, N'The Practice of Early Childhood Teaching', 9)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (47, N'The Institutions of Childhood', 9)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (48, N'People Under Three', 9)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (49, N'Relationships and the Practice of Teaching', 9)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (50, N'Living Curriculum', 9)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (51, N' Politics, Policy, and the Profession', 9)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (52, N'Engineering Drawing', 1)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (53, N'Physics', 1)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (54, N'Chemistry', 1)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (55, N'Maths', 1)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (56, N'Basic of Electical', 1)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (57, N'Basic of Electronics', 1)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (58, N'Basic of Computers', 1)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (59, N'.NET framework', 1)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (60, N'Computer Graphics', 1)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (61, N'Multimedia and System Design', 1)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (62, N'Biology', 16)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (63, N'Chemistry', 16)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (64, N'Physics', 16)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (65, N'Psychology', 16)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (66, N'Related Articles', 16)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (67, N'Sociology', 16)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (68, N'Government', 16)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (69, N'Civics', 16)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (70, N'Computer Forensics', 17)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (71, N'Cyber Law', 17)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (72, N'Introduction to Data Mining', 17)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (73, N'Telecommunication Systems', 17)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (74, N'Secure Software Design', 17)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (75, N'Risk Analysis', 17)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (76, N'Trade Economics ', 6)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (77, N'Money and banking', 6)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (78, N'Market Function', 6)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (79, N'Public finance ', 6)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (80, N'Private finance', 6)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (81, N'Welfare Economics', 6)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (82, N'Labor Economics', 6)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (83, N'Development Economics', 6)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (84, N'Statistics', 6)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (85, N'Business Studies', 6)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (86, N'Senior English', 10)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (87, N'Maths A', 10)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (88, N'Agricultural Science', 10)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (89, N'Biology', 10)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (90, N'Chemistry', 10)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (91, N'Earth Science', 10)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (92, N'Physics', 10)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (93, N'Psychology', 10)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (94, N'Marine Science', 10)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (95, N'Basic Mechanics for Biomedical Engineering', 14)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (96, N'Computer Science', 14)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (97, N'Mathematics', 14)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (98, N'Managerial Science', 14)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (99, N'Marketing', 14)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (100, N'Physics and Chemistry', 14)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (101, N'Systems Engineering', 14)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (102, N'Materials Handling and Plant Layout', 14)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (103, N'Film & Media Cultures', 24)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (104, N'History of Film I', 25)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (105, N'History of Film II', 25)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (106, N'Documentary Film', 25)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (107, N'Avant-Garde Film', 25)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (108, N'Genre', 25)
GO
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (109, N'Auteur', 25)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (110, N'National Cinema', 25)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (111, N'Mathematics', 7)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (112, N'Accounting', 7)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (113, N'Economics', 7)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (114, N'Psychology', 7)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (115, N'Technical Writing', 7)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (116, N'Communications', 7)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (117, N'Computer Course', 7)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (118, N'Healthcare Introduction', 23)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (119, N'Psychology and Health', 23)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (120, N'Business and Professional Writing', 23)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (121, N'Physical Science', 23)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (122, N'Computer Applications in Health Education', 23)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (123, N'Statistics', 23)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (124, N'Children’s Health', 23)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (125, N'Health and Disease', 23)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (126, N'Education', 11)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (127, N'Physical', 11)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (128, N'Life Sciences', 11)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (129, N'Humanities', 11)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (130, N'Social Sciences ', 11)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (131, N'Mathematics', 11)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (132, N'Introduction to Psychology', 21)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (133, N'Developmental Psychology', 21)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (134, N'Case Management', 21)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (135, N'Grant Writing', 21)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (136, N'Lifespan Development', 21)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (137, N'Criminology', 18)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (138, N'Criminal justice', 18)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (139, N'Police science', 18)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (140, N'Emergency management', 18)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (141, N'Homeland security', 18)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (142, N'Sociology', 18)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (143, N'Forensic psychology', 18)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (144, N'Engineering Mathematics', 3)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (145, N'Communication Skills', 3)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (146, N'Engineering Physics', 3)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (147, N'Engineering Graphics', 3)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (148, N'Programming in C Language', 3)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (149, N'Object Oriented Programming using C++', 3)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (150, N'Logic Design and Structure', 3)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (151, N'Database Management System', 3)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (152, N'Mechanics of Solids', 3)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (153, N'Data Structure', 3)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (154, N'American politics', 19)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (155, N'Social theory', 19)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (156, N'Legal systems in American society', 19)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (157, N'Constitutional and business law', 19)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (158, N'Legal ethics', 19)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (159, N'Legal writing and research', 19)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (160, N'Java Basic', 2)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (161, N'C# Basic', 2)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (162, N'C++ Basic', 2)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (163, N'Html5 Programming', 2)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (164, N'Graphics Design', 2)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (165, N'UI-Design', 2)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (166, N'Android developement', 2)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (167, N'Objective-C and C++ programming', 2)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (168, N'Mobile media marketing & deployment', 2)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (169, N'Game & simulation programming', 2)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (170, N'Managerial Communications', 8)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (171, N'Macroeconomics', 8)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (172, N'Public Relations', 8)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (173, N'Principles of Marketing', 8)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (174, N'Quantitative Methods', 8)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (175, N'Principles of Finance', 8)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (176, N'Business to Business Marketing', 8)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (177, N'Global Marketing', 8)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (178, N'Multimedia Standards', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (179, N'Business of Graphics', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (180, N'Engineering Multimedia Technologies', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (181, N'Visual Design Fundamentals', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (182, N'Advanced Design and  Rapid Visualization', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (183, N'Digital Imaging Fundamentals', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (184, N'Information Design', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (185, N'Web Design', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (186, N'Web Animation', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (187, N'Advanced Web Design', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (188, N'Responsive Web Design', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (189, N'Media Porfolio', 26)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (190, N'Anatomy', 20)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (191, N'Microbiology', 20)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (192, N'Chemistry', 20)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (193, N'Nutrition', 20)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (194, N'Psychology', 20)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (195, N'Nursing practice and theory', 20)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (196, N'Internet Resources', 12)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (197, N'Computer Skills', 12)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (198, N'Group Work', 12)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (199, N'Online Assessment', 12)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (200, N'Copyrighted Materials', 12)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (201, N'Course Mapping', 12)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (202, N'Primary Knowledge', 12)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (203, N'Biology', 22)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (204, N'Calculus', 22)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (205, N'English', 22)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (206, N'Environmental health', 22)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (207, N'Health policy and management', 22)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (208, N'Biostatistics', 22)
GO
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (209, N'Computer Programming', 4)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (210, N'Program design', 4)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (211, N'Computer Systems analysis', 4)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (212, N'Mathematics for Computing', 4)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (213, N'Introduction to Software Engineering', 4)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (214, N'Software Requirements & Modeling', 4)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (215, N'Software Design & Construction', 4)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (216, N'Software Testing, Verification, and Validation', 5)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (217, N'Fundamentals of Programming', 4)
INSERT [dbo].[SpeSubjects] ([SubjectID], [SubjectName], [FieldID]) VALUES (218, N'Programming Languages', 4)
SET IDENTITY_INSERT [dbo].[SpeSubjects] OFF
GO


USE [ITMCollege]
GO
SET IDENTITY_INSERT [dbo].[Admissions] ON 

INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10026, N'ST22119993', N'Carolyn Kramer', N'Peter Kramer', N'Julie Kramer', 0, CAST(N'2000-01-22' AS Date), N'2445 3rd Ave S P O Box 34165 MS 31-650 Seattle WA 98134', N'2445 3rd Ave S P O Box 34165 MS 31-650 Seattle WA 98134', 7, 26, N'cjkramer@seattleschools.org', N'Swimming', N'STEPS Schools Coordinator', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10027, N'ST22796538', N'Chanin Kelly Rae', N'Ronnie Rae', N'Carol Rae', 0, CAST(N'2003-02-22' AS Date), N'21st Ave. E Seattle WA 98112', N'21st Ave. E Seattle WA 98112', 6, 20, N'cbkellyrae@seattleschools.org', N'Swimming', N'Meany Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10028, N'ST22705118', N'Ronnie Katz', N'Ted Katz', N'Jennifer Katz', 1, CAST(N'2007-01-22' AS Date), N'8825 Rainier Ave S Seattle WA 98118', N'8825 Rainier Ave S Seattle WA 98118', 1, 1, N'rkatz@gmail.com', N'Football', N'The New School at South Shore', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10029, N'ST22927561', N'Julie Melver', N'Robert Melver', N'Jenny Melver', 0, CAST(N'2012-06-22' AS Date), N'11212 10th Ave SW Seattle WA 98146-2265', N'11212 10th Ave SW Seattle WA 98146-2265', 2, 5, N'melverja@hsd401.org', N'Swimming', N'Cascade Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10030, N'ST22863452', N'Dan Magee', N'Robert Magee', N'Julie Magee', 1, CAST(N'2004-01-22' AS Date), N'1012 SW Trenton St Seattle WA 98106', N'1012 SW Trenton St Seattle WA 98106', 1, 4, N'dwmagee@seattleschools.org', N'Football, Volleyball', N'Highland Park Elementary', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10031, N'ST22293370', N'Tracy Wertman', N'Robert Wertman', N'Halley Wertman', 0, CAST(N'2003-07-22' AS Date), N'3814 E Dear Park-Milan Rd Chattaroy, WA 99003-9733', N'3814 E Dear Park-Milan Rd Chattaroy, WA 99003-9733', 3, 10, N'tracy@gmail.com', N'Tennis', N'Riverside Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10032, N'ST22377826', N'Ellen Boggs', N'Dan Boggs', N'Carol Boggs', 0, CAST(N'2001-05-22' AS Date), N'2901 Falk Rd P O Box 8937 Vancouver WA 98668', N'2901 Falk Rd P O Box 8937 Vancouver WA 98668', 4, 13, N'ellen@gmail.com', N'Swimming', N'Vancouver School District', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10033, N'ST22821819', N'Nguyen Quang Hieu', N'Nguyen Quang Nguyen', N'Doan Ngoc Mi', 1, CAST(N'2005-12-09' AS Date), N'354 Phan Van Tri street, Ward 11, Binh Thanh District, , Ho Chi Minh City', N'354 Phan Van Tri street, Ward 11, Binh Thanh District, , Ho Chi Minh City', 1, 4, N'quanghieu@gmail.com', N'Football', N'Ho Chi Minh City University of Technology', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10034, N'ST22070890', N'Donna Goodwin', N'Robert Goodwin', N'Julia Goodwin', 0, CAST(N'2003-06-22' AS Date), N'13501 NE 28th St P O Box 8910 Vancouver, WA 98668', N'13501 NE 28th St P O Box 8910 Vancouver, WA 98668', 6, 23, N'donna18@gmail.com', N'Swimming', N'Meany Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10035, N'ST22147174', N'Kelly Wallace', N'Kelly Wallace', N'Kelly Wallace', 0, CAST(N'2000-11-22' AS Date), N'1012 SW Trenton St Seattle WA 98106', N'11212 10th Ave SW Seattle WA 98146-2265', 7, 25, N'kelly2020@gmail.com', N'Swimming', N'The New School at South Shore', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10036, N'ST22801075', N'Racie McKee', N'Henry McKee', N'Christina McKee', 0, CAST(N'1998-10-22' AS Date), N'1012 SW Trenton St Seattle WA 98106', N'21st Ave. E Seattle WA 98112', 7, 24, N'racie121@gmail.com', N'Swimming', N'Highland Park Elementary', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10037, N'ST22774779', N'Jennifer Douglas', N'Paul Douglas', N'Kelly Douglas', 1, CAST(N'2004-06-22' AS Date), N'2445 3rd Ave S P O Box 34165 MS 31-650 Seattle WA 98134', N'8825 Rainier Ave S Seattle WA 98118', 7, 26, N'jennifer32@gmail.com', N'Swimming', N'STEPS Schools Coordinator', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10038, N'ST22360091', N'Ted Julius', N'RobertJulius', N'Kelly Julius', 1, CAST(N'2002-06-22' AS Date), N'21st Ave. E Seattle WA 98112', N'21st Ave. E Seattle WA 98112', 7, 26, N'ted@gmail.com', N'Football', N'The New School at South Shore', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10039, N'ST22312138', N'Rick Brown', N'Ted Brown', N'Jennifer Brown', 1, CAST(N'2004-05-02' AS Date), N'1012 SW Trenton St Seattle WA 98106', N'11212 10th Ave SW Seattle WA 98146-2265', 5, 16, N'ted1231@gmail.com', N'Football, Volleyball', N'Meany Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10040, N'ST22821449', N'Larry Carpenter', N'Ted Carpenter', N'Lauriel Carpenter', 1, CAST(N'2001-06-22' AS Date), N'21st Ave. E Seattle WA 98112', N'21st Ave. E Seattle WA 98112', 5, 17, N'larry231@gmail.com', N'Swimming', N'STEPS Schools Coordinator', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10041, N'ST22240571', N'Bob Ochoa', N'Ted Ochoa', N'Halley Ochoa', 1, CAST(N'1993-06-22' AS Date), N'3814 E Dear Park-Milan Rd Chattaroy, WA 99003-9733', N'3814 E Dear Park-Milan Rd Chattaroy, WA 99003-9733', 5, 18, N'bod2313@gmail.com', N'Football', N'Riverside Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10042, N'ST22563385', N'Chris Magley', N'Ted Magley', N'Jennifer Magley', 1, CAST(N'2001-01-22' AS Date), N'8825 Rainier Ave S Seattle WA 98118', N'8825 Rainier Ave S Seattle WA 98118', 4, 14, N'chris3231@gmail.com', N'Football, Volleyball', N'Riverside Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10043, N'ST22038008', N'Tran Thanh Phong', N'Tran Thanh Nam', N'Nguyen Thi Cuc', 1, CAST(N'2003-07-22' AS Date), N'81, 6th street, Ward 15, Tan Binh district, Ho Chi Minh city', N'81, 6th street, Ward 15, Tan Binh district, Ho Chi Minh city', 4, 14, N'thanhphong@gmail.com', N'Football, Volleyball', N'STEPS Schools Coordinator', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10044, N'ST22663010', N'Nguyen Thi Hai', N'Nguyen Ngoc Minh', N'Tran Mai Vy', 0, CAST(N'2004-06-22' AS Date), N'1012 SW Trenton St Seattle WA 98106', N'21st Ave. E Seattle WA 98112', 4, 15, N'thihai@gmail.com', N'Football, Volleyball', N'Cascade Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10045, N'ST22515540', N'Doan Ngoc Mi', N'Doan Ngoc Thanh', N'Hoang Thi Mai', 0, CAST(N'2003-12-05' AS Date), N'8825 Rainier Ave S Seattle WA 98118', N'11212 10th Ave SW Seattle WA 98146-2265', 3, 9, N'ngocmi@gmail.com', N'Swimming', N'Highland Park Elementary', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10046, N'ST22435970', N'Maria Matig', N'Robert Matig', N'Jenny Matig', 0, CAST(N'2004-08-19' AS Date), N'3814 E Dear Park-Milan Rd Chattaroy, WA 99003-9733', N'11212 10th Ave SW Seattle WA 98146-2265', 3, 11, N'maria@gmail.com', N'Swimming', N'Meany Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10047, N'ST22008370', N'Elizabet Espinoza', N'Ted Espinoza', N'Lauriel Espinoza', 1, CAST(N'2004-03-10' AS Date), N'3814 E Dear Park-Milan Rd Chattaroy, WA 99003-9733', N'11212 10th Ave SW Seattle WA 98146-2265', 3, 12, N'eliza32@gmail.com', N'Swimming', N'Highland Park Elementary', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10048, N'ST22177445', N'Leroy J Miller', N'Paul Miller', N'Christina Miller', 1, CAST(N'2000-12-04' AS Date), N'1012 SW Trenton St Seattle WA 98106', N'11212 10th Ave SW Seattle WA 98146-2265', 2, 6, N'leroy99@gmail.com', N'Football, Volleyball', N'The New School at South Shore', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10049, N'ST22607626', N'Nicholas J Cooke', N'Ted Cooke', N'Jenny Cooke', 1, CAST(N'2003-07-17' AS Date), N'3814 E Dear Park-Milan Rd Chattaroy, WA 99003-9733', N'21st Ave. E Seattle WA 98112', 2, 5, N'nicholas3@gmail.com', N'Swimming', N'Riverside Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10050, N'ST22006960', N'Carl John', N'Nicholas John', N'Carol John', 1, CAST(N'2002-11-12' AS Date), N'21st Ave. E Seattle WA 98112', N'11212 10th Ave SW Seattle WA 98146-2265', 2, 7, N'cart242@gmail.com', N'Football, Volleyball', N'Highland Park Elementary', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10051, N'ST22838816', N'Carlos Juarez', N'Ted Juarez', N'Taylor Juarez', 1, CAST(N'2005-10-02' AS Date), N'21st Ave. E Seattle WA 98112', N'11212 10th Ave SW Seattle WA 98146-2265', 2, 8, N'calort@gmail.com', N'Football, Volleyball', N'The New School at South Shore', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10052, N'ST22576118', N'Moussa N Diaye', N'Teddy Diaye', N'Jenny Diaye', 1, CAST(N'2009-05-09' AS Date), N'3814 E Dear Park-Milan Rd Chattaroy, WA 99003-9733', N'11212 10th Ave SW Seattle WA 98146-2265', 1, 1, N'moussa@gmail.com', N'Football, Volleyball', N'The New School at South Shore', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10053, N'ST22481350', N'Hugh N Aeyaert', N'Ted Aeyaert', N'Lauriel Aeyaert', 1, CAST(N'2003-05-16' AS Date), N'8825 Rainier Ave S Seattle WA 98118', N'3814 E Dear Park-Milan Rd Chattaroy, WA 99003-9733', 1, 3, N'hugh312@gmail.com', N'Swimming', N'The New School at South Shore', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10054, N'ST22164067', N'Lindsay Reigrut', N'Michael Reigrut', N'Carol Reigrut', 0, CAST(N'2003-05-13' AS Date), N'21st Ave. E Seattle WA 98112', N'2445 3rd Ave S P O Box 34165 MS 31-650 Seattle WA 98134', 1, 3, N'lindsay@gmail.com', N'Tennis', N'STEPS Schools Coordinator', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10055, N'ST22661552', N'Wilheml Beckman', N'Michael Beckman', N'Lindsay Beckman', 1, CAST(N'2002-07-11' AS Date), N'1012 SW Trenton St Seattle WA 98106', N'1012 SW Trenton St Seattle WA 98106', 1, 4, N'wilhelm22@gmail.com', N'Football, Volleyball', N'Riverside Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10056, N'ST22143828', N'Joseph Span', N'Wilheml Span', N'Carol Span', 1, CAST(N'2001-10-23' AS Date), N'21st Ave. E Seattle WA 98112', N'11212 10th Ave SW Seattle WA 98146-2265', 1, 4, N'joseph22@gmail.com', N'Swimming', N'Cascade Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10057, N'ST22026880', N'Charles Niemann', N'George Niemann', N'Lauriel Niemann', 1, CAST(N'2004-07-07' AS Date), N'2445 3rd Ave S P O Box 34165 MS 31-650 Seattle WA 98134', N'8825 Rainier Ave S Seattle WA 98118', 7, 24, N'charles22@gmail.com', N'Football, Volleyball', N'Cascade Middle School', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10058, N'ST22553584', N'Lyle McCracken', N'Ted McCracken', N'Jenny McCracken', 0, CAST(N'1998-04-04' AS Date), N'3814 E Dear Park-Milan Rd Chattaroy, WA 99003-9733', N'21st Ave. E Seattle WA 98112', 6, 20, N'lyle2222@gmail.com', N'Tennis', N'The New School at South Shore', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10059, N'ST22253122', N'Elmer Fennern', N'Michael Fennern', N'Lindsay Fennern', 1, CAST(N'2002-09-16' AS Date), N'3814 E Dear Park-Milan Rd Chattaroy, WA 99003-9733', N'8825 Rainier Ave S Seattle WA 98118', 7, 25, N'ermer123@gmail.com', N'Swimming', N'Highland Park Elementary', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Admissions] ([AdmissionID], [RegNum], [FullName], [FatherName], [MotherName], [Gender], [DateOfBirth], [ResAddress], [PerAddress], [StreamID], [FieldID], [Email], [Sport], [ExUniversity], [ExEnrollNum], [ExCenter], [ExStream], [ExField], [ExMarks], [ExOutOfDate], [ExClass], [Status]) VALUES (10060, N'ST22437042', N'Gabriel Godinez', N'Peter Godinez', N'Halley Godinez', 1, CAST(N'2004-03-04' AS Date), N'2445 3rd Ave S P O Box 34165 MS 31-650 Seattle WA 98134', N'8825 Rainier Ave S Seattle WA 98118', 5, 17, N'garbiel080@gmail.com', N'Football, Volleyball', N'Highland Park Elementary', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Admissions] OFF
GO



USE [ITMCollege]
GO
SET IDENTITY_INSERT [dbo].[Registrations] ON 

INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (21, N'ST22119993', N'CarolynKramer-9873f26b-87bf-406a-a2ba-e3630a2f9dd35ae9dc92bceb77b52efa.jpg', 12, 2, N'Carolyn Kramer', N'2445 3rd Ave S P O Box 34165 MS 31-650 Seattle ', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (22, N'ST22705118', N'CarolynKramer-9873f26b-87bf-406a-a2ba-e3630a2f9dd35ae9dc92bceb77b52efa.jpg', 56, NULL, N'Ronnie Katz', N'8825 Rainier Ave S Seattle WA 98118', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (23, N'ST22927561', N'JulieMelver-d757bcb3-1162-463f-b4ac-6391b8995f7b2a362f4d4f34846add25 (1).jpg', 30, NULL, N'Julie Melver', N'8825 Rainier Ave S Seattle WA 98118', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (24, N'ST22863452', N'DanMagee-d5089a9a-61ba-4785-8a25-13f80416a482f4fc9387f3fe38a061ef.jpg', 209, 5, N'Dan Magee', N'8825 Rainier Ave S Seattle WA 98118', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (25, N'ST22070890', N'DanMagee-d5089a9a-61ba-4785-8a25-13f80416a482f4fc9387f3fe38a061ef.jpg', 118, 4, N'Ronnie Katz', N'13501 NE 28th St P O Box 8910 Vancouver, WA 98668', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (26, N'ST22801075', N'JulieMelver-d757bcb3-1162-463f-b4ac-6391b8995f7b2a362f4d4f34846add25 (1).jpg', 10, 2, N'Racie McKee', N'8825 Rainier Ave S Seattle WA 98118', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (27, N'ST22774779', N'CarolynKramer-9873f26b-87bf-406a-a2ba-e3630a2f9dd35ae9dc92bceb77b52efa.jpg', 179, 7, N'Jennifer Douglas', N'8825 Rainier Ave S Seattle WA 98118', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (28, N'ST22312138', N'DanMagee-d5089a9a-61ba-4785-8a25-13f80416a482f4fc9387f3fe38a061ef.jpg', 63, 5, N'Rick Brown', N'13501 NE 28th St P O Box 8910 Vancouver, WA 98668', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (29, N'ST22821449', N'LarryCarpenter-763b4d88-e711-4151-afb1-76947683318f2845a83ec84703195a56.jpg', 71, 4, N'Larry Carpenter', N'8825 Rainier Ave S Seattle WA 98118', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (30, N'ST22038008', N'DanMagee-d5089a9a-61ba-4785-8a25-13f80416a482f4fc9387f3fe38a061ef.jpg', 96, 2, N'Jennifer Douglas', N'13501 NE 28th St P O Box 8910 Vancouver, WA 98668', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (31, N'ST22435970', N'MariaMatig-21882169-826c-403b-a415-c4cc79f877c82a362f4d4f34846add25 (1).jpg', 127, NULL, N'Maria Matig', N'13501 NE 28th St P O Box 8910 Vancouver, WA 98668', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (32, N'ST22481350', N'HughNAeyaert-3b30c836-5ddd-4db6-bb08-df7bba0f04c53dfe238443fd88a3d1ec.jpg', 149, 4, N'Julie Melver', N'13501 NE 28th St P O Box 8910 Vancouver, WA 98668', N'0961858271')
INSERT [dbo].[Registrations] ([RegistrationID], [RegNum], [Image], [SpeSubjectID], [OpSubjectID], [EmergencyName], [EmergencyAddress], [EmergencyPhone]) VALUES (33, N'ST22661552', N'WilhemlBeckman-8b4381ce-cb79-4a4c-b171-7c380014fb9e3dfe238443fd88a3d1ec.jpg', 210, 11, N'Rick Brown', N'13501 NE 28th St P O Box 8910 Vancouver, WA 98668', N'0961858271')
SET IDENTITY_INSERT [dbo].[Registrations] OFF
GO