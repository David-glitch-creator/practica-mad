/* 
 * SQL Server Script
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */



/* 
 * Drop tables.                                                             
 * NOTE: before dropping a table (when re-executing the script), the tables 
 * having columns acting as foreign keys of the table to be dropped must be 
 * dropped first (otherwise, the corresponding checks on those tables could 
 * not be done).                                                            
 */
 
 USE photogram /* TODO PENSAR SI EL PROYECTO SE VA LLAMAR PRACTICAMAD O PHOTOGRAM (PHOTOGRAM MEJOR NO?) */

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
	login VARCHAR(30) UNIQUE NOT NULL,
	password VARCHAR(50) NOT NULL,
	firstName VARCHAR(30) NOT NULL,
	lastName VARCHAR(40) NOT NULL,
	email VARCHAR(60) UNIQUE NOT NULL,
	language VARCHAR(2) NOT NULL,
	country VARCHAR(2) NOT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (userId)
)

/*CREATE NONCLUSTERED INDEX [IX_AccountIndexByUserId] 
ON Account (accId ASC, usrId ASC) */


PRINT N'Table UserProfile created.'
GO

/*  Category */

CREATE TABLE Category (
	categoryId BIGINT IDENTITY(1,1) UNIQUE NOT NULL,
	name VARCHAR(30) UNIQUE NOT NULL,

	CONSTRAINT [PK_Category] PRIMARY KEY (categoryId)
)

PRINT N'Table Category created.'
GO

/*  ImageEntity */
/* TODO REVISAR COMO LLAMAR A LAS IMAGENES */
CREATE TABLE ImageEntity (
	imageId BIGINT IDENTITY(1,1) UNIQUE NOT NULL,
	title VARCHAR(30) UNIQUE NOT NULL,
	fechaSubida DATETIME NOT NULL,
	aperture VARCHAR(30),
	exposureTime VARCHAR(30),
	iso VARCHAR(30),
	whiteBalance VARCHAR(30),
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


/*CREATE NONCLUSTERED INDEX IX_AccountOpIndexByDate 
ON AccountOp (accOpId, date);

CREATE NONCLUSTERED INDEX IX_FK_AccountOpIndexByAccId
ON AccountOP (accId);*/

PRINT N'Done'