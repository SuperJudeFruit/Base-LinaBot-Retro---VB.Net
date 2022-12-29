Namespace Metier

    Module Metier

        Sub Equipement(data As String)

            With Bot

                Try

                    ' OT 28
                    ' OT Id Métier

                    If data.Length > 2 Then

                        If .Metier.ContainsKey(Mid(data, 3)) Then

                            .Metier(Mid(data, 3)).ItemEquipe = True

                        End If

                    Else

                        For Each pair As Metier_Variable.Base In .Metier.Values

                            pair.ItemEquipe = False

                        Next

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Metier_Equipement", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Mode_Public(data As String)

            With Bot

                Try

                    'EW + OU -

                    For Each Pair As Metier_Variable.Base In .Metier.Values

                        With Pair

                            Select Case Mid(data, 3)

                                Case "+"

                                    .ModePublic = True

                                Case "-"

                                    .ModePublic = False

                            End Select

                        End With

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Metier_Mode_Public", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Information(data As String)

            With Bot

                Try


                    ' JS | 17        ; 142                   ~ 2                    ~ 0                    ~ 0 ~ 70                  , next info métier actuel | Nex métier
                    ' JS | ID_Métier ; ID_Atelier/ressources ~ Nbr_Case/Récolte min ~ Nbr_Case/Récolte max ~ ? ~ %_Réussite ou temps ,                         |

                    Dim separateData As String() = Split(data, "|")

                    For i = 1 To separateData.Length - 1

                        Dim separate As String() = Split(separateData(i), ";")

                        Dim idJob As Integer = separate(0)

                        separate = Split(separate(1), ",")

                        Dim newMetier As New Metier_Variable.Base

                        With newMetier

                            .ID = idJob
                            .Nom = VarMetier(idJob).Nom

                            .Atelier = New Dictionary(Of Integer, Metier_Variable.Atelier)

                            For a = 0 To separate.Length - 1

                                Dim separateCraft As String() = Split(separate(a), "~")

                                Dim newMétierAtelierRessource As New Metier_Variable.Atelier

                                With newMétierAtelierRessource

                                    .ID = separateCraft(0)
                                    .Nom = VarMetier(idJob).AtelierRessource(separateCraft(0)).Nom
                                    .NombreCaseRecolte.Minimum = separateCraft(1)
                                    .NombreCaseRecolte.Maximum = separateCraft(2)
                                    .TempsReussite = separateCraft(4)
                                    .Action = VarMetier(idJob).AtelierRessource(separateCraft(0)).Action

                                End With

                                If .Atelier.ContainsKey(separateCraft(0)) Then

                                    .Atelier(separateCraft(0)) = newMétierAtelierRessource

                                Else

                                    .Atelier.Add(separateCraft(0), newMétierAtelierRessource)

                                End If

                            Next

                        End With

                        If .Metier.ContainsKey(idJob) Then

                            newMetier.ItemEquipe = .Metier(idJob).ItemEquipe
                            .Metier(idJob) = newMetier

                        Else

                            .Metier.Add(idJob, newMetier)

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Metier_Information", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Up(data As String)

            With Bot

                Try

                    'JN 28        | 73
                    'JN ID Métier | Level

                    Dim separateData As String() = Split(Mid(data, 3), "|")

                    If .Metier.ContainsKey(separateData(0)) Then

                        .Metier(separateData(0)).Niveau = separateData(1)

                    End If

                    EcritureMessage("[Dofus]", "Ton métier " & VarMetier(separateData(0)).Nom & " passe niveau " & separateData(1) & ".", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Metier_Up", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Experience(data As String)

            With Bot

                Try


                    ' JX | 17         ; 42     ; 41044   ; 43205      ; 43378   ; ? |
                    ' JX | ID_Métiers ; Niveau ; Exp_Min ; Exp_actuel ; Exp_Max ;   | Métier_Suivant

                    Dim separateData As String() = Split(data, "|")

                    For i = 1 To separateData.Length - 1

                        Dim separate As String() = Split(separateData(i), ";")

                        If separate(4) < 0 Then separate(4) = separate(3)

                        If .Metier.ContainsKey(separate(0)) Then

                            With .Metier(separate(0))

                                .Niveau = separate(1)

                                With .Experience

                                    .Minimum = separate(2)
                                    .Actuelle = separate(3)
                                    .Maximum = separate(4)
                                    .Pourcentage = .Actuelle / .Maximum * 100

                                End With

                            End With

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Metier_Experience", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub [Option](data As String)

            With Bot

                Try

                    'JO 0             | 4                      | 5
                    'JO Numéro_Métier | Nombre_Pour_Check_Case | Nbr minimum ingrédient 

                    Dim separateData As String() = Split(Mid(data, 3), "|")

                    For Each pair As Metier_Variable.Base In .Metier.Values

                        If CInt(separateData(0)) = 0 Then

                            With pair

                                .Payant = False
                                .NeFournitAucuneRessource = False
                                .GratuitSurEchec = False
                                .NombreIngredientMinimum = separateData(2)

                                While CInt(separateData(1)) > 0

                                    Select Case separateData(1)

                                        Case >= 4 'Ne Fournit aucune ressource

                                            .NeFournitAucuneRessource = True
                                            separateData(1) = CInt(separateData(1)) - 4

                                        Case >= 2 'Gratuit sur échec

                                            .GratuitSurEchec = True
                                            separateData(1) = CInt(separateData(1)) - 2

                                        Case >= 1 'Payant

                                            .Payant = True
                                            separateData(1) = CInt(separateData(1)) - 1

                                    End Select

                                End While

                            End With

                            Exit For

                        Else

                            separateData(0) = CInt(separateData(0)) - 1

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Metier_Option", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Supprime(data As String)

            With Bot

                Try

                    ' JR 56
                    ' JR id métier

                    If .Metier.ContainsKey(Mid(data, 3)) Then

                        .Metier.Remove(Mid(data, 3))

                    End If

                    EcritureMessage("[Dofus]", "Tu as désappris le métier " & VarMetier(Mid(data, 3)).Nom & ".", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Metier_Supprime", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace