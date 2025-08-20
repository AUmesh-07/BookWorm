// src/pages/AddToCartModal.jsx

import React, { useState, useEffect } from 'react';
import { toast } from 'react-toastify';
import './AddToCartModal.css';

const AddToCartModal = ({ book, mode, onClose, onConfirm }) => {
  const [rentDays, setRentDays] = useState(book?.minRentDays || 7);

  // Reset rentDays if the book or mode changes
  useEffect(() => {
    setRentDays(book?.minRentDays || 7);
  }, [book, mode]);

  if (!book) return null;

  const handleSubmit = (e) => {
    e.preventDefault();

    if (mode === 'rent' && rentDays <= 0) {
      toast.error('Please enter a valid number of days for rent.'); // ✅ Error toast
      return;
    }

    // Pass the data with a fixed quantity of 1
    onConfirm({
      quantity: 1,
      rentNumberOfDays: mode === 'rent' ? rentDays : 0,
    });

    onClose(); // ✅ Close modal right after adding to cart
  };

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header">
          <h4 className="fw-bold">{`Add "${book.name}" to Cart`}</h4>
          <button onClick={onClose} className="close-button">&times;</button>
        </div>
        <div className="modal-body">
          <form onSubmit={handleSubmit}>
            {mode === 'rent' && (
              <div className="form-group">
                <label htmlFor="rentDays">Number of Days for Rent</label>
                <input
                  id="rentDays"
                  type="number"
                  className="form-control"
                  value={rentDays}
                  onChange={(e) =>
                    setRentDays(Math.max(1, parseInt(e.target.value, 10)))
                  }
                  min={book.minRentDays || 1}
                />
                {book.rentPricePerDay && (
                  <small className="form-text text-muted">
                    Total Rent: ₹{(rentDays * book.rentPricePerDay).toFixed(2)}
                  </small>
                )}
              </div>
            )}
            <div className="modal-footer">
              <button type="button" className="btn btn-secondary" onClick={onClose}>
                Cancel
              </button>
              <button type="submit" className="btn btn-primary">
                Add to Cart
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default AddToCartModal;
