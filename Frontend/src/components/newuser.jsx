import React, { useState } from "react";
import Navigation from "../components/Navigation";
import { useNavigate } from "react-router-dom";
import "./css/style.css";
import axios from "axios";
import img from "./img/Add-User.png";

const URL = "https://pfg10itla-001-site1.gtempurl.com/Registrar";
const newuser = () => {
  const [user, setUser] = "";
  const navigate = useNavigate();
  const [formulario, setFormulario] = useState({
    idUsuario: 0,
    nombreCompleto: "",
    nombreUsuario: "",
    contrasenaHash: "",
    salting: "",
    rolUsuario: "ADMIN",
    activo: 0,
  });

  const registrarUser = async (user) => {
    try {
      await axios.post(URL, user, {
        headers: { "Content-Type": "application/json" },
      });
    } catch (error) {
      console.log("Error al intentar crear un Usuario", error);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await registrarUser(formulario);
      navigate("/");
      console.log("Usuario creado correctamente");
    } catch (error) {
      console.log("Error al intentar crear un Usuario", error);
    }
  };

  return (
    <>
      <Navigation />
      <div className="row g-3" id="contenido">
        <div className="col-md-6">
          <img src={img} className="img-adduser" alt="Add User" />
        </div>

        <div className="col-md-6">
          <h2 className="p-3 color1 fw-medium" style={{ color: "#0C5A79" }}>
            <b>Crear nuevo usuario</b>
          </h2>
          <form onSubmit={handleSubmit}>
            <div className="form-row">
              <div className="form-group col-md-12 p-2">
                <label htmlFor="inputNombre">Nombre Completo</label>
                <input
                  type="text"
                  className="form-control"
                  id="inputNombre"
                  placeholder="Nombre"
                  value={formulario.nombreCompleto}
                  onChange={(e) =>
                    setFormulario({
                      ...formulario,
                      nombreCompleto: e.target.value,
                    })
                  }
                />
              </div>

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
            </div>

            <div className="form-group col-md-5 p-2">
              <button type="submit" className="btn btn-dark">
                Guardar
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default newuser;
