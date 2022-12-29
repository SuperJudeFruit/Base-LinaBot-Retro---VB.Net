Namespace Dragodinde_Variable

    Public Class Base

        Public Monter As Boolean = False
        Public Xp As Integer = -1
        Public Inventaire As Boolean = False
        Public Enclos As New Dictionary(Of Integer, Information)
        Public Etable As New Dictionary(Of Integer, Information)
        Public Equiper As New Information
        Public Information As New Information

    End Class

    Public Class Information

        Public Pods As New MinMax

        Public Experience As New MinMax

        Public Capacite As New Capaciter

        Public IdUnique As Integer = -1
        Public ID As Integer = -1
        Public ArbreGenealogique As String = ""
        Public Nom As String = ""
        Public Type As String = ""
        Public Sexe As String = ""
        Public Niveau As Integer = -1
        Public Montable As Boolean = False

        Public Etat As New Etat
        Public Fecondation As New Fecondation

        Public Endurance As New MinMax
        Public Maturite As New MinMax
        Public Amour As New MinMax
        Public Energie As New MinMax
        Public Fatigue As New MinMax
        Public Reproduction As New MinMax

        Public Caracteristique As New Item_Variable.Caracteristique

    End Class

    Public Class Capaciter

        Public Primaire As String = ""
        Public Secondaire As String = ""

    End Class

    Public Class Etat

        Public Agressiviter As Integer = -1
        Public Equilibrer As Integer = -1
        Public Sereniter As Integer = -1

    End Class

    Public Class Fecondation

        Public Heure As Integer = -1
        Public Fecondable As Boolean = False

    End Class

End Namespace