USE [CamSafeDB]

-- Insert Customers
IF NOT EXISTS (SELECT * FROM Customers)
BEGIN
	INSERT INTO Customers VALUES ('Customer1')
	INSERT INTO Customers VALUES ('Customer2')
	INSERT INTO Customers VALUES ('Customer3')
END

-- Insert Cameras
IF NOT EXISTS (SELECT * FROM Cameras)
BEGIN
	INSERT INTO Cameras VALUES ('Camera1', '1.1.1.1', 'true', 1)
	INSERT INTO Cameras VALUES ('Camera2', '1.1.1.1', 'true', 2)
	INSERT INTO Cameras VALUES ('Camera3', '1.1.1.1', 'false', 3)
END

-- Insert Reports
IF NOT EXISTS (SELECT * FROM Reports)
BEGIN
	INSERT INTO Reports VALUES ('2024-02-02T00:00:00', 1)
	INSERT INTO Reports VALUES ('2024-02-03T00:00:00', 1)
	INSERT INTO Reports VALUES ('2024-02-09T00:00:00', 2)
	INSERT INTO Reports VALUES ('2024-02-10T00:00:00', 2)
	INSERT INTO Reports VALUES ('2024-02-22T00:00:00', 3)
	INSERT INTO Reports VALUES ('2024-02-24T00:00:00', 3) 
END