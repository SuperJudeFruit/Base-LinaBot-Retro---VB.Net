Namespace Dragodinde_Function

    Public Class Dragodinde_Function

        Public Function Monte() As Boolean

            Try

                With Bot

                    If .Dragodinde.Monter = False Then

                        Return .Mitm.Send("Rr",
                                         {"Rr+"})

                    End If

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

        Public Function Descend() As Boolean

            Try

                With Bot

                    If .Dragodinde.Monter = True Then

                        Return .Mitm.Send("Rr",
                                         {"Rr-"})

                    End If

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

        Public Function XP(Valeur As Integer) As Boolean

            Try

                With Bot

                    If .Dragodinde.Equiper.Montable Then

                        Return .Mitm.Send("Rx" & Valeur,
                                         {"Rx"})

                    End If

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

        Public Shared Function Nom(_Nom As String) As Boolean

            Try

                If _Nom <> "" Then

                    Return Bot.Mitm.Send("Rn" & _Nom,
                                        {"Rn"})

                End If

            Catch ex As Exception

            End Try

            Return False

        End Function

        Public Function Libere() As Boolean

            Try

                With Bot

                    If .Dragodinde.Equiper.IdUnique > -1 Then

                        Return .Mitm.Send("Rf",
                                         {"Re-",
                                          "Rx0"})

                    End If

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

        Public Function Castre() As Boolean

            Try

                With Bot

                    If .Dragodinde.Equiper.IdUnique > -1 Then

                        Return .Mitm.Send("Rc",
                                         {"Re+"})


                    End If

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

        Public Function Inventaire() As Boolean

            Try

                With Bot

                    If .Dragodinde.Equiper.IdUnique > -1 Then

                        Return .Mitm.Send("ER15|",
                                         {"ECK15"})

                    End If

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

        Private Function Information(IdUnique As Integer) As Dragodinde_Variable.Information

            Try

                With Bot

                    Dim newInventaire As Dictionary(Of Integer, Item_Variable.Information) = .Inventaire.Item

                    If newInventaire.ContainsKey(IdUnique) Then

                        .Mitm.Send(newInventaire(IdUnique).Caracteristique.Dragodinde.IdUnique,
                                  {"Rd"})

                        Return .Dragodinde.Information

                    End If

                End With

            Catch ex As Exception

            End Try

            Return New Dragodinde_Variable.Information

        End Function

#Region "Etable"

        Public Function Etable_Equipe(nomIDType As String) As Boolean

            Try

                With Bot

                    For Each pair As KeyValuePair(Of Integer, Dragodinde_Variable.Information) In .Dragodinde.Etable

                        If pair.Value.ID.ToString = nomIDType OrElse pair.Value.Type.ToLower = nomIDType.ToLower OrElse pair.Value.Nom.ToLower = nomIDType.ToLower Then

                            Return .Mitm.Send("Erg" & pair.Key,
                                             {"Re+",
                                              "Ee-"})

                        End If

                    Next

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

        Public Function Etable_Enclos(nomIDType As String) As Boolean

            Try

                With Bot

                    For Each pair As KeyValuePair(Of Integer, Dragodinde_Variable.Information) In .Dragodinde.Etable

                        If pair.Value.ID.ToString = nomIDType OrElse pair.Value.Type.ToLower = nomIDType.ToLower OrElse pair.Value.Nom.ToLower = nomIDType.ToLower Then

                            Return .Mitm.Send("Efp" & pair.Key,
                                             {"Ef+",
                                              "Ee-"})

                        End If

                    Next

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

        Public Function Etable_Echanger(nomIDType As String) As Boolean

            Try

                With Bot

                    For Each pair As KeyValuePair(Of Integer, Dragodinde_Variable.Information) In .Dragodinde.Etable

                        If pair.Value.ID.ToString = nomIDType OrElse pair.Value.Type.ToLower = nomIDType.ToLower OrElse pair.Value.Nom.ToLower = nomIDType.ToLower Then

                            Return .Mitm.Send("Erc" & pair.Key,
                                             {"OAKO",
                                              "Ee-"})

                        End If

                    Next

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

#End Region

#Region "Equipe"

        Public Function Equipe_Etable(nomIDType As String) As Boolean

            Try

                With Bot

                    If .Dragodinde.Equiper.ID.ToString = nomIDType OrElse .Dragodinde.Equiper.Type.ToLower = nomIDType.ToLower OrElse .Dragodinde.Equiper.Nom.ToLower = nomIDType.ToLower Then

                        Return .Mitm.Send("Erp" & .Dragodinde.Equiper.IdUnique,
                                         {"Re-",
                                          "Ee+"})

                    End If

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

#End Region

#Region "Enclos"

        Public Function Enclos_Etable(nomIDType As String) As Boolean

            Try

                With Bot

                    For Each pair As KeyValuePair(Of Integer, Dragodinde_Variable.Information) In .Dragodinde.Enclos

                        If pair.Value.ID.ToString = nomIDType OrElse pair.Value.Type.ToLower = nomIDType.ToLower OrElse pair.Value.Nom.ToLower = nomIDType.ToLower Then

                            Return .Mitm.Send("Efg" & pair.Key,
                                             {"Ee+"})

                        End If

                    Next

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

#End Region

#Region "Certificas"

        Public Function Echange_Etable(nomIDType As String) As Boolean

            Try

                With Bot

                    For Each pair As KeyValuePair(Of Integer, Item_Variable.Information) In .Inventaire.Item

                        If pair.Value.IdObjet.ToString = nomIDType.ToLower OrElse pair.Value.IdUnique.ToString = nomIDType.ToLower OrElse
                       pair.Value.Caracteristique.Dragodinde.Nom.ToLower = nomIDType.ToLower OrElse pair.Value.Nom.ToLower = nomIDType.ToLower Then

                            Return .Mitm.Send("ErC" & pair.Key,
                                             {"Ee+"})

                        End If

                    Next

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

#End Region

    End Class

End Namespace