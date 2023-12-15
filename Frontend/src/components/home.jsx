import React, { useState, useEffect } from "react";
import Navigation from "../components/Navigation";
import './css/style.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faBed, faBedPulse, faSprayCanSparkles, faClipboard } from '@fortawesome/free-solid-svg-icons';
import axios from "axios";
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';

const URL = "https://pfg10itla-001-site1.gtempurl.com/Habitacion";

const Reservacion = () => {
  const [habitaciones, setHabitaciones] = useState([]);

  useEffect(() => {
    const obtenerHabitaciones = async () => {
      try {
        const response = await axios.get(URL);
        setHabitaciones(response.data);
      } catch (error) {
        console.error("Error al obtener las habitaciones", error);
      }
    };

    obtenerHabitaciones();
  }, []);

  const contarHabitacionesPorEstado = (estado) => {
    if (!Array.isArray(habitaciones)) {
      return 0;
    }
    return habitaciones.filter(habitacion => habitacion.estado === estado).length;
  };
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
                      <p className="IcoTex"><FontAwesomeIcon icon={faBed} /> {contarHabitacionesPorEstado("Disponible")}</p>
                    </div>
                  </div>
                </div>

                <div className="col-md-3">
                  <div className="card" id="card">
                    <div className="card-body d-flex flex-column align-items-center justify-content-center">
                      <h3 id="titulo-card" className="color1 p-4">Habitaciones <br />Ocupadas</h3>
                      <p className="IcoTex"><FontAwesomeIcon icon={faBedPulse} /> {contarHabitacionesPorEstado("Ocupada")}</p>
                    </div>
                  </div>
                </div>

                <div className="col-md-3">
                  <div className="card" id="card">
                    <div className="card-body d-flex flex-column align-items-center justify-content-center">
                      <h3 id="titulo-card" className="color1 p-4">Habitaciones <br />En Limpieza</h3>           
                      <p className="IcoTex"><FontAwesomeIcon icon={faSprayCanSparkles} /> {contarHabitacionesPorEstado("En Limpieza")}</p>
                    </div>
                  </div>
                </div>

                <div className="col-md-3">
                  <div className="card" id="card">
                    <div className="card-body d-flex flex-column align-items-center justify-content-center">
                      <h3 id="titulo-card" className="color1 p-4">Reservaciones Realizadas</h3>           
                      <p className="IcoTex"><FontAwesomeIcon icon={faClipboard}/> {contarHabitacionesPorEstado("Reservada")}</p>
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
