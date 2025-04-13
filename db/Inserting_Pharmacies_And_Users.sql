-- Enable manual insertion of Ids for the Pharmacies table
SET IDENTITY_INSERT Pharmacies ON;

-- Insert 5 pharmacies with forced Id and valid AddressId
INSERT INTO Pharmacies (Id, Name, Email, Phone, CreatedAt, AddressId)
VALUES 
(1, 'Pharmacy 1', 'pharmacy1@example.com', '+201234567890', '2024-01-01 00:00:00', 1),  -- Cairo address Id 1
(2, 'Pharmacy 2', 'pharmacy2@example.com', '+201234567891', '2024-02-15 00:00:00', 2),  -- Giza address Id 2
(3, 'Pharmacy 3', 'pharmacy3@example.com', '+201234567892', '2024-03-10 00:00:00', 3),  -- Alexandria address Id 3
(4, 'Pharmacy 4', 'pharmacy4@example.com', '+201234567893', '2024-04-05 00:00:00', 4),  -- Los Angeles address Id 4
(5, 'Pharmacy 5', 'pharmacy5@example.com', '+201234567894', '2024-05-20 00:00:00', 5); -- London address Id 5

-- Disable manual insertion of Ids for the Pharmacies table
SET IDENTITY_INSERT Pharmacies OFF;

GO

-- Enable manual insertion of Ids for the Customers table
SET IDENTITY_INSERT Customers ON;

-- Insert 10 customers with forced Id and valid AddressId
INSERT INTO Customers (Id, Name, Email, Phone, CreatedAt, AddressId)
VALUES 
(1, 'Customer 1', 'customer1@example.com', '+201234567800', '2024-01-01 00:00:00', 6),  -- Cairo address Id 6
(2, 'Customer 2', 'customer2@example.com', '+201234567801', '2024-02-10 00:00:00', 7),  -- Giza address Id 7
(3, 'Customer 3', 'customer3@example.com', '+201234567802', '2024-03-15 00:00:00', 8),  -- Alexandria address Id 8
(4, 'Customer 4', 'customer4@example.com', '+201234567803', '2024-04-01 00:00:00', 9),  -- Los Angeles address Id 9
(5, 'Customer 5', 'customer5@example.com', '+201234567804', '2024-05-05 00:00:00', 10), -- London address Id 10
(6, 'Customer 6', 'customer6@example.com', '+201234567805', '2024-06-10 00:00:00', 11),  -- Cairo address Id 11
(7, 'Customer 7', 'customer7@example.com', '+201234567806', '2024-07-15 00:00:00', 12),  -- Giza address Id 12
(8, 'Customer 8', 'customer8@example.com', '+201234567807', '2024-08-01 00:00:00', 13),  -- Alexandria address Id 13
(9, 'Customer 9', 'customer9@example.com', '+201234567808', '2024-09-10 00:00:00', 14),  -- Los Angeles address Id 14
(10, 'Customer 10', 'customer10@example.com', '+201234567809', '2024-10-20 00:00:00', 15); -- London address Id 15

-- Disable manual insertion of Ids for the Customers table
SET IDENTITY_INSERT Customers OFF;
