Namespace Item

    Module Item

#Region "Ajoute"

        Sub Ajoute(data As String, choix As Item_Variable.Base)

            With Bot

                Try

                    ' 262c1bc   ~ 241      ~ 5        ~ 1                 ~ 64#2#4#0#1d3+1  ,
                    ' Id unique ~ Id Objet ~ Quantité ~ Numéro Equipement ~ Caractéristique , etc... ; tchatItem Suivant

                    If data <> "" Then

                        Dim separateData As String() = Split(data, ";")

                        For i = 0 To separateData.Length - 2

                            Dim separateItem As String() = Split(separateData(i), "~")

                            Dim newItem As New Item_Variable.Information

                            Try

                                With newItem

                                    .IdObjet = Convert.ToInt64(separateItem(1), 16)

                                    .IdUnique = Convert.ToInt64(separateItem(0), 16)

                                    .Nom = VarItems(Convert.ToInt64(separateItem(1), 16)).Nom

                                    .Quantiter = Convert.ToInt64(separateItem(2), 16)

                                    .Caracteristique = Caracteristique(separateItem(4), Convert.ToInt64(separateItem(1), 16))

                                    .CaracteristiqueBrute = separateItem(4)

                                    .Categorie = VarItems(.IdObjet).Catégorie

                                    If separateItem(3) <> "" Then

                                        .Equipement = Convert.ToInt64(separateItem(3), 16)

                                    ElseIf VarItems(Convert.ToInt64(separateItem(1), 16)).Catégorie = "24" Then

                                        .Equipement = "Quete"

                                    Else

                                        .Equipement = ""

                                    End If

                                End With

                            Catch ex As Exception

                                ErreurFichier(.Personnage.NomDuPersonnage, "Item_Ajoute_NewItem", data & vbCrLf & ex.Message)

                            End Try

                            choix.Ajoute = newItem

                        Next

                        If separateData(separateData.Length - 1).Contains("G"c) Then

                            .Echange.Moi.Kamas = Mid(separateData(separateData.Length - 1), 2, separateData(separateData.Length - 1).Length)

                        End If

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Item_Ajoute", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Bonus_Ajoute(data As String)

            With Bot

                Try

                    ' OS+ 5               | 2476     ; 2478    | 76#a#0#0,7d#a#0#0,77#a#0#0,7b#a#0#0,7c#a#0#0,7e#a#0#0
                    ' OS+ Numéro_Panoplie | ID_Objet ; ID_Objet| Caractéristique

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    Dim newBonus As New Item_Variable.Bonus

                    With newBonus

                        .NumeroPanoplie = separateData(0)
                        .IDObjet = Split(separateData(1), ";")
                        .Caracteristique = Caracteristique(separateData(2))
                        .CaracteristiqueBrute = separateData(2)

                    End With

                    If .BonusEquipement.ContainsKey(separateData(0)) Then

                        .BonusEquipement(separateData(0)) = newBonus

                    Else

                        .BonusEquipement.Add(separateData(0), newBonus)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Item_Ajoute", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

#End Region

#Region "Supprime"

        Sub Bonus_Supprime(data As String)

            With Bot

                Try

                    ' OS- 5               
                    ' OS- Numéro_Panoplie 

                    If .BonusEquipement.ContainsKey(Mid(data, 4)) Then

                        .BonusEquipement.Remove(Mid(data, 4))

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Item_Supprimer", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

#End Region

