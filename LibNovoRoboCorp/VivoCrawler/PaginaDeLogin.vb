Imports OpenQA.Selenium
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium.Support.UI

Public Class PaginaDeLogin


    Dim driver As ChromeDriver
    Public wait As WebDriverWait

    Sub New()
        Me.driver = WebdriverCt.Driver
        Me.wait = WebdriverCt.Wait
        Login()
    End Sub


    Sub Login()
        Dim UserField, UserPass, BtnEntrar As IWebElement

Login:

        Console.WriteLine("Tentando conectar ao vivo Corp às " + Now.TimeOfDay.ToString)
        Try
            driver.Navigate.GoToUrl("https://vivocorp-parceiro.vivo.com.br/vivocorp_oui/start.swe")
        Catch ex As Exception
            GoTo Login
        End Try

        driver.Manage.Window.Maximize()

        UserField = driver.FindElement(By.XPath("//*[@id='username']"))
        UserPass = driver.FindElement(By.XPath("//*[@id='password']"))
        BtnEntrar = driver.FindElement(By.XPath("//*[@id='form']/div[3]/div/input"))

        UserField.SendKeys("80032734")
        UserPass.SendKeys("AS@2109tel")

        BtnEntrar.Click()
        Crawler.EnviarLog("Vivo Corp conectado, iniciando pesquisa de CNPJS")


    End Sub


End Class
