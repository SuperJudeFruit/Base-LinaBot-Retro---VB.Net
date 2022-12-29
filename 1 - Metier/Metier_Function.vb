Namespace Metier_Function

    Module Metier_Function

        Public Function [Option](nomID As String, payant As Boolean, gratuitSurEchec As Boolean, neFournitAucuneRessource As Boolean, nombreIngredientMinimum As Integer) As Boolean

            With Bot

                Try

                    Dim numeroMetier As Integer = 0
                    Dim NbrOption As Integer

                    For Each pair As Metier_Variable.Base In .Metier.Values

                        If pair.Nom.ToLower = nomID.ToLower OrElse pair.ID = nomID Then

                            If payant Then

                                NbrOption += 1

                            End If

                            If gratuitSurEchec Then

                                NbrOption += 2

                            End If

                            If neFournitAucuneRessource Then

                                NbrOption += 4

                            End If

                            Return .Mitm.Send("JO" & numeroMetier & "|" & NbrOption & "|" & nombreIngredientMinimum,
                                             {"JO" & numeroMetier})

                        End If

                        numeroMetier += 1

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Metier_Function_[Option]", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function [Public](Activer As Boolean) As Boolean

            With Bot

                Try

                    If Activer Then

                        Return .Mitm.Send("EW+",
                                         {"EW+"})

                    Else

                        Return .Mitm.Send("EW-",
                                         {"EW-"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Metier_Function_Public", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Existe(nomID As String) As Boolean

            With Bot

                Try

                    For Each pair As Metier_Variable.Base In .Metier.Values

                        If pair.Nom.ToLower = nomID.ToLower OrElse pair.ID = nomID Then

                            Return True

                        End If

                    Next

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

        Public Function Niveau(nomID As String) As Integer

            With Bot

                Try

                    For Each pair As Metier_Variable.Base In .Metier.Values

                        If pair.Nom.ToLower = nomID.ToLower OrElse pair.ID = nomID Then

                            Return pair.Niveau

                        End If

                    Next

                Catch ex As Exception

                End Try

                Return 0

            End With

        End Function

    End Module

End Namespace

