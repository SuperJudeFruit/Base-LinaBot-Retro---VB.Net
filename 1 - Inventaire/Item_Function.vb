Namespace Item_Function

    Module Item_Function

        Public Function Supprime(nomID As String, Optional quantite As Integer = 999999) As Boolean

            With Bot

                Try

                    For Each Pair As Item_Variable.Information In .Inventaire.Item.Values

                        If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.IdObjet.ToString = nomID OrElse Pair.IdUnique.ToString = nomID Then

                            If Pair.Equipement = "" Then

                                If quantite > Pair.Quantiter Then quantite = Pair.Quantiter

                                EcritureMessage("(Bot)", "Suppression de l'item " & Pair.Nom & " x " & quantite, Color.Lime)

                                Return .Mitm.Send("Od" & Pair.IdUnique & "|" & quantite,
                                                 {"OR",
                                                  "OQ"})

                            End If


                        End If

                    Next

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

        Public Function Retire(nomID As String, Optional quantite As Integer = 999999) As Boolean

            With Bot

                Try

                    For Each Pair As Item_Variable.Information In .Echange.Moi.Inventaire.Item.Values

                        If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.IdObjet.ToString = nomID OrElse Pair.IdUnique.ToString = nomID Then

                            If Pair.Equipement = "" Then

                                If quantite > Pair.Quantiter Then quantite = Pair.Quantiter

                                EcritureMessage("(Bot)", "Retire l'item " & Pair.Nom & " x " & quantite, Color.Lime)

                                Return .Mitm.Send("EMO-" & Pair.IdUnique & "|" & quantite,
                                                 {"OQ",
                                                  "EsKO-",
                                                  "EMKO-",
                                                  "OAKO"})

                            End If


                        End If

                    Next

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

        Public Function Depose(nomID As String, Optional quantite As Integer = 999999) As Boolean

            With Bot

                Try

                    For Each Pair As Item_Variable.Information In .Inventaire.Item.Values

                        If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.IdObjet.ToString = nomID OrElse Pair.IdUnique.ToString = nomID OrElse nomID.ToLower = "all" Then

                            If Pair.Equipement = "" Then

                                If quantite > Pair.Quantiter Then quantite = Pair.Quantiter

                                EcritureMessage("(Bot)", "Dépose l'item " & Pair.Nom & " x " & quantite, Color.Lime)

                                Return .Mitm.Send("EMO+" & Pair.IdUnique & "|" & quantite,
                                                 {"OR",
                                                  "OQ",
                                                  "EMKO+",
                                                  "EsKO+"})

                            End If

                        End If

                    Next

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

        Public Function Existe(nomID As String) As Boolean

            With Bot

                Try

                    For Each Pair As Item_Variable.Information In .Inventaire.Item.Values

                        If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.IdObjet.ToString = nomID OrElse Pair.IdUnique.ToString = nomID Then

                            Return True

                        End If

                    Next

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

        Public Function Equipe(nomID As String, numero As Integer) As Boolean

            With Bot

                Try

                    For Each Pair As Item_Variable.Information In .Inventaire.Item.Values

                        If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.IdUnique.ToString = nomID OrElse Pair.IdObjet.ToString = nomID Then

                            If Pair.Equipement = "" Then

                                Return .Mitm.Send("OM" & Pair.IdUnique & "|" & numero,
                                                 {"OM",
                                                  "OCO"})

                            Else

                                Return True

                            End If

                        End If

                    Next

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

        Public Function Desequipe(nomID As String, numero As Integer) As Boolean

            With Bot

                Try

                    For Each Pair As Item_Variable.Information In .Inventaire.Item.Values

                        If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.IdUnique.ToString = nomID OrElse Pair.IdObjet.ToString = nomID Then

                            If Pair.Equipement <> "" AndAlso Pair.Equipement = numero Then

                                EcritureMessage("(Bot)", "Déséquipe l'item : " & Pair.Nom, Color.Lime)

                                Return .Mitm.Send("OM" & Pair.IdUnique & "|-1",
                                                 {"OM"})

                            End If

                        End If

                    Next

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

        Public Function Jette(nomID As String, Optional quantité As Integer = 999999) As Boolean

            With Bot

                Try

                    For Each Pair As Item_Variable.Information In .Inventaire.Item.Values

                        If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.IdObjet.ToString = nomID OrElse Pair.IdUnique.ToString = nomID Then

                            If Pair.Equipement = "" Then

                                If quantité > Pair.Quantiter Then quantité = Pair.Quantiter

                                EcritureMessage("(Bot)", "Jette l'item " & Pair.Nom & " x " & quantité, Color.Lime)

                                Return .Mitm.Send("OD" & Pair.IdUnique & "|" & quantité,
                                                 {"OR",
                                                  "OQ",
                                                  "GDO+"})

                            End If

                        End If

                    Next

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

        Public Function Utilise(nomID As String) As Boolean

            With Bot

                Try

                    For Each Pair As Item_Variable.Information In .Inventaire.Item.Values

                        If Pair.Nom.ToLower = nomID.ToLower OrElse Pair.IdObjet.ToString = nomID OrElse Pair.IdUnique.ToString = nomID Then

                            EcritureMessage("(Bot)", "Utilisation de l'item " & Pair.Nom, Color.Lime)

                            Return .Mitm.Send("OU" & Pair.IdUnique & "|",
                                             {"OQ",
                                              "OR",
                                              "GDM"})

                        End If

                    Next

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

    End Module

End Namespace