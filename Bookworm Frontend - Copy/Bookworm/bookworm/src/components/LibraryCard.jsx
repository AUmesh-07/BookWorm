import React from 'react';
import moment from 'moment';
import PropTypes from 'prop-types';
import './LibraryCard.css';

const LibraryCard = ({ item, activeTab }) => {
  // Destructure item prop for easier access
  const { title, author, type, image, dueDate, status } = item;

  // Calculate days left with validation to prevent errors with invalid dates
  const daysLeft = dueDate && moment(dueDate, 'MMM D, YYYY', true).isValid()
    ? moment(dueDate, 'MMM D, YYYY').diff(moment(), 'days')
    : null;

  // A helper function to render the status badge based on due date and status
  const renderStatusBadge = () => {
    // If a date is provided, check if it's overdue based on the calculated days
    if (daysLeft !== null && daysLeft < 0) {
      return <span className="badge bg-danger rounded-pill">Overdue</span>;
    }

    // Otherwise, use the status provided in the item data
    switch (status) {
      case 'overdue':
        // This case is for when daysLeft is not available but the status is explicitly 'overdue'
        return <span className="badge bg-danger rounded-pill">Overdue</span>;
      case 'expired':
        return <span className="badge bg-danger rounded-pill">Expired</span>;
      case 'active':
        // Display 'days left' if a due date is present and valid
        if (daysLeft !== null && daysLeft >= 0) {
          return <span className="badge bg-success rounded-pill">{daysLeft} days left</span>;
        }
        // Fallback to a simple 'Active' badge
        return <span className="badge bg-success rounded-pill">Active</span>;
      default:
        return null;
    }
  };

  // A helper function to render action buttons based on content type and status
  const renderButtons = () => {
    if (type === 'Ebook') {
      return (
        <>
          <button className="btn btn-pink me-2">Read Now</button>
          <button className="btn btn-outline-secondary me-2">Return Early</button>
          {status === 'overdue' && (
            <button className="btn btn-outline-secondary me-2">Renew Loan</button>
          )}
        </>
      );
    } else if (type === 'Audiobook' || type === 'Music') {
      return (
        <>
          <button className="btn btn-pink me-2">Listen Now</button>
          {status === 'active' && (
            <button className="btn btn-outline-secondary me-2">Extend Rental</button>
          )}
        </>
      );
    }
    return null;
  };

  return (
    <div className="library-card card mb-3 p-3">
      <div className="row g-0 align-items-center">
        <div className="col-auto">
          <img
            src={image}
            className="library-card-img rounded"
            alt={`${title} by ${author}`}
          />
        </div>
        <div className="col ms-3">
          <div className="card-body p-0">
            <h5 className="card-title fw-bold mb-1">{title}</h5>
            <p className="card-text text-muted mb-1">{author}</p>
            <span className={`badge ${type === 'Ebook' ? 'bg-primary' : type === 'Audiobook' ? 'bg-secondary' : 'bg-success'} my-2`}>
              {type}
            </span>
            <div className="mt-3">
              {renderButtons()}
            </div>
          </div>
        </div>
        <div className="col-auto text-end d-flex flex-column align-items-end">
          <div className="mb-2">
            <p className="text-muted mb-1">
              <i className="bi bi-calendar me-1"></i>
              {activeTab === 'rented' ? 'Rental expires:' : 'Due date:'}
              <span className="fw-bold"> {dueDate}</span>
            </p>
            {renderStatusBadge()}
          </div>
          {/* Show the 'Return by' text only for active Ebooks */}
          {status === 'active' && type === 'Ebook' && (
            <p className="return-by text-primary mt-2">Return by {dueDate}</p>
          )}
        </div>
      </div>
    </div>
  );
};

// PropTypes for validation
LibraryCard.propTypes = {
  item: PropTypes.shape({
    title: PropTypes.string.isRequired,
    author: PropTypes.string.isRequired,
    type: PropTypes.oneOf(['Ebook', 'Audiobook', 'Music']).isRequired,
    image: PropTypes.string.isRequired,
    dueDate: PropTypes.string,
    status: PropTypes.oneOf(['overdue', 'expired', 'active']).isRequired,
  }).isRequired,
  activeTab: PropTypes.oneOf(['rented', 'borrowed']).isRequired,
};

export default LibraryCard;