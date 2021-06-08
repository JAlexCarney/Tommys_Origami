CREATE PROCEDURE MostReviewedDestinations
AS 
BEGIN
	select d.DestinationID, d.City, d.StateProvince, d.Country, Count(r.DestinationID) as 'Number of Reviews'
	from Review r
	Join Destination d on r.DestinationID = d.DestinationID
	group by d.DestinationID, d.City, d.StateProvince, d.Country
	order by Count(r.DestinationID) DESC
END
GO  