#Region "Equipement"

        Sub Joueur(data As String)

            With Bot

                Try


                    ' Oa 1234567   | 553 , 2412~16~1 , 2411~17~1 ,          , 2509
                    ' Oa ID Unique | Cac , Coiffe    , Cape      , Familier , Bouclier

                    Dim separateData As String() = Split(Mid(data, 3), "|") '1234567 | 553,2412~16~1,2411~17~1,,2509

                    Dim idUnique As String = separateData(0) '1234567

                    separateData = Split(separateData(1), ",") '553,2412~16~1,2411~17~1,,2509

                    If .Map.Entite.ContainsKey(idUnique) Then

                        With .Map.Entite(idUnique).Equipement

                            If separateData(0) <> Nothing Then

                                .Cac = Convert.ToInt64(separateData(0), 16)

                            End If

                            If separateData(1) <> Nothing Then

                                Dim separateObvijevan As String() = Split(separateData(1), "~")

                                .Chapeau.ID = Convert.ToInt64(separateObvijevan(0), 16)

                                If separateData(1).Contains("~"c) Then

                                    .Chapeau.Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                    .Chapeau.Forme = Convert.ToInt64(separateObvijevan(2), 16)

                                End If

                            Else

                                .Chapeau = New Map_Variable.Information

                            End If

                            If separateData(2) <> Nothing Then

                                Dim separateObvijevan As String() = Split(separateData(2), "~")

                                .Cape.ID = Convert.ToInt64(separateObvijevan(0), 16)

                                If separateData(2).Contains("~"c) Then

                                    .Cape.Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                    .Cape.Forme = Convert.ToInt64(separateObvijevan(2), 16)

                                End If

                            Else

                                .Cape = New Map_Variable.Information

                            End If

                            If separateData(3) <> Nothing Then

                                .Familier = Convert.ToInt64(separateData(3), 16)

                            End If

                            If separateData(4) <> Nothing Then

                                .Bouclier = Convert.ToInt64(separateData(4), 16)

                            End If

                        End With

                        If idUnique = .Personnage.ID Then

                            .Personnage.Equipement = .Map.Entite(idUnique).Equipement

                        End If

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Item_Joueur", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

#End Region

