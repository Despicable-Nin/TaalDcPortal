USE
taaldb_admin
GO

--Populate Tables
EXEC sp_MSforeachtable 'if ("?" NOT IN ("[dbo].[__EFMigrationsHistory]"))
         DELETE FROM ?'
EXEC sp_MSForEachTable 'DISABLE TRIGGER ALL ON ?'
GO
EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
GO
EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
GO
EXEC sp_MSForEachTable 'ENABLE TRIGGER ALL ON ?'
GO

INSERT INTO catalog.scenicview (Id, [Name]) VALUES ('1', 'N/A');
INSERT INTO catalog.scenicview (Id, [Name])
VALUES ('2', 'TAAL');
INSERT INTO catalog.scenicview (Id, [Name])
VALUES ('3', 'HIGHLANDS');
INSERT INTO catalog.scenicview (Id, [Name])
VALUES ('4', 'MANILA SKYLINE');
INSERT INTO catalog.scenicview (Id, [Name])
VALUES ('5', 'ROTONDA');

INSERT INTO catalog.unittype (Id, ShortCode, [Name])
VALUES ('1', 'N/A', 'NOT APPLICABLE');
INSERT INTO catalog.unittype (Id, ShortCode, [Name])
VALUES ('2', '1BR', 'ONE BEDROOM');
INSERT INTO catalog.unittype (Id, ShortCode, [Name])
VALUES ('3', '2BR', 'TWO BEDROOM');
INSERT INTO catalog.unittype (Id, ShortCode, [Name])
VALUES ('4', '3BR', 'THREE BEDROOM');
INSERT INTO catalog.unittype (Id, ShortCode, [Name])
VALUES ('5', 'PH', 'PENTHOUSE');
INSERT INTO catalog.unittype (Id, ShortCode, [Name])
VALUES ('6', 'RP', 'RESIDENTIAL PARKING');
INSERT INTO catalog.unittype (Id, ShortCode, [Name])
VALUES ('7', 'MP', 'MOTORCYCLE PARKING');
INSERT INTO catalog.unittype (Id, ShortCode, [Name])
VALUES ('8', 'CS', 'COMMERCIAL SPACE');

INSERT INTO catalog.unitstatus (Id, [Name])
VALUES ('1', 'AVAILABLE');
INSERT INTO catalog.unitstatus (Id, [Name])
VALUES ('2', 'SOLD');
INSERT INTO catalog.unitstatus (Id, [Name])
VALUES ('3', 'RESERVED');
INSERT INTO catalog.unitstatus (Id, [Name])
VALUES ('4', 'BLOCKED');


INSERT INTO catalog.project
([Id], [Name], [Developer], [CreatedOn], [CreatedBy], [ModifiedBy], [ModifiedOn], [IsActive])
VALUES (1, 'OTE', 'TAALDC', GETDATE(), '', '', '', '1')
    INSERT
INTO catalog.property
([Id], [Name], [LandArea], [ProjectId], [CreatedOn], [CreatedBy], [ModifiedBy], [ModifiedOn], [IsActive])
VALUES (1, 'One Tolentino East Residences', '9221', '1', GETDATE(), '', '', '', '1')


INSERT INTO catalog.tower
([Id], [Name], [Number], [Address], [PropertyId], [CreatedOn], [CreatedBy], [ModifiedBy], [ModifiedOn], [IsActive])
VALUES (1, 'Common Tower', '0', 'Brgy. East Tolentino, Tagaytay City', '1', GETDATE(), '', '', '', '1')


INSERT INTO catalog.floors
([Id], [Name], [Description], [TowerId], [CreatedOn], [CreatedBy], [ModifiedBy], [ModifiedOn], [IsActive])
VALUES (1, 'B2', 'Basement 2', 1, GETDATE(), '', '', '', '1')

INSERT INTO catalog.floors
([Id], [Name], [Description], [TowerId], [CreatedOn], [CreatedBy], [ModifiedBy], [ModifiedOn], [IsActive])
VALUES (2, 'B1', 'Basement 1', 1, GETDATE(), '', '', '', '1')

INSERT INTO catalog.floors
([Id], [Name], [Description], [TowerId], [CreatedOn], [CreatedBy], [ModifiedBy], [ModifiedOn], [IsActive])
VALUES (3, 'LF', 'Lower Ground Floor', 1, GETDATE(), '', '', '', '1')

INSERT INTO catalog.floors
([Id], [Name], [Description], [TowerId], [CreatedOn], [CreatedBy], [ModifiedBy], [ModifiedOn], [IsActive])
VALUES (4, 'GF', 'Ground Floor', 1, GETDATE(), '', '', '', '1')

