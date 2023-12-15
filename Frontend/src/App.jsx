import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Navigation from './components/Navigation';
import Cliente from './components/clientes';
import Home from './components/home';
import Category from './components/categoria';
import Room from './components/habitacion';
import Reservation from './components/reservacion';
import NewUser from "./components/newuser";
import NewPass from "./components/newpassword";
import Log_in from "./components/Login";
import { UserProvider } from './components/UserContext';
function App() {
  return (
    <BrowserRouter>
    <UserProvider>
      <div>
        
        
        {/* Las rutas para tus componentes */}
        <Routes>
          <Route path="/" element={<Log_in />} />
          <Route path="/Inicio" element={<Home />} />
          <Route path="/Cliente" element={<Cliente />} />
          <Route path="/Categoria" element={<Category />} />
          <Route path="/Habitacion" element={<Room />} />
          <Route path="/Reservacion" element={<Reservation />} />          
          <Route path="/NuevoUsuario" element={<NewUser />} />
          <Route path="/CambiarContrasena" element={<NewPass />} />
          
        </Routes>
      </div>
      </UserProvider>
    </BrowserRouter>
  );
}

export default App;

