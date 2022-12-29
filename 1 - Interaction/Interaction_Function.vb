Namespace Interaction_Function

    Module Interaction_Function

        Public Function Interaction(nomInteraction As String, action As String, Optional cellule As String = "") As Boolean

            With Bot

                Try

                    For Each Pair As Interaction_Variable.Base In .Map.Interaction.Values

                        If Pair.Nom.ToLower = nomInteraction.ToLower Then

                            If Pair.Disponible Then

                                For Each PairValue As KeyValuePair(Of String, Integer) In VarInteraction(Pair.Sprite).DicoInteraction

                                    If PairValue.Key.ToLower = action.ToLower Then

                                        If cellule = "" Then cellule = Pair.Cellule

                                        Dim newDeplacement As New Map_Function.Map_Function
                                        newDeplacement.Deplacement(cellule)

                                        Return .Mitm.Send("GA500" & cellule & ";" & PairValue.Value,
                                                         {"GA1;500;" & .Personnage.ID.ToString, ' Recolte
                                                          "GA0;500;" & .Personnage.ID.ToString, ' Recolte
                                                          "GDF|" & cellule & ";2;0", ' En Utilisation (Utile pour recolte)          
                                                          "Wc", ' Zaapi
                                                          "WC", ' Zaap
                                                          "ECK16" ' Enclos
                                                         },
                                                         {
                                                          "GDF|" & cellule & ";3;0", ' Indisponible (Utile pour recolte)
                                                          "GDF|" & cellule & ";4;0", ' Indisponible (Utile pour recolte)
                                                          "BN",
                                                          "GA;0"
                                                         })

                                    End If

                                Next

                            End If

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Interaction_Function_Interaction", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Quitte(choix As String) As Boolean

            With Bot

                Try

                    Select Case choix.ToLower

                        Case Else

                            Return .Mitm.Send("EV",
                                        {"EV"})

                    End Select

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Interaction_Function_Quitte", ex.Message)

                End Try

                Return False

            End With

        End Function

    End Module

End Namespace