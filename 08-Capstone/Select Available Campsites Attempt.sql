DECLARE 
@campgroundID int = 1,
@arrivalDate DATETIME = '2019-01-01',
@departureDate DATETIME = '2019-02-01'
;

SELECT DISTINCT TOP 5
	            site_number,
	            max_occupancy,
	            accessible,
	            max_rv_length,
	            utilities,
	            campground.daily_fee,
                site.campground_id,
                site.site_id
            FROM site
            JOIN campground ON site.campground_id = campground.campground_id
            JOIN reservation ON site.site_id = reservation.site_id
			campground.campground_id = @campgroundID
            EXCEPT 
			(
					SELECT site_id
					FROM reservation
					WHERE
		            @arrivalDate BETWEEN to_date AND from_date AND
		            @departureDate BETWEEN to_date AND from_date
			) 

            ORDER BY site_number;