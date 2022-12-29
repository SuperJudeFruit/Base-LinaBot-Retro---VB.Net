Namespace Connexion_Function

    Module Connexion_Function

        Public Function Connexion(nomDeCompte As String, motDePasse As String, serveur As String, nomDuPersonnage As String) As Boolean

            With Bot

                Try

                    If .Connexion.Connecter = False AndAlso .Connexion.Connexion = False AndAlso .Connexion.Authentification = False Then

                        With .Personnage

                            .NomDeCompte = nomDeCompte
                            .MotDePasse = motDePasse
                            .Serveur = serveur
                            .NomDuPersonnage = nomDuPersonnage

                        End With

                        .Mitm.CreateSocketAuthentification(.Socket_Authentification, VarServeur("Authentification").IP, VarServeur("Authentification").Port, .Proxy)

                        Return True

                    End If

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

        Public Function Deconnexion() As Boolean

            With Bot.Connexion

                Try

                    If .Connecter Then

                        Bot.Socket.Connexion_Game(False)

                        Task.Delay(5000).Wait()

                        Return .Connecter

                    End If

                    If .Connexion Then

                        Bot.Socket.Connexion_Game(False)

                        Task.Delay(5000).Wait()

                        Return .Connexion

                    End If

                    If .Authentification Then

                        Bot.Socket_Authentification.Connexion_Game(False)

                        Task.Delay(5000).Wait()

                        Return .Authentification

                    End If

                Catch ex As Exception

                End Try

                Return False

            End With

        End Function

        Public Function Cadeau(ID_Nom As String, Nom_ID_Personnage As String) As Boolean

            Try

                With Bot

                    'AG  1                 | 123456789
                    'AG  2                 | 123456789
                    ' AG Numéro de l'objet | Id Personnage
                    'recv : BN AGK AAK 
                    For Each pair As KeyValuePair(Of String, Item_Variable.Information) In .Personnage.Cadeau

                        If pair.Value.Nom.ToLower = ID_Nom.ToLower OrElse pair.Value.IdObjet = ID_Nom Then

                            Return .Mitm.Send("AG" & pair.Key & "|" & .Personnage.ID, {"AGK", "AAK"})

                        End If

                    Next

                End With

            Catch ex As Exception
            End Try

            Return False

        End Function

    End Module

End Namespace