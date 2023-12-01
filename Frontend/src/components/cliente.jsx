import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {useForm} from 'react-hook-form'
import Navigation from './Navigation';
const API_URL = "https://pfg10itla-001-site1.gtempurl.com/Clientes";
import 'bootstrap/dist/css/bootstrap.min.css';
const Cliente = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    reset,
  } = useForm({
    defaultValues: {
      idCliente: 0,
      nombre: '',
      apellido: '',
      telefono: '',
      correo: '',
      activo: 0,
    },
  });

  const [clientes, setClientes] = useState([]);

  const cargarClientes = async () => {
    try {
      const response = await axios.get(API_URL);
      setClientes(response.data);
    } catch (error) {
      console.error('Error al cargar clientes:', error);
    }
  };

  const handleGuardar = async (data) => {
    try {
      if (data.idCliente === 0) {
        await axios.post(API_URL, data, { headers: { 'Content-Type': 'application/json' } });
      } else {
        await axios.put(API_URL, data, { headers: { 'Content-Type': 'application/json' } });
      }

      cargarClientes();
      reset(); 
    } catch (error) {
      console.error('Error al guardar el cliente:', error);
    }
  };

  const handleEditar = (cliente) => {
    setValue('idCliente', cliente.idCliente);
    setValue('nombre', cliente.nombre);
    setValue('apellido', cliente.apellido);
    setValue('telefono', cliente.telefono);
    setValue('correo', cliente.correo);
  };

  const handleEliminar = async (idCliente) => {
    try {
      await axios.delete(API_URL, {data: {id: idCliente, activar: 0,},});
      cargarClientes();
      reset();
    } catch (error) {
      if (error.response.status === 404) {
        console.error('El cliente con idCliente', idCliente, 'no fue encontrado.');
      } else {
      console.error('Error al eliminar el cliente:', error);
      }
    }
  };

  useEffect(() => {
    cargarClientes();
  }, []);

  return (
    <div>
      <Navigation />
      <h1>Formulario de Clientes</h1>
      <form onSubmit={handleSubmit(handleGuardar)}>
        <div>
          <label>Nombre:</label>
          <input
            type="text"
            {...register("nombre", {
              required: "Nombre es requerido",
              maxLength: { value: 20, message: "Nombre no debe ser mayor a 20 caracteres" },
              minLength: { value: 2, message: "Nombre debe ser mayor a 2 caracteres" },
            })}
          />
          {errors.nombre && <span>{errors.nombre.message}</span>}
        </div>

        <div>
          <label>Apellido:</label>
          <input
            type="text"
            {...register("apellido", {
              required: "Apellido es requerido",
              maxLength: { value: 20, message: "Apellido no debe ser mayor a 20 caracteres" },
              minLength: { value: 2, message: "Apellido debe ser mayor a 2 caracteres" },
            })}
          />
          {errors.apellido && <span>{errors.apellido.message}</span>}
        </div>

        <div>
          <label>Teléfono:</label>
          <input
          type="text" // Usar "text" para permitir caracteres no numéricos
          {...register('telefono', {
            required: 'Teléfono es requerido',
            pattern: {
              value: "^\+(?:[0-9] ?){6,14}[0-9]$",
              message: 'Por favor, ingrese un número de teléfono válido',
            },
          })}
        />
          {errors.telefono && <span>{errors.telefono.message}</span>}
        </div>

        <div>
          <label>Correo:</label>
          <input
            type="email"
            {...register("correo", {
              required: "Correo es requerido",
              pattern: {
                value: /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/,
                message: "Correo no válido",
              },
            })}
          />
          {errors.correo && <span>{errors.correo.message}</span>}
        </div>

        <button type="submit">Guardar</button>
        <button type="button" onClick={() => reset()}>Cancelar</button>
      </form>

      <div>
        <h2>Clientes Registrados</h2>
        <ul className="list-group">
          {clientes &&
            clientes.length &&
            clientes.map((cliente) => (
              <li key={cliente.idCliente} className="list-group-item d-flex justify-content-between align-items-center">
                {cliente.nombre} {cliente.apellido} - {cliente.telefono} - {cliente.correo} {cliente.activo}
                <div>
                  <button
                    type="button"
                    className="btn btn-primary mx-1"
                    onClick={() => handleEditar(cliente)}
                  >
                    Editar
                  </button>
                  <button
                    type="button"
                    className="btn btn-danger mx-1"
                    onClick={() => handleEliminar(cliente.idCliente)}
                  >
                    Eliminar
                  </button>
                </div>
              </li>
            ))}
        </ul>
      </div>
    </div>
  );
};

export default Cliente;
