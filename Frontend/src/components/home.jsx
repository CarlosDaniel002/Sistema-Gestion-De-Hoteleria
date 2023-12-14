import React, { useState } from "react";
import Navigation from "../components/Navigation";
import './css/style.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBed, faBedPulse, faSprayCanSparkles, faClipboard } from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';

const Reservacion = () => {
   return (
      <div>
        
       <Navigation /> 
         <div className="row g-3" id="contenido">

            <div className="col-md-9 p-3">
              <h2 className="p-3 color1 fw-medium" style={{ color: '#0C5A79' }}><b>Bienvenido/a!</b></h2>
            </div>

            <div className="col-md-3 p-3">
              <p className="p-3 text-end fst-italic">Jueves 14 de diciembre, 2023</p>
            </div>

            <div className="container-fluid text-center p-3">
              <div className="row">
                <div className="col-md-3">
                  <div className="card" id="card">
                    <div className="card-body d-flex flex-column align-items-center justify-content-center">
                      <h3 id="titulo-card" className="color1 p-4">Habitaciones Disponibles</h3>
                      <p className="IcoTex"><FontAwesomeIcon icon={faBed} /> 25</p>
                    </div>
                  </div>
                </div>

                <div className="col-md-3">
                  <div className="card" id="card">
                    <div className="card-body d-flex flex-column align-items-center justify-content-center">
                      <h3 id="titulo-card" className="color1 p-4">Habitaciones <br />Ocupadas</h3>
                      <p className="IcoTex"><FontAwesomeIcon icon={faBedPulse} /> 25</p>
                    </div>
                  </div>
                </div>

                <div className="col-md-3">
                  <div className="card" id="card">
                    <div className="card-body d-flex flex-column align-items-center justify-content-center">
                      <h3 id="titulo-card" className="color1 p-4">Habitaciones <br />En Limpieza</h3>           
                      <p className="IcoTex"><FontAwesomeIcon icon={faSprayCanSparkles} /> 25</p>
                    </div>
                  </div>
                </div>

                <div className="col-md-3">
                  <div className="card" id="card">
                    <div className="card-body d-flex flex-column align-items-center justify-content-center">
                      <h3 id="titulo-card" className="color1 p-4">Reservaciones Realizadas</h3>           
                      <p className="IcoTex"><FontAwesomeIcon icon={faClipboard}/> 25</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
      </div>
   );
}

export default Reservacion;
