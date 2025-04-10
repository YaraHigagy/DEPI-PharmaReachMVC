-- Database: PharmaReach

-- Table: COUNTRY
CREATE TABLE COUNTRY (
    id INT PRIMARY KEY,
    name VARCHAR(255)
);

-- Table: CITY
CREATE TABLE CITY (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    country_id INT,
    FOREIGN KEY (country_id) REFERENCES COUNTRY(id)
);

-- Table: ADDRESS
CREATE TABLE ADDRESS (
    id INT PRIMARY KEY,
    country_id INT,
    city_id INT,
    street VARCHAR(255),
    building VARCHAR(255),
    apartment VARCHAR(255),
    notes TEXT,
    delivery_instructions TEXT,
    FOREIGN KEY (country_id) REFERENCES COUNTRY(id),
    FOREIGN KEY (city_id) REFERENCES CITY(id)
);

-- Table: CUSTOMER
CREATE TABLE CUSTOMER (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    email VARCHAR(255),
    phone VARCHAR(20),
    created_at DATETIME,
    updated_at DATETIME,
    address_id INT,
    FOREIGN KEY (address_id) REFERENCES ADDRESS(id)
);

-- Table: PHARMACY
CREATE TABLE PHARMACY (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    email VARCHAR(255),
    phone VARCHAR(20),
    created_at DATETIME,
    updated_at DATETIME,
    address_id INT,
    FOREIGN KEY (address_id) REFERENCES ADDRESS(id)
);

-- Table: MEDICINE
CREATE TABLE MEDICINE (
    id INT PRIMARY KEY,
    name VARCHAR(255),
    description TEXT,
    price FLOAT,
    image_url VARCHAR(255),
    created_at DATETIME,
    updated_at DATETIME
);

-- Table: PHARMACY_MEDICINE
CREATE TABLE PHARMACY_MEDICINE (
    pharmacy_id INT,
    medicine_id INT,
    quantity_available INT,
    price_override FLOAT,
    PRIMARY KEY (pharmacy_id, medicine_id),
    FOREIGN KEY (pharmacy_id) REFERENCES PHARMACY(id),
    FOREIGN KEY (medicine_id) REFERENCES MEDICINE(id)
);

-- Table: "ORDER"
CREATE TABLE "ORDER" (
    id INT PRIMARY KEY,
    name VARCHAR(255), -- Assuming a name for the order, adjust if needed
    status VARCHAR(50),
    customer_id INT,
    pharmacy_id INT,
    created_at DATETIME,
    updated_at DATETIME,
    total_price FLOAT,
    FOREIGN KEY (customer_id) REFERENCES CUSTOMER(id),
    FOREIGN KEY (pharmacy_id) REFERENCES PHARMACY(id)
);

-- Table: ORDERDETAIL
CREATE TABLE ORDERDETAIL (
    id INT PRIMARY KEY,
    order_id INT,
    medicine_id INT,
    quantity INT,
    price_per_item FLOAT,
    total_price_per_item FLOAT,
    FOREIGN KEY (order_id) REFERENCES "ORDER"(id),
    FOREIGN KEY (medicine_id) REFERENCES MEDICINE(id)
);

-- Table: MEDICINE_PHARMACY_ISFREE
CREATE TABLE MEDICINE_PHARMACY_ISFREE (
    pharmacy_id INT,
    medicine_id INT,
    available_quantity INT,
    PRIMARY KEY (pharmacy_id, medicine_id),
    FOREIGN KEY (pharmacy_id, medicine_id) REFERENCES PHARMACY_MEDICINE(pharmacy_id, medicine_id)
);

-- Table: MEDICINE_PHARMACY_CANBEFREE
CREATE TABLE MEDICINE_PHARMACY_CANBEFREE (
    pharmacy_id INT,
    medicine_id INT,
    available_quantity INT,
    PRIMARY KEY (pharmacy_id, medicine_id),
    FOREIGN KEY (pharmacy_id, medicine_id) REFERENCES PHARMACY_MEDICINE(pharmacy_id, medicine_id)
);