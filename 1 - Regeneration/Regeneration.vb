Namespace Regeneration

    Module Regeneration

        Sub Restaure(data As String)

            With Bot

                Try

                    ' ILF 1
                    ' ILF Nbr de pdv récupèré

                    EcritureMessage("[Dofus]", "Tu as retrouvé " & Mid(data, 4) & " points de vie.", Color.Green)

                    With .Personnage.Vitaliter

                        Dim Vitalité As Integer = .Actuelle + CInt(Mid(data, 4))

                        If Vitalité > .Maximum Then

                            .Actuelle = .Maximum

                        Else

                            .Actuelle = Vitalité

                        End If

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Regeneration_Restaure", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Seconde(data As String)

            With Bot

                Try


                    ' ILS 2000
                    ' ILS Temps à attendre pour 1 pdv

                    EcritureMessage("[Dofus]", "Votre personnage récupére 1 point de vie toutes les " & Mid(data, 4, 1) & " seconde(s).", Color.Green)

                    With LinaBot

                        .Timer_Regeneration.Enabled = False
                        .Timer_Regeneration.Interval = Mid(data, 4)
                        .Timer_Regeneration.Enabled = True

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Regeneration_Seconde", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace

