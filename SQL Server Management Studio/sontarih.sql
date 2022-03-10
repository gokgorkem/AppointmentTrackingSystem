select Musteri.MusteriID,yedek from dbo.Musteri  inner join 
	(select  Seans.MusteriID,
		LAST_VALUE(SeansBaslangicTarihi)over( order by  Seans.MusteriID) as yedek 
			from Seans 
	) as tek on tek.MusteriID = dbo.Musteri.MusteriID 
	where tek.yedek<'2020-10-24 13:00:00.000'
	group by Musteri.MusteriID,tek.yedek;