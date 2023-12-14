{
  /*
    npm install
    npm i --save @fortawesome/fontawesome-svg-core
    npm i --save @fortawesome/free-solid-svg-icons
    npm i --save @fortawesome/free-regular-svg-icons
    npm i --save @fortawesome/free-brands-svg-icons
    npm i --save @fortawesome/react-fontawesome@latest
    npm install --save react-bootstrap bootstrap
    npm install react-bootstrap bootstrap
    npm install react-bootstrap
  */
}

import React, { useState, useEffect } from "react";
import Navigation from "../components/Navigation";
import "./css/style.css";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faAngleLeft,
  faAngleRight,
  faFileCirclePlus,
  faMagnifyingGlass,
  faPenToSquare,
  faTrashCan,
} from "@fortawesome/free-solid-svg-icons";
import Modal from "react-bootstrap/Modal";
import Button from "react-bootstrap/Button";
import ButtonGroup from "react-bootstrap/ButtonGroup";

const URL = "https://pfg10itla-001-site1.gtempurl.com/Categorias";

const Category = () => {
  {
    /* Del Modal*/
  }
  const [categorias, setCategorias] = useState([]);
  const [categoriaSeleccionada, setCategoriaSeleccionada] = useState(null);
  const [modalShow, setModalShow] = useState(false);
  const [paginaActual, setPaginaActual] = useState(0);
  const filasPorPagina = 5;
  const [formulario, setFormulario] = useState({
    nombre: "",
    cantidadMax: 0,
    cuota1: 0,
    cuota2: 0,
    precio: 0,
    comentarioC: "",
  });

  useEffect(() => {
    cargarCategorias();
    mostrarFilas();
  }, [paginaActual]);

  const cargarCategorias = () => {
    fetch(URL)
      .then((response) => response.json())
      .then((data) => setCategorias(data))
      .catch((error) => console.error("Error al cargar categorías:", error));
  };

  const abrirModalInsertar = () => {
    setCategoriaSeleccionada(null);
    setFormulario({
      nombre: "",
      cantidadMax: 0,
      cuota1: 0,
      cuota2: 0,
      precio: 0,
      comentarioC: "",
    });
    setModalShow(true);
  };

  const agregarCategoria = (nuevaCategoria) => {
    fetch(URL, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(nuevaCategoria),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log("Respuesta al agregar categoría:", data);
        cargarCategorias();
      })
      .catch((error) => console.error("Error al agregar categoría:", error));
  };

  const editarCategoria = (categoriaEditada) => {
    fetch(URL, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(categoriaEditada),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log("Respuesta al editar categoría:", data);
        cargarCategorias();
      })
      .catch((error) => console.error("Error al editar categoría:", error));
  };

  const eliminarCategoria = (idCategoria) => {
    fetch(URL, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ Id: idCategoria }),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log("Respuesta al eliminar categoría:", data);
        cargarCategorias();
      })
      .catch((error) => console.error("Error al eliminar categoría:", error));
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormulario((prevFormulario) => ({
      ...prevFormulario,
      [name]: value,
    }));
  };

  const abrirModalEditar = (categoria) => {
    setCategoriaSeleccionada(categoria);
    setFormulario({
      nombre: categoria.nombre,
      cantidadMax: categoria.cantidadMax,
      cuota1: categoria.cuota1,
      cuota2: categoria.cuota2,
      precio: categoria.precio,
      comentarioC: categoria.comentarioC,
    });
    setModalShow(true);
  };

  const handleGuardar = (e) => {
    e.preventDefault()    
    if (categoriaSeleccionada) {
    
      editarCategoria({ ...categoriaSeleccionada, ...formulario });
    } else {
      agregarCategoria(formulario);
    }

    setModalShow(false);
  };

  const filtrarCategorias = (filtro) => {
    const categoriasFiltradas = categorias.filter(
      (categoria) =>
        categoria.nombre.toLowerCase().includes(filtro.toLowerCase()) ||
        categoria.idCategoria.toString().includes(filtro)
    );
      setCategorias(categoriasFiltradas);
  };
  {
    /* De La Tabla */
  }
  const mostrarFilas = () => {
    const filas = document
      .getElementById("PTabla")
      .getElementsByTagName("tbody")[0].rows;
    const totalPaginas = Math.ceil(filas.length / filasPorPagina);

    for (let i = 0; i < filas.length; i++) {
      filas[i].style.display = "none";
    }

    for (
      let i = paginaActual * filasPorPagina;
      i < (paginaActual + 1) * filasPorPagina;
      i++
    ) {
      if (filas[i]) {
        filas[i].style.display = "";
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
    const filas = document
      .getElementById("PTabla")
      .getElementsByTagName("tbody")[0].rows;
    const totalPaginas = Math.ceil(filas.length / filasPorPagina);

    if (paginaActual < totalPaginas - 1) {
      setPaginaActual((prevPagina) => prevPagina + 1);
    }
  };

  const mostrarNumeroDeSeccion = () => {
    const seccionBtn = document.getElementById("seccionBtn");
    if (seccionBtn) {
      seccionBtn.textContent = "Pag. " + (paginaActual + 1);
    }
  };

  const ModalInsertar = (props) => {
    return (
      <Modal
        {...props}
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
      >
        <Modal.Header closeButton>
          <Modal.Title
            id="contained-modal-title-vcenter"
            className="fw-semibold fs-4"
          >
            Agregar Categoría
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form className="row g-3" onSubmit={handleGuardar}>
            <div className="col-md-8">
              <label htmlFor="validationDefault01" className="form-label">
                Nombre de Categoría
              </label>
              <input
                type="text"
                className="form-control"
                id="validationDefault01"
                placeholder="Escriba la nueva categoria"
                max="100"
                required
                name="nombre"
                value={formulario.nombre}
                onChange={handleInputChange}
              />
            </div>

            <div className="col-md-4">
              <label htmlFor="validationDefault02" className="form-label">
                Cantidad max huésped
              </label>
              <input
                type="number"
                className="form-control"
                id="validationDefault02"
                placeholder="Del 1 al 6"
                max="6"
                min="1"
                required
                name="cantidadMax"
                value={formulario.cantidadMax}
                onChange={handleInputChange}
              />
            </div>

            <div className="col-md-4">
              <label htmlFor="validationDefault03" className="form-label">
                Costo de 1ra Cuota
              </label>
              <input
                type="number"
                className="form-control"
                id="validationDefault03"
                placeholder="$0.00"
                min="1"
                required
                name="cuota1"
                value={formulario.cuota1}
                onChange={handleInputChange}
              />
            </div>

            <div className="col-md-4">
              <label htmlFor="validationDefault04" className="form-label">
                Costo de 2da Cuota
              </label>
              <input
                type="number"
                className="form-control"
                id="validationDefault04"
                placeholder="$0.00"
                min="1"
                required
                name="cuota2"
                value={formulario.cuota2}
                onChange={handleInputChange}
              />
            </div>

            <div className="col-md-4">
              <label htmlFor="validationDefault05" className="form-label">
                Precio
              </label>
              <input
                type="number"
                className="form-control"
                id="validationDefault05"
                placeholder="$0.00"
                min="1"
                required
                name="precio"
                value={formulario.precio}
                onChange={handleInputChange}
              />
            </div>

            <div className="col-md-12">
              <label htmlFor="validationDefault06" className="form-label">
                Comentario
              </label>
              <textarea
                className="form-control"
                id="validationDefault06"
                name="comentarioC"
                value={formulario.comentarioC}
                onChange={handleInputChange}
              ></textarea>
            </div>
          </form>
        </Modal.Body>
        <Modal.Footer>
          <button className="btn btn-dark col-md-3" onClick={handleGuardar}>
            Agregar
          </button>
        </Modal.Footer>
      </Modal>
    );
  };
  
  return (
    <div>
      <Navigation />
      <div className="row g-3" id="contenido">
        <div className="col-md-9">
          <h3 id="sub-titulo">Categorías</h3>
        </div>

        {/* Boton agregar, llama modal */}
        <div className="col-md-3">
          <button
            onClick={() => {
              setModalShow(true), abrirModalInsertar();
            }}
            className="btn"
            id="btn-add"
          >
            <FontAwesomeIcon icon={faFileCirclePlus} /> Agregar Categorías
          </button>
          <ModalInsertar show={modalShow} onHide={() => setModalShow(false)} />
        </div>

        {/* Buscador */}
        <div className="col-md-12">
          <div className="input-group">
            <input
              type="text"
              className="form-control"
              placeholder="Escribir aqui..."
              onChange={(e) => setFormulario({ nombre: e.target.value })}
            />
            <button className="btn btn-secondary" type="button"onClick={() => filtrarCategorias(formulario.nombre)}>
              <FontAwesomeIcon icon={faMagnifyingGlass} />
            </button>
          </div>
        </div>

        {/* Tabla */}
        <div className="col-md-12">
          <div className="card table-responsive" style={{ height: "204.5px" }}>
            <table
              className="table table-hover align-middle table-sm"
              id="PTabla"
              style={{ marginBottom: "0px" }}
            >
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
                {categorias.map((categoria) => (
                  <tr key={categoria.idCategoria}>
                    <th scope="row">{categoria.idCategoria}</th>
                    <td>{categoria.nombre}</td>
                    <td>{categoria.cantidadMax}</td>
                    <td>${categoria.cuota1}</td>
                    <td>${categoria.cuota2}</td>
                    <td>${categoria.precio}</td>
                    <td>{categoria.comentarioC}</td>
                    <td>
                      <button
                        type="button"
                        className="btn btn-primary btn-sm"
                        onClick={() => abrirModalEditar(categoria)}
                      >
                        <FontAwesomeIcon icon={faPenToSquare} />
                      </button>
                      <button
                        type="button"
                        className="btn btn-danger btn-sm"
                        onClick={() => eliminarCategoria(categoria.idCategoria)}
                      >
                        <FontAwesomeIcon icon={faTrashCan} />
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          <div className="col-md-12">
            <div className="btn-group" role="group" id="btn-tabla">
              <button
                type="button"
                className="btn btn-secondary btn-sm"
                onClick={irHaciaAtras}
              >
                <FontAwesomeIcon icon={faAngleLeft} />
              </button>
              <button
                type="button"
                className="btn btn-outline-secondary btn-sm"
                id="seccionBtn"
              >
                -
              </button>
              <button
                type="button"
                className="btn btn-secondary btn-sm"
                onClick={irHaciaAdelante}
              >
                <FontAwesomeIcon icon={faAngleRight} />
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Category;