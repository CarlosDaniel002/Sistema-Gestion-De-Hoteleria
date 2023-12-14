
{/*
npm install

npm i --save @fortawesome/fontawesome-svg-core
npm i --save @fortawesome/free-solid-svg-icons
npm i --save @fortawesome/free-regular-svg-icons
npm i --save @fortawesome/free-brands-svg-icons
npm i --save @fortawesome/react-fontawesome@latest


npm install --save react-bootstrap bootstrap
npm install react-bootstrap bootstrap
npm install react-bootstrap*/}

import React, { useState, useEffect } from "react";
import Navigation from "../components/Navigation";
import './css/style.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleLeft, faAngleRight, faFileCirclePlus, faMagnifyingGlass, faPenToSquare, faTrashCan} from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import ButtonGroup from 'react-bootstrap/ButtonGroup';

const Category = () => {

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
           Agregar Categoría
           </Modal.Title>
         </Modal.Header>
         <Modal.Body>
 
 
         <form className="row g-3">
               <div className="col-md-8">
                 <label htmlFor="validationDefault01" className="form-label">Nombre de Categoría</label>
                 <input type="text" className="form-control" id="validationDefault01" placeholder="Escriba la nueva categoria" max="100" required />
               </div>
 
               <div className="col-md-4">
                 <label htmlFor="validationDefault02" className="form-label">Cantidad max huésped</label>
                 <input type="number" className="form-control" id="validationDefault02" placeholder="Del 1 al 6" max="6" min="1" required />
               </div>
 
               <div className="col-md-4">
                 <label htmlFor="validationDefault03" className="form-label">Costo de 1ra Cuota</label>
                 <input type="number" className="form-control" id="validationDefault03" placeholder="$0.00" min="1" required />
               </div>
 
               <div className="col-md-4">
                 <label htmlFor="validationDefault04" className="form-label">Costo de 2da Cuota</label>
                 <input type="number" className="form-control" id="validationDefault04" placeholder="$0.00" min="1" required />
               </div>
 
               <div className="col-md-4">
                 <label htmlFor="validationDefault05" className="form-label">Precio</label>
                 <input type="number" className="form-control" id="validationDefault05" placeholder="$0.00" min="1" required />
               </div>
 
               <div className="col-md-12">
                 <label htmlFor="validationDefault06" className="form-label">Comentario</label>
                 <textarea className="form-control" id="validationDefault06"></textarea>
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
  return(
  <div>
    <Navigation/>
    <div className="row g-3" id="contenido">
      <div className="col-md-9">
        <h3 id="sub-titulo">Categorías</h3>
      </div>


      {/* Boton agregar, llama modal */}
      <div className="col-md-3">
      <button onClick={() => setModalShow(true)} className="btn" id="btn-add"><FontAwesomeIcon icon={faFileCirclePlus} /> Agregar Categorías</button>
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

      {/* Tabla */}
      <div className="col-md-12">
        <div className="card table-responsive" style={{ height: '204.5px' }}>
        <table className="table table-hover align-middle table-sm" id="PTabla" style={{ marginBottom: '0px' }}>
          <thead className="table-light">
            <tr>
              <th scope="col">ID Categoría</th>
              <th scope="col">Categoría</th>
              <th scope="col">Cantidad huésped</th>
              <th scope="col">1ra Cuota</th>
              <th scope="col">2da Cuota</th>
              <th scope="col">Precio</th>
              <th scope="col">Comentario</th>
              <th scope="col">Opciones</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <th scope="row">1</th>
              <td>Primeum</td>
              <td>3</td>
              <td>$0.00</td>
              <td>$0.00</td>
              <td>$0.00</td>
              <td>Nada</td>
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
              <th scope="row">1</th>
              <td>Primeum</td>
              <td>3</td>
              <td>$0.00</td>
              <td>$0.00</td>
              <td>$0.00</td>
              <td>Nada</td>
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
  </div>);
}

export default Category