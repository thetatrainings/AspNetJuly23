USE [master]
GO
/****** Object:  Database [ecommerce_app]    Script Date: 09/08/2023 14:27:04 ******/
CREATE DATABASE [ecommerce_app]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ecommerce_app', FILENAME = N'C:\Users\MuhammadUsman\ecommerce_app.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ecommerce_app_log', FILENAME = N'C:\Users\MuhammadUsman\ecommerce_app_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ecommerce_app] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ecommerce_app].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ecommerce_app] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ecommerce_app] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ecommerce_app] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ecommerce_app] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ecommerce_app] SET ARITHABORT OFF 
GO
ALTER DATABASE [ecommerce_app] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ecommerce_app] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ecommerce_app] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ecommerce_app] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ecommerce_app] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ecommerce_app] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ecommerce_app] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ecommerce_app] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ecommerce_app] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ecommerce_app] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ecommerce_app] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ecommerce_app] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ecommerce_app] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ecommerce_app] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ecommerce_app] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ecommerce_app] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ecommerce_app] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ecommerce_app] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ecommerce_app] SET  MULTI_USER 
GO
ALTER DATABASE [ecommerce_app] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ecommerce_app] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ecommerce_app] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ecommerce_app] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ecommerce_app] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ecommerce_app] SET QUERY_STORE = OFF
GO
USE [ecommerce_app]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [ecommerce_app]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 09/08/2023 14:27:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[role] [nvarchar](10) NULL,
	[created_by] [nvarchar](50) NULL,
	[created_date] [datetime] NULL,
	[modify_by] [nvarchar](50) NULL,
	[modified_date] [datetime] NULL,
	[image] [nvarchar](max) NULL,
	[status] [nvarchar](10) NULL,
	[system_user_id] [int] NULL,
 CONSTRAINT [PK_admin] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 09/08/2023 14:27:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
	[status] [nvarchar](10) NOT NULL,
	[parent_id] [int] NULL,
	[created_by] [varchar](50) NULL,
	[created_date] [datetime] NULL,
	[modified_by] [nvarchar](50) NULL,
	[modified_date] [datetime] NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 09/08/2023 14:27:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[phone_number] [nvarchar](50) NULL,
	[status] [nvarchar](10) NULL,
	[created_by] [nvarchar](50) NULL,
	[created_date] [datetime] NULL,
	[modify_by] [nchar](50) NULL,
	[modified_date] [datetime] NULL,
	[image] [nvarchar](max) NULL,
	[role] [nvarchar](10) NULL,
	[system_user_id] [int] NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order]    Script Date: 09/08/2023 14:27:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[quantity] [int] NULL,
	[product_id] [int] NULL,
	[order_detail] [nvarchar](50) NULL,
	[customer_id] [int] NULL,
	[created_by] [nvarchar](50) NULL,
	[created_date] [datetime] NULL,
	[modify_by] [nvarchar](50) NULL,
	[modified_date] [datetime] NULL,
	[status] [nvarchar](10) NULL,
 CONSTRAINT [PK_order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 09/08/2023 14:27:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](max) NULL,
	[quantity] [int] NULL,
	[created_by] [nvarchar](50) NULL,
	[created_date] [datetime] NULL,
	[modify_by] [nvarchar](50) NULL,
	[modified_date] [datetime] NULL,
	[category_id] [int] NULL,
	[price] [int] NULL,
	[image] [nvarchar](max) NULL,
	[status] [nvarchar](10) NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[seller]    Script Date: 09/08/2023 14:27:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[seller](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[address] [nvarchar](50) NULL,
	[phone_number] [nvarchar](10) NULL,
	[join_date] [datetime] NULL,
	[company_name] [int] NULL,
	[created_by] [nvarchar](50) NULL,
	[created_date] [datetime] NULL,
	[modify_by] [nvarchar](50) NULL,
	[modified_date] [datetime] NULL,
	[image] [nvarchar](max) NULL,
	[status] [nvarchar](10) NULL,
	[role] [nvarchar](10) NULL,
	[system_user_id] [int] NULL,
 CONSTRAINT [PK_seller] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[system_user]    Script Date: 09/08/2023 14:27:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[system_user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[role] [nvarchar](10) NULL,
	[status] [nvarchar](10) NULL,
 CONSTRAINT [PK_system_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [ecommerce_app] SET  READ_WRITE 
GO
