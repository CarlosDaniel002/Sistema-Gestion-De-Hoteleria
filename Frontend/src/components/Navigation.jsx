import React from 'react';
import { Link } from 'react-router-dom';
import login from '../img/logo.png';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faClipboard, faUser, faBed, faTag,faGear, faUserPlus, faLock, faArrowRightFromBracket} from '@fortawesome/free-solid-svg-icons';

const Navigation = () => {
  return (
    <nav className="navbar bg-secundary navbar-expand-lg">
      <div className="container-fluid" id="NavPadding">
        <Link to="/" className="navbar-brand p-1" id="logo">
          <img src={login} alt="Logo" className="logo-img" />
        </Link>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navmenu"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navmenu">
          <div className="navbar-nav">
            <Link to="/Reservation" className="nav-link" id="SubMenu">
            <FontAwesomeIcon icon={faClipboard} />{' '} Reservaciones
            </Link>
            <Link to="/Cliente" className="nav-link" id="SubMenu">
            <FontAwesomeIcon icon={faUser} /> {' '}Clientes
            </Link>
            <Link to="/Room" className="nav-link" id="SubMenu">
            <FontAwesomeIcon icon={faBed} /> {' '}Habitaciones
            </Link>
            <Link to="/Category" className="nav-link" id="SubMenu">
            <FontAwesomeIcon icon={faTag} /> {' '}Categorías
            </Link>
            <div className="nav-link" id="herramienta">
              <div className="dropdown">
                <Link
                  to="#"
                  className="d-block link-body-emphasis text-decoration-none"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  <FontAwesomeIcon icon={faGear} />
                </Link>
                <ul className="dropdown-menu">
                  <li className="dropdown-item"> Nombre del usuario</li>
                  <li>
                    <hr className="dropdown-divider " />
                  </li>
                  <li>
                    <Link to="/newuser" className="dropdown-item text-start">
                      {' '}
                      <FontAwesomeIcon icon={faUserPlus} />{' '}Nuevo Usuario
                    </Link>
                  </li>
                  <li>
                    <Link
                      to="/newpassword"
                      className="dropdown-item text-start"
                    >
                      <FontAwesomeIcon icon={faLock} />{' '} Cambiar Contraseña
                    </Link>
                  </li>
                  <li>
                    <hr className="dropdown-divider" />
                  </li>
                  <li>
                    <Link to="login.html" className="dropdown-item text-start">
                    <FontAwesomeIcon icon={faArrowRightFromBracket} />{' '}
                      Cerrar Sesion{' '}
                    </Link>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Navigation;
