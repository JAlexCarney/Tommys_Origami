CREATE PROCEDURE DestinationTripsWithCity
AS 
BEGIN
	select d.City, d.Country, dt.DestinationID, dt.TripID, dt.[Description]
	from Destination d 
	join DestinationTrip dt on d.DestinationID = dt.DestinationID 
END
GO  
