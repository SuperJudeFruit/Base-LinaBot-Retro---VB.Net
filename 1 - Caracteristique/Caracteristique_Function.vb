Namespace Caracteristique_Function

    Class Caracteristique_Function

        Public Function Up(Valeur As String) As Boolean

            Try

                With Bot

                    Select Case Valeur.ToLower

                        Case "vitaliter", "vitalite", "vitalité"

                            Return .Mitm.Send("AB11",
                                        {"As"}, ' Caractéristique reçu
                                        {"ABE"}) ' Impossible de Up la caractéristique.

                        Case "sagesse"

                            Return .Mitm.Send("AB12",
                                        {"As"}, ' Caractéristique reçu
                                        {"ABE"}) ' Impossible de Up la caractéristique.

                        Case "force"

                            Return .Mitm.Send("AB10",
                                        {"As"}, ' Caractéristique reçu
                                        {"ABE"}) ' Impossible de Up la caractéristique.

                        Case "chance"

                            Return .Mitm.Send("AB13",
                                        {"As"}, ' Caractéristique reçu
                                        {"ABE"}) ' Impossible de Up la caractéristique.

                        Case "intelligence"

                            Return .Mitm.Send("AB15",
                                        {"As"}, ' Caractéristique reçu
                                        {"ABE"}) ' Impossible de Up la caractéristique.

                        Case "agilité", "agiliter", "agilite"

                            Return .Mitm.Send("AB14",
                                        {"As"}, ' Caractéristique reçu
                                        {"ABE"}) ' Impossible de Up la caractéristique.

                    End Select

                End With

            Catch ex As Exception

                ErreurFichier(Bot.Personnage.NomDuPersonnage & "_" & Bot.Personnage.Serveur, "Caracteristique_Function_Up", ex.Message)

            End Try

            Return False

        End Function

    End Class

End Namespace