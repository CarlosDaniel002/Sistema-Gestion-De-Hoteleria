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
import axios from "axios";

const URL = "https://pfg10itla-001-site1.gtempurl.com/Habitacion";

function Habitacion() {
  const [modalShow, setModalShow] = useState(false);
  const [habitaciones, setHabitaciones] = useState([]);
  const [habitacionSeleccionada, setHabitacionSeleccionada] = useState(null);
  const [paginaActual, setPaginaActual] = useState(0);
  const filasPorPagina = 5;
  const [formulario, setFormulario] = useState({
    idCategoria: 0,
    ubicacion: "",
    estado: "",
    comentarioH: "",
  });

  useEffect(() => {
    cargarHabitaciones();
    mostrarFilas();
  }, [paginaActual]);

  const cargarHabitaciones = async () => {
    try {
      const response = await axios.get(URL);
      setHabitaciones(response.data);
    } catch (error) {
      console.log("Error al cargar las habitaciones", error);
    }
  };

  const abrirModalInsertar = () => {
    setHabitacionSeleccionada(null);
    setFormulario({
      idCategoria: 0,
      ubicacion: "",
      estado: "",
      comentarioH: "",
    });
    setModalShow(true);
  };

  const agregarHabitacion = async (nuevaHabitacion) => {
    try {
      await axios.post(URL, nuevaHabitacion, {
        headers: { "Content-Type": "application/json" },
      });
      cargarHabitaciones();
    } catch (error) {
      console.error("Error al agregar una habitacion:", error);
    }
  };

  const editarHabitacion = async (habitacionEditada) => {
    try {
      await axios.put(URL, habitacionEditada, {
        headers: { "Content-Type": "application/json" },
      });
      cargarHabitaciones();
    } catch (error) {
      console.error("Error al editar una habitacion:", error);
    }
  };

  const eliminarHabitacion = async (idHabitacion) => {
    try {
      await axios.delete(URL, {
        data: { Id: idHabitacion, activar: 0 },
        headers: { "Content-Type": "application/json" },
      });
      cargarHabitaciones();
    } catch (error) {
      console.error("Error al eliminar habitacion:", error);
    }
  };

  const abrirModalEditar = (habitacion) => {
    setHabitacionSeleccionada(habitacion);
    setFormulario({
      idCategoria: habitacion.idCategoria,
      ubicacion: habitacion.ubicacion,
      estado: habitacion.estado,
      comentarioH: habitacion.comentarioH,
    });
    setModalShow(true);
  };

  const handleGuardar = () => {
    if (habitacionSeleccionada) {
      editarHabitacion({ ...habitacionSeleccionada, ...formulario });
    } else {
      agregarHabitacion(formulario);
    }
    setModalShow(false);
  };

  const filtrarHabitaciones = (filtro) => {
    const habitacionesFiltradas = habitaciones.filter(
      (habitacion) =>
        habitacion.estado.toLowerCase().includes(filtro.toLowerCase()) ||
        habitacion.idHabitacion.toString().includes(filtro)
    );
    setHabitaciones(habitacionesFiltradas);
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormulario((prevFormulario) => ({
      ...prevFormulario,
      [name]: value,
    }));
  };

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
            Agregar Habitacion
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form className="row g-3" onSubmit={(e) => e.preventDefault()}>
            <div className="col-md-6">
              <label htmlFor="validationDefault01" className="form-label">
                Tipo de Categoría
              </label>
              <select
                className="form-select"
                id="validationDefault01"
                required
                name="idCategoria"
                value={formulario.idCategoria}
                onChange={handleInputChange}
              >
                <option disabled value="">
                  Elige
                </option>
                <option value="1">Primeum</option>
                <option value="2">Primeum</option>
                <option value="3">Primeum</option>
              </select>
            </div>

            <div className="col-md-6">
              <label htmlFor="validationDefault02" className="form-label">
                Estado de habitación
              </label>
              <select
                className="form-select"
                id="validationDefault02"
                required
                name="estado"
                value={formulario.estado}
                onChange={handleInputChange}
              >
                <option  disabled value="">
                  Elige
                </option>
                <option value="Disponible">Disponible</option>
                <option value="Ocupada">Ocupada</option>
                <option value="Reservada">Reservada</option>
              </select>
            </div>

            <div className="col-md-12">
              <label htmlFor="validationDefault03" className="form-label">
                Ubicación de la habitación
              </label>
              <input
                type="text"
                className="form-control"
                id="validationDefault03"
                placeholder="Ala, piso, No."
                maxLength="255"
                required
                name="ubicacion"
                value={formulario.ubicacion}
                onChange={handleInputChange}
              />
            </div>

            <div className="col-md-12">
              <label htmlFor="validationDefault04" className="form-label">
                Comentario
              </label>
              <textarea
                className="form-control"
                id="validationDefault04"
                name="comentarioH"
                value={formulario.comentarioH}
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
    <>
      <Navigation />

      <div className="row g-3" id="contenido">
        <div className="col-md-9">
          <h3 id="sub-titulo">Habitaciones</h3>
        </div>

        <div className="col-md-3">
          <button
            onClick={() => {
              setModalShow(true), abrirModalInsertar();
            }}
            className="btn"
            id="btn-add"
          >
            <FontAwesomeIcon icon={faFileCirclePlus} /> Agregar Habitaciones
          </button>
          <ModalInsertar show={modalShow} onHide={() => setModalShow(false)} />
        </div>

        <div className="col-md-12">
          <div className="input-group">
            <input
              type="text"
              className="form-control"
              placeholder="Escribir aqui..."
              onChange={(e) => setFormulario({ ubicacion: e.target.value })}
            />
            <button
              className="btn btn-secondary"
              type="button"
              onClick={() => filtrarHabitaciones(formulario.ubicacion)}
            >
              <FontAwesomeIcon icon={faMagnifyingGlass} />
            </button>
          </div>
        </div>

        <div className="col-md-12">
          <div className="card table-responsive" style={{ height: "204.5px" }}>
            <table
              className="table table-hover align-middle table-sm"
              id="PTabla"
              style={{ marginBottom: "0px" }}
            >
              <thead className="table-light">
                <tr>
                  <th scope="col">ID Habitación</th>
                  <th scope="col">Categoria</th>
                  <th scope="col">Ubicación</th>
                  <th scope="col">Estado</th>
                  <th scope="col">Comentario</th>
                  <th scope="col">Opciones</th>
                </tr>
              </thead>
              <tbody>
                {habitaciones.map((habi) => (
                  <tr key={habi.idHabitacion}>
                    <th scope="row">{habi.idHabitacion}</th>
                    <td>{habi.idCategoria}</td>
                    <td>{habi.ubicacion}</td>
                    <td>{habi.estado}</td>
                    <td>{habi.comentarioH}</td>
                    <td>
                      <button
                        type="button"
                        className="btn btn-primary btn-sm"
                        onClick={() => abrirModalEditar(habi)}
                      >
                        <FontAwesomeIcon icon={faPenToSquare} />
                      </button>
                      <button
                        type="button"
                        className="btn btn-danger btn-sm"
                        onClick={() => eliminarHabitacion(habi.idHabitacion)}
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
    </>
  );
}

export default Habitacion;
