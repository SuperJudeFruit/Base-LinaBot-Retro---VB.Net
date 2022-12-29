Namespace Connexion

    Module Connexion

#Region "Connexion au serveur."

        Sub Serveur_Authentification(data As String)

            With Bot

                Try

                    'HC trkzqijwpzvunfezdxdhhlmmgxxgsbqm 
                    'HC Clef Crypt Mdp 

                    With .Connexion

                        .Connexion = False
                        .Connecter = False
                        .Authentification = True

                    End With

                    EcritureMessage("(Bot)", "Connecté au serveur d'authentification.", Color.Lime)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Serveur_Authentification", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Serveur_Jeu(data As String)

            With Bot

                Try

                    'HG 

                    With .Connexion

                        .Authentification = False
                        .Connecter = False
                        .Connexion = True

                    End With

                    .Mitm.Send("AT" & .Personnage.Ticket)

                    EcritureMessage("(Bot)", "Connecté au serveur de jeu, envoie du ticket.", Color.Lime)

                    Reset_Config_Xml()

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Serveur_Jeu", ex.Message)

                End Try

            End With

        End Sub

#End Region

#Region "File d'attente"

        Sub FileAttente_Authentification(data As String)

            With Bot

                Try

                    ' Af 82              | 272            | 0 |   | -1 
                    ' Af Position actuel | sur X personne | ? | ? | ?

                    Dim separate As String() = Split(Mid(data, 3), "|")

                    EcritureMessage("[Dofus]", "En attente de connexion sur le serveur..." & vbCrLf &
                                               "Position dans la file d'attente : " & separate(0), Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_FileAttente_Authentification", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub FileAttente_Jeu(data As String)

            With Bot

                Try

                    'Aq 1
                    'Aq Position dans la queu.

                    EcritureMessage("[Dofus]", "Connexion au serveur... ( Position dans la file d'attente : " & Mid(data, 3) & " )", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_FileAttente_Jeu", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

#End Region

        Sub Pseudo(data As String)

            With Bot

                Try


                    ' Ad Linaculer
                    ' Ad Pseudo du compte

                    .Personnage.Pseudo = Mid(data, 3)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Pseudo", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Reception_Serveur(data As String)

            With Bot

                Try

                    'AH 601        ; 1            ; 75      ; 1       | 602;1;75;1
                    'AH ID_Serveur ; Etat_Serveur ; Inconnu ; Inconnu | Next

                    Dim separateData As String() = Split(Mid(data, 3), "|")

                    For i = 0 To separateData.Length - 1

                        Dim separate As String() = Split(separateData(i), ";")

                        If separate(0) = VarServeur(.Personnage.Serveur).ID Then

                            Select Case separate(1)

                                Case "0"

                                    EcritureMessage("[Dofus]", "Serveur en maintenance ! Déconnexion.", Color.Red)

                                    .Socket_Authentification.Connexion_Game(False)

                                    .Mitm.Client.Close()

                                Case "1"

                                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Reception_Serveur", "Information Inconnu : 1" & vbCrLf & data)

                                Case "2" ' En sauvegarde

                                    EcritureMessage("[Dofus]", "Serveur en sauvegarde !", Color.Red)

                                Case Else

                                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Reception_Serveur", data)

                            End Select

                        End If

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Reception_Serveur", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub QuestionSecrete(data As String)

            With Bot

                Try

                    ' AQ Quel+est+mon+mod%C3%A8le+de+voiture+pr%C3%A9f%C3%A9r%C3%A9+%3F
                    ' AQ Question secréte

                    If data.Length > 2 Then

                        'Je prend tout se qui se trouve après le "AQ"
                        Dim Question As String = Mid(data, 3)

                        'Je remplace les "+" par un espace.
                        Question = Question.Replace("+", " ")

                        .Personnage.QuestionSecrete = AsciiDecoder(Question)

                        EcritureMessage("[Dofus]", "Question secréte : " & .Personnage.QuestionSecrete, Color.Green)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_QuestionSecrete", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Selection_Serveur(data As String)

            With Bot

                Try

                    ' AxK 758257142                  | 601        , 5                    |
                    ' AxK Abonnement en milliseconde | Id Serveur , Nombre de personnage | Next

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    .Personnage.Abonnement = DateAdd("s", separateData(0) \ 1000, Date.Now)


                    If separateData.Length > 1 Then

                        For i = 1 To separateData.Length - 1

                            Dim separateServeur As String() = Split(separateData(i), ",")

                            If VarServeur(.Personnage.Serveur).ID = separateServeur(0) Then

                                EcritureMessage("(Bot)", "Connexion au serveur : " & VarServeur(.Personnage.Serveur).Nom, Color.Lime)

                                .Mitm.Send("AX" & VarServeur(.Personnage.Serveur).ID)

                                Exit Sub

                            End If

                        Next

                        EcritureMessage("(Bot)", "Le serveur demander est introuvable, vérifier d'avoir bien créer un personnage en jeu avant de lancer le bot.", Color.Red)

                    Else

                        EcritureMessage("(Bot)", "Aucun serveur détecté, déconnexion du bot.", Color.Red)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Selection_Serveur", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Reception_Personnage(data As String)

            With Bot

                Try

                    'ALK 25487456210      | 4              | 1234567       ; Linaculer      ; 101    ; 90     ; -1       ; -1       ; -1       ; 48a , 1bea   , 1b0f , 1f40     , Bouclier ; 0 ; 601        ;   ;   ;   | Next personnage
                    'ALK Abonnement_Dofus | Nbr_Personnage | ID_Personnage ; Nom_Personnage ; Niveau ; Classe ; Couleur1 ; Couleur2 ; Couleur3 ; Cac , Coiffe , Cape , Familier , Bouclier ; ? ; ID_Serveur ; ? ; ? ; ? | Next

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    .Personnage.Abonnement = Date.Now.AddMilliseconds(separateData(0))

                    If separateData(1) <> "0" Then

                        EcritureMessage("[Dofus]", "Réception des personnages. (" & separateData(1) & ")", Color.Green)

                        For i = 2 To separateData.Length - 1

                            Dim separate() As String = Split(separateData(i), ";")

                            If separate(1).ToLower = .Personnage.NomDuPersonnage.ToLower Then

                                With .Personnage

                                    .ID = separate(0)

                                    .NomDuPersonnage = separate(1)

                                    .Niveau = separate(2)

                                    .ClasseSexe = separate(3)

                                    'Pour obtenir la couleur sur une var 'Color' = ColorTranslator.FromOle(.Couleur1)
                                    .Couleur1 = "&H" & separate(4)
                                    .Couleur2 = "&H" & separate(5)
                                    .Couleur3 = "&H" & separate(6)

                                    Dim separateItem As String() = Split(separate(7), ",")

                                    If separate(7) <> "null" Then

                                        With .Equipement

                                            If separateItem(0) <> Nothing Then

                                                .Cac = Convert.ToInt64(separateItem(0), 16)

                                            End If

                                            If separateItem(1) <> Nothing Then

                                                Dim separateObvijevan As String() = Split(separateItem(1), "~")

                                                .Chapeau.ID = Convert.ToInt64(separateObvijevan(0), 16)

                                                If separateItem(1).Contains("~"c) Then

                                                    .Chapeau.Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                                    .Chapeau.Forme = Convert.ToInt64(separateObvijevan(2), 16)

                                                End If

                                            Else

                                                .Chapeau = New Map_Variable.Information

                                            End If

                                            If separateItem(2) <> Nothing Then

                                                Dim separateObvijevan As String() = Split(separateItem(2), "~")

                                                .Cape.ID = Convert.ToInt64(separateObvijevan(0), 16)

                                                If separateItem(2).Contains("~"c) Then

                                                    .Cape.Niveau = Convert.ToInt64(separateObvijevan(1), 16)
                                                    .Cape.Forme = Convert.ToInt64(separateObvijevan(2), 16)

                                                End If

                                            Else

                                                .Cape = New Map_Variable.Information

                                            End If

                                            If separateItem(3) <> Nothing Then

                                                .Familier = Convert.ToInt64(separateItem(3), 16)

                                            End If

                                            If separateItem(4) <> Nothing Then

                                                .Bouclier = Convert.ToInt64(separateItem(4), 16)

                                            End If

                                        End With

                                    End If

                                    .IDServeur = separate(9)

                                End With

                                EcritureMessage("(Bot)", "Connexion au personnage : " & .Personnage.NomDuPersonnage, Color.Lime)

                                .Mitm.Send("AS" & .Personnage.ID)
                                .Mitm.Send("Af")

                                Exit For

                            End If

                        Next

                    Else

                        If .Personnage.NomDuPersonnage.ToLower = "aleatoire" Then

                            .Mitm.Send("AP")

                        Else

                            .Mitm.Send("AA" & .Personnage.NomDuPersonnage & "|" & .Personnage.Classe & "|" & .Personnage.Sexe & "|" & .Personnage.Couleur1 & "|" & .Personnage.Couleur2 & "|" & .Personnage.Couleur3)

                        End If

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Reception_Personnage", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

#Region "Ip port"

        Sub Serveur_Ip_Port_Ticket(data As String)

            With Bot

                Try


                    ' AXK 98752:98 gr5   32tr9f
                    ' AXK IP       Port  Ticket 

                    ' AYK eratz.ankama-games.com ; 0123f45
                    ' AYK Ip et Port             ; Ticket

                    EcritureMessage("(Bot)", "Récuperation de l'IP, Port et du Ticket.", Color.Lime)

                    Dim Ip As String = ""
                    Dim Port As String = ""

                    Select Case Mid(data, 1, 3)

                        Case "AXK"

                            .Personnage.Ticket = Mid(data, 15)

                            Ip = DecryptIP(Mid(data, 4, 8))
                            Port = DecryptPort(Mid(data, 12, 3))

                        Case "AYK"

                            Dim separateData As String() = Split(Mid(data, 4), ";")

                            .Personnage.Ticket = separateData(1)

                            If data.Contains(".com") Then

                                Ip = HostNameIP(Split(separateData(0), ":")(0))
                                Port = Split(separateData(0), ":")(1)

                            End If

                    End Select

                    If Ip <> VarServeur(.Personnage.Serveur).IP OrElse Port <> VarServeur(.Personnage.Serveur).Port Then

                        ReplaceIpPort(.Personnage.Serveur, Ip, Port)

                    End If

                    .Socket = New All_CallBack(Ip, 443)

                    AddHandler .Socket.Deconnexion, AddressOf .Mitm.e_Deconnexion
                    AddHandler .Socket.Envoie, AddressOf .Mitm.e_Envoi
                    AddHandler .Socket.Reception, AddressOf .Mitm.E_Reception
                    ' .CreateSocketServeurJeu(.Socket, Ip, 443, .Proxy)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Serveur_Ip_Port_Ticket", ex.Message)

                End Try

            End With

        End Sub

        Private Function HostNameIP(hostname As String) As String

            With Bot

                Try

                    Dim hostname2 As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(hostname)
                    Dim ip As System.Net.IPAddress() = hostname2.AddressList
                    Return ip(0).ToString()

                Catch ex As Exception

                    ErreurFichier("All", "Connexion_HostNameIP", ex.Message)

                End Try

            End With

            Return 0

        End Function

        Private Sub ReplaceIpPort(Serveur As String, Ip As String, Port As String)

            With Bot

                Try

#Region "Lecture"

                    Dim swLecture As New IO.StreamReader(Application.StartupPath + "\Data/Serveur.txt")

                    Dim ligneFinal As String = ""

                    Do Until swLecture.EndOfStream

                        Dim Ligne As String = swLecture.ReadLine

                        If Ligne <> "" Then

                            Dim separate As String() = Split(Ligne, "|")

                            If separate(0) = Serveur Then

                                ligneFinal &= Serveur & "|" & Ip & "|" & Port & "|" & VarServeur(Serveur).ID & vbCrLf

                            Else

                                ligneFinal &= Ligne & vbCrLf

                            End If

                        End If

                    Loop

                    swLecture.Close()

#End Region

#Region "Ecriture"

                    Dim swEcriture As New IO.StreamWriter(Application.StartupPath + "\Data/Serveur.txt")

                    swEcriture.Write(ligneFinal)

                    swEcriture.Close()

#End Region

                    ChargeServeur()

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_ReplaceIpPort", ex.Message)

                End Try

            End With

        End Sub

#Region "Cryptage/Décryptage"

        'Maxoubot
        Private Function DecryptIP(IP_Crypt As String) As String

            With Bot

                Dim ipServeurJeu As String = ""

                Try

                    Dim i As Long = 0
                    Dim fois As Long = 0

                    While (i < 8)

                        i += 1
                        fois += 1

                        Dim dat1 As Integer = Asc(Mid(IP_Crypt, i, 1)) - 48

                        i += 1

                        Dim dat2 As Integer = Asc(Mid(IP_Crypt, i, 1)) - 48
                        Dim Dat3 As String = Str((dat1 And 15) << 4 Or dat2 And 15)

                        If fois > 1 Then

                            ipServeurJeu += Mid(Dat3, 2)

                        Else

                            ipServeurJeu += Dat3

                        End If

                        If i < 8 Then

                            ipServeurJeu += "."

                        End If

                    End While

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_DecryptIP", ex.Message)

                End Try

                Return ipServeurJeu.Replace(" ", "")

            End With

        End Function

        'Salesprendes
        Dim caracteres_array As Char() = New Char() {"a"c, "b"c, "c"c, "d"c, "e"c, "f"c, "g"c, "h"c, "i"c, "j"c, "k"c, "l"c, "m"c, "n"c, "o"c, "p"c, "q"c, "r"c, "s"c, "t"c, "u"c, "v"c, "w"c, "x"c, "y"c, "z"c, "A"c, "B"c, "C"c, "D"c, "E"c, "F"c, "G"c, "H"c, "I"c, "J"c, "K"c, "L"c, "M"c, "N"c, "O"c, "P"c, "Q"c, "R"c, "S"c, "T"c, "U"c, "V"c, "W"c, "X"c, "Y"c, "Z"c, "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "-"c, "_"c}

        Private Function DecryptPort(chars As Char()) As Integer

            Dim port As Integer = 0

            Try

                If chars.Length <> 3 Then Throw New ArgumentOutOfRangeException("Le port doit contenir au minimum 3 caractéres.")

                For i As Integer = 0 To 2 - 1

                    port += CInt((Math.Pow(64, 2 - i) * Get_Hash(chars(i))))

                Next

                port += Get_Hash(chars(2))

            Catch ex As Exception

            End Try

            Return port

        End Function

        Private Function Get_Hash(ch As Char) As Short

            Try

                For i As Short = 0 To caracteres_array.Length - 1

                    If caracteres_array(i) = ch Then Return i

                Next

            Catch ex As Exception

            End Try

            Return 0

        End Function

#End Region

#End Region

        Sub Version(data As String)

            With Bot

                Try

                    ' AlEv 1.30.1 
                    ' AlEv Version

#Region "Lecture"

                    Dim swLecture As New IO.StreamReader(Application.StartupPath & "\Data/Serveur.txt")

                    Dim ligneFinal As String = ""

                    Do Until swLecture.EndOfStream

                        Dim ligne As String = swLecture.ReadLine

                        If ligne <> "" Then

                            Dim separate As String() = Split(ligne, "|")

                            If separate(0) = "Authentification" Then

                                ligneFinal &= ligne.Replace(separate(3), Mid(data, 5) & "e") & vbCrLf

                            Else

                                ligneFinal &= ligne & vbCrLf

                            End If

                        End If

                    Loop

                    swLecture.Close()

#End Region

#Region "Ecriture"

                    'J'ouvre le fichier pour y écrire se que je souhaite
                    Dim swEcriture As New IO.StreamWriter(Application.StartupPath + "\Data/Serveur.txt")

                    'J'écris dedans avant de le fermer.
                    swEcriture.WriteLine(ligneFinal)

                    'Puis je le ferme.
                    swEcriture.Close()

#End Region

                    ChargeServeur()

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Version", ex.Message)

                End Try

            End With

        End Sub

        Sub Erreur_Compte(data As String)

            With Bot

                Try

                    Select Case Mid(data, 4)

                        Case "AlEa"

                            EcritureMessage("[Dofus]", "Déjà en connexion.", Color.Red)

                        Case "AlEb"

                            EcritureMessage("[Dofus]", "Votre compte à été banni.", Color.Red)

                        Case "AlEc"

                            EcritureMessage("[Dofus]", "Vous êtes déjà connécté au serveur du jeu.", Color.Red)

                        Case "AlEd"

                            EcritureMessage("[Dofus]", "Vous avez déconnecté une personne utilisant le compte.", Color.Red)

                        Case "AlEf"

                            EcritureMessage("[Dofus]", "Mauvais mot de passe.", Color.Red)

                        Case "AlEk"

                            'AlEk Jour | Heure | Minute

                            Dim Separation() As String = Split(Mid(data, 5), "|")

                            'Le like me permet de regarde si le résultat correspond à chaque séparation.
                            If "1" = Separation(0) Like (Separation(1) Like Separation(2)) Then

                                EcritureMessage("[Dofus]", "Compte invalide, si vous avez 1j 1h 1m, il s'agît d'une IP bannie définitivement" & vbCrLf & "il vous suffit de changer d'IP pour régler le problème.", Color.Red)

                            Else

                                EcritureMessage("[Dofus]", "Ton compte est invalide pendant " & Separation(0) & " Jour(s) " & Separation(1) & " Heure(s) " & Separation(2) & " Minute(s)'.", Color.Red)

                            End If

                        Case "AlEn"

                            EcritureMessage("[Dofus]", "La connexion ne sait pas faite correctement.", Color.Red)

                        Case "AlEp"

                            EcritureMessage("[Dofus]", "Votre compte n'est pas valide.", Color.Red)

                        Case "AlEs"

                            EcritureMessage("[Dofus]", "Le Pseudo est déjà utilisé, veuillez en choisir un autre.", Color.Red)

                        Case "AlEv"

                            EcritureMessage("[Dofus]", "La version de DOFUS installée est invalide pour ce serveur. Pour accéder au jeu, la version '" & Mid(data, 5) & "' est nécessaire.", Color.Red)

                        Case "AlEw"

                            EcritureMessage("[Dofus]", "Le serveur est complet. (Vous n'étes donc plus abonnée)", Color.Red)

                        Case Else

                            ErreurFichier("[Dofus - Compte]", "Unknow", data)

                    End Select

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Erreur_Compte", ex.Message)

                End Try

            End With

        End Sub

        Sub Erreur_Serveur(data As String)

            With Bot

                Try

                    Select Case data

                        Case "AXEf"

                            EcritureMessage("[Dofus]",
                                            "Serveur : COMPLET" & vbCrLf &
                                            "Nombre maximum de joueurs atteint." & vbCrLf &
                                            "Pour bénéficier d'un accès prioritaire aux serveurs, nous vous invitons à vous abonner.
                                            Vous pouvez également tenter de vous connecter sur un autre serveur. Vous pouvez également 
                                            télécharger et vous connecter sur les serveurs Dofus 2.0, qui proposent un plus grand nombre
                                            de place pour accueillir les joueurs !", Color.Red)

                            .Socket_Authentification.Connexion_Game(False)

                            .Mitm.Client.Close()

                        Case "AXEd"

                            EcritureMessage("[Dofus]", "Serveur : En sauvegarde.", Color.Red)

                        Case "ATE"


                            EcritureMessage("[Dofus]", "Connexion interrompue avec le serveur." & vbCrLf &
                                                       "Votre connexion est trop lente ou instable.", Color.Red)

                            .Socket_Authentification.Connexion_Game(False)

                            .Mitm.Client.Close()

                    End Select

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Connexion_Erreur_Serveur", ex.Message)

                End Try

            End With

        End Sub

        Sub Cadeau(data As String)

            With Bot

                'Ag 1 | 1     | Chienchien         | Avec+sous+peine+de+perdre+une+main. |   | 44390e4~6af~1~~320#0#0#a,7c#14###0d0+20; d'abord pdv 10 ensuite sagesse a 20
                'Ag ? | Ordre | Nom                | Description                         | ? | id unique etc...

                Dim separateData As String() = Split(data, "|")
                Dim separateItem As String() = Split(separateData(4), "~")

                Dim NewItem As New Item_Variable.Information

                With NewItem

                    .Nom = AsciiDecoder(separateData(2))
                    .Description = separateData(3)
                    .IdObjet = Convert.ToInt64(separateItem(1), 16)
                    .IdUnique = Convert.ToInt64(separateItem(0), 16)
                    .Quantiter = Convert.ToInt64(separateItem(2), 16)
                    .Caracteristique = Item.Caracteristique(separateItem(4), Convert.ToInt64(separateItem(1), 16))
                    .CaracteristiqueBrute = separateItem(4)
                    .Categorie = VarItems(.IdObjet).Catégorie

                    If separateItem(3) <> "" Then

                        .Equipement = Convert.ToInt64(separateItem(3), 16)

                    ElseIf VarItems(Convert.ToInt64(separateItem(1), 16)).Catégorie = "24" Then

                        .Equipement = "Quete"

                    Else

                        .Equipement = ""

                    End If

                End With

                If .Personnage.Cadeau.ContainsKey(separateData(1)) Then

                    .Personnage.Cadeau(separateData(1)) = NewItem

                Else

                    .Personnage.Cadeau.Add(separateData(1), NewItem)

                End If

            End With

        End Sub

    End Module

End Namespace