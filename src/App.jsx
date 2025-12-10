import './App.css';
import { Routes, Route, Link } from 'react-router-dom'; // Importálva
import Customers from './Customers'; // Feltöltve
import Products from './Products'; // Placeholder komponens
import Cart from './Cart'; // Placeholder komponens

function App() {
  return (
    <div className="App">
      {/* Navigációs sáv a Link komponensekkel */}
      <nav style={{ padding: '10px', borderBottom: '2px solid #333', backgroundColor: '#f4f4f4' }}>
        <Link to="/" style={{ margin: '0 15px', textDecoration: 'none', fontWeight: 'bold' }}>Kezdőlap</Link>
        <Link to="/customers" style={{ margin: '0 15px', textDecoration: 'none', fontWeight: 'bold' }}>Vevők</Link>
        <Link to="/products" style={{ margin: '0 15px', textDecoration: 'none', fontWeight: 'bold' }}>Termékek</Link>
        <Link to="/cart" style={{ margin: '0 15px', textDecoration: 'none', fontWeight: 'bold' }}>Kosár</Link>
      </nav>

      <main style={{ padding: '20px' }}>
        {/* Útvonalak definiálása */}
        <Routes>
          <Route path="/" element={<h1>Üdvözöljük a Webáruház Admin Felületén!</h1>} />
          <Route path="/customers" element={<Customers />} />
          <Route path="/products" element={<Products />} />
          <Route path="/cart" element={<Cart />} />
        </Routes>
      </main>
    </div>
  );
}

export default App;