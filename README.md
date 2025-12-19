This project uses Entity Framework Core – Database First Approach.
Student table schema

CREATE TABLE [dbo].[Student](
    [Id] INT IDENTITY(1,1) NOT NULL,
    [FirstName] NVARCHAR(250) NOT NULL,
    [LastName] NVARCHAR(250) NOT NULL,
    [Email] NVARCHAR(50) NOT NULL,
    [RollNumber] INT NOT NULL,
    [Gender] NVARCHAR(50) NULL,
    [ProfilePhoto] NVARCHAR(500) NULL,
    [CreatedAt] DATETIME NOT NULL,
    [UpdatedAt] NCHAR(10) NULL,
    [IsDelete] BIT NOT NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED ([Id] ASC)
);

ALTER TABLE [dbo].[Student]
ADD CONSTRAINT [DF_Student_CreatedAt] DEFAULT (GETDATE()) FOR [CreatedAt];

ALTER TABLE [dbo].[Student]
ADD CONSTRAINT [DF_Student_IsDelete] DEFAULT ((0)) FOR [IsDelete];
