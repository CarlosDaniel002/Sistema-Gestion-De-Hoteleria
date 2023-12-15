import React, { useState, useEffect } from "react";
import Navigation from "./Navigation";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faAngleLeft,
  faAngleRight,
  faFileCirclePlus,
  faMagnifyingGlass,
  faPenToSquare,
  faTrashCan,
} from "@fortawesome/free-solid-svg-icons";
import axios from "axios";
import { useForm } from "react-hook-form";
import "bootstrap/dist/css/bootstrap.min.css";

const API_URL = "https://pfg10itla-001-site1.gtempurl.com/Clientes";

function Clientes() {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    reset,
  } = useForm({
    defaultValues: {
      idCliente: 0,
      nombre: "",
      apellido: "",
      telefono: "",
      correo: "",
      activo: 1,
    },
  });

  const [clientes, setClientes] = useState([]);
  const [clienteSeleccionado, setClienteSeleccionado] = useState(null);
  const [paginaActual, setPaginaActual] = useState(0);
  const [filtro, setFiltro] = useState("");
  const filasPorPagina = 5;

  useEffect(() => {
    cargarClientes();
    mostrarFilas();
  }, [paginaActual]);

  const cargarClientes = async () => {
    try {
      const response = await axios.get(API_URL);
      setClientes(response.data);
    } catch (error) {
      console.error("Error al cargar los clientes", error);
    }
  };

  const abrirModalInsertar = () => {
    setClienteSeleccionado(null);
    limpiarFormulario();
    cargarClientes();
  };

  const handleGuardar = async (data) => {
    try {
      if (data.idCliente === 0) {
        await axios.post(API_URL, data, {
          headers: { "Content-Type": "application/json" },
        });
      } else {
        await axios.put(API_URL, data, {
          headers: { "Content-Type": "application/json" },
        });
      }

      cargarClientes();
      reset();
      abrirModalInsertar();
    } catch (error) {
      console.error("Error al guardar el cliente:", error);
    }
  };

  const handleEditar = (cliente) => {
    setClienteSeleccionado(cliente);
    setValue("idCliente", cliente.idCliente);
    setValue("nombre", cliente.nombre);
    setValue("apellido", cliente.apellido);
    setValue("telefono", cliente.telefono);
    setValue("correo", cliente.correo);
  };

  const handleEliminar = async (idCliente) => {
    try {
      console.log("Eliminando cliente con idCliente:", idCliente);
  
      const response = await fetch(API_URL, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ id: idCliente, activar: 0 }), 
      });
  
      console.log("Respuesta del servidor:", response);
  
      if (response.ok) {
        const data = await response.json();
        console.log("Datos recibidos del servidor:", data);
        cargarClientes();
        reset();
      } else {
        console.error("Error al eliminar el cliente. Código de estado:", response.status);
        const errorData = await response.json();
        console.error("Detalles del error:", errorData);
      }
    } catch (error) {
      console.error("Error al eliminar el cliente:", error);
    }
  };
  
  const filtrarCliente = (filtro) => {
    const clientesFiltrados = clientes.filter(
      (cliente) =>
        cliente.nombre.toLowerCase().includes(filtro.toLowerCase()) ||
        cliente.idCliente.toString().includes(filtro)
    );
    setClientes(clientesFiltrados);
  };
  
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setClienteSeleccionado((prevCliente) => ({
      ...prevCliente,
      [name]: value,
    }));
  };

  const limpiarFormulario = () => {
    setClienteSeleccionado(null);
    reset();
  };

  const mostrarFilas = () => {
    const filas = document
      .getElementById("PTabla")
      .getElementsByTagName("tbody")[0].rows;

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

    if (paginaActual < Math.ceil(filas.length / filasPorPagina) - 1) {
      setPaginaActual((prevPagina) => prevPagina + 1);
    }
  };

  const mostrarNumeroDeSeccion = () => {
    const seccionBtn = document.getElementById("seccionBtn");
    if (seccionBtn) {
      seccionBtn.textContent = "Pag. " + (paginaActual + 1);
    }
  };

  return (
    <>
      <Navigation />
      <div className="row g-3" id="contenido">
        <div className="col-md-9">
          <h3 id="sub-titulo">Clientes</h3>
        </div>

        <div className="col-md-3">
          <button
            onClick={() => abrirModalInsertar()}
            className="btn"
            id="btn-add"
          >
            <FontAwesomeIcon icon={faFileCirclePlus} /> limpiar
          </button>
        </div>

        <div className="col-md-12">
          <div className="input-group">
            <input
              type="text"
              className="form-control"
              name="lupa"
              placeholder="Escribir aquí..."
              value={filtro}
              onChange={(e) => setFiltro(e.target.value)}
            />
            <button
              className="btn btn-secondary"
              type="button"
              onClick={() => filtrarCliente(filtro)}
            >
              <FontAwesomeIcon icon={faMagnifyingGlass} />
            </button>
          </div>
        </div>

        <form className="row g-3" onSubmit={handleSubmit(handleGuardar)}>
          <div className="col-md-6">
            <label htmlFor="nombre" className="form-label">
              Nombres
            </label>
            <input
              type="text"
              className="form-control"
              id="nombre"
              maxLength="100"
              name="nombre"
              {...register("nombre", {
                required: "Nombre es requerido",
                maxLength: {
                  value: 100,
                  message: "Nombre no debe ser mayor a 100 caracteres",
                },
              })}
              onChange={(e) => handleInputChange(e)}
              value={clienteSeleccionado ? clienteSeleccionado.nombre : ""}
            />
            {errors.nombre && (
              <span className="text-danger">{errors.nombre.message}</span>
            )}
          </div>

          <div className="col-md-6">
            <label htmlFor="apellido" className="form-label">
              Apellidos
            </label>
            <input
              type="text"
              className="form-control"
              id="apellido"
              maxLength="100"
              name="apellido"
              {...register("apellido", {
                required: "Apellido es requerido",
                maxLength: {
                  value: 100,
                  message: "Apellido no debe ser mayor a 100 caracteres",
                },
              })}
              onChange={(e) => handleInputChange(e)}
              value={clienteSeleccionado ? clienteSeleccionado.apellido : ""}
            />
            {errors.apellido && (
              <span className="text-danger">{errors.apellido.message}</span>
            )}
          </div>

          <div className="col-md-6">
            <label htmlFor="telefono" className="form-label">
              Teléfono
            </label>
            <input
              type="tel"
              className="form-control"
              id="telefono"
              placeholder="809 934 4434"
              maxLength="12"
              name="telefono"
              {...register("telefono", {
                required: "Teléfono es requerido",
                pattern: {
                  value: "^[0-9]*$",
                  message: "Por favor, ingrese un número de teléfono válido",
                },
              })}
              onChange={(e) => handleInputChange(e)}
              value={clienteSeleccionado ? clienteSeleccionado.telefono : ""}
            />
            {errors.telefono && (
              <span className="text-danger">{errors.telefono.message}</span>
            )}
          </div>

          <div className="col-md-6">
            <label htmlFor="correo" className="form-label">
              Correo
            </label>
            <input
              type="email"
              className="form-control"
              id="correo"
              placeholder="correo@gmail.com"
              maxLength="50"
              name="correo"
              {...register("correo", {
                required: "Correo es requerido",
                pattern: {
                  value: /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/,
                  message: "Correo no válido",
                },
              })}
              onChange={(e) => handleInputChange(e)}
              value={clienteSeleccionado ? clienteSeleccionado.correo : ""}
            />
            {errors.correo && (
              <span className="text-danger">{errors.correo.message}</span>
            )}
          </div>

          <div className="col-md-12">
            <button
              className="btn btn-dark col-md-3"
              type="submit"
            >
              Agregar
            </button>
          </div>
        </form>

        <div className="col-md-12">
          <div className="card table-responsive" style={{ height: "204.5px" }}>
            <table
              className="table table-hover align-middle table-sm"
              id="PTabla"
              style={{ marginBottom: "0px" }}
            >
              <thead className="table-light">
                <tr>
                  <th scope="col">idCliente</th>
                  <th scope="col">Nombre</th>
                  <th scope="col">Apellido</th>
                  <th scope="col">Telefono</th>
                  <th scope="col">Correo</th>
                  <th scope="col">Opciones</th>
                </tr>
              </thead>
              <tbody>
                {clientes.length > 0 ? (
                  clientes.map((cliente) => (
                    <tr key={cliente.idCliente}>
                      <th scope="row">{cliente.idCliente}</th>
                      <td>{cliente.nombre}</td>
                      <td>{cliente.apellido}</td>
                      <td>{cliente.telefono}</td>
                      <td>{cliente.correo}</td>
                      <td>
                        <button
                          type="button"
                          className="btn btn-primary btn-sm"
                          onClick={() => handleEditar(cliente)}
                        >
                          <FontAwesomeIcon icon={faPenToSquare} />
                        </button>
                        <button
                          type="button"
                          className="btn btn-danger btn-sm"
                          onClick={() => handleEliminar(cliente.idCliente)}
                        >
                          <FontAwesomeIcon icon={faTrashCan} />
                        </button>
                      </td>
                    </tr>
                  ))
                ) : (
                  <tr>
                    <td colSpan="6">No data available</td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
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
    </>
  );
}

export default Clientes;
