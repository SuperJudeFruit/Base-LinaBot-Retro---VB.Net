Namespace Tchat_Function

    Public Class Tchat_Function

        Public Function Canal(_canal As String, choix As Boolean) As Boolean

            Try

                With Bot

                    Dim envoyer As String = "cC" & If(choix, "+", "-")

                    Select Case _canal.ToLower

                        Case "information"

                            Return .Mitm.Send(envoyer & "i",
                                             {envoyer & "i"}) ' GoodData

                        Case "communs", "commun"

                            Return .Mitm.Send(envoyer & "*",
                                             {envoyer & "*"}) ' GoodData

                        Case "groupe", "equipe", "message privee"

                            Return .Mitm.Send(envoyer & "#$p",
                                             {envoyer & "#$p"}) ' GoodData

                        Case "guilde"

                            Return .Mitm.Send(envoyer & "%",
                                             {envoyer & "%"}) ' GoodData

                        Case "alignement"

                            Return .Mitm.Send(envoyer & "!",
                                             {envoyer & "!"}) ' GoodData

                        Case "recrutement"

                            Return .Mitm.Send(envoyer & "?",
                                             {envoyer & "?"}) ' GoodData

                        Case "commerce"

                            Return .Mitm.Send(envoyer & ":",
                                             {envoyer & ":"}) ' GoodData

                        Case "evenement"

                            Return .Mitm.Send(envoyer & ":",
                                             {envoyer & ":"}) ' GoodData
                        Case Else

                            Return False

                    End Select

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

        Public Function Message(canal As String, _message As String) As Boolean

            Try

                With Bot

                    Dim joueur As String = Split(_message, " ")(0)

                    If _message.ToLower.StartsWith("/w") Then

                        _message = _message.Replace(joueur & " ", "")

                    End If

                    Select Case canal.ToLower

                        Case "/c", "commun" ' Communs

                            Return .Mitm.Send("BM*|" & _message & "|",
                                             {"cMK|" & .Personnage.ID},
                                             {"BN"})

                        Case "/w", "/mp" ' Message Privée

                            Return .Mitm.Send("BM" & joueur & "|" & _message.Replace(joueur & " ", "") & "|",
                                             {"cMKT" & .Personnage.ID},
                                             {"BN",
                                              "cMEf"})

                        Case "/p", "groupe" ' Groupe

                            Return .Mitm.Send("BM$|" & _message & "|",
                                             {"cMK$" & .Personnage.ID},
                                             {"BN"})

                        Case "/g", "guilde" ' Guilde

                            Return .Mitm.Send("BM%|" & _message & "|",
                                             {"cMK%" & .Personnage.ID},
                                             {"BN"})

                        Case "/r", "recrutement" ' Recrutement

                            Return .Mitm.Send("BM?|" & _message & "|",
                                             {"cMK?" & .Personnage.ID},
                                             {"BN"})

                        Case "/b", "commerce" ' Commerce

                            Return .Mitm.Send("BM:|" & _message & "|",
                                             {"cMK:" & .Personnage.ID},
                                             {"BN"})

                        Case "/a", "alignement" ' Alignement

                            Return .Mitm.Send("BM!|" & _message & "|",
                                             {"cMK!" & .Personnage.ID},
                                             {"BN",
                                              "cMEA",
                                              "Im0106",
                                              "Im0115;"})

                        Case Else

                            Return False

                    End Select

                End With

            Catch ex As Exception

            End Try

            Return False

        End Function

    End Class

End Namespace