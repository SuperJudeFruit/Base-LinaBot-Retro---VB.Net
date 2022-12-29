Namespace Metier_Variable

    Public Class Base


        Public Nom As String = ""
        Public ID As Integer = -1
        Public Niveau As Integer = -1
        Public ItemEquipe As Boolean = False
        Public Experience As New MinMax
        Public Action As String = ""
        Public NeFournitAucuneRessource As Boolean = False
        Public GratuitSurEchec As Boolean = False
        Public Payant As Boolean = False
        Public NombreIngredientMinimum As Integer = -1
        Public ModePublic As Boolean = False

        Public Atelier As New Dictionary(Of Integer, Atelier)

    End Class

    Public Class Atelier

        Public Nom As String = ""
        Public ID As Integer = -1
        Public Action As String = ""
        Public NombreCaseRecolte As New MinMax
        Public TempsReussite As Integer = -1

    End Class
End Namespace
