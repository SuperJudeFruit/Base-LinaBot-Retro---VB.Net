Namespace Caracteristique_Variable

    Public Class Base

        Public Event EventCaracteristique(Choix As String, value As Object)

        Dim _Avancee As New Caracteristiques
        Public Property Avancee As Caracteristiques

            Get

                Return _Avancee

            End Get

            Set(value As Caracteristiques)

                _Avancee = value
                RaiseEvent EventCaracteristique("Avancee", _Avancee)

            End Set

        End Property


        Dim _Capital As Integer = 0
        Public Property Capital As Integer

            Get

                Return _Capital

            End Get

            Set(value As Integer)

                _Capital = value
                RaiseEvent EventCaracteristique("Capital", _Capital)

            End Set

        End Property

    End Class

    Public Class Caracteristiques

        Public Primaire As New Primaire

        Public Bonus As New Bonus

        Public Esquive As New Esquive

        Public Resistance As New Resistance

    End Class

    Public Class Primaire

        Public Vitalite As New Statistique
        Public Sagesse As New Statistique
        Public Force As New Statistique
        Public Intelligence As New Statistique
        Public Chance As New Statistique
        Public Agilite As New Statistique
        Public PA As New Statistique
        Public PM As New Statistique
        Public Initiative As New Statistique
        Public Prospection As New Statistique
        Public Portee As New Statistique
        Public Maximum_De_Creatures_Invocables As New Statistique

    End Class

    Public Class Bonus

        Public Degats As New Statistique
        Public Degats_Physiques As New Statistique
        Public Maitrise_Arme As New Statistique
        Public Dommages_PR As New Statistique
        Public Pieges As New Statistique
        Public Pieges_PR As New Statistique
        Public Soins As New Statistique
        Public Renvoi_De_Dommages As New Statistique
        Public Coups_Critiques As New Statistique
        Public Echecs_Critiques As New Statistique

    End Class

    Public Class Esquive

        Public PA As New Statistique
        Public PM As New Statistique

    End Class

    Public Class Statistique

        Public Base As String = ""
        Public Equipement As String = ""
        Public Dons As String = ""
        Public Boost As String = ""
        Public Total As String = ""

    End Class

#Region "Resistance"


    Public Class Resistance

        Public Combat As New Combat
        Public PvP As New PvP

    End Class

    Public Class PvP

        Public Neutre As New Fixe_Pr
        Public Terre As New Fixe_Pr
        Public Feu As New Fixe_Pr
        Public Eau As New Fixe_Pr
        Public Air As New Fixe_Pr

    End Class

    Public Class Combat

        Public Neutre As New Fixe_Pr
        Public Terre As New Fixe_Pr
        Public Feu As New Fixe_Pr
        Public Eau As New Fixe_Pr
        Public Air As New Fixe_Pr

    End Class

    Public Class Fixe_Pr

        Public Pourcentage As New Statistique
        Public Fixe As New Statistique

    End Class

#End Region

End Namespace