Namespace Map_Variable

    Public Class Base

        Public Largeur As Integer
        Public Hauteur As Integer
        Public Handler(1280) As Cell
        Public PathTotal As String
        Public ID As Integer
        Public StopDeplacement As Boolean
        Public Haut, Bas, Gauche, Droite As Integer
        Public Deplacement As Boolean
        Public Coordonnees As String
        Public Spectateur As Boolean
        Public Entite As New Dictionary(Of Integer, Entite)
        Public Objet As New Dictionary(Of Integer, Objet)
        Public Interaction As New Dictionary(Of Integer, Interaction_Variable.Base)
        Public Bloque As Threading.ManualResetEvent = New Threading.ManualResetEvent(False)

    End Class

    Public Class Objet

        Public Cellule As Integer
        Public Id As Integer
        Public IdUnique As Integer
        Public Nom As String
        Public Resistance As New MinMax


    End Class

    Public Class Entite

        Public Categorie As Integer
        Public IDUnique As Integer
        Public Cellule As Integer
        Public Nom As String
        Public Niveau As String
        Public ID As String
        Public Etoile As Integer
        Public Classe As String
        Public Sexe As String
        Public Guilde As String
        Public ModeMarchand As Boolean
        Public Alignement As String
        Public Orientation As Boolean
        Public Assis As Boolean
        Public Equipement As New Equipement

    End Class

    Public Class Equipement

        Public Chapeau As New Information
        Public Cape As New Information
        Public Cac, Familier, Bouclier As Integer

    End Class

    Public Class Information

        Public ID As Integer
        Public Niveau As Integer
        Public Forme As Integer

    End Class


End Namespace