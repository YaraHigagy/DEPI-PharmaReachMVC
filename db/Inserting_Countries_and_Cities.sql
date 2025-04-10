-- Inserting into Countries table
INSERT INTO Countries (Name)
VALUES 
    ('United States'),    -- Western
    ('Germany'),          -- Western
    ('France'),           -- Western
    ('Canada'),           -- Western
    ('Italy'),            -- Western
    ('Saudi Arabia'),     -- Arabic
    ('United Arab Emirates'), -- Arabic
    ('Egypt'),            -- African, Arabic
    ('Jordan'),           -- Arabic
    ('Lebanon'),          -- Arabic
    ('Iran'),             -- Eastern
    ('Turkey'),           -- Eastern
    ('South Africa'),     -- African
    ('Nigeria'),          -- African
    ('Kenya'),            -- African
    ('Ghana'),            -- African
    ('Morocco'),          -- African, Arabic
    ('Algeria'),          -- African, Arabic
    ('Tunisia');          -- African, Arabic

-- Inserting into Cities table, with foreign key referencing Countries
-- Note: The CountryId is automatically linked via the foreign key
INSERT INTO Cities (Name, CountryId)
VALUES 
    -- United States
    ('New York', 1),  
    ('Los Angeles', 1),
    
    -- Germany
    ('Berlin', 2),
    ('Munich', 2),
    
    -- France
    ('Paris', 3),
    ('Lyon', 3),
    
    -- Canada
    ('Toronto', 4),
    ('Vancouver', 4),
    
    -- Italy
    ('Rome', 5),
    ('Milan', 5),
    
    -- Saudi Arabia
    ('Riyadh', 6),
    ('Jeddah', 6),
    
    -- United Arab Emirates
    ('Dubai', 7),
    ('Abu Dhabi', 7),
    
    -- Egypt (with more cities added)
    ('Cairo', 8),
    ('Alexandria', 8),
    ('Giza', 8),
    ('Sharm El Sheikh', 8),
    ('Luxor', 8),
    ('Aswan', 8),
    ('Port Said', 8),
    ('Suez', 8),
    ('Tanta', 8),
    ('Mansoura', 8),
    ('Ismailia', 8),
    ('Fayoum', 8),
    ('Damanhur', 8),
    ('Beni Suef', 8),
    ('Minya', 8),
    ('Hurghada', 8),
    
    -- Jordan
    ('Amman', 9),
    ('Zarqa', 9),
    
    -- Lebanon
    ('Beirut', 10),
    ('Tripoli', 10),
    
    -- Iran
    ('Tehran', 11),
    ('Isfahan', 11),
    
    -- Turkey
    ('Istanbul', 12),
    ('Ankara', 12),
    
    -- South Africa
    ('Cape Town', 13),
    ('Johannesburg', 13),
    
    -- Nigeria
    ('Lagos', 14),
    ('Abuja', 14),
    
    -- Kenya
    ('Nairobi', 15),
    ('Mombasa', 15),
    
    -- Ghana
    ('Accra', 16),
    ('Kumasi', 16),
    
    -- Morocco
    ('Casablanca', 17),
    ('Marrakech', 17),
    
    -- Algeria
    ('Algiers', 18),
    ('Oran', 18),
    
    -- Tunisia
    ('Tunis', 19),
    ('Sfax', 19);