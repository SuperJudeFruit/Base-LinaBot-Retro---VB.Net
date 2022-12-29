Namespace Ami

    Module Ami

        Sub Reception(data As String)

            Try

                With Bot

                    'FL | Linaculer#9999  ; 1      ; Linaculers   ; 1     ; 0           ; 7         ; 1    ; 71 
                    'FL | Pseudo          ; en ami ; Nom          ; Level ; alignement  ; id classe ; Sexe ; classe + sexe
                    'iL = ennemi

                    Dim separateData As String() = Split(data, "|")

                    Select Case separateData(0)

                        Case "FL"

                            .Ami.Reset()

                        Case "iL"

                            .Ennemi.Reset()

                    End Select

                    If separateData.Length > 1 Then

                        For i = 1 To separateData.Length - 1

                            Dim separate As String() = Split(separateData(i), ";")

                            Dim newFriend As New Ami_Variable.Information

                            With newFriend

                                .Pseudo = separate(0) ' Linaculer#9999

                                If separate.Length > 1 Then

                                    .Ajoute = separate(1) <> "?"

                                    .Connecte = True

                                    .Nom = separate(2)

                                    .Niveau = If(separate(3) = "?", -1, separate(3))

                                    .Alignement = separate(4)

                                    .Classe = separate(5)

                                    .Sex = separate(6)

                                    .ClasseSex = separate(7)

                                End If

                            End With

                            Select Case separateData(0)

                                Case "FL"

                                    .Ami.Ajoute = newFriend

                                Case "iL"

                                    .Ennemi.Ajoute = newFriend

                            End Select

                        Next

                    End If

                End With

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Reception", data & vbCrLf & ex.Message)

            End Try

        End Sub

        Sub Ajoute(data As String)

            Try

                With Bot

                    ' FAK Linacu ; 2 ; Linaculer ; 99     ; 9      ; 0    ; 90 
                    ' FAK Pseudo ; ? ; Nom       ; Niveau ; Classe ; Sexe ; Classe + Sexe

                    Dim separateData As String() = Split(Mid(data, 4), ";")

                    Select Case Mid(separateData(0), 1, 3)

                        Case "FAK"

                            EcritureMessage("[Dofus]", "(" & separateData(0) & ") " & separateData(2) & " a été ajouté à votre liste d'ami.", Color.Green)

                        Case "iAK"

                            EcritureMessage("[Dofus]", "(" & separateData(0) & ") " & separateData(2) & " a été ajouté à votre liste d'ennemi", Color.Green)

                    End Select

                End With

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Ajoute", data & vbCrLf & ex.Message)

            End Try

        End Sub

        Sub Supprimer(data As String)

            Try

                With Bot

                    'FDK 
                    'iDK

                    Select Case data

                        Case "FDK"

                            EcritureMessage("[Dofus]", "Tu viens de perdre un ami.", Color.Green)

                        Case "iDK"

                            EcritureMessage("[Dofus]", "L'ennemi a été effacé, la paix gagne une bataille.", Color.Green)

                    End Select

                End With

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Supprimer", data & vbCrLf & ex.Message)

            End Try

        End Sub

        Sub Ajoute_Supprime_Echec(data As String)

            Try

                With Bot

                    ' FAEf = Ami
                    ' iAEf = Ennemi
                    ' FAEa
                    ' iAEa

                    Select Case data

                        Case "FAEf", "iAEf"

                            EcritureMessage("[Dofus]", "Impossible, ce perso ou compte n'existe pas ou n'est pas connecté.", Color.Red)

                        Case "FAEa"

                            EcritureMessage("[Dofus]", "Déjà dans ta liste d'amis.", Color.Red)

                        Case "iAEa"

                            EcritureMessage("[Dofus]", "Déjà dans ta liste d'ennemis.", Color.Red)

                    End Select

                End With

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Ajoute_Supprime_Echec", data & vbCrLf & ex.Message)

            End Try

        End Sub

        Sub Information(data As String)

            Try

                With Bot

                    ' BWK Linaculer | 1 | Lenculé | 7
                    ' BWK Pseudo    | ? | Nom     | Zone

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    Dim phrase As String = separateData(2) & " (" & separateData(1) & ") se trouve en "

                    Select Case separateData(3)

                        Case "-1"

                            phrase &= "zone inconnue."

                        Case "7"

                            phrase &= "bonta."

                        Case "11"

                            phrase &= "Brakmar"

                        Case "18"

                            phrase &= "Astrub"

                    End Select

                    EcritureMessage("[Dofus]", phrase, Color.Green)

                End With

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Information", data & vbCrLf & ex.Message)

            End Try

        End Sub

        Sub Information_Echec(data As String)

            Try

                With Bot

                    ' BWE Linaculer 
                    ' BWE Pseudo    

                    EcritureMessage("[Dofus]", Mid(data, 4) & " n'est pas connecté ou n'existe pas.", Color.Green)

                End With

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Information_Echec", data & vbCrLf & ex.Message)

            End Try

        End Sub

        Sub Averti(data As String)

            Try

                With Bot

                    ' FO - ou +

                    Select Case data

                        Case "FO-"

                            .Ami.Averti = False

                            EcritureMessage("[Dofus]", "Vous serez pas avertis lors de la connexion d'un ami.", Color.Green)

                        Case "FO+"

                            .Ami.Averti = True

                            EcritureMessage("[Dofus]", "Vous serez avertis lors de la connexion d'un ami.", Color.Green)

                    End Select

                End With

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage, "Ami_Averti", data & vbCrLf & ex.Message)

            End Try

        End Sub

    End Module

End Namespace