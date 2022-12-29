Namespace Pnj

    Namespace Dialogue

        Module Pnj_Dialogue

            Sub Dialogue(data As String)

                With Bot

                    Try

                        ' DCK -2  
                        ' DCK ID sur la map

                        .Pnj.Parler = True

                        'J'affiche le nom du PNJ auquel je parle.
                        Dim idUnique As String = Mid(data, 4)

                        If .Map.Entite.ContainsKey(idUnique) Then

                            EcritureMessage("[Dofus]", "Je parle actuellement avec " & .Map.Entite(idUnique).Nom, Color.Green)

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_Dialogue_Dialogue", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Question_Reponse(data As String)

                With Bot.Pnj

                    Try

                        ' DQ 318        ; 449                                           |   259     ;    329    ;
                        ' DQ ID Réponse ; Information à mettre dans le dialogue de base | Réponse 1 ; Réponse 2 ; etc....

                        .Reponse.Clear()
                        .IdReponse = 0

                        data = Mid(data, 3)

                        Dim separateData As String() = Split(data, "|")
                        Dim separate As String() = Split(separateData(0), ";")

                        .IdReponse = separate(0)

                        If data.Contains("|"c) Then

                            separateData = Split(separateData(1), ";")

                            For i = 0 To separateData.Length - 1

                                .Reponse.Add(separateData(i))

                                EcritureMessage("[Dofus]", i + 1 & ") " & VarPnjRéponse(separateData(i)), Color.Green)

                            Next

                        Else

                            EcritureMessage("(Bot)", "Il n'y a plus aucune réponse disponible pour ce Pnj.", Color.Green)

                        End If

                    Catch ex As Exception

                        ErreurFichier(Bot.Personnage.NomDuPersonnage, "Pnj_Dialogue_Question_Reponse", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Fin(data As String)

                With Bot

                    Try

                        ' DV

                        With .Pnj

                            .Parler = False
                            .Reponse.Clear()
                            .IdReponse = 0

                        End With

                        EcritureMessage("[Dofus]", "Fin du dialogue avec le Pnj.", Color.Green)

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_Dialogue_Fin", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub En_Cours(data As String)

                With Bot

                    Try

                        ' DCE

                        With .Pnj

                            .Parler = True

                        End With

                        EcritureMessage("[Dofus]", "Vous êtes déjà en dialogue.", Color.Red)

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_Dialogue_En_Cours", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

    Namespace Acheter_Vendre

        Module Pnj_Acheter_Vendre

            Sub Information(data As String)

                With Bot

                    Try

                        ' EL 596      ; 60#1#4##1d4+0   | 1860;60#1#14##1d20+0 
                        ' EL Id Objet ; Caractéristique | Item Suivant

                        Dim separateData As String() = Split(Mid(data, 3), "|")

                        For i = 0 To separateData.Length - 1

                            Dim separate As String() = Split(separateData(i), ";")

                            Dim newPnjAcheterVendre As New Pnj_Variable.Acheter_Vendre

                            With newPnjAcheterVendre

                                .ID = separate(0)
                                .Caracteristique = Item.Caracteristique(separate(1), separate(0))
                                .CaracteristiqueBrute = separate(1)
                                .Prix = 0

                            End With

                            .Pnj.AcheterVendre_Item.Add(separate(0), newPnjAcheterVendre)

                        Next

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_Acheter_Vendre_Information", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Achat(data As String)

                With Bot

                    Try

                        ' EBK

                        EcritureMessage("[Dofus]", "Achat effectué", Color.Green)

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_Acheter_Vendre_Achat", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub Vente(data As String)

                With Bot

                    Try

                        ' ESK

                        EcritureMessage("[Dofus]", "Vente effectuée", Color.Green)

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_Acheter_Vendre_Vente", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

    End Namespace

    Namespace HotelDeVente

        Module Pnj_HotelDeVente

            Sub AcheterVendre(data As String)

                With Bot

                    Try

                        ' ECK10 | 1            , 10            , 100            ; 2            , 3            , 4            ; 2.0      ; 1000            ; 20                ; -1 ; 350
                        ' ECK10 | Quantité * 1 , Quantité * 10 , Quantité * 100 ; Id Catégorie , Id Catégorie , Id Catégorie ; Taxe (%) ; Niveau Max Item ; Nbr item vendable ; ?  ; Nbr heure Max


                        Dim separateData As String() = Split(data, "|")
                        Dim separateInfo As String() = Split(separateData(1), ";")

                        If separateData(0) = "ECK10" Then

                            .Pnj.Vendre = True

                        Else

                            .Pnj.Acheter = True

                        End If

                        With If(.Pnj.Vendre, .Pnj.Hdv_Vendre, .Pnj.Hdv_Acheter)

                            Dim separate As String() = Split(separateInfo(0), ",")

                            With .Quantiter

                                .x1 = separate(0)
                                .x10 = separate(1)
                                .x100 = separate(2)

                            End With

                            separate = Split(separateInfo(1), ",")

                            .Liste.Categorie.Clear()

                            For i = 0 To separate.Length - 1

                                .Liste.Categorie.Add(separate(i))

                            Next

                            .Taxe = separateInfo(2)
                            .NiveauMax = separateInfo(3)
                            .StockEnMagasin = separateInfo(4)
                            .HeureMax = separateInfo(6)

                        End With

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_HotelDeVente_AcheterVendre", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

            Sub PrixMoyen(data As String)

                With Bot

                    Try

                        ' EHP 180      | 4836469
                        ' EHP Id Objet | Prix Moyen

                        Dim separateData As String() = Split(data, "|")

                        If .Pnj.Vendre Then

                            .Pnj.Hdv_Vendre.PrixMoyen = separateData(1)

                        Else

                            .Pnj.Hdv_Acheter.PrixMoyen = separateData(1)

                        End If

                        EcritureMessage("[Dofus]", "Prix Moyen constaté dans cet hôtel : " & separateData(1) & " kamas/u.", Color.Green)

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_HotelDeVente_PrixMoyen", data & vbCrLf & ex.Message)

                    End Try

                End With

            End Sub

        End Module

        Namespace Vendre

            Module Pnj_HotelDeVente_Vendre

                Sub Information(data As String)

                    With Bot

                        Try

                            ' EL 11759152  ; 100      ; 304      ;                 ; 10000 ; 1800          | 11803628;100;304;;10000;1800  
                            ' EL Id Unique ; Quantité ; Id Objet ; Caractéristique ; Prix  ; Temps restant | Suivant

                            Dim separateData As String() = Split(Mid(data, 3), "|")

                            With .Pnj.Hdv_Vendre.Liste.Item

                                .Clear()

                                For i = 1 To separateData.Length - 1

                                    Dim separate As String() = Split(separateData(i), ";")

                                    Dim newHDV As New Pnj_Variable.Item

                                    With newHDV

                                        .IDUnique = separate(0)
                                        .Quantiter = separate(1)
                                        .IdObjet = separate(2)
                                        .Nom = VarItems(separate(2)).Nom
                                        .Caracteristique = Item.Caracteristique(separate(3), separate(2))
                                        .Prix.Actuelle = separate(4)
                                        .TempsRestant = separate(5)

                                    End With

                                    .Add(separate(0), newHDV)

                                Next

                            End With

                        Catch ex As Exception

                            ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_HotelDeVente_Information", data & vbCrLf & ex.Message)

                        End Try

                    End With

                End Sub

                Sub Ajoute(data As String)

                    With Bot

                        Try

                            ' EmK+ 11759152  | 100      | 304      |          | 10000 | 1800 
                            ' EmK+ id unique | quantité | id objet | caract ? | Prix  | Temps Restant

                            With .Pnj.Hdv_Vendre.Liste.Item

                                Dim separateData As String() = Split(Mid(data, 5), "|")

                                Dim newHDV As New Pnj_Variable.Item

                                With newHDV

                                    .IDUnique = separateData(0)
                                    .Quantiter = separateData(1)
                                    .IdObjet = separateData(2)
                                    .Nom = VarItems(separateData(2)).Nom
                                    .Caracteristique = Item.Caracteristique(separateData(3), separateData(2))
                                    .Prix.Actuelle = separateData(4)
                                    .TempsRestant = separateData(5)

                                End With

                                .Add(separateData(0), newHDV)

                            End With

                        Catch ex As Exception

                            ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_HotelDeVente_Ajoute", data & vbCrLf & ex.Message)

                        End Try

                    End With

                End Sub

                Sub Supprime(data As String)

                    With Bot

                        Try

                            ' EmK- 11791321
                            ' EmK- Id Unique

                            If .Pnj.Hdv_Vendre.Liste.Item.ContainsKey(Mid(data, 5)) Then

                                .Pnj.Hdv_Vendre.Liste.Item.Remove(Mid(data, 5))

                            End If

                        Catch ex As Exception

                            ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_HotelDeVente_Supprime", data & vbCrLf & ex.Message)

                        End Try

                    End With

                End Sub

            End Module

        End Namespace

        Namespace Acheter

            Module Pnj_HotelDeVente_Acheter

                Sub Categorie(data As String)

                    With Bot

                        Try

                            ' EHL 2         | 829    ; 1351    ; etc...
                            ' EHL Categorie | IdItem ; Id Item ; etc...

                            Dim separateData As String() = Split(data, "|")

                            EcritureMessage("[Dofus]", "Vous avez sélectionner la catégorie : " & VarItemsCategorieNom(Mid(separateData(0), 4, separateData(0).Length)), Color.Green)

                            separateData = Split(separateData(1), ";")

                            With .Pnj.Hdv_Acheter.Liste

                                .Item.Clear()

                                For i = 0 To separateData.Length - 1

                                    .ID.Add(separateData(i))

                                Next

                            End With

                        Catch ex As Exception

                            ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_HotelDeVente_Acheter_Categorie", data & vbCrLf & ex.Message)

                        End Try

                    End With

                End Sub

                Sub Information(data As String)

                    With Bot

                        Try

                            ' EHl 180      | 7335643   ; 6f#1###0d0+1,64#10#14##1d5+15 ; 4150000  ;           ;            | Suivant
                            ' EHl ID Objet | Id Unique ; Caracteristique               ; Prix * 1 ; Prix * 10 ; Prix * 100 | Suivant

                            Dim separateData As String() = Split(data, "|")

                            With .Pnj.Hdv_Acheter.Liste.Item

                                .Clear()

                                For i = 1 To separateData.Length - 1

                                    Dim separate As String() = Split(separateData(i), ";")

                                    Dim newHDV As New Pnj_Variable.Item

                                    With newHDV

                                        .IDUnique = separate(0)
                                        .Caracteristique = Item.Caracteristique(separate(1), Mid(separateData(0), 4))
                                        .IdObjet = Mid(separateData(0), 4)
                                        .Nom = VarItems(Mid(separateData(0), 4)).Nom

                                        With .Prix

                                            .x1 = separate(2)
                                            .x10 = separate(3)
                                            .x100 = separate(4)

                                        End With

                                    End With

                                    .Add(separate(0), newHDV)

                                Next

                            End With

                        Catch ex As Exception

                            ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_HotelDeVente_Acheter_Information", data & vbCrLf & ex.Message)

                        End Try

                    End With

                End Sub

                Sub Recherche_Echoue(data As String)

                    With Bot

                        Try

                            EcritureMessage("[Dofus]", "L'objet recherché n'est pas en vente dans cet hôtel des ventes.", Color.Red)

                        Catch ex As Exception

                            ErreurFichier(.Personnage.NomDuPersonnage, "Pnj_HotelDeVente_Acheter_Recherche_Echoue", data & vbCrLf & ex.Message)

                        End Try

                    End With

                End Sub

            End Module

        End Namespace

    End Namespace

End Namespace
