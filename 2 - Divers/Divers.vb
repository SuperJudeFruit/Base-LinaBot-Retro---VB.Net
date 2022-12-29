Imports System.Net.Sockets, System.Net, Org.Mentalis.Network.ProxySocket
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Module Divers

    Private Delegate Sub dlgDivers()
    Private Delegate Function dlgFDivers()



    Public Function ChiffreSeparation(chiffre As String, separateur As String) As String

        Dim resultat As String = ""
        chiffre = StrReverse(chiffre)

        Try

            For i = 1 To chiffre.Length Step 3

                resultat &= Mid(chiffre, i, 3) & separateur

            Next

        Catch ex As Exception

        End Try

        Return Mid(StrReverse(resultat), 2)

    End Function

    Public Sub ErreurFichier(ByVal nomJoueur As String, ByVal nomErreur As String, ByVal erreur As String)

        Try

            EcritureMessage("[Erreur]", "Une erreur est survenue, veuillez envoyez les fichiers qui se trouve dans le dossier 'Erreur' à l'administrateur.", Color.Red)

            'Si le dossier erreur n'existe pas, alors je le créer
            If Not IO.Directory.Exists(Application.StartupPath & "\AllErreur") Then IO.Directory.CreateDirectory(Application.StartupPath & "\AllErreur")

            'J'ouvre le fichier pour y écrire se que je souhaite
            Dim swEcriture As New IO.StreamWriter(Application.StartupPath + "\AllErreur/" & nomJoueur & "_" & nomErreur & ".txt")

            swEcriture.WriteLine(erreur)

            'Puis je le ferme.
            swEcriture.Close()

        Catch ex As Exception

        End Try

    End Sub

    Public Sub EcritureMessage(indice As String, message As String, couleur As Color)

        With Bot

            Try

                If LinaBot.InvokeRequired Then

                    LinaBot.Invoke(New dlgDivers(Sub() EcritureMessage(indice, message, couleur)))

                Else


                    If .Tchat.Message.Count > 500 Then

                        .Tchat.Message.Clear()

                    End If

                    Dim newTchat As New Tchat_Variable.Information

                    With newTchat

                        .Nom_Joueur = ""
                        .Item = ""
                        .Id_Joueur = 0
                        .Canal = indice
                        .Message = message
                        .Couleur = couleur
                        .Heure = TimeOfDay

                    End With

                    '   With .FrmUser.RichTextBox_Tchat

                    '       .SelectionColor = couleur
                    '       .AppendText("[" & TimeOfDay & "] " & indice & " " & message & vbCrLf)
                    '       .ScrollToCaret()

                    '   End With

                    .Tchat.Message = newTchat

                End If

            Catch ex As Exception

            End Try

        End With

    End Sub

    Public Sub EcritureMessageSocket(indice As String, message As String, couleur As Color)

        With Bot


            Try



                If LinaBot.InvokeRequired Then

                    LinaBot.Invoke(New dlgDivers(Sub() EcritureMessageSocket(indice, message, couleur)))

                Else

                    Dim newInformations As New Tchat_Variable.Information

                    With newInformations

                        .Canal = indice
                        .Couleur = couleur
                        .Message = message
                        .Heure = TimeOfDay

                    End With

                    .Console.Message = newInformations

                End If

            Catch ex As Exception

            End Try

        End With

    End Sub

    Public Function AsciiDecoder(msg As String) As String

        Dim msgFinal As String = ""

        Try

            Dim iMax As Integer = msg.Length
            Dim i As Integer = 0
            While (i < iMax)
                Dim caractC As Char = msg(i)
                Dim caractI As Integer = Asc(caractC)
                Dim nbLettreCode As Integer = 1
                If (caractI And 128) = 0 Then
                    msgFinal &= ChrW(caractI)
                Else
                    Dim temp As Integer = 64
                    Dim codecPremLettre As Integer = 63
                    While (caractI And temp)
                        temp >>= 1
                        codecPremLettre = codecPremLettre Xor temp
                        nbLettreCode += 1
                    End While
                    codecPremLettre = codecPremLettre And 255
                    Dim caractIFinal As Integer = caractI And codecPremLettre
                    nbLettreCode -= 1
                    i += 1
                    While (nbLettreCode <> 0)
                        caractC = msg(i)
                        caractI = Asc(caractC)
                        caractIFinal <<= 6
                        caractIFinal = caractIFinal Or (caractI And 63)
                        nbLettreCode -= 1
                        i += 1
                    End While
                    msgFinal &= ChrW(caractIFinal)
                End If
                i += nbLettreCode
            End While

        Catch ex As Exception

        End Try


        Return msgFinal.Replace("%27", "'").Replace("%C3%A9", "é").Replace("%2C", ",").Replace("%3F", "?").Replace("%C3%A8", "é").Replace("%29", "]").Replace("%28", "[").Replace("%E2%80%99", "'")

    End Function 'Provient de Maxoubot.

    Public Function ProxySocketUtilisateur(ipAnkama As String, portAnkama As Integer, proxyIP As String, proxyPort As Integer, nomUtilisateur As String, motDePasse As String) As Socket

        Dim monProxy As New ProxySocket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) With
            {
                .ProxyEndPoint = New IPEndPoint(IPAddress.Parse(proxyIP), proxyPort)
            }

        If nomUtilisateur <> "" Then

            monProxy.ProxyUser = nomUtilisateur
            monProxy.ProxyPass = motDePasse

        End If

        monProxy.ProxyType = ProxyTypes.Socks5

        monProxy.Connect(ipAnkama, portAnkama)

        Return monProxy

    End Function


    ''' <summary>
    ''' Retourne l'ID ou la categorie de l'item.
    ''' </summary>
    ''' <param name="index">Indique le numéro du bot.</param>
    ''' <param name="nomID">Le nom de l'item ou son ID.</param>
    ''' <param name="choix">l'un des choix suivant : <br/>
    ''' ID = Retourne l'ID de l'item.
    ''' Categorie = Retourne la categorie de l'item.</param>
    ''' <returns>
    ''' Retourne l'ID ou la categorie selon le nom ou l'ID de l'item.
    ''' </returns>
    Public Function RetourneItemNomIdCategorie(nomID As String, choix As String) As String

        With Bot

            For Each pair As sItems In VarItems.Values

                If pair.Nom.ToLower = nomID.ToLower OrElse pair.ID = nomID Then

                    Select Case choix.ToLower

                        Case "id"

                            Return pair.ID

                        Case "categorie", "categori"

                            Return pair.Catégorie

                    End Select

                End If

            Next

        End With

        Return ""

    End Function

End Module
