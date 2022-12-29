Namespace Tchat

    Module Tchat

        Sub Canal(data As String)

            With Bot

                'cC +                *#%!$:?pi^
                'cC Active/Désactive Canaux

                Try

                    Dim checked As Boolean = Mid(data, 3, 1)

                    Dim newCanaux As Tchat_Variable.Canaux = .Tchat.Canaux

                    With newCanaux

                        For i = 3 To data.Length - 1

                            Select Case data(i)

                                Case "i" 'Information

                                    .Information = checked

                                Case "*" 'Communs/Défaut

                                    .Commun = checked

                                Case "#", "$", "p" 'groupe/privée/équipe

                                    .GroupeEquipeMP = checked

                                Case "%" 'guilde

                                    .Guilde = checked

                                Case "!" 'alignement

                                    .Alignement = checked

                                Case "?" 'recrutement

                                    .Recrutement = checked

                                Case ":" 'Commerce

                                    .Commerce = checked

                                Case "e" ' Evenement

                                    .Evenement = checked

                            End Select

                        Next

                    End With

                    .Tchat.Canaux = newCanaux

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage & "_" & .Personnage.Serveur, "Tchat_Canal", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Information(data As String)

            With Bot

                'Im 1165
                'Im Numéro du texte a affiché

                Try

                    data = Mid(data, 3)

                    Dim newInformation As New Tchat_Variable.Information

                    With newInformation

                        If data.Contains(";"c) Then

                            Dim Separation() As String = Split(data, ";")

                            Select Case Separation(0)

                                Case "1202" 'Im1201;[Seydlex] 


                                    .Canal = "[Modérateur]"
                                    .Couleur = Color.Red
                                    .Message = "Attention un modérateur vous surveille : " & Separation(1) & "."

                                Case "1184" 'Im1184;Linaculer

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Green
                                    .Message = Separation(1) & " vient de se reconnecter en combat."

                                Case "1171" 'Im1171;1~9~19

                                    Separation = Split(Separation(1), "~")

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Red
                                    .Message = "Impossible de lancer ce sort : Vous avez une portée de " & Separation(0) & " à " & Separation(1) & " et vous visez à " & Separation(2) & " !"

                                Case "1170" 'Im1170;0~4

                                    Separation = Split(Separation(1), "~")

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Red
                                    .Message = "Vous avez '" & Separation(0) & "' PA, hors il vous en faut minimum '" & Separation(1) & "' PA pour lancer ce sort."

                                Case "1168" 'Im1168;1

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Vous ne pouvez pas poser plus de " & Separation(1) & " percepteur(s) par zone."

                                Case "1167" 'Im1167;54

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Vous ne pouvez pas poser de percepteur ici avant " & Separation(1) & " minutes."

                                Case "1139" 'Im1139;5

                                    .Canal = "[Percepteur]"
                                    .Couleur = Color.Red
                                    .Message = "Attention, la fenêtre d'échange se fermera automatiquement dans " & Separation(1) & " minutes."

                                Case "1111" 'Im1111;3

                                    .Canal = "[Dragodinde]"
                                    .Couleur = Color.Fuchsia
                                    .Message = "A peine entrée dans l'étable, votre monture s'accroupit et commence à mettre bas. Après quelques instants, vous pouvez constater que tout s'est bien passé. Vous voilà responsable de " & Separation(1) & " nouvelle(s) monture(s)."

                                Case "0188" '"Im0188;player"

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Red
                                    .Message = "Et comme d'habitude, c'est à " & Separation(1) & " que l'on doit cet exploit..."

                                Case "0157" ' Im0157;6

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Ce canal n'est accessible en diffusion aux abonnés qu'à partir du niveau " & Separation(1)

                                Case "0153" 'Im0153;xx.xxx.xx.xx

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Votre adresse IP actuelle est : " & Separation(1) & "."

                                Case "0152"

                                    'Im0152; 2019  ~ 06   ~ 27   ~ 7     ~ 19     ~ xx.xxx.xx.xx
                                    'Im0152; Année ~ Mois ~ Jour ~ Heure ~ Minute ~ IP

                                    Separation = Split(Separation(1), "~")

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Précédente connexion sur votre compte effectuée le : " &
                                           Separation(2) & "/" & Separation(1) & "/" & Separation(0) & " à " & Separation(3) & ":" & Separation(4) &
                                          " via l'adresse IP  : " & Separation(5)

                                Case "0143"

                                    ' Im0143;Linaculer (<b><a href="asfunction:onHref,ShowPlayerPopupMenu,Linacular">Linaculeur</a></b>) 

                                    Separation = Split(Separation(1), " (<b><a href=""""asfunction: onHref, ShowPlayerPopupMenu, Linacular"""">")
                                    Dim V_Nom_De_Compte As String = Separation(0)
                                    Separation = Split(Separation(1), "</a></b>)")

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Le joueur : " & V_Nom_De_Compte & "(" & Separation(0) & ") vient de se connecter."

                                Case "0115"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Ce canal est restreint pour améliorer sa lisibilité. Vous pourrez envoyer un nouveau message dans " & Separation(1) & " secondes. Ceci ne vous autorise cependant pas pour autant à surcharger ce canal."

                                Case "128" 'Im128;Linaculer

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Red
                                    .Message = "En attente du joueur " & Separation(1) & "..."

                                Case "120"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Cet emplacement de stockage est déjà utilisé."

                                Case "116"

                                    'Im116;[Seydlex]~Bot tchatJoueur

                                    Separation = Split(Separation(1), "~")

                                    .Canal = "[Modérateur]"
                                    .Couleur = Color.Red
                                    .Message = "Vous avez été banni par " & Separation(0) & ". Motif : " & Separation(2)

                                Case "115"

                                    ' Im115;16 heures, 43 minutes, 43 secondes

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Pour des raisons de maintenances, le serveur va être redémarré dans " & Separation(1)

                                Case "092"

                                    'Im092;50

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Vous avez récupéré " & Separation(1) & " points d'énergie en vous reposant."

                                Case "065"

                                    'Im065; 300         ~ 2598     ~ 2598     ~ 1
                                    'Im065; Kamas gagné ~ ID Objet ~ ID Objet ~ Quantité

                                    Separation = Split(Separation(1), "~")

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Votre compte en banque a été crédité de " & Separation(0) & " kamas suite à la vente de '" & VarItems(Separation(1)).Nom & "' (x " & Separation(3) & ")."

                                Case "056"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Quête terminée : " & VarQuête(Separation(1))

                                Case "055"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Quête mise à jour : " & VarQuête(Separation(1))

                                Case "054"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Nouvelle quête : " & VarQuête(Separation(1))

                                Case "053"

                                    'Im053;Linaculer

                                    .Canal = "[Groupe]"
                                    .Couleur = Color.Green
                                    .Message = Separation(1) & " ne suit plus votre déplacement."

                                Case "052"

                                    'Im052;Linaculer

                                    .Canal = "[Groupe]"
                                    .Couleur = Color.Green
                                    .Message = Separation(1) & " suit votre déplacement."

                                Case "045"

                                    ' Im045;50

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Tu as gagné " & Separation(1) & " kamas."

                                Case "036"

                                    'Im036;Linaculer

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = Separation(1) & " vient de rejoindre le combat en spectateur."

                                Case "034"

                                    'Im034;60 

                                    'Comptes(index).Combat.EnCombat = False

                                    .Canal = "[Familier]"
                                    .Couleur = Color.Red
                                    .Message = "Tu as perdu " & Separation(1) & " points d'énergie."

                                Case "022"

                                    'Im022;1~1568

                                    Separation = Split(Separation(1), "~")

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Tu as perdu " & Separation(0) & " '" & VarItems(Separation(1)).Nom & "'."

                                Case "020"

                                    ' Im022;481

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Tu as dû donner " & Separation(1) & " kamas pour pouvoir accéder à ce coffre."

                                Case "08"

                                    'Im08;17293

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Tu as gagné " & Separation(1) & " points d'expérience."

                                Case "01"

                                    'Im01;100

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Tu as récupéré " & Separation(1) & " points de vie."

                                Case Else

                                    .Canal = "[Inconnu]"
                                    .Couleur = Color.Red
                                    .Message = data

                                    ErreurFichier(Bot.Personnage.NomDeCompte, "GiDofusInformation", data)

                            End Select

                        Else

                            Select Case data

                                Case "1183"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "La zone 'Incarnam' fonctionne sur plusieurs instances, pour éviter qu'un trop grand nombre de joueurs soient présent dans cette zone de petite taille. Ceci signifie qu'il existe plusieurs 'Incarnam' en parallèle, afin qu'il n'y ait pas plus d'un certain nombre de joueurs dans la même instance. Vous pouvez donc ne pas être dans le même 'Incarnam' que vos amis, pour les rejoindre, vous pouvez utiliser la liste d'amis, et vous retrouver instantanément à leurs côtés, à conditions qu'ils soient eux aussi dans Incarnam en dehors des grottes et donjons."

                                Case "1177"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Vous avez trop d'objets dans votre inventaire, vous ne pouvez pas les voir tous. (1000 objets maximum)"

                                Case "1175"

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Red
                                    .Message = "Impossible de lancer ce sort actuellement."""

                                Case "1174"

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Red
                                    .Message = "Un obstacle géne le passage."

                                Case "1165"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "La sauvegarde du serveur est terminée. L'accès au serveur est de nouveau possible. Merci de votre compréhension."

                                Case "1164"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Une sauvegarde du serveur est en cours... Vous pouvez continuer de jouer, mais l'accès au serveur est temporairement bloqué. La connexion sera de nouveau possible d'ici quelques instants. Merci de votre patience."

                                Case "1159"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Vous êtes à court de potion d'enclos de guilde."

                                Case "1127"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Incarnam ne vous est plus accessible désormais, votre expérience fait de vous un aventurier apte à parcourir le monde sans continuer dans cette zone..."

                                Case "1120"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Impossible d'interagir avec votre percepteur sur la carte même où vous vous êtes connecté."

                                Case "1117"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Impossible d'être sur une monture à l'intérieur d'une maison."

                                Case "1105"

                                    .Canal = "[Dragodinde]"
                                    .Couleur = Color.Red
                                    .Message = "L'étable est pleine. Vous ne pouvez conserver que 100 montures maximum."

                                Case "1104"

                                    .Canal = "[Dragodinde]"
                                    .Couleur = Color.Red
                                    .Message = "Monture désignée invalide, trop de monture dans l'étable"

                                Case "1102"

                                    .Canal = "[Dragodinde]"
                                    .Couleur = Color.Red
                                    .Message = "Cellule cible invalide"

                                Case "0194"

                                    .Canal = "[Forgemagie]"
                                    .Couleur = Color.Red
                                    .Message = "La magie n'a pas parfaitement fonctionné, une des caractéristiques de l'objet a baissé en puissance."

                                Case "0183"

                                    .Canal = "[Forgemagie]"
                                    .Couleur = Color.Red
                                    .Message = "Malgré vos talents, la magie n'opère pas et vous sentez l'échec de la transformation."

                                Case "0144"

                                    .Canal = "[Récolte]"
                                    .Couleur = Color.Red
                                    .Message = "Votre inventaire est plein. Votre récolte est perdue..."

                                Case "0143"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Le joueur : " & data & " vient de se connecter."

                                Case "0118"

                                    .Canal = "[Craft]"
                                    .Couleur = Color.Red
                                    .Message = "Vous n'arrivez pas à assembler correctement les ingrédients, et vous n'arrivez pas à concevoir quoi que ce soit d'utilisable cette fois."

                                Case "0117"

                                    .Canal = "[Forgemagie]"
                                    .Couleur = Color.Red
                                    .Message = "Malgré vos talents, la magie n'opère pas et vous sentez l'échec de la transformation, ainsi que la diminution de la puissance de l'objet.."

                                Case "0106" ' Im0106

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Pour utiliser le canal d'alignement vous devez développer vos ailes à 3 ou plus, ou encore avoir choisi une spécialisation par les quêtes d'alignement (niveau de quêtes à partir de 20)"

                                Case "0104"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Demande d'aide annulée..."

                                Case "0103"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Demande d'aide signalée..."

                                Case "189"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Bienvenue sur Dofus, dans le Monde des douze !" & vbCrLf &
                            "Rappel : prenez garde, il est interdit de transmettre votre identifiant de connexion ainsi que votre mot de passe."

                                Case "172"

                                    .Canal = "[Hôtel de Vente]"
                                    .Couleur = Color.Red
                                    .Message = "Cet objet n'est plus disponible à ce prix. Quelqu'un a été plus rapide..."

                                Case "167"

                                    .Canal = "[Hôtel de Vente]"
                                    .Couleur = Color.Red
                                    .Message = "Vous ne pouvez pas mettre plus d'objets en vente actuellement..."

                                Case "165"

                                    .Canal = "[Hôtel de vente]"
                                    .Couleur = Color.Red
                                    .Message = "Vous ne disposez pas d'assez de kamas pour acquitter la taxe de mise en vente..."

                                Case "137"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Ton mari n'est pas connecté."

                                Case "120"

                                    .Canal = "[Maison]"
                                    .Couleur = Color.Red
                                    .Message = "Cet emplacement de stockage est déjà utilisé."

                                Case "118" 'Im188

                                    .Canal = "[Familier]"
                                    .Couleur = Color.Red
                                    .Message = "Votre familier ne peut vous suivre tant que vous êtes sur votre monture..."

                                Case "113" ' Im113

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Cette action n'est pas autorisée sur cette carte."

                                Case "112"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Red
                                    .Message = "Vous êtes trop chargé. Jetez quelques objets afin de pouvoir bouger."

                                Case "096"

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Green
                                    .Message = "L'équipe accepte de nouveau des personnages supplémentaires."

                                Case "095"

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Green
                                    .Message = "L'équipe n'accepte plus de personnages supplémentaires."

                                Case "094"

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Green
                                    .Message = "L'équipe accepte les membres de tous les groupes."

                                Case "093"

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Green
                                    .Message = "L'équipe n'accepte désormais que les membres du groupe du personnage principal."

                                Case "068"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Lot acheté."

                                Case "040"

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Green
                                    .Message = "Le mode 'spectateur' est désactivé."

                                Case "039"

                                    .Canal = "[Combat]"
                                    .Couleur = Color.Green
                                    .Message = "Le mode 'spectateur' est activé."

                                Case "037"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Vous êtes désormais considéré comme absent."

                                Case "032"

                                    .Canal = "[Familier]"
                                    .Couleur = Color.Green
                                    .Message = "Votre familier apprécie le repas."

                                Case "031"

                                    .Canal = "[Familier]"
                                    .Couleur = Color.Red
                                    .Message = "Vous donnez à manger à votre familier famélique qui traînait comme un zombi. Il se force à manger mais la nourriture qu'il avale fait 3 fois son estomac et il se tord de douleur. Au moins il a mangé."

                                Case "029"

                                    .Canal = "[Familier]"
                                    .Couleur = Color.Green
                                    .Message = "Vous donnez à manger à votre familier. Il semble qu'il avait très faim."

                                Case "027"

                                    .Canal = "[Familier]"
                                    .Couleur = Color.Red
                                    .Message = "Vous donnez à manger à répétition à votre familier déjà obèse. Il avale quand même la ressource et fait une indigestion."

                                Case "026"

                                    .Canal = "[Familier]"
                                    .Couleur = Color.Red
                                    .Message = "Vous donnez à manger à votre familier alors qu'il n'avait plus faim. Il se force pour vous faire plaisir."

                                Case "025"

                                    .Canal = "[Familier]"
                                    .Couleur = Color.Green
                                    .Message = "Votre familier vous fait la fête !"

                                Case "153"

                                    .Canal = "[Familier]"
                                    .Couleur = Color.Red
                                    .Message = "Votre familier prend la ressource, la renifle un peu, ne semble pas convaincu et vous la rend."

                                Case "024"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Tu viens de mémoriser un nouveau zaap."

                                Case "06"

                                    .Canal = "[Dofus]"
                                    .Couleur = Color.Green
                                    .Message = "Position sauvegardée."

                                Case Else

                                    .Canal = "[INCONNU]"
                                    .Couleur = Color.Red
                                    .Message = data

                                    ErreurFichier(Bot.Personnage.NomDeCompte, "GiDofusInformation", data)

                            End Select

                        End If

                    End With

                    .Tchat.Message = newInformation

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage & "_" & .Personnage.Serveur, "Tchat_Information", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Message(data As String)

            With Bot

                'cMK %     | 1234567   | Linaculer  | salut tout le monde |
                'cMK Canal | Id Joueur | Nom Joueur | Message             | Caractéristique des objets (Rien si aucun)

                Try

                    Dim separateData As String() = Split(data, "|")

                    Dim newInformation As New Tchat_Variable.Information

                    With newInformation

                        Select Case Mid(data, 4, 1)

                            Case "|"

                                .Canal = "[General]"
                                .Couleur = Color.Black

                            Case "$"

                                .Canal = "[Groupe]"
                                .Couleur = Color.Blue

                            Case "F"

                                .Canal = "[Privée de]"
                                .Couleur = Color.Blue

                            Case "T"

                                .Canal = "[Privée à]"
                                .Couleur = Color.Blue

                            Case "%"

                                .Canal = "[Guilde]"
                                .Couleur = Color.Violet

                            Case "!"

                                .Canal = "[Alignement]"
                                .Couleur = Color.Orange

                            Case "?"

                                .Canal = "[Recrutement]"
                                .Couleur = Color.DimGray

                            Case ":"

                                .Canal = "[Commerce]"
                                .Couleur = Color.Sienna

                            Case "e" ' Evenement

                                .Canal = "[Evenement]"
                                .Couleur = Color.HotPink

                        End Select

                        .Id_Joueur = separateData(1)
                        .Nom_Joueur = separateData(2)
                        .Message = separateData(3) 'AsciiDecoder(separateData(3))
                        .Item = separateData(4)

                        .Heure = TimeOfDay

                    End With

                    .Tchat.Message = newInformation

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage & "_" & .Personnage.Serveur, "Tchat_Message", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

        Sub Erreur(data As String)

            With Bot

                'cME X    
                'cME Canal

                Try

                    Dim newInformation As New Tchat_Variable.Information

                    With newInformation

                        Select Case Mid(data, 4, 1)

                            Case "A" ' cMEA

                                .Canal = "[Alignement]"
                                .Message = "Impossible d'utilise ce canal."

                            Case "f" ' cMEf

                                .Canal = "[Dofus]"
                                .Message = "Le joueur " & Mid(data, 5) & " n'est pas connecté."

                        End Select

                        .Couleur = Color.Red
                        .Heure = TimeOfDay

                    End With

                    .Tchat.Message = newInformation

                Catch ex As Exception

                    ErreurFichier(.Personnage.NomDuPersonnage & "_" & .Personnage.Serveur, "Tchat_Erreur", data & vbCrLf & ex.Message)

                End Try

            End With

        End Sub

    End Module

End Namespace