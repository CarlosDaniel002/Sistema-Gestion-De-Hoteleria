import React, { useState, useEffect } from "react";
import Navigation from "../components/Navigation";
import './css/style.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleLeft, faAngleRight, faFileCirclePlus, faMagnifyingGlass, faPenToSquare, faTrashCan, faSackDollar,faBuildingCircleArrowRight, faBuildingCircleCheck} from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import ButtonGroup from 'react-bootstrap/ButtonGroup';

function reservacion() {
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
            Agregar  Reservacion
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
  
  
          <form className="row g-3">
      <div className="col-md-4">
        <label htmlFor="validationDefault01" className="form-label">
          Tipo de Habitacion
        </label>
        <select className="form-select" id="validationDefault01" required>
          <option selected disabled value="">
            Elige
          </option>
          <option value="Primeum">Primeum</option>
          <option value="Primeum">Primeum</option>
          <option value="Primeum">Primeum</option>
        </select>
      </div>

      <div className="col-md-4">
        <label htmlFor="validationDefault02" className="form-label">
          Cantidad max huésped
        </label>
        <select className="form-select" id="validationDefault02" required>
          <option selected disabled value="">
            Elige
          </option>
          <option value="1">1</option>
          <option value="2">2</option>
          <option value="3">3</option>
          <option value="4">4</option>
          <option value="5">5</option>
          <option value="6">6</option>
        </select>
      </div>

      <div className="col-md-4">
        <label htmlFor="validationDefault03" className="form-label">
          ID Cliente
        </label>
        <input
          type="text"
          className="form-control"
          id="validationDefault03"
          maxLength="100"
          required
        />
      </div>

      <div className="col-md-6">
        <label htmlFor="validationDefault04" className="form-label">
          Fecha de Check-In
        </label>
        <input type="date" className="form-control" id="validationDefault04" required />
      </div>
      <div className="col-md-6">
        <label htmlFor="validationDefault05" className="form-label">
          Hora de Check-In
        </label>
        <input type="time" className="form-control" id="validationDefault05" required />
      </div>

      <div className="col-md-6">
        <label htmlFor="validationDefault06" className="form-label">
          Fecha de Check-Out
        </label>
        <input type="date" className="form-control" id="validationDefault06" required />
      </div>
      <div className="col-md-6">
        <label htmlFor="validationDefault07" className="form-label">
          Hora de Check-Out
        </label>
        <input type="time" className="form-control" id="validationDefault07" required />
      </div>

      <div className="col-md-12">
        <label htmlFor="validationDefault08" className="form-label">
          Comentario
        </label>
        <textarea className="form-control" id="validationDefault08"></textarea>
      </div>
    </form>
  
  
          </Modal.Body>
          <Modal.Footer>
          <button className="btn btn-dark col-md-3">Agregar</button>
          </Modal.Footer>
        </Modal>
      );   
    } 

    {/*Otros Modales */}
    const [modalCInShow, setModalCInShow] = useState(false);
    const [modalCOutShow, setModalCOutShow] = useState(false);
    const [modalPagoShow, setModalPagoShow] = useState(false);
 
    const ModalCIn = (props) => {
       return (
          <Modal {...props} size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
             <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter" className="fw-semibold fs-4">
                   Realizar Check-In
                </Modal.Title>
             </Modal.Header>
             <Modal.Body>
               <form className="row g-3">
               <div className="col-md-6">
                 <label htmlFor="validationDefault01" className="form-label">
                   ID de Reservacion
                 </label>
                 <input type="number" className="form-control" id="validationDefault01" required />
               </div>
               <div className="col-md-6">
                 <label htmlFor="validationDefault05" className="form-label">
                   Estado de Check-In
                 </label>
                 <input
                   type="number"
                   className="form-control"
                   id="validationDefault05"
                   value="1"
                   readOnly
                   required
                 />
                 </div>
               </form>
             </Modal.Body>
             <Modal.Footer>
                <button className="btn btn-dark col-md-3">Agregar</button>
             </Modal.Footer>
          </Modal>
       );
    }
    const ModalCOut = (props) => {
       return (
          <Modal {...props} size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
             <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter" className="fw-semibold fs-4">
                   Realizar Check-Out
                </Modal.Title>
             </Modal.Header>
             <Modal.Body>
             <form className="row g-3">
               <div className="col-md-6">
                 <label htmlFor="validationDefault01" className="form-label">
                   ID de Reservacion
                 </label>
                 <input type="number" className="form-control" id="validationDefault01" required />
               </div>
               <div className="col-md-6">
                 <label htmlFor="validationDefault05" className="form-label">
                   Estado de Check-Out
                 </label>
                 <input
                   type="number"
                   className="form-control"
                   id="validationDefault05"
                   value="1"
                   readOnly
                   required
                 />
               </div>
             </form>
             </Modal.Body>
             <Modal.Footer>
                <button className="btn btn-dark col-md-3">Agregar</button>
             </Modal.Footer>
          </Modal>
       );
    }
    const ModalPago = (props) => {
       return (
          <Modal {...props} size="lg" aria-labelledby="contained-modal-title-vcenter" centered>
             <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter" className="fw-semibold fs-4">
                Realizar el 2do Pago
                </Modal.Title>
             </Modal.Header>
             <Modal.Body>
             <form className="row g-3">
               <div className="col-md-6">
                 <label htmlFor="validationDefault01" className="form-label">
                   ID de Reservacion
                 </label>
                 <input type="number" className="form-control" id="validationDefault01" required />
               </div>
 
               <div className="col-md-6">
                 <label htmlFor="validationDefault02" className="form-label">
                   Metodo de pago
                 </label>
                 <input type="text" className="form-control" id="validationDefault02" maxLength="200" required />
               </div>
 
               <div className="col-md-6">
                 <label htmlFor="validationDefault03" className="form-label">
                   Pago
                 </label>
                 <input type="number" className="form-control" id="validationDefault03" value="1" readOnly required />
               </div>
 
               <div className="col-md-6">
                 <label htmlFor="validationDefault05" className="form-label">
                   Fecha
                 </label>
                 <input type="date" className="form-control" id="validationDefault05" required />
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
   {/*<Clientes/>*/}
   <div className="row g-3" id="contenido">
      <div className="col-md-4">
        <h3 id="sub-titulo">Reservaciones</h3>
      </div>

      {/* Boton agregar, llama modal */}
      <div className="col-md-8">
      <button onClick={() => setModalShow(true)} className="btn" id="btn-add"> <FontAwesomeIcon icon={faFileCirclePlus} /> Agregar  Reservaciones</button>
        <ModalInsertar  show={modalShow} onHide={() => setModalShow(false)}/>
        <button onClick={() => setModalCInShow(true)} className="btn btn-outline-secondary btn-otros">
               <FontAwesomeIcon icon={faBuildingCircleCheck} /> Check-In
            </button>
            <ModalCIn show={modalCInShow} onHide={() => setModalCInShow(false)} />

            <button onClick={() => setModalPagoShow(true)} className="btn btn-secondary btn-otros">
               <FontAwesomeIcon icon={faSackDollar} /> 2do Pago
            </button>
            <ModalPago show={modalPagoShow} onHide={() => setModalPagoShow(false)} />

            <button onClick={() => setModalCOutShow(true)} className="btn btn-outline-secondary btn-otros">
               <FontAwesomeIcon icon={faBuildingCircleArrowRight} /> Check-Out
            </button>
            <ModalCOut show={modalCOutShow} onHide={() => setModalCOutShow(false)} />
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
              <th scope="col">ID Reservacion</th>
              <th scope="col">ID Cli.</th>
              <th scope="col">ID Hab.</th>
              <th scope="col">No. Huesped</th>
              <th scope="col">Fecha</th>
              <th scope="col">Fec. CheckIn</th>
              <th scope="col">Hora C-In</th>
              <th scope="col">Fec. CheckOut</th>
              <th scope="col">Hora C-Out</th>
              <th scope="col">CheckIn</th>
              <th scope="col">CheckOut</th>
              <th scope="col">Opciones</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <th scope="row">1</th>
              <td> - </td>
              <td>2</td>
              <td>2</td>
              <td>01/10/2023</td>
              <td>01/10/2023</td>
              <td>02:00</td>
              <td>01/10/2023</td>
              <td>02:00</td>
              <td>Yes</td>
              <td>No</td>
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
    </div>
    </>   
  )
}

export default reservacion