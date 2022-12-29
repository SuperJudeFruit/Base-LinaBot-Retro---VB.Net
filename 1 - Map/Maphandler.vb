Public Module Maphandler

    Private HEX_CHARS() As String = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F"}
    Private ZKARRAY As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_"

    Private Function Unescape(StringToDecode As String) As String
        Return Web.HttpUtility.UrlDecode(StringToDecode)
    End Function

    Public Function PrepareKey(key As String)

        Dim d As String = key

        Dim _loc3 As String = ""
        Dim _loc4 As Integer = 0

        While _loc4 < d.Length

            _loc3 += Chr(Convert.ToInt64(d.Substring(_loc4, 2), 16))
            _loc4 += 2
        End While

        _loc3 = Unescape(_loc3)

        Return _loc3

    End Function

    Public Function DecypherData(d As String, k As String, c As Integer)

        Dim _loc5 As String = ""
        Dim _loc6 As Integer = k.Length
        Dim _loc7 As Integer = 0
        Dim _loc9 As Integer = 0

        While _loc9 < d.Length
            Dim a As Integer = Convert.ToInt64(d.Substring(_loc9, 2), 16)
            Dim b As Integer = Asc(k.Substring((_loc7 + c) Mod _loc6, 1))
            _loc5 &= Chr(a Xor b)
            _loc7 += 1
            _loc9 += 2
        End While

        _loc5 = Unescape(_loc5)

        Return _loc5

    End Function

    Public Function Checksum(s As String)

        Dim _loc3 As String = 0
        Dim _loc4 As String = 0

        While _loc4 < s.Length
            _loc3 += Asc(s.Substring(_loc4, 1)) Mod 16
            _loc4 += 1
        End While

        Return HEX_CHARS(_loc3 Mod 16)

    End Function

    Private Function HashCodes(a As String)
        Return ZKARRAY.IndexOf(a)
    End Function

    Public Structure Cell

        Dim movement As Integer
        Dim groundLevel As Integer
        Dim groundSlope As Integer
        Dim layerGroundRot As Integer
        Dim layerGroundNum As Integer
        Dim layerObject1Num As Integer
        Dim layerObject2Num As Integer
        Dim layerObject1Rot As Integer

        Dim active As Boolean 'Cellule active ou non (ou on peut marcher)
        Dim lineOfSight As Boolean
        Dim layerGroundFlip As Boolean
        Dim layerObject1Flip As Boolean
        Dim layerObject2Flip As Boolean
        Dim layerObject2Interactive As Boolean

    End Structure

    Private Function UncompressCell(sData As String) As Cell

        Dim Cellule As Cell
        Dim sDataLenght As Integer = sData.Length - 1
        Dim Numero(5000) As Integer

        While (sDataLenght >= 0)
            Numero(sDataLenght) = HashCodes(sData(sDataLenght))
            sDataLenght -= 1
        End While

        Cellule.active = (Numero(0) And 32) >> 5
        Cellule.lineOfSight = Numero(0) And 1
        Cellule.layerGroundRot = (Numero(1) And 48) >> 4
        Cellule.groundLevel = Numero(1) And 15
        Cellule.movement = (Numero(2) And 56) >> 3
        Cellule.layerGroundNum = ((Numero(0) And 24) << 6) + ((Numero(2) And 7) << 6) + Numero(3)
        Cellule.groundSlope = (Numero(4) And 60) >> 2
        Cellule.layerGroundFlip = (Numero(4) And 2) >> 1
        Cellule.layerObject1Num = ((Numero(0) And 4) << 11) + ((Numero(4) And 1) << 12) + (Numero(5) << 6) + Numero(6)
        Cellule.layerObject1Rot = (Numero(7) And 48) >> 4
        Cellule.layerObject1Flip = (Numero(7) And 8) >> 3
        Cellule.layerObject2Flip = (Numero(7) And 4) >> 2
        Cellule.layerObject2Interactive = (Numero(7) And 2) >> 1
        Cellule.layerObject2Num = ((Numero(0) And 2) << 12) + ((Numero(7) And 1) << 12) + (Numero(8) << 6) + Numero(9)

        Return Cellule

    End Function

    Public Function UncompressMap(sData As String) As Cell()

        Dim Cellule(1024) As Cell
        Dim Fin As Integer = sData.Length
        Dim Numero As Integer = 0
        Dim Debut As Integer = 0

        While Debut < Fin
            Cellule(Numero) = UncompressCell(sData.Substring(Debut, 10))
            Debut += 10
            Numero += 1
        End While

        Return Cellule

    End Function

End Module
