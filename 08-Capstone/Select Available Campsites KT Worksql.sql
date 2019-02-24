DECLARE 
@campgroundID int = 1,
@arrivalDate DATETIME = '2019-01-02',
@departureDate DATETIME = '2019-01-30'
;
DECLARE
@topNumber int =	(SELECT COUNT(*)
					FROM site
					JOIN campground ON site.campground_id = campground.campground_id
					JOIN reservation ON site.site_id = reservation.site_id
					WHERE campground.campground_id = @campgroundID AND NOT
							((CASE WHEN @arrivalDate > from_date
								THEN @arrivalDate
								ELSE from_date
								END) >=
							(CASE WHEN @departureDate<to_date
								THEN @departureDate
								ELSE to_date
								END)))
;
            SELECT DISTINCT TOP(@topNumber + 5)
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
            LEFT JOIN reservation ON site.site_id = reservation.site_id
            WHERE	campground.campground_id = @campgroundID AND 
					(reservation.reservation_id IS NOT NULL OR reservation.reservation_id IS NULL)

            EXCEPT

            SELECT DISTINCT
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
            LEFT JOIN reservation ON site.site_id = reservation.site_id
            WHERE campground.campground_id = @campgroundID AND NOT
                    ((CASE WHEN @arrivalDate > from_date
                        THEN @arrivalDate
                        ELSE from_date
                        END) >=
					(CASE WHEN @departureDate<to_date
                        THEN @departureDate
                        ELSE to_date
                        END))
			
            ORDER BY site_number;