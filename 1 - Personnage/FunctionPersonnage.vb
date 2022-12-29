Public Class FunctionPersonnage

    Public Function Energie(choix As String) As Integer

        Try

            With Bot

                With .Personnage.Energie

                    Select Case choix.ToLower

                        Case "actuelle", "actuel"

                            Return .Actuelle

                        Case "maximum"

                            Return .Maximum

                        Case "pr", "%"

                            Return .Pourcentage

                        Case Else

                            Return 0

                    End Select

                End With

            End With

        Catch ex As Exception

            ErreurFichier(Bot.Personnage.NomDuPersonnage & "_" & Bot.Personnage.Serveur, "FunctionCaracteristique_Energie", ex.Message)

        End Try

        Return 0

    End Function

    Public Function Niveau() As Integer

        Try

            With Bot

                Return .Personnage.Niveau

            End With

        Catch ex As Exception

            ErreurFichier(Bot.Personnage.NomDuPersonnage & "_" & Bot.Personnage.Serveur, "FunctionCaracteristique_Niveau", ex.Message)

        End Try

        Return 0

    End Function

    Public Function Experience(choix As String) As Integer

        Try

            With Bot

                With .Personnage.Experience

                    Select Case choix.ToLower

                        Case "minimum"

                            Return .Minimum

                        Case "actuelle", "actuel"

                            Return .Actuelle

                        Case "maximum"

                            Return .Maximum

                        Case "pr", "%"

                            Return .Pourcentage

                        Case Else

                            Return 0

                    End Select

                End With

            End With

        Catch ex As Exception

            ErreurFichier(Bot.Personnage.NomDuPersonnage & "_" & Bot.Personnage.Serveur, "FunctionCaracteristique_Experience", ex.Message)

        End Try

        Return 0

    End Function

    Public Function PointDeVie(choix As String) As Integer

        Try

            With Bot

                With .Personnage.Vitaliter

                    Select Case choix.ToLower

                        Case "actuelle", "actuel"

                            Return .Actuelle

                        Case "maximum"

                            Return .Maximum

                        Case "pr", "%"

                            Return .Pourcentage

                        Case Else

                            Return 0

                    End Select

                End With

            End With

        Catch ex As Exception

            ErreurFichier(Bot.Personnage.NomDuPersonnage & "_" & Bot.Personnage.Serveur, "FunctionCaracteristique_PointDeVie", ex.Message)

        End Try

        Return 0

    End Function

End Class
