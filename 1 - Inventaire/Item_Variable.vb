Imports Dofus_RetroBot.Caracteristique_Variable

Namespace Item_Variable

    Public Class Base

        Public Event EventItem(Choix As String, value As Object)

        Dim _Item As New Dictionary(Of Integer, Information)

        Public ReadOnly Property Item(Optional IdUnique As String = "") As Object

            Get

                If IdUnique = "" Then

                    Return _Item

                Else

                    Return _Item(IdUnique)

                End If

            End Get

        End Property

        Public Function Existe(ID As String) As Boolean

            Return _Item.ContainsKey(ID)

        End Function

        Public WriteOnly Property Ajoute As Information

            Set(value As Information)

                _Item.Add(value.IdUnique, value)
                RaiseEvent EventItem("Ajoute", value)

            End Set

        End Property

        Public WriteOnly Property Supprimer As Integer

            Set(value As Integer)

                _Item.Remove(value)
                RaiseEvent EventItem("Supprimer", value)

            End Set

        End Property

        Public WriteOnly Property Modifie As Information

            Set(value As Information)

                _Item(value.IdUnique) = value
                RaiseEvent EventItem("Modifie", value)

            End Set

        End Property

        Public Sub Reset()

            _Item = New Dictionary(Of Integer, Information)

        End Sub

    End Class

    Public Class Information

        Public IdObjet As Integer = 0
        Public IdUnique As Integer = 0
        Public Nom As String = ""
        Public Quantiter As Integer = 0
        Public Caracteristique As New Caracteristique
        Public CaracteristiqueBrute As String = ""
        Public Equipement As String = ""
        Public Categorie As Integer = 0
        Public Description As String = ""

    End Class

    Public Class Caracteristique

        Public Vole As New Vole

        Public Dommage As New Dommage

        Public Piege As New Fixe_Pr

        Public Resistance As New Resistance

        Public Familier As New Familier

        Public Dragodinde As New Dragodinde

        Public PertePA As String = "0"
        Public Pdv As String = "0"
        Public Force As String = "0"
        Public Vitalite As String = "0"
        Public Sagesse As String = "0"
        Public Intelligence As String = "0"
        Public Chance As String = "0"
        Public Agilite As String = "0"
        Public PA As String = "0"
        Public PM As String = "0"
        Public PO As String = "0"
        Public Invocation As String = "0"
        Public Initiative As String = "0"
        Public Prospection As String = "0"
        Public Pods As String = "0"
        Public CC As String = "0"


        Public Soin As String = "0"

        Public Cac As String = "0"
        Public PierreAme As List(Of String)
        Public Puissance As String = "0"

        Public ResistanceItem As String = "0"

        Public Etheree As String = "0"
        Public TourRestant As String = 0

    End Class

    Public Class Vole

        Public Eau As String = "0"
        Public Feu As String = "0"
        Public Terre As String = "0"
        Public Air As String = "0"
        Public Neutre As String = "0"

    End Class

    Public Class Dommage

        Public Eau As String = "0"
        Public Feu As String = "0"
        Public Terre As String = "0"
        Public Air As String = "0"
        Public Neutre As String = "0"
        Public Physique As New Fixe_Pr

    End Class

    Public Class Resistance

        Public Neutre As New Fixe_Pr
        Public Terre As New Fixe_Pr
        Public Eau As New Fixe_Pr
        Public Feu As New Fixe_Pr
        Public Air As New Fixe_Pr

    End Class

    Public Class Fixe_Pr

        Public Pourcentage As New Integer
        Public Fixe As New Integer

    End Class

    Public Class Dragodinde

        Public [Date] As String = ""
        Public IdUnique As String = ""
        Public Possesseur As String = ""
        Public Nom As String = ""
        Public Parchemin As Date = TimeOfDay

    End Class

    Public Class Familier

        Public Pdv As String = "0"
        Public Repas As Integer = 0
        Public Corpulence As String = "Normal"
        Public Repas_Dernier As String = "Aliment Inconnu"
        Public Repas_Date As Date = TimeOfDay
        Public Repas_Prochain As Date = TimeOfDay
        Public Capacite_Accrue As String = "0"

    End Class

    Public Class Bonus

        Public NumeroPanoplie As Integer = -1
        Public IDObjet As String()
        Public Caracteristique As New Caracteristique
        Public CaracteristiqueBrute As String = ""

    End Class

End Namespace