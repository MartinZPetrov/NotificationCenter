USE [master]
GO
/****** Object:  Database [Notification]    Script Date: 07/20/2016 09:56:46 ******/
CREATE DATABASE [Notification] ON  PRIMARY 
( NAME = N'Notification', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS2008R2\MSSQL\DATA\Notification.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Notification_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS2008R2\MSSQL\DATA\Notification_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Notification] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Notification].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Notification] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Notification] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Notification] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Notification] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Notification] SET ARITHABORT OFF
GO
ALTER DATABASE [Notification] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Notification] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Notification] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Notification] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Notification] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Notification] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Notification] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Notification] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Notification] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Notification] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Notification] SET  ENABLE_BROKER
GO
ALTER DATABASE [Notification] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Notification] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Notification] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Notification] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Notification] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Notification] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Notification] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Notification] SET  READ_WRITE
GO
ALTER DATABASE [Notification] SET RECOVERY SIMPLE
GO
ALTER DATABASE [Notification] SET  MULTI_USER
GO
ALTER DATABASE [Notification] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Notification] SET DB_CHAINING OFF
GO
USE [Notification]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 07/20/2016 09:56:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[CriteriaGroup] [varchar](50) NULL,
	[TypeGroup] [varchar](50) NULL,
	[Channel] [varchar](50) NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Client]    Script Date: 07/20/2016 09:56:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Client](
	[ClientID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[GroupType] [int] NULL,
	[Password] [varchar](255) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Validate_User]    Script Date: 07/20/2016 09:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  PROCEDURE [dbo].[Validate_User]
	@Username NVARCHAR(20),
	@Password NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @ClinetID INT
	
	SELECT @ClinetID = ClientID
	FROM Client WHERE Name = @Username AND [Password] = @Password
	
	IF @ClinetID IS NOT NULL
		BEGIN
			SELECT @ClinetID
		END
	ELSE
	BEGIN
		SELECT -1 -- User invalid.
	END
END
GO
/****** Object:  StoredProcedure [dbo].[SqlQueryNotificationStoredProcedure-7db2e56b-192d-433c-9f38-c0a27742e644]    Script Date: 07/20/2016 09:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SqlQueryNotificationStoredProcedure-7db2e56b-192d-433c-9f38-c0a27742e644] AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM [SqlQueryNotificationService-7db2e56b-192d-433c-9f38-c0a27742e644]; IF (SELECT COUNT(*) FROM [SqlQueryNotificationService-7db2e56b-192d-433c-9f38-c0a27742e644] WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = 'SqlQueryNotificationService-7db2e56b-192d-433c-9f38-c0a27742e644') > 0)   DROP SERVICE [SqlQueryNotificationService-7db2e56b-192d-433c-9f38-c0a27742e644]; if (OBJECT_ID('SqlQueryNotificationService-7db2e56b-192d-433c-9f38-c0a27742e644', 'SQ') IS NOT NULL)   DROP QUEUE [SqlQueryNotificationService-7db2e56b-192d-433c-9f38-c0a27742e644]; DROP PROCEDURE [SqlQueryNotificationStoredProcedure-7db2e56b-192d-433c-9f38-c0a27742e644]; END COMMIT TRANSACTION; END
GO
/****** Object:  Table [dbo].[Request]    Script Date: 07/20/2016 09:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Request](
	[RequestID] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NOT NULL,
	[Type] [varchar](50) NULL,
	[Date] [datetime] NULL,
	[Status] [varchar](50) NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Certificate]    Script Date: 07/20/2016 09:56:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Certificate](
	[CertificateID] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NOT NULL,
	[ValidFrom] [datetime] NULL,
	[ValidTo] [datetime] NULL,
 CONSTRAINT [PK_Certificate] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Request_Client]    Script Date: 07/20/2016 09:56:47 ******/
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([ClientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Client]
GO
/****** Object:  ForeignKey [PK_Certificate_Client]    Script Date: 07/20/2016 09:56:47 ******/
ALTER TABLE [dbo].[Certificate]  WITH CHECK ADD  CONSTRAINT [PK_Certificate_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([ClientID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Certificate] CHECK CONSTRAINT [PK_Certificate_Client]
GO


---Requited For database notifications
ALTER DATABASE [Notification] SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE

insert into Client values ('Martin', 1, '123')
insert into Client values ('Pesho',  2, '123')
--- Invalid Certificate prior to todays date
insert into Certificate values (1, dateadd(day,datediff(day,20,GETDATE()),0), dateadd(day,datediff(day,1,GETDATE()),0))
-- Valid Certificate
insert into Certificate values (2, dateadd(day,datediff(day,20,GETDATE()),0), DATEADD(month, 1, GETDATE()))