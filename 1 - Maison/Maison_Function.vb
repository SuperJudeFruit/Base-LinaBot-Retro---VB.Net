Namespace Maison_Function

    Module Maison_Function

        Public Function Fermer() As Boolean

            With Bot

                Try

                    If .Maison.Ouverture Then

                        Return .Mitm.Send("EV",
                                         {"EV"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Function_Fermer", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Acheter(Prix As Integer) As Boolean

            With Bot

                Try

                    For Each pair As KeyValuePair(Of String, Maison_Variable.Maison) In .Maison.Map

                        If pair.Value.Prix <= Prix AndAlso .Personnage.Kamas >= pair.Value.Prix Then

                            Return .Mitm.Send("hB" & pair.Value.Prix,
                                             {"hP" & pair.Key & "|" & .Personnage.Pseudo,
                                              "hL+" & pair.Key,
                                              "hBK" & pair.Key})

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Function_Acheter", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Vendre(Prix As Integer) As Boolean

            With Bot

                Try


                    Return .Mitm.Send("hS" & Prix,
                                     {"hSK"})


                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Function_Vendre", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Code_Change(Code As String) As Boolean

            With Bot

                Try

                    If Code = "" Then

                        Code = "-"

                    End If

                    Return .Mitm.Send("KK1|" & Code,
                                     {"KKK"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Function_Code_Change", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Parametre_Guilde() As Boolean

            With Bot

                Try

                    Return .Mitm.Send("hG",
                                     {"hG" & .Maison.Personnelle.ID})


                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Function_Parametre_Guilde", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Parametre_Gestion(Active As Boolean) As Boolean

            With Bot

                Try

                    Return .Mitm.Send("hG" & If(Active, "+", "-"),
                                     {"hG" & .Maison.Personnelle.ID & ";" & .Guilde.Nom})


                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Function_Parametre_Gestion", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Parametre_Droits(Droits As String) As Boolean

            With Bot

                Try

                    Dim resultat As Integer = 0

                    Dim separate As String() = Split(Droits, "|")

                    For i = 0 To separate.Length - 1

                        Dim separateDroits As String() = Split(separate(i), " = ")

                        Select Case separateDroits(0).ToLower

                            Case "pour les membres de la guilde"

                                resultat += 2

                            Case "pour les autres"

                                resultat += 4

                            Case "autoriser l'acces aux membres de la guilde sans code (maison)"

                                resultat += 8

                            Case "interdire l'acces aux non-membres de la guilde (maison)"

                                resultat += 16

                            Case "autoriser l'acces aux membres de la guilde sans code (coffre)"

                                resultat += 32

                            Case "interdire l'acces aux non-membres de la guilde (coffre)"

                                resultat += 64

                            Case "autoriser les membres de la guilde a se teleporter dans la maison"

                                resultat += 128

                            Case "autoriser les membres de la guilde a se reposer dans la maison"

                                resultat += 256

                        End Select

                    Next

                    Return .Mitm.Send("hG" & resultat,
                                     {"BN"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Function_Parametre_Droits", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Code_Porte(Code As String) As Boolean

            With Bot

                Try

                    If .Maison.Ouverture Then

                        Return .Mitm.Send("KK0|" & Code,
                                         {"GDM"},
                                         {"KKE"})

                    End If


                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Function_Code_Porte", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Code_Coffre(Code As String) As Boolean

            With Bot

                Try

                    If .Maison.Ouverture Then

                        Return .Mitm.Send("KK0|" & Code,
                                         {"ECK5"},
                                         {"KKE",
                                          "Im120"})

                    End If


                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Maison_Function_Code_Coffre", ex.Message)

                End Try

                Return False

            End With

        End Function

    End Module

End Namespace


