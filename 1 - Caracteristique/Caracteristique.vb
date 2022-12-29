Namespace Caracteristique

    Module Caracteristique

        Sub Information(data As String)

            With Bot

                Try

                    'As 93821075 ,92071000  ,95886000   |165888|10                                 |8                      |0~0,0,0,0,0,0|793       ,793        |10000         ,10000          |439       |100        |6      ,2            ,0      ,0        ,8       |3      ,0            ,0      ,0        ,3       |0         ,-15             ,0         ,0           |0,248,0,0|220,137,0,0|0,0,0,0|1,30,0,0|158,250,0,0 |0,0,0,0|1,0,0,0|0,0,0,0|0,0,0,0         |0,0,0,0 |0,0,0,0 |0,7,0,0|0,0,0,0      |0,0,0,0       |0,0,0,0        |0,5,0,0|0,0,0,0|55,34,0,0|55,34,0,0|0,4,0,0               |0,0,0,0                |0,0,0,0                   |0,0,0,0                    |0,5,0,0              |0,1,0,0               |0,0,0,0                  |0,0,0,0                   |0,10,0,0           |0,2,0,0             |0,0,0,0                |0,0,0,0                 |0,4,0,0            |0,0,0,0             |0,0,0,0                |0,0,0,0                 |0,3,0,0            |0,0,0,0             |0,0,0,0                |0,0,0,0                 |19
                    'As XP Actuel,Xp Minimum,XP Maximum |Kamas |Capital Caractéristiques disponible|Capital Sort disponible|Inconnu      |Pdv_Actuel,PDV_Maximum|Energie_Actuel,Energie_Maximum|Initiative|Prospection|PA_Base,PA_Equipement,PA_Dons,PA_Booste,PA_Total|PM_Base,PM_Equipement,PM_Dons,PM_Booste,PM_Total|Force_Base,Force_Equipement,Force_Dons,Force_Booste|Vitalité |Sagesse    |Chance |Agilité |Intelligence|PO     |Invoc  |Dommage|Dommage Physique|Maîtrise|%Dommage|Soin   |Dommage_Piège|%Dommage_Piège|Renvoie dommage|CC     |EC     |Esq PA   | Esq PM  |Résistance_Neutre_Fixe|%Résistance_Neutre_Fixe|pvp_Résistance_Neutre_Fixe|pvp_%Résistance_Neutre_Fixe|Résistance_Terre_Fixe|%Résistance_Terre_Fixe|pvp_Résistance_Terre_Fixe|pvp_%Résistance_Terre_Fixe|Résistance_Eau_Fixe|%Résistance_Eau_Fixe|pvp_Résistance_Eau_Fixe|pvp_%Résistance_Eau_Fixe|Résistance_Air_Fixe|%Résistance_Air_Fixe|pvp_Résistance_Air_Fixe|pvp_%Résistance_Air_Fixe|Résistance_Feu_Fixe|%Résistance_Feu_Fixe|pvp_Résistance_Feu_Fixe|pvp_%Résistance_Feu_Fixe|Inconnu

                    'data reçu :
                    'AsXp_Actuel,XP_Minimum,Xp_Maximum| = 0
                    'Kamas| = 1
                    'Capital Caractéristiques disponible| = 2
                    'Capital Sort disponible| = 3
                    '0~0,0,0,0,0,0| = 4 (Inconnu)
                    'Pdv_Actuel,PDV_Maximum| = 5
                    'Energie_Actuel,Energie_Maximum| = 6
                    'Initiative_Actuel| = 7
                    'Prospection_Actuel| = 8

                    'PA_Base,PA_Equipement,PA_Dons,PA_Booste,PA_Total| = 9
                    'PM_Base,PM_Equipement,PM_Dons,PM_Booste,PM_Total| = 10

                    'Force_Base,Force_Equipement,Force_Dons,Force_Booste| = 11
                    'Vitalité_Base,Vitalité_Equipement,Vitalité_Dons,Vitalité_Booste| = 12
                    'Sagesse_Base,Sagesse_Equipement,Sagesse_Dons,Sagesse_Booste| = 13
                    'Chance_Base,Chance_Equipement,Chance_Dons,Chance_Booste| = 14
                    'Agilité_Base,Agilité_Equipement,Agilité_Dons,Agilité_Booste| = 15
                    'Intelligence_Base,Intelligence_Equipement,Intelligence_Dons,Intelligence_Booste| = 16

                    'PO_Base,PO_Equipement,PO_Dons,PO_Booste| = 17
                    'Invocation_Base,Invocation_Equipement,Invocation_Dons,Invocation_Booste| = 18
                    'Dommage_Base,Dommage_Equipement,Dommage_Dons,Dommage_Booste| = 19
                    'Dommage_Physique_Base,Dommage_Physique_Equipement,Dommage_Physique_Dons,Dommage_Physique_Booste| = 20
                    'Maîtrise_Base,Maîtrise_Equipement,Maîtrise_Dons,Maîtrise_Booste| = 21
                    '%Dommage_Base,%Dommage_Equipement,%Dommage_Dons,%Dommage_Booste| = 22
                    'Soin_Base,Soin_Equipement,Soin_Dons,Soin_Booste| = 23
                    'Dommage_Piège_Base,Dommage_Piège_Equipement,Dommage_Piège_Dons,Dommage_Piège_Booste| = 24
                    '%Dommage_Piège_Base,%Dommage_Piège_Equipement,%Dommage_Piège_Dons,%Dommage_Piège_Booste| = 25
                    'Renvoie_Dommage_Base,Renvoie_Dommage_Equipement,Renvoie_Dommage_Dons,Renvoie_Dommage_Booste| = 26

                    'Coups_Critiques_Base,Coups_Critiques_Equipement,Coups_Critiques_Dons,Coups_Critiques_Booste| = 27
                    'Echec_Critique_Base,Echec_Critique_Equipement,Echec_Critique_Dons,Echec_Critique_Booste| = 28

                    'Esquive_PA_Base,Esquive_PA_Equipement,Esquive_PA_Dons,Esquive_PA_Booste| = 29
                    'Esquive_PM_Base,Esquive_PM_Equipement,Esquive_PM_Dons,Esquive_PM_Booste| = 30

                    'Résistance_Neutre_Fixe_Base,Résistance_Neutre_Fixe_Equipement,Résistance_Neutre_Fixe_Dons,Résistance_Neutre_Fixe_Booste| = 31
                    '%Résistance_Neutre_Fixe_Base,%Résistance_Neutre_Fixe_Equipement,%Résistance_Neutre_Fixe_Dons,%Résistance_Neutre_Fixe_Booste| = 32
                    'Résistance_Neutre_Fixe_PVP_Base,Résistance_Neutre_Fixe_PVP_Equipement,Résistance_Neutre_Fixe_PVP_Dons,Résistance_Neutre_Fixe_PVP_Booste| = 33
                    '%Résistance_Neutre_Fixe_PVP_Base,%Résistance_Neutre_Fixe_PVP_Equipement,%Résistance_Neutre_Fixe_PVP_Dons,%Résistance_Neutre_Fixe_PVP_Booste| = 34

                    'Résistance_Terre_Fixe_Base,Résistance_Terre_Fixe_Equipement,Résistance_Terre_Fixe_Dons,Résistance_Terre_Fixe_Booste| = 35
                    '%Résistance_Terre_Fixe_Base,%Résistance_Terre_Fixe_Equipement,%Résistance_Terre_Fixe_Dons,%Résistance_Terre_Fixe_Booste| = 36
                    'Résistance_Terre_Fixe_PVP_Base,Résistance_Terre_Fixe_PVP_Equipement,Résistance_Terre_Fixe_PVP_Dons,Résistance_Terre_Fixe_PVP_Booste| = 37
                    '%Résistance_Terre_Fixe_PVP_Base,%Résistance_Terre_Fixe_PVP_Equipement,%Résistance_Terre_Fixe_PVP_Dons,%Résistance_Terre_Fixe_PVP_Booste| = 38

                    'Résistance_Eau_Fixe_Base,Résistance_Eau_Fixe_Equipement,Résistance_Eau_Fixe_Dons,Résistance_Eau_Fixe_Booste| = 39
                    '%Résistance_Eau_Fixe_Base,%Résistance_Eau_Fixe_Equipement,%Résistance_Eau_Fixe_Dons,%Résistance_Eau_Fixe_Booste| = 40
                    'Résistance_Eau_Fixe_PVP_Base,Résistance_Eau_Fixe_PVP_Equipement,Résistance_Eau_Fixe_PVP_Dons,Résistance_Eau_Fixe_PVP_Booste| = 41
                    '%Résistance_Eau_Fixe_PVP_Base,%Résistance_Eau_Fixe_PVP_Equipement,%Résistance_Eau_Fixe_PVP_Dons,%Résistance_Eau_Fixe_PVP_Booste| = 42

                    'Résistance_Air_Fixe_Base,Résistance_Air_Fixe_Equipement,Résistance_Air_Fixe_Dons,Résistance_Air_Fixe_Booste| = 43
                    '%Résistance_Air_Fixe_Base,%Résistance_Air_Fixe_Equipement,%Résistance_Air_Fixe_Dons,%Résistance_Air_Fixe_Booste| = 44
                    'Résistance_Air_Fixe_PVP_Base,Résistance_Air_Fixe_PVP_Equipement,Résistance_Air_Fixe_PVP_Dons,Résistance_Air_Fixe_PVP_Booste| = 45
                    '%Résistance_Air_Fixe_PVP_Base,%Résistance_Air_Fixe_PVP_Equipement,%Résistance_Air_Fixe_PVP_Dons,%Résistance_Air_Fixe_PVP_Booste| = 46

                    'Résistance_Feu_Fixe_Base,Résistance_Feu_Fixe_Equipement,Résistance_Feu_Fixe_Dons,Résistance_Feu_Fixe_Booste| = 47
                    '%Résistance_Feu_Fixe_Base,%Résistance_Feu_Fixe_Equipement,%Résistance_Feu_Fixe_Dons,%Résistance_Feu_Fixe_Booste| = 48
                    'Résistance_Feu_Fixe_PVP_Base,Résistance_Feu_Fixe_PVP_Equipement,Résistance_Feu_Fixe_PVP_Dons,Résistance_Feu_Fixe_PVP_Booste| = 49
                    '%Résistance_Feu_Fixe_PVP_Base,%Résistance_Feu_Fixe_PVP_Equipement,%Résistance_Feu_Fixe_PVP_Dons,%Résistance_Feu_Fixe_PVP_Booste| = 50

                    '73 = 51 (Inconnu)

                    Dim separateData As String() = Split(Mid(data, 3), "|")
                    Dim separate As String()

                    .Caracteristique.Capital = separateData(2)
                    '.Sort.Capital = separateData(3)

                    With .Personnage

                        .Kamas = separateData(1)

                        separate = Split(separateData(5), ",")
                        .Regeneration = CInt(separate(0)) / CInt(separate(1)) * 100

                        With .Experience

                            separate = Split(separateData(0), ",")

                            .Minimum = separate(1)
                            .Maximum = separate(2)
                            .Actuelle = separate(0)
                            .Pourcentage = (separate(0) - separate(1)) / (separate(2) - separate(1)) * 100

                        End With

                        With .Vitaliter

                            separate = Split(separateData(5), ",")

                            .Maximum = separate(1)
                            .Actuelle = If(separate(0) < 0, 0, separate(0))
                            .Pourcentage = If(separate(0) < 0, 0, separate(0) / separate(1) * 100)

                        End With

                        With .Energie

                            separate = Split(separateData(6), ",")

                            .Maximum = separate(1)
                            .Actuelle = separate(0)
                            .Pourcentage = If(separate(0) < 0, 0, separate(0) / separate(1) * 100)

                        End With

                    End With

                    Dim newCaracteristiqueAvancee As New Caracteristique_Variable.Caracteristiques

                    With newCaracteristiqueAvancee

                        With .Primaire

                            With .Initiative

                                separate = Split(separateData(7), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Prospection

                                separate = Split(separateData(8), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .PA

                                separate = Split(separateData(9), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .PM

                                separate = Split(separateData(10), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Force

                                separate = Split(separateData(11), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Vitalite

                                separate = Split(separateData(12), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Sagesse

                                separate = Split(separateData(13), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Chance

                                separate = Split(separateData(14), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Agilite

                                separate = Split(separateData(15), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Intelligence

                                separate = Split(separateData(16), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Portee

                                separate = Split(separateData(17), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Maximum_De_Creatures_Invocables

                                separate = Split(separateData(18), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                        End With

                        With .Bonus

                            With .Degats

                                separate = Split(separateData(19), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Degats_Physiques

                                separate = Split(separateData(20), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Maitrise_Arme

                                separate = Split(separateData(21), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Dommages_PR

                                separate = Split(separateData(22), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Soins

                                separate = Split(separateData(23), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Pieges

                                separate = Split(separateData(24), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Pieges_PR

                                separate = Split(separateData(25), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Renvoi_De_Dommages

                                separate = Split(separateData(26), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Coups_Critiques

                                separate = Split(separateData(27), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .Echecs_Critiques

                                separate = Split(separateData(28), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                        End With

                        With .Esquive

                            With .PA

                                separate = Split(separateData(29), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                            With .PM

                                separate = Split(separateData(30), ",")

                                .Base = separate(0)
                                .Equipement = If(separate.Length > 1, separate(1), "0")
                                .Dons = If(separate.Length > 1, separate(2), "0")
                                .Boost = If(separate.Length > 1, separate(3), "0")
                                .Total = .Base + .Equipement + .Dons + .Boost

                            End With

                        End With

                        With .Resistance

                            With .Combat

                                With .Neutre

                                    With .Fixe

                                        separate = Split(separateData(31), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                    With .Pourcentage

                                        separate = Split(separateData(32), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                End With

                                With .Terre

                                    With .Fixe

                                        separate = Split(separateData(35), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                    With .Fixe

                                        separate = Split(separateData(36), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                End With

                                With .Eau

                                    With .Fixe

                                        separate = Split(separateData(39), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                    With .Pourcentage

                                        separate = Split(separateData(40), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                End With

                                With .Air

                                    With .Fixe

                                        separate = Split(separateData(43), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                    With .Pourcentage

                                        separate = Split(separateData(44), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                End With

                                With .Feu

                                    With .Fixe

                                        separate = Split(separateData(47), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                    With .Pourcentage

                                        separate = Split(separateData(48), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                End With

                            End With

                            With .PvP

                                With .Neutre

                                    With .Fixe

                                        separate = Split(separateData(33), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                    With .Pourcentage

                                        separate = Split(separateData(34), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                End With

                                With .Terre

                                    With .Fixe

                                        separate = Split(separateData(37), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                    With .Fixe

                                        separate = Split(separateData(38), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                End With

                                With .Eau

                                    With .Fixe

                                        separate = Split(separateData(41), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                    With .Pourcentage

                                        separate = Split(separateData(42), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                End With

                                With .Air

                                    With .Fixe

                                        separate = Split(separateData(45), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                    With .Pourcentage

                                        separate = Split(separateData(46), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                End With

                                With .Feu

                                    With .Fixe

                                        separate = Split(separateData(49), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                    With .Pourcentage

                                        separate = Split(separateData(50), ",")

                                        .Base = separate(0)
                                        .Equipement = If(separate.Length > 1, separate(1), "0")
                                        .Dons = If(separate.Length > 1, separate(2), "0")
                                        .Boost = If(separate.Length > 1, separate(3), "0")
                                        .Total = .Base + .Equipement + .Dons + .Boost

                                    End With

                                End With

                            End With

                        End With

                    End With

                    .Caracteristique.Avancee = newCaracteristiqueAvancee

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "MdlCaracteristique_Caracteristique", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Up_Echec(data As String)

            With Bot

                Try


                    ' ABE
                    ' Echec

                    EcritureMessage("[Dofus]", "Impossible de up la caractéristique.", Color.Red)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "MdlCaracteristique_GiCaracteristiqueUpEchec", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace