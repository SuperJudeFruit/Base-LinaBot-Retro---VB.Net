#Region "Personnage"


Public Class MinMax

    Public Minimum As Integer = 0
    Public Maximum As Integer = 0
    Public Actuelle As Integer = 0
    Public Pourcentage As Integer = 0

End Class


#End Region


Public Class ClassServeur

    Public Nom As String
    Public IP As String
    Public Port As Integer
    Public ID As String

End Class

Public Class ClassProxy

    Public Active As Boolean
    Public IP As String
    Public Port As String
    Public Identifiant As String
    Public MotDePasse As String

End Class