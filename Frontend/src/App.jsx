import React, { useState, useEffect } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Navigation from './components/Navigation';
import Cliente from './components/cliente';
import Home from './components/home';
import Category from './components/categoria';
import Room from './components/habitacion';
import Reservation from './components/reservacion';

function App() {
  return (
  <BrowserRouter>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/Cliente" element={<Cliente />} />
          <Route path="/Category" element={<Category />} />
          <Route path="/Room" element={<Room />} />
          <Route path="/Reservation" element={<Reservation />} />
        </Routes>
    </BrowserRouter>
  );
}

export default App;
