import React, { useState, useEffect } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Navigation from './components/Navigation';
import Cliente from './components/cliente';
import Home from './components/home';

function App() {
  return (
  <BrowserRouter>
        <Routes>
          <Route path="/" element={<Navigation />} />
          <Route path="/Home" element={<Home />} />
          <Route path="/Cliente" element={<Cliente />} />
          {/* Add more routes as needed */}
        </Routes>
    </BrowserRouter>
  );
}

export default App;
