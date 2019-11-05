/* DONE
Begin Tran

insert into GuitarRentals_Dev.dbo.ShippingRegions(Abbrv, Region, States) Values
('NE', 'Northeast', 'Maine, New Hampshire, Vermont, Massachusetts, Rhode Island, Connecticut, New York, New Jersey, Pennsylvania'),
('SE', 'Southeast', 'Delaware, Maryland, Virginia, West Virginia, Kentucky, North Carolina, South Carolina, Tennessee, Georgia, Florida, Alabama, Mississippi, Arkansas, Louisiana, Washington D.C'),
('SW', 'Southwest', 'Oklahoma, Texas, New Mexico, Arizona'),
('W', 'West', 'Montana, Idaho, Wyoming, Colorado, Utah, Nevada, California, Oregon, Washington, Alaska, Hawaii'),
('MW', 'Midwest', 'Ohio, Michigan, Indiana, Wisconsin, Illinois, Minnesota, Iowa, Missouri, North Dakota, South Dakota, Nebraska, Kansas')

select* from GuitarRentals_Dev.dbo.ShippingRegions
Commit Tran
*/

/* DONE
Begin Tran

insert into GuitarRentals_Dev.dbo.DistributionCenters(Name, Address, Telephone, ShippingRegionId) Values
('Virginia Shipping Center', '123 Maple Syrup Lane, Norfolk, Virginia 23510', '434-123-4777', 2),
('California Shipping Warehouse', '425 Minus Five Drive, Los Angeles, California 90001', '111-445-6842', 4),
('NY Warehousing', '22 Albany Street, Albany, New York, 12084', '518-988-0775', 1),
('Texas Storage and Shipping', '13 Texas Street, North, Houston, Texas 77001', '281-130-8181', 3),
('Ohio Shipping Center', '41 Buckeye Drive, Columbus, Ohio, 43004', '740-221-7777', 5)

select* from GuitarRentals_Dev.dbo.DistributionCenters
commit tran
*/

/* DONE
Begin Tran

insert into GuitarRentals_Dev.dbo.Couriers( DistributionCenterId, Name, DeliveryStartTime, DeliveryEndTime, DayOfWeek) Values
(1,'UPS', '8:30:00.00', '18:30:00.00', 1 ),
(1,'UPS', '8:30:00.00', '18:30:00.00', 2 ),
(1,'UPS', '8:30:00.00', '18:30:00.00', 3 ),
(1,'UPS', '8:30:00.00', '18:30:00.00', 4 ),
(1,'UPS', '8:30:00.00', '18:30:00.00', 5 ),
(1,'UPS', '8:30:00.00', '16:30:00.00', 6 ),
(1,'UPS', '8:30:00.00', '16:30:00.00', 7 ),

(2,'FedEx', '7:30:00.00', '21:00:00.00', 1 ),
(2,'FedEx', '7:30:00.00', '21:00:00.00', 2 ),
(2,'FedEx', '7:30:00.00', '21:00:00.00', 3 ),
(2,'FedEx', '7:30:00.00', '21:00:00.00', 4 ),
(2,'FedEx', '7:30:00.00', '21:00:00.00', 5 ),
(3,'FedEx', '7:30:00.00', '12:30:00.00', 6 ),
(3,'FedEx', '7:30:00.00', '12:30:00.00', 7 )

select* from GuitarRentals_Dev.dbo.Couriers
commit Tran
*/

/* DONE
Begin Tran

Insert into GuitarRentals_Dev.dbo.RentalAssets(Brand, Name, Available, Description, Rating, ImageUrl, Discriminator, Type, Style, NumberOfStrings, Specifications) Values
('Ibanez', 'RGA series RGAR42MFMT Electric Guitar', 1, 'If Ibanez can lay claim to the title of being the strongest name in Metal guitars, then the RGA is the model this reputation was built on. Every inch of this classic screams speed, fury, and expression.', 4.2, '*TBD*', 'Guitar', 'Electric', 'Flat Dragon Eye Burst', 6, 'Made-for-metal body design , Stunning maple top, Fast-playing Wizard III neck, Versatile Quantum humbuckers with 5-way switch, Rock-stable, double-locking tremolo'),
('Ibanez', 'RGA series RGAR42MFMT Electric Guitar', 1, 'If Ibanez can lay claim to the title of being the strongest name in Metal guitars, then the RGA is the model this reputation was built on. Every inch of this classic screams speed, fury, and expression.', 4.2, '*TBD*', 'Guitar', 'Electric', 'Flat Blue Lagoon Burst', 6, 'Made-for-metal body design , Stunning maple top, Fast-playing Wizard III neck, Versatile Quantum humbuckers with 5-way switch, Rock-stable, double-locking tremolo'),

('Rogue', 'Starter Acoustic Guitar', 1, 'The small-bodied Rogue Starter Acoustic Guitar is an amazing deal for a starter guitar. Its smaller profile (7/8" scale) makes it very playable for kids or aspiring guitarists with smaller body frames and hands.', 4.0, '*TBD*', 'Guitar', 'Acoustic', 'Matte Natural', 6, 'Smaller body style ideal for kids, 7/8 scale ,Maple neck, Rosewood fretboard, Martin strings, Case sold separately'),
('Rogue', 'Starter Acoustic Guitar', 1, 'The small-bodied Rogue Starter Acoustic Guitar is an amazing deal for a starter guitar. Its smaller profile (7/8" scale) makes it very playable for kids or aspiring guitarists with smaller body frames and hands.', 4.0, '*TBD*', 'Guitar', 'Acoustic', 'Pink', 6, 'Smaller body style ideal for kids, 7/8 scale ,Maple neck, Rosewood fretboard, Martin strings, Case sold separately'),
('Rogue', 'Starter Acoustic Guitar', 0, 'The small-bodied Rogue Starter Acoustic Guitar is an amazing deal for a starter guitar. Its smaller profile (7/8" scale) makes it very playable for kids or aspiring guitarists with smaller body frames and hands.', 4.0, '*TBD*', 'Guitar', 'Acoustic', 'Walnut', 6, 'Smaller body style ideal for kids, 7/8 scale ,Maple neck, Rosewood fretboard, Martin strings, Case sold separately'),
('Rogue', 'Starter Acoustic Guitar', 1, 'The small-bodied Rogue Starter Acoustic Guitar is an amazing deal for a starter guitar. Its smaller profile (7/8" scale) makes it very playable for kids or aspiring guitarists with smaller body frames and hands.', 4.0, '*TBD*', 'Guitar', 'Acoustic', 'Black', 6, 'Smaller body style ideal for kids, 7/8 scale ,Maple neck, Rosewood fretboard, Martin strings, Case sold separately'),

('Fender',' Player Jazz Bass Maple Fingerboard', 0, 'With its dual single-coil pickups and smooth playing feel, the Player Jazz Bass is an inspiring instrument with classic, elevated style and authentic Fender bass tone.', 5, '*TBD*', 'Guitar', 'Bass', '3-Color Sunburst', 4, 'Alder body with gloss finish, Two Player Series single-coil Jazz Bass pickups, Two volume controls, master tone control, �Modern C"-shaped neck profile, 9.5"-radius fingerboard'),
('Fender',' Player Jazz Bass Maple Fingerboard', 1, 'With its dual single-coil pickups and smooth playing feel, the Player Jazz Bass is an inspiring instrument with classic, elevated style and authentic Fender bass tone.', 5, '*TBD*', 'Guitar', 'Bass', 'Polar White', 4, 'Alder body with gloss finish, Two Player Series single-coil Jazz Bass pickups, Two volume controls, master tone control, �Modern C"-shaped neck profile, 9.5"-radius fingerboard')

select* from GuitarRentals_Dev.dbo.RentalAssets
Commit Tran
*/

