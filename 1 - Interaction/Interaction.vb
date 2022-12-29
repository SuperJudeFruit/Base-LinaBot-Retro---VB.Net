Namespace Interaction

    Module Interaction

        Sub Information(data As String)

            With Bot

                Try

                    'GDF | 206     ; 3    ; 0
                    'GDF | Cellule ; Etat ; Utilisation                

                    Dim separateData As String() = Split(data, "|")

                    For i = 1 To separateData.Length - 1

                        Dim separate As String() = Split(separateData(i), ";")

                        If .Interaction.ContainsKey(separate(0)) Then

                            With .Interaction(separate(0))

                                .Disponible = separate(2)

                                Select Case separate(1) 'State now

                                    Case 2 'In Cut

                                        .Etat = "En Utilisation"

                                    Case 3, 4 'Cut

                                        .Disponible = "Indisponible"

                                    Case Else

                                        .Disponible = "Disponible"

                                        EcritureMessage("(Bot)", "L'état de la ressource '" & .Nom & "' est inconnu, cellid : " & separate(0) & " Etat : " & separate(1), Color.Red)

                                End Select

                            End With

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Interaction_Information", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace