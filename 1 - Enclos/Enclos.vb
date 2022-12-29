Namespace Enclos

    Module Enclos

        Public Sub Information(data As String)

            With Bot

                Try

                    ' Rp 12345    ; 0             ; 2               ; 2               ; Lenculer de bot ; i      , 5      , a      , 9srth
                    ' Rp id enclo ; Prix de vente ; Nbr DD ou objet ; Nbr DD ou objet ; Guilde          ; Blason , Blason , Blason , Blason

                    Dim separateData As String() = Split(Mid(data, 3), ";")

                    With .Enclos

                        .ID = separateData(0)
                        .Prix = separateData(1)
                        .Dragodinde = separateData(2)
                        .Objet = separateData(3)
                        .Guilde = separateData(4)
                        .Blason = separateData(5)

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Enclos_mdlEnclos_GiEncloMap", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace
