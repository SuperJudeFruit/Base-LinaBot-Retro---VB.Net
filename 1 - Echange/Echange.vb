Namespace Echange

    Module Echange

        Sub Reception(data As String)

            With Bot

                Try

                    ' ERK 1234567    | 7654321     | 1
                    ' ERK Id Lanceur | Id Receveur | ?

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    .Echange.Invitation = True

                    If separateData(0) <> .Personnage.ID Then

                        .Echange.ID = separateData(0)
                        EcritureMessage("[Dofus]", .Map.Entite(separateData(0)).Nom & " te propose de faire un échange. acceptes-tu ?", Color.Green)

                    Else

                        EcritureMessage("[Dofus]", "En Attente de la réponse de " & .Map.Entite(separateData(1)).Nom & " pour un échange...", Color.Green)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Reception", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Impossible(data As String)

            Try

                ' EREO

                EcritureMessage("[Dofus]", "Ce joueur est déjà en échange.", Color.Green)

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Echange_Impossible", data & vbCrLf & ex.Message)

            End Try

        End Sub

        Sub Accepter(data As String)

            With Bot

                Try

                    ' ECK 1
                    ' ECK Echange avec un joueur

                    With .Echange

                        .Echange = True
                        .Invitation = False
                        .Interaction = "Joueur"
                        .ID = -1

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Accepter", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Annuler(data As String)

            Try

                ' EV

                Bot.Echange = New Echange_Variable.Base

                EcritureMessage("[Dofus]", "Echange annulé.", Color.Red)

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Echange_Annuler", data & vbCrLf & ex.Message)

            End Try

        End Sub

        Sub Effectuer(data As String)

            Try

                ' EVa

                Bot.Echange = New Echange_Variable.Base

                EcritureMessage("[Dofus]", "Echange effectué.", Color.Green)

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Echange_Effectuer", data & vbCrLf & ex.Message)

            End Try

        End Sub

        Sub Validation(data As String)

            With Bot

                Try

                    ' EK 1      1234567
                    ' EK 1 ou 0 Id perso

                    If Mid(data, 4) = .Personnage.ID Then

                        .Echange.Moi.Valider = Mid(data, 3, 1)

                    Else

                        .Echange.Lui.Valider = Mid(data, 3, 1)

                    End If


                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Validation", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace

Namespace Echange_Lui

    Module Echange_Lui

        Sub Ajoute(data As String)

            With Bot

                Try

                    ' EmKO+ 40514824  | 1        | 7659     |
                    ' EmKO+ Id Unique | Quantité | Id Objet | Caractéristique

                    Dim separateData As String() = Split(Mid(data, 6), "|")

                    Dim newItem As New Item_Variable.Information

                    With newItem

                        .IdObjet = separateData(2)

                        .IdUnique = separateData(0)

                        .Nom = VarItems(Convert.ToInt64(separateData(2))).Nom

                        .Quantiter = separateData(1)

                        .Caracteristique = Item.Caracteristique(separateData(3), separateData(2))

                        .CaracteristiqueBrute = separateData(3)

                        .Categorie = VarItems(.IdObjet).Catégorie

                        .Equipement = ""

                    End With

                    If .Echange.Lui.Inventaire.Existe(separateData(0)) Then

                        .Echange.Lui.Inventaire.Modifie = newItem

                    Else

                        .Echange.Lui.Inventaire.Ajoute = newItem

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Lui_Ajoute", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Supprime(data As String)

            With Bot

                Try

                    ' EmKO- 40420233
                    ' EmKO- Id Unique

                    Dim idUnique As String = Mid(data, 6)

                    If .Echange.Lui.Inventaire.Existe(idUnique) Then

                        .Echange.Lui.Inventaire.Supprimer = idUnique

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Lui_Supprime", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Kamas(data As String)

            Try


                ' EmKG 5
                ' EmKG Kamas

                Bot.Echange.Lui.Kamas = Mid(data, 5)


            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Echange_Lui_Kamas", data & vbCrLf & ex.Message)

            End Try

        End Sub

    End Module

End Namespace

Namespace Echange_Moi

    Module Echange_Moi

        Sub Ajoute(data As String)

            With Bot

                Try

                    ' EMKO+ 40420233  | 20 
                    ' EMKO+ Id Unique | Quantité

                    Dim separateData As String() = Split(Mid(data, 6), "|")

                    Dim newItem As Item_Variable.Information = .Inventaire.Item(separateData(0))

                    newItem.Quantiter = separateData(1)

                    If .Echange.Moi.Inventaire.Existe(separateData(0)) Then

                        .Echange.Moi.Inventaire.Modifie = newItem

                    Else

                        .Echange.Moi.Inventaire.Ajoute = newItem

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Moi_Ajoute", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Supprime(data As String)

            With Bot

                Try

                    ' EMKO- 40420233
                    ' EMKO- Id Unique

                    Dim idUnique As String = Mid(data, 6)

                    If .Echange.Moi.Inventaire.Existe(idUnique) Then

                        .Echange.Moi.Inventaire.Supprimer = idUnique

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Moi_Supprime", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Kamas(data As String)

            Try

                ' Echange
                ' EMKG 5
                ' EMKG kamas

                Bot.Echange.Moi.Kamas = Mid(data, 5)

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Echange_Moi_Kamas", data & vbCrLf & ex.Message)

            End Try

        End Sub

    End Module

End Namespace

Namespace Echange_Banque

    Module Echange_Moi_Banque

        Sub Ajoute(data As String)

            With Bot

                Try

                    ' EsKO+ 78415959  | 2        | 393      |
                    ' EsKO+ Id Unique | Quantité | Id Objet | Caracteristique

                    Dim separateData As String() = Split(Mid(data, 6), "|")

                    Dim newItem As New Item_Variable.Information

                    With newItem

                        .IdObjet = separateData(2)

                        .IdUnique = separateData(0)

                        .Nom = VarItems(separateData(2)).Nom

                        .Quantiter = separateData(1)

                        .Caracteristique = Item.Caracteristique(separateData(3), separateData(2))

                        .CaracteristiqueBrute = separateData(3)

                        .Categorie = VarItems(.IdObjet).Catégorie

                        .Equipement = ""

                    End With

                    If .Echange.Moi.Inventaire.Existe(separateData(0)) Then

                        .Echange.Moi.Inventaire.Modifie = newItem

                    Else

                        .Echange.Moi.Inventaire.Ajoute = newItem

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Banque_Ajoute", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Supprime(data As String)

            With Bot

                Try

                    ' EsKO- 78415959 
                    ' EsKO- Id Unique 

                    Dim idUnique As String = Mid(data, 6)

                    If .Echange.Moi.Inventaire.Existe(idUnique) Then

                        .Echange.Moi.Inventaire.Supprimer = idUnique

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Banque_Supprime", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Kamas(data As String)

            Try

                ' Banque/Coffre
                ' EsKG 5
                ' EsKG kamas

                Bot.Echange.Moi.Kamas = Mid(data, 5)

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Echange_Banque_Kamas", data & vbCrLf & ex.Message)

            End Try

        End Sub

    End Module

End Namespace