Namespace Zaap

    Module Zaap

        Public Sub Information(data As String)

            With Bot

                Try

                    ' WC 1242         | 3250  ; 450  | Next
                    ' WC Mapid actuel | Mapid ; Prix | Next

                    .Personnage.EnInteraction = True

                    .ZaapI.Clear()

                    Dim separateData As String() = Split(Mid(data, 3), "|")

                    For i = 1 To separateData.Length - 1

                        Dim separate As String() = Split(separateData(i), ";") '3250;450

                        If Not .ZaapI.ContainsKey(separate(0)) Then

                            .ZaapI.Add(separate(0), separate(1))

                        Else

                            .ZaapI(separate(0)) = separate(1)

                        End If

                    Next

                    EcritureMessage("[Dofus]", "Utilisation du Zaap en cours.", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Zaap_Information", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Public Sub Ferme(data As String)

            With Bot

                Try

                    ' WV

                    .Personnage.EnInteraction = False

                    .ZaapI.Clear()

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Zaap_Ferme", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace