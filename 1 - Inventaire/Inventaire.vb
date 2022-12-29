Namespace Inventaire

    Module Inventaire

        Sub Pods(data As String)

            With Bot

                Try

                    ' Ow 750         | 3353
                    ' Ow Pods actuel | Pods Max

                    Dim separateData As String() = Split(Mid(data, 3), "|")

                    With .Personnage.Pods

                        .Maximum = separateData(1)
                        .Actuelle = separateData(0)
                        .Pourcentage = (separateData(0) / separateData(1)) * 100

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Inventaire_Pods", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Information(data As String)

            With Bot

                Try

                    ' ASK | 1234567   | Linaculer  | 99    | 9         | 0       | 90            | -1       | -1       | -1       | 262c1bc        ~ 241      ~ 1         ~ 1                 ~ 64#2#4#0#1d3+1  , 7d#1#0#0#0d0+1 ; Next Item
                    ' ASK | ID Joueur | Nom Joueur | Level | Id Classe | Id Sexe | Classe + Sexe | Couleur1 | Couleur2 | Couleur3 | Id Unique Item ~ Id Objet ~ Quantity  ~ Number equipment  ~ Caractéristique , Caract Next    ; Item suivent

                    Dim separateData As String() = Split(data, "|")

                    .Inventaire.Reset()

                    LinaBot.PictureBox_Interface_Personnage.Image = LinaBot.ImageList_Personnage.Images.Item(LinaBot.ImageList_Personnage.Images.IndexOfKey(separateData(6)))

                    Item.Ajoute(separateData(10), .Inventaire)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Inventaire_Information", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Supprimer(data As String)

            With Bot

                Try

                    ' OR 55156977
                    ' OR id Unique

                    .Inventaire.Supprimer = .Inventaire.Item(Mid(data, 3))

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Inventaire_Supprime", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Quantiter(data As String)

            With Bot

                Try


                    ' OQ 55259212  | 699
                    ' OQ Id Unique | Quantité

                    Dim separateData As String() = Split(Mid(data, 3), "|")

                    Dim newItem As Item_Variable.Information = .Inventaire.Item(separateData(0))

                    newItem.Quantiter = separateData(1)

                    .Inventaire.Modifie = newItem

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Inventaire_Quantiter", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Equipement(data As String)

            With Bot

                Try

                    ' OM 123515576 | 7
                    ' OM id unique | Numéro équipement

                    Dim separateData As String() = Split(Mid(data, 3), "|")

                    Dim newItem As Item_Variable.Information = .Inventaire.Item(separateData(0))

                    With newItem

                        If separateData(1) <> Nothing Then

                            .Equipement = separateData(1)

                        Else

                            .Equipement = ""

                        End If

                    End With

                    .Inventaire.Modifie = newItem

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Inventaire_Equipement", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Caracteristique(data As String)

            With Bot

                Try

                    ' OCO 4a239fd  ~ 1f40    ~ 1        ~ 8                 ~ 320#5#48#9,328#28a#1f5#466,326#0#0#48,327#0#0#18a,9e#2da#0#0#0d0+730 ; 
                    ' OCO idUnique ~ IdObjet ~ Quantité ~ Numéro Equipement ~ Caractéristique                                                      ; Next item

                    Dim separateData As String() = Split(Mid(data, 4), ";")

                    For i = 0 To separateData.Length - 1

                        If separateData(i) <> "" Then

                            Dim separateItem As String() = Split(separateData(i), "~")

                            Dim IdUnique As String = Convert.ToInt64(separateItem(0), 16)

                            Dim newItem As Item_Variable.Information = .Inventaire.Item(IdUnique)

                            With newItem

                                .Quantiter = Convert.ToInt64(separateItem(2), 16)

                                .Caracteristique = Item.Caracteristique(separateItem(4), Convert.ToInt64(separateItem(1), 16))
                                .CaracteristiqueBrute = separateItem(4)

                                If separateItem(3) <> "" Then

                                    .Equipement = Convert.ToInt64(separateItem(3), 16)

                                ElseIf VarItems(Convert.ToInt64(separateItem(1), 16)).Catégorie = "24" Then

                                    .Equipement = "24"

                                Else

                                    .Equipement = ""

                                End If

                            End With

                            .Inventaire.Modifie = newItem

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Inventaire_Caracteristique", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace