// src/components/ShelfBookCard.jsx

import React from 'react';
import './ShelfBookCard.css';

const ShelfBookCard = ({ book, isRented }) => {

  // Helper function to calculate remaining days
  const getRemainingDays = (expiryDate) => {
    const now = new Date();
    const expiry = new Date(expiryDate);
    const diffTime = expiry - now;
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    return diffDays > 0 ? diffDays : 0;
  };
  
  // Function to open the PDF in a new tab
  const handleReadClick = (e) => {
    e.stopPropagation();
    // Assuming your PDFs are in the public folder and named after the book ID
    // Corrected: Use backticks for the template literal
    const pdfPath = `/books/${book.id}.pdf`;
    window.open(pdfPath, '_blank');
  };

  return (
    <div className="card h-100 shelf-book-card shadow-sm">
      <img
        src={book.imageSource.replace("public", "")}
        className="card-img-top"
        alt={book.name}
      />
      <div className="card-body d-flex flex-column">
        <h5 className="card-title fw-bold text-truncate">{book.name}</h5>
        <p className="card-subtitle mb-2 text-muted text-truncate">by {book.author}</p>

        <div className="my-shelf-info mt-auto">
          {isRented ? (
            // Rented Book Info
            <>
              <p className="card-text small text-muted mb-1">
                Rented on: {new Date(book.rentalDate).toLocaleDateString()}
              </p>
              <p className="card-text small text-muted mb-1">
                Expires on: {new Date(book.rentalExpiryDate).toLocaleDateString()}
              </p>
              <p className="card-text small fw-bold text-danger">
                {getRemainingDays(book.rentalExpiryDate)} days remaining
              </p>
              <button
                className="btn btn-success w-100 mt-2"
                onClick={handleReadClick}
              >
                Read
              </button>
            </>
          ) : (
            // Purchased Book Info
            <>
              <p className="card-text small text-muted mb-1">
                Purchased on: {new Date(book.purchaseDate).toLocaleDateString()}
              </p>
              <button
                className="btn btn-success w-100 mt-2"
                onClick={handleReadClick}
              >
                View
              </button>
            </>
          )}
        </div>
      </div>
    </div>
  );
};

export default ShelfBookCard;