/*
Begin Tran

insert into GuitarRentals_Dev.dbo.Inventory(RentalAssetId, Price, Stock, DistributionCenterId) Values
(1, 399.99, 17, 1),
(3, 49.99, 12, 1),
(4, 49.99, 22, 1),
(6, 49.99, 13, 1),

(1, 399.99, 10, 2),
(2 ,399.99, 10, 2),
(8, 674.99, 2, 2),

(1, 399.99, 13, 3),
(2, 399.99, 8, 3),
(3, 49.99, 19, 3),
(6,	49.99, 3, 3),
(8, 674.99, 1, 3),

(3, 49.99, 12, 4),
(4, 49.99, 14, 4),

(2, 399.99, 2, 5),
(6, 49.99, 7, 5)

select* from GuitarRentals_Dev.dbo.Inventory
Commit Tran
*/

/*
Begin Tran

insert into GuitarRentals_Dev.dbo.Users(FirstName, LastName, Address, Email, Telephone, DateOfBirth, Discriminator, ShippingRegionId, RenewalDate, ExperationDate, Active) Values
('Tom', 'Brown', '123 Fairytale Lane', 'TBrown82@gmail.com', '518-234-7455', '1982-12-03', 'User', null, null, null, null),
('Sarah', 'Brown', '123 Fairytale Lane', 'SBrown84@gmail.com', '518-234-7455', '1984-03-17', 'User', null, null, null, null),
('Jared', 'Michaels', '123 Binary Road', 'JBM98@gmail.com', '518-318-6062', '1998-06-06', 'Subscriber', 1, '2019-11-10', '2020-01-01', 1),
('Charlie', 'Philips', '99 Hexigonal Drive', 'PHC02@gmail.com', '518-944-6417', '2002-06-06', 'User', null, null, null, null),
('Sam', 'Pitzo', '77 Heavenly Ave', 'jojaph42@yahoo.com', '518-817-9999', '1942-09-21', 'Subscriber', 2, '2019-10-31', '2020-09-30', 1),
('Cherry', 'Pitzo', '77 Heavenly Ave', 'CP47@rocketmail.com', '518-817-9999', '1947-10-26', 'Subscriber', 2, '2019-10-31', '2020-09-30', 0),
('Samwise', 'Smithers', '207 Steward Court', 'SS96@gmail.com', '518-223-4343', '1996-08-28', 'User', null, null, null, null),
('Derrick', 'Smithers', '207 Steward Court', 'DS96@gmail.com', '518-223-4344', '1996-08-28', 'User', null, null, null, null),
('Lori', 'Billies', '19 Jeffries Road', 'LoriAnn67@earthlink.net', '484-308-7493', '1967-07-12', 'Subscriber', 3, '2019-11-01', '2020-01-10', 1),
('Jon', 'Lawrence', '2 Ramsey Road', 'JLB66@gmail.com', '484-842-9811', '1966-01-10', 'User', null, null, null, null),
('Renee', 'Ober', '1 Valentines Bvld', 'AObie96@gmail.com', '615-379-7518', '1996-04-29', 'Subscriber', 4, '2019-10-11', '2020-03-01', 1),
('Renee', 'Anthony', '2 Service Dr', 'AR15@yahoo.com', '394-494-3839', '1970-03-08', 'User', null, null, null, null)


select* from GuitarRentals_Dev.dbo.Users
Commit Tran

*/

/*
select* from GuitarRentals_Dev.dbo.Holds
*/

/*
select* from GuitarRentals_Dev.dbo.RentalHistories
*/

/*
select* from GuitarRentals_Dev.dbo.Rentals
*/