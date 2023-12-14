import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import './css/Login.css';
import login from './img/logo.png';

const Login = () => {
  return (
    <div className="login-container">
    <div className="login-content">
      <header id="header-L">
        <img src={login} id="logo-l" style={{ width: '40%' }} alt="Logo" />
        <h2>Iniciar Sesión</h2>
      </header>

      <form className="p-2" id="input-L">
        <div className="mb-3">
          <input type="text" placeholder="Usuario" className="form-control" name="usuario" required />
        </div>
        <div className="mb-3">
          <input type="password" placeholder="Contraseña" className="form-control" name="contrasena" required />
        </div>
        <div className="mb-3">
          <button type="submit" className="btn-login">Iniciar Sesión</button>
          <a href='/Inicio'>ir a home</a>
        </div>
      </form>
    </div>
  </div>
  );
};

export default Login;