#Region "Function"

        Public Function Caracteristique(_caracteristique As String, Optional id As Integer = 0) As Item_Variable.Caracteristique

            Dim resultat As New Item_Variable.Caracteristique

            Try

                ' 76 # a      # 0      # 0      # 0d0+1  , next
                ' 7b # 1      # 0      # 0      # 0d0+1  , next
                ' ID # Divers # Divers # Divers # Aléatoire (exemple CAC) , Caractéristique suivante

                '1d3+5 = Un chiffre alétoire entre 1 à 3, puis rajoute à ça +5

                If _caracteristique <> "" Then

                    Dim separateCaracteristique As String() = Split(_caracteristique, ",") ' 76#a#0#0,7b#1#0#0#0d0+1

                    Try

                        With resultat

                            For i = 0 To separateCaracteristique.Length - 1 ' 76#a#0#0

                                If separateCaracteristique(i) <> "" Then

                                    Dim separate As String() = Split(separateCaracteristique(i), "#")

                                    Dim choixCaractéristique As Integer = If(separate(0) <> "-1", Convert.ToInt64(separate(0), 16), separate(0)) ' 76
                                    '15
                                    '3
                                    Dim valeur1 As Integer = Convert.ToInt64(separate(1), 16)
                                    Dim valeur2 As Integer
                                    Dim valeur3 As Integer = If(separate(3) <> "", Convert.ToInt64(separate(3), 16), 0)

                                    Select Case choixCaractéristique

                                        Case -1

                                        Case 93

                                            .Vole.Air = valeur1 & " a " & Convert.ToInt64(separate(2), 16)

                                        Case 96

                                            .Dommage.Eau = valeur1 & " a " & Convert.ToInt64(separate(2), 16)

                                        Case 97

                                            .Dommage.Terre = valeur1 & " a " & Convert.ToInt64(separate(2), 16)

                                        Case 98 'Dégât air

                                            .Dommage.Air = valeur1 & " a " & Convert.ToInt64(separate(2), 16)

                                        Case 99

                                            .Dommage.Feu = valeur1 & " a " & Convert.ToInt64(separate(2), 16)

                                        Case 100 ' 64 = Dommage neutre ?

                                            .Dommage.Neutre = valeur1 & " a " & Convert.ToInt64(separate(2), 16)

                                        Case 101 ' 65 = PA -

                                            .PertePA = -valeur1

                                        Case 110

                                            .Pdv = valeur1

                                        Case 118 ' 76 = Force +

                                            .Force = valeur1

                                        Case 157 ' 9d = Force -

                                            .Force = valeur1

                                        Case 125 ' 7d = Vitalité +

                                            .Vitalite = valeur1

                                        Case 153 ' 99 = Vitalité -

                                            .Vitalite = valeur1

                                        Case 124 ' 7c = Sagesse +

                                            .Sagesse = valeur1

                                        Case 156 ' 9c = Sagesse -

                                            .Sagesse = valeur1

                                        Case 126 ' 7e = Intelligence +

                                            .Intelligence = valeur1

                                        Case 155 ' 9b = Intelligence -

                                            .Intelligence = valeur1

                                        Case 123 ' 7b = Chance +

                                            .Chance = valeur1

                                        Case 152 ' 98 = Chance -

                                            .Chance = valeur1

                                        Case 119 ' 77 = Agilité +

                                            .Agilite = valeur1

                                        Case 154 ' 9a = Agilité -

                                            .Agilite = valeur1

                                        Case 111 ' 6f = PA +

                                            .PA = valeur1

                                        Case 128 ' 80 = PM +

                                            .PM = valeur1

                                        Case 127 ' 7f = PM -

                                            .PM = valeur1

                                        Case 117 ' 75 = PO +

                                            .PO = valeur1

                                        Case 116 ' 74 = PO -

                                            .PO = valeur1

                                        Case 182 ' b6 = Invocation +

                                            .Invocation = valeur1

                                        Case 174 ' ae = Initiative +

                                            .Initiative = valeur1

                                        Case 175 ' af = Initiative -

                                            .Initiative = valeur1

                                        Case 176 ' b0 = Prospection +

                                            .Prospection = valeur1

                                        Case 177 ' b1 = Prospection -

                                            .Prospection = valeur1

                                        Case 158 ' 9e = Pods +

                                            .Pods = valeur1

                                        Case 115 ' 73 = Coups Critiques +   

                                            .CC = valeur1

                                        Case 112 ' 70 = Dommage +

                                            .Dommage.Physique.Fixe = valeur1

                                        Case 138 ' 8a = %Dommage +

                                            .Dommage.Physique.Pourcentage = valeur1

                                        Case 225 ' e1 = Dommage Piège +

                                            .Piege.Fixe = valeur1

                                        Case 226 ' e2 = %Dommage Piège +

                                            .Piege.Pourcentage = valeur1

                                        Case 178 ' b2 = Soin +

                                            .Soin = valeur1

                                        Case 110 ' 6e = Régénération +

                              '  résultat &= "Regeneration : " & valeur1 & vbCrLf

                                        Case 193

                                   ' resultat &= "Effet : " & DicoItems(valeur3).NameItem & vbCrLf

                                        Case 240 ' f0 = Résistance Terre +

                                            .Resistance.Terre.Fixe = valeur1

                                        Case 241 ' f1 = Résistance Eau +

                                            .Resistance.Eau.Fixe = valeur1

                                        Case 242 ' f2 = Résistance Air +

                                            .Resistance.Air.Fixe = valeur1

                                        Case 243 ' f3 = Résistance Feu +

                                            .Resistance.Feu.Fixe = valeur1

                                        Case 244 ' f4 = Résistance Neutre +

                                            .Resistance.Neutre.Fixe = valeur1

                                        Case 210 ' d2 = %Résistance Terre +

                                            .Resistance.Terre.Pourcentage = valeur1

                                        Case 215 ' d7 = %Résistance Terre -

                                            .Resistance.Terre.Pourcentage = valeur1

                                        Case 211 ' d3 = %Résistance Eau +

                                            .Resistance.Eau.Pourcentage = valeur1

                                        Case 216 ' d8 = %Résistance Eau -

                                            .Resistance.Eau.Pourcentage = valeur1

                                        Case 212 ' d4 = %Résistance Air  +

                                            .Resistance.Air.Pourcentage = valeur1

                                        Case 217 ' d9 = %Résistance Air  -

                                            .Resistance.Air.Pourcentage = valeur1

                                        Case 213 ' d5 = %Résistance Feu +

                                            .Resistance.Feu.Pourcentage = valeur1

                                        Case 218 ' da = %Résistance Feu -

                                            .Resistance.Feu.Pourcentage = valeur1

                                        Case 214 ' d6 = %Résistance Neutre +

                                            .Resistance.Neutre.Pourcentage = valeur1

                                        Case 219 ' db = %Résistance Neutre -

                                            .Resistance.Neutre.Pourcentage = valeur1

                                        Case 100 '64 = Corps à Corps +

                                            .Cac = separate(4)

                                        Case 101 ' 65 = PA perdus à la cible : X à Y

                               ' résultat &= "Pa perdus a la cible : " & valeur1 & vbCrLf & " a " & Convert.ToInt64(separate(2), 16) & vbCrLf

                                        Case 108 ' 6c = PDV rendus : X à Y

                               ' résultat &= "Pdv rendus : " & valeur1 & vbCrLf & " a " & Convert.ToInt64(separate(2), 16) & vbCrLf

                                        Case 600

                                  '  resultat &= "Potion de : Rappel" & vbCrLf

                                        Case 601 ' 259

                                            '  resultat &= "Potion de cite : "

                                            If separate(2) <> Nothing Then

                                                Select Case Convert.ToInt64(separate(2), 16)

                                                    Case 6167

                                          '  resultat &= "Brakmar" & vbCrLf

                                                    Case 6159

                                                        '  resultat &= "Bonta" & vbCrLf

                                                    Case Else

                                                        ErreurFichier("Unknow", "Item_Caracteristique", "Caracteristique Inconnu" & vbCrLf & _caracteristique & vbCrLf & separateCaracteristique(i))

                                                End Select

                                            End If

                                        Case 605 ' 25d

                                ' 1# 
                                ' 3e8# = 1000
                                ' 0#
                                ' 1d1000+0

                             '   résultat &= "Xp gagnee : " & Convert.ToInt64(separate(1), 16) & " a " & Convert.ToInt64(separate(2), 16) & vbCrLf

                                        Case 614

                                  '  resultat &= "+ " & valeur3 & "d'XP dans le métier " & DicoJob(Convert.ToInt64(separate(2), 16)).NameJob

                                        Case 622

                                    '26e#0#0#0
                                  '  resultat &= "Potion de : Foyer" & vbCrLf


                                        Case 623 '26f = Pierre d'âme 

                                            '26f#0#0#93,26f#0#0#94,26f#0#0#94,26f#0#0#94,26f#0#0#65,26f#0#0#65,26f#0#0#65,26f#0#0#65;
                                            If IsNothing(.PierreAme) Then

                                                .PierreAme = New List(Of String)

                                            End If

                                            .PierreAme.Add(valeur3)

                                        Case 699

                                 '   resultat &= "Lier son métier : " & DicoJob(valeur1).NameJob & vbCrLf

                                        Case 701

                                            .Puissance = valeur1

                                        Case 795

                                   ' resultat &= "Arme de chasse" & vbCrLf

                                        Case 800 '320 = Point de vie +

                                            '320 #5      #48     #7
                                            .Familier.Pdv = valeur3
                                 '   resultat &= "Point de Vie : " & valeur3 & vbCrLf

                                        Case 806 ' 326 = 'Repas et Corpulence 

                                            ' 326#1#0#1ab

                                            With .Familier

                                                valeur2 = Convert.ToInt64(separate(2), 16)

                                                If valeur3 >= 7 Then

                                                    valeur3 = If(valeur3 > 100, 100, valeur3)

                                                    .Repas = -valeur3
                                                    .Corpulence = "Maigrichon"

                                                ElseIf valeur2 >= 7 Then

                                                    .Repas = valeur3
                                                    .Corpulence = "Obese"

                                                Else

                                                    .Repas = "0"
                                                    .Corpulence = "Normal"

                                                End If

                                            End With


                                        Case 807 ' 327 = Dernier Repas (objet utilisé)

                                            '327#0#0#734

                                            With .Familier

                                                Select Case valeur3

                                                    Case 2114

                                                        .Repas_Dernier = "Aliment inconnu"

                                                    Case "0"

                                                        .Repas_Dernier = "Aucun"

                                                    Case Else

                                                        .Repas_Dernier = VarItems(valeur3).Nom

                                                End Select

                                            End With


                                        Case 808 '"328" 'Date / Heure  

                                            ' 328 # 28a   # cc          # 398   = A mangé le : 04/03/650 9:20
                                            ' 328 # Année # Mois + Jour # Heure

                                            If VarItems(id).Catégorie <> 90 Then

                                                valeur2 = Convert.ToInt64(separate(2), 16)

                                                Dim Année As Integer = valeur1 + 1370

                                                Dim Mois As Integer = If(valeur2 < 100, 1, Mid(valeur2, 1, valeur2.ToString.Length - 2) + 1)
                                                Dim Jour As Integer = If(valeur2 < 100, valeur2, Mid(valeur2, valeur2.ToString.Length - 1, 2))

                                                Dim Heure As String = valeur3.ToString.Insert(valeur3.ToString.Length - 2, ":")
                                                If Heure.Length = 3 Then Heure = "00" & Heure

                                                Dim dateFinal As Date = CDate(Jour & "/" & Mois & "/" & Année & " " & Heure)

                                                .Familier.Repas_Date = dateFinal
                                                .Familier.Repas_Prochain = dateFinal.AddHours(VarFamilier(id).IntervalRepasMin)

                                            End If

                                        Case 811 'Malédiction/booste bonbon etc....

                                            Select Case valeur1

                                                Case 15 ' Malédiction du ballotin

                                                    .TourRestant = valeur3

                                            End Select


                                        Case 812

                                            .ResistanceItem = valeur1 & "/" & valeur3

                                            Select Case VarItems(id).Catégorie

                                                Case 5, 19, 8, 22, 7, 3, 4, 6, 20, 21, 83

                                                    .Etheree = True

                                                Case Else

                                                    .Etheree = False

                                            End Select

                                        Case 830

                                            '  resultat &= "Potion de : "

                                            Select Case valeur3

                                                Case 1

                                          '  resultat &= "Foyer de guilde" & vbCrLf

                                                Case 2

                                                    ' resultat &= "Enclos de guilde" & vbCrLf

                                            End Select

                                        Case 850 ' ?

                                        Case 851 ' ? Vole or 1d5+5 d'or ?

                                        Case 940 '"3ac" 'Capacité accrue Familier

                                            '3ac#0#0#a
                                            ' a = 10, donc le familier peut avoir +10 en caract, etc... selon le familier.
                                            .Familier.Capacite_Accrue = True

                                        Case 948

                                   ' resultat &= "Objet pour enclos" & vbCrLf

                                        Case 970

                                   ' resultat &= "Apparence : " & DicoItems(valeur3).NameItem & vbCrLf

                                        Case 971

                            '3cb#0#0#0
                            'Aucune idée, mais Objivevan

                                        Case 972

                                   ' resultat &= "Niveau : " & valeur3 & vbCrLf

                                        Case 973

                            '3cd#0#0#10
                            'Indique la forme à afficher (sprite)
                            '10 = le forme (sprite) à afficher

                                        Case 974

                            '3ce#0#0#17c
                            'Info Objivevan inconnu

                                        Case 985

                                 '   resultat &= "Modifie par : " & separate(4) & vbCrLf

                                        Case 988

                                   ' resultat &= "Fabrique par : " & separate(4) & vbCrLf

                                        Case 994 ' 3e2

                                            .Dragodinde.Date = TimeOfDay

                                        Case 995 '3e3 = ID de la dragodinde pour avoir les caractéristiques (quand elle se trouve dans l'inventaire)
                                            '3e3#c0a#1710bbb0c60#0

                                            .Dragodinde.IdUnique = "Rd" & valeur1 & vbCrLf & "|" & separate(2)

                                        Case 996 ' 3e4 = Nom du joueur qui posséde la dragodinde.
                                            '3e4#0#0#0#Linaculer

                                            .Dragodinde.Possesseur = separate(4)

                                        Case 997 '3e5 = Nom de la dragodinde
                                            '3e5#15#0#0#Linaculeur

                                            .Dragodinde.Nom = separate(4)

                                        Case 998 '"3e6" ' Jour/ heure / minute restant.
                                            '3e6#13#17#3b

                                            .Dragodinde.Parchemin = DateAdd(DateInterval.Day, valeur1, Date.Today).ToString & " " & Convert.ToInt64(separate(2), 16) & ":" & valeur3

                                        Case 805 '"325" 'Divers

                                            '  resultat &= "Divers : Certificat Dopeul" & vbCrLf


                                        Case Else

                                            ErreurFichier("Unknow", "Item_Caracteristique", _caracteristique & vbCrLf & separateCaracteristique(i))

                                    End Select

                                End If

                            Next

                        End With

                    Catch ex As Exception

                    End Try

                End If

            Catch ex As Exception

                ErreurFichier("Unknow", "Item_Caracteristique", _caracteristique & vbCrLf & ex.Message)

            End Try

            Return resultat

        End Function

#End Region

    End Module

End Namespace