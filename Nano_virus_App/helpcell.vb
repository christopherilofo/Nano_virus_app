Imports System.IO

Public Class helpcell


    Dim list As New List(Of cell)
    Dim infectedCells As New List(Of Integer)
    Dim celltypeprobability As New List(Of String)
    Dim nano_virus_location As Integer
    Dim nano_virus_last_location = New Random(System.DateTime.Now.Millisecond)

#Region "Selected Random Cell"
    Public Function selectRandCell(cellTypeSelected As String) As Integer
        Dim selectrandomcell As Boolean = False
        Dim locationofvirus As Integer = 0
        Dim randomvirus As New Random(System.DateTime.Now.Millisecond)

        If cellTypeSelected <> "" Then
            Do
                locationofvirus = randomvirus.[Next](0, list.Count - 1)
                If list.ElementAt(locationofvirus).typecell = cellTypeSelected Then
                    selectrandomcell = True
                End If

            Loop While selectrandomcell = False
        Else
            locationofvirus = randomvirus.[Next](0, list.Count - 1)
        End If
        selectRandCell = locationofvirus
    End Function
#End Region

#Region " Nano virus Move"

    Public Sub nano_virus_move(ByVal tumorouscount)
        If tumorouscount = 1 Then
            Dim selectrandomcell = False
            Do While (selectrandomcell = False)

                nano_virus_location = nano_virus_last_location.[Next](0, list.Count - 1)
                If list.ElementAt(nano_virus_location).typecell = "Red Blood" Then
                    selectrandomcell = True
                End If

            Loop

        Else
            nano_virus_location = nano_virus_last_location.Next(0, list.Count - 1)
        End If

        If list.ElementAt(nano_virus_location).typecell = "Tumorous" Then
            list.Item(nano_virus_location).Destroy()
            nano_virus_location = nano_virus_last_location.Next(0, list.Count - 1)

        Else
            nano_virus_location = nano_virus_last_location.Next(0, list.Count - 1)
        End If
    End Sub
#End Region

#Region "Infected cells"

    Public Sub infectedCellsfunc()

        For i = 0 To infectedCells.Count - 1
            list.Item(infectedCells.Item(i)).typecell = "Tumorous"
            list.Item(infectedCells.Item(i)).status = ""
        Next

        For x = 0 To infectedCells.Count - 1
            infectedCells.Remove(x)
        Next

    End Sub
#End Region

#Region " Generate Cells Type"

    Public Sub Generatecelltype()

        Dim r As New Random(System.DateTime.Now.Millisecond)
        Dim r1 As New Random(System.DateTime.Now.Millisecond)

        For i = 0 To 99
            If i < 5 Then
                celltypeprobability.Add("Tumorous")

            End If
            If i >= 5 And i < 30 Then
                celltypeprobability.Add("White Blood cell")

            End If
            If i >= 30 Then
                celltypeprobability.Add("Red Blood")

            End If

        Next
        For i = 0 To 99
            Dim celltemp As New cell
            celltemp.x = r.Next(1, 5000)
            celltemp.y = r.Next(1, 5000)
            celltemp.z = r.Next(1, 5000)
            celltemp.typecell = celltypeprobability.Item(r1.Next(0, 99))
            list.Add(celltemp)
        Next


    End Sub
#End Region

#Region " Get cells Display"

    Public Sub getcellsdisplay()
        For i = 0 To list.Count - 1
            Console.WriteLine(list.Item(i).typecell)
        Next
        Console.ReadKey()
    End Sub
#End Region

#Region "Get Distance"
    Public Function GetDistance(ByVal c1 As cell, ByVal c2 As cell) As Integer
        Return Math.Sqrt((Math.Abs(c2.x - c1.x) ^ 2) +
                   (Math.Abs(c2.y - c1.y) ^ 2) + (c2.z - c1.z) ^ 2)
    End Function
#End Region

#Region " Get Cell Type count"

    Public Function getCellTypeCount(ByVal typecell As String, Optional ByRef cellcounter As Int32 = 0) As Boolean

        Dim mycounter As Int32 = 0
        For x = 0 To list.Count - 1
            If list(x).typecell = typecell Then
                mycounter = mycounter + 1
            End If
        Next
        cellcounter = mycounter
        If mycounter > 0 Then
            getCellTypeCount = True
        Else
            getCellTypeCount = False
        End If

    End Function
