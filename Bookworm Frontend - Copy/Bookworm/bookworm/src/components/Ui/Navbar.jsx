import { useNavigate, Link, useLocation } from 'react-router-dom';
import React from "react";
// ✅ NEW: Import the new icon
import { FaSearch, FaUser, FaShoppingCart, FaFileInvoiceDollar } from "react-icons/fa";
import { useAuth } from '../../context/AuthContext';
import BookLoader from "../BookLoader";
import "./Navbar.css";
import { useCart } from '../../context/CartContext';

function Navbar() {
  const navigate = useNavigate();
  const location = useLocation();
  const isCategoriesPage = location.pathname === '/CategoriesPage';
  const { cartCount } = useCart();
  const isCartPage = location.pathname.startsWith('/cart');
  const { isAuthenticated, logout } = useAuth(); 

  const userString = localStorage.getItem('user');
  let userObject = null;
  if (userString) {
    try {
      userObject = JSON.parse(userString);
    } catch (error) {
      console.error("Failed to parse user data from localStorage:", error);
    }
  }

  const customerId = userObject?.id;

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  return (
    <nav className="navbar navbar-expand-lg bg-white py-3 shadow-sm px-4">
      <div className="container-fluid d-flex align-items-center justify-content-between">
        {/* Brand */}
        <div className="d-flex align-items-center">
          <Link
            to="/"
            className="d-flex align-items-center text-decoration-none"
          >
            <div
              className="loader me-2"
              style={{ width: "40px", height: "40px" }}
            >
              <BookLoader />
            </div>
            <span className="fw-bold fs-4 text-pink">Bookworm</span>
          </Link>
        </div>

        {/* Search Bar... */}
        {!isCategoriesPage && ( 
        <div className="flex-grow-1 mx-4">
          <div className="input-group">
            <span className="input-group-text bg-light border-0">
              <FaSearch />
            </span>
            <input
              type="text"
              className="form-control bg-light border-0"
              placeholder="Search books, audiobooks, music..."
            />
          </div>
        </div>)}

        {/* Right Side Menu */}
        <div className="d-flex align-items-center gap-4">
          <Link to="/CategoriesPage" className="text-dark fw-medium text-decoration-none">
            Categories
          </Link>

          {isAuthenticated && (
            <>
              <Link to="/MyShelfPage" className="text-dark fw-medium text-decoration-none">
                My Shelf
              </Link>
              
              {/* ✅ NEW: Link to the Orders Page */}
              <Link to={`/orders/${customerId}`} className="text-dark fw-medium text-decoration-none">
                Orders
              </Link>

              <Link
                to={customerId ? `/cart/${customerId}` : '/login'}
                className="btn border border-pink rounded-3 position-relative"
              >
                <FaShoppingCart className="text-pink" />
                  {cartCount > 0 && (
                    <span className="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger">
                      {cartCount}
                      <span className="visually-hidden">cart items</span>
                    </span>
                )}
              </Link>
            </>
          )}

          {/* Auth Actions */}
          {isAuthenticated ? (
            <div className="d-flex align-items-center gap-3">
              <span className="fw-bold text-pink">
                Hello, {userObject?.name || "User"}
              </span>
              <button
                className="btn text-danger fw-semibold"
                onClick={handleLogout}
                style={{ background: "none", border: "none" }}
              >
                <FaUser className="me-1" /> Logout
              </button>
            </div>
          ) : (
            <Link to="/Login" className="text-dark d-flex align-items-center text-decoration-none">
              <FaUser className="me-1" /> Sign In
            </Link>
          )}
        </div>
      </div>
    </nav>
  );
}

export default Navbar;