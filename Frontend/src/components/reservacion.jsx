import React, { useState, useEffect } from "react";
import Navigation from "../components/Navigation";
import './css/style.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faAngleLeft, faAngleRight, faFileCirclePlus, faMagnifyingGlass} from '@fortawesome/free-solid-svg-icons';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import ButtonGroup from 'react-bootstrap/ButtonGroup';

function reservacion() {
  return (
    <>
    <Navigation/>
    <div>reservacion</div>
    </>
    
  )
}

export default reservacion