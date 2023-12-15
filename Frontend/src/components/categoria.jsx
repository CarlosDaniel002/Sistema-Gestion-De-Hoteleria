import React, { useState, useEffect } from "react";
import Navigation from "../components/Navigation";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faAngleLeft,
  faAngleRight,
  faFileCirclePlus,
  faMagnifyingGlass,
  faPenToSquare,
  faTrashCan,
} from "@fortawesome/free-solid-svg-icons";
import Button from "react-bootstrap/Button";
import ButtonGroup from "react-bootstrap/ButtonGroup";
import "./css/style.css";

const URL = "https://pfg10itla-001-site1.gtempurl.com/Categorias";

const Category = () => {
  const [categorias, setCategorias] = useState([]);
  const [categoriaSeleccionada, setCategoriaSeleccionada] = useState(null);
  const [paginaActual, setPaginaActual] = useState(0);
  const [filtro, setFiltro] = useState("");
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

  const cargarCategorias = async () => {
    try {
      const response = await fetch(URL);
      const data = await response.json();
      setCategorias(data);
    } catch (error) {
      console.error("Error al cargar categorías:", error);
    }
  };

  const abrirModalInsertar = () => {
    setCategoriaSeleccionada(null);
    limpiarFormulario();
    cargarCategorias();
  };

  const agregarCategoria = async (nuevaCategoria) => {
    try {
      const response = await fetch(URL, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(nuevaCategoria),
      });

      const data = await response.json();
      console.log("Respuesta al agregar categoría:", data);
      cargarCategorias();
    } catch (error) {
      console.error("Error al agregar categoría:", error);
    }
  };

  const editarCategoria = async (categoriaEditada) => {
    try {
      const response = await fetch(URL, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(categoriaEditada),
      });

      const data = await response.json();
      console.log("Respuesta al editar categoría:", data);
      cargarCategorias();
    } catch (error) {
      console.error("Error al editar categoría:", error);
    }
  };

  const eliminarCategoria = async (idCategoria) => {
    try {
      const response = await fetch(URL, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ Id: idCategoria }),
      });

      const data = await response.json();
      console.log("Respuesta al eliminar categoría:", data);
      cargarCategorias();
    } catch (error) {
      console.error("Error al eliminar categoría:", error);
    }
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
  };

  const handleGuardar = (e) => {
    e.preventDefault();
    if (categoriaSeleccionada) {
      editarCategoria({ ...categoriaSeleccionada, ...formulario });
    } else {
      agregarCategoria(formulario);
    }
    abrirModalInsertar();
  };

  const filtrarCategorias = (filtro) => {
    const categoriasFiltradas = categorias.filter(
      (categoria) =>
        categoria.nombre.toLowerCase().includes(filtro.toLowerCase()) ||
        categoria.idCategoria.toString().includes(filtro)
    );
    setCategorias(categoriasFiltradas);
  };

  const mostrarFilas = () => {
    const filas = document
      .getElementById("PTabla")
      .getElementsByTagName("tbody")[0].rows;

    for (let i = 0; i < filas.length; i++) {
      filas[i].style.display = i >= paginaActual * filasPorPagina && i < (paginaActual + 1) * filasPorPagina ? "" : "none";
    }

    mostrarNumeroDeSeccion();
  };

  const limpiarFormulario = () => {
    setFormulario({
      nombre: "",
      cantidadMax: 0,
      cuota1: 0,
      cuota2: 0,
      precio: 0,
      comentarioC: "",
    });
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
      seccionBtn.textContent = `Pag. ${paginaActual + 1}`;
    }
  };

  return (
    <div>
      <Navigation />
      <div className="row g-3" id="contenido">
        <div className="col-md-9">
          <h3 id="sub-titulo">Categorías</h3>
        </div>

        {/* Boton agregar */}
        <div className="col-md-3">
          <button
            onClick={abrirModalInsertar}
            className="btn"
            id="btn-add"
          >
            <FontAwesomeIcon icon={faFileCirclePlus} /> Limpiar
          </button>
        </div>

        {/* Buscador */}
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
              onClick={() => filtrarCategorias(filtro)}
            >
              <FontAwesomeIcon icon={faMagnifyingGlass} />
            </button>
          </div>
        </div>

        {/* Formulario */}
        <div className="col-md-12">
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

            <div className="col-md-12">
              <Button className="btn btn-dark col-md-3" type="submit" variant="primary">
                Agregar
              </Button>
            </div>
          </form>
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
                      <Button
                        type="button"
                        variant="primary"
                        size="sm"
                        onClick={() => abrirModalEditar(categoria)}
                      >
                        <FontAwesomeIcon icon={faPenToSquare} />
                      </Button>
                      <Button
                        type="button"
                        variant="danger"
                        size="sm"
                        onClick={() => eliminarCategoria(categoria.idCategoria)}
                      >
                        <FontAwesomeIcon icon={faTrashCan} />
                      </Button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          <div className="col-md-12">
            <ButtonGroup role="group" id="btn-tabla">
              <Button
                type="button"
                variant="secondary"
                size="sm"
                onClick={irHaciaAtras}
              >
                <FontAwesomeIcon icon={faAngleLeft} />
              </Button>
              <Button
                type="button"
                variant="outline-secondary"
                size="sm"
                id="seccionBtn"
              >
                -
              </Button>
              <Button
                type="button"
                variant="secondary"
                size="sm"
                onClick={irHaciaAdelante}
              >
                <FontAwesomeIcon icon={faAngleRight} />
              </Button>
            </ButtonGroup>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Category;
