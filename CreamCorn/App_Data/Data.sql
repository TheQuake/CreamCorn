USE [CreamCorn]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Category];
GO
CREATE TABLE [dbo].[Category] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (MAX) NOT NULL
);

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Company];
GO
CREATE TABLE [dbo].[Company] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [PhoneNumber] NVARCHAR (MAX) NOT NULL,
	[CategoryId] INT NULL
);
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Contact];
GO
CREATE TABLE [dbo].[Contact] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [CompanyId] INT NULL
);

DROP TABLE [dbo].[CompanyCategories];
GO
CREATE TABLE [dbo].[CompanyCategories] (
    [CompanyId] INT NULL,
    [CategoryId] INT NULL
);

insert into category select 'Financial'
insert into category select 'Information Technology'
insert into category select 'Manufacturing'
insert into category select 'Real Estate'

insert into company select 'Redgate', '123456790', 2
insert into company select 'Microsoft', '9876543210', 2
insert into company select 'Berkshire Hathaway', '3125551212', 4
insert into company select 'Apple', '2109637854', 2
insert into company select 'Exxon Mobile', '9019875200', 3
insert into company select 'McKesson', '6307541200', 1
insert into company select 'United Health', '2017548500', 1
insert into company select 'CVS Health', '5042584120', 1
insert into company select 'General Motors', '6053259620', 3
insert into company select 'AT&T', '7046395420', 2
insert into company select 'Ford Motor', '9016307820', 3
insert into company select 'AmerisourceBergen', '8005551200', 1
insert into company select 'Amazon.com', '8009634500', 2

insert into companycategories select 1,2
insert into companycategories select 2,2
insert into companycategories select 3,4
insert into companycategories select 4,2

insert into contact select 'Jim Beam', 2
insert into contact select 'Johnnie Walker', 2
insert into contact select 'Jose Cuervo', 2
insert into contact select 'Jack Daniels', 2
