import React, { useState } from "react";
import Navigation from "../components/Navigation";
import { useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "./css/Login.css";
import axios from "axios";
import login from "./img/logo.png";

import { useUser } from './UserContext'

const URL = "https://pfg10itla-001-site1.gtempurl.com/Login/login";

const Login = () => {
  const [user, setUser] = "";
  const navigate = useNavigate();
  const { updateUser } = useUser();
  const storedUsername = localStorage.getItem('username') || '';
  const [formulario, setFormulario] = useState({
    usuario: "",
    clave: "",
  });

  const loginUser = async (user) => {
    try {
      await axios.post(URL, user, {
        headers: { "Content-Type": "application/json" },
      });
    } catch (error) {
      console.log("Error al intentar logear un Usuario", error);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await loginUser(formulario);
      updateUser({ nombre: formulario.nombreUsuario });
      navigate("/Inicio");
      localStorage.setItem('username', formulario.nombreUsuario);
      console.log("Usuario Logeado correctamente");
    } catch (error) {
      console.log("Error al intentar logearse un Usuario", error);
    }
  };
  return (
    <div className="login-container">
      <div className="login-content">
        <header id="header-L">
          <img src={login} id="logo-l" style={{ width: "40%" }} alt="Logo" />
          <h2>Iniciar Sesión</h2>
        </header>

        <form className="p-2" id="input-L" onSubmit={handleSubmit}>
          <div className="form-group col-md-12 p-2">
            <label htmlFor="inputUsuario4">Usuario</label>
            <input
              type="text"
              className="form-control"
              id="inputUsuario4"
              placeholder="Usuario"
              value={formulario.nombreUsuario}
              onChange={(e) =>
                setFormulario({
                  ...formulario,
                  nombreUsuario: e.target.value,
                })
              }
            />
          </div>

          <div className="form-group col-md-12 p-2">
            <label htmlFor="inputPassword4">Contraseña</label>
            <input
              type="password"
              className="form-control"
              id="inputPassword4"
              placeholder="Contraseña"
              value={formulario.contrasenaHash}
              onChange={(e) =>
                setFormulario({
                  ...formulario,
                  contrasenaHash: e.target.value,
                })
              }
            />
          </div>
          <div className="mb-3">
            <button type="submit" className="btn-login">
              Iniciar Sesión
            </button>
            <a href="/Inicio">ir a home</a>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Login;