#End Region

#Region "Red blood selection"
    Public Function selectredbloodcell() As Int32
        selectredbloodcell = 0
        Dim selectrandomredcell As Boolean = False
        Dim locationofvirus As Int32
        Dim rvirus As New Random(System.DateTime.Now.Millisecond)

        Do While (selectrandomredcell = False)
            locationofvirus = rvirus.Next(0, list.Count - 1)
            If list.Item(locationofvirus).typecell = "Red Blood" Then

                selectrandomredcell = True
            End If
        Loop
        selectredbloodcell = locationofvirus
    End Function
#End Region

#Region "Play Game"

    Public Sub playme()
        Dim is_game_finish = False
        Dim tumorouscount As Int32 = 1
        Dim gettumorcount As Int32 = 0
        Dim cycle As Integer = 0
        Dim message As String
        Do While (is_game_finish = False)

            nano_virus_move(tumorouscount)

            If tumorouscount = 5 Then
                Dim position As Integer
                Dim currentvalue As Integer = 5001
                Dim mydistance As Int32

                For i = 0 To list.Count - 1

                    If list(i).typecell = "Tumorous" Then
                        position = 0
                        mydistance = 0
                        currentvalue = 5001

                        For e = 0 To list.Count - 1
                            If getCellTypeCount("Red Blood") = True And getCellTypeCount("White Blood cell") = True And list.Item(e).status <> "infected" Then
                                If list(e).typecell = "Red Blood" Then
                                    mydistance = GetDistance(list(i), list(e))
                                    If mydistance < currentvalue And mydistance < 5000 Then
                                            currentvalue = GetDistance(list(i), list(e))
                                        position = e

                                    End If
                                End If


                            End If
                            If getCellTypeCount("Red Blood") = False And getCellTypeCount("White Blood cell") = True And list.Item(e).status <> "infected" Then
                                If list(e).typecell = "White Blood cell" Then
                                    If GetDistance(list(i), list(e)) < currentvalue Then
                                        currentvalue = GetDistance(list(i), list(e))
                                        position = e
                                    End If
                                End If
                            End If
                        Next

                        infectedCells.Add(position)
                        list.Item((position)).typecell = "Tumorous"
                        list.Item(position).status = "infected"

                    End If
                Next
               
                tumorouscount = 0
                gettumorcount = 0

                For x = 0 To list.Count - 1
                    If list(x).typecell = "Tumorous" Then
                        gettumorcount = gettumorcount + 1
                    End If
                Next


                Dim tumor1, redcell1, whitecell1 As Int32
                getCellTypeCount("Tumorous", gettumorcount)
                getCellTypeCount("Tumorous", tumor1)
                getCellTypeCount("Red Blood", redcell1)
                getCellTypeCount("White Blood cell", whitecell1)

                Threading.Thread.Sleep(200)
                If redcell1 = 0 And whitecell1 = 0 Then
                    is_game_finish = True
                End If

               

            End If
            tumorouscount = tumorouscount + 1
            cycle = cycle + 1
            Dim tumor, redcell, whitecell, Destroycells As Int32

            getCellTypeCount("Tumorous", tumor)
            getCellTypeCount("Red Blood", redcell)
            getCellTypeCount("White Blood cell", whitecell)
            Threading.Thread.Sleep(200)

            getCellTypeCount("", Destroycells)

            ' Define the stream writer
            Dim filewriter As StreamWriter

            'save component values
            message = ("Cycle No. " & cycle & "No of tumor, Red , White cell, Destroyed Cell : " & tumor & "," & redcell & "," & whitecell & "," & Destroycells)
            Console.WriteLine("Cycle No. " & cycle & "No of tumor, Red , White cell, Destroyed Cell : " & tumor & "," & redcell & "," & whitecell & "," & Destroycells)

            filewriter = New StreamWriter("textfile.txt", True)
            filewriter.WriteLine(message)

            'save and close file
            filewriter.Flush()
            filewriter.Close()

            Threading.Thread.Sleep(300)
        Loop


        Console.ReadKey()
    End Sub
#End Region


End Class
