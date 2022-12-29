Namespace [Option]

    Namespace Charger

        Module Charger

            Public Sub Initialiser()

                If IO.File.Exists(Application.StartupPath + "Compte\Option\" & Bot.Personnage.NomDuPersonnage & "_" & Bot.Personnage.Serveur & ".txt") Then

                    Dim swLecture As New IO.StreamReader(Application.StartupPath + "Compte\Option\" & Bot.Personnage.NomDuPersonnage & "_" & Bot.Personnage.Serveur & ".txt")

                    Do Until swLecture.EndOfStream

                        Dim Ligne As String = swLecture.ReadLine

                        If Ligne <> "" Then

                            Dim separation As String() = Split(Ligne, "|")

                            Select Case separation(0)

                                Case "DLL"

                                    Dll(separation(1))

                            End Select

                        End If

                    Loop

                    swLecture.Close()

                End If

            End Sub

            Private Sub Dll(Ligne As String)

                Dim separationDLL As String() = Split(Ligne, ";")

                For i = 0 To separationDLL.Length - 1

                    If LinaBot.Dll.DllAll.ContainsKey(separationDLL(i)) Then

                        With LinaBot.Dll.DllAll(separationDLL(i))

                            .monType.GetMethod("Main").Invoke(Activator.CreateInstance(.monType), {LinaBot})

                        End With

                    End If

                Next

            End Sub

            Private Sub Proxy()

                With Bot

                    .Proxy.Active = False
                    .Proxy.Identifiant = ""
                    .Proxy.MotDePasse = ""
                    .Proxy.IP = ""
                    .Proxy.Port = ""

                End With

            End Sub

        End Module

    End Namespace

    Namespace Sauvegarder

        Module Sauvegarder

            Public Sub Initialiser()

                Dim Ligne As String = DLL() & vbCrLf

                Dim swEcriture As New IO.StreamWriter(Application.StartupPath + "Compte\Option/" & Bot.Personnage.NomDuPersonnage & "_" & Bot.Personnage.Serveur & ".txt")

                swEcriture.Write(Ligne)

                swEcriture.Close()

            End Sub

            Private Function DLL() As String

                Dim Ligne As String = "DLL|"

                With Bot

                    For Each pair As String In LinaBot.Dll_Liste

                        Ligne &= pair & ";"

                    Next

                End With

                Return Ligne

            End Function

        End Module

    End Namespace

End Namespace