INSERT INTO catalog.floors
([Id], [Name], [Description], [TowerId], [CreatedOn], [CreatedBy], [ModifiedBy], [ModifiedOn], [IsActive])
VALUES (5, 'Mezzanine', 'Mezzanine', 1, GETDATE(), '', '', '', '1')

INSERT INTO catalog.floors
([Id], [Name], [Description], [TowerId], [CreatedOn], [CreatedBy], [ModifiedBy], [ModifiedOn], [IsActive])
VALUES (6, '2nd', '2nd Floor', 1, GETDATE(), '', '', '', '1')

INSERT INTO catalog.floors
([Id], [Name], [Description], [TowerId], [CreatedOn], [CreatedBy], [ModifiedBy], [ModifiedOn], [IsActive])
VALUES (7, '3rd', '3rd Floor', 1, GETDATE(), '', '', '', '1')

DECLARE
@Floors TABLE (FloorNo INT)
INSERT INTO @Floors VALUES (4),(5),(6),(7),(8),(9),(10),(11),(12),(14),(15),(16),(17),(18),(19),(20),(21),(22)

INSERT INTO catalog.floors
([Id], [Name], [Description], [TowerId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
SELECT flr.FloorNo + 4 AS Id,
       CAST(flr.FloorNo AS VARCHAR(10)) + CASE
                                              WHEN flr.FloorNo % 100 IN (11, 12, 13) THEN 'th' 
					WHEN flr.FloorNo % 10 = 1 THEN 'st' 
					WHEN flr.FloorNo % 10 = 2 THEN 'nd' 
					WHEN flr.FloorNo % 10 = 3 THEN 'rd' 
					ELSE 'th'
END
AS [Name],
	CAST(flr.FloorNo AS VARCHAR(10)) + CASE 
					WHEN flr.FloorNo % 100 IN (11,12,13) THEN 'th Floor' 
					WHEN flr.FloorNo % 10 = 1 THEN 'st Floor' 
					WHEN flr.FloorNo % 10 = 2 THEN 'nd Floor' 
					WHEN flr.FloorNo % 10 = 3 THEN 'rd Floor' 
					ELSE 'th Floor'
END
AS [Description],
	1 AS TowerId,
	 GETDATE(),'','','','1'
FROM @Floors flr

INSERT INTO [catalog].unit ([Id],[Identifier],[Price],[FloorArea],[ScenicViewId],[UnitStatus],[UnitType],[FloorId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
VALUES (11,'4-430','10189800','55.08','3',1,2,(4+4), GETDATE(),'','','','1')

INSERT INTO [catalog].unit ([Id],[Identifier],[Price],[FloorArea],[ScenicViewId],[UnitStatus],[UnitType],[FloorId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
VALUES (1,'4-431','9394300','50.78','3',1,2,(4+4), GETDATE(),'','','','1')

INSERT INTO [catalog].unit ([Id],[Identifier],[Price],[FloorArea],[ScenicViewId],[UnitStatus],[UnitType],[FloorId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
VALUES (2,'4-432','9394300','50.78','3',1,2,(4+4), GETDATE(),'','','','1')

INSERT INTO [catalog].unit ([Id],[Identifier],[Price],[FloorArea],[ScenicViewId],[UnitStatus],[UnitType],[FloorId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
VALUES (3,'4-433','9394300','50.78','3',1,2,(4+4), GETDATE(),'','','','1')

INSERT INTO [catalog].unit ([Id],[Identifier],[Price],[FloorArea],[ScenicViewId],[UnitStatus],[UnitType],[FloorId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
VALUES (4,'4-434','9394300','50.78','3',1,2,(4+4), GETDATE(),'','','','1')

INSERT INTO [catalog].unit ([Id],[Identifier],[Price],[FloorArea],[ScenicViewId],[UnitStatus],[UnitType],[FloorId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
VALUES (5,'4-435','9394300','50.78','3',1,2,(4+4), GETDATE(),'','','','1')

INSERT INTO [catalog].unit ([Id],[Identifier],[Price],[FloorArea],[ScenicViewId],[UnitStatus],[UnitType],[FloorId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
VALUES (6,'4-436','9394300','50.78','3',1,2,(4+4), GETDATE(),'','','','1')

INSERT INTO [catalog].unit ([Id],[Identifier],[Price],[FloorArea],[ScenicViewId],[UnitStatus],[UnitType],[FloorId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
VALUES (7,'4-437','9394300','50.78','3',1,2,(4+4), GETDATE(),'','','','1')

INSERT INTO [catalog].unit ([Id],[Identifier],[Price],[FloorArea],[ScenicViewId],[UnitStatus],[UnitType],[FloorId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
VALUES (8,'4-438','9394300','50.78','3',1,2,(4+4), GETDATE(),'','','','1')

INSERT INTO [catalog].unit ([Id],[Identifier],[Price],[FloorArea],[ScenicViewId],[UnitStatus],[UnitType],[FloorId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
VALUES (9,'4-439','9394300','50.78','3',1,2,(4+4), GETDATE(),'','','','1')

INSERT INTO [catalog].unit ([Id],[Identifier],[Price],[FloorArea],[ScenicViewId],[UnitStatus],[UnitType],[FloorId],[CreatedOn],[CreatedBy],[ModifiedBy],[ModifiedOn],[IsActive])
VALUES (10,'4-440','10189800','55.08','3',1,2,(4+4), GETDATE(),'','','','1')

INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy, IsActive) VALUES ('12', '4-407', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('13', '4-408', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('14', '4-409', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('15', '4-410', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('16', '4-411', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('17', '4-412', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('18', '4-413', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('19', '4-414', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('20', '4-415', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('21', '4-416', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('22', '4-417', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('23', '4-418', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('24', '4-419', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('25', '4-420', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('26', '4-421', '9711800', '57.13', '5', '1', '2', '8', '', '', '', '', '1');

INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('27', '5-507', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('28', '5-508', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('29', '5-509', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('30', '5-510', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('31', '5-511', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('32', '5-512', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('33', '5-513', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('34', '5-514', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('35', '5-515', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('36', '5-516', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('37', '5-517', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('38', '5-518', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('39', '5-519', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('40', '5-520', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('41', '5-521', '9883680', '57.13', '5', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('42', '5-531', '9883680', '57.13', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('43', '5-532', '9883680', '57.13', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('44', '5-533', '9883680', '57.13', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('45', '5-534', '9883680', '57.13', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('46', '5-540', '9883680', '57.13', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('47', '5-541', '9883680', '57.13', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('48', '5-542', '9883680', '57.13', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('49', '5-543', '9883680', '57.13', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('50', '5-544', '9883680', '57.13', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('51', '6-606', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('52', '6-607', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('53', '6-608', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('54', '6-609', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('55', '6-610', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('56', '6-611', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('57', '6-612', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('58', '6-613', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('59', '6-614', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('60', '6-615', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('61', '6-616', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('62', '6-617', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('63', '6-618', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('64', '6-619', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('65', '6-620', '9826060', '57.13', '5', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('66', '6-629', '9826060', '57.13', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('67', '6-630', '9826060', '57.13', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('68', '6-631', '9826060', '57.13', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('69', '6-632', '9826060', '57.13', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('70', '6-638', '9826060', '57.13', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('71', '6-639', '9826060', '57.13', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('72', '6-640', '9826060', '57.13', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('73', '6-641', '9826060', '57.13', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('74', '6-642', '9826060', '57.13', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('75', '7-706', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('76', '7-707', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('77', '7-708', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('78', '7-709', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('79', '7-710', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('80', '7-711', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('81', '7-712', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('82', '7-713', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('83', '7-714', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('84', '7-715', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('85', '7-716', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('86', '7-717', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('87', '7-718', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('88', '7-719', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('89', '7-720', '9997940', '57.13', '5', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('90', '7-729', '9997940', '57.13', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('91', '7-730', '9997940', '57.13', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('92', '7-731', '9997940', '57.13', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('93', '7-732', '9997940', '57.13', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('94', '7-738', '9997940', '57.13', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('95', '7-739', '9997940', '57.13', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('96', '7-740', '9997940', '57.13', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('97', '7-741', '9997940', '57.13', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('98', '7-742', '9997940', '57.13', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('99', '9-906', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('100', '9-907', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('101', '9-908', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('102', '9-909', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('103', '9-910', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('104', '9-911', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('105', '9-912', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('106', '9-913', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('107', '9-914', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('108', '9-915', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('109', '9-916', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('110', '9-917', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('111', '9-918', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('112', '9-919', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('113', '9-920', '10112200', '57.13', '5', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('114', '9-929', '10112200', '57.13', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('115', '9-930', '10112200', '57.13', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('116', '9-931', '10112200', '57.13', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('117', '9-932', '10112200', '57.13', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('118', '9-938', '10112200', '57.13', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('119', '9-939', '10112200', '57.13', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('120', '9-940', '10112200', '57.13', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('121', '9-941', '10112200', '57.13', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('122', '9-942', '10112200', '57.13', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('123', '11-1106', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('124', '11-1107', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('125', '11-1108', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('126', '11-1109', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('127', '11-1110', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('128', '11-1111', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('129', '11-1112', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('130', '11-1113', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('131', '11-1114', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('132', '11-1115', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('133', '11-1116', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('134', '11-1117', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('135', '11-1118', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('136', '11-1119', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('137', '11-1120', '10226460', '57.13', '5', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('138', '11-1129', '10226460', '57.13', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('139', '11-1130', '10226460', '57.13', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('140', '11-1131', '10226460', '57.13', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('141', '11-1132', '10226460', '57.13', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('142', '11-1138', '10226460', '57.13', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('143', '11-1139', '10226460', '57.13', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('144', '11-1140', '10226460', '57.13', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('145', '11-1141', '10226460', '57.13', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('146', '11-1142', '10226460', '57.13', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('147', '12-1206', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('148', '12-1207', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('149', '12-1208', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('150', '12-1209', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('151', '12-1210', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('152', '12-1211', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('153', '12-1212', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('154', '12-1213', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('155', '12-1214', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('156', '12-1215', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('157', '12-1216', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('158', '12-1217', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('159', '12-1218', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('160', '12-1219', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('161', '12-1220', '10168840', '57.13', '5', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('162', '12-1229', '10168840', '57.13', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('163', '12-1230', '10168840', '57.13', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('164', '12-1231', '10168840', '57.13', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('165', '12-1232', '10168840', '57.13', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('166', '12-1238', '10168840', '57.13', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('167', '12-1239', '10168840', '57.13', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('168', '12-1240', '10168840', '57.13', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('169', '12-1241', '10168840', '57.13', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('170', '12-1242', '10168840', '57.13', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('171', '14-1406', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('172', '14-1407', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('173', '14-1408', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('174', '14-1409', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('175', '14-1410', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('176', '14-1411', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('177', '14-1412', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('178', '14-1413', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('179', '14-1414', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('180', '14-1415', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('181', '14-1416', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('182', '14-1417', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('183', '14-1418', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('184', '14-1419', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('185', '14-1420', '10340720', '57.13', '5', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('186', '14-1429', '10340720', '57.13', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('187', '14-1430', '10340720', '57.13', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('188', '14-1431', '10340720', '57.13', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('189', '14-1432', '10340720', '57.13', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('190', '14-1438', '10340720', '57.13', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('191', '14-1439', '10340720', '57.13', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('192', '14-1440', '10340720', '57.13', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('193', '14-1441', '10340720', '57.13', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('194', '14-1442', '10340720', '57.13', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('195', '15-1506', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('196', '15-1507', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('197', '15-1508', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('198', '15-1509', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('199', '15-1510', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('200', '15-1511', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('201', '15-1512', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('202', '15-1513', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('203', '15-1514', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('204', '15-1515', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('205', '15-1516', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('206', '15-1517', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('207', '15-1518', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('208', '15-1519', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('209', '15-1520', '10283100', '57.13', '5', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('210', '15-1529', '10283100', '57.13', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('211', '15-1530', '10283100', '57.13', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('212', '15-1531', '10283100', '57.13', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('213', '15-1532', '10283100', '57.13', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('214', '15-1538', '10283100', '57.13', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('215', '15-1539', '10283100', '57.13', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('216', '15-1540', '10283100', '57.13', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('217', '15-1541', '10283100', '57.13', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('218', '15-1542', '10283100', '57.13', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('219', '16-1606', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('220', '16-1607', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('221', '16-1608', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('222', '16-1609', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('223', '16-1610', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('224', '16-1611', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('225', '16-1612', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('226', '16-1613', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('227', '16-1614', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('228', '16-1615', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('229', '16-1616', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('230', '16-1617', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('231', '16-1618', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('232', '16-1619', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('233', '16-1620', '10454980', '57.13', '5', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('234', '16-1629', '10454980', '57.13', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('235', '16-1630', '10454980', '57.13', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('236', '16-1631', '10454980', '57.13', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('237', '16-1632', '10454980', '57.13', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('238', '16-1638', '10454980', '57.13', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('239', '16-1639', '10454980', '57.13', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('240', '16-1640', '10454980', '57.13', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('241', '16-1641', '10454980', '57.13', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('242', '16-1642', '10454980', '57.13', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('243', '18-1806', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('244', '18-1807', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('245', '18-1808', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('246', '18-1809', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('247', '18-1810', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('248', '18-1811', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('249', '18-1812', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('250', '18-1813', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('251', '18-1814', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('252', '18-1815', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('253', '18-1816', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('254', '18-1817', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('255', '18-1818', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('256', '18-1819', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('257', '18-1820', '10569240', '57.13', '5', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('258', '18-1829', '10569240', '57.13', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('259', '18-1830', '10569240', '57.13', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('260', '18-1831', '10569240', '57.13', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('261', '18-1832', '10569240', '57.13', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('262', '18-1838', '10569240', '57.13', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('263', '18-1839', '10569240', '57.13', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('264', '18-1840', '10569240', '57.13', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('265', '18-1841', '10569240', '57.13', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('266', '18-1842', '10569240', '57.13', '3', '1', '2', '22', '', '', '', '', '1');


INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('267', '4-429', '11100000', '60', '3', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('268', '4-441', '11100000', '60', '3', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('269', '4-403', '10509300', '61.47', '2', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('270', '4-425', '10509300', '61.47', '4', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('271', '5-503', '10570770', '61.47', '2', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('272', '5-525', '10570770', '61.47', '4', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('273', '5-530', '10570770', '61.47', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('274', '5-535', '10570770', '61.47', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('275', '5-545', '10570770', '61.47', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('276', '6-603', '10632240', '61.47', '2', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('277', '6-623', '10632240', '61.47', '4', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('278', '6-628', '10632240', '61.47', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('279', '6-633', '10632240', '61.47', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('280', '6-643', '10632240', '61.47', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('281', '7-703', '10693710', '61.47', '2', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('282', '7-723', '10693710', '61.47', '4', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('283', '7-728', '10693710', '61.47', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('284', '7-733', '10693710', '61.47', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('285', '7-743', '10693710', '61.47', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('286', '9-903', '10816650', '61.47', '2', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('287', '9-923', '10816650', '61.47', '4', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('288', '9-928', '10816650', '61.47', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('289', '9-933', '10816650', '61.47', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('290', '9-943', '10816650', '61.47', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('291', '11-1103', '10939590', '61.47', '2', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('292', '11-1123', '10939590', '61.47', '4', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('293', '11-1128', '10939590', '61.47', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('294', '11-1133', '10939590', '61.47', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('295', '11-1143', '10939590', '61.47', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('296', '12-1203', '11001060', '61.47', '2', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('297', '12-1223', '11001060', '61.47', '4', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('298', '12-1228', '11001060', '61.47', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('299', '12-1233', '11001060', '61.47', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('300', '12-1243', '11001060', '61.47', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('301', '14-1403', '11062530', '61.47', '2', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('302', '14-1423', '11062530', '61.47', '4', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('303', '14-1428', '11062530', '61.47', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('304', '14-1433', '11062530', '61.47', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('305', '14-1443', '11062530', '61.47', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('306', '15-1503', '11124000', '61.47', '2', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('307', '15-1523', '11124000', '61.47', '4', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('308', '15-1528', '11124000', '61.47', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('309', '15-1533', '11124000', '61.47', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('310', '15-1543', '11124000', '61.47', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('311', '16-1603', '11185470', '61.47', '2', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('312', '16-1623', '11185470', '61.47', '4', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('313', '16-1628', '11185470', '61.47', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('314', '16-1633', '11185470', '61.47', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('315', '16-1643', '11185470', '61.47', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('316', '18-1803', '11308410', '61.47', '2', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('317', '18-1823', '11308410', '61.47', '4', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('318', '18-1828', '11308410', '61.47', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('319', '18-1833', '11308410', '61.47', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('320', '18-1843', '11308410', '61.47', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('321', '4-404', '10649400', '62.76', '2', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('322', '4-424', '10649400', '62.76', '4', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('323', '5-504', '10712160', '62.76', '2', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('324', '5-524', '10712160', '62.76', '4', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('325', '4-428', '10850550', '64.65', '3', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('326', '4-442', '10850550', '64.65', '3', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('327', '4-402', '11127400', '65.03', '2', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('328', '4-426', '11127400', '65.03', '4', '1', '2', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('329', '5-502', '11192430', '65.03', '2', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('330', '5-526', '11192430', '65.03', '4', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('331', '6-602', '11257460', '65.03', '2', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('332', '6-624', '11257460', '65.03', '4', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('333', '7-702', '11322490', '65.03', '2', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('334', '7-724', '11322490', '65.03', '4', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('335', '9-902', '11452550', '65.03', '2', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('336', '9-924', '11452550', '65.03', '4', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('337', '11-1102', '11582610', '65.03', '2', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('338', '11-1124', '11582610', '65.03', '4', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('339', '12-1202', '11647640', '65.03', '2', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('340', '12-1224', '11647640', '65.03', '4', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('341', '14-1402', '11712670', '65.03', '2', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('342', '14-1424', '11712670', '65.03', '4', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('343', '15-1502', '11777700', '65.03', '2', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('344', '15-1524', '11777700', '65.03', '4', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('345', '16-1602', '11842730', '65.03', '2', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('346', '16-1624', '11842730', '65.03', '4', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('347', '18-1802', '11972790', '65.03', '2', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('348', '18-1824', '11972790', '65.03', '4', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('349', '5-529', '11441520', '65.52', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('350', '5-536', '11441520', '65.52', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('351', '5-546', '11441520', '65.52', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('352', '6-627', '11507040', '65.52', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('353', '6-634', '11507040', '65.52', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('354', '6-644', '11507040', '65.52', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('355', '7-727', '11572560', '65.52', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('356', '7-734', '11572560', '65.52', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('357', '7-744', '11572560', '65.52', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('358', '9-927', '11703600', '65.52', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('359', '9-934', '11703600', '65.52', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('360', '9-944', '11703600', '65.52', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('361', '11-1127', '11834640', '65.52', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('362', '11-1134', '11834640', '65.52', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('363', '11-1144', '11834640', '65.52', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('364', '12-1227', '11900160', '65.52', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('365', '12-1234', '11900160', '65.52', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('366', '12-1244', '11900160', '65.52', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('367', '14-1427', '11965680', '65.52', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('368', '14-1434', '11965680', '65.52', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('369', '14-1444', '11965680', '65.52', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('370', '15-1527', '12031200', '65.52', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('371', '15-1534', '12031200', '65.52', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('372', '15-1544', '12031200', '65.52', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('373', '16-1627', '12096720', '65.52', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('374', '16-1634', '12096720', '65.52', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('375', '16-1644', '12096720', '65.52', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('376', '18-1827', '12227760', '65.52', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('377', '18-1834', '12227760', '65.52', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('378', '18-1844', '12227760', '65.52', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('379', '5-538', '10685220', '67.42', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('380', '6-636', '10752640', '67.42', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('381', '7-736', '10820060', '67.42', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('382', '9-936', '10954900', '67.42', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('383', '11-1136', '11089740', '67.42', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('384', '12-1236', '11157160', '67.42', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('385', '14-1436', '11224580', '67.42', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('386', '15-1536', '11292000', '67.42', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('387', '16-1636', '11359420', '67.42', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('388', '18-1836', '11494260', '67.42', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('389', '5-528', '11026290', '69.29', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('390', '5-547', '11026290', '69.29', '3', '1', '2', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('391', '6-626', '11095580', '69.29', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('392', '6-645', '11095580', '69.29', '3', '1', '2', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('393', '7-726', '11164870', '69.29', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('394', '7-745', '11164870', '69.29', '3', '1', '2', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('395', '9-926', '11303450', '69.29', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('396', '9-945', '11303450', '69.29', '3', '1', '2', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('397', '11-1126', '11442030', '69.29', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('398', '11-1145', '11442030', '69.29', '3', '1', '2', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('399', '12-1226', '11511320', '69.29', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('400', '12-1245', '11511320', '69.29', '3', '1', '2', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('401', '14-1426', '11580610', '69.29', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('402', '14-1445', '11580610', '69.29', '3', '1', '2', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('403', '15-1526', '11649900', '69.29', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('404', '15-1545', '11649900', '69.29', '3', '1', '2', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('405', '16-1626', '11719190', '69.29', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('406', '16-1645', '11719190', '69.29', '3', '1', '2', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('407', '18-1826', '11857770', '69.29', '3', '1', '2', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('408', '18-1845', '11857770', '69.29', '3', '1', '2', '22', '', '', '', '', '1');

INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('409', '4-406', '13909750', '81.5', '5', '1', '3', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('410', '4-422', '13909750', '81.5', '5', '1', '3', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('411', '5-506', '13991250', '81.5', '5', '1', '3', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('412', '5-522', '13991250', '81.5', '5', '1', '3', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('413', '6-605', '14072750', '81.5', '5', '1', '3', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('414', '6-621', '14072750', '81.5', '5', '1', '3', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('415', '7-705', '14154250', '81.5', '5', '1', '3', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('416', '7-721', '14154250', '81.5', '5', '1', '3', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('417', '9-905', '14317250', '81.5', '5', '1', '3', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('418', '9-921', '14317250', '81.5', '5', '1', '3', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('419', '11-1105', '14480250', '81.5', '5', '1', '3', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('420', '11-1121', '14480250', '81.5', '5', '1', '3', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('421', '12-1205', '14561750', '81.5', '5', '1', '3', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('422', '12-1221', '14561750', '81.5', '5', '1', '3', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('423', '14-1405', '14643250', '81.5', '5', '1', '3', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('424', '14-1421', '14643250', '81.5', '5', '1', '3', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('425', '15-1505', '14724750', '81.5', '5', '1', '3', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('426', '15-1521', '14724750', '81.5', '5', '1', '3', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('427', '16-1605', '14806250', '81.5', '5', '1', '3', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('428', '16-1621', '14806250', '81.5', '5', '1', '3', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('429', '18-1805', '14969250', '81.5', '5', '1', '3', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('430', '18-1821', '14969250', '81.5', '5', '1', '3', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('431', '19-1904', '15050750', '81.5', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('432', '19-1915', '15050750', '81.5', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('433', '20-2013', '15132250', '81.5', '5', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('434', '21-2107', '15213750', '81.5', '5', '1', '3', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('435', '19-1905', '16002560', '86.69', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('436', '19-1908', '16002560', '86.69', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('437', '19-1909', '16002560', '86.69', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('438', '19-1912', '16002560', '86.69', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('439', '19-1913', '16002560', '86.69', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('440', '19-1927', '16002560', '86.69', '3', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('441', '20-2003', '16089250', '86.69', '5', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('442', '20-2006', '16089250', '86.69', '5', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('443', '20-2007', '16089250', '86.69', '5', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('444', '20-2010', '16089250', '86.69', '5', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('445', '20-2011', '16089250', '86.69', '5', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('446', '20-2025', '16089250', '86.69', '3', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('447', '21-2101', '16175940', '86.69', '5', '1', '3', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('448', '21-2104', '16175940', '86.69', '5', '1', '3', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('449', '21-2105', '16175940', '86.69', '5', '1', '3', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('450', '19-1906', '16371000', '87.3', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('451', '19-1907', '16371000', '87.3', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('452', '19-1910', '16371000', '87.3', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('453', '19-1911', '16371000', '87.3', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('454', '19-1914', '16371000', '87.3', '5', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('455', '19-1926', '16371000', '87.3', '3', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('456', '20-2004', '16458300', '87.3', '5', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('457', '20-2005', '16458300', '87.3', '5', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('458', '20-2008', '16458300', '87.3', '5', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('459', '20-2009', '16458300', '87.3', '5', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('460', '20-2012', '16458300', '87.3', '5', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('461', '20-2024', '16458300', '87.3', '3', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('462', '21-2102', '16545600', '87.3', '5', '1', '3', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('463', '21-2103', '16545600', '87.3', '5', '1', '3', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('464', '21-2106', '16545600', '87.3', '5', '1', '3', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('465', '5-539', '15062700', '91.1', '3', '1', '3', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('466', '6-637', '15153800', '91.1', '3', '1', '3', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('467', '7-737', '15244900', '91.1', '3', '1', '3', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('468', '9-937', '15427100', '91.1', '3', '1', '3', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('469', '11-1137', '15609300', '91.1', '3', '1', '3', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('470', '12-1237', '15700400', '91.1', '3', '1', '3', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('471', '14-1437', '15791500', '91.1', '3', '1', '3', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('472', '15-1537', '15882600', '91.1', '3', '1', '3', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('473', '16-1637', '15973700', '91.1', '3', '1', '3', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('474', '18-1837', '16155900', '91.1', '3', '1', '3', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('475', '19-1924', '16247000', '91.1', '3', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('476', '20-2022', '16338100', '91.1', '3', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('477', '5-501', '16132680', '99.93', '2', '1', '3', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('478', '5-527', '16132680', '99.93', '4', '1', '3', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('479', '6-601', '16232610', '99.93', '2', '1', '3', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('480', '6-625', '16232610', '99.93', '4', '1', '3', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('481', '7-701', '16332540', '99.93', '2', '1', '3', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('482', '7-725', '16332540', '99.93', '4', '1', '3', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('483', '9-901', '16532400', '99.93', '2', '1', '3', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('484', '9-925', '16532400', '99.93', '4', '1', '3', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('485', '11-1101', '16732260', '99.93', '2', '1', '3', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('486', '11-1125', '16732260', '99.93', '4', '1', '3', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('487', '12-1225', '16832190', '99.93', '4', '1', '3', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('488', '14-1401', '16932120', '99.93', '2', '1', '3', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('489', '14-1425', '16932120', '99.93', '4', '1', '3', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('490', '15-1501', '17032050', '99.93', '2', '1', '3', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('491', '15-1525', '17032050', '99.93', '4', '1', '3', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('492', '16-1601', '17131980', '99.93', '2', '1', '3', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('493', '16-1625', '17131980', '99.93', '4', '1', '3', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('494', '18-1801', '17331840', '99.93', '2', '1', '3', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('495', '18-1825', '17331840', '99.93', '4', '1', '3', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('496', '5-537', '16092930', '100.13', '3', '1', '3', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('497', '6-635', '16193060', '100.13', '3', '1', '3', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('498', '7-735', '16293190', '100.13', '3', '1', '3', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('499', '9-935', '16493450', '100.13', '3', '1', '3', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('500', '11-1135', '16693710', '100.13', '3', '1', '3', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('501', '12-1235', '16793840', '100.13', '3', '1', '3', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('502', '14-1435', '16893970', '100.13', '3', '1', '3', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('503', '15-1535', '16994100', '100.13', '3', '1', '3', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('504', '16-1635', '17094230', '100.13', '3', '1', '3', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('505', '18-1835', '17294490', '100.13', '3', '1', '3', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('506', '4-401', '16558000', '102.47', '2', '1', '3', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('507', '4-427', '16558000', '102.47', '4', '1', '3', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('508', '4-405', '18491600', '113.44', '2', '1', '3', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('509', '4-423', '18491600', '113.44', '4', '1', '3', '8', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('510', '5-505', '18605040', '113.44', '2', '1', '3', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('511', '5-523', '18605040', '113.44', '4', '1', '3', '9', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('512', '19-1920', '21313280', '114.32', '3', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('513', '19-1921', '21313280', '114.32', '3', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('514', '19-1925', '21313280', '114.32', '3', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('515', '20-2018', '21427600', '114.32', '3', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('516', '20-2019', '21427600', '114.32', '3', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('517', '20-2023', '21427600', '114.32', '3', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('518', '21-2112', '21541920', '114.32', '3', '1', '3', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('519', '21-2113', '21541920', '114.32', '3', '1', '3', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('520', '19-1917', '23138850', '125.4', '4', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('521', '19-1919', '23421000', '125.4', '3', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('522', '19-1922', '23421000', '125.4', '3', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('523', '19-1928', '23421000', '125.4', '3', '1', '3', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('524', '20-2015', '23264250', '125.4', '4', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('525', '20-2017', '23546400', '125.4', '3', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('526', '20-2020', '23546400', '125.4', '3', '1', '3', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('527', '21-2109', '23389650', '125.4', '4', '1', '3', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('528', '21-2111', '23671800', '125.4', '3', '1', '3', '25', '', '', '', '', '1');

INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('529', '19-1901', '27227970', '154.53', '2', '1', '4', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('530', '19-1918', '27227970', '154.53', '4', '1', '4', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('531', '19-1923', '27227970', '154.53', '3', '1', '4', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('532', '20-2016', '27382500', '154.53', '3', '1', '4', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('533', '20-2021', '27382500', '154.53', '3', '1', '4', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('534', '21-2110', '27537030', '154.53', '3', '1', '4', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('535', '6-604', '29495870', '174.56', '2', '1', '4', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('536', '6-622', '29495870', '174.56', '4', '1', '4', '10', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('537', '7-704', '29670430', '174.56', '2', '1', '4', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('538', '7-722', '29670430', '174.56', '4', '1', '4', '11', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('539', '9-904', '30019550', '174.56', '2', '1', '4', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('540', '9-922', '30019550', '174.56', '4', '1', '4', '13', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('541', '11-1104', '30368670', '174.56', '2', '1', '4', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('542', '11-1122', '30368670', '174.56', '4', '1', '4', '15', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('543', '12-1204', '30543230', '174.56', '2', '1', '4', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('544', '12-1222', '30543230', '174.56', '4', '1', '4', '16', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('545', '14-1404', '30717790', '174.56', '2', '1', '4', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('546', '14-1422', '30717790', '174.56', '4', '1', '4', '18', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('547', '15-1504', '30892350', '174.56', '2', '1', '4', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('548', '15-1522', '30892350', '174.56', '4', '1', '4', '19', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('549', '16-1604', '31066910', '174.56', '2', '1', '4', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('550', '16-1622', '31066910', '174.56', '4', '1', '4', '20', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('551', '18-1804', '31416030', '174.56', '2', '1', '4', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('552', '18-1822', '31416030', '174.56', '4', '1', '4', '22', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('553', '19-1916', '31590590', '174.56', '4', '1', '4', '23', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('554', '20-2014', '31765150', '174.56', '4', '1', '4', '24', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('555', '21-2108', '31939710', '174.56', '4', '1', '4', '25', '', '', '', '', '1');

INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('556', '21-2115', '32381850', '174.55', '3', '1', '5', '25', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('557', '22-2202', '34722830', '187.34', '4', '1', '5', '26', '', '', '', '', '1');
INSERT INTO catalog.unit (Id, Identifier, Price, FloorArea, ScenicViewId, UnitStatus, UnitType, FloorId, CreatedOn,
                          CreatedBy, ModifiedOn, ModifiedBy, IsActive)
VALUES ('558', '22-2203', '43969060', '253.33', '4', '1', '5', '26', '', '', '', '', '1');


UPDATE catalog.unit
SET CreatedOn = GETDATE()

--
SELECT prop.[Name]       AS Property,
       twr.[Name]        AS Tower,
       flr.[Name]        AS Floor,
       unit.Identifier AS UnitNo,
       ut.[Name]         AS UnitType,
       sv.[Name] AS [View],
	us.[Name] AS [Status],
	unit.CreatedOn
FROM catalog.unit unit
    JOIN catalog.floors flr
on flr.Id = unit.FloorId
    JOIN catalog.unittype ut
    on ut.Id = unit.UnitType
    JOIN catalog.scenicview sv
    on sv.Id = unit.ScenicViewId
    JOIN catalog.tower twr
    ON twr.Id = flr.TowerId
    JOIN catalog.property prop
    ON prop.Id = twr.PropertyId
    JOIN catalog.unitstatus us
    ON us.Id = unit.UnitStatus