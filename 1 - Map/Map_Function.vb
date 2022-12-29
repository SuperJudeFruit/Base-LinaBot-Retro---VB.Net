Namespace Map_Function

    Public Class Map_Function

        Public Function Deplacement(celluleDirection As String) As Boolean

            With Bot

                Try

                    If .Map.Deplacement = False Then

                        Dim macell = .Map.Entite(.Personnage.ID).Cellule

                        Select Case celluleDirection.ToLower

                            Case "haut"

                                celluleDirection = .Map.Haut

                            Case "bas"

                                celluleDirection = .Map.Bas

                            Case "gauche"

                                celluleDirection = .Map.Gauche

                            Case "droite"

                                celluleDirection = .Map.Droite

                        End Select

                        Dim pather As New Pathfinding()
                        Dim path As String = pather.Pathing(celluleDirection)

                        If path <> "" Then

                            .Map.Bloque.Reset()

                            .Mitm.Send("GA001" & path,
                                      {"GA0;1;" & .Personnage.ID.ToString},
                                      {"GA;0"})

                        End If

                        Return .Map.Bloque.WaitOne(30000)

                    End If

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

        Public Function ChangerOrientation(index As Integer, monOrientation As String) As Boolean

            With Bot

                Try

                    If .Combat.Combat = False AndAlso .Recolte.Recolte = False Then

                        Return .Mitm.Send("eD" & Orientation(monOrientation),
                                         {"eD" & .Personnage.ID})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "ChangerOrientation", ex.Message)

                End Try

                Return False

            End With

        End Function

        Private Shared Function Orientation(choix As String) As String

            Try

                Select Case choix.ToLower

                    Case "0", "est"

                        Return If(IsNumeric(choix), "est", "0")

                    Case "1", "sudest"

                        Return If(IsNumeric(choix), "sudest", "1")

                    Case "2", "sud"

                        Return If(IsNumeric(choix), "sud", "2")

                    Case "3", "sudouest"

                        Return If(IsNumeric(choix), "sudouest", "3")

                    Case "4", "ouest"

                        Return If(IsNumeric(choix), "ouest", "4")

                    Case "5", "nordouest"

                        Return If(IsNumeric(choix), "nordouest", "5")

                    Case "6", "nord"

                        Return If(IsNumeric(choix), "nord", "6")

                    Case "7", "nordest"

                        Return If(IsNumeric(choix), "nordest", "7")

                    Case Else

                        Return If(IsNumeric(choix), "sud", "2")

                End Select

            Catch ex As Exception

            End Try

            Return "1"

        End Function

        Public Function Agresser(index As Integer, nomIDJoueur As String) As Boolean

            With Bot

                Try

                    For Each pair As Map_Variable.Entite In .Map.Entite.Values

                        If pair.Nom.ToLower = nomIDJoueur.ToLower OrElse pair.IDUnique.ToString = nomIDJoueur Then

                            Return .Mitm.Send("GA906" & pair.IDUnique,
                                             {"GA;906;" & .Personnage.ID & ";" & pair.IDUnique},
                                             {"GA;906;" & .Personnage.ID & ";p"})

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "FunctionMap_Agresser", ex.Message)

                End Try

                Return False

            End With

        End Function

    End Class

End Namespace