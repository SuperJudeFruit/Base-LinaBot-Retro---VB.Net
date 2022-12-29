Public Module Fonctions

    'Tout refaire avec explication + simplification du code.

    Public Function DistanceLa(ByVal pos1 As Integer, ByVal pos2 As Integer, ByVal MapLargeur As Integer) As Double

        Dim num4 As Decimal = Decimal.op_Decrement(Math.Ceiling(CDec(pos1 / ((MapLargeur * 2) - 1))))
        Dim num12 As Decimal = Decimal.op_Decrement(Math.Ceiling(CDec(pos2 / ((15 * 2) - 1))))
        Dim num15 As Decimal = num12 - Decimal.op_Modulus(pos2 - (num12 * ((15 * 2) - 1)), 15)
        Return Math.Sqrt(Math.Pow(Convert.ToDouble(pos2 - ((15 - 1) * num15 / 15) - (pos1 - ((MapLargeur - 1) * (num4 - Decimal.op_Modulus(pos1 - (num4 * ((MapLargeur * 2) - 1)), MapLargeur)))) / MapLargeur), 2) + Math.Pow(Convert.ToDouble(num15 - (num4 - Decimal.op_Modulus(pos1 - (num4 * ((MapLargeur * 2) - 1)), MapLargeur))), 2))

    End Function

    Public Function Distance2(ByVal pos1 As Integer, ByVal pos2 As Integer, ByVal MapLargeur As Integer) As Double
        Dim num18 As Double
        Dim num As Integer = pos1
        Dim num2 As Integer = MapLargeur
        Dim d As Decimal = num / ((num2 * 2) - 1)
        Dim num4 As Decimal = Decimal.op_Decrement(Math.Ceiling(d))
        Dim num5 As Decimal = num - (num4 * ((num2 * 2) - 1))
        Dim num6 As Decimal = Decimal.op_Modulus(num5, num2)
        Dim num7 As Decimal = num4 - num6
        Dim num8 As Decimal = (num - ((num2 - 1) * num7)) / num2
        Dim num9 As Integer = pos2
        Dim num10 As Integer = 15
        Dim num11 As Decimal = num9 / ((num10 * 2) - 1)
        Dim num12 As Decimal = Decimal.op_Decrement(Math.Ceiling(num11))
        Dim num13 As Decimal = num9 - (num12 * ((num10 * 2) - 1))
        Dim num14 As Decimal = Decimal.op_Modulus(num13, num10)
        Dim num15 As Decimal = num12 - num14
        Dim num16 As Decimal = (num9 - ((num10 - 1) * num15)) / num10
        num18 = Math.Sqrt(Math.Pow(Convert.ToDouble(num16 - num8), 2) + Math.Pow(Convert.ToDouble(num15 - num7), 2))
        Return num18
    End Function

    Private Class loc8
        Public y As Integer = 0
        Public x As Integer = 0
    End Class

    Public Function getX(ByVal laCase As Integer, ByVal MapLargeur As Integer)
        Try
            Dim _loc4 = MapLargeur
            Dim _loc5 = Math.Floor(laCase / (_loc4 * 2 - 1))
            Dim _loc6 = laCase - _loc5 * (_loc4 * 2 - 1)
            Dim _loc7 = _loc6 Mod _loc4
            Dim _loc8 As New loc8

            Dim y As Integer = _loc5 - _loc7
            Dim x As Integer = (laCase - (_loc4 - 1) * y) / _loc4
            Return x
        Catch ex As Exception

        End Try
        Return 0
    End Function

    Public Function getY(ByVal laCase As Integer, ByVal MapLargeur As Integer)
        Try
            Dim _loc4 = MapLargeur
            Dim _loc5 = Math.Floor(laCase / (_loc4 * 2 - 1))
            Dim _loc6 = laCase - _loc5 * (_loc4 * 2 - 1)
            Dim _loc7 = _loc6 Mod _loc4
            Dim _loc8 As New loc8
            Dim y As Integer = _loc5 - _loc7
            Dim x As Integer = (laCase - (_loc4 - 1) * y) / _loc4
            Return y
        Catch ex As Exception

        End Try
        Return 0
    End Function

    Public Function goalDistance(ByVal pos1 As Integer, ByVal pos2 As Integer, ByVal MapLargeur As Integer) As Integer
        Dim _loc7 = Math.Abs(getX(pos1, MapLargeur) - getX(pos2, MapLargeur))
        Dim _loc8 = Math.Abs(getY(pos1, MapLargeur) - getY(pos2, MapLargeur))
        Return _loc7 + _loc8
    End Function

    Public Function ReturnLastCell(ByVal code As String) As Integer
        Try
            Dim Number As Integer = 0
            Dim hash() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "-", "_"}
            Dim hash2() As String = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
            Dim i As Integer = 0
            For i = 0 To hash2.Length - 1
                Dim j As Integer = 0
                For j = 0 To hash.Length - 1
                    If hash2(i) & hash(j) = code Then Return Number
                    Number += 1
                Next
            Next i
        Catch ex As Exception
        End Try
        Return 0
    End Function

End Module
