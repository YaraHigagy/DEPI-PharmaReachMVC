-- Insert 10 rows into MedicinePharmacyCanBeFree table with random selection of medicines and pharmacies
INSERT INTO MedicinePharmacyCanBeFrees (PharmacyId, MedicineId, AvailableQuantity)
VALUES
(1, 4, 50), 
(2, 16, 100), 
(3, 34, 70),
(4, 37, 120), 
(5, 45, 60), 
(1, 5, 80),
(2, 9, 50), 
(3, 15, 30), 
(4, 7, 110),
(5, 30, 90);

GO

-- Insert 15 rows into MedicinePharmacyIsFree table
INSERT INTO MedicinePharmacyIsFrees (PharmacyId, MedicineId, AvailableQuantity)
VALUES
(1, 5, 40),
(2, 9, 60),
(3, 15, 35),
(1, 22, 90),
(4, 7, 55),
(5, 30, 70),
(3, 25, 45),
(4, 3, 80),
(2, 12, 65),
(5, 10, 50),
(1, 17, 75),
(2, 8, 85),
(3, 20, 95),
(4, 13, 100),
(5, 28, 120);
