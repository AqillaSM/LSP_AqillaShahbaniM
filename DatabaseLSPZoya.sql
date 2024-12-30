create database subsift8_lsp3;
use subsift8_lsp3;

CREATE TABLE BOOK (
   ID_BOOK CHAR(5) NOT NULL PRIMARY KEY,
   TITLE CHAR(255) NOT NULL,
   AUTHOR CHAR(255) NULL,
   STOK INT NOT NULL,
   DELETE_BOOK INT NOT NULL
);

CREATE TABLE BOOK_PEMINJAMAN (
   ID_BOOK CHAR(5) NOT NULL,
   ID_PEMINJAMAN CHAR(5) NOT NULL,
   DELETE_BOOK_PEMINJAMAN INT NOT NULL,
   PRIMARY KEY (ID_BOOK, ID_PEMINJAMAN)
);

CREATE TABLE PEMINJAMAN (
   ID_PEMINJAMAN CHAR(5) NOT NULL PRIMARY KEY,
   ID_CUSTOMER CHAR(5) NOT NULL,
   TANGGAL_PEMINJAMAN DATE NOT NULL,
   TANGGAL_PENGEMBALIAN DATE NOT NULL, 
   STATUS_PEMINJAMAN INT NOT NULL
);

CREATE TABLE CUSTOMER (
   ID_CUSTOMER CHAR(5) NOT NULL PRIMARY KEY,
   NAME_CUSTOMER CHAR(255) NOT NULL,
   ADDRESS CHAR(255) NOT NULL,
   PHONENUMBER CHAR(13) NOT NULL,
   DELETE_CUSTOMER INT NOT NULL
);

alter table BOOK_PEMINJAMAN
   add constraint FK_BOOK_PEM_BOOK_PEMI_BOOK foreign key (ID_BOOK)
      references BOOK (ID_BOOK)
      on update restrict
      on delete restrict;

alter table BOOK_PEMINJAMAN
   add constraint FK_BOOK_PEM_BOOK_PEMI_PEMINJAM foreign key (ID_PEMINJAMAN)
      references PEMINJAMAN (ID_PEMINJAMAN)
      on update restrict
      on delete restrict;

alter table PEMINJAMAN
   add constraint FK_PEMINJAM_PEMINJAMA_CUSTOMER foreign key (ID_CUSTOMER)
      references CUSTOMER (ID_CUSTOMER)
      on update restrict
      on delete restrict;
      
/* Trigger untuk BOOK */
DELIMITER //
CREATE TRIGGER BEFORE_INSERT_BOOK
BEFORE INSERT ON BOOK
FOR EACH ROW
BEGIN
    DECLARE MAX_ID INT;
    DECLARE NEW_ID INT;
    
    SELECT COALESCE(MAX(CAST(SUBSTRING(ID_BOOK, 2) AS UNSIGNED)), 0) INTO MAX_ID FROM BOOK;

    SET NEW_ID = MAX_ID + 1;
    SET NEW.ID_BOOK = CONCAT('B', LPAD(NEW_ID, 4, '0'));
    SET NEW.DELETE_BOOK = 0;
END;
//
DELIMITER ;

/* Trigger untuk PEMINJAMAN */
DELIMITER //
CREATE TRIGGER BEFORE_INSERT_PEMINJAMAN
BEFORE INSERT ON PEMINJAMAN
FOR EACH ROW
BEGIN
    DECLARE MAX_ID INT;
    DECLARE NEW_ID INT;
    
    SELECT COALESCE(MAX(CAST(SUBSTRING(ID_PEMINJAMAN, 2) AS UNSIGNED)), 0) INTO MAX_ID FROM PEMINJAMAN;

    SET NEW_ID = MAX_ID + 1;
    SET NEW.ID_PEMINJAMAN = CONCAT('P', LPAD(NEW_ID, 4, '0'));
     SET NEW.TANGGAL_PEMINJAMAN = CURDATE();
    SET NEW.TANGGAL_PENGEMBALIAN = CURDATE() + INTERVAL 7 DAY;
    SET NEW.STATUS_PEMINJAMAN = 1;
END;
//
DELIMITER ;

/* Trigger untuk CUSTOMER */
DELIMITER //
CREATE TRIGGER BEFORE_INSERT_CUSTOMER
BEFORE INSERT ON CUSTOMER
FOR EACH ROW
BEGIN
    DECLARE MAX_ID INT;
    DECLARE NEW_ID INT;
    
    SELECT COALESCE(MAX(CAST(SUBSTRING(ID_CUSTOMER, 3) AS UNSIGNED)), 0) INTO MAX_ID FROM CUSTOMER;

    SET NEW_ID = MAX_ID + 1;
    SET NEW.ID_CUSTOMER = CONCAT('C', LPAD(NEW_ID, 4, '0'));
     SET NEW.DELETE_CUSTOMER = 0;
END;
//
DELIMITER ;

/* Trigger untuk BOOK_PEMINJAMAN */
DELIMITER //
CREATE TRIGGER BEFORE_INSERT_BOOK_PEMINJAMAN
BEFORE INSERT ON BOOK_PEMINJAMAN
FOR EACH ROW
BEGIN
    SET NEW.DELETE_BOOK_PEMINJAMAN = 0;
END;
//
DELIMITER ;


INSERT INTO BOOK (TITLE, AUTHOR, STOK) VALUES
('The Great Gatsby', 'F. Scott Fitzgerald', 10),
('1984', 'George Orwell', 15),
('To Kill a Mockingbird', 'Harper Lee', 12),
('Moby-Dick', 'Herman Melville', 8),
('Pride and Prejudice', 'Jane Austen', 20);

INSERT INTO CUSTOMER (NAME_CUSTOMER, ADDRESS, PHONENUMBER) VALUES
('Alice Johnson', '123 Main St', '081234567890'),
('Bob Smith', '456 Maple Ave', '081298765432'),
('Carol Lee', '789 Oak Dr', '081345678912'),
('David Brown', '321 Pine St', '081456789123'),
('Emma Wilson', '654 Elm St', '081567890234');

INSERT INTO PEMINJAMAN (ID_CUSTOMER) VALUES
('C0001'),
('C0002'),
('C0003'),
('C0004'),
('C0005');

INSERT INTO BOOK_PEMINJAMAN (ID_BOOK, ID_PEMINJAMAN) VALUES
('B0001', 'P0001'),
('B0002', 'P0002'),
('B0001', 'P0003'),
('B0004', 'P0002'),
('B0005', 'P0005');