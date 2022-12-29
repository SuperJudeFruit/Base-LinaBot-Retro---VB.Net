Namespace Defi

    Module Defi

        Sub Reception(data As String)

            With Bot

                Try

                    ' GA ; 900 ; 1234567    ; 7654321
                    ' GA ; 900 ; Id Lanceur ; Id Receveur

                    Dim separateData As String() = Split(data, ";")

                    .Defi.ID = separateData(2)
                    .Defi.Invitation = True

                    If separateData(2) = .Personnage.ID Then

                        EcritureMessage("[Dofus]", "Tu défie " & .Map.Entite(separateData(3)).Nom, Color.Green)

                    ElseIf separateData(3) = .Personnage.ID Then

                        EcritureMessage("[Dofus]", .Map.Entite(separateData(2)).Nom & " te défie. acceptes-tu ?", Color.Green)

                    Else

                        .Defi.ID = -1
                        .Defi.Invitation = False

                        EcritureMessage("[Dofus]", .Map.Entite(separateData(2)).Nom & " défie " & .Map.Entite(separateData(3)).Nom, Color.Green)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Defi_Reception", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Refuser(data As String)

            With Bot

                Try

                    ' GA ; 902 ; 1234567    ; 7654321
                    ' GA ; 902 ; Id Lanceur ; Id Receveur

                    .Defi.ID = -1
                    .Defi.Invitation = False

                    EcritureMessage("[Dofus]", "Défi refusé.", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Defi_Refuser", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Accepter(data As String)

            With Bot

                Try

                    ' GA ; 901 ; 1234567    ; 7654321
                    ' GA ; 901 ; Id Lanceur ; Id Receveur

                    .Defi.Combat = True
                    .Defi.Invitation = False

                    EcritureMessage("[Dofus]", "Défi accepté.", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Defi_Accepter", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace