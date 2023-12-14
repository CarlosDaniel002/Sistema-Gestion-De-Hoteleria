import React from 'react'
import Navigation from "../components/Navigation";
import './css/style.css';

import img from './img/Add-User.png';

const newuser = () => {
  return (
    <><Navigation/>
    <div className="row g-3" id="contenido">
    <div className="col-md-6">
      <img src={img} className="img-adduser" alt="Add User" />
    </div>

    <div className="col-md-6">
      <h2 className="p-3 color1 fw-medium" style={{ color: '#0C5A79' }}>
        <b>Crear nuevo usuario</b>
      </h2>
      <form>
        <div className="form-row">
          <div className="form-group col-md-12 p-2">
            <label htmlFor="inputNombre">Nombre Completo</label>
            <input type="text" className="form-control" id="inputNombre" placeholder="Nombre" />
          </div>

          <div className="form-group col-md-12 p-2">
            <label htmlFor="inputUsuario4">Usuario</label>
            <input type="text" className="form-control" id="inputUsuario4" placeholder="Usuario" />
          </div>

          <div className="form-group col-md-12 p-2">
            <label htmlFor="inputPassword4">Contraseña</label>
            <input type="password" className="form-control" id="inputPassword4" placeholder="Contraseña" />
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
  )
}

export default newuser