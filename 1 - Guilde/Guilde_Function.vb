Namespace Guilde_Function

    Module Guilde_Function

#Region "Ouverture"

        Public Function Ouvre() As Boolean

            With Bot

                Try

                    Return .Mitm.Send("gIG",
                                     {"gIG",
                                      "gS"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Ouvre", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Membres() As Boolean

            With Bot

                Try

                    Return .Mitm.Send("gIM",
                                     {"gIM"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Membres", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Personnalisation() As Boolean

            With Bot

                Try

                    Return .Mitm.Send("gIB",
                                     {"gIB"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Personnalisation", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Percepteurs() As Boolean

            With Bot

                Try

                    Return .Mitm.Send("gIT",
                                     {"gIT"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Percepteurs", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Enclos() As Boolean

            With Bot

                Try

                    .Mitm.Send("gITV")

                    Return .Mitm.Send("gIF",
                                     {"gIF"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Enclos", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Maisons() As Boolean

            With Bot

                Try

                    Return .Mitm.Send("gIH",
                                     {"gIH"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Maisons", ex.Message)

                End Try

                Return False

            End With

        End Function

#End Region

#Region "Membres"

        Public Function Exclure(nom As String) As Boolean

            With Bot

                Try

                    Return .Mitm.Send("gK" & nom,
                                     {"gKK"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Exclure", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Invite(nom As String) As Boolean

            With Bot

                Try

                    .Guilde.Inviter = nom

                    Return .Mitm.Send("gJR" & nom,
                                     {"gJR"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Invite", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Refuse() As Boolean

            With Bot

                Try

                    Return .Mitm.Send("gJE" & .Guilde.Inviteur,
                                     {"gJE"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Refuse", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Rang(membre As String, _Rang As String) As Boolean

            With Bot

                Try

                    Dim rangActuel() As String = {"a l'essai", "meneur", "bras droit", "tresorier", "protecteur", "artisan", "reserviste",
                "gardien", "eclaireur", "espion", "diplomate", "secretaire", "tueur de familiers", "braconnier", "chercheur de tresor",
                "voleur", "initie", "assassin", "gouverneur", "muse", "conseiller", "elu", "guide", "mentor", "recruteur",
                "eleveur", "marchand", "apprenti", "bourreau", "mascotte", "penitent", "tueur de percepteurs", "deserteur", "traitre",
                "boulet", "larbin", "a l'essai"}

                    For i = 0 To rangActuel.Length - 1

                        If rangActuel(i) = _Rang.ToLower Then

                            For Each pair As Guilde_Variable.Membre In .Guilde.Membre.Values

                                If pair.Nom.ToLower = membre.ToLower Then

                                    .Mitm.Send("gP" & pair.ID & "|" & i & "|" & pair.Experience.Pourcentage & "|" & pair.Droit_Chiffre)

                                    Return True

                                End If

                            Next

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Rang", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Experience(membre As String, valeur As String) As Boolean

            With Bot

                Try

                    For Each pair As Guilde_Variable.Membre In .Guilde.Membre.Values

                        If pair.Nom.ToLower = membre.ToLower Then

                            .Mitm.Send("gP" & pair.ID & "|" & pair.Rang_Chiffre & "|" & valeur & "|" & pair.Droit_Chiffre)

                            Return True

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Experience", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Droits(membre As String, _Droits As String, Active As Boolean) As Boolean

            With Bot

                Try

                    For Each pair As Guilde_Variable.Membre In .Guilde.Membre.Values

                        If pair.Nom.ToLower = membre.ToLower Then

                            .Mitm.Send("gP" & pair.ID & "|" & pair.Rang_Chiffre & "|" & pair.Experience.Pourcentage & "|" & ReturnDroit(pair.Droit, _Droits, Active))

                            Return True

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Droits", ex.Message)

                End Try

                Return False

            End With

        End Function

        Private Function ReturnDroit(membre As Guilde_Variable.Droit, Droits As String, Active As Boolean) As Integer

            Dim valeur As Integer = 0

            Try

                With membre

                    valeur += If(.GererLesBoosts = True, 16384, 0)
                    valeur += If(.GererLesDroits = True, 8192, 0)
                    valeur += If(.InviterDeNouveauxMembres = True, 4096, 0)
                    valeur += If(.Bannir = True, 512, 0)
                    valeur += If(.GererLesRepartitionsXP = True, 256, 0)
                    valeur += If(.GererSaRepartitionXP = True, 128, 0)
                    valeur += If(.GererLesRangs = True, 64, 0)
                    valeur += If(.PoserUnPercepteur = True, 32, 0)
                    valeur += If(.CollecterSurUnPercepteur = True, 16, 0)
                    valeur += If(.UtiliserLesEnclos = True, 8, 0)
                    valeur += If(.AmenagerLesEnclos = True, 4, 0)
                    valeur += If(.GererLesMonturesDesAutresMembres = True, 2, 0)

                    Dim separate As String() = Split(Droits, " | ")

                    For i = 0 To separate.Length - 1

                        Select Case separate(i).ToLower

                            Case "gerer les boosts"

                                valeur += If(Active = True, If(.GererLesBoosts = True, 0, 16384), If(.GererLesBoosts = True, -16384, 0))

                            Case "gerer les droits"

                                valeur += If(Active = True, If(.GererLesDroits = True, 0, 8192), If(.GererLesDroits = True, -8192, 0))

                            Case "inviter de nouveaux membres"

                                valeur += If(Active = True, If(.InviterDeNouveauxMembres = True, 0, 4096), If(.InviterDeNouveauxMembres = True, -4096, 0))

                            Case "bannir"

                                valeur += If(Active = True, If(.Bannir = True, 0, 512), If(.Bannir = True, -512, 0))

                            Case "gerer les repartitions d'xp"

                                valeur += If(Active = True, If(.GererLesRepartitionsXP = True, 0, 256), If(.GererLesRepartitionsXP = True, -256, 0))

                            Case "gerer sa repartition d'xp"

                                valeur += If(Active = True, If(.GererSaRepartitionXP = True, 0, 128), If(.GererSaRepartitionXP = True, -128, 0))

                            Case "gerer les rangs"

                                valeur += If(Active = True, If(.GererLesRangs = True, 0, 64), If(.GererLesRangs = True, -64, 0))

                            Case "poser un percepteur"

                                valeur += If(Active = True, If(.PoserUnPercepteur = True, 0, 32), If(.PoserUnPercepteur = True, -32, 0))

                            Case "collecter sur un percepteur"

                                valeur += If(Active = True, If(.CollecterSurUnPercepteur = True, 0, 16), If(.CollecterSurUnPercepteur = True, -16, 0))

                            Case "utiliser les enclos"

                                valeur += If(Active = True, If(.UtiliserLesEnclos = True, 0, 8), If(.UtiliserLesEnclos = True, -8, 0))

                            Case "amenager les enclos"

                                valeur += If(Active = True, If(.AmenagerLesEnclos = True, 0, 4), If(.AmenagerLesEnclos = True, -4, 0))

                            Case "gerer les montures des autres membres"

                                valeur += If(Active = True, If(.GererLesMonturesDesAutresMembres = True, 0, 2), If(.GererLesMonturesDesAutresMembres = True, -2, 0))

                        End Select

                    Next

                End With

            Catch ex As Exception

            End Try

            Return valeur

        End Function

#End Region

#Region "Personnalisation"

        Public Function Up(choix As String) As Boolean

            With Bot

                Try

                    Select Case choix.ToLower

                        Case "prospection"

                            If .Guilde.Percepteur.Prospection < 500 Then

                                If .Guilde.Percepteur.ResteARepartir >= 1 Then

                                    Return .Mitm.Send("gBp",
                                                     {"gIB"})

                                End If

                            End If

                        Case "sagesse"

                            If .Guilde.Percepteur.Sagesse < 400 Then

                                If .Guilde.Percepteur.ResteARepartir >= 1 Then

                                    Return .Mitm.Send("gBx",
                                                     {"gIB"})

                                End If

                            End If

                        Case "pods"

                            If .Guilde.Percepteur.Pods < 5000 Then

                                If .Guilde.Percepteur.ResteARepartir >= 1 Then

                                    Return .Mitm.Send("gBo",
                                                     {"gIB"})

                                End If

                            End If

                        Case "nombre de percepteur"

                            If .Guilde.Percepteur.NombreDePercepteur < 50 Then

                                If .Guilde.Percepteur.ResteARepartir >= 10 Then

                                    Return .Mitm.Send("gBk",
                                                     {"gIB"})

                                End If

                            End If

                        Case "armure aqueuse"

                            If .Guilde.Percepteur.ArmureAqueuse < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB451",
                                                     {"gIB"})

                                End If

                            End If

                        Case "armure incandescente"

                            If .Guilde.Percepteur.ArmureIncandescente < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB452",
                                                     {"gIB"})

                                End If

                            End If

                        Case "armure terrestre"

                            If .Guilde.Percepteur.ArmureTerrestre < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB453",
                                                     {"gIB"})

                                End If

                            End If

                        Case "armure venteuse"

                            If .Guilde.Percepteur.ArmureVenteuse < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB454",
                                                     {"gIB"})

                                End If

                            End If

                        Case "flamme"

                            If .Guilde.Percepteur.Flamme < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB455",
                                                     {"gIB"})

                                End If

                            End If

                        Case "cyclone"

                            If .Guilde.Percepteur.Cyclone < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB456",
                                                     {"gIB"})

                                End If

                            End If

                        Case "vague"

                            If .Guilde.Percepteur.Vague < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB457",
                                                     {"gIB"})

                                End If

                            End If

                        Case "rocher"

                            If .Guilde.Percepteur.Rocher < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB458",
                                                     {"gIB"})

                                End If

                            End If

                        Case "mot soignant"

                            If .Guilde.Percepteur.MotSoignant < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB459",
                                                     {"gIB"})

                                End If

                            End If

                        Case "desenvoutement"

                            If .Guilde.Percepteur.Desenvoutement < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB460",
                                                     {"gIB"})

                                End If

                            End If

                        Case "compulsion de masse"

                            If .Guilde.Percepteur.CompulsionDeMasse < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB461",
                                                     {"gIB"})

                                End If

                            End If

                        Case "destabilisation"

                            If .Guilde.Percepteur.Destabilisation < 5 Then

                                If .Guilde.Percepteur.ResteARepartir >= 5 Then

                                    Return .Mitm.Send("gB462",
                                                     {"gIB"})

                                End If

                            End If

                    End Select

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Up", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Poser() As Boolean

            With Bot

                Try

                    If .Personnage.Kamas >= .Guilde.Percepteur.CoutPourPoserPercepteur Then

                        Return .Mitm.Send("gH",
                                         {"gTS"})

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Poser", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Retirer() As Boolean

            With Bot

                Try

                    For Each pair As Map_Variable.Entite In .Map.Entite.Values

                        If pair.Classe = "-6" Then

                            Return .Mitm.Send("gF" & pair.IDUnique,
                                             {"gTR"})

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Retirer", ex.Message)

                End Try

                Return False

            End With

        End Function

        Public Function Relever() As Boolean

            With Bot

                Try

                    For Each pair As Map_Variable.Entite In .Map.Entite.Values

                        If pair.Classe = "-6" Then

                            Return .Mitm.Send("ER8|-88" & pair.IDUnique,
                                             {"gTR"})

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Relever", ex.Message)

                End Try

                Return False

            End With

        End Function

#End Region

#Region "Percepteurs"

#End Region

#Region "Enclos"

        Public Function Enclos_Teleporter(enclos As String) As Boolean

            With Bot

                Try

                    Return .Mitm.Send("gf" & enclos,
                                     {"GDM"})

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Enclos_Teleporter", ex.Message)

                End Try

                Return False

            End With

        End Function

#End Region

#Region "Maisons"

        Public Function Maisons_Teleporter(maisons As String) As Boolean

            With Bot

                Try

                    For Each pair As Guilde_Variable.Maison In .Guilde.Maison.Values

                        If pair.Prorietaire.ToLower = maisons.ToLower Then

                            Return .Mitm.Send("gh" & pair.ID,
                                             {"GDM"})

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Function_Maisons_Teleporter", ex.Message)

                End Try

                Return False

            End With

        End Function

#End Region


    End Module

End Namespace
