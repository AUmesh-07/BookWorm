import React from 'react';
import './ShelfCard.css';

// A placeholder image for items that don't have one
const placeholderImage = 'https://via.placeholder.com/150x220.png?text=No+Cover';

const ShelfCard = ({ item }) => {
  // IMPROVED: Destructuring with default values to prevent errors
  const {
    productName = 'Untitled',
    author = 'Unknown Author',
    imageSource,
    rentalDate,
    rentalExpiryDate,
    purchaseDate,
    quantity = 0,
    price = 0.00,
    type,
  } = item;

  // Calculate rental progress based on dates
  const calculateRentalProgress = () => {
    if (!rentalDate || !rentalExpiryDate) return 0;
    const start = new Date(rentalDate);
    const end = new Date(rentalExpiryDate);
    const now = new Date();
    const totalDuration = end - start;
    const elapsed = now - start;
    if (totalDuration <= 0) return 100; // If expired or invalid duration
    return Math.min(100, Math.max(0, (elapsed / totalDuration) * 100));
  };

  // Format dates for display
  const formatDate = (dateString) => {
    if (!dateString) return 'N/A';
    return new Date(dateString).toLocaleDateString();
  };

  // Render content based on item type
  const renderContent = () => {
    switch (type) {
      case 'Rented':
        return (
          <>
            <div className="progress mb-2" style={{ height: '5px' }}>
              <div
                className="progress-bar bg-pink"
                role="progressbar"
                style={{ width: `${calculateRentalProgress()}%` }}
                aria-valuenow={calculateRentalProgress()}
                aria-valuemin="0"
                aria-valuemax="100"
              ></div>
            </div>
            <p className="text-muted mb-3 progress-text">
              Rented: {formatDate(rentalDate)} | Expires: {formatDate(rentalExpiryDate)}
            </p>
            <div className="d-flex align-items-center">
              <button className="btn btn-sm btn-link text-pink text-decoration-none p-0 me-3">
                <i className="bi bi-book me-1"></i> Read
              </button>
              <button className="btn btn-sm btn-link text-muted p-0">
                <i className="bi bi-trash"></i>
              </button>
            </div>
          </>
        );
      case 'Purchased':
        return (
          <>
            <p className="text-muted mb-3 progress-text">
              Purchased: {formatDate(purchaseDate)} | Quantity: {quantity} | Price: ₹{price?.toFixed(2)}
            </p>
            <div className="d-flex align-items-center">
              <button className="btn btn-sm btn-link text-purple text-decoration-none p-0 me-3">
                <i className="bi bi-play-circle me-1"></i> Play
              </button>
              <button className="btn btn-sm btn-link text-muted p-0">
                <i className="bi bi-trash"></i>
              </button>
            </div>
          </>
        );
      default:
        return null;
    }
  };

  return (
    <div className="shelf-card card mb-3">
      <div className="row g-0">
        <div className="col-auto">
          <img 
            src={imageSource || placeholderImage} 
            className="shelf-card-img rounded-start" 
            alt={`${productName} cover`} 
            // Add an error handler for broken image links
            onError={(e) => { e.target.onerror = null; e.target.src = placeholderImage; }}
          />
        </div>
        <div className="col">
          <div className="card-body">
            <h6 className="card-title fw-bold mb-1">{productName}</h6>
            <p className="card-text text-muted author-text">{author}</p>
            {renderContent()}
          </div>
        </div>
      </div>
    </div>
  );
};

export default ShelfCard;