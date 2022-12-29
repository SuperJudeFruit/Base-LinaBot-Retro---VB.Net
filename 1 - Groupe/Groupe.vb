Namespace Groupe

    Module Groupe

        Sub Invitation(data As String)

            With Bot

                Try

                    ' PIK Linaculer           | Sispano
                    ' PIK Personne qui invite | Invité

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    With .Groupe

                        .Invitation = True
                        .Inviteur = separateData(0)
                        .Inviter = separateData(1)

                    End With

                    If separateData(0).ToLower = .Personnage.NomDuPersonnage.ToLower Then

                        EcritureMessage("[Dofus]", "Tu invites " & separateData(1) & " à rejoindre ton groupe...", Color.Green)

                    Else

                        EcritureMessage("[Dofus]", separateData(0) & " t'invite à rejoindre son groupe." & vbCrLf &
                                                   "Acceptes-tu ?", Color.Green)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Invitation", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Rejoint(data As String)

            With Bot

                Try


                    ' PCK Linaculer
                    ' PCK nom

                    With .Groupe

                        .Invitation = False
                        .Groupe = True

                    End With

                    EcritureMessage("[Dofus]", "Tu viens de rejoindre le groupe de " & Mid(data, 4) & ".", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Rejoint", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Erreur(data As String)

            With Bot

                Try

                    ' PIE a
                    ' PIE n Linaculer

                    .Groupe.Invitation = False

                    Select Case Mid(data, 4, 1)

                        Case "a"

                            EcritureMessage("[Dofus]", "Impossible, ce joueur fait déjà partie d'un groupe.", Color.Red)

                        Case "n"

                            EcritureMessage("[Dofus]", Mid(data, 5) & " n'est pas connecté ou n'existe pas.", Color.Red)

                    End Select

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Erreur", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Chef(data As String)

            With Bot

                Try

                    ' PL 1234567
                    ' PL id chef

                    .Groupe.Chef = Mid(data, 3)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Chef", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Fin(data As String)

            With Bot

                Try

                    ' PR

                    .Groupe.Invitation = False

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Fin", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Ajoute(data As String)

            With Bot

                Try


                    ' PM+ 1234567   ; Linaculer ; 91         ; -1       ; -1       ; -1       ;     , 9aa    , 9a9  ,          ,          ; 111        , 111     ; 4      ; 203          ; 104         ; 0 |Next
                    ' PM+ Id Unique ; Nom       ; Id Classe  ; Couleur1 ; Couleur2 ; Couleur3 ; Cac , Coiffe , Cape , Familier , Bouclier ; Pdv actuel , Pdv Max ; Niveau ; Initiative   ; Prospection ; ? |

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    For i = 0 To separateData.Length - 1

                        Dim separate As String() = Split(separateData(i), ";")

                        If Not .Groupe.Membre.ContainsKey(separate(0)) Then

                            Dim newGroupe As New Groupe_Variable.Information

                            With newGroupe

                                .ID = separate(0) ' IdUnique
                                .Nom = separate(1) ' Nom
                                .ClasseSexe = separate(2) ' IdClasse
                                .Couleur1 = "&H" & separate(3) ' Couleur1
                                .Couleur2 = "&H" & separate(4) ' Couleur2
                                .Couleur3 = "&H" & separate(5) ' Couleur3

                                With .Equipement

                                    Dim separateInfo As String() = Split(separate(6), ",")

                                    If separateInfo(0) <> Nothing Then

                                        .Cac = Convert.ToInt64(separateInfo(0), 16)

                                    End If

                                    If separateInfo(1) <> Nothing Then

                                        Dim separateObvijevan As String() = Split(separateInfo(1), "~")

                                        .Chapeau.ID = Convert.ToInt64(separateObvijevan(0), 16)

                                        If separateInfo(1).Contains("~"c) Then

                                            .Chapeau.Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                            .Chapeau.Forme = Convert.ToInt64(separateObvijevan(2), 16)

                                        End If

                                    Else

                                        .Chapeau = New Map_Variable.Information

                                    End If

                                    If separateInfo(2) <> Nothing Then

                                        Dim separateObvijevan As String() = Split(separateInfo(2), "~")

                                        .Cape.ID = Convert.ToInt64(separateObvijevan(0), 16)

                                        If separateInfo(2).Contains("~"c) Then

                                            .Cape.Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                            .Cape.Forme = Convert.ToInt64(separateObvijevan(2), 16)

                                        End If

                                    Else

                                        .Cape = New Map_Variable.Information

                                    End If

                                    If separateInfo(3) <> Nothing Then

                                        .Familier = Convert.ToInt64(separateInfo(3), 16)

                                    End If

                                    If separateInfo(4) <> Nothing Then

                                        .Bouclier = Convert.ToInt64(separateInfo(4), 16)

                                    End If

                                End With

                                With .Vitaliter

                                    .Actuelle = Split(separate(7), ",")(0)
                                    .Maximum = Split(separate(7), ",")(1)
                                    .Pourcentage = .Actuelle / .Maximum * 100

                                End With

                                .Niveau = separate(8) ' Niveau
                                .Initiative = separate(9) ' Initiative
                                .Prospection = separate(10) ' Prospection

                            End With

                            .Groupe.Membre.Add(separate(0), newGroupe)

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Ajoute", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Supprime(data As String)

            With Bot

                Try

                    ' PM- 01234567
                    ' PM- Id Unique

                    Dim idUnique As Integer = Mid(data, 4)

                    If .Groupe.Membre.ContainsKey(idUnique) Then

                        EcritureMessage("[Groupe]", "Le joueur " & .Groupe.Membre(idUnique).Nom & " a quitté le groupe.", Color.Red)

                        .Groupe.Membre.Remove(idUnique)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Supprime", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Modifie(data As String)

            With Bot

                Try

                    ' PM~ 1234567   ; Linaculer ; 91         ; -1       ; -1       ; -1       ;     , 9aa    , 9a9  ,          ,          ; 107        , 111     ; 4      ; 195        ; 104         ; 0 
                    ' PM~ Id Unique ; Nom       ; Id Classe  ; Couleur1 ; Couleur2 ; Couleur3 ; Cac , Coiffe , Cape , Familier , Bouclier ; Pdv actuel , Pdv Max ; Niveau ; Initiative ; Prospection ; ? 

                    Dim separate As String() = Split(Mid(data, 4), ";")

                    If .Groupe.Membre.ContainsKey(separate(0)) Then

                        With .Groupe.Membre(separate(0))

                            .ID = separate(0) ' IdUnique
                            .Nom = separate(1) ' Nom
                            .ClasseSexe = separate(2) ' IdClasse
                            .Couleur1 = "&H" & separate(3) ' Couleur1
                            .Couleur2 = "&H" & separate(4) ' Couleur2
                            .Couleur3 = "&H" & separate(5) ' Couleur3

                            With .Equipement

                                Dim separateInfo As String() = Split(separate(6), ",")

                                If separateInfo(0) <> Nothing Then

                                    .Cac = Convert.ToInt64(separateInfo(0), 16)

                                End If

                                If separateInfo(1) <> Nothing Then

                                    Dim separateObvijevan As String() = Split(separateInfo(1), "~")

                                    .Chapeau.ID = Convert.ToInt64(separateObvijevan(0), 16)

                                    If separateInfo(1).Contains("~"c) Then

                                        .Chapeau.Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                        .Chapeau.Forme = Convert.ToInt64(separateObvijevan(2), 16)

                                    End If

                                Else

                                    .Chapeau = New Map_Variable.Information

                                End If

                                If separateInfo(2) <> Nothing Then

                                    Dim separateObvijevan As String() = Split(separateInfo(2), "~")

                                    .Cape.ID = Convert.ToInt64(separateObvijevan(0), 16)

                                    If separateInfo(2).Contains("~"c) Then

                                        .Cape.Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                        .Cape.Forme = Convert.ToInt64(separateObvijevan(2), 16)

                                    End If

                                Else

                                    .Cape = New Map_Variable.Information

                                End If

                                If separateInfo(3) <> Nothing Then

                                    .Familier = Convert.ToInt64(separateInfo(3), 16)

                                End If

                                If separateInfo(4) <> Nothing Then

                                    .Bouclier = Convert.ToInt64(separateInfo(4), 16)

                                End If

                            End With

                            With .Vitaliter

                                .Actuelle = Split(separate(7), ",")(0)
                                .Maximum = Split(separate(7), ",")(1)
                                .Pourcentage = .Actuelle / .Maximum * 100

                            End With

                            .Niveau = separate(8) ' Niveau
                            .Initiative = separate(9) ' Initiative
                            .Prospection = separate(10) ' Prospection

                        End With

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Modifie", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Quitte(data As String)

            With Bot

                Try

                    ' PV 1234567
                    ' PV Id joueur

                    If data = "PV" Then

                        EcritureMessage("[Dofus]", "Tu as quitté ton groupe.", Color.Green)

                    Else

                        If .Groupe.Membre.ContainsKey(Mid(data, 3)) Then

                            EcritureMessage("[Dofus]", .Groupe.Membre(Mid(data, 3)).Nom & " vient de t'exclure du groupe.", Color.Green)

                        Else

                            EcritureMessage("[Dofus]", "Tu as quitté ton groupe.", Color.Green)

                        End If

                    End If

                    .Groupe = New Groupe_Variable.Base

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Quitte", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Suivez_Tous(data As String)

            With Bot

                Try

                    ' PFK 1234567
                    ' PFK id Unique

                    If data = "PFK" Then

                        .Groupe.ID = -1

                    Else

                        .Groupe.ID = Mid(data, 4)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Suivez_Tous", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Suivre_Coordonnees(data As String)

            With Bot

                Try

                    ' IC
                    ' IC  -23 | 34  | 2 | 1234567 
                    ' IC  Map | Map | ? | Id Unique Suivi
                    ' Map = [-23,34]

                    With .Groupe

                        If data <> "IC" Then

                            Dim separate As String() = Split(Mid(data, 3), "|")

                            .Coordonnees = separate(0) & "," & separate(1)

                            If .Membre.ContainsKey(separate(3)) Then

                                EcritureMessage("[Dofus]", .Membre(separate(3)).Nom & " se trouve sur la map [" & separate(0) & "," & separate(1) & "].", Color.Green)

                            End If

                        Else

                            .Coordonnees = ""

                        End If

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Suivre_Coordonnees", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace