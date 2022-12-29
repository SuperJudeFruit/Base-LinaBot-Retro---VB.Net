Namespace Maison_Variable

    Public Class Base

        Public Ouverture As Boolean = False
        Public Personnelle As New Maison
        Public Map As New Dictionary(Of String, Maison)

    End Class

    Public Class Maison

        Public Proprietaire As String = ""
        Public ID As Integer = -1
        Public Verouiller As Boolean = False
        Public Vente As Boolean = False
        Public Guilde As Boolean = False
        Public Nom_Guilde As String = ""
        Public Cellule As Integer = -1
        Public MapID As Integer = -1
        Public Coordonnees As String = ""
        Public Prix As Integer = -1
        Public Code As Integer = -1
        Public Coffre As New Dictionary(Of String, Coffre)


    End Class

    Public Class Coffre

        Public Verouiller As Boolean = False
        Public Cellule As Integer = -1
        Public MapID As Integer = -1
        Public Coordonnees As String = ""
        Public Code As Integer = -1

    End Class

End Namespace