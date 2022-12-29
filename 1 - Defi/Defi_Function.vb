Namespace Defi_Function

    Public Class Defi_Function

        Public Function Annule() As Boolean

            With Bot

                Try

                    If .Defi.Invitation Then

                        Return .Mitm.Send("GQ",
                                         {"GE",
                                          "GV",
                                          "GDM"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Defi_Function_Annule", ex.Message)

                End Try

            End With

            Return False

        End Function

        Public Function Abandonne() As Boolean

            With Bot

                Try

                    If .Defi.Combat Then

                        Return .Mitm.Send("GQ",
                                         {"GE",
                                          "GV",
                                          "GDM"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Defi_Function_Abandonne", ex.Message)

                End Try

            End With

            Return False

        End Function

        Public Function Refuse() As Boolean

            With Bot

                Try

                    If .Defi.Invitation Then

                        Return .Mitm.Send("GA902" & .Defi.ID,
                                         {"GA;902"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Defi_Function_Refuse", ex.Message)

                End Try

            End With

            Return False

        End Function

        Public Function Accepte() As Boolean

            With Bot

                Try

                    If .Defi.Invitation Then

                        Return .Mitm.Send("GA901" & .Defi.ID,
                                         {"GA;901;" & .Personnage.ID.ToString & ";" & .Defi.ID.ToString})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Defi_Function_Accepte", ex.Message)

                End Try

            End With

            Return False

        End Function

        Public Function Invite(nom As String) As Boolean

            With Bot

                Try

                    For Each pair As Map_Variable.Entite In .Map.Entite.Values

                        If pair.Nom.ToLower = nom.ToLower Then

                            Return .Mitm.Send("GA900" & pair.IDUnique,
                                             {"GA;900;" & .Personnage.ID.ToString & ";" & pair.IDUnique.ToString}) ' Défi reçu.

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Defi_Function_Invite", ex.Message)

                End Try

            End With

            Return False

        End Function

    End Class

End Namespace