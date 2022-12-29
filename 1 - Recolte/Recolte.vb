Namespace Recolte

    Module Recolte

        Sub Information(data As String)

            With Bot

                Try

                    ' GA0 ; 501     ; 0123456   ; 35         , 18800
                    ' GA0 ; Récolte ; ID Joueur ; Cellule ID , Temps en milliseconde

                    Dim separateData As String() = Split(data, ";")

                    Dim idPlayer As Integer = separateData(2) ' 0123456

                    Dim send As String = Mid(separateData(0), 3) ' GA0

                    separateData = Split(separateData(3), ",") ' 35,18800

                    If idPlayer = .Personnage.ID Then

                        .Recolte.Recolte = True
                        .Personnage.EnInteraction = True

                        .Personnage.InteractionCellule = separateData(0)
                        .Recolte.Numero += 1

                        EcritureMessage("(Bot)", "Temps de récolte : " & If(separateData(1).Length = 4, Mid(separateData(1), 1, 1), Mid(separateData(1), 1, 2)) & " Seconde(s)", Color.Lime)
                        EcritureMessage("(Bot)", "Récolte n° " & .Recolte.Numero, Color.Lime)

                        Wait(separateData(1), "GKK" & send, separateData(0))

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Recolte_Information", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Private Async Sub Wait(pause As Integer, envoie As String, cellule As String)

            With Bot

                Try

                    Await Task.Delay(pause)

                    If .Mitm.Send(envoie,
                                 {"GDF|" & cellule & ";3;0",
                                  "GDF|" & cellule & ";4;0"}) Then

                        .Personnage.EnInteraction = False
                        .Recolte.Recolte = False

                    Else

                        Echec(cellule)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Recolte_Wait", envoie & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Echec(cellule As String)

            With Bot

                Try

                    If .Map.Interaction.ContainsKey(cellule) Then

                        .Map.Interaction(cellule).Disponible = False

                        EcritureMessage("(Bot)", "Impossible de récolter.", Color.Red)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Recolte_Echec", ex.Message)

                End Try

            End With

        End Sub

        Sub Drop(data As String)

            With Bot

                Try

                    'IQ 1234567   | 2
                    'IQ ID Joueur | Quantité

                    Dim separate As String() = Split(Mid(data, 3), "|")

                    If separate(0) = .Personnage.ID Then

                        EcritureMessage("[Dofus]", "Vous avez obtenue " & separate(1) & " récolte(s).", Color.Green)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Recolte_Drop", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace