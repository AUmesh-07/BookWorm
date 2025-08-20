// CartItemCard.jsx
import React from 'react';
import './CartItemCard.css';

// Component for a single item in the cart
// The component now receives an 'isRented' prop to determine the item's status.
const CartItemCard = ({ item, isRented, onRemove }) => {
  // 1. Create a placeholder image URL, similar to BookCard.
  //    The text is encoded to ensure the URL is valid.
  const placeholderImage = `https://placehold.co/100x150/9c27b0/white?text=${encodeURIComponent(item.title)}`;

  // 2. Use the item's image if it exists, otherwise use the placeholder.
  const imageUrl = item.image || placeholderImage;

  return (
    <div className="cart-item card p-3 mb-3">
      <div className="row g-0 align-items-center">
        <div className="col-auto me-3">
          <img
            src={imageUrl}
            className="cart-item-img rounded"
            alt={item.title}
            // 3. Add an onError handler to catch broken image links and show the placeholder instead.
            onError={(e) => {
              e.target.onerror = null; // Prevents infinite loops if the placeholder also fails
              e.target.src = placeholderImage;
            }}
          />
        </div>
        <div className="col">
          <h6 className="fw-bold mb-0">{item.title}</h6>
          <p className="text-muted mb-1">{item.author}</p>
          {/* Replaced the quantity selector with text based on isRented prop */}
          <p className="fw-bold mb-2">
            {isRented ? 'Rent' : 'Purchase'}
          </p>
          <div className="d-flex align-items-center">
            {/* The quantity selector has been removed as requested. */}
            <button className="btn btn-link text-muted p-0" onClick={() => onRemove(item.id)}>
              <i className="bi bi-trash me-1"></i> Remove
            </button>
          </div>
        </div>
        <div className="col-auto fw-bold text-end">
          {/* Price is still calculated by quantity, as that seems to be the intended behavior. */}
          ₹{(item.price * item.quantity).toFixed(2)}
        </div>
      </div>
    </div>
  );
};

export default CartItemCard;