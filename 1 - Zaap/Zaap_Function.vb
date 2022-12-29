Namespace Zaap_Function

    Module Zaap_Function

        Public Function Utiliser() As Boolean

            With Bot

                Try

                    For Each pair As Interaction_Variable.Base In .Map.Interaction.Values

                        If pair.Nom = "Zaap" Then


                            Return .Mitm.Send("GA500" & pair.Cellule & ";114",
                                             {"WC",
                                              "Im024"})

                            Exit For

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Zaap_Function_Utiliser", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Sauvegarder() As Boolean

            With Bot

                Try

                    For Each pair As Interaction_Variable.Base In .Map.Interaction.Values

                        If pair.Nom = "Zaap" Then


                            Return .Mitm.Send("GA500" & pair.Cellule & ";44",
                                             {"Im06"})

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Zaap_Function_Sauvegarder", ex.Message)

                End Try

                Return False

            End With


        End Function

        Public Function Destination(map As String) As Boolean

            With Bot

                Try

                    If .Personnage.EnInteraction Then

                        If Not IsNumeric(map) Then

                            For Each pair As KeyValuePair(Of Integer, String) In VarMap

                                If pair.Value = map Then

                                    If .ZaapI.ContainsKey(pair.Key) Then

                                        map = pair.Key

                                        Exit For

                                    End If

                                End If

                            Next

                        End If

                        If .ZaapI.ContainsKey(map) Then

                            If .Personnage.Kamas >= .ZaapI(map) Then

                                Return .Mitm.Send("WU" & map,
                                                 {"GDM",
                                                  "WV"})

                            Else

                                EcritureMessage("(Bot)", "Vous avez pas asse de kamas pour utiliser le zaap, kamas requis : " & .ZaapI(map), Color.Red)

                            End If

                        Else

                            EcritureMessage("(Bot)", "Le bot n'a pas trouvé la map voulu dans les maps enregistré du Zaap.", Color.Red)

                        End If

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Zaap_Function_Destination", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Quitte() As Boolean

            With Bot

                Try

                    If .Personnage.EnInteraction Then

                        Return .Mitm.Send("WV",
                                         {"WV"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Zaap_Function_Quitte", ex.Message)

                End Try

                Return False

            End With

        End Function

    End Module

End Namespace