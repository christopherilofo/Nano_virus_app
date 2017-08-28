Public Class cell
    Public x, y, z As Int16
    Public typecell As String
    Public status As String

    Public Sub New(ByVal x, ByVal y, ByVal z)
        Me.x = x
        Me.y = x
        Me.z = x




    End Sub
    Public Sub New()
        Me.x = 0
        Me.y = 0
        Me.z = 0
        Me.status = ""


    End Sub
    Public Sub settypecell(ByVal type As String)
        Me.typecell = type
    End Sub

    Public Sub Destroy()
        typecell = ""
    End Sub


    


End Class
