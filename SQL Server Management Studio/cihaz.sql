USE [RandevuDatabase]
GO

/****** Object:  Table [dbo].[Cihaz]    Script Date: 26.10.2020 11:07:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON 
GO

CREATE TABLE [dbo].[Cihaz](
	[CihazID] [tinyint] IDENTITY(1,1) NOT NULL,
	[CihazTipiID] [tinyint] NULL,
	[CihazAdı] [nvarchar](50) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifyTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Cihaz] PRIMARY KEY CLUSTERED 
(
	[CihazID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cihaz] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Cihaz] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Cihaz] ADD  DEFAULT (getdate()) FOR [ModifyTime]
GO


