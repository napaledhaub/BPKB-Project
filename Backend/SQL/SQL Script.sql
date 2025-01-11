USE BPKB;

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tr_bpkb')
BEGIN
    DROP TABLE tr_bpkb;
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ms_storage_location')
BEGIN
    DROP TABLE ms_storage_location;
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ms_user')
BEGIN
    DROP TABLE ms_user;
END


CREATE TABLE ms_storage_location (
    location_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    location_name VARCHAR(100)
);

CREATE TABLE ms_user (
    user_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    user_name VARCHAR(20),
    password VARCHAR(50),
    is_active BIT
);

CREATE TABLE tr_bpkb (
    agreement_number VARCHAR(100) PRIMARY KEY,
    bpkb_no VARCHAR(100),
    branch_id VARCHAR(10),
    bpkb_date DATETIME,
    faktur_no VARCHAR(100),
    faktur_date DATETIME,
    location_id BIGINT,
    police_no VARCHAR(20),
    bpkb_date_in DATETIME,
    created_by VARCHAR(20),
    created_on DATETIME,
    last_updated_by VARCHAR(20),
    last_updated_on DATETIME,
    FOREIGN KEY (location_id) REFERENCES ms_storage_location(location_id)
);

INSERT INTO ms_storage_location(location_name)
SELECT 'SSD'
UNION ALL SELECT 'Hard Disk'
UNION ALL SELECT 'Punch Card';


INSERT INTO ms_user(user_name, password, is_active)
SELECT 'jhonUmiro', 'admin1*', 1
UNION ALL SELECT 'trisNatan', 'admin2@', 1
UNION ALL SELECT 'hugoRess', 'admin3#', 0;