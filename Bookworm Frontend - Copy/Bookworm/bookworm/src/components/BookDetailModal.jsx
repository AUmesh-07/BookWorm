// src/components/BookDetailModal.jsx
import React from 'react';
import './BookDetailModal.css'; // optional for styling

const BookDetailModal = ({ book, onClose }) => {
  if (!book) return null;

  const placeholderImage = `https://placehold.co/400x600/2196f3/ffffff?text=${encodeURIComponent(book.name)}`;
  const imageUrl = book.imageSource || placeholderImage;

  return (
    <div className="modal-backdrop">
      <div className="modal-content container p-4 shadow-lg bg-white rounded-4">
        <button className="btn-close float-end" onClick={onClose}></button>
        <div className="row">
          <div className="col-md-4">
            <img src={imageUrl} className="img-fluid rounded" alt={book.name} />
          </div>
          <div className="col-md-8">
            <h2 className="fw-bold">{book.name}</h2>
            <p className="text-muted">{book.author}</p>
            <p><strong>Genre:</strong> {book.genreName}</p>
            <p><strong>Language:</strong> {book.languageName}</p>
            <p><strong>ISBN:</strong> {book.isbn}</p>
            <p><strong>Description:</strong> {book.longDescription}</p>
            <p><strong>Price:</strong> ₹{book.offerPrice?.toFixed(2)}</p>
            {book.rentable && (
              <p><strong>Rent:</strong> ₹{book.rentPerDay}/day (Min {book.minRentDays} days)</p>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default BookDetailModal;