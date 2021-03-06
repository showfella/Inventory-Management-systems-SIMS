USE Master;
DROP DATABASE IF EXISTS InventoryStock;
CREATE DATABASE	InventoryStock ;

USE InventoryStock;
DROP TABLE IF EXISTS EmployeeData ;
CREATE TABLE EmployeeData(
	[Lastname] [nvarchar](max) NULL,
	[Firstname] [nvarchar](max) NULL,
	[EmployeeNumber] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	
);
INSERT INTO EmployeeData (Lastname,Firstname,EmployeeNumber,Email,PhoneNumber)
 VALUES ('Ford','Henry','23','ford.henry@me.com',8529638632),
		('Sui','Cho','23','sui.cho@me.com',8529638632),
		('Dave','Henry','23','dave.henry@me.com',8529638632),
		('Ford','Thomas','23','ford.thomas@me.com',8529638632),
		('Harrison','Barnes','23','haarrison.barnes@me.com',8529638632),
		('Stephen','Curry','23','stephen.curry @me.com',8529638632),
		('Steve','Kerr','23','steve.kerr@me.com',8529638632),
		('Bynum','Andrew','23','bynum.andrew@me.com',8529638632),
		('Kobi','Harrison','23','kobi.harrison@me.com',8529638632),
		('Riche','Lionel','23','richie.lionel@me.com',8529638632),
		('Jobs','Dow','23','jobs.dow@me.com',8529638632);
--SELECT* FROM[dbo].[EmployeeData]


CREATE TABLE Inventory (
	[InventoryID] [nvarchar](128) NOT NULL,
	[Product_Name] [nvarchar](max) NULL,
	[Ttem_id][nvarchar](max) NULL,
	[Quantity] [nvarchar](max) NULL
);
INSERT INTO Inventory (InventoryID,Product_Name,Ttem_id,Quantity)
VALUES	('2017','48565668','Kellogs','500'),
		('3201','78895488','HP Laptop',' 10'),
		('4087','22697865','Grape','300'),
		('2002','48626358','Strawberry','1000'),
		('2019','85339955','Apple','1000'),
		('4018','48524796','Gain','1000'),
		('2781','49958792','Steak','50'),
		('9201','20589864','Sensodyne','100'),
		('5301','85296453','Maclean','100'),
		('8201','47524896','Caprisun','300')
--SELECT *FROM [dbo].[Inventory]

CREATE TABLE Items
(
	[Item No] [nvarchar](128) NOT NULL,
	[Bar Code] [nvarchar](max) NULL,
	[Product_Name][nvarchar](max) NULL,
	[Item_discription] [nvarchar](max) NULL
);
INSERT INTO Items ([Item No],[Bar Code],Product_Name,Item_discription)
			VALUES('2017','48565668','Kellogs','Cereal'),
				  ('3201','78895488','HP Laptop','Computer'),
				  ('4087','22697865','Grape','Fruits'),
				  ('2002','48626358','Strawberry','Fruits'),
			      ('2019','85339955','Apple','Fruits'),
			      ('4018','48524796','Gain','Detergent'),
			      ('2781','49958792','Steak','Meat'),
				  ('9201','20589864','Sensodyne','Toothpaste'),
			      ('5301','85296453','Maclean','Toothpaste'),
				  ('8201','47524896','Caprisun','Beverage');

--SELECT * FROM[dbo].[Items]

CREATE TABLE Orders
(
	[Order_No][nvarchar](128) NOT NULL,	
	[Bar_Code][nvarchar](max) NULL,
	[Product_Name] [nvarchar](max) NULL,
	[Date Completed] [nvarchar](max) NULL	
);
INSERT INTO Orders (Order_No,Bar_Code,Product_Name,[Date Completed])
VALUES
('2017','48565668','Kellogs','01-01-2019 '),
('3201','78895488','HP Laptop', '01-04-2019 '),
('4087','22697865','Grape','01-17-2019 '),
('2002','48626358','Strawberry','01-12-2019 '),
('2019','85339955','Apple','01-16-2019 '),
('4018','48524796','Gain','01-12-2019 '),
('2781','49958792','Steak','01-19-2019 '),
('9201','20589864','Sensodyne','01-18-2019'),
('5301','85296453','Maclean','01-15-2019 '),
('8201','47524896','Caprisun','01-14-2019 ');

--SELECT* FROM[dbo].[Orders]

CREATE TABLE Shipments
(
	[Shipment no][nvarchar](max) NULL,
	[Product_Name][nvarchar](max) NULL,	
	[Shipment Date][nvarchar](128) NOT NULL
);
INSERT INTO Shipments ([Shipment no],Product_Name,[Shipment Date]) 
VALUES		('7748565668','Kellogs','01-01-2019 '),
			('7778895488','HP Laptop', '01-04-2019 '),
			('7722697865','Grape','01-17-2019'),
			('7748626358','Strawberry','01-12-2019 '),
			('7785339955','Apple','01-16-2019 '),
			('7748524796','Gain','01-12-2019 '),
			('7749958792','Steak','01-19-2019 '),
			('7720589864','Sensodyne','01-18-2019'),
			('7885296453','Maclean','01-15-2019 '),
			('7747524896','Caprisun','01-14-2019 ');
--SELECT *FROM[dbo].[Shipments]


CREATE TABLE Storage
(
	[Bar_Code][nvarchar](128) NOT NULL,
	[Product_Name] [nvarchar](max) NULL,
	[Position][nvarchar](max) NULL,
	[Sno#][nvarchar](max) NULL,	
); 
INSERT INTO Storage (Bar_Code,Product_Name,Position,Sno#)
VALUES	('48565668','Kellogs','5','017'),
		('78895488','HP Laptop',' 8','321'),
		('22697865','Grape','7','587'),
		('48626358','Strawberry','7', '202'),
		('85339955','Apple','7', '2019'),
		('48524796','Gain','10','401'),
		('49958792','Steak','1', '278'),
		('20589864','Sensodyne','13','901'),
		('85296453','Maclean','12', '501'),
		('47524896','Caprisun','15', '801')
;
--SELECT * FROM[dbo].[Storage]


CREATE TABLE WarehousePosition_1
(
	[Product_Name11][nvarchar](max) NULL,
	[Product_Name12][nvarchar](max) NULL,
	[Product_Name13][nvarchar](max) NULL,
	[Product_Name14][nvarchar](max) NULL,
	[Product_Name15][nvarchar](max) NULL,
	[Product_Name16][nvarchar](max) NULL,
	[Product_Name17][nvarchar](max) NULL,
	[Product_Name18][nvarchar](max) NULL
);
 


