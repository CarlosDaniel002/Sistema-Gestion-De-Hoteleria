using Data.Models;
using Data.UserData;
using Negocio.User;
using System;
using System.Data;
using System.Data.SqlClient;
using Xunit;

public class LoginFunctionsTest
{
    private readonly LoginDB _loginDB;

    public LoginFunctionsTest()
    {
        _loginDB = new LoginDB();
    }

    [Fact]
    public void TestLoginUser_ValidUserAndCorrectPassword_ReturnsToken()
    {
        // Arrange
        string userName = "UsuarioDePrueba"; // Reemplaza con un nombre de usuario real en tu base de datos.
        string password = "ContraseñaDePrueba"; // Reemplaza con la contraseña válida para el usuario de prueba.

        // Act
        string result = LoginFunctions.LoginUser(userName, password);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        // Asegúrate de que el resultado sea un token válido en lugar de un mensaje de error.
    }

    [Fact]
    public void TestLoginUser_InvalidUser_ReturnsErrorMessage()
    {
        // Arrange
        string userName = "UsuarioDePrueba"; // Reemplaza con un nombre de usuario real en tu base de datos.
        string password = "ContraseñaDePrueba"; // Contraseña válida para el usuario de prueba.

        // Act
        string result = LoginFunctions.LoginUser(userName, password);

        // Assert
        Assert.Equal("El usuario no existe.", result);
    }

    [Fact]
    public void TestLoginUser_ValidUserAndInvalidPassword_ReturnsErrorMessage()
    {
        // Arrange
        string userName = "UsuarioDePrueba"; // Reemplaza con un nombre de usuario real en tu base de datos.
        string password = "Cueba";

        // Act
        string result = LoginFunctions.LoginUser(userName, password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Contraseña incorrecta.", result); // Resultado esperado cuando la contraseña es incorrecta.
    }
}
