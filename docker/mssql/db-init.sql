USE master
GO
CREATE DATABASE mrjb_RepoPattern
GO
USE [mrjb_RepoPattern]
GO 

/* create Customers table */
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[BillingAddressId] [int] NULL,
	[ShippingAddressId] [int] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_BillingAddresses] FOREIGN KEY([BillingAddressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO

ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_BillingAddresses]
GO

ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_ShippingAddresses] FOREIGN KEY([ShippingAddressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO

ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_ShippingAddresses]
GO

/* create Addresses table */
CREATE TABLE [dbo].[Addresses](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[Street1] [varchar](100) NOT NULL,
	[Street2] [varchar](100) NULL,
	[City] [varchar](100) NOT NULL,
	[State] [varchar](100) NOT NULL,
	[PostalCode] [varchar](11) NOT NULL,
	[Country] [varchar](100) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[OnCreated] [datetime2](7) NOT NULL,
	[ModifedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/* create Stored Procedures */
GO
-- =============================================
-- Author:		Jamie Bowman
-- Create date: 07/07/2020
-- Description:	Create Customer
-- =============================================
CREATE PROCEDURE [dbo].[uspCustomerCreate]
	 @FirstName VARCHAR(100)
	,@LastName VARCHAR(100)
	,@Email VARCHAR(255)
	,@BillingAddressId INT = NULL
	,@ShippingAddressId INT = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	INSERT INTO dbo.Customers
	(
		 FirstName
		,LastName
		,Email
		,BillingAddressId
		,ShippingAddressId
	)
	VALUES
	(
		 @FirstName
		,@LastName
		,@Email
		,@BillingAddressId
		,@ShippingAddressId
	)

	SELECT
		CustomerId
		,FirstName
		,LastName
		,Email
		,BillingAddressId
		,ShippingAddressId
	FROM dbo.Customers c
	WHERE c.CustomerId = (SELECT SCOPE_IDENTITY())
END

GO
-- =============================================
-- Author:		Jamie Bowman
-- Create date: 07/07/2020
-- Description:	Creates Address
-- =============================================
CREATE PROCEDURE [dbo].[uspAddressUpsert]
	-- Add the parameters for the stored procedure here
    @AddressId INT = NULL OUTPUT,
	@Street1 VARCHAR(100),
	@Street2 VARCHAR(100),
	@City VARCHAR(100),
	@State VARCHAR(100),
	@PostalCode VARCHAR(11),
	@Country VARCHAR(100),
	@FirstName VARCHAR(100),
	@LastName VARCHAR(100),
	@Phone VARCHAR(15)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	/* testing */
	--DECLARE @AddressId INT = 9
	--DECLARE @Street1 VARCHAR(100) = '123 Sesame St'
	--DECLARE @Street2 VARCHAR(100) = NULL
	--DECLARE @City VARCHAR(100) = 'Brooklyn'
	--DECLARE @State VARCHAR(100) = 'NY'
	--DECLARE @PostalCode VARCHAR(11) = '27104'
	--DECLARE @Country VARCHAR(100) = 'US'
	--DECLARE @FirstName VARCHAR(100) = 'Jamie'
	--DECLARE @LastName VARCHAR(100) = 'Bowman'
	--DECLARE @Phone VARCHAR(15) = '18001234567'
	/* testing */

	DECLARE @CreatedOn DATETIME2(7) = GETUTCDATE()
	DECLARE @ModifiedOn DATETIME2(7) = GETUTCDATE()

	IF OBJECT_ID('tempdb..#tmpAddr') IS NOT NULL DROP TABLE #tmpAddr

	CREATE TABLE #tmpAddr
	(
		AddressId INT,
		Street1 VARCHAR(100),
		Street2 VARCHAR(100),
		City VARCHAR(100),
		[State] VARCHAR(100),
		PostalCode VARCHAR(11),
		Country VARCHAR(100),
		FirstName VARCHAR(100),
		LastName VARCHAR(100),
		Phone VARCHAR(15)
	)

	INSERT INTO #tmpAddr VALUES (@AddressId, @Street1, @Street2, @City, @State, @PostalCode, @Country, @FirstName, @LastName, @Phone)

	;WITH cteAddr AS (
		SELECT
		     AddressId
			,Street1
			,Street2	
			,City
			,[State]
			,PostalCode
			,Country
			,FirstName
			,LastName
			,Phone
		FROM #tmpAddr
		)
	MERGE dbo.Addresses AS t
	USING cteAddr AS s
	ON 
		t.AddressId = s.AddressId
	WHEN MATCHED AND
		(
			ISNULL(t.Street1, '')	<> ISNULL(s.Street1, '')
		AND ISNULL(t.Street2, '')	<> ISNULL(s.Street2, '')
		AND ISNULL(t.City, '')		<> ISNULL(s.City, '')
		AND ISNULL(t.[State], '')	<> ISNULL(s.[State], '')
		AND ISNULL(t.PostalCode, '')<> ISNULL(s.PostalCode, '')
		AND ISNULL(t.Country, '')	<> ISNULL(s.Country, '')
		AND ISNULL(t.FirstName, '') <> ISNULL(s.FirstName, '')
		AND ISNULL(t.LastName, '')	<> ISNULL(s.LastName, '')
		AND ISNULL(t.Phone, '')		<> ISNULL(s.Phone, '')
		)
	THEN UPDATE SET
		 t.Street1 = s.Street1
		,t.Street2 = s.Street2
		,t.City = s.City
		,t.[State] = s.[State]
		,t.Country = s.Country
		,t.FirstName = s.FirstName
		,t.LastName = s.LastName
		,t.Phone = s.Phone
	WHEN NOT MATCHED
	THEN INSERT
		(
			 Street1
			,Street2
			,City
			,[State]
			,PostalCode
			,Country
			,FirstName
			,LastName
			,Phone
			,OnCreated
			,ModifedOn
		) VALUES (
			 s.Street1
			,s.Street2
			,s.City
			,s.[State]
			,s.PostalCode
			,s.Country
			,s.FirstName
			,s.LastName
			,s.Phone
			,@CreatedOn
			,@ModifiedOn
		)
	;

	SELECT @AddressId = ISNULL(@AddressId, SCOPE_IDENTITY())
    
	SELECT
		 AddressId
		,Street1
		,Street2
		,City
		,[State]
		,PostalCode
		,Country
		,FirstName
		,LastName
		,Phone
	FROM dbo.Addresses a
	WHERE a.AddressId = @AddressId

END
GO


-- =============================================
-- Author:		Jamie Bowman
-- Create date: 07/09/2020
-- Description:	Customer Delete
-- =============================================
CREATE PROCEDURE [dbo].[uspCustomerDelete]
	@CustomerId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM dbo.Customers WHERE CustomerId = @CustomerId
END


-- =============================================
-- Author:		Jamie Bowman
-- Create date: 07/09/2020
-- Description:	Returns a single Customer
-- =============================================
CREATE PROCEDURE [dbo].[uspCustomerGet]
	@CustomerId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT
		 c.CustomerId
		,c.FirstName
		,c.LastName
		,c.Email
		,ba.AddressId
		,ba.FirstName
		,ba.LastName
		,ba.Phone
		,ba.Street1
		,ba.Street2
		,ba.City
		,ba.State
		,ba.Country
		,sa.AddressId
		,sa.FirstName
		,sa.LastName
		,sa.Street1
		,sa.Street2
		,sa.City
		,sa.State
		,sa.Country
		FROM dbo.Customers c
			LEFT JOIN dbo.Addresses ba ON ba.AddressId = c.BillingAddressId
			LEFT JOIN dbo.Addresses sa ON sa.AddressId = c.ShippingAddressId
		WHERE
			c.CustomerId = @CustomerId

END



-- =============================================
-- Author:		Jamie Bowman
-- Create date: 07/09/2020
-- Description:	Search Customers
-- =============================================
CREATE PROCEDURE [dbo].[uspCustomersGet]
	@FirstName VARCHAR(100),
	@LastName VARCHAR(100),
	@Email VARCHAR(100),
	@City VARCHAR(100),
	@State VARCHAR(100),
	@Country VARCHAR(100),

	/* Pagination Parameters */
    @PageNo INT = 1,
    @PageSize INT = 25,

    /* Sorting Parameters */
    @SortColumn NVARCHAR(20) = 'CustomerId',
    @SortOrder NVARCHAR(4) = 'ASC'
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SET NOCOUNT ON;

DECLARE @DEBUG BIT = 0

/* TESTING */
--DECLARE @FirstName VARCHAR(100) = NULL
--DECLARE @LastName VARCHAR(100) = NULL
--DECLARE @Email VARCHAR(100) = NULL
--DECLARE @City VARCHAR(100) = NULL
--DECLARE @State VARCHAR(100) = NULL
--DECLARE @Country VARCHAR(100) = NULL

--/* Pagination Parameters */
--DECLARE @PageNo INT = 1
--DECLARE @PageSize INT = 100

--/* Sorting Parameters */
--DECLARE @SortColumn NVARCHAR(20) = 'FirstName'
--DECLARE @SortOrder NVARCHAR(4) = 'ASC'
/* TESTING */


/* both */
SET @FirstName = COALESCE(NULLIF(@FirstName,''), NULL)
SET @LastName = COALESCE(NULLIF(@LastName,''), NULL)
SET @Email = COALESCE(NULLIF(@Email,''), NULL)
SET @City = COALESCE(NULLIF(@City,''), NULL)
SET @State = COALESCE(NULLIF(@State,''), NULL)
SET @Country = COALESCE(NULLIF(@Country,''), NULL)

/* page no. & page size */
SET @PageNo = COALESCE(NULLIF(@PageNo,''), 1)
SET @PageSize = COALESCE(NULLIF(@PageSize, ''), 25)

/* sorting and ordering */
SET @SortColumn = COALESCE(NULLIF(@SortColumn,''), 'LastName')
SET @SortOrder = COALESCE(NULLIF(@SortOrder, ''), 'ASC')


/* local variables for modification */
DECLARE @lSortColumn NVARCHAR(20),
   @lPageNbr INT,
   @lPageSize INT,
   @lSortCol NVARCHAR(20),
   @lFirstRec INT,
   @lLastRec INT,
   @lTotalRows INT

SET @lPageNbr = @PageNo
SET @lPageSize = @PageSize
SET @lSortCol = LTRIM(RTRIM(@SortColumn))

SET @lSortColumn = LTRIM(RTRIM(@SortColumn))
SET @lFirstRec = ( @lPageNbr - 1 ) * @lPageSize
SET @lLastRec = ( @lPageNbr * @lPageSize + 1 )
SET @lTotalRows = @lFirstRec - @lLastRec + 1


/* drop temp table if it exists */
IF OBJECT_ID(N'tempdb..#FilteredData') IS NOT NULL
BEGIN
	DROP TABLE #FilteredData
END


;WITH CteData AS (
	SELECT ROW_NUMBER() OVER (ORDER BY

	/* FIRST NAME */
	CASE WHEN (@lSortColumn = 'FirstName' AND @SortOrder = 'ASC') THEN c.FirstName END ASC,
	CASE WHEN (@lSortColumn = 'FirstName' AND @SortOrder = 'DESC') THEN c.FirstName END DESC,

	/* LAST NAME */
	CASE WHEN (@lSortColumn = 'LastName' AND @SortOrder = 'ASC') THEN c.LastName END ASC,
	CASE WHEN (@lSortColumn = 'LastName' AND @SortOrder = 'DESC') THEN c.LastName END DESC,

	/* EMAIL */
	CASE WHEN (@lSortColumn = 'Email' AND @SortOrder = 'ASC') THEN c.Email END ASC,
	CASE WHEN (@lSortColumn = 'Email' AND @SortOrder = 'DESC') THEN c.Email END DESC

  )  AS RowNumber
	,c.CustomerId
	,c.FirstName
	,c.LastName
	,c.Email
	,c.BillingAddressId
	,c.ShippingAddressId
	FROM dbo.Customers c
WHERE
	/* search */
	(@FirstName IS NULL OR c.FirstName LIKE '%' + @FirstName + '%')
	AND (@LastName IS NULL OR c.LastName LIKE '%' + @LastName + '%')
	AND (@Email IS NULL OR c.Email LIKE '%' + @Email + '%')
	)

    SELECT * INTO #FilteredData FROM CteData

    SELECT
		 RowNumber
		,d.CustomerId
		,d.FirstName
		,d.LastName
		,d.Email
		,ba.AddressId
		,ba.FirstName
		,ba.LastName
		,ba.Phone
		,ba.Street1
		,ba.Street2
		,ba.City
		,ba.State
		,ba.Country
		,sa.AddressId
		,sa.FirstName
		,sa.LastName
		,sa.Street1
		,sa.Street2
		,sa.City
		,sa.State
		,sa.Country
    FROM #FilteredData d
		LEFT JOIN dbo.Addresses ba ON ba.AddressId = d.BillingAddressId
		LEFT JOIN dbo.Addresses sa ON sa.AddressId = d.ShippingAddressId
	WHERE
		RowNumber > @lFirstRec
		AND RowNumber < @lLastRec
			ORDER BY RowNumber ASC

	
    /* return dataset of info */
    DECLARE @PageCount INT, @TotalRowCount INT, @DIFF INT

    SET @TotalRowCount = (SELECT COUNT(*) FROM #FilteredData)
    SET @PageCount = (@TotalRowCount / @PageSize)

    SET @DIFF = (@TotalRowCount - (@PageCount * @PageSize))
    IF (@DIFF > 0)
  BEGIN
  SET @PageCount = @PageCount + 1
    END

	SELECT
		COUNT(*) AS ItemCount
		,@PageNo as PageNo
		,@PageSize as PageSize
		,@SortColumn as SortColumn
		,@SortOrder as SortOrder
		,@PageCount as PageCount
	FROM #FilteredData
	
END


-- =============================================
-- Author:		Jamie Bowman
-- Create date: 07/11/2020
-- Description:	Save/update Customer
-- =============================================
CREATE PROCEDURE [dbo].[uspCustomerSave]
	@CustomerId INT = NULL,
	@FirstName VARCHAR(100),
	@LastName VARCHAR(100),
	@Email VARCHAR(255),
	@BillingAddressId INT = null,
	@ShippingAddressId INT = null,
	@Upsert BIT = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	/* testing */
	--DECLARE @CustomerId INT = 1
	--DECLARE @FirstName VARCHAR(100) = 'Jamie'
	--DECLARE @LastName VARCHAR(100) = 'Bowman'
	--DECLARE @Email VARCHAR(255) = 'test@test.com'
	--DECLARE @BillingAddressId INT = null
	--DECLARE @ShippingAddressId INT = null
	/* testing */

	/* verify if customer exists */
	IF @CustomerId IS NULL AND @Upsert = 1
	BEGIN
		INSERT INTO dbo.Customers
		(
			 FirstName
			,LastName
			,Email
			,BillingAddressId
			,ShippingAddressId
		)
		VALUES
		(
			 @FirstName
			,@LastName
			,@Email
			,@BillingAddressId
			,@ShippingAddressId
		)
		END
	ELSE
		BEGIN
		UPDATE dbo.Customers 
		SET 
			 FirstName = @FirstName
			,LastName = @LastName
			,Email = @Email
			,BillingAddressId = @BillingAddressId
			,ShippingAddressId = @ShippingAddressId
		WHERE CustomerId = @CustomerId
	END

	SELECT
		 c.CustomerId
		,c.FirstName
		,c.LastName
		,c.Email
		,c.BillingAddressId
		,c.ShippingAddressId
		FROM dbo.Customers c
		WHERE c.CustomerId = @CustomerId
END



/* seed data */
DECLARE @AddressId1 INT,
		@AddressId2 INT

EXEC [dbo].[uspAddressUpsert]
	 @AddressId = @AddressId1 OUTPUT 
	,@Street1 = '123 Avenue B'
	,@Street2 = NULL
	,@City = 'Manhattan'
	,@State = 'NY'
	,@PostalCode = '10023'
	,@Country = 'USA'
	,@FirstName = 'Joan'
	,@LastName = 'Cooney'
	,@Phone = '11234567'

EXEC [dbo].[uspAddressUpsert]
	 @AddressId = @AddressId2 OUTPUT
	,@Street1 = '123 Avenue B'
	,@Street2 = NULL
	,@City = 'Manhattan'
	,@State = 'NY'
	,@PostalCode = '10023'
	,@Country = 'USA'
	,@FirstName = 'Timothy'
	,@LastName = 'Cooney'
	,@Phone = '11234567'

EXEC [dbo].[uspCustomerCreate]
	 @FirstName = 'Joan'
	,@LastName = 'Cooney'
	,@Email = 'loxoyaj737@aqumail.com'
	,@BilliningAddressId = @AddressId1
	,@ShippingAddressId = @AddressId2