CREATE PROCEDURE TopRatedDestinations
AS 
BEGIN
	select d.DestinationID, d.City, d.StateProvince, d.Country, CAST(Avg(Rating) as decimal(10,2)) as 'Average Rating'
	from Review r
	Join Destination d on r.DestinationID = d.DestinationID
	group by d.DestinationID, d.City, d.StateProvince, d.Country
	order by AVG(Rating) Desc
END
GO    
