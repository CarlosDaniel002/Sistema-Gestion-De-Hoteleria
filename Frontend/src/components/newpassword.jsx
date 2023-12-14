import React from 'react'
import Navigation from "../components/Navigation";
import './css/style.css';

import img from './img/New-Password.png';

const newpassword = () => {
  return (
    <><Navigation/>
    <div className="row g-3" id="contenido">
    <div className="col-md-6">
      <img src={img} className="img-adduser" alt="Add User" />
    </div>

    <div className="col-md-6">
      <h2 className="p-3 color1 fw-medium" style={{ color: '#0C5A79' }}>
        <b>Cambiar Contraseña</b>
      </h2>
      <form>
        <div className="form-row">
        <div className="form-group col-md-12 p-2">
        <label htmlFor="inputPassword1">Contraseña Actual</label>
        <input type="password" className="form-control" id="inputPassword1" />
      </div>

      <div className="form-group col-md-12 p-2">
        <label htmlFor="inputPassword2">Contraseña Nueva</label>
        <input type="password" className="form-control" id="inputPassword2" />
      </div>

      <div className="form-group col-md-12 p-2">
        <label htmlFor="inputPassword3">Confirmar Contraseña Nueva</label>
        <input type="password" className="form-control" id="inputPassword3" />
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

export default newpassword