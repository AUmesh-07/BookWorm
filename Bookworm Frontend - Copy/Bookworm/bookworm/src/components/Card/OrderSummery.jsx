import React, { useState } from 'react';
import './OrderSummery.css';

// We add `onCheckout` and `isCheckingOut` to the props
const OrderSummary = ({ subtotal, discount, estimatedTax, onPromoApply, onCheckout, isCheckingOut }) => {
  const [promoCode, setPromoCode] = useState('');
  const total = subtotal - discount + estimatedTax;

  return (
    <div className="order-summary card p-4">
      <h5 className="fw-bold mb-4">Order Summary</h5>
      <div className="d-flex justify-content-between mb-2">
        <span>Subtotal</span>
        <span>₹{subtotal.toFixed(2)}</span>
      </div>
      <div className="d-flex justify-content-between mb-2">
        <span>Discount</span>
        <span className="text-danger">-₹{discount.toFixed(2)}</span>
      </div>
      {/* <div className="d-flex justify-content-between mb-4 border-bottom pb-3">
        <span>Estimated Tax</span>
        <span>₹{estimatedTax.toFixed(2)}</span>
      </div> */}
      <div className="d-flex justify-content-between fw-bold fs-5 mb-4">
        <span>Total</span>
        <span>₹{total.toFixed(2)}</span>
      </div>
      <div className="mb-4">
        <div className="input-group">
          <input
            type="text"
            className="form-control"
            placeholder="Enter code"
            value={promoCode}
            onChange={(e) => setPromoCode(e.target.value)}
          />
          <button className="btn btn-outline-secondary" onClick={() => onPromoApply(promoCode)}>
            Apply
          </button>
        </div>
      </div>

      {/*
        This button now calls the onCheckout function passed in props.
        It is disabled and shows a "Processing..." message when isCheckingOut is true.
      */}
      <button
        className="btn btn-pink btn-lg fw-bold mb-3"
        onClick={onCheckout}
        disabled={isCheckingOut}
      >
        <i className="bi bi-bag-check me-2"></i>
        {isCheckingOut ? 'Processing...' : 'Checkout'}
      </button>

      <p className="text-muted text-center small">
        By checking out, you agree to our <a href="#">Terms & Conditions</a>
      </p>
    </div>
  );
};

export default OrderSummary;
