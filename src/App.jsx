import { BrowserRouter, Routes, Route } from "react-router-dom";
import "./App.css";
import Home from "./pages/Home.jsx";
import Navbar from "./components/NavBar";
import Login from "./components/Login";
import Register from "./components/Register";
//import ProductDetail from "./pages/ProductDetail.jsx";
//import ShoppingCart from "./pages/ShoppingCart.jsx";

function App() {
  return (
    <>
      <BrowserRouter>
        <Navbar />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          {/* <Route path="/product/:id" element={<ProductDetail />} /> 
          <Route path="/cart" element={<ShoppingCart />} /> */}
        </Routes>
      </BrowserRouter>
    </>
  );
}

export default App;
