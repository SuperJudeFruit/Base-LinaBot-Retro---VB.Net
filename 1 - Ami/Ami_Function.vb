Namespace Ami_Function

    Public Class Ami_Function

        Public Function Ouvre(Valeur As String) As Boolean

            Try

                With Bot

                    Select Case Valeur.ToLower

                        Case "ami", "amie"

                            Return .Mitm.Send("FL",
                                        {"FL"})

                        Case "ennemi", "ennemie"

                            Return .Mitm.Send("iL",
                                        {"iL"})

                    End Select

                End With

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Function_Ouvre", ex.Message)

            End Try

            Return False

        End Function

        Public Function Supprime(pseudoNom As String, choix As String) As Boolean

            Try

                With Bot

                    For Each Pair As Ami_Variable.Information In If(choix.ToLower.StartsWith("ami"), Bot.Ami.Liste, Bot.Ennemi.Liste).Values

                        If Pair.Pseudo.ToLower = pseudoNom.ToLower OrElse Pair.Nom.ToLower = pseudoNom.ToLower Then

                            Select Case choix.ToLower

                                Case "ami", "amie"

                                    Return .Mitm.Send("FD*" & Pair.Pseudo,
                                                    {"FDK"},
                                                    {"FAEf"})

                                Case "ennemi", "ennemie"

                                    Return .Mitm.Send("iD*" & Pair.Pseudo,
                                                    {"iDK"},
                                                    {"iAEf"})

                            End Select

                        End If

                    Next

                End With

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Function_Supprime", ex.Message)

            End Try

            Return False

        End Function

        Public Function Ajoute(pseudoNom As String, choix As String) As Boolean

            Try

                With Bot

                    Select Case choix.ToLower

                        Case "ami", "amie"

                            Return .Mitm.Send("FA" & pseudoNom,
                                        {"FAEa",
                                         "FAK"},
                                        {"FAEf"})


                        Case "ennemi", "ennemie"

                            Return .Mitm.Send("iA%" & pseudoNom,
                                        {"iAEa",
                                         "iAK"},
                                        {"iAEf"})

                    End Select

                End With

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Function_Ajoute", ex.Message)

            End Try

            Return False

        End Function

        Public Function Information(Nom As String) As Boolean

            Try

                Return Bot.Mitm.Send("BW" & Nom,
                                    {"BWK"},
                                    {"BWE"})

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Function_Information", ex.Message)

            End Try

            Return False

        End Function

        Public Function Rejoindre(Nom As String) As Boolean

            Try

                Return Bot.Mitm.Send("FJF" & Nom,
                                    {"GDM"},
                                    {"Im113",
                                     "Im137"})

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Function_Rejoindre", ex.Message)

            End Try

            Return False

        End Function

        Public Function Averti(Valeur As Boolean) As Boolean

            Try

                Return Bot.Mitm.Send("FO" &
                                     If(Valeur, "+", "-"),
                                    {"BN"})

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Function_Averti", ex.Message)

            End Try

            Return False

        End Function

    End Class

End Namespace