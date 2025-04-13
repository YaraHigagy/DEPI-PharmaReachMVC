-- تعطيل القيود
ALTER TABLE Addresses NOCHECK CONSTRAINT ALL;
ALTER TABLE Cities NOCHECK CONSTRAINT ALL;
ALTER TABLE Countries NOCHECK CONSTRAINT ALL;

GO

-- حذف البيانات
DELETE FROM Addresses;
DELETE FROM Cities;
DELETE FROM Countries;

GO

-- إعادة تفعيل القيود
ALTER TABLE Addresses CHECK CONSTRAINT ALL;
ALTER TABLE Cities CHECK CONSTRAINT ALL;
ALTER TABLE Countries CHECK CONSTRAINT ALL;

GO

-- Enable manual insertion of IDs
SET IDENTITY_INSERT Countries ON;

-- Insert countries with forced IDs
INSERT INTO Countries (Id, Name)
VALUES (1, 'Egypt'),
       (2, 'Saudi Arabia'),
       (3, 'UAE');

-- Disable manual insertion of IDs
SET IDENTITY_INSERT Countries OFF;

GO

-- Enable manual insertion of Id for the Cities table
SET IDENTITY_INSERT Cities ON;

-- Insert Cities with forced Id and valid CountryIds
INSERT INTO Cities (Id, Name, CountryId) 
VALUES 
(1, 'Cairo', 1),        -- Egypt (CountryId 1)
(2, 'Giza', 1),         -- Egypt (CountryId 1)
(3, 'Alexandria', 1),   -- Egypt (CountryId 1)
(4, 'Los Angeles', 2),  -- USA (CountryId 2)
(5, 'London', 3);       -- UK (CountryId 3)

-- Disable manual insertion of Id for the Cities table
SET IDENTITY_INSERT Cities OFF;

GO

-- Enable manual insertion of Ids for the Addresses table
SET IDENTITY_INSERT Addresses ON;

-- Insert addresses with forced Id, valid CountryId and CityId
INSERT INTO Addresses (Id, CountryId, CityId, Street, Building, Apartment, Notes, DeliveryInstructions)
VALUES 
(1, 1, 1, '123 Nile St', 'Building A', 'Apt 101', 'Near the river', 'Leave at the door'),  -- Cairo, Egypt
(2, 1, 2, '456 Giza Road', 'Building B', 'Apt 202', 'Close to pyramids', 'Ring the bell'), -- Giza, Egypt
(3, 1, 3, '789 Alexandria Ave', 'Building C', 'Apt 303', 'Near the corniche', 'Call on arrival'), -- Alexandria, Egypt
(4, 2, 4, '987 Sunset Blvd', 'Building D', 'Apt 404', 'Near Hollywood', 'Leave with doorman'), -- Los Angeles, USA
(5, 3, 5, '321 Baker Street', 'Building E', 'Apt 505', 'Near Sherlock Holmes Museum', 'Call ahead'), -- London, UK
(6, 1, 1, '111 Tahrir Square', 'Building F', 'Apt 606', 'Near the Egyptian Museum', 'Ring the bell at the gate'),  -- Cairo, Egypt
(7, 1, 2, '222 Giza Street', 'Building G', 'Apt 707', 'Near the Sphinx', 'Leave under the porch'), -- Giza, Egypt
(8, 1, 3, '333 Alexandria Blvd', 'Building H', 'Apt 808', 'Near the library', 'Leave it with the concierge'), -- Alexandria, Egypt
(9, 2, 4, '555 Beverly Hills', 'Building I', 'Apt 909', 'Close to Rodeo Drive', 'Leave with the security'), -- Los Angeles, USA
(10, 3, 5, '444 Piccadilly St', 'Building J', 'Apt 1010', 'Near Buckingham Palace', 'Leave at front door'), -- London, UK
(11, 1, 1, '666 Zamalek Street', 'Building K', 'Apt 1111', 'Near the Cairo Opera House', 'Call upon arrival'), -- Cairo, Egypt
(12, 1, 2, '777 Mohandessin St', 'Building L', 'Apt 1212', 'Close to the Nile River', 'Leave it with the doorman'), -- Giza, Egypt
(13, 1, 3, '888 Smouha St', 'Building M', 'Apt 1313', 'Near Alexandria Library', 'Ring bell when you arrive'), -- Alexandria, Egypt
(14, 2, 4, '666 Hollywood Blvd', 'Building N', 'Apt 1414', 'Near the Walk of Fame', 'Leave at front desk'), -- Los Angeles, USA
(15, 3, 5, '555 Oxford Street', 'Building O', 'Apt 1515', 'Near Oxford Circus', 'Leave at the concierge'); -- London, UK

-- Disable manual insertion of Ids for the Addresses table
SET IDENTITY_INSERT Addresses OFF;