import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { BrowserRouter } from 'react-router-dom'; // Hozzáadva

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter> {/* Hozzáadva */}
      <App />
    </BrowserRouter> {/* Hozzáadva */}
  </StrictMode>,
)