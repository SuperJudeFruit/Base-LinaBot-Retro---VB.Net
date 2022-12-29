Namespace Groupe_Variable

    Public Class Base

        Public Groupe As Boolean = False
        Public Invitation As Boolean = False
        Public Inviteur As String = ""
        Public Inviter As String = ""
        Public Membre As New Dictionary(Of Integer, Information)
        Public ID As Integer = -1
        Public Coordonnees As String = ""
        Public Chef As Integer = -1

    End Class

    Public Class Information

        Public ID As Integer = -1
        Public Nom As String = ""
        Public ClasseSexe As Integer = -1
        Public Couleur1 As String = ""
        Public Couleur2 As String = ""
        Public Couleur3 As String = ""

        Public Equipement As New Map_Variable.Equipement

        Public Vitaliter As New MinMax

        Public Niveau As Integer = -1
        Public Initiative As Integer = -1
        Public Prospection As Integer = -1

    End Class

End Namespace