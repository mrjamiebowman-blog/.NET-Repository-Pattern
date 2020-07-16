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
