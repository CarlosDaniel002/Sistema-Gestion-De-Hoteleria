import React, { Component } from 'react'
import { Link } from 'react-router-dom'
import Logo from '../img/NavLogo.png'
export default class Navigation extends Component {
  render() {
    return (
      <nav className="navbar">
        <Link to="/" className="logo">
        <img className='logo1' src={Logo} alt="Logo del Hotel" />
        </Link>
        <ul className='menu'>
          <li>
            <Link className="menu-link" to="/">
              Inicio
            </Link>
          </li>
          <li>
            <Link className="menu-link" to="/Cliente">
              Cliente
            </Link>
          </li>
          <li>
            <Link className="menu-link" to="/Category">
              Categoria
            </Link>
          </li>
          <li>
            <Link className="menu-link" to="/Room">
              Habitacion
            </Link>
          </li>
          <li>
            <Link className="menu-link" to="/Reservation">
              Reservacion
            </Link>
          </li>
        </ul>
      </nav>
    );
  }
}
