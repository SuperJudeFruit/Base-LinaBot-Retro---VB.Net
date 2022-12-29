Namespace Sort_Function

    Module Sort_Function

        Public Function Up(nomID As String) As Boolean

            With Bot

                Try

                    If Not IsNumeric(nomID) Then

                        For Each pair As KeyValuePair(Of Integer, Sort_Variable.Information) In .Sort.Sort

                            If pair.Value.Nom.ToLower = nomID.ToLower OrElse pair.Key = nomID Then

                                nomID = pair.Value.ID

                                Exit For

                            End If

                        Next

                    End If

                    If .Sort.Sort.ContainsKey(nomID) Then

                        If .Personnage.Niveau >= .Sort.Sort(nomID).NiveauRequisUp Then

                            If .Personnage.Capital_Sort >= .Sort.Sort(nomID).Niveau Then

                                Return .Mitm.Send("SB" & .Sort.Sort(nomID).ID,
                                                 {"SUK"})

                            End If

                        End If

                    End If

                    Return False

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Sort_Function_Up", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Placement(nomID As String, barreSort As String) As Boolean

            With Bot

                Try

                    If Not IsNumeric(nomID) Then

                        For Each pair As KeyValuePair(Of Integer, Sort_Variable.Information) In .Sort.Sort

                            If pair.Value.Nom.ToLower = nomID.ToLower OrElse pair.Key = nomID Then

                                nomID = pair.Value.ID

                                Exit For

                            End If

                        Next

                    End If

                    If .Sort.Sort.ContainsKey(nomID.ToLower) Then

                        Return .Mitm.Send("SM" & .Sort.Sort(nomID.ToLower).ID & "|" & barreSort,
                                         {"BN"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Sort_Function_Placement", ex.Message)

                End Try

                Return False

            End With

        End Function

    End Module

End Namespace