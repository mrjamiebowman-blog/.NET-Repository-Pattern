/* create database */

CREATE DATABASE repopattern
    WITH 
    OWNER = dbuser
    ENCODING = 'UTF8'
    CONNECTION LIMIT = -1;

GRANT ALL PRIVILEGES ON DATABASE repopattern TO dbuser;

\c repopattern

/* table: addresses */
-- Table: public.Addresses
-- DROP TABLE public."Addresses";
CREATE TABLE public."Addresses"
(
    "AddressId" bigint NOT NULL,
    "City" character(100) COLLATE pg_catalog."default" NOT NULL,
    "Country" character(100) COLLATE pg_catalog."default" NOT NULL,
    "FirstName" character(100) COLLATE pg_catalog."default" NOT NULL,
    "LastName" character(100) COLLATE pg_catalog."default" NOT NULL,
    "OnCreated" date NOT NULL,
    "Phone" character(15) COLLATE pg_catalog."default" NOT NULL,
    "PostalCode" character(11) COLLATE pg_catalog."default" NOT NULL,
    "State" character(100) COLLATE pg_catalog."default" NOT NULL,
    "Street1" character(100) COLLATE pg_catalog."default" NOT NULL,
    "Street2" character(100) COLLATE pg_catalog."default",
    "ModifiedOn" date NOT NULL,
    CONSTRAINT "Addresses_pkey" PRIMARY KEY ("AddressId")
);

ALTER TABLE public."Addresses"
    OWNER to dbuser;


/* table: customers */
-- Table: public.Customers
-- DROP TABLE public."Customers";
CREATE TABLE public."Customers"
(
    "CustomerId" bigint NOT NULL,
    "FirstName" character(100) COLLATE pg_catalog."default" NOT NULL,
    "LastName" character(100) COLLATE pg_catalog."default" NOT NULL,
    "Email" character(255) COLLATE pg_catalog."default" NOT NULL,
    "BirthDate" date NOT NULL,
    "BillingAddressId" bigint,
    "ShippingAddressId" bigint,
    CONSTRAINT "Customers_pkey" PRIMARY KEY ("CustomerId"),
    CONSTRAINT "BillingAddressFKey" FOREIGN KEY ("BillingAddressId")
        REFERENCES public."Addresses" ("AddressId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "ShippingAddressFKey" FOREIGN KEY ("ShippingAddressId")
        REFERENCES public."Addresses" ("AddressId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

ALTER TABLE public."Customers"
    OWNER to dbuser;














CREATE OR REPLACE PROCEDURE public.uspgetcustomers(firstname character, lastname character, email character, city character, state character, country character, pageno bigint DEFAULT 1, pagesize integer DEFAULT 25, sortcolumn character DEFAULT 'CustomerId'::bpchar, sortorder character DEFAULT 'ASC'::bpchar)
 LANGUAGE plpgsql
AS $procedure$
	BEGIN	
		/* both */
		FirstName := COALESCE(NULLIF(FirstName,''), NULL);
		LastName := COALESCE(NULLIF(LastName,''), NULL);
		Email := COALESCE(NULLIF(Email,''), NULL);
		City := COALESCE(NULLIF(City,''), NULL);
		State := COALESCE(NULLIF(State,''), NULL);
		Country := COALESCE(NULLIF(Country,''), NULL);
		
		/* page no. & page size */
		PageNo := COALESCE(NULLIF(PageNo,''), 1);
		PageSize := COALESCE(NULLIF(PageSize, ''), 25);
		
		/* sorting and ordering */
		SortColumn := COALESCE(NULLIF(SortColumn,''), 'LastName');
		SortOrder := COALESCE(NULLIF(SortOrder, ''), 'ASC');
	
		/* cte */			
		WITH cte_data as
		(
			select 
				c.FirstName
				from Customers
		)
		select c.FirstName from Customers;
	
		/* create temp table */
		create temp table tmpfiltereddata as select * from cte_data;


		select * from tmpfiltereddata;
	
	
--	    SELECT
--			 RowNumber
--			,CustomerId
--			,FirstName
--			,LastName
--			,Email
--			,BirthDate
--			,AddressId
--			,FirstName
--			,LastName
--			,Phone
--			,Street1
--			,Street2
--			,City
--			,State
--			,Country
--			,AddressId
--			,FirstName
--			,LastName
--			,Street1
--			,Street2
--			,City
--			,State
--			,Country
--	    FROM tmpfiltereddata d
--			LEFT JOIN Addresses ba ON ba.AddressId = d.BillingAddressId
--			LEFT JOIN Addresses sa ON sa.AddressId = d.ShippingAddressId
--		WHERE
--			RowNumber > @lFirstRec
--			AND RowNumber < @lLastRec
--				ORDER BY RowNumber ASC
----	
--		
--	    /* return dataset of info */
--	    DECLARE @PageCount INT, @TotalRowCount INT, @DIFF INT
--	
--	    SET @TotalRowCount = (SELECT COUNT(*) FROM #FilteredData)
--	    SET @PageCount = (@TotalRowCount / @PageSize)
--	
--	    SET @DIFF = (@TotalRowCount - (@PageCount * @PageSize))
--	    IF (@DIFF > 0)
--	  BEGIN
--	  SET @PageCount = @PageCount + 1
--	    END
--	
--		SELECT
--			COUNT(*) AS ItemCount
--			,@PageNo as PageNo
--			,@PageSize as PageSize
--			,@SortColumn as SortColumn
--			,@SortOrder as SortOrder
--			,@PageCount as PageCount
--		FROM #FilteredData	
--	
		
--		DROP TABLE tmpFilteredData;

				commit;
	
	END;
$procedure$
;
