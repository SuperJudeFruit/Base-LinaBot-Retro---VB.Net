Namespace Guilde

    Module Guilde

#Region "Guilde Base"

        Sub Information(data As String)

            With Bot

                Try

                    ' gS LinaculerBot     | a | 0 | i | 8fsdgj | 0 
                    ' gS Nom de la guilde | ? | ? | ? | ?      | ?

                    Dim separateData As String() = Split(Mid(data, 3), "|")

                    With .Guilde

                        .Guilde = True

                        .Nom = separateData(0)

                        If .Invitation Then

                            .Invitation = False

                            EcritureMessage("[Dofus]", "Tu viens d'intégrer la guilde " & separateData(0), Color.Green)

                        End If

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Information", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Experience(data As String)

            With Bot

                Try

                    ' gIG 0 | 5      | 28000   | 30271        | 48000
                    ' gIG ? | Niveau | exp min | exp actuelle | exp max

                    Dim separate As String() = Split(data, "|")

                    With .Guilde

                        .Niveau = separate(1)

                        With .Experience

                            .Minimum = separate(2)
                            .Actuelle = separate(3)
                            .Maximum = separate(4)
                            .Pourcentage = .Actuelle / .Maximum * 100

                        End With

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Experience", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Recrutement(data As String)

            With Bot

                Try


                    ' gJR Linaculer
                    ' gJR Nom

                    .Guilde.Invitation = True

                    .Guilde.Inviteur = .Personnage.ID

                    EcritureMessage("[Dofus]", "Tu invites " & Mid(data, 4) & " à rejoindre ta guilde...", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Recrutement", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Invitation(data As String)

            With Bot

                Try


                    ' gJr 1234567 | Linaculer | [Ankama]
                    ' gJr ID      | Nom       | Nom Guilde

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    With .Guilde

                        .Invitation = True

                        .Inviteur = separateData(0)

                    End With

                    EcritureMessage("[Dofus]", separateData(1) & " t'invite à rejoindre sa guilde (" & separateData(2) & ") acceptes-tu ?", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Invitation", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Refuse(data As String)

            With Bot

                Try

                    ' gJE 1234567
                    ' gJE id

                    With .Guilde

                        .Invitation = False
                        .Inviteur = ""
                        .Inviter = ""

                    End With

                    EcritureMessage("[Dofus]", Mid(data, 4) & " refuse d'intégrer ta guilde.", Color.Red)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Refuse", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Accepte(data As String)

            With Bot

                Try

                    ' gJKa Linaculer
                    ' gJKa nom

                    With .Guilde

                        .Invitation = False
                        .Inviteur = ""
                        .Inviter = ""

                    End With

                    EcritureMessage("[Dofus]", Mid(data, 4) & " accepte d'intégrer ta guilde.", Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Accepte", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

#End Region

#Region "Guilde Membres"

        Sub Exclut(data As String)

            With Bot

                Try

                    ' gKK

                    Dim separateData As String() = Split(Mid(data, 4), "|")

                    If separateData(0).ToLower = .Personnage.NomDuPersonnage.ToLower Then

                        EcritureMessage("[Dofus]", "Tu as banni " & separateData(1) & " de ta guilde.", Color.Green)

                    Else

                        EcritureMessage("[Dofus]", "Tu es banni de la guilde.", Color.Green)

                    End If


                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Exclut", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Ajoute(data As String)

            With Bot

                Try

                    ' gIM+ 1234567  ; Linaculer ; 60     ; 81     ; 2    ; 0        ; 0   ; 29694 ; 1        ; 0          ; 0                  | Next
                    ' gIM+ IdUnique ; Nom       ; Niveau ; Classe ; Rang ; XpGagnée , %Xp ; Droit ; Connecté ; Alignement ; Dernière connexion | 

                    Dim rangActuel() As String = {"A l'essai", "Meneur", "Bras Droit", "Trésorier", "Protecteur", "Artisan", "Réserviste", "Gardien",
                                                  "Eclaireur", "Espion", "Diplomate", "Secrétaire", "Tueur de familiers", "Braconnier",
                                                  "Chercheur de trésor", "Voleur", "Initié", "Assassin", "Gouverneur", "Muse", "Conseiller", "Elu", "Guide",
                                                  "Mentor", "Recruteur", "Eleveur", "Marchand", "Apprenti", "Bourreau", "Mascotte", "Pénitent",
                                                  "Tueur de Percepteurs", "Déserteur", "Traître", "Boulet", "Larbin", "A l'essai"}

                    With .Guilde

                        .Membre.Clear()

                        Dim separateData As String() = Split(data, "|")

                        For i = 0 To separateData.Length - 1

                            Dim separate As String() = Split(separateData(i), ";")

                            Dim NewJoueur As New Guilde_Variable.Membre

                            With NewJoueur

                                .ID = separate(0)
                                .Nom = separate(1)
                                .Niveau = separate(2)
                                .Classe = GuildeClasseAlignement(separate(3))
                                .Rang = rangActuel(separate(4))
                                .Rang_Chiffre = separate(4)

                                With .Experience

                                    .Actuelle = separate(5)
                                    .Pourcentage = separate(6)

                                End With

                                .Connecter = separate(8)
                                .Alignement = GuildeClasseAlignement(separate(9))
                                .DerniereConnection = If(separate(10) = -1, "Dernière connexion il y a moins d'un jour", "")
                                .Droit = GuildeDroits(separate(1), separate(7))
                                .Droit_Chiffre = separate(7)

                            End With

                            .Membre.Add(separate(1), NewJoueur)

                        Next

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Ajoute", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Private Function GuildeClasseAlignement(Valeur As Integer) As String

            Try

                Select Case Valeur
                    Case 0 : Return "Neutre" 'Neutre
                    Case 1 : Return "Brakmarien"
                    Case 2 : Return "Bontarien"
                    Case 10, 11 : Return "Feca"
                    Case 20, 21 : Return "Osamodas"
                    Case 30, 31 : Return "Enutrof"
                    Case 40, 41 : Return "Sram"
                    Case 50, 51 : Return "Xelor"
                    Case 60, 61 : Return "Ecaflip"
                    Case 70, 71 : Return "Eniripsa"
                    Case 80, 81 : Return "Iop"
                    Case 90, 91 : Return "Cra"
                    Case 100, 101 : Return "Sadida"
                    Case 110, 111 : Return "Sacrieur"
                    Case 120, 121 : Return "Pandawa"
                    Case Else
                        Return Valeur
                End Select

            Catch ex As Exception

                ErreurFichier("unknow", "", ex.Message)

            End Try

            Return Valeur

        End Function

        Private Function GuildeDroits(Name_Joueur As String, Information As Integer) As Guilde_Variable.Droit

            With Bot

                Dim newRights As New Guilde_Variable.Droit
                Dim Valeur() As Integer = {16384, 8192, 4096, 512, 256, 128, 64, 32, 16, 8, 4, 2}

                Try

                    With newRights

                        If Information = 1 Then

                            .GererLesBoosts = True
                            .GererLesDroits = True
                            .InviterDeNouveauxMembres = True
                            .Bannir = True
                            .GererLesRepartitionsXP = True
                            .GererSaRepartitionXP = True
                            .GererLesRangs = True
                            .PoserUnPercepteur = True
                            .CollecterSurUnPercepteur = True
                            .UtiliserLesEnclos = True
                            .AmenagerLesEnclos = True
                            .GererLesMonturesDesAutresMembres = True

                        Else

                            For i = 0 To 11

                                If Information >= Valeur(i) Then

                                    Select Case Information

                                        Case 16384

                                            .GererLesBoosts = True

                                        Case 8192

                                            .GererLesDroits = True

                                        Case 4096

                                            .InviterDeNouveauxMembres = True

                                        Case 512

                                            .Bannir = True

                                        Case 256

                                            .GererLesRepartitionsXP = True

                                        Case 128

                                            .GererSaRepartitionXP = True

                                        Case 64

                                            .GererLesRangs = True

                                        Case 32

                                            .PoserUnPercepteur = True

                                        Case 16

                                            .CollecterSurUnPercepteur = True

                                        Case 8

                                            .UtiliserLesEnclos = True

                                        Case 4

                                            .AmenagerLesEnclos = True

                                        Case 2

                                            .GererLesMonturesDesAutresMembres = True

                                    End Select

                                    Information -= Valeur(i)

                                End If

                            Next

                        End If

                    End With

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_mdlGuilde_GuildeDroits", ex.Message)

                End Try

                Return newRights

            End With

        End Function

        Sub Supprime(data As Integer)

            With Bot

                Try

                    ' gIM- Linaculer
                    ' gIM- nom du joueur

                    Dim nom As String = Mid(data, 5)

                    If .Guilde.Membre.ContainsKey(nom) Then

                        .Guilde.Membre.Remove(nom)

                    End If

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Supprime", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

#End Region

#Region "Guilde Personnalisation"

        Sub Personnalisation(data As String)

            With Bot

                Try

                    ' gIB1 |0|200|2|1000|100|0|1|5|1020|462;0|461;0|460;0|459;0|458;0|457;0|456;0|455;0|454;0|453;0|452;0|451;0
                    ' gIB1 |                   ?

                    Dim NewGuildePercepteur As New Guilde_Variable.Percepteur

                    Dim separateData As String() = Split(data, "|")

                    With NewGuildePercepteur

                        .NombreDePercepteur = separateData(0)
                        .ActuellementPercepteur = separateData(1)
                        .PointsDeVie = separateData(2)
                        .BonusAuxDommages = separateData(3)
                        .Pods = separateData(4)
                        .Prospection = separateData(5)
                        .Sagesse = separateData(6)

                        .ResteARepartir = separateData(8)
                        .CoutPourPoserPercepteur = separateData(9)

                        .ArmureAqueuse = Split(separateData(21), ";")(1)
                        .ArmureIncandescente = Split(separateData(20), ";")(1)
                        .ArmureTerrestre = Split(separateData(19), ";")(1)
                        .ArmureVenteuse = Split(separateData(18), ";")(1)
                        .Flamme = Split(separateData(17), ";")(1)
                        .Cyclone = Split(separateData(16), ";")(1)
                        .Vague = Split(separateData(15), ";")(1)
                        .Rocher = Split(separateData(14), ";")(1)
                        .MotSoignant = Split(separateData(13), ";")(1)
                        .Desenvoutement = Split(separateData(12), ";")(1)
                        .CompulsionDeMasse = Split(separateData(11), ";")(1)
                        .Destabilisation = Split(separateData(10), ";")(1)

                    End With

                    .Guilde.Percepteur = NewGuildePercepteur

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Personnalisation", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

#End Region

#Region "Guilde Percepteurs"

        Sub Percepteur_Pose(data As String)

            With Bot

                Try

                    ' gTS 12 , 28 | 8840   | -15   | 6     | Name
                    ' gTS ?  , ?  | Map ID | Pos X | Pos Y | Nom du poseur

                    Dim separateData As String() = Split(data, "|")

                    EcritureMessage("[Dofus]", "Le percepteur [Name] a été posé en (" & separateData(2) & ", " & separateData(3) & ") par " & separateData(4), Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Percepteur_Pose", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Percepteur(data As String)

            With Bot

                Try

                    ' gITM+ 2ki ; b , 28 , Linaculer1    , 1516232486517 ,   , 0 , 0 ; 2ki ; 0 
                    ' gITM+     ; ? , ?  , Nom du poseur , ?             , ? , ? , ? ; ?   ; ?

                    'Inconnu actuellement

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Percepteur", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Percepteur_Echec(data As String)

            With Bot

                Try

                    ' gHEy

                    EcritureMessage("[Dofus]", "Impossible de poser le percepteur maintenant, il doit se reposer.", Color.Red)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Percepteur_Echec", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Percepteur_Retire(data As String)

            With Bot

                Try

                    ' gTR m , f | 8858   | -17   | 7     | Name
                    ' gTR ? , ? | Map ID | Pos X | Pos Y | Nom du joueur qui retire

                    Dim separateData As String() = Split(data, "|")

                    EcritureMessage("[Dofus]", "Le percepteur [Name] en (" & separateData(2) & ", " & separateData(3) & ") a été retiré par " & separateData(4), Color.Green)

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Percepteur_Retire", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

#End Region

#Region "Guilde Enclos"

        Sub Enclos(data As String)

            With Bot

                Try


                    ' gIF3 | 9999   ; 2           ; 2      ;  
                    ' gIF3 | Map ID ; DD actuelle ; DD max ; ?

                    .Guilde.Enclos.Clear()

                    Dim separateData As String() = Split(data, "|")

                    For i = 1 To separateData.Length - 1

                        Dim separate As String() = Split(separateData(i), ";")

                        Dim newEnclos As New Guilde_Variable.Enclos

                        With newEnclos

                            .MapID = separate(0)
                            .Position = VarMap(separate(0))

                            With .Dragodinde

                                .Actuelle = separate(1)
                                .Maximum = separate(2)

                            End With


                        End With

                        .Guilde.Enclos.Add(separate(0), newEnclos)

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Enclos", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

#End Region

#Region "Guilde Maisons"

        Sub Maison(data As String)

            With Bot

                Try

                    'gIH+ 999 ; Linaculer   ; 666,66 ;            ; 499    |
                    '     ID  ; proriétaire ; Pos    ; compétence ; Droits | Next

                    .Guilde.Maison.Clear()

                    Dim separateData As String() = Split(Mid(data, 5), "|")

                    For i = 0 To separateData.Length - 1

                        Dim separate As String() = Split(separateData(i), ";")

                        Dim Valeur As Integer() = {256, 128, 64, 32, 16, 8, 4, 2, 1}

                        Dim newMaison As New Guilde_Variable.Maison

                        With newMaison

                            For a = 0 To 8

                                If CInt(separate(4)) >= Valeur(a) Then

                                    Select Case Valeur(a)

                                        Case 256 '256 =  Repos autorisé aux membres de la guilde dans cette maison

                                            .ReposAutoriser = True

                                        Case 128 '128 = Téléportation autorisée vers cette maison

                                            .TeleportationAutoriser = True

                                        Case 64 '64 = Accès aux coffres interdit aux non-membres de la guilde

                                            .AccesCoffresInterditNonMembreGuilde = True

                                        Case 32 '32 = Accès aux coffres autorisé aux membres de la guilde

                                            .AccesCoffresAutoriseMembreGuilde = True

                                        Case 16 '16 = Accès interdit aux non-membres de la guilde

                                            .AccesInterditNonMembreGuilde = True

                                        Case 8 '8 = Accès autorisé aux membres de la guilde

                                            .AccesAutoriserMembreGuilde = True

                                        Case 4 '4 = Blason Visible pour tout le monde

                                            .BlasonVisiblePourToutMonde = True

                                        Case 2 '2 = Blason Visible pour la guilde

                                            .BlasonVisiblePourGuilde = True

                                        Case 1 '1 = Maison Visible pour la guilde.

                                            .MaisonVisiblePourGuilde = True

                                    End Select

                                    separate(4) = CInt(separate(4)) - Valeur(a)

                                End If

                            Next

                            .ID = separate(0)
                            .Prorietaire = separate(1)
                            .Position = separate(2)
                            .Competence = separate(3)

                        End With

                        .Guilde.Maison.Add(separate(1), newMaison)

                    Next

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage, "Guilde_Maison", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

#End Region

    End Module

End Namespace