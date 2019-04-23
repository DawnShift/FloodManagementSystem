/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [FloodManagementSystem]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 23-04-2019 23:23:01 ******/
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
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Discription] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[RegionId] [int] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[StateId] [int] NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CityAudit]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityAudit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[resourceId] [int] NOT NULL,
	[TotalAvailable] [int] NOT NULL,
	[CityId] [int] NOT NULL,
 CONSTRAINT [PK_CityAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CityRequests]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ResourceId] [int] NOT NULL,
	[TotalNeeded] [int] NOT NULL,
	[CityId] [int] NOT NULL,
 CONSTRAINT [PK_CityRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Disaster]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[ImagePath] [varchar](max) NULL,
 CONSTRAINT [PK_Disaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DisasterDetails]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisasterDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisasterId] [int] NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Description] [varchar](max) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_DisasterDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DistributerRequests]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistributerRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ResourceId] [int] NOT NULL,
	[TotalNeeded] [int] NOT NULL,
	[RegionId] [int] NOT NULL,
 CONSTRAINT [PK_DistributerRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EffectedCities]    Script Date: 23-04-2019 23:23:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EffectedCities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Stateid] [int] NOT NULL,
	[DisasterDetailsId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_EffectedCities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Regions]    Script Date: 23-04-2019 23:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Regions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CityId] [int] NULL,
 CONSTRAINT [PK_Regions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceAudit]    Script Date: 23-04-2019 23:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceAudit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ResourceId] [int] NOT NULL,
	[TotalCountAvailable] [int] NOT NULL,
	[RegionId] [int] NOT NULL,
	[CityId] [int] NOT NULL,
	[StateId] [int] NOT NULL,
 CONSTRAINT [PK_ResourceAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceCollection]    Script Date: 23-04-2019 23:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceCollection](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ResourceId] [int] NOT NULL,
	[TotalCollected] [int] NOT NULL,
	[RgionId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_ResourceCollection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceRequest]    Script Date: 23-04-2019 23:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequestDetails] [varchar](max) NOT NULL,
	[ResourceId] [int] NOT NULL,
	[TotalNeeded] [int] NOT NULL,
	[RegionId] [int] NOT NULL,
	[DisasterDetailsId] [int] NOT NULL,
	[ResourceStatus] [int] NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_ResourceRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resources]    Script Date: 23-04-2019 23:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Resources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceStatus]    Script Date: 23-04-2019 23:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](15) NOT NULL,
 CONSTRAINT [PK_ResourceStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 23-04-2019 23:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StateAudit]    Script Date: 23-04-2019 23:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StateAudit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateId] [int] NOT NULL,
	[ResourceId] [int] NOT NULL,
	[TotalAvailable] [int] NOT NULL,
 CONSTRAINT [PK_StateAudit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190318110734_Initial', N'2.2.2-servicing-10034')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discription]) VALUES (N'01ee50e6-5cbb-45d9-a0ff-9e98f7e9376a', N'Distributer', N'DISTRIBUTER', N'0e4bfd55-021f-4ebe-a233-691153a22e09', N'Distributer')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discription]) VALUES (N'0ab76a5d-5a52-4510-98e0-3d3c0782a4a6', N'Members', N'MEMBERS', N'874ece9c-6870-4eba-a34c-ef960096a52f', N'Members')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discription]) VALUES (N'343387cd-92ef-4209-aa56-15d882e14ffa', N'State Co-Ordinator', N'STATE CO-ORDINATOR', N'2a3f8b36-68e6-4c20-91da-856d21d892eb', N'State Co-Ordinator')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discription]) VALUES (N'88d78a9a-8a74-4166-9679-23863e37e2a1', N'District Co-Ordinator', N'DISTRICT CO-ORDINATOR', N'9415782b-c439-4fa1-8061-34d269272ef7', N'District Co-Ordinator')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [Discription]) VALUES (N'e86b354b-a8ab-45c2-a198-2a648ae56906', N'Administrator', N'ADMINISTRATOR', N'057a7ef3-f12a-4ede-b8ff-9a931f50577e', N'Administrator')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4ad1b305-d3ca-49bc-a609-612cf49d7923', N'01ee50e6-5cbb-45d9-a0ff-9e98f7e9376a')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'be39c26b-3440-4035-9e82-a0e689798a7b', N'0ab76a5d-5a52-4510-98e0-3d3c0782a4a6')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ab67070b-1504-4fcd-97ba-6ff55acc0d36', N'343387cd-92ef-4209-aa56-15d882e14ffa')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fbf4245a-ea6c-482b-8aeb-5bd5f0da2667', N'88d78a9a-8a74-4166-9679-23863e37e2a1')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'05b852fc-6d5f-4597-84e6-36640b3ec471', N'e86b354b-a8ab-45c2-a198-2a648ae56906')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RegionId]) VALUES (N'05b852fc-6d5f-4597-84e6-36640b3ec471', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEE2dL4jN5h+cvyd5yT6eKNRrVSZtQnjf4nX3XLCex386i8AaOJq8vTFPL3nhbSyagg==', N'4QUD7C3KTPYGYU463RWYBYASDLB4TSZD', N'627e2a2b-9d73-450b-a079-7ca8c53e0a77', NULL, 0, 0, NULL, 1, 0, NULL, NULL, NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RegionId]) VALUES (N'4ad1b305-d3ca-49bc-a609-612cf49d7923', N'distributer@gmail.com', N'DISTRIBUTER@GMAIL.COM', N'distributer@gmail.com', N'DISTRIBUTER@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEDjWDMlttRuy2P+njdZxhzo06wX5+jp5dp0Sxlg+knknzx7UeX75EwKO5JoQTnPi7g==', N'4ARE6WYBC2MI3Q7ZLM342VWXVVNRLD4X', N'2d2d66d7-1818-4955-b421-d96df366dee2', N'9947543407', 0, 0, NULL, 1, 0, N'Distributer', N'Default', 2)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RegionId]) VALUES (N'ab67070b-1504-4fcd-97ba-6ff55acc0d36', N'state@gmail.com', N'STATE@GMAIL.COM', N'state@gmail.com', N'STATE@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAECJJEro46m0a5Z1ac/vYRxeSbMRdlpnQ+dUDmN/OXk/uVONAnAeknTIKy9GANd+0pw==', N'LQBZS2GH6JJTLY76LYVAD67V32NB3WMU', N'e482cb3a-9388-4ebd-8009-d1dba33266cd', N'2345689654', 0, 0, NULL, 1, 0, N'State', N'Co-Coordinator', 4)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RegionId]) VALUES (N'be39c26b-3440-4035-9e82-a0e689798a7b', N'member@gmail.com', N'MEMBER@GMAIL.COM', N'member@gmail.com', N'MEMBER@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEOOSC9+EAe1ls7e9t4Hx+1yOpLHyvs5pmU7ZP2S4UiHa7P5nn3jPeUPj2DvWeWkrYQ==', N'VQOYD2CWUF6XN2LZD24536KU77EUODRO', N'5fe62e23-72d7-46fd-9999-e0e6a34336b7', N'9947543407', 0, 0, NULL, 1, 0, N'Rahul', N'SR', 2)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RegionId]) VALUES (N'fbf4245a-ea6c-482b-8aeb-5bd5f0da2667', N'rahuls1.rakvsgnr@gmail.com', N'RAHULS1.RAKVSGNR@GMAIL.COM', N'rahuls1.rakvsgnr@gmail.com', N'RAHULS1.RAKVSGNR@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEBdpUmipKTffpS4i3fkCGgV05vsbLj1TPY9dktVt4xUhlUs4FRTMLM97sRdzcE0tAA==', N'DXY2XSBBL7EIIKUZPKQL4FS54IY3LHS5', N'c0859259-6e0b-410b-ad83-074bc990a1de', N'9947543407', 0, 0, NULL, 1, 0, N'Rahul', N'S R', 2)
GO
SET IDENTITY_INSERT [dbo].[City] ON 
GO
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (1, N'Chennai', 2)
GO
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (2, N'Trivandrum', 1)
GO
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (3, N'Kollam', 1)
GO
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (4, N'Amritsar', 3)
GO
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[CityAudit] ON 
GO
INSERT [dbo].[CityAudit] ([Id], [resourceId], [TotalAvailable], [CityId]) VALUES (1, 1, 40, 2)
GO
INSERT [dbo].[CityAudit] ([Id], [resourceId], [TotalAvailable], [CityId]) VALUES (2, 3, 50, 2)
GO
SET IDENTITY_INSERT [dbo].[CityAudit] OFF
GO
SET IDENTITY_INSERT [dbo].[Disaster] ON 
GO
INSERT [dbo].[Disaster] ([Id], [Name], [ImagePath]) VALUES (1, N'Flood', N'\Resources\_ 534programming-wallpapers-30748-9103588.jpg')
GO
INSERT [dbo].[Disaster] ([Id], [Name], [ImagePath]) VALUES (2, N'Earth Quake', N'\Resources\_ 121quotes-inspire-success8.jpg')
GO
INSERT [dbo].[Disaster] ([Id], [Name], [ImagePath]) VALUES (3, N'Drought', N'\Resources\_ 587programming-wallpaper-full-hd-1080p-313822.jpg')
GO
SET IDENTITY_INSERT [dbo].[Disaster] OFF
GO
SET IDENTITY_INSERT [dbo].[DisasterDetails] ON 
GO
INSERT [dbo].[DisasterDetails] ([Id], [DisasterId], [Name], [Description], [StartDate], [EndDate], [CreatedDate], [IsActive]) VALUES (1, 1, N'Flood', N'A Flood hits Kerala on 15th Aug 2018', CAST(N'2018-02-12T00:00:00.000' AS DateTime), NULL, CAST(N'2019-03-24T15:18:54.037' AS DateTime), 1)
GO
INSERT [dbo].[DisasterDetails] ([Id], [DisasterId], [Name], [Description], [StartDate], [EndDate], [CreatedDate], [IsActive]) VALUES (2, 2, N'RS-7 EarthQuake ', N'RS-7 EarthQuake  hits Kerala and Tamil Nadu', CAST(N'2019-04-14T00:00:00.000' AS DateTime), NULL, CAST(N'2019-04-14T11:57:11.933' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[DisasterDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[EffectedCities] ON 
GO
INSERT [dbo].[EffectedCities] ([Id], [Stateid], [DisasterDetailsId], [IsActive]) VALUES (1, 1, 1, 1)
GO
INSERT [dbo].[EffectedCities] ([Id], [Stateid], [DisasterDetailsId], [IsActive]) VALUES (2, 2, 1, 1)
GO
INSERT [dbo].[EffectedCities] ([Id], [Stateid], [DisasterDetailsId], [IsActive]) VALUES (3, 1, 2, 0)
GO
INSERT [dbo].[EffectedCities] ([Id], [Stateid], [DisasterDetailsId], [IsActive]) VALUES (4, 2, 2, 0)
GO
SET IDENTITY_INSERT [dbo].[EffectedCities] OFF
GO
SET IDENTITY_INSERT [dbo].[Regions] ON 
GO
INSERT [dbo].[Regions] ([Id], [Name], [CityId]) VALUES (1, N'Chennai City', 1)
GO
INSERT [dbo].[Regions] ([Id], [Name], [CityId]) VALUES (2, N'Sreekaryam', 2)
GO
INSERT [dbo].[Regions] ([Id], [Name], [CityId]) VALUES (3, N'Gurudaspur', 4)
GO
INSERT [dbo].[Regions] ([Id], [Name], [CityId]) VALUES (4, N'thirumala', 2)
GO
SET IDENTITY_INSERT [dbo].[Regions] OFF
GO
SET IDENTITY_INSERT [dbo].[ResourceAudit] ON 
GO
INSERT [dbo].[ResourceAudit] ([Id], [ResourceId], [TotalCountAvailable], [RegionId], [CityId], [StateId]) VALUES (1, 1, 40, 2, 2, 1)
GO
INSERT [dbo].[ResourceAudit] ([Id], [ResourceId], [TotalCountAvailable], [RegionId], [CityId], [StateId]) VALUES (2, 2, 50, 2, 2, 1)
GO
INSERT [dbo].[ResourceAudit] ([Id], [ResourceId], [TotalCountAvailable], [RegionId], [CityId], [StateId]) VALUES (3, 3, 38, 2, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[ResourceAudit] OFF
GO
SET IDENTITY_INSERT [dbo].[ResourceCollection] ON 
GO
INSERT [dbo].[ResourceCollection] ([Id], [ResourceId], [TotalCollected], [RgionId], [Status], [UserId]) VALUES (1, 1, 23, 2, 4, N'be39c26b-3440-4035-9e82-a0e689798a7b')
GO
INSERT [dbo].[ResourceCollection] ([Id], [ResourceId], [TotalCollected], [RgionId], [Status], [UserId]) VALUES (2, 2, 50, 2, 4, N'be39c26b-3440-4035-9e82-a0e689798a7b')
GO
INSERT [dbo].[ResourceCollection] ([Id], [ResourceId], [TotalCollected], [RgionId], [Status], [UserId]) VALUES (3, 3, 12, 2, 4, N'be39c26b-3440-4035-9e82-a0e689798a7b')
GO
SET IDENTITY_INSERT [dbo].[ResourceCollection] OFF
GO
SET IDENTITY_INSERT [dbo].[ResourceRequest] ON 
GO
INSERT [dbo].[ResourceRequest] ([Id], [RequestDetails], [ResourceId], [TotalNeeded], [RegionId], [DisasterDetailsId], [ResourceStatus], [UserId]) VALUES (1, N'saasdasd', 1, 2, 2, 1, 4, N'be39c26b-3440-4035-9e82-a0e689798a7b')
GO
INSERT [dbo].[ResourceRequest] ([Id], [RequestDetails], [ResourceId], [TotalNeeded], [RegionId], [DisasterDetailsId], [ResourceStatus], [UserId]) VALUES (2, N'Need Clothes', 1, 2, 2, 1, 4, N'be39c26b-3440-4035-9e82-a0e689798a7b')
GO
INSERT [dbo].[ResourceRequest] ([Id], [RequestDetails], [ResourceId], [TotalNeeded], [RegionId], [DisasterDetailsId], [ResourceStatus], [UserId]) VALUES (3, N'sadasd', 1, 22, 2, 1, 1, N'be39c26b-3440-4035-9e82-a0e689798a7b')
GO
INSERT [dbo].[ResourceRequest] ([Id], [RequestDetails], [ResourceId], [TotalNeeded], [RegionId], [DisasterDetailsId], [ResourceStatus], [UserId]) VALUES (4, N'adsadasd', 1, 22, 2, 1, 1, N'be39c26b-3440-4035-9e82-a0e689798a7b')
GO
INSERT [dbo].[ResourceRequest] ([Id], [RequestDetails], [ResourceId], [TotalNeeded], [RegionId], [DisasterDetailsId], [ResourceStatus], [UserId]) VALUES (9, N'ssd', 3, 12, 2, 1, 4, N'be39c26b-3440-4035-9e82-a0e689798a7b')
GO
INSERT [dbo].[ResourceRequest] ([Id], [RequestDetails], [ResourceId], [TotalNeeded], [RegionId], [DisasterDetailsId], [ResourceStatus], [UserId]) VALUES (10, N'Need more Items', 3, 12, 2, 1, 4, N'be39c26b-3440-4035-9e82-a0e689798a7b')
GO
SET IDENTITY_INSERT [dbo].[ResourceRequest] OFF
GO
SET IDENTITY_INSERT [dbo].[Resources] ON 
GO
INSERT [dbo].[Resources] ([Id], [Name]) VALUES (1, N'Clothing')
GO
INSERT [dbo].[Resources] ([Id], [Name]) VALUES (2, N'Food')
GO
INSERT [dbo].[Resources] ([Id], [Name]) VALUES (3, N'Stationary')
GO
SET IDENTITY_INSERT [dbo].[Resources] OFF
GO
SET IDENTITY_INSERT [dbo].[ResourceStatus] ON 
GO
INSERT [dbo].[ResourceStatus] ([Id], [Name]) VALUES (1, N'Requested')
GO
INSERT [dbo].[ResourceStatus] ([Id], [Name]) VALUES (2, N'Waiting')
GO
INSERT [dbo].[ResourceStatus] ([Id], [Name]) VALUES (3, N'Transfered')
GO
INSERT [dbo].[ResourceStatus] ([Id], [Name]) VALUES (4, N'Complete')
GO
SET IDENTITY_INSERT [dbo].[ResourceStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[State] ON 
GO
INSERT [dbo].[State] ([Id], [Name]) VALUES (1, N'Kerala')
GO
INSERT [dbo].[State] ([Id], [Name]) VALUES (2, N'Tamil Nadu')
GO
INSERT [dbo].[State] ([Id], [Name]) VALUES (3, N'Punjab')
GO
SET IDENTITY_INSERT [dbo].[State] OFF
GO
SET IDENTITY_INSERT [dbo].[StateAudit] ON 
GO
INSERT [dbo].[StateAudit] ([Id], [StateId], [ResourceId], [TotalAvailable]) VALUES (1, 1, 1, 0)
GO
INSERT [dbo].[StateAudit] ([Id], [StateId], [ResourceId], [TotalAvailable]) VALUES (2, 1, 2, 22)
GO
INSERT [dbo].[StateAudit] ([Id], [StateId], [ResourceId], [TotalAvailable]) VALUES (3, 1, 3, 150)
GO
SET IDENTITY_INSERT [dbo].[StateAudit] OFF
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Regions_RegionId] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Regions_RegionId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_State_StateId]
GO
ALTER TABLE [dbo].[CityAudit]  WITH CHECK ADD  CONSTRAINT [FK_CityAudit_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[CityAudit] CHECK CONSTRAINT [FK_CityAudit_City]
GO
ALTER TABLE [dbo].[CityAudit]  WITH CHECK ADD  CONSTRAINT [FK_CityAudit_Resources] FOREIGN KEY([resourceId])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[CityAudit] CHECK CONSTRAINT [FK_CityAudit_Resources]
GO
ALTER TABLE [dbo].[CityRequests]  WITH CHECK ADD  CONSTRAINT [FK_CityRequests_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[CityRequests] CHECK CONSTRAINT [FK_CityRequests_City]
GO
ALTER TABLE [dbo].[CityRequests]  WITH CHECK ADD  CONSTRAINT [FK_CityRequests_Resources] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[CityRequests] CHECK CONSTRAINT [FK_CityRequests_Resources]
GO
ALTER TABLE [dbo].[DisasterDetails]  WITH CHECK ADD  CONSTRAINT [FK_DisasterDetails_Disaster] FOREIGN KEY([DisasterId])
REFERENCES [dbo].[Disaster] ([Id])
GO
ALTER TABLE [dbo].[DisasterDetails] CHECK CONSTRAINT [FK_DisasterDetails_Disaster]
GO
ALTER TABLE [dbo].[DistributerRequests]  WITH CHECK ADD  CONSTRAINT [FK_DistributerRequests_Regions] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([Id])
GO
ALTER TABLE [dbo].[DistributerRequests] CHECK CONSTRAINT [FK_DistributerRequests_Regions]
GO
ALTER TABLE [dbo].[DistributerRequests]  WITH CHECK ADD  CONSTRAINT [FK_DistributerRequests_Resources] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[DistributerRequests] CHECK CONSTRAINT [FK_DistributerRequests_Resources]
GO
ALTER TABLE [dbo].[EffectedCities]  WITH CHECK ADD  CONSTRAINT [FK_EffectedCities_Disaster] FOREIGN KEY([DisasterDetailsId])
REFERENCES [dbo].[DisasterDetails] ([Id])
GO
ALTER TABLE [dbo].[EffectedCities] CHECK CONSTRAINT [FK_EffectedCities_Disaster]
GO
ALTER TABLE [dbo].[EffectedCities]  WITH CHECK ADD  CONSTRAINT [FK_EffectedCities_State] FOREIGN KEY([Stateid])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[EffectedCities] CHECK CONSTRAINT [FK_EffectedCities_State]
GO
ALTER TABLE [dbo].[Regions]  WITH CHECK ADD  CONSTRAINT [FK_Regions_City_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Regions] CHECK CONSTRAINT [FK_Regions_City_CityId]
GO
ALTER TABLE [dbo].[ResourceAudit]  WITH CHECK ADD  CONSTRAINT [FK_ResourceAudit_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[ResourceAudit] CHECK CONSTRAINT [FK_ResourceAudit_City]
GO
ALTER TABLE [dbo].[ResourceAudit]  WITH CHECK ADD  CONSTRAINT [FK_ResourceAudit_Regions] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([Id])
GO
ALTER TABLE [dbo].[ResourceAudit] CHECK CONSTRAINT [FK_ResourceAudit_Regions]
GO
ALTER TABLE [dbo].[ResourceAudit]  WITH CHECK ADD  CONSTRAINT [FK_ResourceAudit_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[ResourceAudit] CHECK CONSTRAINT [FK_ResourceAudit_State]
GO
ALTER TABLE [dbo].[ResourceCollection]  WITH CHECK ADD  CONSTRAINT [FK_ResourceCollection_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ResourceCollection] CHECK CONSTRAINT [FK_ResourceCollection_AspNetUsers]
GO
ALTER TABLE [dbo].[ResourceCollection]  WITH CHECK ADD  CONSTRAINT [FK_ResourceCollection_Regions] FOREIGN KEY([RgionId])
REFERENCES [dbo].[Regions] ([Id])
GO
ALTER TABLE [dbo].[ResourceCollection] CHECK CONSTRAINT [FK_ResourceCollection_Regions]
GO
ALTER TABLE [dbo].[ResourceCollection]  WITH CHECK ADD  CONSTRAINT [FK_ResourceCollection_Resources] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[ResourceCollection] CHECK CONSTRAINT [FK_ResourceCollection_Resources]
GO
ALTER TABLE [dbo].[ResourceCollection]  WITH CHECK ADD  CONSTRAINT [FK_ResourceCollection_ResourceStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[ResourceStatus] ([Id])
GO
ALTER TABLE [dbo].[ResourceCollection] CHECK CONSTRAINT [FK_ResourceCollection_ResourceStatus]
GO
ALTER TABLE [dbo].[ResourceRequest]  WITH CHECK ADD  CONSTRAINT [FK_ResourceRequest_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ResourceRequest] CHECK CONSTRAINT [FK_ResourceRequest_AspNetUsers]
GO
ALTER TABLE [dbo].[ResourceRequest]  WITH CHECK ADD  CONSTRAINT [FK_ResourceRequest_DisasterDetails] FOREIGN KEY([DisasterDetailsId])
REFERENCES [dbo].[DisasterDetails] ([Id])
GO
ALTER TABLE [dbo].[ResourceRequest] CHECK CONSTRAINT [FK_ResourceRequest_DisasterDetails]
GO
ALTER TABLE [dbo].[ResourceRequest]  WITH CHECK ADD  CONSTRAINT [FK_ResourceRequest_Regions] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([Id])
GO
ALTER TABLE [dbo].[ResourceRequest] CHECK CONSTRAINT [FK_ResourceRequest_Regions]
GO
ALTER TABLE [dbo].[StateAudit]  WITH CHECK ADD  CONSTRAINT [FK_StateAudit_Resources] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resources] ([Id])
GO
ALTER TABLE [dbo].[StateAudit] CHECK CONSTRAINT [FK_StateAudit_Resources]
GO
ALTER TABLE [dbo].[StateAudit]  WITH CHECK ADD  CONSTRAINT [FK_StateAudit_State] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[StateAudit] CHECK CONSTRAINT [FK_StateAudit_State]
GO
/****** Object:  StoredProcedure [dbo].[GetCityRequests]    Script Date: 23-04-2019 23:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[GetCityRequests]
 @cityId int
AS
BEGIN
     declare @stateId int
	 select @stateId =[StateId] from [dbo].[City] where Id = @cityId
	  select * from [dbo].[CityRequests] where [CityId] in(select Id from [dbo].[City] where StateId = @stateId)
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegionalRequests]    Script Date: 23-04-2019 23:23:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
CREATE PROCEDURE [dbo].[GetRegionalRequests]
 (
 @regionId int
 )
 AS
BEGIN
	 declare @cityId int
	 select @cityId = [CityId]  from [dbo].[Regions] where Id = @regionId
	 select * from [dbo].[DistributerRequests] where [RegionId] in (select id from [dbo].[Regions] where CityId =@cityId)
END
GO
