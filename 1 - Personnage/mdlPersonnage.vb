Module mdlPersonnage

    Sub GiPersonnageNiveauUp(data As String)

        With Bot

            Try

                ' AN 2
                ' AN Niveau

                .Personnage.Niveau = Mid(data, 3)

                EcritureMessage("[Dofus]", "Tu passes niveau " & Mid(data, 3) & vbCrLf & "Tu gagnes 5 points pour faire évoluer tes caractéristiques et 1 point pour tes sorts.", Color.Green)

            Catch ex As Exception

                ErreurFichier(.Personnage.NomDuPersonnage, "GiPersonnageNiveauUp", data & vbCrLf & ex.Message)

            End Try

        End With

    End Sub

    Sub GiPersonnageInformation(data As String)

        With Bot

            Try

                ' ASK | 1234567   | Linaculer  | 99    | 9         | 0       | 90            | -1       | -1       | -1       | 262c1bc        ~ 241      ~ 1         ~ 1                 ~ 64#2#4#0#1d3+1  , 7d#1#0#0#0d0+1 ; Next Item
                ' ASK | ID Joueur | Nom Joueur | Level | Id Classe | Id Sexe | Classe + Sexe | Couleur1 | Couleur2 | Couleur3 | Id Unique Item ~ Id Objet ~ Quantity  ~ Number equipment  ~ Caractéristique , Caract Next    ; Item suivent

                Dim separateData As String() = Split(data, "|")

                .Connexion.Connexion = False
                .Connexion.Connecter = True

                With .Personnage

                    .ID = separateData(1)
                    .NomDuPersonnage = separateData(2)
                    .Niveau = separateData(3)
                    .Classe = separateData(4)
                    .Sexe = separateData(5)
                    .ClasseSexe = separateData(6)
                    .Couleur1 = "&H" & separateData(7)
                    .Couleur2 = "&H" & separateData(8)
                    .Couleur3 = "&H" & separateData(9)

                End With

                ' Form_LinaBot.TreeView_SelectedImageKey(index, separateData(6))

                EcritureMessage("[Dofus]", "Connecté au personnage '" & separateData(2) & "' (Niveau : " & separateData(3) & ")", Color.Green)
                EcritureMessage("[Dofus]", "Réception de l'inventaire.", Color.Green)

            Catch ex As Exception

                ErreurFichier(.Personnage.NomDuPersonnage, "GiPersonnageInformation", data & vbCrLf & ex.Message)

            End Try

        End With

    End Sub

End Module


Namespace nsPersonnage

    Public Class Personnage

        Public Event RaisePersonnage(Choix As String, value As Object)

        Public NomDeCompte As String
        Public MotDePasse As String
        Public NomDuPersonnage As String
        Public Serveur As String



        Public Alignement As String = ""
        Public ID As Integer
        Public Pseudo As String = ""
        Public QuestionSecrete As String = ""
        Public Abonnement As Date = TimeOfDay
        Public Ticket As String
        Public Niveau As Integer
        Public Classe As Integer
        Public ClasseSexe As Integer
        Public Sexe As Integer
        Public IDServeur As Integer
        Public Kamas As Integer
        Public Regeneration As Integer
        Public Capital_Sort As Integer
        Public Vivant As Boolean = True
        Public EnInteraction As Boolean
        Public Couleur1, Couleur2, Couleur3 As String
        Public InteractionCellule As Integer
        Public Pods As New MinMax

        Public Cadeau As New Dictionary(Of String, Item_Variable.Information)

        Public Equipement As New Map_Variable.Equipement

        Dim _Experience As New MinMax
        Public Property Experience As MinMax

            Get

                Return _Experience

            End Get

            Set(value As MinMax)

                _Experience = value
                RaiseEvent RaisePersonnage("Experience", value)

            End Set

        End Property

        Dim _Energie As New MinMax
        Public Property Energie As MinMax

            Get

                Return _Energie

            End Get

            Set(value As MinMax)

                _Energie = value
                RaiseEvent RaisePersonnage("Energie", value)

            End Set

        End Property

        Dim _Vitaliter As New MinMax
        Public Property Vitaliter As MinMax

            Get

                Return _Vitaliter

            End Get

            Set(value As MinMax)

                _Vitaliter = value
                RaiseEvent RaisePersonnage("Vitaliter", value)

            End Set

        End Property

    End Class

    Public Class CStatut

        Public Etat As String = ""
        Public Couleur As Color

    End Class


End Namespace