Namespace Combat_Variable

    Public Class Base

        Public Preparation As Boolean = False
        Public Placement As Boolean = False
        Public Combat As Boolean = False
        Public Defi As Boolean = False

        Public Spectateur As Boolean = False
        Public Cadenas As Boolean = False
        Public Aide As Boolean = False
        Public Groupe As Boolean = False

        Public MonTour As Boolean = False
        Public Echec As Boolean = False
        Public Pause As Integer = -1

        Public Lancer As New Dictionary(Of Integer, List(Of Lancer))
        Public Entite As New Dictionary(Of Integer, Entite)
        Public Challenge As New Dictionary(Of Integer, Challenge)
        Public Placement_Cellule As New Dictionary(Of Integer, List(Of Integer))

    End Class

    Public Class Lancer

        Public idUnique As Integer = -1
        Public IdNom As String = ""
        Public Niveau As Integer = -1

    End Class

    Public Class Challenge

        Public ID As Integer = -1
        Public Nom As String = ""
        Public Rate As Boolean = False
        Public Xp As Integer = -1
        Public Xp_Groupe As Integer = -1
        Public Butin As Integer = -1
        Public Butin_Groupe As Integer = -1

    End Class

    Public Class Entite

        Public Pret As Boolean = False
        Public OrdreTour As Integer = -1
        Public Vivant As Boolean = False
        Public Vitalite As Integer = -1
        Public PA As Integer = -1
        Public PM As Integer = -1

        Public Resistance As New Resistance

        Public Esquive As New Esquive

        Public NumeroTour As Integer = -1
        Public Etat As String = ""
        Public Equipe As Integer = -1

    End Class

    Public Class Esquive

        Public PA As Integer = 0
        Public PM As Integer = 0

    End Class

    Public Class Resistance

        Public Eau As Integer = 0
        Public Feu As Integer = 0
        Public Terre As Integer = 0
        Public Air As Integer = 0
        Public Neutre As Integer = 0

    End Class

    Public Class Drop

        Public Kamas As Integer = -1
        Public Gagne As Integer = -1
        Public Perdu As Integer = -1
        Public Item As New Dictionary(Of Integer, Integer)

    End Class

End Namespace