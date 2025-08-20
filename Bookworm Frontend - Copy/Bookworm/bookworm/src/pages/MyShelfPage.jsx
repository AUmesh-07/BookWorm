// src/pages/MyShelfPage.jsx

import React, { useState, useEffect } from "react";
import axios from "axios";
import ShelfBookCard from "../components/ShelfBookCard";
import "./MyShelfPage.css";
import Footer from "../components/Footer";

const axiosInstance = axios.create({
  baseURL: 'https://localhost:7261/api/',
});

axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      // Corrected: Use backticks for the template literal
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

const MyShelfPage = () => {
  const [activeView, setActiveView] = useState('rented');
  const [books, setBooks] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  const fetchBooks = async (view) => {
    const userStr = localStorage.getItem('user');
    const user = userStr ? JSON.parse(userStr) : null;

    if (!user || !user.id) {
      setError("Please log in to view your shelf.");
      setBooks([]);
      return;
    }

    setIsLoading(true);
    setError(null);
    setBooks([]);

    let endpoint = '';
    if (view === 'rented') {
      // Corrected: Use backticks for the template literal
      endpoint = `/user-collection/${user.id}/library`;
    } else if (view === 'purchased') {
      // Corrected: Use backticks for the template literal
      endpoint = `/user-collection/${user.id}/shelf`;
    }

    try {
      const response = await axiosInstance.get(endpoint);
      const fetchedItems = response.data.items || [];

      const formattedBooks = fetchedItems.map(item => ({
        id: item.productId,
        name: item.productName,
        author: item.author,
        imageSource: item.imageSource,
        isRented: view === 'rented',
        rentalDate: item.rentalDate,
        rentalExpiryDate: item.rentalExpiryDate,
        purchaseDate: item.purchaseDate,
      }));

      setBooks(formattedBooks);

    } catch (err) {
      // Corrected: Use backticks for the template literal
      console.error(`Failed to fetch ${view} books:`, err);
      // Corrected: Use backticks for the template literal
      setError(`Failed to load your ${view} items.`);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchBooks(activeView);
  }, [activeView]);

  return (
    <div className="my-shelf container ">
      <h1 className="fw-bold">My Shelf</h1>
      <p className="text-muted">Access your Purchased and Rented content</p>

      <div className="d-flex justify-content-start mb-4">
        <button
          // Corrected: Use backticks for the template literal
          className={`btn me-2 ${activeView === 'rented' ? 'btn-primary' : 'btn-outline-primary'}`}
          onClick={() => setActiveView('rented')}
        >
          Rented
        </button>
        <button
          // Corrected: Use backticks for the template literal
          className={`btn ${activeView === 'purchased' ? 'btn-primary' : 'btn-outline-primary'}`}
          onClick={() => setActiveView('purchased')}
        >
          Purchased
        </button>
      </div>

      {isLoading && (
        <div className="text-center mt-5">
          <p>Loading your {activeView} items...</p>
        </div>
      )}

      {error && (
        <div className="alert alert-danger mt-5">{error}</div>
      )}

      {!isLoading && !error && (
        <>
          {books.length > 0 ? (
            <div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 row-cols-xl-5 g-4">
              {books.map((book, index) => (
                <div key={index} className="col">
                  <ShelfBookCard
                    book={book}
                    isRented={book.isRented}
                  />
                </div>
              ))}
            </div>
          ) : (
            <div className="text-center mt-5 p-5">
              <p className="text-muted fs-4">
                You have no {activeView} items.
              </p>
            </div>
          )}
        </>
      )}
      <Footer />
    </div>
  );
};

export default MyShelfPage;
