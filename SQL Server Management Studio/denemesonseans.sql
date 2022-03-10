update Seans
set CihazID = 1 from Seans inner join SeansEpilasyonMap on SeansEpilasyonMap.SeansID = Seans.SeansID
					inner join Epilasyon on Epilasyon.EpilasyonID = SeansEpilasyonMap.EpilasyonID
					where Epilasyon.EpilasyonID = 51
	