IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Users_dbo.Operators_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_dbo.Users_dbo.Operators_Id]
GO
/****** Object:  Index [IX_UserName]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_UserName')
DROP INDEX [IX_UserName] ON [dbo].[Users]
GO
/****** Object:  Index [IX_Role]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_Role')
DROP INDEX [IX_Role] ON [dbo].[Users]
GO
/****** Object:  Index [IX_IsActive]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_IsActive')
DROP INDEX [IX_IsActive] ON [dbo].[Users]
GO
/****** Object:  Index [IX_Id]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_Id')
DROP INDEX [IX_Id] ON [dbo].[Users]
GO
/****** Object:  Index [IX_LastName]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND name = N'IX_LastName')
DROP INDEX [IX_LastName] ON [dbo].[Operators]
GO
/****** Object:  Index [IX_IsActive]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND name = N'IX_IsActive')
DROP INDEX [IX_IsActive] ON [dbo].[Operators]
GO
/****** Object:  Index [IX_FirstName]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND name = N'IX_FirstName')
DROP INDEX [IX_FirstName] ON [dbo].[Operators]
GO
/****** Object:  Index [IX_EmployeeNbr]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND name = N'IX_EmployeeNbr')
DROP INDEX [IX_EmployeeNbr] ON [dbo].[Operators]
GO
/****** Object:  Index [IX_Document]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND name = N'IX_Document')
DROP INDEX [IX_Document] ON [dbo].[Operators]
GO
/****** Object:  Index [IX_Name]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Clients]') AND name = N'IX_Name')
DROP INDEX [IX_Name] ON [dbo].[Clients]
GO
/****** Object:  Index [IX_Code]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Clients]') AND name = N'IX_Code')
DROP INDEX [IX_Code] ON [dbo].[Clients]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Operators]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND type in (N'U'))
DROP TABLE [dbo].[Operators]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 5/17/2018 9:47:43 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Clients]') AND type in (N'U'))
DROP TABLE [dbo].[Clients]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 5/17/2018 9:47:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Clients]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Clients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](8) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Operators]    Script Date: 5/17/2018 9:47:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Operators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Document] [nvarchar](200) NOT NULL,
	[EmployeeNbr] [int] NOT NULL,
	[FirstName] [nvarchar](200) NOT NULL,
	[LastName] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((0)),
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.Operators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/17/2018 9:47:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[Id] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[Role] [int] NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (1, N'00000001', N'Client Name.1')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (2, N'00000002', N'Client Name.2')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (3, N'00000003', N'Client Name.3')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (4, N'00000004', N'Client Name.4')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (5, N'00000005', N'Client Name.5')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (6, N'00000006', N'Client Name.6')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (7, N'00000007', N'Client Name.7')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (8, N'00000008', N'Client Name.8')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (9, N'00000009', N'Client Name.9')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (10, N'00000010', N'Client Name.10')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (11, N'00000011', N'Client Name.11')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (12, N'00000012', N'Client Name.12')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (13, N'00000013', N'Client Name.13')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (14, N'00000014', N'Client Name.14')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (15, N'00000015', N'Client Name.15')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (16, N'00000016', N'Client Name.16')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (17, N'00000017', N'Client Name.17')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (18, N'00000018', N'Client Name.18')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (19, N'00000019', N'Client Name.19')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (20, N'00000020', N'Client Name.20')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (21, N'00000021', N'Client Name.21')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (22, N'00000022', N'Client Name.22')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (23, N'00000023', N'Client Name.23')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (24, N'00000024', N'Client Name.24')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (25, N'00000025', N'Client Name.25')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (26, N'00000026', N'Client Name.26')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (27, N'00000027', N'Client Name.27')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (28, N'00000028', N'Client Name.28')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (29, N'00000029', N'Client Name.29')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (30, N'00000030', N'Client Name.30')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (31, N'00000031', N'Client Name.31')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (32, N'00000032', N'Client Name.32')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (33, N'00000033', N'Client Name.33')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (34, N'00000034', N'Client Name.34')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (35, N'00000035', N'Client Name.35')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (36, N'00000036', N'Client Name.36')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (37, N'00000037', N'Client Name.37')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (38, N'00000038', N'Client Name.38')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (39, N'00000039', N'Client Name.39')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (40, N'00000040', N'Client Name.40')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (41, N'00000041', N'Client Name.41')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (42, N'00000042', N'Client Name.42')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (43, N'00000043', N'Client Name.43')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (44, N'00000044', N'Client Name.44')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (45, N'00000045', N'Client Name.45')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (46, N'00000046', N'Client Name.46')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (47, N'00000047', N'Client Name.47')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (48, N'00000048', N'Client Name.48')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (49, N'00000049', N'Client Name.49')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (50, N'00000050', N'Client Name.50')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (51, N'00000051', N'Client Name.51')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (52, N'00000052', N'Client Name.52')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (53, N'00000053', N'Client Name.53')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (54, N'00000054', N'Client Name.54')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (55, N'00000055', N'Client Name.55')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (56, N'00000056', N'Client Name.56')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (57, N'00000057', N'Client Name.57')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (58, N'00000058', N'Client Name.58')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (59, N'00000059', N'Client Name.59')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (60, N'00000060', N'Client Name.60')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (61, N'00000061', N'Client Name.61')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (62, N'00000062', N'Client Name.62')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (63, N'00000063', N'Client Name.63')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (64, N'00000064', N'Client Name.64')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (65, N'00000065', N'Client Name.65')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (66, N'00000066', N'Client Name.66')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (67, N'00000067', N'Client Name.67')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (68, N'00000068', N'Client Name.68')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (69, N'00000069', N'Client Name.69')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (70, N'00000070', N'Client Name.70')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (71, N'00000071', N'Client Name.71')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (72, N'00000072', N'Client Name.72')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (73, N'00000073', N'Client Name.73')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (74, N'00000074', N'Client Name.74')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (75, N'00000075', N'Client Name.75')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (76, N'00000076', N'Client Name.76')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (77, N'00000077', N'Client Name.77')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (78, N'00000078', N'Client Name.78')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (79, N'00000079', N'Client Name.79')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (80, N'00000080', N'Client Name.80')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (81, N'00000081', N'Client Name.81')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (82, N'00000082', N'Client Name.82')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (83, N'00000083', N'Client Name.83')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (84, N'00000084', N'Client Name.84')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (85, N'00000085', N'Client Name.85')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (86, N'00000086', N'Client Name.86')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (87, N'00000087', N'Client Name.87')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (88, N'00000088', N'Client Name.88')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (89, N'00000089', N'Client Name.89')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (90, N'00000090', N'Client Name.90')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (91, N'00000091', N'Client Name.91')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (92, N'00000092', N'Client Name.92')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (93, N'00000093', N'Client Name.93')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (94, N'00000094', N'Client Name.94')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (95, N'00000095', N'Client Name.95')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (96, N'00000096', N'Client Name.96')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (97, N'00000097', N'Client Name.97')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (98, N'00000098', N'Client Name.98')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (99, N'00000099', N'Client Name.99')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (100, N'00000100', N'Client Name.100')
GO
INSERT [dbo].[Clients] ([Id], [Code], [Name]) VALUES (101, N'mensi', N'mongo')
GO
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Operators] ON 

GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (1, N'Employee Nbr 1', 1, N'Name 1', N'LastName1', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (2, N'Employee Nbr 2', 2, N'Name 2', N'LastName2', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (3, N'Employee Nbr 3', 3, N'Name 3', N'LastName3', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (4, N'Employee Nbr 4', 4, N'Name 4', N'LastName4', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (5, N'Employee Nbr 5', 5, N'Name 5', N'LastName5', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (6, N'Employee Nbr 6', 6, N'Name 6', N'LastName6', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (7, N'Employee Nbr 7', 7, N'Name 7', N'LastName7', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (8, N'Employee Nbr 8', 8, N'Name 8', N'LastName8', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (9, N'Employee Nbr 9', 9, N'Name 9', N'LastName9', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (10, N'Employee Nbr 10', 10, N'Name 10', N'LastName10', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (11, N'Employee Nbr 11', 11, N'Name 11', N'LastName11', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (12, N'Employee Nbr 12', 12, N'Name 12', N'LastName12', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (13, N'Employee Nbr 13', 13, N'Name 13', N'LastName13', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (14, N'Employee Nbr 14', 14, N'Name 14', N'LastName14', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (15, N'Employee Nbr 15', 15, N'Name 15', N'LastName15', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (16, N'Employee Nbr 16', 16, N'Name 16', N'LastName16', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (17, N'Employee Nbr 17', 17, N'Name 17', N'LastName17', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (18, N'Employee Nbr 18', 18, N'Name 18', N'LastName18', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (19, N'Employee Nbr 19', 19, N'Name 19', N'LastName19', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (20, N'Employee Nbr 20', 20, N'Name 20', N'LastName20', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (21, N'Employee Nbr 21', 21, N'Name 21', N'LastName21', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (22, N'Employee Nbr 22', 22, N'Name 22', N'LastName22', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (23, N'Employee Nbr 23', 23, N'Name 23', N'LastName23', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (24, N'Employee Nbr 24', 24, N'Name 24', N'LastName24', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (25, N'Employee Nbr 25', 25, N'Name 25', N'LastName25', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (26, N'Employee Nbr 26', 26, N'Name 26', N'LastName26', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (27, N'Employee Nbr 27', 27, N'Name 27', N'LastName27', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (28, N'Employee Nbr 28', 28, N'Name 28', N'LastName28', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (29, N'Employee Nbr 29', 29, N'Name 29', N'LastName29', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (30, N'Employee Nbr 30', 30, N'Name 30', N'LastName30', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (31, N'Employee Nbr 31', 31, N'Name 31', N'LastName31', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (32, N'Employee Nbr 32', 32, N'Name 32', N'LastName32', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (33, N'Employee Nbr 33', 33, N'Name 33', N'LastName33', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (34, N'Employee Nbr 34', 34, N'Name 34', N'LastName34', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (35, N'Employee Nbr 35', 35, N'Name 35', N'LastName35', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (36, N'Employee Nbr 36', 36, N'Name 36', N'LastName36', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (37, N'Employee Nbr 37', 37, N'Name 37', N'LastName37', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (38, N'Employee Nbr 38', 38, N'Name 38', N'LastName38', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (39, N'Employee Nbr 39', 39, N'Name 39', N'LastName39', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (40, N'Employee Nbr 40', 40, N'Name 40', N'LastName40', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (41, N'Employee Nbr 41', 41, N'Name 41', N'LastName41', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (42, N'Employee Nbr 42', 42, N'Name 42', N'LastName42', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (43, N'Employee Nbr 43', 43, N'Name 43', N'LastName43', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (44, N'Employee Nbr 44', 44, N'Name 44', N'LastName44', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (45, N'Employee Nbr 45', 45, N'Name 45', N'LastName45', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (46, N'Employee Nbr 46', 46, N'Name 46', N'LastName46', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (47, N'Employee Nbr 47', 47, N'Name 47', N'LastName47', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (48, N'Employee Nbr 48', 48, N'Name 48', N'LastName48', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (49, N'Employee Nbr 49', 49, N'Name 49', N'LastName49', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (50, N'Employee Nbr 50', 50, N'Name 50', N'LastName50', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (51, N'Employee Nbr 51', 51, N'Name 51', N'LastName51', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (52, N'Employee Nbr 52', 52, N'Name 52', N'LastName52', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (53, N'Employee Nbr 53', 53, N'Name 53', N'LastName53', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (54, N'Employee Nbr 54', 54, N'Name 54', N'LastName54', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (55, N'Employee Nbr 55', 55, N'Name 55', N'LastName55', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (56, N'Employee Nbr 56', 56, N'Name 56', N'LastName56', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (57, N'Employee Nbr 57', 57, N'Name 57', N'LastName57', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (58, N'Employee Nbr 58', 58, N'Name 58', N'LastName58', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (59, N'Employee Nbr 59', 59, N'Name 59', N'LastName59', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (60, N'Employee Nbr 60', 60, N'Name 60', N'LastName60', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (61, N'Employee Nbr 61', 61, N'Name 61', N'LastName61', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (62, N'Employee Nbr 62', 62, N'Name 62', N'LastName62', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (63, N'Employee Nbr 63', 63, N'Name 63', N'LastName63', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (64, N'Employee Nbr 64', 64, N'Name 64', N'LastName64', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (65, N'Employee Nbr 65', 65, N'Name 65', N'LastName65', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (66, N'Employee Nbr 66', 66, N'Name 66', N'LastName66', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (67, N'Employee Nbr 67', 67, N'Name 67', N'LastName67', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (68, N'Employee Nbr 68', 68, N'Name 68', N'LastName68', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (69, N'Employee Nbr 69', 69, N'Name 69', N'LastName69', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (70, N'Employee Nbr 70', 70, N'Name 70', N'LastName70', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (71, N'Employee Nbr 71', 71, N'Name 71', N'LastName71', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (72, N'Employee Nbr 72', 72, N'Name 72', N'LastName72', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (73, N'Employee Nbr 73', 73, N'Name 73', N'LastName73', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (74, N'Employee Nbr 74', 74, N'Name 74', N'LastName74', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (75, N'Employee Nbr 75', 75, N'Name 75', N'LastName75', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (76, N'Employee Nbr 76', 76, N'Name 76', N'LastName76', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (77, N'Employee Nbr 77', 77, N'Name 77', N'LastName77', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (78, N'Employee Nbr 78', 78, N'Name 78', N'LastName78', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (79, N'Employee Nbr 79', 79, N'Name 79', N'LastName79', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (80, N'Employee Nbr 80', 80, N'Name 80', N'LastName80', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (81, N'Employee Nbr 81', 81, N'Name 81', N'LastName81', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (82, N'Employee Nbr 82', 82, N'Name 82', N'LastName82', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (83, N'Employee Nbr 83', 83, N'Name 83', N'LastName83', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (84, N'Employee Nbr 84', 84, N'Name 84', N'LastName84', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (85, N'Employee Nbr 85', 85, N'Name 85', N'LastName85', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (86, N'Employee Nbr 86', 86, N'Name 86', N'LastName86', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (87, N'Employee Nbr 87', 87, N'Name 87', N'LastName87', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (88, N'Employee Nbr 88', 88, N'Name 88', N'LastName88', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (89, N'Employee Nbr 89', 89, N'Name 89', N'LastName89', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (90, N'Employee Nbr 90', 90, N'Name 90', N'LastName90', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (91, N'Employee Nbr 91', 91, N'Name 91', N'LastName91', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (92, N'Employee Nbr 92', 92, N'Name 92', N'LastName92', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (93, N'Employee Nbr 93', 93, N'Name 93', N'LastName93', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (94, N'Employee Nbr 94', 94, N'Name 94', N'LastName94', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (95, N'Employee Nbr 95', 95, N'Name 95', N'LastName95', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (96, N'Employee Nbr 96', 96, N'Name 96', N'LastName96', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (97, N'Employee Nbr 97', 97, N'Name 97', N'LastName97', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (98, N'Employee Nbr 98', 98, N'Name 98', N'LastName98', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (99, N'Employee Nbr 99', 99, N'Name 99', N'LastName99', 0)
GO
INSERT [dbo].[Operators] ([Id], [Document], [EmployeeNbr], [FirstName], [LastName], [IsActive]) VALUES (100, N'Employee Nbr 100', 100, N'Name 100', N'LastName100', 0)
GO
SET IDENTITY_INSERT [dbo].[Operators] OFF
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (1, 0, N'1234', 2, N'UserName1')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (2, 0, N'1234', 1, N'UserName2')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (3, 0, N'1234', 2, N'UserName3')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (4, 0, N'1234', 1, N'UserName4')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (5, 0, N'1234', 2, N'UserName5')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (6, 0, N'1234', 2, N'UserName6')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (7, 0, N'1234', 1, N'UserName7')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (8, 0, N'1234', 2, N'UserName8')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (9, 0, N'1234', 2, N'UserName9')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (10, 0, N'1234', 1, N'UserName10')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (11, 0, N'1234', 2, N'UserName11')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (12, 0, N'1234', 1, N'UserName12')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (13, 0, N'1234', 1, N'UserName13')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (14, 0, N'1234', 1, N'UserName14')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (15, 0, N'1234', 1, N'UserName15')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (16, 0, N'1234', 1, N'UserName16')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (17, 0, N'1234', 1, N'UserName17')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (18, 0, N'1234', 2, N'UserName18')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (19, 0, N'1234', 2, N'UserName19')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (20, 0, N'1234', 2, N'UserName20')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (21, 0, N'1234', 1, N'UserName21')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (22, 0, N'1234', 1, N'UserName22')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (23, 0, N'1234', 1, N'UserName23')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (24, 0, N'1234', 2, N'UserName24')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (25, 0, N'1234', 2, N'UserName25')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (26, 0, N'1234', 1, N'UserName26')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (27, 0, N'1234', 1, N'UserName27')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (28, 0, N'1234', 2, N'UserName28')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (29, 0, N'1234', 2, N'UserName29')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (30, 0, N'1234', 1, N'UserName30')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (31, 0, N'1234', 1, N'UserName31')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (32, 0, N'1234', 2, N'UserName32')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (33, 0, N'1234', 2, N'UserName33')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (34, 0, N'1234', 1, N'UserName34')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (35, 0, N'1234', 2, N'UserName35')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (36, 0, N'1234', 2, N'UserName36')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (37, 0, N'1234', 2, N'UserName37')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (38, 0, N'1234', 1, N'UserName38')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (39, 0, N'1234', 2, N'UserName39')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (40, 0, N'1234', 2, N'UserName40')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (41, 0, N'1234', 2, N'UserName41')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (42, 0, N'1234', 2, N'UserName42')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (43, 0, N'1234', 2, N'UserName43')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (44, 0, N'1234', 2, N'UserName44')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (45, 0, N'1234', 1, N'UserName45')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (46, 0, N'1234', 2, N'UserName46')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (47, 0, N'1234', 2, N'UserName47')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (48, 0, N'1234', 1, N'UserName48')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (49, 0, N'1234', 2, N'UserName49')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (50, 0, N'1234', 1, N'UserName50')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (51, 0, N'1234', 1, N'UserName51')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (52, 0, N'1234', 1, N'UserName52')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (53, 0, N'1234', 1, N'UserName53')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (54, 0, N'1234', 1, N'UserName54')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (55, 0, N'1234', 1, N'UserName55')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (56, 0, N'1234', 1, N'UserName56')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (57, 0, N'1234', 1, N'UserName57')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (58, 0, N'1234', 2, N'UserName58')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (59, 0, N'1234', 2, N'UserName59')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (60, 0, N'1234', 2, N'UserName60')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (61, 0, N'1234', 2, N'UserName61')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (62, 0, N'1234', 1, N'UserName62')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (63, 0, N'1234', 2, N'UserName63')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (64, 0, N'1234', 2, N'UserName64')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (65, 0, N'1234', 2, N'UserName65')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (66, 0, N'1234', 1, N'UserName66')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (67, 0, N'1234', 1, N'UserName67')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (68, 0, N'1234', 1, N'UserName68')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (69, 0, N'1234', 2, N'UserName69')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (70, 0, N'1234', 2, N'UserName70')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (71, 0, N'1234', 1, N'UserName71')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (72, 0, N'1234', 2, N'UserName72')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (73, 0, N'1234', 2, N'UserName73')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (74, 0, N'1234', 1, N'UserName74')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (75, 0, N'1234', 1, N'UserName75')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (76, 0, N'1234', 1, N'UserName76')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (77, 0, N'1234', 1, N'UserName77')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (78, 0, N'1234', 1, N'UserName78')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (79, 0, N'1234', 1, N'UserName79')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (80, 0, N'1234', 1, N'UserName80')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (81, 0, N'1234', 1, N'UserName81')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (82, 0, N'1234', 1, N'UserName82')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (83, 0, N'1234', 2, N'UserName83')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (84, 0, N'1234', 1, N'UserName84')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (85, 0, N'1234', 2, N'UserName85')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (86, 0, N'1234', 1, N'UserName86')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (87, 0, N'1234', 2, N'UserName87')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (88, 0, N'1234', 1, N'UserName88')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (89, 0, N'1234', 1, N'UserName89')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (90, 0, N'1234', 1, N'UserName90')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (91, 0, N'1234', 1, N'UserName91')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (92, 0, N'1234', 2, N'UserName92')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (93, 0, N'1234', 2, N'UserName93')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (94, 0, N'1234', 2, N'UserName94')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (95, 0, N'1234', 2, N'UserName95')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (96, 0, N'1234', 2, N'UserName96')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (97, 0, N'1234', 1, N'UserName97')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (98, 0, N'1234', 2, N'UserName98')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (99, 0, N'1234', 2, N'UserName99')
GO
INSERT [dbo].[Users] ([Id], [IsActive], [Password], [Role], [UserName]) VALUES (100, 0, N'1234', 2, N'UserName100')
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Code]    Script Date: 5/17/2018 9:47:43 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Clients]') AND name = N'IX_Code')
CREATE NONCLUSTERED INDEX [IX_Code] ON [dbo].[Clients]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Name]    Script Date: 5/17/2018 9:47:43 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Clients]') AND name = N'IX_Name')
CREATE NONCLUSTERED INDEX [IX_Name] ON [dbo].[Clients]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Document]    Script Date: 5/17/2018 9:47:43 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND name = N'IX_Document')
CREATE NONCLUSTERED INDEX [IX_Document] ON [dbo].[Operators]
(
	[Document] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmployeeNbr]    Script Date: 5/17/2018 9:47:43 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND name = N'IX_EmployeeNbr')
CREATE NONCLUSTERED INDEX [IX_EmployeeNbr] ON [dbo].[Operators]
(
	[EmployeeNbr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FirstName]    Script Date: 5/17/2018 9:47:43 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND name = N'IX_FirstName')
CREATE NONCLUSTERED INDEX [IX_FirstName] ON [dbo].[Operators]
(
	[FirstName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IsActive]    Script Date: 5/17/2018 9:47:43 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND name = N'IX_IsActive')
CREATE NONCLUSTERED INDEX [IX_IsActive] ON [dbo].[Operators]
(
	[IsActive] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_LastName]    Script Date: 5/17/2018 9:47:43 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operators]') AND name = N'IX_LastName')
CREATE NONCLUSTERED INDEX [IX_LastName] ON [dbo].[Operators]
(
	[LastName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Id]    Script Date: 5/17/2018 9:47:43 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_Id')
CREATE NONCLUSTERED INDEX [IX_Id] ON [dbo].[Users]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_IsActive]    Script Date: 5/17/2018 9:47:43 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_IsActive')
CREATE NONCLUSTERED INDEX [IX_IsActive] ON [dbo].[Users]
(
	[IsActive] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Role]    Script Date: 5/17/2018 9:47:43 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_Role')
CREATE NONCLUSTERED INDEX [IX_Role] ON [dbo].[Users]
(
	[Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserName]    Script Date: 5/17/2018 9:47:43 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_UserName')
CREATE NONCLUSTERED INDEX [IX_UserName] ON [dbo].[Users]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Users_dbo.Operators_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.Operators_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Operators] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Users_dbo.Operators_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.Operators_Id]
GO
