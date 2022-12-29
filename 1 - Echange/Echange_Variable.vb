Namespace Echange_Variable

    Public Class Base

        Public Interaction As String = ""
        Public Echange As Boolean = False
        Public Invitation As Boolean = False
        Public ID As Integer = -1
        Public Moi As New Information
        Public Lui As New Information

    End Class

    Public Class Information

        Public Inventaire As New Item_Variable.Base
        Public Kamas As Integer = -1
        Public Valider As Boolean = False

    End Class

End Namespace
