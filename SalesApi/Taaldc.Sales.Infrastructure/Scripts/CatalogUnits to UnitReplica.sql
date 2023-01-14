USE taaldb_admin
GO

INSERT INTO taaldb_sales.sales.unitreplica
(Id, 
CreatedBy, 
CreatedOn, 
ModifiedBy, 
ModifiedOn, 
IsActive, 
PropertyId, 
TowerId, 
FloorId, 
UnitId, 
ScenicViewId, 
UnitTypeId, 
Property, 
Tower, 
Floor,
Unit,
ScenicView,
UnitType,
UnitArea,
BalconyArea,
UnitStatus,
UnitStatusId,
OriginalPrice,
SellingPrice)
SELECT
u.Id, u.CreatedBy, u.CreatedOn, u.ModifiedBy, u.ModifiedOn, u.IsActive, 
p.Id AS PropertyId, 
t.Id AS TowerId, 
u.FloorId, 
u.Id AS UnitId,
u.ScenicViewId,
u.UnitType AS UnitTypeId,
p.Name AS Property,
t.Name AS TowerName,
f.Name AS Floor,
u.Identifier AS Unit,
sv.Name AS ScenicView,
ut.Name AS UnitType,
u.FloorArea AS UnitArea,
u.BalconyArea AS BalconyArea,
us.Name AS UnitStatus,
u.UnitStatus AS UnitStatusId,
u.Price AS OriginalPrice,
u.Price AS SellingPrice
FROM catalog.unit u 
JOIN catalog.floors f
ON u.FloorId = f.Id
JOIN catalog.tower t
ON f.TowerId = t.Id
JOIN catalog.property p
ON t.PropertyId = p.Id
JOIN catalog.scenicview sv
ON u.ScenicViewId = sv.Id
JOIN catalog.unittype ut
ON u.UnitType = ut.Id
JOIN catalog.unitstatus us
ON u.UnitStatus = us.Id