Namespace Groupe_Function

    Module Groupe_Function

        Public Function Invite(nom As String) As Boolean

            With Bot

                Try

                    If .Groupe.Invitation = False Then

                        If .Groupe.Membre.Count < 8 Then

                            Return .Mitm.Send("PI" & nom,
                                             {"PIK"},
                                             {"PIEa",
                                              "PIEn"})

                        End If

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_Invite", ex.Message)

                End Try

                Return .Groupe.Invitation

            End With

        End Function

        Public Function Refuse() As Boolean

            With Bot

                Try

                    If .Groupe.Invitation Then

                        Return .Mitm.Send("PR",
                                         {"PR"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_Refuse", ex.Message)

                End Try

                Return .Groupe.Invitation

            End With

        End Function

        Public Function Arrete() As Boolean

            With Bot

                Try

                    If .Groupe.Invitation Then

                        Return .Mitm.Send("PR",
                                         {"PR"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_Arrete", ex.Message)

                End Try

                Return .Groupe.Invitation

            End With

        End Function

        Public Function Accepte() As Boolean

            With Bot

                Try

                    If .Groupe.Invitation Then

                        Return .Mitm.Send("PA",
                                         {"PCK"},
                                         {"PR"})


                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_Accepte", ex.Message)

                End Try

                Return .Groupe.Groupe

            End With

        End Function

        Public Function Quitte() As Boolean

            With Bot

                Try

                    If .Groupe.Groupe Then

                        Return .Mitm.Send("PV",
                                         {"PV"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_Quitte", ex.Message)

                End Try

                Return Not .Groupe.Groupe

            End With

        End Function

        Public Function SuivezMoiTous() As Boolean

            With Bot

                Try

                    If .Groupe.Groupe Then

                        Return .Mitm.Send("PG+" & .Personnage.ID,
                                         {"PFK",
                                          "Im052"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_SuivezMoiTous", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function ArretezTousDeMeSuivre() As Boolean

            With Bot

                Try

                    If .Groupe.Groupe Then

                        Return .Mitm.Send("PG-" & .Personnage.ID,
                                         {"PFK",
                                          "Im053",
                                          "IC"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_ArretezTousDeMeSuivre", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function SuivreLeDeplacement(nomID As String) As Boolean

            With Bot

                Try

                    If .Groupe.Groupe Then

                        For Each pair As Groupe_Variable.Information In CopyGroupe(.Groupe.Membre).Values

                            If pair.Nom.ToLower = nomID.ToLower OrElse pair.ID = nomID Then

                                Return .Mitm.Send("PF+" & pair.ID,
                                                 {"IC",
                                                  "PFK"})

                            End If

                        Next

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_SuivreLeDeplacement", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function NePlusSuivreLeDeplacement(nomID As String) As Boolean

            With Bot

                Try

                    If .Groupe.Groupe Then

                        For Each pair As Groupe_Variable.Information In CopyGroupe(.Groupe.Membre).Values

                            If pair.Nom.ToLower = nomID.ToLower OrElse pair.ID = nomID Then

                                Return .Mitm.Send("PF-" & pair.ID,
                                                 {"IC",
                                                  "PFK"})

                            End If

                        Next

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_NePlusSuivreLeDeplacement", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function SuivezLeTous(nomID As String) As Boolean

            With Bot

                Try

                    If .Groupe.Groupe Then

                        For Each pair As Groupe_Variable.Information In CopyGroupe(.Groupe.Membre).Values

                            If pair.Nom.ToLower = nomID.ToLower OrElse pair.ID = nomID Then

                                Return .Mitm.Send("PG+" & pair.ID,
                                                 {"IC",
                                                  "PFK"})

                            End If

                        Next

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_SuivezLeTous", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function ArretezTousDeLeSuivre(nomID As String) As Boolean

            With Bot

                Try

                    If .Groupe.Groupe Then

                        For Each pair As Groupe_Variable.Information In CopyGroupe(.Groupe.Membre).Values

                            If pair.Nom.ToLower = nomID.ToLower OrElse pair.ID = nomID Then

                                Return .Mitm.Send("PG-" & pair.ID,
                                                 {"IC",
                                                  "PFK"})

                            End If

                        Next

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_ArretezTousDeLeSuivre", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Exclure(nomID As String) As Boolean

            With Bot

                Try

                    If .Groupe.Groupe Then

                        For Each pair As Groupe_Variable.Information In CopyGroupe(.Groupe.Membre).Values

                            If pair.Nom.ToLower = nomID.ToLower OrElse pair.ID = nomID Then

                                Return .Mitm.Send("PV" & pair.ID,
                                                 {"PM-",
                                                  "PV"})

                            End If

                        Next

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Groupe_Function_Exclure", ex.Message)

                End Try

                Return False

            End With

        End Function

        Private Delegate Function DlgF()
        Public Function CopyGroupe(dico As Dictionary(Of Integer, Groupe_Variable.Information)) As Dictionary(Of Integer, Groupe_Variable.Information)

            Dim newDico As New Dictionary(Of Integer, Groupe_Variable.Information)

            Try

                For Each pair As KeyValuePair(Of Integer, Groupe_Variable.Information) In dico

                    newDico.Add(pair.Key, pair.Value)

                Next

            Catch ex As Exception

            End Try

            Return newDico

        End Function


    End Module

End Namespace