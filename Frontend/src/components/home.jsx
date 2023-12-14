import React from "react";
import Navigation from "../components/Navigation";
import clientes from "./clientesp";
const home = () => {
  return(
  <div>
    <Navigation/>
    <p>Home</p>
    <clientes />
  </div>);
}

export default home