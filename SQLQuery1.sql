-- Create CategoryTbl first
CREATE TABLE CategoryTbl (
    CatCode INT PRIMARY KEY IDENTITY,
    CatName VARCHAR(50)
);

-- Then create CustomerTbl
CREATE TABLE CustomerTbl (
    CustCode INT PRIMARY KEY IDENTITY,
    Name VARCHAR(50),
    Gender VARCHAR(10),
    Phone VARCHAR(15)
);

-- Then create ItemTbl
CREATE TABLE ItemTbl (
    ItCode INT PRIMARY KEY IDENTITY,
    ItName VARCHAR(80),
    ItCategory INT,
    Price INT,
    Stock INT,
    Manufacturer VARCHAR(20),
    FOREIGN KEY (ItCategory) REFERENCES CategoryTbl(CatCode)
);

-- Finally, create BillingTbl
CREATE TABLE BillingTbl (
    BCode INT PRIMARY KEY IDENTITY,
    BDate DATE,
    Customer INT,
    Amount INT,
    PaymentMode VARCHAR(50),
    FOREIGN KEY (Customer) REFERENCES CustomerTbl(CustCode)
);
