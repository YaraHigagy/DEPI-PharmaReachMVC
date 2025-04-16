-- Insert 10 rows into MedicinePharmacyCanBeFrees table with PharmacyId-MedicineId pairs from PharmacyMedicines
INSERT INTO MedicinePharmacyCanBeFrees (PharmacyId, MedicineId, AvailableQuantity)
VALUES
(1, 59, 50), 
(2, 66, 100), 
(3, 72, 70),
(4, 82, 120), 
(5, 55, 60), 
(1, 91, 80),
(2, 60, 50), 
(3, 89, 30), 
(4, 94, 110),
(5, 53, 90);

GO

-- Insert 15 rows into MedicinePharmacyIsFrees table with PharmacyId-MedicineId pairs from PharmacyMedicines
INSERT INTO MedicinePharmacyIsFrees (PharmacyId, MedicineId, AvailableQuantity)
VALUES
(1, 75, 40),
(2, 68, 60),
(3, 54, 35),
(1, 98, 90),
(4, 79, 55),
(5, 77, 70),
(3, 73, 45),
(4, 81, 80),
(2, 93, 65),
(5, 95, 50),
(1, 67, 75),
(2, 96, 85),
(3, 84, 95),
(4, 97, 100),
(5, 87, 120);
