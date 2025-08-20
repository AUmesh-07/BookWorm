import './App.css'
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import Login from './components/AuthPage/Login';
import CategoriesPage from './pages/CategoriesPage';
import HomePage1 from './pages/HomePage1';
import MyLibraryPage from './pages/MyLibraryPage';
import MyShelfPage from './pages/MyShelfPage';
import ShoppingCartPage from './pages/ShoppingCardPage';
import Navbar from './components/Ui/Navbar';
import PrivateRoute from './routes/PrivateRoute';
import AboutUsPage from './pages/AboutUsPage';
import { CartProvider } from './context/CartContext';
import OrdersPage from './pages/OrdersPage'; // ✅ NEW: Import the OrdersPage
import Admin from './components/Admin/Admin';

function App() {
  return (
    <CartProvider>
    <BrowserRouter>
      <Navbar />
      <Routes>
        {/* Public Routes */}
        <Route path="/" element={<HomePage1 />} />
        <Route path="/CategoriesPage" element={<CategoriesPage />} />
        <Route path="/Login" element={<Login />} />
        <Route path="/Admin" element={<Admin />}/>
        
        {/* Protected Routes */}
        <Route path="/MyLibraryPage" element={<PrivateRoute><MyLibraryPage /></PrivateRoute>} />
        <Route path="/MyShelfPage" element={<PrivateRoute><MyShelfPage /></PrivateRoute>} />
        <Route path="/AboutUsPage" element={<PrivateRoute><AboutUsPage /></PrivateRoute>} />
        <Route path="/cart/:customerId" element={<PrivateRoute><ShoppingCartPage /></PrivateRoute>} />
        
        {/* ✅ NEW: Add the route for the Orders Page */}
        <Route path="/orders/:customerId" element={<PrivateRoute><OrdersPage /></PrivateRoute>} />
      </Routes>

      <ToastContainer
        position="top-center"
        autoClose={3000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="colored"
      />
    </BrowserRouter>
    </CartProvider>
  );
}

export default App;