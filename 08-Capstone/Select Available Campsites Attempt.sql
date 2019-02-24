DECLARE 
@campgroundID int = 1,
@arrivalDate DATETIME = '2019-01-02',
@departureDate DATETIME = '2019-01-30'
;

SELECT --TOP 5
	            site_number,
	            max_occupancy,
	            accessible,
	            max_rv_length,
	            utilities,
	            campground.daily_fee,
                site.campground_id,
                site.site_id,
				CASE
					WHEN (@arrivalDate BETWEEN to_date AND from_date) OR (@departureDate BETWEEN to_date AND from_date) THEN 1
					ELSE 0
				END AS is_booked
            FROM site
            JOIN campground ON site.campground_id = campground.campground_id
            JOIN reservation ON site.site_id = reservation.site_id
			WHERE campground.campground_id = @campgroundID /*AND
		            @arrivalDate NOT BETWEEN to_date AND from_date AND
		            @departureDate NOT BETWEEN to_date AND from_date*/
			

            ORDER BY site_number;