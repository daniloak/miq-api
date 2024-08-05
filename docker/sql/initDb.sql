IF db_id('SBOCorcerama') IS NULL 
    CREATE DATABASE SBOCorcerama
GO

USE SBOCorcerama;
GO

IF OBJECT_ID('dbo.OITM', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.OITM (
        ItemCode NVARCHAR(50),
        U_MIN_VENDA FLOAT NOT NULL,
        FrgnName NVARCHAR(50),
        U_Vales FLOAT NOT NULL,
        ItemName NVARCHAR(255) NOT NULL,
        PRIMARY KEY (ItemCode)
    );
    PRINT 'Table dbo.OITM created successfully.';

    INSERT INTO dbo.OITM (ItemCode, U_MIN_VENDA, FrgnName, U_Vales, ItemName)
    VALUES 
        ('402206', 10, 'Vendor1', 5, 'Ceramic Tile A'),
        ('402207', 15, 'Vendor2', 10, 'Ceramic Tile B'),
        ('402208', 20, 'Vendor3', 15, 'Ceramic Tile C');
END
GO

IF OBJECT_ID('dbo.[@SPBZ_CADVALE]', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.[@SPBZ_CADVALE] (
        U_Verified BIT,
        U_Status VARCHAR(100),
        U_ExternalID int
    );
    PRINT 'Table dbo.@SPBZ_CADVALE created successfully.';

    INSERT INTO dbo.[@SPBZ_CADVALE] (U_Verified, U_ExternalID, U_Status)
    VALUES
        (0, 1, 'O'),
        (1, 2, 'C'),
        (1, 3, 'V');
END
GO

IF OBJECT_ID('dbo.[OPLN]', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.OPLN (
        U_ListaIPAQ INT NOT NULL,
        ListNum INT NOT NULL,
        PRIMARY KEY (U_ListaIPAQ, ListNum)
    );
    PRINT 'Table dbo.OPLN created successfully.';

    INSERT INTO dbo.OPLN (U_ListaIPAQ, ListNum)
    VALUES
        (1, 388),
        (0, 333),
        (0, 21);
END
GO

IF OBJECT_ID('dbo.[OITW]', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.OITW (
        ItemCode NVARCHAR(50),
        WhsCode NVARCHAR(50) NOT NULL,
        OnHand FLOAT NOT NULL DEFAULT 0,
        IsCommited FLOAT NOT NULL DEFAULT 0,
        U_BO char NOT NULL DEFAULT 'N',
        PRIMARY KEY (ItemCode, WhsCode)
    );
    PRINT 'Table dbo.OITW created successfully.';

    INSERT INTO dbo.OITW (ItemCode, WhsCode, OnHand, IsCommited, U_BO)
    VALUES
        ('402206', '01', 82, 81, 'N'),
        ('402206', '03', 82, 81, 'N'),
        ('402207', '01', 150, 5, 'N'),
        ('402208', '01', 80, 20, 'S'),
        ('402208', '03', 80, 20, 'S');
END
GO

IF OBJECT_ID('dbo.[ITM1]', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ITM1 (
        PriceList INT NOT NULL,
        ItemCode NVARCHAR(50),
        Price FLOAT NOT NULL DEFAULT 0,
        PRIMARY KEY (PriceList, ItemCode)
    );
    PRINT 'Table dbo.ITM1 created successfully.';

    INSERT INTO dbo.ITM1 (PriceList, ItemCode, Price)
    VALUES
        (388, '402206', 10.50),
        (388, '402207', 12.00),
        (388, '402208', 11.25);
END
GO

IF OBJECT_ID('dbo.[OCRD]', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.OCRD (
        CardCode NVARCHAR(15) PRIMARY KEY,
        CardName NVARCHAR(100) NOT NULL,
        GroupCode INT NOT NULL,
        LicTradNum NVARCHAR(32),
        E_Mail NVARCHAR(100),
        Address NVARCHAR(254),
        ZipCode NVARCHAR(20),
        U_Cnpj NVARCHAR(20),
        CardType CHAR(1) CHECK (CardType IN ('C', 'S', 'L')),
        U_ActiveMiq BIT NOT NULL DEFAULT 1
    );
    PRINT 'Table dbo.OCRD created successfully.';

    INSERT INTO dbo.OCRD (CardCode, CardName, GroupCode, LicTradNum, E_Mail, Address, ZipCode, U_Cnpj, CardType, U_ActiveMiq)
    VALUES
        ('C0001', 'Company A', 100, '12.345.678/0001-91', 'contact@companya.com', 'Street A, 123', '12345-678', '12.345.678/0001-91', 'C', 1),
        ('C0002', 'Company B', 100, '87.654.321/0002-54', 'contact@companyb.com', 'Street B, 456', '23456-789', '87.654.321/0002-54', 'C', 1),
        ('C0003', 'Company C', 101, '23.456.789/0003-11', 'contact@companyc.com', 'Street C, 789', '34567-890', '23.456.789/0003-11', 'C', 1);
END
GO