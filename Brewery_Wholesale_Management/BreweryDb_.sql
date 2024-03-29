USE [master]
GO
/****** Object:  Database [BreweryDb_]    Script Date: 3/6/2023 12:48:35 PM ******/
CREATE DATABASE [BreweryDb_]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BreweryDb_', FILENAME = N'C:\Users\AzComputer\BreweryDb_.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BreweryDb__log', FILENAME = N'C:\Users\AzComputer\BreweryDb__log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BreweryDb_] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BreweryDb_].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BreweryDb_] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BreweryDb_] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BreweryDb_] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BreweryDb_] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BreweryDb_] SET ARITHABORT OFF 
GO
ALTER DATABASE [BreweryDb_] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BreweryDb_] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BreweryDb_] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BreweryDb_] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BreweryDb_] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BreweryDb_] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BreweryDb_] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BreweryDb_] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BreweryDb_] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BreweryDb_] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BreweryDb_] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BreweryDb_] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BreweryDb_] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BreweryDb_] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BreweryDb_] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BreweryDb_] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BreweryDb_] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BreweryDb_] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BreweryDb_] SET  MULTI_USER 
GO
ALTER DATABASE [BreweryDb_] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BreweryDb_] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BreweryDb_] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BreweryDb_] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BreweryDb_] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BreweryDb_] SET QUERY_STORE = OFF
GO
USE [BreweryDb_]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [BreweryDb_]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/6/2023 12:48:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Beers]    Script Date: 3/6/2023 12:48:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Beers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[AlcoholContent] [float] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[BreweryId] [int] NOT NULL,
 CONSTRAINT [PK_Beers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Breweries]    Script Date: 3/6/2023 12:48:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Breweries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Breweries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wholesalers]    Script Date: 3/6/2023 12:48:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wholesalers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Wholesalers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WholesalerStocks]    Script Date: 3/6/2023 12:48:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WholesalerStocks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[BeerId] [int] NOT NULL,
	[WholesalerId] [int] NOT NULL,
 CONSTRAINT [PK_WholesalerStocks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230302165444_InitialMigrationScript', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230302213210_db1', N'6.0.0')
GO
SET IDENTITY_INSERT [dbo].[Beers] ON 

INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (2, N'Italian beers', 0.5, CAST(36.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (4, N'Black Butte Porter', 2.7, CAST(6.30 AS Decimal(18, 2)), 1)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (5, N'Koli beer', 2.5, CAST(3.60 AS Decimal(18, 2)), 1)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (11, N'Mirror Pond Pale', 3.6, CAST(3.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (13, N'Black Butte ', 3.6, CAST(6.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (15, N'Whiskey Butte', 6.5, CAST(7.25 AS Decimal(18, 2)), 2)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (16, N'Family Tree', 6.7, CAST(9.98 AS Decimal(18, 2)), 2)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (17, N'Dark Cherry', 12.5, CAST(9.87 AS Decimal(18, 2)), 3)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (18, N'Twilight Summer ', 7.9, CAST(16.87 AS Decimal(18, 2)), 3)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (19, N'Black Butte ', 9.89, CAST(7.67 AS Decimal(18, 2)), 4)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (20, N'The Ages', 19.99, CAST(17.67 AS Decimal(18, 2)), 4)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (21, N'King Crispy', 9.99, CAST(7.67 AS Decimal(18, 2)), 4)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (22, N'The Dissident', 4.5, CAST(4.67 AS Decimal(18, 2)), 1)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (23, N'Red Chair NWPA', 3.5, CAST(4.67 AS Decimal(18, 2)), 3)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (24, N'Chasin'' Freshies', 3.6, CAST(4.99 AS Decimal(18, 2)), 1)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (25, N'Hazetron Imperial', 7.9, CAST(9.99 AS Decimal(18, 2)), 3)
INSERT [dbo].[Beers] ([Id], [Name], [AlcoholContent], [Price], [BreweryId]) VALUES (26, N'Fresh Variety Pack', 14.9, CAST(9.99 AS Decimal(18, 2)), 2)
SET IDENTITY_INSERT [dbo].[Beers] OFF
GO
SET IDENTITY_INSERT [dbo].[Breweries] ON 

INSERT [dbo].[Breweries] ([Id], [Name]) VALUES (1, N'Avondale')
INSERT [dbo].[Breweries] ([Id], [Name]) VALUES (2, N'Back Forty')
INSERT [dbo].[Breweries] ([Id], [Name]) VALUES (3, N'Chattahoochee ')
INSERT [dbo].[Breweries] ([Id], [Name]) VALUES (4, N'Fairhope ')
SET IDENTITY_INSERT [dbo].[Breweries] OFF
GO
SET IDENTITY_INSERT [dbo].[Wholesalers] ON 

INSERT [dbo].[Wholesalers] ([Id], [Name]) VALUES (1, N'Andria')
INSERT [dbo].[Wholesalers] ([Id], [Name]) VALUES (2, N'Cabezuela del Valle')
INSERT [dbo].[Wholesalers] ([Id], [Name]) VALUES (3, N'Camigliatello Silano')
INSERT [dbo].[Wholesalers] ([Id], [Name]) VALUES (4, N'Melendugno')
SET IDENTITY_INSERT [dbo].[Wholesalers] OFF
GO
SET IDENTITY_INSERT [dbo].[WholesalerStocks] ON 

INSERT [dbo].[WholesalerStocks] ([Id], [Quantity], [BeerId], [WholesalerId]) VALUES (2, 5, 2, 1)
INSERT [dbo].[WholesalerStocks] ([Id], [Quantity], [BeerId], [WholesalerId]) VALUES (3, 203, 4, 1)
INSERT [dbo].[WholesalerStocks] ([Id], [Quantity], [BeerId], [WholesalerId]) VALUES (5, 6060, 13, 1)
SET IDENTITY_INSERT [dbo].[WholesalerStocks] OFF
GO
/****** Object:  Index [IX_Beers_BreweryId]    Script Date: 3/6/2023 12:48:35 PM ******/
CREATE NONCLUSTERED INDEX [IX_Beers_BreweryId] ON [dbo].[Beers]
(
	[BreweryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WholesalerStocks_BeerId]    Script Date: 3/6/2023 12:48:35 PM ******/
CREATE NONCLUSTERED INDEX [IX_WholesalerStocks_BeerId] ON [dbo].[WholesalerStocks]
(
	[BeerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WholesalerStocks_WholesalerId]    Script Date: 3/6/2023 12:48:35 PM ******/
CREATE NONCLUSTERED INDEX [IX_WholesalerStocks_WholesalerId] ON [dbo].[WholesalerStocks]
(
	[WholesalerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Beers]  WITH CHECK ADD  CONSTRAINT [FK_Beers_Breweries_BreweryId] FOREIGN KEY([BreweryId])
REFERENCES [dbo].[Breweries] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Beers] CHECK CONSTRAINT [FK_Beers_Breweries_BreweryId]
GO
ALTER TABLE [dbo].[WholesalerStocks]  WITH CHECK ADD  CONSTRAINT [FK_WholesalerStocks_Beers_BeerId] FOREIGN KEY([BeerId])
REFERENCES [dbo].[Beers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WholesalerStocks] CHECK CONSTRAINT [FK_WholesalerStocks_Beers_BeerId]
GO
ALTER TABLE [dbo].[WholesalerStocks]  WITH CHECK ADD  CONSTRAINT [FK_WholesalerStocks_Wholesalers_WholesalerId] FOREIGN KEY([WholesalerId])
REFERENCES [dbo].[Wholesalers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WholesalerStocks] CHECK CONSTRAINT [FK_WholesalerStocks_Wholesalers_WholesalerId]
GO
USE [master]
GO
ALTER DATABASE [BreweryDb_] SET  READ_WRITE 
GO
