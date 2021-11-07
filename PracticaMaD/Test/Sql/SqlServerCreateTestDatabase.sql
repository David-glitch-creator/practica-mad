/* 
 * SQL Server Script
 * 
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */

 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\software\Mad\DataBase'
 

USE [master]

/****** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'photogramTest')
DROP DATABASE [photogramTest]



/* DataBase Creation */
                                  
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [photogramTest] 
    ON PRIMARY ( NAME = photogramTest, FILENAME = "' + @Default_DB_Path + N'photogramTest.mdf")
    LOG ON ( NAME = photogramTest_log, FILENAME = "' + @Default_DB_Path + N'photogramTest_log.ldf")'

EXEC(@sql)
PRINT N'Database created.'
PRINT N'Done'
GO

/* 
 * Drop tables.                                                             
 * NOTE: before dropping a table (when re-executing the script), the tables 
 * having columns acting as foreign keys of the table to be dropped must be 
 * dropped first (otherwise, the corresponding checks on those tables could 
 * not be done).                                                            
 */
 
 USE photogramTest

 /* Drop Table Comment if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Follow]') 
AND type in ('U')) DROP TABLE [Follow]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Likes]') 
AND type in ('U')) DROP TABLE [Likes]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') 
AND type in ('U')) DROP TABLE [Comment]
GO

/* Drop Table ImageEntity if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[ImageEntity]') 
AND type in ('U')) DROP TABLE [ImageEntity]
GO
 
/* Drop Table Category if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') 
AND type in ('U')) DROP TABLE [Category]
GO

/* Drop Table UserProfile if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') 
AND type in ('U')) DROP TABLE [UserProfile]
GO


/*
 * Create tables.
 * Tables are created. Indexes required for the 
 * most common operations are also defined.
 */

/*  UserProfile */

CREATE TABLE UserProfile (
	userId BIGINT IDENTITY(1,1) UNIQUE NOT NULL,
	loginName VARCHAR(30) UNIQUE NOT NULL,
	enPassword VARCHAR(50) NOT NULL,
	firstName VARCHAR(30) NOT NULL,
	lastName VARCHAR(40) NOT NULL,
	email VARCHAR(60) UNIQUE NOT NULL,
	lang VARCHAR(2) NOT NULL,
	country VARCHAR(2) NOT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (userId)
)


PRINT N'Table UserProfile created.'
GO

/*  Category */

CREATE TABLE Category (
	categoryId BIGINT IDENTITY(1,1) UNIQUE NOT NULL,
	categoryName VARCHAR(30) UNIQUE NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (categoryId)
)

PRINT N'Table Category created.'
GO

/*  ImageEntity */
CREATE TABLE ImageEntity (
	imageId BIGINT IDENTITY(1,1) UNIQUE NOT NULL,
	title VARCHAR(30) UNIQUE NOT NULL,
	uploadDate DATETIME NOT NULL,
	aperture VARCHAR(30),
	exposureTime VARCHAR(30),
	iso VARCHAR(30),
	whiteBalance VARCHAR(30),
	imageFile VARBINARY(MAX) NOT NULL,
	author BIGINT NOT NULL,
	categoryId BIGINT NOT NULL,
	
	CONSTRAINT [PK_Image] PRIMARY KEY (imageId),
	
	CONSTRAINT [FK_ImageAuthor] FOREIGN KEY (author)
		REFERENCES UserProfile(userId) ON DELETE CASCADE,
		
	CONSTRAINT [FK_ImageCategory] FOREIGN KEY (categoryId)
		REFERENCES Category (categoryId) ON DELETE CASCADE
)

PRINT N'Table ImageEntity created.'
GO

/*  Comment */
CREATE TABLE Comment (
	commentId BIGINT IDENTITY(1,1) UNIQUE NOT NULL,
	author BIGINT NOT NULL,
	imageId BIGINT NOT NULL,
	commentText VARCHAR(60) NOT NULL,
	
	CONSTRAINT [PK_Comment] PRIMARY KEY (commentId),
	
	CONSTRAINT [FK_CommentAuthor] FOREIGN KEY (author)
		REFERENCES UserProfile(userId) ON DELETE CASCADE,
		
	CONSTRAINT [FK_CommentedImage] FOREIGN KEY (imageId)
		REFERENCES ImageEntity (imageId) /*ON DELETE CASCADE*/
)

PRINT N'Table Comment created.'
GO

/*  Likes */
CREATE TABLE Likes (
	imageId BIGINT NOT NULL,
	userId BIGINT NOT NULL,

	CONSTRAINT [PK_Likes] PRIMARY KEY (imageId, userId),
		
	CONSTRAINT [FK_LikedImage] FOREIGN KEY (imageId)
		REFERENCES ImageEntity (imageId) ON DELETE CASCADE,
	
	CONSTRAINT [FK_UserWhoLikes] FOREIGN KEY (userId)
		REFERENCES UserProfile(userId) /*ON DELETE CASCADE*/,
)

PRINT N'Table Likes created.'
GO

/*  Follow */
CREATE TABLE Follow (
	followedUserId BIGINT NOT NULL,
	followerUserId BIGINT NOT NULL,

	CONSTRAINT [PK_Follow] PRIMARY KEY (followedUserId, followerUserId),
		
	CONSTRAINT [FK_FollowedUser] FOREIGN KEY (followedUserId)
		REFERENCES UserProfile (userId) ON DELETE CASCADE,
	
	CONSTRAINT [FK_FollowerUser] FOREIGN KEY (followerUserId)
		REFERENCES UserProfile (userId) /*ON DELETE CASCADE*/,
)

PRINT N'Table Follow created.'
GO

PRINT N'Done'