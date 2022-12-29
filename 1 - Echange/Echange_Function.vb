Namespace Echange_Function

    Module Echange_Function

        Public Function Invite(nom As String) As Boolean

            With Bot

                Try

                    If .Echange.Invitation = False AndAlso .Echange.Echange = False Then

                        For Each pair As KeyValuePair(Of Integer, Map_Variable.Entite) In .Map.Entite

                            If pair.Value.Nom.ToLower = nom.ToLower Then

                                Return .Mitm.Send("ER1|" & pair.Key,
                                                 {"ERK"},
                                                 {"EREO"})

                            End If

                        Next

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Function_Invite", ex.Message)

                End Try

                Return .Echange.Invitation

            End With

        End Function

        Public Function Refuse() As Boolean

            With Bot

                Try

                    If .Echange.Invitation Then

                        Return .Mitm.Send("EV",
                                         {"EV"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Function_Refuse", ex.Message)

                End Try

                Return Not .Echange.Invitation

            End With

        End Function

        Public Function Arrete() As Boolean

            With Bot

                Try

                    If .Echange.Echange Then

                        Return .Mitm.Send("EV",
                                         {"EV"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Function_Arrete", ex.Message)

                End Try

                Return Not .Echange.Echange

            End With

        End Function

        Public Function Accepte() As Boolean

            With Bot

                Try

                    If .Echange.Invitation Then

                        Return .Mitm.Send("EA",
                                         {"ECK1"},
                                         {"EV"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Function_Accepte", ex.Message)

                End Try

                Return .Echange.Echange

            End With

        End Function

        Public Function Kamas(quantite As String) As Boolean


            With Bot

                Try

                    If quantite > .Personnage.Kamas Then

                        quantite = .Personnage.Kamas

                    End If

                    Return .Mitm.Send("EMG" & quantite,
                                     {"EMKG",
                                      "EsKG"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Function_Kamas", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Valide() As Boolean

            With Bot

                Try

                    If .Echange.Echange AndAlso .Echange.Moi.Valider = False Then

                        Return .Mitm.Send("EK",
                                         {"EK1"},
                                         {"EK0"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Echange_Function_Valide", ex.Message)

                End Try

                Return .Echange.Moi.Valider

            End With

        End Function

    End Module

End Namespace
