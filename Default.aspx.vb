Imports System.Data.SqlClient
Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("testConnectionString").ConnectionString)
    Dim cmd As SqlCommand
    Dim ds As New DataSet
    Dim ad As SqlDataAdapter

    'DECLARE GLOBLE VARIABLE WHICH HOLDS CURRENT PAGE INDEX.
    Dim currentIndex As Integer = 0
    Dim pages As New PagedDataSource 'INITIALIZE THE PAGE VARIABLE.
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'DISPLAY ONLY 5 RECORD AT PAGE LOAD.
            bind()
        End If

    End Sub
    Protected Sub bind()
        'FETCH DATA FROM SQL DATABASE.
        cmd = New SqlCommand("select * from tb", con)
        ad = New SqlDataAdapter(cmd)
        ad.Fill(ds)

        'ALLOW PAGING
        pages.AllowPaging = True
        'DISPLAY NUMBER OF DATA PER PAGE 
        pages.PageSize = 5

        'CONFIGURE DATA SOURCE TO PAGEDATASOURCE.
        pages.DataSource = ds.Tables(0).DefaultView

        'SET CURRENT PAGE INDEX RETRIEVED BY VIEWSTATE.
        pages.CurrentPageIndex = ViewState("currentval")

        'NOW SIMPLY PASS ALL PAGED VARIABLE VALUE TO DATALIST CONTROL.
        DataList1.DataSource = pages    'TYPE YOUR DATA CONTROL NAME LIKE LISTVIEW,REPEATER ETC.
        DataList1.DataSourceID = ""
        'IT IS NECESSARY TO CLEAR DATASOURCE ID ,IF YOU CONFIGURE DATALIST AT DESIGN TIME.
        DataList1.DataBind()
        DataList1.DataKeyField = "num" 'ALL OPERATION PERFORMED BY PRIMARY KEY COLUMN THAT IS "num".


        'RETAINED THE ABOVE CURRENTINDEX VALUES WHICH I STORE IN VIEWSTATE. YOU CAN STORE IN SESSION.
        ViewState("currentval") = currentIndex

    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        '--------------CODE FOR NAVIAGATING TO FIRST PAGE-------------------


        'SET THE INDEX OF DATALIST TO 0 .MEANS FIRST PAGE OF PAGEDDATASOURCE.
        currentIndex = 0
        'store CURRENTVAL VARIABLE TO VIEWSTATE FOR RETAINING VALUE.
        ViewState("currentval") = currentIndex
        'CALL BIND METHOD FOR BINDING DATALIST CONTROL.
        bind()

        '--------------END OF CODING  FOR NAVIAGATING TO FIRST PAGE-------------------


    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        '--------------CODING  FOR NAVIAGATING TO NEXT PAGE'S-------------------




        'FIRST WE FETCH ALL RECORDS FROM DATABASE.
        cmd = New SqlCommand("select count(*) from tb", con)
        con.Open()
        'HERE WE CALL EXECUTESCALER .BECAUSE WE WANT TOTAL AVAILABLE RECORD NUMBER FROM DATABASE.
        Dim getvalue As Integer = cmd.ExecuteScalar()
        con.Close()

        'NOW DIVIDE THIS NUMBER BY PAGESIZE NUMBER .FOR EX IN OUR EXAMPLE PAGE SIZE IS 5.
        getvalue = getvalue / 5


        'HERE getvalue VARIABLE CONTAINS 5 RECORD AT A TIME.

        If ViewState("currentval") < getvalue - 1 Then

            'INCRESE VALUE OF GLOBLE VARIABL TO SEE NEXT PAGE DATA.
            currentIndex = ViewState("currentval") + 1
            'STORE THE CURRENTVAL VALUE TO VIEWSTATE.
            ViewState("currentval") = currentIndex
            'CALL BIND METHOD TO BIND CONTROL DATA.
            bind()
        Else
            'MEANS YOU ARE IN LAST PAGE.
            Response.Write("YOU ARE ALREADY IN LAST PAGE...")
        End If


        '--------------END OF CODING  FOR NAVIAGATING TO NEXT PAGE'S-------------------


    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        '--------------CODING FOR NAVIAGATING TO PREVIOUS PAGE'S-------------------




        'WE CHECK WETHER VIEWSTATE VALUE IS GREATER THAN 0 OR NOT.
        'IF GREATER THAN 0 THEN WE DECREASE THE VIEWSTATE VALUE AND BIND THE DATALIST CONTROL.
        If ViewState("currentval") > 0 Then
            'DECREMENT THE GLOBLE VARIABLE (currentIndex) TO 1 .FOR VIEWING PREVIOUS RECORD.
            currentIndex = ViewState("currentval") - 1
            'STORE THE NEW VALUE TO VIEWSTATE .
            ViewState("currentval") = currentIndex
            'CALL CUSTOM BIND METHOD. .
            bind()
        Else
            'MEANS YOU ARE IN FIRST PAGE.
            Response.Write("YOU ARE ALREADY IN FIRST PAGE...")
        End If


        '--------------END OF CODING  FOR NAVIAGATING TO PREVIOUS PAGE'S-------------------

    End Sub
    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        '--------------CODING  FOR NAVIAGATING TO LAST PAGE-------------------


        cmd = New SqlCommand("select count(*) from tb", con)
        con.Open()
        'HERE WE CALL EXECUTESCALER .BECAUSE WE WANT TOTAL AVAILABLE RECORD NUMBER FROM DATABASE.
        Dim getvalue As Integer = cmd.ExecuteScalar()
        con.Close()

        'NOW DIVIDE THIS NUMBER BY PAGESIZE NUMBER .FOR EX IN OUR EXAMPLE PAGE SIZE IS 5.
        getvalue = getvalue / 5
        'HERE getvalue VARIABLE CONTAINS 5 RECORD AT A TIME.


        currentIndex = getvalue - 1 'OUR GLOABLE VAIRABLE INITIAL VALUE IS 0.
        'SO WE NEED TO DECREMENT VALUE BY 1.

        'STORE THE CURRENTINDEX VALUE TO VIEWSTATE.
        ViewState("currentval") = currentIndex
        'CALL CUSTOM BIND METHOD.
        bind()



        '--------------END OF CODING  FOR NAVIAGATING TO PREVIOUS PAGE'S-------------------



    End Sub
End Class
