-- 1. Add ApplicationUserId column to Pharmacies table
ALTER TABLE Pharmacies
ADD ApplicationUserId NVARCHAR(450);

-- 2. Add ApplicationUserId column to Customers table
ALTER TABLE Customers
ADD ApplicationUserId NVARCHAR(450);

GO

UPDATE Pharmacies
SET ApplicationUserId = 'c52a5c9b-56de-4e8f-b7b5-1d3b4a8f15d1'
WHERE Id = 1;

UPDATE Pharmacies
SET ApplicationUserId = 'f914b49d-2f9d-466c-9c77-8135cf799c11'
WHERE Id = 2;

UPDATE Pharmacies
SET ApplicationUserId = '7d1a5805-8bd8-4f26-9f93-1a9396f621a2'
WHERE Id = 3;

UPDATE Pharmacies
SET ApplicationUserId = '6ecba2c9-f3e5-4122-b760-2a12d62a8e6d'
WHERE Id = 4;

UPDATE Pharmacies
SET ApplicationUserId = '5c713bde-5d86-4b3b-9f99-91c9cf4910a3'
WHERE Id = 5;

GO

UPDATE Customers
SET ApplicationUserId = 'customer-user-1'
WHERE Id = 1;

UPDATE Customers
SET ApplicationUserId = 'customer-user-2'
WHERE Id = 2;

UPDATE Customers
SET ApplicationUserId = 'customer-user-3'
WHERE Id = 3;

UPDATE Customers
SET ApplicationUserId = 'customer-user-4'
WHERE Id = 4;

UPDATE Customers
SET ApplicationUserId = 'customer-user-5'
WHERE Id = 5;

UPDATE Customers
SET ApplicationUserId = 'customer-user-6'
WHERE Id = 6;

UPDATE Customers
SET ApplicationUserId = 'customer-user-7'
WHERE Id = 7;

UPDATE Customers
SET ApplicationUserId = 'customer-user-8'
WHERE Id = 8;

UPDATE Customers
SET ApplicationUserId = 'customer-user-9'
WHERE Id = 9;

UPDATE Customers
SET ApplicationUserId = 'customer-user-10'
WHERE Id = 10;
