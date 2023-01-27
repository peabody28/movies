USE [film]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 27.01.2023 11:37:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](32) NOT NULL,
	[Code] [nvarchar](8) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Director]    Script Date: 27.01.2023 11:37:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Director](
	[Id] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](32) NOT NULL,
	[LastName] [nvarchar](32) NULL,
	[Age] [int] NULL,
 CONSTRAINT [PK_Director] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Film]    Script Date: 27.01.2023 11:37:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Film](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](64) NULL,
	[Description] [text] NULL,
	[DirectorFk] [uniqueidentifier] NOT NULL,
	[CountryFk] [uniqueidentifier] NOT NULL,
	[Year] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Film] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 27.01.2023 11:37:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[Id] [uniqueidentifier] NOT NULL,
	[FilmFk] [uniqueidentifier] NOT NULL,
	[RatingTypeFk] [uniqueidentifier] NOT NULL,
	[Value] [decimal](5, 2) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Rating] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RatingType]    Script Date: 27.01.2023 11:37:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RatingType](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_RatingType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Film] ADD  CONSTRAINT [DF_Film_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Rating] ADD  CONSTRAINT [DF_Rating_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Film]  WITH CHECK ADD  CONSTRAINT [FK_Film_Country] FOREIGN KEY([CountryFk])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[Film] CHECK CONSTRAINT [FK_Film_Country]
GO
ALTER TABLE [dbo].[Film]  WITH CHECK ADD  CONSTRAINT [FK_Film_Director] FOREIGN KEY([DirectorFk])
REFERENCES [dbo].[Director] ([Id])
GO
ALTER TABLE [dbo].[Film] CHECK CONSTRAINT [FK_Film_Director]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_Film] FOREIGN KEY([FilmFk])
REFERENCES [dbo].[Film] ([Id])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_Film]
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD  CONSTRAINT [FK_Rating_RatingType] FOREIGN KEY([RatingTypeFk])
REFERENCES [dbo].[RatingType] ([Id])
GO
ALTER TABLE [dbo].[Rating] CHECK CONSTRAINT [FK_Rating_RatingType]
GO


USE [film]
GO

INSERT INTO [dbo].[Country]
           ([Id]
           ,[Name]
           ,[Code])
     VALUES
           ('{5E5FDAC3-A45E-4C36-AD93-D48642C14A95}'
           ,'United States of America'
           ,'US')
GO

INSERT INTO [dbo].[Director]
           ([Id]
           ,[FirstName]
           ,[LastName]
           ,[Age])
     VALUES
           ('{2B8AC57B-921F-4DFC-95EC-A1666ABD91D8}'
           ,'Guy'
           ,'Ritchie'
           ,54)
GO

INSERT INTO [dbo].[RatingType]
           ([Id]
           ,[Name])
     VALUES
           ('{ECDEAF93-3E30-4B77-9ED0-0D6F6BF93567}'
           ,'IMDB')
GO




