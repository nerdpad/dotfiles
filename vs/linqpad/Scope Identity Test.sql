BEGIN TRAN

CREATE TABLE dbo.Test
(
  ID int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  [Name] nvarchar(500)
)

CREATE TABLE dbo.TestAudit
(
  ID int IDENTITY(1, 1) NOT NULL PRIMARY KEY,
  TestID int NOT NULL,
  [Name] nvarchar(500),
  [Operation] char(1)
)

INSERT INTO dbo.Test
(
  [Name]
)
VALUES
(
  'First'
)

INSERT INTO dbo.Test
(
  [Name]
)
OUTPUT
  inserted.ID,
  inserted.[Name],
  'I'
INTO dbo.TestAudit
(
  TestID,
  [Name],
  [Operation]
)
VALUES
(
  'Zuhaib'
)

SELECT SCOPE_IDENTITY()

SELECT * FROM dbo.Test
SELECT * FROM dbo.TestAudit

UPDATE dbo.Test
  SET [Name] = 'Zuhaib Zakaria'
OUTPUT
  inserted.ID,
  inserted.[Name],
  'U'
INTO dbo.TestAudit
(
  TestID,
  [Name],
  [Operation]
)
WHERE ID = 2

SELECT SCOPE_IDENTITY()

SELECT * FROM dbo.Test
SELECT * FROM dbo.TestAudit

ROLLBACK