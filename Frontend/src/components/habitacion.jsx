import React, { useState, useEffect } from "react";
import Navigation from "../components/Navigation";
import './css/style.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleLeft, faAngleRight, faFileCirclePlus, faMagnifyingGlass, faPenToSquare, faTrashCan} from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import ButtonGroup from 'react-bootstrap/ButtonGroup';
function habitacion() {
  {/* Del Modal*/}
  const [modalShow, setModalShow] = useState(false);
  const ModalInsertar = (props) => {
    return (
      <Modal
        {...props}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter" className="fw-semibold fs-4">
          Agregar Habitacion
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>


        <form className="row g-3">
        <div className="col-md-6">
          <label htmlFor="validationDefault01" className="form-label">Tipo de Categoría</label>
          <select className="form-select" id="validationDefault01" required>
            <option selected disabled value="">Elige</option>
            <option value="1">Primeum</option>
            <option value="2">Primeum</option>
            <option value="3">Primeum</option>
          </select>
        </div>

        <div className="col-md-6">
          <label htmlFor="validationDefault02" className="form-label">Estado de habitación</label>
          <select className="form-select" id="validationDefault02" required>
            <option selected disabled value="">Elige</option>
            <option value="Disponible">Disponible</option>
            <option value="Ocupada">Ocupada</option>
            <option value="Reservada">Reservada</option>
          </select>
        </div>

        <div className="col-md-12">
          <label htmlFor="validationDefault03" className="form-label">Ubicación de la habitación</label>
          <input type="text" className="form-control" id="validationDefault03" placeholder="Ala, piso, No." maxLength="255" required />
        </div>

        <div className="col-md-12">
          <label htmlFor="validationDefault04" className="form-label">Comentario</label>
          <textarea className="form-control" id="validationDefault04"></textarea>
        </div>
      </form>


        </Modal.Body>
        <Modal.Footer>
        <button className="btn btn-dark col-md-3">Agregar</button>
        </Modal.Footer>
      </Modal>
    );
  }


    {/* De La Tabla */}
    const [paginaActual, setPaginaActual] = useState(0);
    const filasPorPagina = 5;
    useEffect(() => {
      mostrarFilas();
    }, [paginaActual]);
  
    const mostrarFilas = () => {
      const filas = document.getElementById('PTabla').getElementsByTagName('tbody')[0].rows;
      const totalPaginas = Math.ceil(filas.length / filasPorPagina);
  
      for (let i = 0; i < filas.length; i++) {
        filas[i].style.display = 'none';
      }
  
      for (let i = paginaActual * filasPorPagina; i < (paginaActual + 1) * filasPorPagina; i++) {
        if (filas[i]) {
          filas[i].style.display = '';
        }
      }
  
      mostrarNumeroDeSeccion();
    };
  
    const irHaciaAtras = () => {
      if (paginaActual > 0) {
        setPaginaActual((prevPagina) => prevPagina - 1);
      }
    };
  
    const irHaciaAdelante = () => {
      const filas = document.getElementById('PTabla').getElementsByTagName('tbody')[0].rows;
      const totalPaginas = Math.ceil(filas.length / filasPorPagina);
  
      if (paginaActual < totalPaginas - 1) {
        setPaginaActual((prevPagina) => prevPagina + 1);
      }
    };
  
    const mostrarNumeroDeSeccion = () => {
      const seccionBtn = document.getElementById('seccionBtn');
      if (seccionBtn) {
        seccionBtn.textContent = 'Pag. ' + (paginaActual + 1);
      }
    };

  return (
    <>
    <Navigation/>

    <div className="row g-3" id="contenido">
      <div className="col-md-9">
        <h3 id="sub-titulo">Habitaciones</h3>
      </div>

      {/* Boton agregar, llama modal */}
      <div className="col-md-3">
      <button onClick={() => setModalShow(true)} className="btn" id="btn-add"><FontAwesomeIcon icon={faFileCirclePlus} /> Agregar Habitaciones</button>
        <ModalInsertar
          show={modalShow}
          onHide={() => setModalShow(false)}
        />
      </div>

      {/* Buscador */}
      <div className="col-md-12">
        <div className="input-group">
          <input type="text" className="form-control" placeholder="Escribir aqui..." />
          <button className="btn btn-secondary" type="button">
          <FontAwesomeIcon icon={faMagnifyingGlass} />
          </button>
        </div>
      </div>


      <div className="col-md-12">
        <div className="card table-responsive" style={{ height: '204.5px' }}>
          <table className="table table-hover align-middle table-sm" id="PTabla" style={{ marginBottom: '0px' }}>
            <thead className="table-light">
              <tr>
                <th scope="col">ID Habitación</th>
                <th scope="col">Categoria</th>
                <th scope="col">Ubicación</th>
                <th scope="col">Estado</th>
                <th scope="col">Opciones</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <th scope="row">1</th>
                <td>Primeum</td>
                <td>Ala norte, piso 1, habitacion no. 23</td>
                <td>Ocupada</td>
                <td>
                <button type="button" className="btn btn-primary btn-sm" id="btn-opcion">
                <FontAwesomeIcon icon={faPenToSquare} />
                </button>
                <button type="button" className="btn btn-danger btn-sm" id="btn-opcion">
                <FontAwesomeIcon icon={faTrashCan} />
                </button>
              </td>
              </tr>

              <tr>
                <th scope="row">6</th>
                <td>Primeum</td>
                <td>Ala norte, piso 1, habitacion no. 23</td>
                <td>Ocupada</td>
                <td>
                <button type="button" className="btn btn-primary btn-sm" id="btn-opcion">
                <FontAwesomeIcon icon={faPenToSquare} />
                </button>
                <button type="button" className="btn btn-danger btn-sm" id="btn-opcion">
                <FontAwesomeIcon icon={faTrashCan} />
                </button>
              </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>

      <div className="col-md-12">
      <div className="btn-group" role="group" id="btn-tabla">
        <button type="button" className="btn btn-secondary btn-sm" onClick={irHaciaAtras}>          
          <FontAwesomeIcon icon={faAngleLeft} />
        </button>
        <button type="button" className="btn btn-outline-secondary btn-sm" id="seccionBtn">-</button>
        <button type="button" className="btn btn-secondary btn-sm" onClick={irHaciaAdelante}>
          <FontAwesomeIcon icon={faAngleRight} />
        </button>
      </div>
      </div>

    </div>
    </>
  )
}

export default habitacion