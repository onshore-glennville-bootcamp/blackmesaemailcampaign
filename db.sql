USE [EmailCampaign]
GO
ALTER TABLE [dbo].[Subscribers] DROP CONSTRAINT [DF__Subscribe__Activ__1BFD2C07]
GO
ALTER TABLE [dbo].[MarketingManagers] DROP CONSTRAINT [DF__Marketing__Activ__1B0907CE]
GO
/****** Object:  Table [dbo].[Subscribers]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP TABLE [dbo].[Subscribers]
GO
/****** Object:  Table [dbo].[MarketingManagers]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP TABLE [dbo].[MarketingManagers]
GO
/****** Object:  StoredProcedure [dbo].[UpdateMarketingManager]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP PROCEDURE [dbo].[UpdateMarketingManager]
GO
/****** Object:  StoredProcedure [dbo].[SearchSubscribers]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP PROCEDURE [dbo].[SearchSubscribers]
GO
/****** Object:  StoredProcedure [dbo].[GetSubscribersByID]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP PROCEDURE [dbo].[GetSubscribersByID]
GO
/****** Object:  StoredProcedure [dbo].[GetMarketingManagerByID]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP PROCEDURE [dbo].[GetMarketingManagerByID]
GO
/****** Object:  StoredProcedure [dbo].[GetMarketingManagerByEmail]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP PROCEDURE [dbo].[GetMarketingManagerByEmail]
GO
/****** Object:  StoredProcedure [dbo].[GetAllSubscribers]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP PROCEDURE [dbo].[GetAllSubscribers]
GO
/****** Object:  StoredProcedure [dbo].[GetAllMarketingManagers]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP PROCEDURE [dbo].[GetAllMarketingManagers]
GO
/****** Object:  StoredProcedure [dbo].[DeleteMarketingManager]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP PROCEDURE [dbo].[DeleteMarketingManager]
GO
/****** Object:  StoredProcedure [dbo].[CreateSubscriber]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP PROCEDURE [dbo].[CreateSubscriber]
GO
/****** Object:  StoredProcedure [dbo].[CreateMarketingManager]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP PROCEDURE [dbo].[CreateMarketingManager]
GO
USE [master]
GO
/****** Object:  Database [EmailCampaign]    Script Date: 12/20/2013 1:29:13 PM ******/
DROP DATABASE [EmailCampaign]
GO
/****** Object:  Database [EmailCampaign]    Script Date: 12/20/2013 1:29:13 PM ******/
CREATE DATABASE [EmailCampaign]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EmailCampaign', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\EmailCampaign.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EmailCampaign_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\EmailCampaign_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EmailCampaign] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmailCampaign].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmailCampaign] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmailCampaign] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmailCampaign] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmailCampaign] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmailCampaign] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmailCampaign] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [EmailCampaign] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [EmailCampaign] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmailCampaign] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmailCampaign] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmailCampaign] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmailCampaign] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmailCampaign] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmailCampaign] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmailCampaign] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmailCampaign] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EmailCampaign] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmailCampaign] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmailCampaign] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmailCampaign] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmailCampaign] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmailCampaign] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmailCampaign] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmailCampaign] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EmailCampaign] SET  MULTI_USER 
GO
ALTER DATABASE [EmailCampaign] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmailCampaign] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EmailCampaign] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EmailCampaign] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [EmailCampaign]
GO
/****** Object:  StoredProcedure [dbo].[CreateMarketingManager]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[CreateMarketingManager]
@Email VARCHAR(100),
@Password VARCHAR(25),
@Active BIT
AS
BEGIN
INSERT INTO MarketingManagers
VALUES (@Email, @Password, @Active)
END





GO
/****** Object:  StoredProcedure [dbo].[CreateSubscriber]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateSubscriber]
@Email VARCHAR(100),
@FirstName VARCHAR(100),
@LastName VARCHAR(100),
@Active BIT
AS
BEGIN
INSERT INTO Subscribers
VALUES (@Email, @FirstName, @LastName, @Active)
END





GO
/****** Object:  StoredProcedure [dbo].[DeleteMarketingManager]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteMarketingManager]
	@ID int
AS
BEGIN
	update MarketingManagers
	set Active = 0
	where ID = @ID
END




GO
/****** Object:  StoredProcedure [dbo].[GetAllMarketingManagers]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllMarketingManagers]

AS
BEGIN
SELECT * FROM MarketingManagers
WHERE Active=1
END





GO
/****** Object:  StoredProcedure [dbo].[GetAllSubscribers]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllSubscribers]

AS
BEGIN
SELECT * FROM Subscribers
WHERE Active=1
END





GO
/****** Object:  StoredProcedure [dbo].[GetMarketingManagerByEmail]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMarketingManagerByEmail] 
	-- Add the parameters for the stored procedure here
	@Email varchar(100)
AS
BEGIN
	select * from MarketingManagers
	where Email = @Email and Active = 1
END




GO
/****** Object:  StoredProcedure [dbo].[GetMarketingManagerByID]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetMarketingManagerByID]
@ID INT
AS
BEGIN
SELECT * FROM MarketingManagers
WHERE Active=1 AND ID = @ID
END





GO
/****** Object:  StoredProcedure [dbo].[GetSubscribersByID]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSubscribersByID]
@ID INT
AS
BEGIN
SELECT * FROM Subscribers
WHERE Active=1 AND ID = @ID
END





GO
/****** Object:  StoredProcedure [dbo].[SearchSubscribers]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  <Author,,Name>
-- Create date: <Create Date,,>
-- Description: <Description,,>
-- =============================================
create PROCEDURE [dbo].[SearchSubscribers] 
@search VARCHAR(100)
AS
BEGIN
 SELECT * FROM Subscribers
 WHERE 
 Active = 1
 AND 
 (
 FirstName LIKE @search
 OR LastName LIKE @search
 OR EMAIL LIKE @search
 )
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateMarketingManager]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[UpdateMarketingManager]
	@Email varchar(100), @Password varchar(25), @ID int
AS
BEGIN
	update MarketingManagers
	set Email = @Email, Password = @Password
	where ID = @ID and Active = 1
END




GO
/****** Object:  Table [dbo].[MarketingManagers]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MarketingManagers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](25) NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Subscribers]    Script Date: 12/20/2013 1:29:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Subscribers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[MarketingManagers] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Subscribers] ADD  DEFAULT ((1)) FOR [Active]
GO
USE [master]
GO
ALTER DATABASE [EmailCampaign] SET  READ_WRITE 
GO
