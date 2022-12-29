Namespace Pnj_Variable

    Public Class Base

        Public Parler, Acheter, Vendre, AcheterVendre, Echange As Boolean
        Public Reponse As New List(Of Integer)
        Public IdReponse As Integer
        Public Hdv_Acheter As New HotelDeVente
        Public Hdv_Vendre As New HotelDeVente
        Public AcheterVendre_Item As New Dictionary(Of Integer, Acheter_Vendre)

    End Class

    Public Class Acheter_Vendre

        Public ID As Integer
        Public Caracteristique As New Item_Variable.Caracteristique
        Public CaracteristiqueBrute As String
        Public Prix As Integer

    End Class

    Public Class HotelDeVente

        Public Quantiter As New Quantiter
        Public Liste As New Liste
        Public Taxe As Integer
        Public NiveauMax As Integer
        Public StockEnMagasin As Integer
        Public HeureMax As Integer
        Public PrixMoyen As Integer

    End Class

    Public Class Liste

        Public Item As New Dictionary(Of Integer, Item)
        Public Categorie As New List(Of Integer)
        Public ID As New List(Of Integer)

    End Class

    Public Class Quantiter

        Public x1 As Boolean = False
        Public x10 As Boolean = False
        Public x100 As Boolean = False

    End Class

    Public Class Prix

        Public Actuelle As Integer = 0
        Public x1 As Integer = 0
        Public x10 As Integer = 0
        Public x100 As Integer = 0

    End Class

    Public Class Item

        Public IDUnique As Integer
        Public Nom As String
        Public Caracteristique As New Item_Variable.Caracteristique
        Public Prix As New Prix
        Public IdObjet As Integer
        Public TempsRestant As Integer
        Public Quantiter As Integer

    End Class

End Namespace