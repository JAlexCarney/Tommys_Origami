CREATE PROCEDURE MostVisitedDestinations
AS 
BEGIN
	select d.DestinationID, d.City, d.StateProvince, d.Country, Count(dt.DestinationID) as 'Number of visitors'
	from Destination d
	Join DestinationTrip dt on d.DestinationID = Dt.DestinationID
	group by d.DestinationID, d.City, d.StateProvince, d.Country
	order by Count(dt.DestinationID) desc
END
GO  