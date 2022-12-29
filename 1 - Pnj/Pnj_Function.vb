Namespace Pnj_Function

    Namespace Dialogue
        Module Pnj_Function_Dialogue

            Public Function Parler(nomID As String) As Boolean

                With Bot

                    Try

                        If .Pnj.Parler = False Then

                            For Each Pair As Map_Variable.Entite In .Map.Entite.Values

                                If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.ID = nomID Then

                                    Return .Mitm.Send("DC" & Pair.IDUnique,
                                            {"DCK", ' En dialogue
                                             "DCE"}) ' Déjà en dialogue.

                                    Exit For

                                End If

                            Next

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "Parler", ex.Message)

                    End Try

                    Return .Pnj.Parler

                End With

            End Function

            Public Function Reponse(phrase As String) As Boolean

                With Bot

                    Try

                        If .Pnj.Parler Then

                            If phrase.ToLower.StartsWith("terminer la discussion") Then

                                Return QuitteDialogue()

                            End If

                            If .Pnj.Reponse.Count > 0 Then

                                For i = 0 To .Pnj.Reponse.Count - 1

                                    If phrase.ToLower = VarPnjRéponse(.Pnj.Reponse(i)).ToLower Then

                                        EcritureMessage("(Bot)", "Réponse : " & VarPnjRéponse(.Pnj.Reponse(i)), Color.Orange)

                                        Return .Mitm.Send("DR" & .Pnj.IdReponse & "|" & .Pnj.Reponse(i),
                                                {"DQ", ' Reçoit des nouvelles réponses disponible.
                                                 "DV",' Fin du dialogue avec le Pnj.
                                                 "DR"}) ' ?

                                    End If

                                Next

                            Else

                                Return QuitteDialogue()

                            End If

                        End If

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

            Public Function QuitteDialogue() As Boolean

                With Bot

                    Try

                        If .Pnj.Parler Then

                            Return .Mitm.Send("DV",
                                    {"DV"}) 'Fin du dialogue

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "QuitteDialogue", ex.Message)

                    End Try

                    Return Not .Pnj.Parler

                End With

            End Function

        End Module

    End Namespace

    Namespace AcheterVendre

        Module Pnj_Function_AcheterVendre

            Public Function AcheterVendre(nomID As String) As Boolean

                With Bot

                    Try

                        If .Pnj.AcheterVendre = False Then

                            For Each Pair As Map_Variable.Entite In .Map.Entite.Values

                                If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.ID = nomID Then

                                    Return .Mitm.Send("ER0|" & Pair.IDUnique,
                                            {"ECK10"}) ' Reçoit les informations de l'HDV

                                End If

                            Next

                        End If

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

            Public Function Quitte() As Boolean

                With Bot

                    Try

                        If .Pnj.AcheterVendre Then

                            Return .Mitm.Send("EV",
                                    {"EV"}) ' a quitté le mode "acheter/vendre" du Pnj.

                        End If

                    Catch ex As Exception

                    End Try

                    Return Not .Pnj.AcheterVendre

                End With

            End Function

            Public Function Vend(nomID As String, quantite As Integer) As Boolean

                With Bot

                    Try

                        If .Pnj.AcheterVendre Then

                            For Each pair As Item_Variable.Information In .Inventaire.Item.Values

                                If pair.Nom.ToLower = nomID.ToLower OrElse pair.IdObjet = nomID OrElse nomID.ToLower = "all" Then

                                    If quantite > pair.Quantiter Then quantite = pair.Quantiter

                                    Return .Mitm.Send("ES" & pair.IdUnique & "|" & quantite,
                                                {"ESK"}) ' Item Vendu.

                                End If

                            Next

                        End If

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

            Public Function Achete(nomID As String, quantite As Integer) As Boolean

                With Bot

                    Try

                        If .Pnj.AcheterVendre Then

                            For Each pair As KeyValuePair(Of Integer, Pnj_Variable.Acheter_Vendre) In .Pnj.AcheterVendre_Item

                                If pair.Value.ID = nomID OrElse VarItems(pair.Key).Nom.ToLower = nomID.ToLower Then

                                    Return .Mitm.Send("EB" & pair.Key & "|" & quantite,
                                                {"EBK"},
                                                {"Im172"})

                                End If

                            Next

                        End If

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

        End Module

    End Namespace

    Namespace Vendre

        Module Pnj_Function_Vendre

            Public Function Vendre(nomID As String) As Boolean

                With Bot

                    Try

                        If .Pnj.Vendre = False Then

                            For Each Pair As Map_Variable.Entite In .Map.Entite.Values

                                If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.ID = nomID Then

                                    Return .Mitm.Send("ER10|" & Pair.IDUnique,
                                            {"ECK10"})

                                End If

                            Next

                        End If

                    Catch ex As Exception

                    End Try

                    Return .Pnj.Vendre

                End With

            End Function

            Public Function Quitte() As Boolean

                With Bot

                    Try

                        If .Pnj.Vendre Then

                            Return .Mitm.Send("EV",
                                    {"EV"})

                        End If

                    Catch ex As Exception

                    End Try

                    Return Not .Pnj.Vendre

                End With

            End Function

            Public Function Vend(nomID As String, quantite As Integer, prix As Integer) As Boolean

                With Bot

                    Try

                        For Each Pair As Item_Variable.Information In .Inventaire.Item.Values

                            If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.IdObjet.ToString = nomID Then

                                If Pair.Equipement = "" Then

                                    If quantite > CInt(Pair.Quantiter) Then

                                        EcritureMessage("(Bot)", "Vous n'avez pas asse de quantiter pour vendre.", Color.Red)

                                        Return False

                                    End If

                                    prix = quantite * prix

                                    If ((prix / 100) * .Pnj.Hdv_Vendre.Taxe) > .Personnage.Kamas Then

                                        EcritureMessage("(Bot)", "Vous n'avez pas asse de kamas pour payer la taxe !", Color.Gray)

                                        Return False

                                    End If

                                    EcritureMessage("(Bot)", "Vente de l'item " & Pair.Nom & " x " & quantite & "au prix de : " & (prix * quantite), Color.Gray)

                                    Select Case quantite

                                        Case 1

                                            quantite = 1

                                        Case 10

                                            quantite = 2

                                        Case 100

                                            quantite = 3

                                    End Select

                                    .Mitm.Send("EMO+" & Pair.IdUnique & "|" & quantite & "|" & prix)

                                End If

                            End If

                        Next

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

            Public Function Retirer(nomID As String, quantite As String) As Boolean

                With Bot

                    Try

                        For Each Pair As Pnj_Variable.Item In .Pnj.Hdv_Vendre.Liste.Item.Values

                            If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.IdObjet.ToString = nomID Then

                                If quantite > Pair.Quantiter Then quantite = Pair.Quantiter

                                EcritureMessage("(Bot)", "Retire l'item " & Pair.Nom & " x " & quantite, Color.Lime)

                                Return .Mitm.Send("EMO-" & Pair.IDUnique & "|" & quantite)

                            End If

                        Next

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

            Public Function Selectionne(nomID As String) As Boolean

                With Bot

                    Try

                        For Each Pair As Pnj_Variable.Item In .Pnj.Hdv_Vendre.Liste.Item.Values

                            If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.IdObjet.ToString = nomID Then

                                EcritureMessage("(Bot)", "Sélection de l'item " & Pair.Nom, Color.Lime)


                                Return .Mitm.Send("EHP" & Pair.IdObjet)

                            End If

                        Next


                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

        End Module

    End Namespace

    Namespace Acheter

        Module Pnj_Function_Acheter

            Public Function Acheter(nomID As String) As Boolean

                With Bot

                    Try

                        If .Pnj.Acheter = False Then

                            For Each Pair As Map_Variable.Entite In .Map.Entite.Values

                                If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.ID = nomID Then

                                    Return .Mitm.Send("ER11|" & Pair.IDUnique,
                                                     {"ECK11"})

                                End If

                            Next

                        End If

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

            Public Function Quitte() As Boolean

                With Bot

                    Try

                        If .Pnj.Acheter Then

                            Return .Mitm.Send("EV",
                                             {"EV"})

                        End If

                        Return .Pnj.Acheter

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

            Public Function Recherche(nomID As String) As Boolean

                With Bot

                    Try

                        If .Pnj.Acheter Then

                            For Each pair As sItems In VarItems.Values

                                If pair.ID.ToString = nomID OrElse pair.Nom.ToLower = nomID.ToLower Then

                                    If .Mitm.Send("EHS" & pair.Catégorie & "|" & pair.ID,
                                                 {"EHL"}) Then

                                        Return .Mitm.Send("EHP" & pair.ID,
                                                         {"EHl"})

                                    End If

                                    Return False

                                End If

                            Next

                        End If

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

            Public Function Achete(nomID As String, quantite As Integer, prix As Integer) As Boolean

                With Bot

                    Try

                        For Each pair As Pnj_Variable.Item In .Pnj.Hdv_Acheter.Liste.Item.Values

                            If pair.Nom.ToLower = nomID.ToLower OrElse pair.IdObjet = nomID Then

                                If quantite >= 100 Then

                                    If .Personnage.Kamas >= pair.Prix.x100 Then

                                        Return .Mitm.Send("EHB" & pair.IDUnique & "|3|" & pair.Prix.x100,
                                                  {"EBK"})

                                    End If

                                End If

                                If quantite >= 10 Then

                                    If .Personnage.Kamas >= pair.Prix.x10 Then

                                        Return .Mitm.Send("EHB" & pair.IDUnique & "|2|" & pair.Prix.x10,
                                                  {"EBK"})

                                    End If

                                End If

                                If quantite >= 1 Then

                                    If .Personnage.Kamas >= pair.Prix.x1 Then

                                        Return .Mitm.Send("EHB" & pair.IDUnique & "|1|" & pair.Prix.x1,
                                                  {"EBK"})

                                    End If

                                End If

                            End If

                        Next

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

            Public Function Selectionne(nomID As String) As Boolean

                With Bot

                    Try

                        If .Pnj.Acheter Then

                            For Each pair As sItems In VarItems.Values

                                If pair.ID.ToString = nomID OrElse pair.Nom.ToLower = nomID.ToLower Then

                                    If .Mitm.Send("EHP" & pair.Catégorie) Then

                                        Return .Mitm.Send("EHl" & pair.Catégorie)

                                    End If


                                    Return False

                                End If

                            Next

                        End If

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

            Public Function Categorie(nomID As String) As Boolean

                With Bot

                    Try

                        If .Pnj.Acheter Then

                            For Each pair As sItems In VarItems.Values

                                If pair.ID.ToString = nomID OrElse pair.Nom.ToLower = nomID.ToLower Then

                                    Return .Mitm.Send("EHT" & RetourneItemNomIdCategorie(pair.ID, "categorie"))

                                End If

                            Next

                        End If

                    Catch ex As Exception

                        ErreurFichier(.Personnage.NomDuPersonnage, "PnjSelectionneCategorie", ex.Message)

                    End Try

                    Return False

                End With

            End Function

        End Module

    End Namespace

    Namespace Echange
        Module Pnj_Function_Echange

            Public Function Echanger(nomID As String) As Boolean

                With Bot

                    Try

                        If .Pnj.Echange = False Then

                            For Each Pair As Map_Variable.Entite In .Map.Entite.Values

                                If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.ID = nomID Then

                                    Return .Mitm.Send("ER2|" & Pair.IDUnique,
                                                     {"ECK2"})

                                End If

                            Next

                        End If

                    Catch ex As Exception

                    End Try

                    Return False

                End With

            End Function

        End Module

    End Namespace

End Namespace